using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoIoBeckhoff
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "FuseChannelBasic", "TcoIoBeckhoff", TypeComplexityEnum.Complex)]
	public partial class FuseChannelBasic : Vortex.Connector.IVortexObject, IFuseChannelBasic, IShadowFuseChannelBasic, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		EL922x_ChannelBasic __Chn;
		public EL922x_ChannelBasic _Chn
		{
			get
			{
				return __Chn;
			}
		}

		IEL922x_ChannelBasic IFuseChannelBasic._Chn
		{
			get
			{
				return _Chn;
			}
		}

		IShadowEL922x_ChannelBasic IShadowFuseChannelBasic._Chn
		{
			get
			{
				return _Chn;
			}
		}

		public void LazyOnlineToShadow()
		{
			_Chn.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			_Chn.LazyShadowToOnline();
		}

		public PlainFuseChannelBasic CreatePlainerType()
		{
			var cloned = new PlainFuseChannelBasic();
			cloned._Chn = _Chn.CreatePlainerType();
			return cloned;
		}

		protected PlainFuseChannelBasic CreatePlainerType(PlainFuseChannelBasic cloned)
		{
			cloned._Chn = _Chn.CreatePlainerType();
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

		public void FlushPlainToOnline(TcoIoBeckhoff.PlainFuseChannelBasic source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.PlainFuseChannelBasic source)
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

		public void FlushOnlineToPlain(TcoIoBeckhoff.PlainFuseChannelBasic source)
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

		public FuseChannelBasic(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			__Chn = new EL922x_ChannelBasic(this, "", "_Chn");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public FuseChannelBasic()
		{
			PexPreConstructorParameterless();
			__Chn = new EL922x_ChannelBasic();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcFuseChannelBasic
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcFuseChannelBasic()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IFuseChannelBasic : Vortex.Connector.IVortexOnlineObject
	{
		IEL922x_ChannelBasic _Chn
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainFuseChannelBasic CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoIoBeckhoff.PlainFuseChannelBasic source);
		void FlushOnlineToPlain(TcoIoBeckhoff.PlainFuseChannelBasic source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowFuseChannelBasic : Vortex.Connector.IVortexShadowObject
	{
		IShadowEL922x_ChannelBasic _Chn
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainFuseChannelBasic CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoIoBeckhoff.PlainFuseChannelBasic source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainFuseChannelBasic : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		PlainEL922x_ChannelBasic __Chn;
		public PlainEL922x_ChannelBasic _Chn
		{
			get
			{
				return __Chn;
			}

			set
			{
				if (__Chn != value)
				{
					__Chn = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_Chn)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoIoBeckhoff.FuseChannelBasic target)
		{
			_Chn.CopyPlainToCyclic(target._Chn);
		}

		public void CopyPlainToCyclic(TcoIoBeckhoff.IFuseChannelBasic target)
		{
			this.CopyPlainToCyclic((TcoIoBeckhoff.FuseChannelBasic)target);
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.FuseChannelBasic target)
		{
			_Chn.CopyPlainToShadow(target._Chn);
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.IShadowFuseChannelBasic target)
		{
			this.CopyPlainToShadow((TcoIoBeckhoff.FuseChannelBasic)target);
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.FuseChannelBasic source)
		{
			_Chn.CopyCyclicToPlain(source._Chn);
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.IFuseChannelBasic source)
		{
			this.CopyCyclicToPlain((TcoIoBeckhoff.FuseChannelBasic)source);
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.FuseChannelBasic source)
		{
			_Chn.CopyShadowToPlain(source._Chn);
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.IShadowFuseChannelBasic source)
		{
			this.CopyShadowToPlain((TcoIoBeckhoff.FuseChannelBasic)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainFuseChannelBasic()
		{
			__Chn = new PlainEL922x_ChannelBasic();
		}
	}
}