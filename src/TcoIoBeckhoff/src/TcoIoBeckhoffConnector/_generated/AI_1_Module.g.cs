using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoIoBeckhoff
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "AI_1_Module", "TcoIoBeckhoff", TypeComplexityEnum.Complex)]
	public partial class AI_1_Module : Vortex.Connector.IVortexObject, IAI_1_Module, IShadowAI_1_Module, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		AI_Channel _iCHANNEL_1;
		public AI_Channel iCHANNEL_1
		{
			get
			{
				return _iCHANNEL_1;
			}
		}

		IAI_Channel IAI_1_Module.iCHANNEL_1
		{
			get
			{
				return iCHANNEL_1;
			}
		}

		IShadowAI_Channel IShadowAI_1_Module.iCHANNEL_1
		{
			get
			{
				return iCHANNEL_1;
			}
		}

		WcState _WcState;
		public WcState WcState
		{
			get
			{
				return _WcState;
			}
		}

		IWcState IAI_1_Module.WcState
		{
			get
			{
				return WcState;
			}
		}

		IShadowWcState IShadowAI_1_Module.WcState
		{
			get
			{
				return WcState;
			}
		}

		WcState _InfoData;
		public WcState InfoData
		{
			get
			{
				return _InfoData;
			}
		}

		IWcState IAI_1_Module.InfoData
		{
			get
			{
				return InfoData;
			}
		}

		IShadowWcState IShadowAI_1_Module.InfoData
		{
			get
			{
				return InfoData;
			}
		}

		public void LazyOnlineToShadow()
		{
			iCHANNEL_1.LazyOnlineToShadow();
			WcState.LazyOnlineToShadow();
			InfoData.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			iCHANNEL_1.LazyShadowToOnline();
			WcState.LazyShadowToOnline();
			InfoData.LazyShadowToOnline();
		}

		public PlainAI_1_Module CreatePlainerType()
		{
			var cloned = new PlainAI_1_Module();
			cloned.iCHANNEL_1 = iCHANNEL_1.CreatePlainerType();
			cloned.WcState = WcState.CreatePlainerType();
			cloned.InfoData = InfoData.CreatePlainerType();
			return cloned;
		}

		protected PlainAI_1_Module CreatePlainerType(PlainAI_1_Module cloned)
		{
			cloned.iCHANNEL_1 = iCHANNEL_1.CreatePlainerType();
			cloned.WcState = WcState.CreatePlainerType();
			cloned.InfoData = InfoData.CreatePlainerType();
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

		public void FlushPlainToOnline(TcoIoBeckhoff.PlainAI_1_Module source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.PlainAI_1_Module source)
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

		public void FlushOnlineToPlain(TcoIoBeckhoff.PlainAI_1_Module source)
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

		public AI_1_Module(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@ValueTags = new System.Collections.Generic.List<Vortex.Connector.IValueTag>();
			this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			_iCHANNEL_1 = new AI_Channel(this, "1", "iCHANNEL_1");
			_iCHANNEL_1.AttributeName = "1";
			_WcState = new WcState(this, "WcState", "WcState");
			_WcState.AttributeName = "WcState";
			_InfoData = new WcState(this, "WcState", "InfoData");
			_InfoData.AttributeName = "WcState";
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
			parent.AddChild(this);
		}

		public AI_1_Module()
		{
			PexPreConstructorParameterless();
			_iCHANNEL_1 = new AI_Channel();
			_iCHANNEL_1.AttributeName = "1";
			_WcState = new WcState();
			_WcState.AttributeName = "WcState";
			_InfoData = new WcState();
			_InfoData.AttributeName = "WcState";
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcAI_1_Module
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcAI_1_Module()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IAI_1_Module : Vortex.Connector.IVortexOnlineObject
	{
		IAI_Channel iCHANNEL_1
		{
			get;
		}

		IWcState WcState
		{
			get;
		}

		IWcState InfoData
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainAI_1_Module CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoIoBeckhoff.PlainAI_1_Module source);
		void FlushOnlineToPlain(TcoIoBeckhoff.PlainAI_1_Module source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowAI_1_Module : Vortex.Connector.IVortexShadowObject
	{
		IShadowAI_Channel iCHANNEL_1
		{
			get;
		}

		IShadowWcState WcState
		{
			get;
		}

		IShadowWcState InfoData
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainAI_1_Module CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoIoBeckhoff.PlainAI_1_Module source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainAI_1_Module : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		PlainAI_Channel _iCHANNEL_1;
		public PlainAI_Channel iCHANNEL_1
		{
			get
			{
				return _iCHANNEL_1;
			}

			set
			{
				if (_iCHANNEL_1 != value)
				{
					_iCHANNEL_1 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(iCHANNEL_1)));
				}
			}
		}

		PlainWcState _WcState;
		public PlainWcState WcState
		{
			get
			{
				return _WcState;
			}

			set
			{
				if (_WcState != value)
				{
					_WcState = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(WcState)));
				}
			}
		}

		PlainWcState _InfoData;
		public PlainWcState InfoData
		{
			get
			{
				return _InfoData;
			}

			set
			{
				if (_InfoData != value)
				{
					_InfoData = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(InfoData)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoIoBeckhoff.AI_1_Module target)
		{
			iCHANNEL_1.CopyPlainToCyclic(target.iCHANNEL_1);
			WcState.CopyPlainToCyclic(target.WcState);
			InfoData.CopyPlainToCyclic(target.InfoData);
		}

		public void CopyPlainToCyclic(TcoIoBeckhoff.IAI_1_Module target)
		{
			this.CopyPlainToCyclic((TcoIoBeckhoff.AI_1_Module)target);
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.AI_1_Module target)
		{
			iCHANNEL_1.CopyPlainToShadow(target.iCHANNEL_1);
			WcState.CopyPlainToShadow(target.WcState);
			InfoData.CopyPlainToShadow(target.InfoData);
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.IShadowAI_1_Module target)
		{
			this.CopyPlainToShadow((TcoIoBeckhoff.AI_1_Module)target);
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.AI_1_Module source)
		{
			iCHANNEL_1.CopyCyclicToPlain(source.iCHANNEL_1);
			WcState.CopyCyclicToPlain(source.WcState);
			InfoData.CopyCyclicToPlain(source.InfoData);
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.IAI_1_Module source)
		{
			this.CopyCyclicToPlain((TcoIoBeckhoff.AI_1_Module)source);
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.AI_1_Module source)
		{
			iCHANNEL_1.CopyShadowToPlain(source.iCHANNEL_1);
			WcState.CopyShadowToPlain(source.WcState);
			InfoData.CopyShadowToPlain(source.InfoData);
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.IShadowAI_1_Module source)
		{
			this.CopyShadowToPlain((TcoIoBeckhoff.AI_1_Module)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainAI_1_Module()
		{
			_iCHANNEL_1 = new PlainAI_Channel();
			_WcState = new PlainWcState();
			_InfoData = new PlainWcState();
		}
	}
}