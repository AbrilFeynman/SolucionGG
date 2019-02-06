using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace GGGC.Admin.Support.Threading
{
    public class Async : INotifyPropertyChanged
    {
        #region Fields

        private CancellationTokenSource cancelTokenSource;
        private int iterations = 50;
        private int progressPercentage = 0;
        private string output;
        private bool startEnabled = true;
        private bool cancelEnabled = false;

        #endregion

        #region Bindable Properties

        public int Iterations
        {
            get { return iterations; }
            set
            {
                if (iterations != value)
                {
                    iterations = value;
                    RaisePropertyChanged("Iterations");
                }
            }
        }

        public int ProgressPercentage
        {
            get { return progressPercentage; }
            set
            {
                if (progressPercentage != value)
                {
                    progressPercentage = value;
                    RaisePropertyChanged("ProgressPercentage");
                }
            }
        }

        public string Output
        {
            get { return output; }
            set
            {
                if (output != value)
                {
                    output = value;
                    RaisePropertyChanged("Output");
                }
            }
        }

        public bool StartEnabled
        {
            get { return startEnabled; }
            set
            {
                if (startEnabled != value)
                {
                    startEnabled = value;
                    RaisePropertyChanged("StartEnabled");
                }
            }
        }

        public bool CancelEnabled
        {
            get { return cancelEnabled; }
            set
            {
                if (cancelEnabled != value)
                {
                    cancelEnabled = value;
                    RaisePropertyChanged("CancelEnabled");
                }
            }
        }

        #endregion

        #region Constructor

        public Async()
        {

        }

        #endregion

        #region Public Methods

        public void StartProcess()
        {
            cancelTokenSource = new CancellationTokenSource();
            var progress = new Progress<ProgressObject>(UpdateProgress);

            var task = RunTaskAsync(Iterations,
                cancelTokenSource.Token, progress);
            task.ContinueWith(TaskComplete);

            StartEnabled = false;
            CancelEnabled = true;
            Output = "";
        }

        public void CancelProcess()
        {
            cancelTokenSource.Cancel();
        }

        #endregion

        #region Task Methods

        private class ProgressObject
        {
            public int Percentage { get; set; }
            public string Message { get; set; }
        }

        private Task<int> RunTaskAsync(int iterations, CancellationToken cancelToken,
            IProgress<ProgressObject> progress)
        {
            var task = Task<int>.Factory.StartNew(() =>
            {
                //throw new Exception("Something Bad Happened");
                int result = 0;

                //SlowProcessor processor = new SlowProcessor(iterations);
                //foreach (var current in processor)
                //{
                //    #region Cancellation & Progress Reporting
                //    // Cancellation
                //    cancelToken.ThrowIfCancellationRequested();

                //    // Progress Reporting
                //    int percentageComplete =
                //        (int)((float)current / (float)iterations * 100);
                //    string updateMessage =
                //        string.Format("Iteration {0} of {1}", current, iterations);
                //    var progressData = new ProgressObject()
                //    {
                //        Percentage = percentageComplete,
                //        Message = updateMessage
                //    };
                //    progress.Report(progressData);
                //    #endregion

                //    result = current;
                //}
                return result;
            },
                cancelToken);
            return task;
        }

        private void TaskComplete(Task<int> task)
        {
            if (task.IsFaulted)
            {
                Output = task.Exception.InnerExceptions[0].Message;
            }
            else if (task.IsCanceled)
            {
                Output = "Canceled";
            }
            else if (task.IsCompleted)
            {
                Output = task.Result.ToString();
                ProgressPercentage = 0;
            }
            StartEnabled = true;
            CancelEnabled = false;
        }

        private void UpdateProgress(ProgressObject progress)
        {
            ProgressPercentage = progress.Percentage;
            Output = progress.Message;
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
