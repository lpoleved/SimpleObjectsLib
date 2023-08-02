using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using Simple;
using Simple.Collections;
using Simple.Modeling;

namespace Simple.Controls
{
    /// <summary>
    /// Contains dictionary of the EditorBindingPolicies and related EditorComponents by internaly generated key. 
    /// If EditorComponent is RepositoryItem key is stored in RepositoryItem.Name property while DevExpress editor's use a copy of the RepositoryItem
    /// so storing RepositoryItem instance and metching by editors evenet given RepositoryItem is not usefull.
    /// </summary>
    public class EditorBindingPolicyDictionary
    {
        private SymetricalDictionary<int, EditorBindingPolicy> editorBindingPoliciesByComponentKey = new SymetricalDictionary<int, EditorBindingPolicy>();
        private Dictionary<IPropertyModel, List<EditorBindingPolicy>> editorBindingPolicyListsByObjectPropertyIndex = new Dictionary<IPropertyModel, List<EditorBindingPolicy>>();
        //private SymetricalDictionary<string, EditorBindingPolicy> editorBindingPoliciesByPropertyName = new SymetricalDictionary<string, EditorBindingPolicy>();
        private SymetricalDictionary<int, Component> componentsByComponentKey = new SymetricalDictionary<int, Component>();
        private UniqueKeyGenerator<int> componentKeyGenerator = new UniqueKeyGenerator<int>();

        public ICollection<EditorBindingPolicy> EditorBindingPolices
        {
            get { return this.editorBindingPoliciesByComponentKey.ValuesByKey.Values; }
        }

        public ICollection<Component> EditorComponents
        {
            get { return this.componentsByComponentKey.ValuesByKey.Values; }
        }

        public ICollection<IPropertyModel> ObjectPropertyIndexes
        {
            get { return this.editorBindingPolicyListsByObjectPropertyIndex.Keys; }
        }

        public int Set(Component editorComponent, IPropertyModel propertyModel, EditorBindingPolicy editorBindingPolicy)
        {
            int componentKey = this.componentKeyGenerator.CreateKey();
            List<EditorBindingPolicy> editPanelPolicyList = null;

            if (editorComponent is BaseEdit)
            {
                (editorComponent as BaseEdit).Properties.Name = componentKey.ToString();
            }
            else if (editorComponent is RepositoryItem)
            {
                (editorComponent as RepositoryItem).Name = componentKey.ToString();
            }

            if (!this.editorBindingPolicyListsByObjectPropertyIndex.TryGetValue(propertyModel, out editPanelPolicyList))
            {
                editPanelPolicyList = new List<EditorBindingPolicy>();
                this.editorBindingPolicyListsByObjectPropertyIndex.Add(propertyModel, editPanelPolicyList);
            }

            editPanelPolicyList.Add(editorBindingPolicy);

            this.editorBindingPoliciesByComponentKey.Add(componentKey, editorBindingPolicy);
            
            if (this.componentsByComponentKey.ContainsValue(editorComponent))
                this.componentsByComponentKey.RemoveByValue(editorComponent);

            this.componentsByComponentKey.Add(componentKey, editorComponent);

            return componentKey;
        }

        public EditorBindingPolicy GetEditorBindingPolicyByEditorComponent(Component editorComponent)
        {
            int componentKey = 0;
            EditorBindingPolicy value;

            if (editorComponent is BaseEdit)
            {
                BaseEdit baseEdit = editorComponent as BaseEdit;

                if (!baseEdit.Properties.Name.IsNullOrEmpty())
                    componentKey = Conversion.TryChangeType<int>(baseEdit.Properties.Name);
            }
            else if (editorComponent is RepositoryItem)
            {
                RepositoryItem repositoryItem = editorComponent as RepositoryItem;

                if (!repositoryItem.Name.IsNullOrEmpty())
                    componentKey = Conversion.TryChangeType<int>(repositoryItem.Name);
            }
            else
            {
                componentKey = this.componentsByComponentKey.GetKeyByValue(editorComponent);
            }

            value = this.editorBindingPoliciesByComponentKey.GetValueByKey(componentKey);

            return value;
        }

        public bool ContainsPropertyBinding(Type objectType, IPropertyModel propertyModel)
        {
            IList<EditorBindingPolicy> editorBindingPolicyList = this.GetEditorBindingPolicyListByObjectPropertyModel(propertyModel);

            if (editorBindingPolicyList == null)
                return false;

            foreach (var item in editorBindingPolicyList)
                if (objectType.IsSameOrSubclassOf(item.ObjectType))
                    return true;

            return false;
        }

        public IList<EditorBindingPolicy>? GetEditorBindingPolicyListByObjectPropertyModel(IPropertyModel propertyModel)
        {
            List<EditorBindingPolicy>? value = null;

            if (propertyModel == null)
                return value;

            this.editorBindingPolicyListsByObjectPropertyIndex.TryGetValue(propertyModel, out value);

            if (value != null)
            {
                return value.AsReadOnly();
            }
            else
            {
                return null;
            }
        }

        //public EditorBindingPolicy GetValueByPropertyName(string propertyName)
        //{
        //    EditorBindingPolicy value = this.editorBindingPoliciesByPropertyName.GetValueByKey(propertyName);
        //    return value;
        //}

        public bool Remove(EditorBindingPolicy editorBindingPolicy)
        {
            if (editorBindingPolicy == null)
                return false;
            
            if (this.editorBindingPoliciesByComponentKey.ContainsValue(editorBindingPolicy))
            {
                int componentKey = this.editorBindingPoliciesByComponentKey.GetKeyByValue(editorBindingPolicy);

                this.editorBindingPoliciesByComponentKey.RemoveByKey(componentKey);
                this.componentsByComponentKey.RemoveByKey(componentKey);

                List<IPropertyModel> objectPropertyIndexesWithEmptyEditorBindingPolicyList = new List<IPropertyModel>();

                foreach (var dictionaryItem in this.editorBindingPolicyListsByObjectPropertyIndex)
                    if (dictionaryItem.Value.Remove(editorBindingPolicy) && dictionaryItem.Value.Count == 0)
                        objectPropertyIndexesWithEmptyEditorBindingPolicyList.Add(dictionaryItem.Key);

                foreach (IPropertyModel propertyModel in objectPropertyIndexesWithEmptyEditorBindingPolicyList)
                    this.editorBindingPolicyListsByObjectPropertyIndex.Remove(propertyModel);

                return true;
            }

            return false;
        }
    }
}
