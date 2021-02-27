using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoIoBeckhoff
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "InfoData", "TcoIoBeckhoff", TypeComplexityEnum.Complex)]
	public partial class InfoData : Vortex.Connector.IVortexObject, IInfoData, IShadowInfoData, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		Vortex.Connector.ValueTypes.OnlinerBool _EC_State;
		[IoLinkable("Inputs")]
		public Vortex.Connector.ValueTypes.OnlinerBool EC_State
		{
			get
			{
				return _EC_State;
			}
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineBool IInfoData.EC_State
		{
			get
			{
				return EC_State;
			}
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowInfoData.EC_State
		{
			get
			{
				return EC_State;
			}
		}

		public void LazyOnlineToShadow()
		{
			EC_State.Shadow = EC_State.LastValue;
		}

		public void LazyShadowToOnline()
		{
			EC_State.Cyclic = EC_State.Shadow;
		}

		public PlainInfoData CreatePlainerType()
		{
			var cloned = new PlainInfoData();
			return cloned;
		}

		protected PlainInfoData CreatePlainerType(PlainInfoData cloned)
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

		public void FlushPlainToOnline(TcoIoBeckhoff.PlainInfoData source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.PlainInfoData source)
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

		public void FlushOnlineToPlain(TcoIoBeckhoff.PlainInfoData source)
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

		public InfoData(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			_EC_State = @Connector.Online.Adapter.CreateBOOL(this, "<#EC State (8=OK)#>", "EC_State");
			EC_State.AttributeName = "<#EC State (8=OK)#>";
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public InfoData()
		{
			PexPreConstructorParameterless();
			_EC_State = Vortex.Connector.IConnectorFactory.CreateBOOL();
			EC_State.AttributeName = "<#EC State (8=OK)#>";
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcInfoData
		{
			public object EC_State;
			public object EC_AmsAddr;
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcInfoData()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IInfoData : Vortex.Connector.IVortexOnlineObject
	{
		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineBool EC_State
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainInfoData CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoIoBeckhoff.PlainInfoData source);
		void FlushOnlineToPlain(TcoIoBeckhoff.PlainInfoData source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowInfoData : Vortex.Connector.IVortexShadowObject
	{
		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool EC_State
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainInfoData CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoIoBeckhoff.PlainInfoData source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainInfoData : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		System.Boolean _EC_State;
		[IoLinkable("Inputs")]
		public System.Boolean EC_State
		{
			get
			{
				return _EC_State;
			}

			set
			{
				if (_EC_State != value)
				{
					_EC_State = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(EC_State)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoIoBeckhoff.InfoData target)
		{
			target.EC_State.Cyclic = EC_State;
		}

		public void CopyPlainToCyclic(TcoIoBeckhoff.IInfoData target)
		{
			this.CopyPlainToCyclic((TcoIoBeckhoff.InfoData)target);
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.InfoData target)
		{
			target.EC_State.Shadow = EC_State;
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.IShadowInfoData target)
		{
			this.CopyPlainToShadow((TcoIoBeckhoff.InfoData)target);
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.InfoData source)
		{
			EC_State = source.EC_State.LastValue;
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.IInfoData source)
		{
			this.CopyCyclicToPlain((TcoIoBeckhoff.InfoData)source);
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.InfoData source)
		{
			EC_State = source.EC_State.Shadow;
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.IShadowInfoData source)
		{
			this.CopyShadowToPlain((TcoIoBeckhoff.InfoData)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainInfoData()
		{
		}
	}
}