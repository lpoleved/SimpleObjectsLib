using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects
{
    public class GraphValidationResult
    {
        private bool isValidationErrorFormShown = false;
        public static GraphValidationResult DefaultSuccessResult = new GraphValidationResult(new SimpleObjectValidationResult(null, true, "Passed", null, TransactionRequestAction.Save), graphNode: null, errorColumnIndex: -1, message: "Passed");


		public GraphValidationResult(SimpleObjectValidationResult simpleObjectValidationResult, object graphNode, int errorColumnIndex)
            : this(simpleObjectValidationResult, graphNode, errorColumnIndex, message: null)
        {
        }

        public GraphValidationResult(SimpleObjectValidationResult simpleObjectValidationResult, object graphNode, int errorColumnIndex, string message)
        {
            this.SimpleObjectValidationResult = simpleObjectValidationResult;
            this.ErrorGraphColumnIndex = errorColumnIndex;
			this.GraphNode = graphNode;

            if (message != null)
            {
                this.Message = message;
            }
            else
            {
                this.Message = this.SimpleObjectValidationResult.Message;
            }
        }

        public bool IsValidationErrorFormShown
        {
            get
            {
                if (this.SimpleObjectValidationResult != null)
                    return this.SimpleObjectValidationResult.IsValidationErrorFormShown;

                return this.isValidationErrorFormShown;

            }

            set
            {
                if (this.SimpleObjectValidationResult != null)
				{
                    this.SimpleObjectValidationResult.IsValidationErrorFormShown = value;
                }
                else
				{
                    this.isValidationErrorFormShown = value;
				}
            }
        }

        public SimpleObjectValidationResult SimpleObjectValidationResult { get; set; }
        public int ErrorGraphColumnIndex { get; set; }
        public string Message { get; private set; }
		public object GraphNode { get; private set; }

        public bool Passed
        {
            get { return this.SimpleObjectValidationResult.Passed; }
        }
    }
}
