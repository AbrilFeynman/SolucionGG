using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.MultiThreadedBusyIndicator
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Collections.Specialized;
	using System.ComponentModel;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Runtime.Serialization;
	using System.Runtime.CompilerServices;
#if WinRT
	using Windows.ApplicationModel;
	using Windows.UI.Core;
	using Windows.UI.Xaml;
#else
	using System.Diagnostics;
	using System.Windows;
	using System.Windows.Threading;
#endif
#if !SILVERLIGHT || SL5 || WP8
	using System.Threading.Tasks;
	using System.Reflection;
#endif

	/// <summary>
	///   Enables easy marshalling of code to the UI thread.
	/// </summary>
	public static class Execute
	{
#if WinRT
		static CoreDispatcher dispatcher;
#else
		static Dispatcher dispatcher;
#endif
		static bool? inDesignMode;
		static Action<System.Action> executor = action => action();

		/// <summary>
		///   Indicates whether or not the framework is in design-time mode.
		/// </summary>
		public static bool InDesignMode
		{
			get
			{
				if (inDesignMode == null)
				{
#if WinRT
					inDesignMode = DesignMode.DesignModeEnabled;
#elif SILVERLIGHT
					inDesignMode = DesignerProperties.IsInDesignTool;
#else
					var prop = DesignerProperties.IsInDesignModeProperty;
					inDesignMode = (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;

					if (!inDesignMode.GetValueOrDefault(false) && Process.GetCurrentProcess().ProcessName.StartsWith("devenv", StringComparison.Ordinal))
						inDesignMode = true;
#endif
				}

				return inDesignMode.GetValueOrDefault(false);
			}
		}

		/// <summary>
		///   Initializes the framework using the current dispatcher.
		/// </summary>
		public static void InitializeWithDispatcher()
		{
#if SILVERLIGHT
			dispatcher = System.Windows.Deployment.Current.Dispatcher;
#elif WinRT
			dispatcher = Window.Current.Dispatcher;
#else
			dispatcher = Dispatcher.CurrentDispatcher;
#endif
			executor = null;
		}

		/// <summary>
		///   Resets the executor to use a non-dispatcher-based action executor.
		/// </summary>
		public static void ResetWithoutDispatcher()
		{
			executor = action => action();
			dispatcher = null;
		}

		/// <summary>
		///   Sets a custom UI thread marshaller.
		/// </summary>
		/// <param name="marshaller">The marshaller.</param>
		[Obsolete]
		public static void SetUIThreadMarshaller(Action<System.Action> marshaller)
		{
			executor = marshaller;
			dispatcher = null;
		}

		static void ValidateDispatcher()
		{
			if (dispatcher == null)
				throw new InvalidOperationException("Not initialized with dispatcher.");
		}

		/// <summary>
		///   Executes the action on the UI thread asynchronously.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		public static void BeginOnUIThread(this System.Action action)
		{
			ValidateDispatcher();
#if WinRT
			var dummy = dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action());
#else
			dispatcher.BeginInvoke(action);
#endif
		}

#if !SILVERLIGHT || SL5 || WP8
		/// <summary>
		///   Executes the action on the UI thread asynchronously.
		/// </summary>
		/// <param name = "action">The action to execute.</param>
		public static Task OnUIThreadAsync(this System.Action action)
		{
			ValidateDispatcher();
#if WinRT
			return dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action()).AsTask();
#elif NET45
			return dispatcher.InvokeAsync(action).Task;
#else
			var taskSource = new TaskCompletionSource<object>();
			System.Action method = () =>
			{
				try
				{
					action();
					taskSource.SetResult(null);
				}
				catch (Exception ex)
				{
					taskSource.SetException(ex);
				}
			};
			dispatcher.BeginInvoke(method);
			return taskSource.Task;
#endif
		}
