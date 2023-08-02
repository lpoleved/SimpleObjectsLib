using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Simple;
using Simple.Objects;
using Simple.Modeling;

namespace Simple.Objects.Controls
{
    public partial class ObjectCodeGeneratorControl : UserControl
    {
		public ObjectCodeGeneratorControl()
        {
            InitializeComponent();
        }

        [Category("General")]
        public bool IncludeSystemObjects { get; set; }

		public void GenerateSimpleObjectSourceCodes(SimpleObjectModelDiscovery modelDiscovery)
		{
			this.memoEdit.Text = String.Empty;
			this.memoEdit.Refresh();

			ObjectCodeGenerator.Generate(modelDiscovery, this.IncludeSystemObjects, this.ProgressReporter);
		}

		private void ProgressReporter(object state)
        {
			this.memoEdit.Text += state;
			this.memoEdit.Update();
        }
    }
}
