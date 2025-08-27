using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading;
using Simple;
using Simple.Objects;
using Simple.Modeling;

namespace Simple.Objects
{
	public class ObjectCodeGenerator
	{
		private const string strGenerated = "Generated";
		private static Dictionary<Type, HashSet<string>> implementedPropertyNamesByObjectType = new Dictionary<Type, HashSet<string>>();
		private static Dictionary<Type, IEnumerable<Type>> objectSubclassesByObjectType = new Dictionary<Type, IEnumerable<Type>>();
		public static void Generate(SimpleObjectModelDiscovery modelDiscovery, bool includeSystemObjects, SendOrPostCallback progressReporter)
		{
			//IDictionary<int, ISimpleObjectModel> objectModelsByTableId = manager.GetObjectModelDictionary();
			IDictionary<Type, ISimpleObjectModel> objectModelsByObjectType = modelDiscovery.GetObjectModelsByOriginObjectType();
			List<Type> excludedObjectTypes = new List<Type>() { typeof(object) }; //, typeof(SimpleObject) };
			List<Type> processedTypes = new List<Type>();

			progressReporter(String.Empty);
			ClearFolderCache();

			foreach (ISimpleObjectModel masterObjectModel in objectModelsByObjectType.Values)
			{
				if (!includeSystemObjects && masterObjectModel.ObjectType.Assembly == typeof(SimpleObject).Assembly)
				{
					progressReporter(String.Format("       {0}  --> System object, skipped...\r\n", masterObjectModel.ObjectType.FullName));
					
					continue;
				}

				////Debug Mode
				//if (masterObjectModel.ObjectType.Name != "Device")
				//	continue;

				////Debug Mode
				//if (masterObjectModel.ObjectType.Name != "SecondaryIpAddress")
				//	continue;


				Dictionary<string, PropertyModelInfo> usedPropertyModelInfos = new Dictionary<string, PropertyModelInfo>();
				List<string> abstractPropertyNames = new List<string>();
				string additionalTextInfo = String.Empty;
				//IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies(); //.ToList();
				//IEnumerable<Type> assemblyTypes = ReflectionHelper.CollectTypesInAssemblies(assemblies);
				//IEnumerable<Type> inheritedObjectTypes = ReflectionHelper.SelectTypes(assemblyTypes, type => type == masterObjectModel.ObjectType || masterObjectModel.ObjectType.IsSubclassOf(type) && !type.IsInterface && !type.IsGenericType);
				//inheritedObjectTypes = ReflectionHelper.SortTypesByTopInheritance(inheritedObjectTypes);
				var inheritedObjectTypes = ReflectionHelper.SelectAssemblySubTypesOf(AppDomain.CurrentDomain.GetAssemblies(), masterObjectModel.ObjectType);
				List<Type> objectSubclasses = new List<Type>();

				//objectInheritedTypes.Add(masterObjectModel.ObjectType);

				//if (objectModel.ObjectType.Name == "Network") // IsSubclassOf(typeof(Administrator)))
				//	usedPropertyNames = usedPropertyNames;

				foreach (Type objectType in inheritedObjectTypes)
				{
					if (excludedObjectTypes.Contains(objectType))
						continue;

					objectSubclasses.Add(objectType);
					objectSubclassesByObjectType[objectType] = objectSubclasses;

					List<IPropertyModel> propertyModelsToImplement = new List<IPropertyModel>();
					Dictionary<string, PropertyModelInfo> newUsedPropertyModelInfos = new Dictionary<string, PropertyModelInfo>();
					List<string> includePropertyNames = new List<string>();

					// Fetch attribute IncludeProperty from object class ... [IncludeProperty(string[] properties)]
					foreach (IncludePropertyAttribute includePropertyAttribute in objectType.GetCustomAttributes(typeof(IncludePropertyAttribute), inherit: false))
					{
						includePropertyNames.AddRange(includePropertyAttribute.PropertyNames);

						foreach (string propertyName in includePropertyAttribute.PropertyNames)
						{
							PropertyModelInfo? derivedTypeWithImplementedProperty;

							if (usedPropertyModelInfos.TryGetValue(propertyName, out derivedTypeWithImplementedProperty))
							{
								additionalTextInfo += String.Format("       The derived class already implements {0} property '{1}' - property is not implemented.\r\n", derivedTypeWithImplementedProperty.PropertyType.Name, propertyName);
							}
							else
							{
								IPropertyModel propertyModel = masterObjectModel.PropertyModels[propertyName];

								if (propertyModel != null)
								{
									propertyModelsToImplement.Add(propertyModel);
									newUsedPropertyModelInfos.Add(propertyName, new PropertyModelInfo(objectType, propertyModel.PropertyType, propertyModel.FieldType));
								}
								else
								{
									additionalTextInfo += String.Format("       Requested property '{0}' to include in object class {1} is not defined in the {2} object model definition.\r\n", propertyName, objectType.Name, masterObjectModel.Name);
								}
							}
						}
					}

					if (objectType == masterObjectModel.ObjectType)
					{
						foreach (IPropertyModel propertyModel in masterObjectModel.PropertyModels)
							if (!usedPropertyModelInfos.Keys.Contains(propertyModel.PropertyName) && !newUsedPropertyModelInfos.ContainsKey(propertyModel.PropertyName))
								newUsedPropertyModelInfos.Add(propertyModel.PropertyName, new PropertyModelInfo(objectType, propertyModel.PropertyType, propertyModel.FieldType));

						foreach (PropertyModel propertyModel in masterObjectModel.PropertyModels)
							if (propertyModelsToImplement.FirstOrDefault(item => item.PropertyName == propertyModel.PropertyName) == null) // && propertyModel.TableIdAsRelationKeyPropertyModel == null) // Skip implementing relation TableId fields
								propertyModelsToImplement.Add(propertyModel);
					}
					else if (!objectModelsByObjectType.ContainsKey(objectType)) // This object type will be or is processed in objectModelsByObjectModelDefinitionType foreach loop
					{
						//else // Find already implemented property fields
						//{
						FieldInfo[] fieldInfos = objectType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
						PropertyInfo[] propertyInfos = objectType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

						foreach (IPropertyModel propertyModel in masterObjectModel.PropertyModels)
						{
							if (includePropertyNames.Contains(propertyModel.PropertyName))
								continue; // Required properties enforced with IncludeProperty should be implemented, no matter if already exists

							if (!usedPropertyModelInfos.ContainsKey(propertyModel.PropertyName))
							{
								string fieldName = Char.ToLowerInvariant(propertyModel.PropertyName[0]).ToString();

								if (propertyModel.PropertyName.Length > 1)
									fieldName += propertyModel.PropertyName.Substring(1);

								FieldInfo? fieldInfo = fieldInfos.FirstOrDefault(item => item.Name == fieldName);

								if (fieldInfo != null)
									usedPropertyModelInfos.Add(propertyModel.PropertyName, new PropertyModelInfo(objectType, fieldInfo.FieldType, propertyModel.FieldType));
							}

							//if (!abstractPropertyNames.Contains(propertyModel.Name))
							//{
							//	PropertyInfo propertyInfo = propertyInfos.FirstOrDefault(item => item.Name == propertyModel.Name);

							//	if (propertyInfo != null && propertyInfo.GetMethod.IsAbstract)
							//		abstractPropertyNames.Add(propertyModel.Name);
							//}
						}

						foreach (PropertyInfo propertyInfo in propertyInfos)
							if (propertyInfo.GetMethod != null && (propertyInfo.GetMethod.IsAbstract || propertyInfo.GetMethod.IsVirtual) && !abstractPropertyNames.Contains(propertyInfo.Name))
								abstractPropertyNames.Add(propertyInfo.Name);

						//if (!processedTypes.Contains(objectType))
						//{
						//	progressReporter(String.Format("{0}   -  no need for properties implementation...\r\n", objectType.FullName));
						//	processedTypes.Add(objectType);
						//}

						//continue;
						//}
					}
					else // This is object type for which exist model definition and will be or is processed - update used property names only
					{
						ISimpleObjectModel? currentObjectTypeBusinessPropertyModel;

						if (objectModelsByObjectType.TryGetValue(objectType, out currentObjectTypeBusinessPropertyModel))
							foreach (IPropertyModel propertyModel in currentObjectTypeBusinessPropertyModel.PropertyModels)
								if (!usedPropertyModelInfos.ContainsKey(propertyModel.PropertyName))
									usedPropertyModelInfos.Add(propertyModel.PropertyName, new PropertyModelInfo(objectType, propertyModel.PropertyType, propertyModel.FieldType));

						continue;
					}

					if (processedTypes.Contains(objectType))
					{
						// Update usedPropertyModelInfos with the new ones
						foreach (var item in newUsedPropertyModelInfos)
							if (!usedPropertyModelInfos.ContainsKey(item.Key))
								usedPropertyModelInfos.Add(item.Key, item.Value);

						continue;
					}

					List<string> propertyNamesToImplement = CreatePropertyNames(propertyModelsToImplement, usedPropertyModelInfos);
					string? objectClassText = CreateObjectClassText(modelDiscovery, masterObjectModel, objectType, propertyNamesToImplement, usedPropertyModelInfos, abstractPropertyNames);

					// Update usedPropertyModelInfos with the new ones after objectClassText has been created
					foreach (var item in newUsedPropertyModelInfos)
						if (!usedPropertyModelInfos.ContainsKey(item.Key))
							usedPropertyModelInfos.Add(item.Key, item.Value);

					processedTypes.Add(objectType);

					if (!includeSystemObjects && objectType.Assembly == typeof(SimpleObject).Assembly)
					{
						progressReporter(String.Format("       {0}  --> System object, skipped...\r\n", objectType.FullName));
						
						continue;
					}
					else if (objectClassText.IsNullOrEmpty())
					{
						progressReporter(String.Format("       {0}  -->  No need for generating additional fields, properties or relations...\r\n", objectType.FullName));
						
						continue;
					}

					// File creating and populating
					string objectFilePath = FindObjectFilePath(objectType);
					string assemblyProjectPath = new DirectoryInfo(objectType.Assembly.Location + "..\\..\\..\\..\\..\\").FullName;
					string filePathToDisplay = objectFilePath; //.Substring(assemblyProjectPath.Length);

					progressReporter(String.Format("{0}  -->  Generating file {1}\r\n", objectType.FullName, filePathToDisplay) + additionalTextInfo);

					using (TextWriter objectFileTextWriter = new StreamWriter(objectFilePath, append: false))
					{
						objectFileTextWriter.Write(objectClassText);
						objectFileTextWriter.Flush();
						objectFileTextWriter.Close();
						objectFileTextWriter.Dispose();
					}
				}
			}
		}