#endif

		static bool CheckAccess()
		{
#if WinRT
			return dispatcher == null || Window.Current != null;
#else
			return dispatcher == null || dispatcher.CheckAccess();
#endif
		}

		/// <summary>
		///   Executes the action on the UI thread.
		/// </summary>
		/// <param name = "action">The action to execute.</param>
		public static void OnUIThread(this System.Action action)
		{
			if (executor != null)
				executor(action);
			else if (CheckAccess())
				action();
			else
#if !SILVERLIGHT || SL5 || WP8
				OnUIThreadAsync(action).Wait();
#else
			{
				var waitHandle = new System.Threading.ManualResetEvent(false);
				Exception exception = null;
				BeginOnUIThread(() => {
					try {
						action();
					}
					catch (Exception ex) {
						exception = ex;
					}
					waitHandle.Set();
				});
				waitHandle.WaitOne();
				if (exception != null)
					throw new System.Reflection.TargetInvocationException("An error occurred while dispatching a call to the UI Thread", exception);
			}
#endif
		}
	}

	/// <summary>
	///   Extends <see cref = "INotifyPropertyChanged" /> such that the change event can be raised by external parties.
	/// </summary>
	public interface INotifyPropertyChangedEx : INotifyPropertyChanged
	{
		/// <summary>
		///   Enables/Disables property change notification.
		/// </summary>
		bool IsNotifying { get; set; }

		/// <summary>
		///   Notifies subscribers of the property change.
		/// </summary>
		/// <param name = "propertyName">Name of the property.</param>
		void NotifyOfPropertyChange(string propertyName);

		/// <summary>
		///   Raises a change notification indicating that all bindings should be refreshed.
		/// </summary>
		void Refresh();
	}

	/// <summary>
	///   A base class that implements the infrastructure for property change notification and automatically performs UI thread marshalling.
	/// </summary>
#if !SILVERLIGHT && !WinRT
	[Serializable]
#endif
	public class PropertyChangedBase : INotifyPropertyChangedEx
	{
		/// <summary>
		///   Creates an instance of <see cref = "PropertyChangedBase" />.
		/// </summary>
		public PropertyChangedBase()
		{
			IsNotifying = true;
		}

		/// <summary>
		///   Occurs when a property value changes.
		/// </summary>
#if !SILVERLIGHT && !WinRT
		[field: NonSerialized]
#endif
		public event PropertyChangedEventHandler PropertyChanged = delegate { };

#if !SILVERLIGHT && !WinRT
		[field: NonSerialized]
#endif
		bool isNotifying; //serializator try to serialize even autogenerated fields

		/// <summary>
		///   Enables/Disables property change notification.
		/// </summary>
#if !WinRT
		[Browsable(false)]
#endif
		public bool IsNotifying
		{
			get { return isNotifying; }
			set { isNotifying = value; }
		}

		/// <summary>
		///   Raises a change notification indicating that all bindings should be refreshed.
		/// </summary>
		public void Refresh()
		{
			NotifyOfPropertyChange(string.Empty);
		}

		/// <summary>
		///   Notifies subscribers of the property change.
		/// </summary>
		/// <param name = "propertyName">Name of the property.</param>
#if WinRT || NET45
		public virtual void NotifyOfPropertyChange([CallerMemberName]string propertyName = "") {
#else
		public virtual void NotifyOfPropertyChange(string propertyName)
		{
#endif
			if (IsNotifying)
			{
				Execute.OnUIThread(() => OnPropertyChanged(new PropertyChangedEventArgs(propertyName)));
			}
		}

		/// <summary>
		///   Notifies subscribers of the property change.
		/// </summary>
		/// <typeparam name = "TProperty">The type of the property.</typeparam>
		/// <param name = "property">The property expression.</param>
		public void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property)
		{
			NotifyOfPropertyChange(property.GetMemberInfo().Name);
		}

		/// <summary>
		///   Raises the <see cref="E:PropertyChanged" /> event directly.
		/// </summary>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		/// <summary>
		/// Called when the object is deserialized.
		/// </summary>
		/// <param name="c">The streaming context.</param>
		[OnDeserialized]
		public void OnDeserialized(StreamingContext c)
		{
			IsNotifying = true;
		}

		/// <summary>
		/// Used to indicate whether or not the IsNotifying property is serialized to Xml.
		/// </summary>
		/// <returns>Whether or not to serialize the IsNotifying property. The default is false.</returns>
		public virtual bool ShouldSerializeIsNotifying()
		{
			return false;
		}
	}

	/// <summary>
	///   Represents a collection that is observable.
	/// </summary>
	/// <typeparam name = "T">The type of elements contained in the collection.</typeparam>
	public interface IObservableCollection<T> : IList<T>, INotifyPropertyChangedEx, INotifyCollectionChanged
	{
		/// <summary>
		///   Adds the range.
		/// </summary>
		/// <param name = "items">The items.</param>
		void AddRange(IEnumerable<T> items);

		/// <summary>
		///   Removes the range.
		/// </summary>
		/// <param name = "items">The items.</param>
		void RemoveRange(IEnumerable<T> items);
	}

	/// <summary>
	/// A base collection class that supports automatic UI thread marshalling.
	/// </summary>
	/// <typeparam name="T">The type of elements contained in the collection.</typeparam>
