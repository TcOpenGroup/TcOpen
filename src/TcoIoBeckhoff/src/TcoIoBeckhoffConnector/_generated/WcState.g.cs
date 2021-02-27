using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoIoBeckhoff
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "WcState", "TcoIoBeckhoff", TypeComplexityEnum.Complex)]
	public partial class WcState : Vortex.Connector.IVortexObject, IWcState, IShadowWcState, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
				return TcoIoBeckhoffTwinController.Translator.Translate(_humanReadable).Interpolate(this);
			}

			protected set
			{
				_humanReadable = value;
			}
		}

		protected string _humanReadable;
		Vortex.Connector.ValueTypes.OnlinerBool __WcState;
		[IoLinkable("Inputs")]
		public Vortex.Connector.ValueTypes.OnlinerBool _WcState
		{
			get
			{
				return __WcState;
			}
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineBool IWcState._WcState
		{
			get
			{
				return _WcState;
			}
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowWcState._WcState
		{
			get
			{
				return _WcState;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool __InputToggle;
		[IoLinkable("Inputs")]
		public Vortex.Connector.ValueTypes.OnlinerBool _InputToggle
		{
			get
			{
				return __InputToggle;
			}
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineBool IWcState._InputToggle
		{
			get
			{
				return _InputToggle;
			}
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowWcState._InputToggle
		{
			get
			{
				return _InputToggle;
			}
		}

		public void LazyOnlineToShadow()
		{
			_WcState.Shadow = _WcState.LastValue;
			_InputToggle.Shadow = _InputToggle.LastValue;
		}

		public void LazyShadowToOnline()
		{
			_WcState.Cyclic = _WcState.Shadow;
			_InputToggle.Cyclic = _InputToggle.Shadow;
		}

		public PlainWcState CreatePlainerType()
		{
			var cloned = new PlainWcState();
			return cloned;
		}

		protected PlainWcState CreatePlainerType(PlainWcState cloned)
		{
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
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

		private System.Collections.Generic.List<Vortex.Connector.IVortexElement> Kids
		{
			get;
			set;
		}

		public System.Collections.Generic.IEnumerable<Vortex.Connector.IVortexElement> GetKids()
		{
			return this.Kids;
		}

		public void AddKid(Vortex.Connector.IVortexElement vortexElement)
		{
			this.Kids.Add(vortexElement);
		}

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

		public void FlushPlainToOnline(TcoIoBeckhoff.PlainWcState source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.PlainWcState source)
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

		public void FlushOnlineToPlain(TcoIoBeckhoff.PlainWcState source)
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
				return TcoIoBeckhoffTwinController.Translator.Translate(_AttributeName).Interpolate(this);
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

		public WcState(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@ValueTags = new System.Collections.Generic.List<Vortex.Connector.IValueTag>();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			this.Kids = new System.Collections.Generic.List<Vortex.Connector.IVortexElement>();
			this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			__WcState = @Connector.Online.Adapter.CreateBOOL(this, "<#EC Working Counter State (1=Data Invalid)#>", "_WcState");
			_WcState.AttributeName = "<#EC Working Counter State (1=Data Invalid)#>";
			__InputToggle = @Connector.Online.Adapter.CreateBOOL(this, "<#EC Togle bit#>", "_InputToggle");
			_InputToggle.AttributeName = "<#EC Togle bit#>";
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public WcState()
		{
			PexPreConstructorParameterless();
			__WcState = Vortex.Connector.IConnectorFactory.CreateBOOL();
			_WcState.AttributeName = "<#EC Working Counter State (1=Data Invalid)#>";
			__InputToggle = Vortex.Connector.IConnectorFactory.CreateBOOL();
			_InputToggle.AttributeName = "<#EC Togle bit#>";
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcWcState
		{
			public object _WcState;
			public object _InputToggle;
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcWcState()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IWcState : Vortex.Connector.IVortexOnlineObject
	{
		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineBool _WcState
		{
			get;
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineBool _InputToggle
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainWcState CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoIoBeckhoff.PlainWcState source);
		void FlushOnlineToPlain(TcoIoBeckhoff.PlainWcState source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowWcState : Vortex.Connector.IVortexShadowObject
	{
		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool _WcState
		{
			get;
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool _InputToggle
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainWcState CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoIoBeckhoff.PlainWcState source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainWcState : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		System.Boolean __WcState;
		[IoLinkable("Inputs")]
		public System.Boolean _WcState
		{
			get
			{
				return __WcState;
			}

			set
			{
				if (__WcState != value)
				{
					__WcState = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_WcState)));
				}
			}
		}

		System.Boolean __InputToggle;
		[IoLinkable("Inputs")]
		public System.Boolean _InputToggle
		{
			get
			{
				return __InputToggle;
			}

			set
			{
				if (__InputToggle != value)
				{
					__InputToggle = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_InputToggle)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoIoBeckhoff.WcState target)
		{
			target._WcState.Cyclic = _WcState;
			target._InputToggle.Cyclic = _InputToggle;
		}

		public void CopyPlainToCyclic(TcoIoBeckhoff.IWcState target)
		{
			this.CopyPlainToCyclic((TcoIoBeckhoff.WcState)target);
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.WcState target)
		{
			target._WcState.Shadow = _WcState;
			target._InputToggle.Shadow = _InputToggle;
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.IShadowWcState target)
		{
			this.CopyPlainToShadow((TcoIoBeckhoff.WcState)target);
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.WcState source)
		{
			_WcState = source._WcState.LastValue;
			_InputToggle = source._InputToggle.LastValue;
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.IWcState source)
		{
			this.CopyCyclicToPlain((TcoIoBeckhoff.WcState)source);
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.WcState source)
		{
			_WcState = source._WcState.Shadow;
			_InputToggle = source._InputToggle.Shadow;
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.IShadowWcState source)
		{
			this.CopyShadowToPlain((TcoIoBeckhoff.WcState)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainWcState()
		{
		}
	}
}