		private static string CreateObjectClassText(SimpleObjectModelDiscovery modelDiscovery, ISimpleObjectModel masterObjectModel, Type objectType, IEnumerable<string> propertyNames, IDictionary<string, PropertyModelInfo> usedPropertyInfos, IEnumerable<string> abstractPropertyNames)
		{
			const string strRegion = "#region ";
			const string strEndRegion = "#endregion ";
			const string strClassRegionInfo = "|   Code Generated by Simple.Objects Code Generator   |";
			//const string strPrivateMemberInfo = "|   Private Methods   |";
			const string strProtectedMembersInfo = "|   Protected Members   |";
			const string strPrivateStaticMembersInfo = "|   Private Static Members   |";
			const string strStaticInitializationInfo = "|   Static Methods Initialization   |";
			const string strPropertyMembersInfo = "|   Properties by Object Property Model   |";
			const string strOneToOneRelationPropertyInfo = "|   One-To-One Relation Properties   |";
			const string strOneToManyRelationPropertyInfo = "|   One-To-Many Relation Properties   |";
			const string strManyToManyRelationPropertyInfo = "|   Many-To-Many Relation Properties   |";
			const string strPublicOverridenMethodInfo = "|   Public Overriden Methods   |";
			const string strProtectedOverridenAbstractMethodInfo = "|   Protected Abstract Overriden Methods  |";

			string result = String.Empty;
			string? strNamespace = objectType.Namespace;
			string usingNamespacesText = String.Empty;
			string classText = String.Empty;
			string strProtectedMembers = String.Empty;
			string strPrivateStaticMembers = String.Empty;
			string strStaticInitialization = String.Empty;
			string strGetFieldValueMethodsList = String.Empty;
			string strGetOldFieldValueMethodsList = String.Empty;
			string strSetFieldValueMethodsList = String.Empty;
			string strSetOldFieldValueMethodsList = String.Empty;
			string strPropertyList = String.Empty;
			string strOneToOneRelationList = String.Empty;
			string strOneToManyRelationList = String.Empty;
			string strGetOneToManyForeignObjectCollection = String.Empty;
			string strManyToManyRelationList = String.Empty;
			string strGetGroupMemberCollection = String.Empty;
			string strGetModelMethod = String.Empty;
			string strProtectedOverridenGetInternalPropertyMethods = String.Empty;
			string strElse = String.Empty;

			HashSet<string> usedNamespaces = new HashSet<string>();

			// Create relation model for processing objectType 
			IObjectRelationModel relationModel = new ObjectRelationModel(null!, objectType, modelDiscovery.RelationPolicyModel, includeSubclasses: true);
			string? simpleObjectNamespace = typeof(SimpleObject).Namespace;

			if (simpleObjectNamespace != null && objectType.Namespace != simpleObjectNamespace)
				usedNamespaces.Add(simpleObjectNamespace);

			if (masterObjectModel.ObjectType == objectType)
			{
				int maxPropertyNameLenght = masterObjectModel.PropertyModels.Max(item => item.PropertyName.Length);

				foreach (IPropertyModel propertyModel in masterObjectModel.PropertyModels)
				{
					Type fieldType = (usedPropertyInfos.Keys.Contains(propertyModel.PropertyName)) ? usedPropertyInfos[propertyModel.PropertyName].FieldType : propertyModel.FieldType; // CalculatePropertyType(propertyModel);
					string fieldTypeName = ReflectionHelper.GetTypeName(fieldType);
					string fieldName = propertyModel.PropertyName.UnCapitalizeFirstLetter();
					string oldFieldName = "old" + fieldName.CapitalizeFirstLetter();
					string gap = String.Empty;
					string getFieldValueAction = "(item) => item." + fieldName + ";";
					string setFieldValueAction = "(item, value) => item." + fieldName + " = (" + fieldTypeName + ")value;";
					string getOldFieldValueAction = "(item) => item." + oldFieldName + ";";
					string setOldFieldValueAction = "(item, value) => item." + oldFieldName + " = (" + fieldTypeName + ")value;";

					if (fieldType.Namespace != null)
						usedNamespaces.Add(fieldType.Namespace);

					for (int i = propertyModel.PropertyName.Length; i < maxPropertyNameLenght; i++)
						gap += " ";

					//if (propertyModel.TableIdAsRelationKeyPropertyModel != null)
					//{
					//	fieldName = propertyModel.TableIdAsRelationKeyPropertyModel.Name;

					//	getFieldValueAction = "(item) => item." + fieldName + ".GetTableId();";
					//	setFieldValueAction = "(item, value) => { };";
					//	getOldFieldValueAction = "(item) => item.old" + fieldName.CapitalizeFirstLetter() + ".GetTableId();";
					//	setOldFieldValueAction = "(item, value) => { };";
					//}

					strGetFieldValueMethodsList	   += "            GetFieldValueMethods["	 + masterObjectModel.Name + ".PropertyModel." + propertyModel.PropertyName + ".PropertyIndex] " + gap + "= " + getFieldValueAction + "\r\n";
					strSetFieldValueMethodsList    += "            SetFieldValueMethods["	 + masterObjectModel.Name + ".PropertyModel." + propertyModel.PropertyName + ".PropertyIndex] " + gap + "= " + setFieldValueAction + "\r\n";
					strGetOldFieldValueMethodsList += "            GetOldFieldValueMethods[" + masterObjectModel.Name + ".PropertyModel." + propertyModel.PropertyName + ".PropertyIndex] " + gap + "= " + getOldFieldValueAction + "\r\n";
					strSetOldFieldValueMethodsList += "            SetOldFieldValueMethods[" + masterObjectModel.Name + ".PropertyModel." + propertyModel.PropertyName + ".PropertyIndex] " + gap + "= " + setOldFieldValueAction + "\r\n";
				}

				strPrivateStaticMembers =
					"        private static Func<"	 + objectType.Name + ", object?>[] GetFieldValueMethods;\r\n" +
					"        private static Func<"	 + objectType.Name + ", object?>[] GetOldFieldValueMethods;\r\n" +
					"        private static Action<" + objectType.Name + ", object>[] SetFieldValueMethods;\r\n" +
					"        private static Action<" + objectType.Name + ", object>[] SetOldFieldValueMethods;\r\n";

				strStaticInitialization =
					"        static " + objectType.Name + "()\r\n" +
					"        {\r\n" +
					"            int maxIndex = " + masterObjectModel.Name + ".PropertyModel.GetCollection().GetMaxIndex();\r\n" +
					"\r\n" +
					"            GetFieldValueMethods = new Func<" + objectType.Name + ", object?>[maxIndex + 1];\r\n" +
					strGetFieldValueMethodsList +
					"\r\n" +
					"            GetOldFieldValueMethods = new Func<" + objectType.Name + ", object?>[maxIndex + 1];\r\n" +
					strGetOldFieldValueMethodsList +
					"\r\n" +
					"            SetFieldValueMethods = new Action<" + objectType.Name + ", object>[maxIndex + 1];\r\n" +
					strSetFieldValueMethodsList +
					"\r\n" +
					"            SetOldFieldValueMethods = new Action<" + objectType.Name + ", object>[maxIndex + 1];\r\n" +
					strSetOldFieldValueMethodsList +
					"        }\r\n";

				strGetModelMethod =
					"        /// <summary>\r\n" +
					"        /// Gets the " + masterObjectModel.Name + " definition instance.\r\n" +
					"        /// </summary>\r\n" +
					"        public override ISimpleObjectModel GetModel()\r\n" +
					"        {\r\n" +
					"            return " + masterObjectModel.Name + ".Instance; \r\n" +
					"        }\r\n";

				strProtectedOverridenGetInternalPropertyMethods =
					"        /// <summary>\r\n" +
					"        /// Gets field property value by property index.\r\n" +
					"        /// </summary>\r\n" +
					"        protected override object? GetFieldValue(int propertyIndex)\r\n" +
					"        {\r\n" +
					"            return GetFieldValueMethods[propertyIndex](this);\r\n" +
					"        }\r\n" +
					"\r\n" +
					"        /// <summary>\r\n" +
					"        /// Gets field old property value by property index.\r\n" +
					"        /// </summary>\r\n" +
					"        protected override object? GetOldFieldValue(int propertyIndex)\r\n" +
					"        {\r\n" +
					"            return GetOldFieldValueMethods[propertyIndex](this);\r\n" +
					"        }\r\n" +
					"\r\n" +
					"        /// <summary>\r\n" +
					"        /// Sets field property value by property index.\r\n" +
					"        /// </summary>\r\n" +
					"        protected override void SetFieldValue(int propertyIndex, object value)\r\n" +
					"        {\r\n" +
					"            SetFieldValueMethods[propertyIndex](this, value);\r\n" +
					"        }\r\n" +
					"\r\n" +
					"        /// <summary>\r\n" +
					"        /// Sets field old property value by property index.\r\n" +
					"        /// </summary>\r\n" +
					"        protected override void SetOldFieldValue(int propertyIndex, object value)\r\n" +
					"        {\r\n" +
					"            SetOldFieldValueMethods[propertyIndex](this, value);\r\n" +
					"        }\r\n";
			}

			foreach (string propertyName in propertyNames)
			{
				IPropertyModel propertyModel = masterObjectModel.PropertyModels[propertyName];
				Type fieldType = (usedPropertyInfos.Keys.Contains(propertyName)) ? usedPropertyInfos[propertyName].FieldType : propertyModel.FieldType; // CalculatePropertyType(propertyModel);
				string fieldTypeName = fieldType.GetName();
				string propertyTypeName = propertyModel.PropertyType.GetName();
				string fieldName = propertyModel.PropertyName.UnCapitalizeFirstLetter();
				string oldFieldName = "old" + fieldName.CapitalizeFirstLetter();	
				string defaultFieldValue = String.Empty;
				AccessModifier accessModifier = propertyModel.GetAccessModifier; // ToString("F").Replace(",", "").ToLower(); //propertyModel.AccessModifier.InsertTextOnUpperChange(" ").ToLower();
				string setPropertyAccessModifier = CreateSetAccessModifierString(propertyModel);
				string fieldCasting = String.Empty;
				string propertyCasting = String.Empty; 

				if (usedPropertyInfos.Keys.Contains(propertyModel.PropertyName))
					accessModifier |= AccessModifier.New;

				if (abstractPropertyNames.Contains(propertyModel.PropertyName) && ((accessModifier & AccessModifier.Override) != AccessModifier.Override))
				{
					accessModifier |= AccessModifier.Override;
					accessModifier &= ~AccessModifier.Virtual; // Remove Virtual when adding Override
				}

				if (fieldType.Namespace != null)
					usedNamespaces.Add(fieldType.Namespace);
				
				if (propertyModel.PropertyType.Namespace != null)
					usedNamespaces.Add(propertyModel.PropertyType.Namespace);

				if (!Comparison.IsEqual(propertyModel.DefaultValue, propertyModel.PropertyType.GetDefaultValue()) && propertyModel.DefaultValue != null)
				{
					defaultFieldValue = Conversion.ToString(propertyModel.DefaultValue);

					if (propertyModel.PropertyType.IsArray)
						defaultFieldValue = "new " + defaultFieldValue;

					defaultFieldValue = " = " + defaultFieldValue;
				}
				else if (propertyModel.PropertyType == typeof(string))
				{
					defaultFieldValue = " = String.Empty";
				}

				if (!usedPropertyInfos.Keys.Contains(propertyName)) // Avoid adding duplicate fields
					strProtectedMembers += "        protected " + fieldTypeName + " " + fieldName + defaultFieldValue + ", " + oldFieldName + defaultFieldValue + ";\r\n";

				if (fieldType != propertyModel.PropertyType)
				{
					fieldCasting = "(" + fieldTypeName + ")";
					propertyCasting = "(" + propertyTypeName + ")";
				}

				if (propertyModel.AutoGenerateProperty)
				{
					string strPropertyListSet = String.Empty;

					if (!String.IsNullOrEmpty(strPropertyList))
						strPropertyList += "\r\n";

					if (!(propertyModel.AccessPolicy == PropertyAccessPolicy.ReadOnly && propertyModel.SetAccessModifier == AccessModifier.Public))
						strPropertyListSet = "            " + setPropertyAccessModifier + "set { this.SetPropertyValue(" + masterObjectModel.Name + ".PropertyModel." + propertyModel.PropertyName + ", value); }\r\n";
						//strPropertyListSet = "            " + setPropertyAccessModifier + "set { this.SetPropertyValue<" + fieldTypeName + ">(" + masterObjectModel.Name + ".PropertyModel." + propertyModel.PropertyName + ", " + fieldCasting + "value, ref this." + fieldName + ", this.old" + propertyModel.PropertyName + "); }\r\n";

					strPropertyList += "        /// <summary>\r\n" +
									   "        /// " + (strPropertyListSet.IsNullOrEmpty() ? "Gets " : "Gets or sets ") + (propertyModel.Description.IsNullOrEmpty() ? propertyModel.PropertyName : propertyModel.Description.Replace("\r\n", "\r\n        /// ")) + "\r\n" +
									   "        /// </summary>\r\n" +
									   "        " + accessModifier.ToLowerString() + " " + propertyTypeName + " " + propertyModel.PropertyName + "\r\n" +
									   "        {\r\n" +
									   "            get { return " + propertyCasting + "this." + fieldName + "; }\r\n" +
									   strPropertyListSet +
									   "		}\r\n";
				}
			}

			// Relation properties creation
			foreach (OneToOneRelationModel oneToOneRelationModel in relationModel.AsPrimaryObjectInOneToOneRelations)
			{
				string propertyName = oneToOneRelationModel.ForeignObjectName;
				string summary = oneToOneRelationModel.ForeignObjectSummary.IsNullOrEmpty() ? "Gets or sets one-to-one relation foreign " + propertyName + " object." : oneToOneRelationModel.PrimaryObjectSummary.Replace("\r\n", "        /// \r\n");
				string foreignObjectTypeName = oneToOneRelationModel.ForeignObjectType.GetName();
				string foreignObjectTypeNullableName = oneToOneRelationModel.GetObjectTypeName(oneToOneRelationModel.ForeignObjectType);
				AccessModifier accessModifier = oneToOneRelationModel.PrimaryAccessModifier;
				AccessModifier setAccessModifier = oneToOneRelationModel.PrimarySetAccessModifier;
				string setAccessModifierText = (setAccessModifier != AccessModifier.Default) ? setAccessModifier.ToLowerString() + " " : "";

				if ((accessModifier & AccessModifier.Override) != AccessModifier.Override) // If not AccessModifier.Override already
				{
					if (abstractPropertyNames.Contains(propertyName) && !objectType.IsAbstract)
					{
						accessModifier |= AccessModifier.Override;
						accessModifier &= ~AccessModifier.Virtual; // Remove Virtual when adding Override
					}
					else if (IsPropertyImplemented(objectType, propertyName))
					{
						accessModifier |= AccessModifier.New;
					}
				}

				if (abstractPropertyNames.Contains(propertyName) && !objectType.IsAbstract && ((accessModifier & AccessModifier.Override) != AccessModifier.Override))
					accessModifier |= AccessModifier.Override;
				
				if (!string.IsNullOrEmpty(strOneToOneRelationList))
					strOneToOneRelationList += "\r\n";

				strOneToOneRelationList +=
									   "        /// <summary>\r\n" +
									   "        /// " + summary + "\r\n" +
									   "        /// </summary>\r\n" +
									   "        " + accessModifier.ToLowerString() + " " + foreignObjectTypeNullableName + " " + propertyName + "\r\n" +
									   "        {\r\n" +
						 String.Format("            get {{ return this.GetOneToOneForeignObject<{0}>({1}.{2}){3}; }}\r\n", foreignObjectTypeName, oneToOneRelationModel.DefinitionObjectClassType.Name, oneToOneRelationModel.DefinitionFieldName, (oneToOneRelationModel.CanBeNull) ? "" : "!") +
						 String.Format("            {0}set {{ this.SetOneToOneForeignObject(value, {1}.{2}); }}\r\n", setAccessModifierText, oneToOneRelationModel.DefinitionObjectClassType.Name, oneToOneRelationModel.DefinitionFieldName) +
									   "        }\r\n";

				PropertyIsImplemented(objectType, propertyName);
			}

			foreach (OneToOneRelationModel oneToOneRelationModel in relationModel.AsForeignObjectInOneToOneRelations)
			{
				string propertyName = oneToOneRelationModel.PrimaryObjectName;
				string summary = oneToOneRelationModel.PrimaryObjectSummary.IsNullOrEmpty() ? "Gets or sets one-to-one relation primary " + propertyName + " object." : oneToOneRelationModel.PrimaryObjectSummary.Replace("\r\n", "        /// \r\n");
				string primaryObjectTypeName = oneToOneRelationModel.PrimaryObjectType.GetName();
				string primaryObjectTypeNullableName = oneToOneRelationModel.GetObjectTypeName(oneToOneRelationModel.PrimaryObjectType);
				AccessModifier accessModifier = oneToOneRelationModel.ForeignAccessModifier;
				AccessModifier setAccessModifier = oneToOneRelationModel.ForeignSetAccessModifier;
				string setAccessModifierText = (setAccessModifier != AccessModifier.Default) ? setAccessModifier.ToLowerString() + " ": "";

				if ((accessModifier & AccessModifier.Override) != AccessModifier.Override) // If not AccessModifier.Override already
				{
					if (abstractPropertyNames.Contains(propertyName) && !objectType.IsAbstract)
					{
						accessModifier |= AccessModifier.Override;
						accessModifier &= ~AccessModifier.Virtual; // Remove Virtual when adding Override
					}
					else if (IsPropertyImplemented(objectType, propertyName))
					{
						//continue;
						accessModifier |= AccessModifier.New;
					}
				}

				if (abstractPropertyNames.Contains(propertyName) && !objectType.IsAbstract && ((accessModifier & AccessModifier.Override) != AccessModifier.Override))
					accessModifier |= AccessModifier.Override;

				if (!string.IsNullOrEmpty(strOneToOneRelationList))
					strOneToOneRelationList += "\r\n";

				strOneToOneRelationList +=
									   "        /// <summary>\r\n" +
									   "        /// " + summary + "\r\n" +
									   "        /// </summary>\r\n" +
									   "        " + accessModifier.ToLowerString() + " " + primaryObjectTypeNullableName + " " + propertyName + "\r\n" +
									   "        {\r\n" +
						 String.Format("            get {{ return this.GetOneToOnePrimaryObject<{0}>({1}.{2}){3}; }}\r\n", primaryObjectTypeName, oneToOneRelationModel.DefinitionObjectClassType.Name, oneToOneRelationModel.DefinitionFieldName, (oneToOneRelationModel.CanBeNull) ? "" : "!") +
						 String.Format("            {0}set {{ this.SetOneToOnePrimaryObject(value, {1}.{2}); }}\r\n", setAccessModifierText, oneToOneRelationModel.DefinitionObjectClassType.Name, oneToOneRelationModel.DefinitionFieldName) +
									   "        }\r\n";
				
				PropertyIsImplemented(objectType, propertyName);
			}

			foreach (OneToManyRelationModel oneToManyRelationModel in relationModel.AsForeignObjectInOneToManyRelations)
			{
				string propertyName = oneToManyRelationModel.PrimaryObjectName;
				string summary = oneToManyRelationModel.PrimaryObjectSummary.IsNullOrEmpty() ? "Gets or sets one-to-many relation primary " + propertyName + " object." : oneToManyRelationModel.PrimaryObjectSummary.Replace("\r\n", "        /// \r\n");
				string primaryObjectTypeName = oneToManyRelationModel.PrimaryObjectType.GetName();
				string primaryObjectTypeNullableName = primaryObjectTypeName + ((oneToManyRelationModel.CanBeNull) ? "?" : ""); // oneToManyRelationModel.GetObjectTypeName(oneToManyRelationModel.PrimaryObjectType);
				AccessModifier accessModifier = oneToManyRelationModel.ForeignAccessModifier;
				AccessModifier setAccessModifier = oneToManyRelationModel.ForeignSetAccessModifier;
				string setAccessModifierText = (setAccessModifier != AccessModifier.Default) ? setAccessModifier.ToLowerString() + " " : "";

				if (oneToManyRelationModel.CanBeNull)

				if ((accessModifier & AccessModifier.Override) != AccessModifier.Override) // If not AccessModifier.Override already
				{
					if (abstractPropertyNames.Contains(propertyName) && !objectType.IsAbstract)
					{
						accessModifier |= AccessModifier.Override;
						accessModifier &= ~AccessModifier.Virtual; // Remove Virtual when adding Override
					}
					else if (IsPropertyImplemented(objectType, propertyName))
					{
						//continue;
						accessModifier |= AccessModifier.New;
					}
				}

				if (abstractPropertyNames.Contains(propertyName) && !objectType.IsAbstract && ((accessModifier & AccessModifier.Override) != AccessModifier.Override))
					accessModifier |= AccessModifier.Override;

				if (!string.IsNullOrEmpty(strOneToManyRelationList))
					strOneToManyRelationList += "\r\n";

				strOneToManyRelationList +=
									   "        /// <summary>\r\n" +
									   "        /// " + summary + "\r\n" +
									   "        /// </summary>\r\n" +
									   "        " + accessModifier.ToLowerString() + " " + primaryObjectTypeNullableName + " " + propertyName + "\r\n" +
									   "        {\r\n" +
						 String.Format("            get {{ return this.GetOneToManyPrimaryObject<{0}>({1}.{2}){3}; }}\r\n", primaryObjectTypeName, oneToManyRelationModel.DefinitionObjectClassType.Name, oneToManyRelationModel.DefinitionFieldName, (oneToManyRelationModel.CanBeNull) ? "" : "!") +
						 String.Format("            {0}set {{ this.SetOneToManyPrimaryObject(value, {1}.{2}); }}\r\n", setAccessModifierText, oneToManyRelationModel.DefinitionObjectClassType.Name, oneToManyRelationModel.DefinitionFieldName) +
									   "        }\r\n";

				PropertyIsImplemented(objectType, propertyName);
			}

			strElse = String.Empty;

			foreach (OneToManyRelationModel oneToManyRelationModel in relationModel.AsPrimaryObjectInOneToManyRelations)
			{
				string propertyName = oneToManyRelationModel.ForeignCollectionName;
				string summary = oneToManyRelationModel.ForeignCollectionSummary.IsNullOrEmpty() ? "Gets one-to-many relation foreign " + propertyName + " collection." : oneToManyRelationModel.ForeignCollectionSummary.Replace("\r\n", "        /// \r\n");
				string foreignObjectTypeName = oneToManyRelationModel.ForeignObjectType.GetName();
				//string foreignObjectTypeNullableName = oneToManyRelationModel.GetObjectTypeName(oneToManyRelationModel.ForeignObjectType);
				AccessModifier accessModifier = oneToManyRelationModel.PrimaryAccessModifier;
				bool implementProperty = true;

				if ((accessModifier & AccessModifier.Override) != AccessModifier.Override) // If not AccessModifier.Override already
				{
					if (abstractPropertyNames.Contains(propertyName) && !objectType.IsAbstract)
					{
						accessModifier |= AccessModifier.Override;
						accessModifier &= ~AccessModifier.Virtual; // Remove Virtual when adding Override
					}
					else if (IsPropertyImplemented(objectType, propertyName))
					{
						implementProperty = false;
						//continue;
						//accessModifier |= AccessModifier.New;
					}
				}

				if (implementProperty)
				{
					if (!string.IsNullOrEmpty(strOneToManyRelationList))
						strOneToManyRelationList += "\r\n";

					strOneToManyRelationList +=
											"        /// <summary>\r\n" +
											"        /// " + summary + "\r\n" +
											"        /// </summary>\r\n" +
											"        " + accessModifier.ToLowerString() + " SimpleObjectCollection<" + foreignObjectTypeName + "> " + propertyName + "\r\n" +
											"        {\r\n" +
							  String.Format("            get {{ return this.GetOneToManyForeignObjectCollection<{0}>({1}.{2}); }}\r\n", foreignObjectTypeName, oneToManyRelationModel.DefinitionObjectClassType.Name, oneToManyRelationModel.DefinitionFieldName) +
											"        }\r\n";

					PropertyIsImplemented(objectType, propertyName);
				}

				if (masterObjectModel.ObjectType == objectType)
				{
					if (strGetOneToManyForeignObjectCollection.IsNullOrEmpty())
						strGetOneToManyForeignObjectCollection = "\r\n" +
													 "        /// <summary>\r\n" +
													 "        /// Gets the one-to-many collection based on relation key. If no one-to-many relation specified in relation model for this object, the base class method is called.\r\n" +
													 "        /// </summary>\r\n" +
													 "        /// <param name=\"relationKey\">The one-to-many relation key</param>\r\n" +
													 "        /// <returns>The <see cref=\"SimpleObjectCollection?\"/> result.</returns>\r\n" +
													 "        public override SimpleObjectCollection? GetOneToManyForeignObjectCollection(int relationKey)\r\n" +
													 "        {\r\n";

					strGetOneToManyForeignObjectCollection +=
									   String.Format("            {0}if (relationKey == {1}.{2}.RelationKey)\r\n", strElse, oneToManyRelationModel.DefinitionObjectClassType.Name, oneToManyRelationModel.DefinitionFieldName);

					if (relationModel.AsPrimaryObjectInOneToManyRelations.Count > 1)
						strGetOneToManyForeignObjectCollection += "            {\r\n";

					strGetOneToManyForeignObjectCollection += "                return this." + propertyName + ";\r\n";

					if (relationModel.AsPrimaryObjectInOneToManyRelations.Count > 1)
						strGetOneToManyForeignObjectCollection += "            }\r\n";

					if (strElse.IsNullOrEmpty())
						strElse = "else ";
				}
			}

			if (!strGetOneToManyForeignObjectCollection.IsNullOrEmpty())
				strGetOneToManyForeignObjectCollection += "\r\n            return base.GetOneToManyForeignObjectCollection(relationKey);\r\n        }\r\n";

			strElse = String.Empty;

			foreach (ManyToManyRelationModel manyToManyRelationModel in relationModel.AsFirstObjectInManyToManyRelations)
			{
				string propertyName = manyToManyRelationModel.FirstObjectCollectionName;
				string summary = manyToManyRelationModel.FirstObjectCollectionSummary.IsNullOrEmpty() ? "Gets membership " + propertyName + " collection." : manyToManyRelationModel.FirstObjectCollectionSummary.Replace("\r\n", "        /// \r\n");
				AccessModifier accessModifier = manyToManyRelationModel.FirstObjectCollectionModifier;
				string firstObjectTypeName = manyToManyRelationModel.FirstObjectType.GetName();

				if ((accessModifier & AccessModifier.Override) != AccessModifier.Override) // If not AccessModifier.Override already
				{
					if (abstractPropertyNames.Contains(propertyName) && !objectType.IsAbstract)
					{
						accessModifier |= AccessModifier.Override;
						accessModifier &= ~AccessModifier.Virtual; // Remove Virtual when adding Override
					}
					else if (IsPropertyImplemented(objectType, propertyName))
					{
						accessModifier |= AccessModifier.New;
					}
				}

				if (abstractPropertyNames.Contains(firstObjectTypeName) && !objectType.IsAbstract && ((accessModifier & AccessModifier.Override) != AccessModifier.Override))
					accessModifier |= AccessModifier.Override;

				if (!string.IsNullOrEmpty(strManyToManyRelationList))
					strManyToManyRelationList += "\r\n";

				strManyToManyRelationList +=
									   "        /// <summary>\r\n" +
									   "        /// " + summary + "\r\n" +
									   "        /// </summary>\r\n" +
									   "        " + accessModifier.ToLowerString() + " SimpleObjectCollection<" + firstObjectTypeName + "> " + propertyName + "\r\n" +
									   "        {\r\n" +
						 String.Format("            get {{ return this.GetGroupMemberCollection<{0}>({1}.{2}); }}\r\n", firstObjectTypeName, manyToManyRelationModel.DefinitionObjectClassType.Name, manyToManyRelationModel.DefinitionFieldName) +
									   "        }\r\n";
				
				PropertyIsImplemented(objectType, propertyName);

				if (masterObjectModel.ObjectType == objectType)
				{
					if (strGetGroupMemberCollection.IsNullOrEmpty())
						strGetGroupMemberCollection = "\r\n" +
														"        /// <summary>\r\n" +
														"        /// Gets the group member collection based on relation key. If no many-to-many relation specified in relation model for this object, the base class method is called.\r\n" +
														"        /// </summary>\r\n" +
														"        /// <param name=\"relationKey\">The many-to-many relation key</param>\r\n" +
														"        /// <returns>The <see cref=\"SimpleObjectCollection?\"/> result.</returns>\r\n" +
														"        public override SimpleObjectCollection? GetGroupMemberCollection(int relationKey)\r\n" +
														"        {\r\n";

					strGetGroupMemberCollection += String.Format("            {0}if (relationKey == {1}.{2}.RelationKey)\r\n", strElse, manyToManyRelationModel.DefinitionObjectClassType.Name, manyToManyRelationModel.DefinitionFieldName);

					if (relationModel.AsObjectInGroupMembership.Count > 1)
						strGetGroupMemberCollection += "            {\r\n";

					strGetGroupMemberCollection += "                return this." + propertyName + ";\r\n";

					if (relationModel.AsObjectInGroupMembership.Count > 1)
						strGetGroupMemberCollection += "            }\r\n";

					if (strElse.IsNullOrEmpty())
						strElse = "else ";
				}
			}

			foreach (ManyToManyRelationModel manyToManyRelationModel in relationModel.AsSecondObjectInManyToManyRelations)
			{
				string propertyName = manyToManyRelationModel.SecondObjectCollectionName;
				string summary = manyToManyRelationModel.SecondObjectCollectionSummary.IsNullOrEmpty() ? "Gets many-to-many relation " + propertyName + " collection." : manyToManyRelationModel.SecondObjectCollectionSummary.Replace("\r\n", "        /// \r\n");
				AccessModifier accessModifier = manyToManyRelationModel.SecondObjectCollectionModifier;
				string secondObjectTypeName = manyToManyRelationModel.SecondObjectType.Name;

				if ((accessModifier & AccessModifier.Override) != AccessModifier.Override) // If not AccessModifier.Override already
				{
					if (abstractPropertyNames.Contains(propertyName) && !objectType.IsAbstract)
					{
						accessModifier |= AccessModifier.Override;
						accessModifier &= ~AccessModifier.Virtual; // Remove Virtual when adding Override
					}
					else if (IsPropertyImplemented(objectType, propertyName))
					{
						accessModifier |= AccessModifier.New;
					}
				}

				if (abstractPropertyNames.Contains(secondObjectTypeName) && !objectType.IsAbstract && ((accessModifier & AccessModifier.Override) != AccessModifier.Override))
					accessModifier |= AccessModifier.Override;

				if (!string.IsNullOrEmpty(strManyToManyRelationList))
					strManyToManyRelationList += "\r\n";

				strManyToManyRelationList +=
									   "        /// <summary>\r\n" +
									   "        /// " + summary + "\r\n" +
									   "        /// </summary>\r\n" +
									   "        " + accessModifier.ToLowerString() + " SimpleObjectCollection<" + secondObjectTypeName + "> " + propertyName + "\r\n" +
									   "        {\r\n" +
						 String.Format("            get {{ return this.GetGroupMemberCollection<{0}>({1}.{2}); }}\r\n", secondObjectTypeName, manyToManyRelationModel.DefinitionObjectClassType.Name, manyToManyRelationModel.DefinitionFieldName) +
									   "        }\r\n";

				PropertyIsImplemented(objectType, propertyName);

				if (masterObjectModel.ObjectType == objectType)
				{
					if (strGetGroupMemberCollection.IsNullOrEmpty())
						strGetGroupMemberCollection = "\r\n" +
														"        /// <summary>\r\n" +
														"        /// Gets the group member collection based on relation key. If no many-to-many relation specified in relation model for this object, the base class method is called.\r\n" +
														"        /// </summary>\r\n" +
														"        /// <param name=\"relationKey\">The many-to-many relation key</param>\r\n" +
														"        /// <returns>The <see cref=\"SimpleObjectCollection?\"/> result.</returns>\r\n" +
														"        public override SimpleObjectCollection? GetGroupMemberCollection(int relationKey)\r\n" +
														"        {\r\n";

					strGetGroupMemberCollection += String.Format("            {0}if (relationKey == {1}.{2}.RelationKey)\r\n", strElse, manyToManyRelationModel.DefinitionObjectClassType.Name, manyToManyRelationModel.DefinitionFieldName);

					if (relationModel.AsObjectInGroupMembership.Count > 1)
						strGetGroupMemberCollection += "            {\r\n";

					strGetGroupMemberCollection += "                return this." + propertyName + ";\r\n";

					if (relationModel.AsObjectInGroupMembership.Count > 1)
						strGetGroupMemberCollection += "            }\r\n";

					if (strElse.IsNullOrEmpty())
						strElse = "else ";
				}
			}

			if (!strGetGroupMemberCollection.IsNullOrEmpty())
				strGetGroupMemberCollection += "\r\n            return base.GetGroupMemberCollection(relationKey);\r\n        }\r\n";

			// #region directives inserting
			if (!strProtectedMembers.IsNullOrEmpty())
				strProtectedMembers = "        " + strRegion + strProtectedMembersInfo + "\r\n\r\n" + strProtectedMembers + "\r\n        " + strEndRegion + strProtectedMembersInfo + "\r\n";

			if (!strPrivateStaticMembers.IsNullOrEmpty())
				strPrivateStaticMembers = "        " + strRegion + strPrivateStaticMembersInfo + "\r\n\r\n" + strPrivateStaticMembers + "\r\n        " + strEndRegion + strPrivateStaticMembersInfo + "\r\n";

			if (!strStaticInitialization.IsNullOrEmpty())
				strStaticInitialization = "        " + strRegion + strStaticInitializationInfo + "\r\n\r\n" + strStaticInitialization + "\r\n        " + strEndRegion + strStaticInitializationInfo + "\r\n";

			if (!strPropertyList.IsNullOrEmpty())
				strPropertyList = "        " + strRegion + strPropertyMembersInfo + "\r\n\r\n" + strPropertyList + "\r\n        " + strEndRegion + strPropertyMembersInfo + "\r\n";

			if (!strOneToOneRelationList.IsNullOrEmpty())
				strOneToOneRelationList = "        " + strRegion + strOneToOneRelationPropertyInfo + "\r\n\r\n" + strOneToOneRelationList + "\r\n        " + strEndRegion + strOneToOneRelationPropertyInfo + "\r\n";

			if (!strOneToManyRelationList.IsNullOrEmpty())
				strOneToManyRelationList = "        " + strRegion + strOneToManyRelationPropertyInfo + "\r\n\r\n" + strOneToManyRelationList + "\r\n        " + strEndRegion + strOneToManyRelationPropertyInfo + "\r\n";

			if (!strManyToManyRelationList.IsNullOrEmpty())
				strManyToManyRelationList = "        " + strRegion + strManyToManyRelationPropertyInfo + "\r\n\r\n" + strManyToManyRelationList + "\r\n        " + strEndRegion + strManyToManyRelationPropertyInfo + "\r\n";

			if (!strGetModelMethod.IsNullOrEmpty())
				strGetModelMethod = "        " + strRegion + strPublicOverridenMethodInfo + "\r\n\r\n" + strGetModelMethod + strGetOneToManyForeignObjectCollection + strGetGroupMemberCollection + "\r\n        " + strEndRegion + strPublicOverridenMethodInfo + "\r\n";

			if (!strProtectedOverridenGetInternalPropertyMethods.IsNullOrEmpty())
				strProtectedOverridenGetInternalPropertyMethods = "        " + strRegion + strProtectedOverridenAbstractMethodInfo + "\r\n\r\n" + strProtectedOverridenGetInternalPropertyMethods + "\r\n        " + strEndRegion + strProtectedOverridenAbstractMethodInfo + "\r\n";

			// Remove default NET.Framework assembly (mscorlib), if exists
			string? @namespace = typeof(int).Namespace;
			
			if (@namespace != null)
				usedNamespaces.Remove(@namespace);
			
			if (objectType.Namespace != null)
				usedNamespaces.Remove(objectType.Namespace);

			string[] usedNamespacesArray = usedNamespaces.ToArray();
			usedNamespacesArray.Sort();

			usingNamespacesText = GenerateSegmentText("using ", ";\r\n", usedNamespacesArray);
			classText = GenerateClassSegmentText("\r\n", strProtectedMembers, strPrivateStaticMembers, strStaticInitialization, strPropertyList,
														 strOneToOneRelationList, strOneToManyRelationList, strManyToManyRelationList,
														 strGetModelMethod, strProtectedOverridenGetInternalPropertyMethods);
			if (classText == null || classText.Trim().Length == 0)
				return String.Empty;

			result = "" +
				"using System;\r\n" +
				"using System.Collections.Generic;\r\n" +
				"using System.Linq;\r\n" +
				"using System.Text;\r\n" +
				usingNamespacesText +
				"\r\n#nullable enable\r\n\r\n" +
				"namespace " + strNamespace + "\r\n" +
				"{\r\n" +
				"    " + strRegion + strClassRegionInfo + "\r\n" +
				"\r\n" +
				"    partial class " + objectType.Name + "\r\n" +
				"    {\r\n" +
				classText +
				"    }\r\n" +
				"\r\n" +
				"    " + strEndRegion + strClassRegionInfo + "\r\n" +
				"}\r\n";

			return result;
		}