#if !SILVERLIGHT && !WinRT
	[Serializable]
#endif
	public class BindableCollection<T> : ObservableCollection<T>, IObservableCollection<T>
	{

		/// <summary>
		///   Initializes a new instance of the <see cref = "Caliburn.Micro.BindableCollection{T}" /> class.
		/// </summary>
		public BindableCollection()
		{
			IsNotifying = true;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref = "Caliburn.Micro.BindableCollection{T}" /> class.
		/// </summary>
		/// <param name = "collection">The collection from which the elements are copied.</param>
		/// <exception cref = "T:System.ArgumentNullException">
		///   The <paramref name = "collection" /> parameter cannot be null.
		/// </exception>
		public BindableCollection(IEnumerable<T> collection)
			: base(collection)
		{
			IsNotifying = true;
		}

#if !SILVERLIGHT && !WinRT
		[field: NonSerialized]
#endif
		bool isNotifying; //serializator try to serialize even autogenerated fields

		/// <summary>
		///   Enables/Disables property change notification.
		/// </summary>
#if !WinRT
		[Browsable(false)]
#endif
		public bool IsNotifying
		{
			get { return isNotifying; }
			set { isNotifying = value; }
		}

		/// <summary>
		///   Notifies subscribers of the property change.
		/// </summary>
		/// <param name = "propertyName">Name of the property.</param>
#if WinRT || NET45
		public virtual void NotifyOfPropertyChange([CallerMemberName]string propertyName = "") {
#else
		public virtual void NotifyOfPropertyChange(string propertyName)
		{
#endif
			if (IsNotifying)
				Execute.OnUIThread(() => OnPropertyChanged(new PropertyChangedEventArgs(propertyName)));
		}

		/// <summary>
		///   Raises a change notification indicating that all bindings should be refreshed.
		/// </summary>
		public void Refresh()
		{
			Execute.OnUIThread(() =>
			{
				OnPropertyChanged(new PropertyChangedEventArgs("Count"));
				OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
			});
		}

		/// <summary>
		///   Inserts the item to the specified position.
		/// </summary>
		/// <param name = "index">The index to insert at.</param>
		/// <param name = "item">The item to be inserted.</param>
		protected override sealed void InsertItem(int index, T item)
		{
			Execute.OnUIThread(() => InsertItemBase(index, item));
		}

		/// <summary>
		///   Exposes the base implementation of the <see cref = "InsertItem" /> function.
		/// </summary>
		/// <param name = "index">The index.</param>
		/// <param name = "item">The item.</param>
		/// <remarks>
		///   Used to avoid compiler warning regarding unverifiable code.
		/// </remarks>
		protected virtual void InsertItemBase(int index, T item)
		{
			base.InsertItem(index, item);
		}

#if NET || WP8 || WinRT
	/// <summary>
	/// Moves the item within the collection.
	/// </summary>
	/// <param name="oldIndex">The old position of the item.</param>
	/// <param name="newIndex">The new position of the item.</param>
		protected sealed override void MoveItem(int oldIndex, int newIndex) {
			Execute.OnUIThread(() => MoveItemBase(oldIndex, newIndex));
		}

		/// <summary>
		/// Exposes the base implementation fo the <see cref="MoveItem"/> function.
		/// </summary>
		/// <param name="oldIndex">The old index.</param>
		/// <param name="newIndex">The new index.</param>
		/// <remarks>Used to avoid compiler warning regarding unverificable code.</remarks>
		protected virtual void MoveItemBase(int oldIndex, int newIndex) {
			base.MoveItem(oldIndex, newIndex);
		}
#endif

		/// <summary>
		///   Sets the item at the specified position.
		/// </summary>
		/// <param name = "index">The index to set the item at.</param>
		/// <param name = "item">The item to set.</param>
		protected override sealed void SetItem(int index, T item)
		{
			Execute.OnUIThread(() => SetItemBase(index, item));
		}

		/// <summary>
		///   Exposes the base implementation of the <see cref = "SetItem" /> function.
		/// </summary>
		/// <param name = "index">The index.</param>
		/// <param name = "item">The item.</param>
		/// <remarks>
		///   Used to avoid compiler warning regarding unverifiable code.
		/// </remarks>
		protected virtual void SetItemBase(int index, T item)
		{
			base.SetItem(index, item);
		}

		/// <summary>
		///   Removes the item at the specified position.
		/// </summary>
		/// <param name = "index">The position used to identify the item to remove.</param>
		protected override sealed void RemoveItem(int index)
		{
			Execute.OnUIThread(() => RemoveItemBase(index));
		}

		/// <summary>
		///   Exposes the base implementation of the <see cref = "RemoveItem" /> function.
		/// </summary>
		/// <param name = "index">The index.</param>
		/// <remarks>
		///   Used to avoid compiler warning regarding unverifiable code.
		/// </remarks>
		protected virtual void RemoveItemBase(int index)
		{
			base.RemoveItem(index);
		}

		/// <summary>
		///   Clears the items contained by the collection.
		/// </summary>
		protected override sealed void ClearItems()
		{
			Execute.OnUIThread(ClearItemsBase);
		}

		/// <summary>
		///   Exposes the base implementation of the <see cref = "ClearItems" /> function.
		/// </summary>
		/// <remarks>
		///   Used to avoid compiler warning regarding unverifiable code.
		/// </remarks>
		protected virtual void ClearItemsBase()
		{
			base.ClearItems();
		}

		/// <summary>
		///   Raises the <see cref = "E:System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged" /> event with the provided arguments.
		/// </summary>
		/// <param name = "e">Arguments of the event being raised.</param>
		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			if (IsNotifying)
			{
				base.OnCollectionChanged(e);
			}
		}

		/// <summary>
		///   Raises the PropertyChanged event with the provided arguments.
		/// </summary>
		/// <param name = "e">The event data to report in the event.</param>
		protected override void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (IsNotifying)
			{
				base.OnPropertyChanged(e);
			}
		}

		/// <summary>
		///   Adds the range.
		/// </summary>
		/// <param name = "items">The items.</param>
		public virtual void AddRange(IEnumerable<T> items)
		{
			Execute.OnUIThread(() =>
			{
				var previousNotificationSetting = IsNotifying;
				IsNotifying = false;
				var index = Count;
				foreach (var item in items)
				{
					InsertItemBase(index, item);
					index++;
				}
				IsNotifying = previousNotificationSetting;

				OnPropertyChanged(new PropertyChangedEventArgs("Count"));
				OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
			});
		}

		/// <summary>
		///   Removes the range.
		/// </summary>
		/// <param name = "items">The items.</param>
		public virtual void RemoveRange(IEnumerable<T> items)
		{
			Execute.OnUIThread(() =>
			{
				var previousNotificationSetting = IsNotifying;
				IsNotifying = false;
				foreach (var item in items)
				{
					var index = IndexOf(item);
					if (index >= 0)
					{
						RemoveItemBase(index);
					}
				}
				IsNotifying = previousNotificationSetting;

				OnPropertyChanged(new PropertyChangedEventArgs("Count"));
				OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
			});
		}

		/// <summary>
		/// Called when the object is deserialized.
		/// </summary>
		/// <param name="c">The streaming context.</param>
		[OnDeserialized]
		public void OnDeserialized(StreamingContext c)
		{
			IsNotifying = true;
		}

		/// <summary>
		/// Used to indicate whether or not the IsNotifying property is serialized to Xml.
		/// </summary>
		/// <returns>Whether or not to serialize the IsNotifying property. The default is false.</returns>
		public virtual bool ShouldSerializeIsNotifying()
		{
			return false;
		}
	}

	public static class ExtensionMethods
	{
		/// <summary>
		/// Get's the name of the assembly.
		/// </summary>
		/// <param name="assembly">The assembly.</param>
		/// <returns>The assembly's name.</returns>
		public static string GetAssemblyName(this Assembly assembly)
		{
			return assembly.FullName.Remove(assembly.FullName.IndexOf(','));
		}

		/// <summary>
		/// Gets all the attributes of a particular type.
		/// </summary>
		/// <typeparam name="T">The type of attributes to get.</typeparam>
		/// <param name="member">The member to inspect for attributes.</param>
		/// <param name="inherit">Whether or not to search for inherited attributes.</param>
		/// <returns>The list of attributes found.</returns>
		public static IEnumerable<T> GetAttributes<T>(this MemberInfo member, bool inherit)
		{
#if WinRT
			return member.GetCustomAttributes(inherit).OfType<T>();
#else
			return Attribute.GetCustomAttributes(member, inherit).OfType<T>();
#endif
		}

		/// <summary>
		/// Applies the action to each element in the list.
		/// </summary>
		/// <typeparam name="T">The enumerable item's type.</typeparam>
		/// <param name="enumerable">The elements to enumerate.</param>
		/// <param name="action">The action to apply to each item in the list.</param>
		public static void Apply<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			foreach (var item in enumerable)
			{
				action(item);
			}
		}

		/// <summary>
		/// Converts an expression into a <see cref="MemberInfo"/>.
		/// </summary>
		/// <param name="expression">The expression to convert.</param>
		/// <returns>The member info.</returns>
		public static MemberInfo GetMemberInfo(this System.Linq.Expressions.Expression expression)
		{
			var lambda = (LambdaExpression)expression;

			MemberExpression memberExpression;
			if (lambda.Body is UnaryExpression)
			{
				var unaryExpression = (UnaryExpression)lambda.Body;
				memberExpression = (MemberExpression)unaryExpression.Operand;
			}
			else
			{
				memberExpression = (MemberExpression)lambda.Body;
			}

			return memberExpression.Member;
		}

