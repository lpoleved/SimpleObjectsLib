using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Simple;

namespace Simple.Controls
{
    public class FormBackgroundWorker : FormBackgroundWorker<object>
    {
    }

    public class FormBackgroundWorker<TResult> : FormBackgroundWorker<TResult, object>
    {
    }

    public partial class FormBackgroundWorker<TResult, TArgument> : XtraForm
    {
        private const int defaultCancelTimeout = 5000; // in miliseconds
        private BackgroundWorker worker = new BackgroundWorker();
        private Action<WorkerContext<TResult, TArgument>>? action = null;
        //private AsyncAction<WorkerContext<TResult, TArgument>>? asyncAction = null;
        private WorkerContext<TResult, TArgument> workerContext = new WorkerContext<TResult, TArgument>();
        private Timer cancelTimeoutTimer = new Timer();
        private Cursor? currentCursor;

        private bool showProgressBar = false;
        private string title = String.Empty;
        private bool activated = false;
        private bool canCancel = false;
        private bool running = false;

        public FormBackgroundWorker()
        {
            InitializeComponent();

            this.labelMessage.Text = String.Empty;
            this.worker.WorkerReportsProgress = true;
            this.ShowProgressBar = false;
            this.progressBar.Properties.Step = 1;
            this.worker.DoWork += new DoWorkEventHandler(this.WorkerDoWork);
            this.worker.ProgressChanged += new ProgressChangedEventHandler(this.WorkerProgressChanged);
            this.worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.WorkerRunCompleted);

            this.cancelTimeoutTimer.Interval = defaultCancelTimeout;
            this.cancelTimeoutTimer.Tick += new EventHandler(this.cancelTimeoutTimer_Tick);
        }

        public void Start(string title, Action<WorkerContext<TResult, TArgument>> action)
        {
            this.action = action;
            this.Start(title);
        }

        public void Start(string title, Action<WorkerContext<TResult>> action)
        {
            this.action = action;
            this.Start(title);
        }

        public void Start(string title, Action<WorkerContext> action)
        {
            this.action = action;
            this.Start(title);
        }

        //public void Start(string title, AsyncAction<WorkerContext<TResult, TArgument>> action)
        //{
        //    this.asyncAction = action;
        //    this.Start(title);
        //}

        //public void Start(string title, AsyncAction<WorkerContext<TResult>> action)
        //{
        //    this.asyncAction = action;
        //    this.Start(title);
        //}

        //public void Start(string title, AsyncAction<WorkerContext> action)
        //{
        //    this.asyncAction = action;
        //    this.Start(title);
        //}


        public bool ShowProgressBar
        {
            get { return this.showProgressBar; }
            
            set
            {
            	this.showProgressBar = value;
                this.progressBar.Visible = this.showProgressBar;
            }
        }
        
        public bool CanCancel
        {
            get { return this.canCancel; }
            
            set
            {
                this.canCancel = value;
                this.worker.WorkerSupportsCancellation = this.canCancel;
            }
        }

        public WorkerContext<TResult, TArgument> WorkerContext
        {
            get { return this.workerContext; }
        }

        private void Start(string title)
        {
            this.title = title;
            this.Text = this.title;
            this.ShowDialog();
        }

        private void WorkerRunCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            this.cancelTimeoutTimer.Stop();

            if (e.Error != null)
            {
                this.workerContext.Error = true;
                this.workerContext.Message = e.Error.Message;
            }
            else if (e.Cancelled)
            {
                this.workerContext.Canceled = true;
            }
            else
            {
                //this.workerContext.Error = false;
                //this.workerContext.Message = String.Empty;
                this.workerContext.Canceled = false;
            }

            this.Cursor = this.currentCursor;
            this.running = false;
            this.Close();
        }

        private void WorkerProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage > -1)
            {
                if (this.ShowProgressBar)
                    this.progressBar.EditValue = e.ProgressPercentage;
                else
                    this.Text = String.Format("{0} {1}%", this.title, e.ProgressPercentage);
            }
            else
            {
                this.Text = this.title;
            }

            if (e.UserState != null && e.UserState is String)
                this.labelMessage.Text = e.UserState.ToString();
        }

        private void WorkerDoWork(object? sender, DoWorkEventArgs e)
        {
            this.workerContext.Worker = this.worker;
            this.workerContext.DoWorkArgs = e;

            if (this.action != null)
                this.action(this.workerContext);
            //else if (this.asyncAction != null)
            //    await this.asyncAction(this.workerContext);
        }

        private async void FormBackgroundWorker_Activated(object? sender, EventArgs e)
        {
            if (!activated)
            {
                this.activated = true;
                this.buttonCloseCancel.Enabled = this.canCancel;
                this.running = true;

                if (this.action != null)
                    this.worker.RunWorkerAsync();
                else // if (this.asyncAction != null)
                    await this.worker.RunWorkerTaskAsync();
            }
        }

        private void FormBackgroundWorker_FormClosing(object? sender, FormClosingEventArgs e)
        {
            e.Cancel = this.running;
        }

        private void buttonCloseCancel_Click(object? sender, EventArgs e)
        {
            this.currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            this.worker.CancelAsync();
            this.cancelTimeoutTimer.Start();
        }

        private void cancelTimeoutTimer_Tick(object? sender, EventArgs e)
        {
            this.WorkerRunCompleted(this, new RunWorkerCompletedEventArgs(null, null, cancelled: true));
        }
    }
}