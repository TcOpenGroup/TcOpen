using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoIoBeckhoff
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "FuseChannelExtended", "TcoIoBeckhoff", TypeComplexityEnum.Complex)]
	public partial class FuseChannelExtended : FuseChannelBasic, Vortex.Connector.IVortexObject, IFuseChannelExtended, IShadowFuseChannelExtended, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		EL922x_ChannelExtended __ChnEx;
		public EL922x_ChannelExtended _ChnEx
		{
			get
			{
				return __ChnEx;
			}
		}

		IEL922x_ChannelExtended IFuseChannelExtended._ChnEx
		{
			get
			{
				return _ChnEx;
			}
		}

		IShadowEL922x_ChannelExtended IShadowFuseChannelExtended._ChnEx
		{
			get
			{
				return _ChnEx;
			}
		}

		public new void LazyOnlineToShadow()
		{
			base.LazyOnlineToShadow();
			_ChnEx.LazyOnlineToShadow();
		}

		public new void LazyShadowToOnline()
		{
			base.LazyShadowToOnline();
			_ChnEx.LazyShadowToOnline();
		}

		public new PlainFuseChannelExtended CreatePlainerType()
		{
			var cloned = new PlainFuseChannelExtended();
			base.CreatePlainerType(cloned);
			cloned._ChnEx = _ChnEx.CreatePlainerType();
			return cloned;
		}

		protected PlainFuseChannelExtended CreatePlainerType(PlainFuseChannelExtended cloned)
		{
			base.CreatePlainerType(cloned);
			cloned._ChnEx = _ChnEx.CreatePlainerType();
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
		partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexConstructorParameterless();
		public void FlushPlainToOnline(TcoIoBeckhoff.PlainFuseChannelExtended source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.PlainFuseChannelExtended source)
		{
			source.CopyPlainToShadow(this);
		}

		public new void FlushShadowToOnline()
		{
			this.LazyShadowToOnline();
			this.Write();
		}

		public new void FlushOnlineToShadow()
		{
			this.Read();
			this.LazyOnlineToShadow();
		}

		public void FlushOnlineToPlain(TcoIoBeckhoff.PlainFuseChannelExtended source)
		{
			this.Read();
			source.CopyCyclicToPlain(this);
		}

		public FuseChannelExtended(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail): base (parent, readableTail, symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			__ChnEx = new EL922x_ChannelExtended(this, "", "_ChnEx");
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
		}

		public FuseChannelExtended(): base ()
		{
			PexPreConstructorParameterless();
			__ChnEx = new EL922x_ChannelExtended();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcFuseChannelExtended : TcoIoBeckhoff.FuseChannelBasic.PlcFuseChannelBasic
		{
			public PlainEL922x_ChannelExtended _ChnEx;
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcFuseChannelExtended()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IFuseChannelExtended : Vortex.Connector.IVortexOnlineObject, TcoIoBeckhoff.IFuseChannelBasic
	{
		IEL922x_ChannelExtended _ChnEx
		{
			get;
		}

		new TcoIoBeckhoff.PlainFuseChannelExtended CreatePlainerType();
		new void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoIoBeckhoff.PlainFuseChannelExtended source);
		void FlushOnlineToPlain(TcoIoBeckhoff.PlainFuseChannelExtended source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowFuseChannelExtended : Vortex.Connector.IVortexShadowObject, TcoIoBeckhoff.IShadowFuseChannelBasic
	{
		IShadowEL922x_ChannelExtended _ChnEx
		{
			get;
		}

		new TcoIoBeckhoff.PlainFuseChannelExtended CreatePlainerType();
		new void FlushShadowToOnline();
		void CopyPlainToShadow(TcoIoBeckhoff.PlainFuseChannelExtended source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainFuseChannelExtended : TcoIoBeckhoff.PlainFuseChannelBasic
	{
		PlainEL922x_ChannelExtended __ChnEx;
		public PlainEL922x_ChannelExtended _ChnEx
		{
			get
			{
				return __ChnEx;
			}

			set
			{
				if (__ChnEx != value)
				{
					__ChnEx = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_ChnEx)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoIoBeckhoff.FuseChannelExtended target)
		{
			base.CopyPlainToCyclic(target);
			_ChnEx.CopyPlainToCyclic(target._ChnEx);
		}

		public void CopyPlainToCyclic(TcoIoBeckhoff.IFuseChannelExtended target)
		{
			this.CopyPlainToCyclic((TcoIoBeckhoff.FuseChannelExtended)target);
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.FuseChannelExtended target)
		{
			base.CopyPlainToShadow(target);
			_ChnEx.CopyPlainToShadow(target._ChnEx);
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.IShadowFuseChannelExtended target)
		{
			this.CopyPlainToShadow((TcoIoBeckhoff.FuseChannelExtended)target);
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.FuseChannelExtended source)
		{
			base.CopyCyclicToPlain(source);
			_ChnEx.CopyCyclicToPlain(source._ChnEx);
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.IFuseChannelExtended source)
		{
			this.CopyCyclicToPlain((TcoIoBeckhoff.FuseChannelExtended)source);
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.FuseChannelExtended source)
		{
			base.CopyShadowToPlain(source);
			_ChnEx.CopyShadowToPlain(source._ChnEx);
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.IShadowFuseChannelExtended source)
		{
			this.CopyShadowToPlain((TcoIoBeckhoff.FuseChannelExtended)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainFuseChannelExtended()
		{
			__ChnEx = new PlainEL922x_ChannelExtended();
		}
	}
}