#if WINDOWS_PHONE && !WP8
		//Method missing in WP7.1 Linq

		/// <summary>
		/// Merges two sequences by using the specified predicate function.
		/// </summary>
		/// <typeparam name="TFirst">The type of the elements of the first input sequence.</typeparam>
		/// <typeparam name="TSecond">The type of the elements of the second input sequence.</typeparam>
		/// <typeparam name="TResult">The type of the elements of the result sequence.</typeparam>
		/// <param name="first">The first sequence to merge.</param>
		/// <param name="second">The second sequence to merge.</param>
		/// <param name="resultSelector"> A function that specifies how to merge the elements from the two sequences.</param>
		/// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains merged elements of two input sequences.</returns>
		public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector) {
			if (first == null) {
				throw new ArgumentNullException("first");
			}

			if (second == null) {
				throw new ArgumentNullException("second");
			}

			if (resultSelector == null) {
				throw new ArgumentNullException("resultSelector");
			}

			var enumFirst = first.GetEnumerator();
			var enumSecond = second.GetEnumerator();

			while (enumFirst.MoveNext() && enumSecond.MoveNext()) {
				yield return resultSelector(enumFirst.Current, enumSecond.Current);
			}
		}
#endif

#if WinRT
		/// <summary>
		/// Gets a collection of the public types defined in this assembly that are visible outside the assembly.
		/// </summary>
		/// <param name="assembly">The assembly.</param>
		/// <returns>A collection of the public types defined in this assembly that are visible outside the assembly.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static IEnumerable<Type> GetExportedTypes(this Assembly assembly) {
			if (assembly == null)
				throw new ArgumentNullException("assembly");

			return assembly.ExportedTypes;
		}

		/// <summary>
		/// Returns a value that indicates whether the specified type can be assigned to the current type.
		/// </summary>
		/// <param name="target">The target type</param>
		/// <param name="type">The type to check.</param>
		/// <returns>true if the specified type can be assigned to this type; otherwise, false.</returns>
		public static bool IsAssignableFrom(this Type target, Type type) {
			return target.GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
		}
#endif
	}

}