		//private static string GetFieldName(string propertyName)
		//{
		//	string fieldName = Char.ToLowerInvariant(propertyName[0]).ToString();

		//	if (propertyName.Length > 1)
		//		fieldName += propertyName.Substring(1);

		//	return fieldName;
		//}

		private static bool IsPropertyImplemented(Type objectType, string propertyName)
		{
			//IEnumerable<Type> objectsSubclasses = ReflectionHelper.SelectAssemblySubTypesOf(AppDomain.CurrentDomain.GetAssemblies(), objectType);
			IEnumerable<Type> objectsSubclasses = objectSubclassesByObjectType[objectType];

			foreach (Type item in objectsSubclasses)
				if (implementedPropertyNamesByObjectType.ContainsKey(item) && implementedPropertyNamesByObjectType[item].Contains(propertyName))
					return true;

			return false;
		}

		private static void PropertyIsImplemented(Type objectType, string propertyName)
		{
			HashSet<string> implementedPropertyNames;
			
			if (!implementedPropertyNamesByObjectType.TryGetValue(objectType, out implementedPropertyNames))
			{
				implementedPropertyNames = new HashSet<string>();
				implementedPropertyNamesByObjectType.Add(objectType, implementedPropertyNames);
			}

			implementedPropertyNames.Add(propertyName);
		}

