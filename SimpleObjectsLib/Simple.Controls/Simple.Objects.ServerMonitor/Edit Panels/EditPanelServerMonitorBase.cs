using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks.Dataflow;
using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using Simple;
using Simple.Modeling;
using Simple.Objects;
using Simple.SocketEngine;
using Simple.Objects.SocketProtocol;
using Simple.Controls;
using Simple.Objects.Controls;
using DevExpress.XtraRichEdit.Import.OpenXml;
using DevExpress.XtraGrid.Columns;

namespace Simple.Objects.ServerMonitor
{
	public partial class EditPanelServerMonitorBase : SystemEditPanelWithTabControl
	{
		private string tabPageText;
		private ActionBlock<PackageHexTextVisualInfo>? largePackageHexTextVisualizer = null;

		public EditPanelServerMonitorBase()
		{
			InitializeComponent();
			this.tabPageText = this.tabPageObjectName.Text;
		}

		protected new IMonitorSessionContext? Context => base.Context as IMonitorSessionContext;

		//protected string GetGraphName(int graphKey)
		//{
		//	if (this.Context is FormMain formMain)
		//		return formMain.GetGraphName(graphKey);

		//	return String.Empty;
		//}

		//protected string GetRelationName(int relationKey)
		//{
		//	if (this.Context is FormMain formMain)
		//		return formMain.GetRelationName(relationKey);

		//	return String.Empty;
		//}

		//protected string GetObjectName(int tableId, long objectId)
		//{
		//	if (this.Context is FormMain formMain)
		//		return formMain.GetObjectName(tableId, objectId);

		//	return String.Empty;
		//}

		//protected ServerObjectModelInfo? GetServerObjectModel(int tableId)
		//{
		//	ServerObjectModelInfo? result = null;

		//	if (this.Context is FormMain formMain)
		//		result = formMain.GetServerObjectModel(tableId);

		//	return result;
		//}

		protected string CreatePropertyIndexValuesString(ServerObjectModelInfo? objectModel, IEnumerable<PropertyIndexValuePair>? propertyIndexValues, int startIndex = 0)
		{
			string result = String.Empty;
			string splitter = String.Empty;

			if (propertyIndexValues != null)
			{
				for (int i = startIndex; i < propertyIndexValues.Count(); i++) // avoid Id
				{
					var propertyInfo = propertyIndexValues.ElementAt(i);
					IServerPropertyInfo? propertyModel = objectModel?[propertyInfo.PropertyIndex];
					string propertyValueString = this.GePropertyValueString(propertyModel, propertyInfo.PropertyValue); // SimpleObject.GetPropertyValueString(propertyModel, propertyValue);

					if (propertyModel != null)
						result += String.Format("{0}{1} {2}={3}", splitter, propertyInfo.PropertyIndex, propertyModel.PropertyName, propertyValueString); //, propertyTypeId);
					else
						result += String.Format("{0}{1}-{2}", splitter, propertyInfo.PropertyIndex, propertyValueString); //, propertyTypeId);
					
					splitter = "; ";
				}
			}

			return result;
		}

		protected string GePropertyValueString(IServerPropertyInfo propertyModel, object? propertyValue)
		{
			Type propertyType = PropertyTypes.GetPropertyType(propertyModel.PropertyTypeId);

			if (propertyType.IsEnum)
				return String.Format("{0}({1}.{2})", Conversion.TryChangeType<int>(propertyValue), propertyType.Name, Enum.GetName(propertyType, propertyValue ?? "null"));

			return propertyValue?.ValueToString() ?? String.Empty;
		}

		protected void SetPackageTextToControl(MemoEdit textControl, string text)
		{
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(() => this.OnSetPackageText(textControl, text)));
			else
				this.OnSetPackageText(textControl, text);
		}

		protected void OnSetPackageText(MemoEdit textControl, string text) => textControl.Text = text;


		protected string CreatePackageText(PackageInfo packageInfo)
		{
			string text;

			text = packageInfo.PackageLength.To7BitEncodedHexString() + "   <- Package length, " + this.GetByteText(packageInfo.PackageLengthSize) + "\r\n";
			text += $"  ---- Header, {this.GetByteText(packageInfo.HeaderSize)} ----\r\n";
			text += packageInfo.HeaderInfo.Value.To7BitEncodedHexString(out int headerInfoSize) + "   <- Header Info, " + this.GetByteText(headerInfoSize) + "\r\n";
			text += packageInfo.Key.To7BitEncodedHexString(out int requestIdSize) + "   <- RequestId, " + this.GetByteText(requestIdSize) + "\r\n";

			//if (packageInfo.HeaderInfo.PackageType != PackageType.Message)
			//	text += packageInfo.Token.To7BitEncodedHexString(out int tokenSize) + "   <- Token, " + this.GetByteText(tokenSize) + "\r\n";
			long bodyLength = packageInfo.PackageLength - packageInfo.HeaderSize;

			text += $"  ---- Package Args Body, {this.GetByteText(bodyLength)} ----\r\n";

			if (bodyLength > 0)
				text += BitConverter.ToString(packageInfo.Buffer, startIndex: packageInfo.PackageLengthSize + packageInfo.HeaderSize);

			return text;
		}

		protected GridColumn CreateGridColumn(string columnName) => this.CreateGridColumn(columnName, columnName);

		protected GridColumn CreateGridColumn(string columnName, string caption)
		{
			GridColumn column = new GridColumn();

			column.Name = column.FieldName = columnName;
			column.Caption = caption;
			column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			column.OptionsFilter.AllowFilter = false;
			column.Visible = true;

			return column;
		}

		protected string GetByteText(int value, string keyword = " Byte") => this.GetByteText((long)value);

		protected string GetByteText(long value, string keyword = " Byte")
		{
			return value.ToString() + keyword + ((value != 1) ? "s" : "");
		}
	}
}
