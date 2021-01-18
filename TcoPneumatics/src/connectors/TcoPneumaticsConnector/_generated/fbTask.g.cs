using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoPneumatics
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "fbTask", "TcoPneumatics", TypeComplexityEnum.Complex)]
	public partial class fbTask : Vortex.Connector.IVortexObject, IfbTask, IShadowfbTask, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		public string Symbol
		{
			get;
			protected set;
		}

		public string HumanReadable
		{
			get
			{
				return TcoPneumaticsTwinController.Translator.Translate(_humanReadable).Interpolate(this);
			}

			protected set
			{
				_humanReadable = value;
			}
		}

		protected string _humanReadable;
		Vortex.Connector.ValueTypes.OnlinerInt __taskState;
		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTaskState))]
		public Vortex.Connector.ValueTypes.OnlinerInt _taskState
		{
			get
			{
				return __taskState;
			}
		}

		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTaskState))]
		Vortex.Connector.ValueTypes.Online.IOnlineInt IfbTask._taskState
		{
			get
			{
				return _taskState;
			}
		}

		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTaskState))]
		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadowfbTask._taskState
		{
			get
			{
				return _taskState;
			}
		}

		public void LazyOnlineToShadow()
		{
			_taskState.Shadow = _taskState.LastValue;
		}

		public void LazyShadowToOnline()
		{
			_taskState.Cyclic = _taskState.Shadow;
		}

		public PlainfbTask CreatePlainerType()
		{
			var cloned = new PlainfbTask();
			return cloned;
		}

		protected PlainfbTask CreatePlainerType(PlainfbTask cloned)
		{
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
		partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexConstructorParameterless();
		protected Vortex.Connector.IVortexObject @Parent
		{
			get;
			set;
		}

		public Vortex.Connector.IVortexObject GetParent()
		{
			return this.@Parent;
		}

		private System.Collections.Generic.List<Vortex.Connector.IVortexObject> @Children
		{
			get;
			set;
		}

		public System.Collections.Generic.IEnumerable<Vortex.Connector.IVortexObject> @GetChildren()
		{
			return this.@Children;
		}

		public void AddChild(Vortex.Connector.IVortexObject vortexObject)
		{
			this.@Children.Add(vortexObject);
		}

		private System.Collections.Generic.List<Vortex.Connector.IValueTag> @ValueTags
		{
			get;
			set;
		}

		public System.Collections.Generic.IEnumerable<Vortex.Connector.IValueTag> GetValueTags()
		{
			return this.@ValueTags;
		}

		public void AddValueTag(Vortex.Connector.IValueTag valueTag)
		{
			this.@ValueTags.Add(valueTag);
		}

		protected Vortex.Connector.IConnector @Connector
		{
			get;
			set;
		}

		public Vortex.Connector.IConnector GetConnector()
		{
			return this.@Connector;
		}

		public void FlushPlainToOnline(TcoPneumatics.PlainfbTask source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoPneumatics.PlainfbTask source)
		{
			source.CopyPlainToShadow(this);
		}

		public void FlushShadowToOnline()
		{
			this.LazyShadowToOnline();
			this.Write();
		}

		public void FlushOnlineToShadow()
		{
			this.Read();
			this.LazyOnlineToShadow();
		}

		public void FlushOnlineToPlain(TcoPneumatics.PlainfbTask source)
		{
			this.Read();
			source.CopyCyclicToPlain(this);
		}

		protected System.String @SymbolTail
		{
			get;
			set;
		}

		public System.String GetSymbolTail()
		{
			return this.SymbolTail;
		}

		public System.String AttributeName
		{
			get
			{
				return TcoPneumaticsTwinController.Translator.Translate(_AttributeName).Interpolate(this);
			}

			set
			{
				_AttributeName = value;
			}
		}

		private System.String _AttributeName
		{
			get;
			set;
		}

		public fbTask(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@ValueTags = new System.Collections.Generic.List<Vortex.Connector.IValueTag>();
			this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			__taskState = @Connector.Online.Adapter.CreateINT(this, "", "_taskState");
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
			parent.AddChild(this);
		}

		public fbTask()
		{
			PexPreConstructorParameterless();
			__taskState = Vortex.Connector.IConnectorFactory.CreateINT();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcfbTask
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcfbTask()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IfbTask : Vortex.Connector.IVortexOnlineObject
	{
		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTaskState))]
		Vortex.Connector.ValueTypes.Online.IOnlineInt _taskState
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoPneumatics.PlainfbTask CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoPneumatics.PlainfbTask source);
		void FlushOnlineToPlain(TcoPneumatics.PlainfbTask source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowfbTask : Vortex.Connector.IVortexShadowObject
	{
		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTaskState))]
		Vortex.Connector.ValueTypes.Shadows.IShadowInt _taskState
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoPneumatics.PlainfbTask CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoPneumatics.PlainfbTask source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainfbTask : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		System.Int16 __taskState;
		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTaskState))]
		public System.Int16 _taskState
		{
			get
			{
				return __taskState;
			}

			set
			{
				if (__taskState != value)
				{
					__taskState = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_taskState)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoPneumatics.fbTask target)
		{
			target._taskState.Cyclic = _taskState;
		}

		public void CopyPlainToCyclic(TcoPneumatics.IfbTask target)
		{
			this.CopyPlainToCyclic((TcoPneumatics.fbTask)target);
		}

		public void CopyPlainToShadow(TcoPneumatics.fbTask target)
		{
			target._taskState.Shadow = _taskState;
		}

		public void CopyPlainToShadow(TcoPneumatics.IShadowfbTask target)
		{
			this.CopyPlainToShadow((TcoPneumatics.fbTask)target);
		}

		public void CopyCyclicToPlain(TcoPneumatics.fbTask source)
		{
			_taskState = source._taskState.LastValue;
		}

		public void CopyCyclicToPlain(TcoPneumatics.IfbTask source)
		{
			this.CopyCyclicToPlain((TcoPneumatics.fbTask)source);
		}

		public void CopyShadowToPlain(TcoPneumatics.fbTask source)
		{
			_taskState = source._taskState.Shadow;
		}

		public void CopyShadowToPlain(TcoPneumatics.IShadowfbTask source)
		{
			this.CopyShadowToPlain((TcoPneumatics.fbTask)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainfbTask()
		{
		}
	}
}