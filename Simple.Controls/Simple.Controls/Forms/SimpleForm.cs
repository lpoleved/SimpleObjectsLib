using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using DevExpress.Skins.XtraForm;

namespace Simple.Controls
{
	public class SimpleForm : XtraForm, ISimpleForm
	{
		public SimpleForm()
		{
		}

		FormPainter ISimpleForm.GetPainter()
		{
			return this.FormPainter;
		}
	}

	public interface ISimpleForm
	{
		FormPainter GetPainter();
	}
}