		private static void SetObjectSubclassesOf(Type objectType, IEnumerable<Type> objectSubclasses)
		{
			objectSubclassesByObjectType[objectType] = objectSubclasses;
		}

		private static string FindObjectFilePath(Type objectType)
		{
			string assemblyProjectPath = new DirectoryInfo(objectType.Assembly.Location + "..\\..\\..\\..\\..\\").FullName;
			string assemblyName = ReflectionHelper.GetAssemblyName(objectType);
			string objectFileName = String.Format("{0}.cs", objectType.Name);
			string objectFileNameGenerated = String.Format("{0}.{1}.cs", objectType.Name, strGenerated);
			string? objectFilePath = String.Empty;

			string currentAssemblyProjectPath = assemblyProjectPath;
			bool isRootReached = false;

			do
			{
				//objectFilePath = FindObjectFilePath(objectType, currentAssemblyProjectPath, assemblyName, objectFileNameGenerated);
				objectFilePath = FindObjectFilePathUsingCache(currentAssemblyProjectPath, objectFileNameGenerated);

				if (objectFilePath.IsNullOrEmpty()) // If ObjectType.Generated.cs file does not exists, check if ObjectType.cs file exists
				{
					//objectFilePath = FindObjectFilePath(objectType, currentAssemblyProjectPath, assemblyName, objectFileName);
					objectFilePath = FindObjectFilePathUsingCache(currentAssemblyProjectPath, objectFileName);
				}

				string newAssemblyProjectPath = new DirectoryInfo(currentAssemblyProjectPath + "..\\").FullName; // new DirectoryInfo(objectType.Assembly.Location + "..\\..\\..\\..\\..\\..\\").FullName;

				if (newAssemblyProjectPath == currentAssemblyProjectPath)
					isRootReached = true;
					
				currentAssemblyProjectPath = newAssemblyProjectPath;
			}
			while (objectFilePath.IsNullOrEmpty() && !isRootReached);

			//// If file not found in main project folder
			//if (objectFilePath.IsNullOrEmpty())
			//{
			//	// try to find in some other folders, e.g. if objectType is system object from Simple.Objects assembly go to one folder up and try to find
			//	string newAssemblyProjectPath = new DirectoryInfo(assemblyProjectPath + "..\\").FullName; // new DirectoryInfo(objectType.Assembly.Location + "..\\..\\..\\..\\..\\..\\").FullName;

			//	objectFilePath = FindObjectFilePath(objectType, newAssemblyProjectPath, assemblyName, objectFileNameGenerated);

			//	if (objectFilePath.IsNullOrEmpty())
			//		objectFilePath = assemblyProjectPath + objectFileNameGenerated;
			//}

			if (objectFilePath.IsNullOrEmpty())
			{
				var solutionFolder = TryGetSolutionDirectoryInfo();

				if (solutionFolder != null)
					objectFilePath += "\\" + objectFileNameGenerated;
			}

			return objectFilePath;
		}

