using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace GGGC.Admin.MultiThreadedBusyIndicator
{
    public delegate Visual CreateContentFunction();

    public class BackgroundVisualHost : FrameworkElement
    {
        #region Private Fields
        private ThreadedVisualHelper _threadedHelper = null;
        private HostVisual _hostVisual = null;
        #endregion

        public event EventHandler BusyTextChanged;

        #region BusyText Property
        public static readonly DependencyProperty BusyTextProperty = DependencyProperty.Register(
            "BusyText",
            typeof(string),
            typeof(BackgroundVisualHost),
            new FrameworkPropertyMetadata(string.Empty,
                FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets if the BusyIndicator is being shown.
        /// </summary>
        public string BusyText
        {
            get { return (string)GetValue(BusyTextProperty); }
            set { SetValue(BusyTextProperty, value); }
        }
        #endregion BusyText Property

        #region IsContentShowingProperty
        /// <summary>
        /// Identifies the IsContentShowing dependency property.
        /// </summary>
        public static readonly DependencyProperty IsContentShowingProperty = DependencyProperty.Register(
            "IsContentShowing",
            typeof(bool),
            typeof(BackgroundVisualHost),
            new FrameworkPropertyMetadata(false, OnIsContentShowingChanged));

        /// <summary>
        /// Gets or sets if the content is being displayed.
        /// </summary>
        public bool IsContentShowing
        {
            get { return (bool)GetValue(IsContentShowingProperty); }
            set { SetValue(IsContentShowingProperty, value); }
        }

        static void OnIsContentShowingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BackgroundVisualHost bvh = (BackgroundVisualHost)d;

            if (bvh.CreateContent != null)
            {
                if ((bool)e.NewValue)
                {
                    bvh.CreateContentHelper();
                }
                else
                {
                    bvh.HideContentHelper();
                }
            }
        }
        #endregion

        #region CreateContent Property
        /// <summary>
        /// Identifies the CreateContent dependency property.
        /// </summary>
        public static readonly DependencyProperty CreateContentProperty = DependencyProperty.Register(
            "CreateContent",
            typeof(CreateContentFunction),
            typeof(BackgroundVisualHost),
            new FrameworkPropertyMetadata(OnCreateContentChanged));

        /// <summary>
        /// Gets or sets the function used to create the visual to display in a background thread.
        /// </summary>
        public CreateContentFunction CreateContent
        {
            get { return (CreateContentFunction)GetValue(CreateContentProperty); }
            set { SetValue(CreateContentProperty, value); }
        }

        static void OnCreateContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BackgroundVisualHost bvh = (BackgroundVisualHost)d;

            if (bvh.IsContentShowing)
            {
                bvh.HideContentHelper();
                if (e.NewValue != null)
                    bvh.CreateContentHelper();
            }
        }
        #endregion

        protected override int VisualChildrenCount
        {
            get { return _hostVisual != null ? 1 : 0; }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (_hostVisual != null && index == 0)
                return _hostVisual;

            throw new IndexOutOfRangeException("index");
        }

        protected override System.Collections.IEnumerator LogicalChildren
        {
            get
            {
                if (_hostVisual != null)
                    yield return _hostVisual;
            }
        }

        private void CreateContentHelper()
        {
            _threadedHelper = new ThreadedVisualHelper(CreateContent, SafeInvalidateMeasure, this);
            _hostVisual = _threadedHelper.HostVisual;
        }

        private void SafeInvalidateMeasure()
        {
            Dispatcher.BeginInvoke(new Action(InvalidateMeasure), DispatcherPriority.Loaded);
        }

        private void HideContentHelper()
        {
            if (_threadedHelper != null)
            {
                _threadedHelper.Exit();
                _threadedHelper = null;
                InvalidateMeasure();
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (_threadedHelper != null)
                return _threadedHelper.DesiredSize;

            return base.MeasureOverride(availableSize);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property.Equals(BusyTextProperty) && this.BusyTextChanged != null)
                this.BusyTextChanged(this, new EventArgs());

        }


        private class ThreadedVisualHelper
        {
            private object _localSync = new object();
            private readonly HostVisual _hostVisual = null;
            private readonly AutoResetEvent _sync =
                new AutoResetEvent(false);
            private readonly CreateContentFunction _createContent;
            private readonly Action _invalidateMeasure;
            private readonly BackgroundVisualHost _parent;

            public HostVisual HostVisual { get { return _hostVisual; } }
            public Size DesiredSize { get; private set; }
            private Dispatcher Dispatcher { get; set; }
            private CancellationTokenSource updatePropsCTS;

            public ThreadedVisualHelper(
                CreateContentFunction createContent,
                Action invalidateMeasure,
                BackgroundVisualHost parent)
            {
                _parent = parent;
                _hostVisual = new HostVisual();
                _createContent = createContent;
                _invalidateMeasure = invalidateMeasure;

                Thread backgroundUi = new Thread(CreateAndShowContent);
                backgroundUi.SetApartmentState(ApartmentState.STA);
                backgroundUi.Name = "BackgroundVisualHostThread";
                backgroundUi.IsBackground = true;
                backgroundUi.Start();
                updatePropsCTS = new CancellationTokenSource();
                _sync.WaitOne();
            }

            public void Exit()
            {
                updatePropsCTS.Cancel();
                _target = null;
                _parent.BusyTextChanged -= _parent_BusyTextChanged;
                Dispatcher.BeginInvokeShutdown(DispatcherPriority.Send);
            }

            BridgeControl _target;
            private void CreateAndShowContent()
            {
                Dispatcher = Dispatcher.CurrentDispatcher;
                VisualTargetPresentationSource source =
                    new VisualTargetPresentationSource(_hostVisual);
                _sync.Set();
                source.RootVisual = _createContent();
                _target = source.RootVisual as BridgeControl;
                _parent.BusyTextChanged += _parent_BusyTextChanged;
                UpdateBusyText();
                DesiredSize = source.DesiredSize;
                _invalidateMeasure();

                Dispatcher.Run();
                source.Dispose();
            }


            private void UpdateBusyText()
            {
                try
                {
                    string s = null;
                    var token = updatePropsCTS.Token;
                    if (!updatePropsCTS.IsCancellationRequested && _parent != null && _parent.Dispatcher != null && !_parent.Dispatcher.HasShutdownStarted && !_parent.Dispatcher.HasShutdownFinished)
                        _parent.Dispatcher.Invoke(() => s = _parent.BusyText, DispatcherPriority.Render, updatePropsCTS.Token, TimeSpan.FromSeconds(1));
                    if (!updatePropsCTS.IsCancellationRequested && _target != null && _target.Dispatcher != null && !_target.Dispatcher.HasShutdownStarted && !_target.Dispatcher.HasShutdownFinished)
                        _target.Dispatcher.Invoke(() => _target.BusyText = s, DispatcherPriority.Render, updatePropsCTS.Token, TimeSpan.FromSeconds(1));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message); 
                  
                }
                    
            }

            void _parent_BusyTextChanged(object sender, EventArgs e)
            {
                UpdateBusyText();
            }
        }
    }
}