		public static DirectoryInfo? TryGetSolutionDirectoryInfo(string? currentPath = null)
		{
			var directory = new DirectoryInfo(currentPath ?? Directory.GetCurrentDirectory());
			
			while (directory != null && !directory.GetFiles("*.sln").Any())
				directory = directory.Parent;
			
			return directory;
		}

		private static Dictionary<string, List<string>> FoldersByRootPath = new Dictionary<string, List<string>>();

		private static void ClearFolderCache() => FoldersByRootPath.Clear();

		private static string FindObjectFilePathUsingCache(string rootPath, string searchPattern)
		{
			if (!FoldersByRootPath.TryGetValue(rootPath, out List<string>? files))
			{
				files = GetAllAccessibleFiles(rootPath);
				FoldersByRootPath.Add(rootPath, files);
			}

			List<string> matchFiles = files.FindAll(file => file.EndsWith("\\" + searchPattern)); //, StringComparison.CurrentCultureIgnoreCase) ;

			if (matchFiles.Count > 0)
				return matchFiles[0];

			return String.Empty;
		}

		private static string FindObjectFilePath(Type objectType, string assemblyProjectPath, string assemblyName, string objectFileName)
		{
			string objectFilePath = String.Empty;
			List<string> existingObjectFiles = new List<string>();
			IEnumerable<string> matchFiles;

			// Try to find in assembly project folder, if exists first. e.g. "C:\Develop\SimpleObjects\MyApp.Objects"
			if (Directory.Exists(assemblyProjectPath + assemblyName))
			{
				// Find object file name, e.g. "User.Generated.cs", first
				matchFiles = GetAllFiles(assemblyProjectPath + assemblyName, objectFileName);
				existingObjectFiles.AddRange(matchFiles);
				existingObjectFiles.AddRange(Directory.GetFiles(assemblyProjectPath + assemblyName, objectFileName, SearchOption.AllDirectories));

				if (existingObjectFiles.Count > 0)
				{
					objectFilePath = new DirectoryInfo(existingObjectFiles[0]).FullName;
				}
				else // Find object file name, e.g. "User.cs"
				{
					matchFiles = GetAllFiles(assemblyProjectPath + assemblyName, objectType.Name + ".cs");
					existingObjectFiles.AddRange(matchFiles);

					if (existingObjectFiles.Count > 0)
						objectFilePath = new DirectoryInfo(existingObjectFiles[0] + "\\..\\").FullName + objectFileName;
				}
			}

			// If nothing is found in assembly project folder, try to find in project folder, if exists first. E.g. "C:\Develop\SimpleObjects"
			// Find object file name, e.g. "User.Generated.cs", first
			if (objectFilePath.IsNullOrEmpty())
			{
				matchFiles = GetAllFiles(assemblyProjectPath, objectFileName);
				existingObjectFiles.AddRange(matchFiles);

				if (existingObjectFiles.Count > 0)
				{
					objectFilePath = new DirectoryInfo(existingObjectFiles[0]).FullName;
				}
				else // Find object file name, e.g. "User.cs"
				{
					matchFiles = GetAllFiles(assemblyProjectPath, objectType.Name + ".cs");
					existingObjectFiles.AddRange(matchFiles);

					if (existingObjectFiles.Count > 0)
						objectFilePath = new DirectoryInfo(existingObjectFiles[0] + "\\..\\").FullName + objectFileName;
					//else 
					//{
					//	objectFilePath = assemblyProjectPath + objectFileName;
					//}
				}
			}

			return objectFilePath;
		}

		public static IEnumerable<String> GetAllFiles(string path, string searchPattern)
		{
			return System.IO.Directory.EnumerateFiles(path, searchPattern)
									  .Union(System.IO.Directory.EnumerateDirectories(path)
									  .SelectMany(d =>
										{
											try
											{
												return GetAllFiles(d, searchPattern);
											}
											catch (UnauthorizedAccessException)
											{
												return Enumerable.Empty<String>();
											}
											catch (InvalidOperationException) // ran out of entries
											{
												return Enumerable.Empty<String>();
											}
											catch (PathTooLongException)
											{
												return Enumerable.Empty<String>();
											}
										}));
		}

		public static List<string> GetAllAccessibleFiles(string path, string searchPattern, bool ignoreCase = true)
		{
			List<string> allAccessibleFiles = GetAllAccessibleFiles(path);
			List<string> files = allAccessibleFiles.FindAll(file => file.Equals(searchPattern, StringComparison.CurrentCultureIgnoreCase));

			return files;
		}


		public static List<string> GetAllAccessibleFiles(string rootPath, List<string>? alreadyFound = null)
		{
			if (alreadyFound == null)
				alreadyFound = new List<string>();
			
			DirectoryInfo di = new DirectoryInfo(rootPath);
			var dirs = di.EnumerateDirectories();
			
			foreach (DirectoryInfo dir in dirs)
				if (!((dir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden))
					alreadyFound = GetAllAccessibleFiles(dir.FullName, alreadyFound);

			var files = Directory.GetFiles(rootPath);
			
			foreach (string s in files)
				alreadyFound.Add(s);

			return alreadyFound;
		}

		private static List<string> CreatePropertyNames(IEnumerable<IPropertyModel> propertyModels, IDictionary<string, PropertyModelInfo> propertiesToExclude)
		{
			List<string> result = new List<string>();

			foreach (IPropertyModel propertyModel in propertyModels)
			{
				PropertyModelInfo existingPropertyInfo;

				if (propertiesToExclude.TryGetValue(propertyModel.PropertyName, out existingPropertyInfo))
				{
					if (existingPropertyInfo.PropertyType != propertyModel.PropertyType) // include existing property names but with different type that should be implemented with new property access modifier.
						result.Add(propertyModel.PropertyName);
				}
				else
				{
					result.Add(propertyModel.PropertyName);
				}
			}

			return result;
		}

		private static string GenerateClassSegmentText(string splitter, params string[] classTextSegments)
		{
			string result = String.Empty;

			foreach (string text in classTextSegments)
			{
				if (!String.IsNullOrEmpty(result) && !String.IsNullOrEmpty(text))
					result += splitter;

				result += text ?? "";
			}

			return result;
		}

		private static string GenerateSegmentText(string prefix, string sufix, params string[] textSegments)
		{
			string result = String.Empty;

			foreach (string text in textSegments)
			{
				if (String.IsNullOrEmpty(text))
					continue;

				result += prefix + text + sufix;
			}

			return result;
		}

		private static string CreateSetAccessModifierString(IPropertyModel propertyModel)
		{
			string strSetAccessModifier = propertyModel.SetAccessModifier.ToLowerString() + " "; // ToString("F").Replace(",", "").ToLower();

			if (propertyModel.SetAccessModifier != propertyModel.GetAccessModifier)
			{
				if (propertyModel.GetAccessModifier == AccessModifier.Public)
				{
					return strSetAccessModifier;
				}
				else if (propertyModel.GetAccessModifier == AccessModifier.Protected && (propertyModel.SetAccessModifier == (AccessModifier.Protected | AccessModifier.Internal) ||
																						 propertyModel.SetAccessModifier == AccessModifier.Internal ||
																						 propertyModel.SetAccessModifier == AccessModifier.Private))
				{
					return strSetAccessModifier;
				}
				else if (propertyModel.GetAccessModifier == AccessModifier.Internal && (propertyModel.SetAccessModifier == (AccessModifier.Protected | AccessModifier.Internal) ||
																						propertyModel.SetAccessModifier == AccessModifier.Protected ||
																						propertyModel.SetAccessModifier == AccessModifier.Private))
				{
					return strSetAccessModifier;
				}
				else if (propertyModel.GetAccessModifier == (AccessModifier.Protected | AccessModifier.Internal) && (propertyModel.SetAccessModifier == AccessModifier.Protected ||
																													 propertyModel.SetAccessModifier == AccessModifier.Internal ||
																													 propertyModel.SetAccessModifier == AccessModifier.Private))
				{
					return strSetAccessModifier;
				}
			}

			return String.Empty; // no need for extra access modifier
		}
	}

	internal class PropertyModelInfo
	{
		public PropertyModelInfo(Type objectType, Type propertyType, Type fieldType)
		{
			this.ObjectType = objectType;
			this.PropertyType = propertyType;
			this.FieldType = fieldType;
		}

		public Type ObjectType { get; private set; }
		public Type PropertyType { get; private set; }
		public Type FieldType { get; private set; }
	}
}