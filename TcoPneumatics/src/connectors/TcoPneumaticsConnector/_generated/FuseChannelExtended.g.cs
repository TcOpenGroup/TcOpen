using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoPneumatics
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "FuseChannelExtended", "TcoPneumatics", TypeComplexityEnum.Complex)]
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
		public void FlushPlainToOnline(TcoPneumatics.PlainFuseChannelExtended source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoPneumatics.PlainFuseChannelExtended source)
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

		public void FlushOnlineToPlain(TcoPneumatics.PlainFuseChannelExtended source)
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
		protected abstract class PlcFuseChannelExtended : TcoPneumatics.FuseChannelBasic.PlcFuseChannelBasic
		{
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
	public partial interface IFuseChannelExtended : Vortex.Connector.IVortexOnlineObject, TcoPneumatics.IFuseChannelBasic
	{
		IEL922x_ChannelExtended _ChnEx
		{
			get;
		}

		new TcoPneumatics.PlainFuseChannelExtended CreatePlainerType();
		new void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoPneumatics.PlainFuseChannelExtended source);
		void FlushOnlineToPlain(TcoPneumatics.PlainFuseChannelExtended source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowFuseChannelExtended : Vortex.Connector.IVortexShadowObject, TcoPneumatics.IShadowFuseChannelBasic
	{
		IShadowEL922x_ChannelExtended _ChnEx
		{
			get;
		}

		new TcoPneumatics.PlainFuseChannelExtended CreatePlainerType();
		new void FlushShadowToOnline();
		void CopyPlainToShadow(TcoPneumatics.PlainFuseChannelExtended source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainFuseChannelExtended : TcoPneumatics.PlainFuseChannelBasic
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

		public void CopyPlainToCyclic(TcoPneumatics.FuseChannelExtended target)
		{
			base.CopyPlainToCyclic(target);
			_ChnEx.CopyPlainToCyclic(target._ChnEx);
		}

		public void CopyPlainToCyclic(TcoPneumatics.IFuseChannelExtended target)
		{
			this.CopyPlainToCyclic((TcoPneumatics.FuseChannelExtended)target);
		}

		public void CopyPlainToShadow(TcoPneumatics.FuseChannelExtended target)
		{
			base.CopyPlainToShadow(target);
			_ChnEx.CopyPlainToShadow(target._ChnEx);
		}

		public void CopyPlainToShadow(TcoPneumatics.IShadowFuseChannelExtended target)
		{
			this.CopyPlainToShadow((TcoPneumatics.FuseChannelExtended)target);
		}

		public void CopyCyclicToPlain(TcoPneumatics.FuseChannelExtended source)
		{
			base.CopyCyclicToPlain(source);
			_ChnEx.CopyCyclicToPlain(source._ChnEx);
		}

		public void CopyCyclicToPlain(TcoPneumatics.IFuseChannelExtended source)
		{
			this.CopyCyclicToPlain((TcoPneumatics.FuseChannelExtended)source);
		}

		public void CopyShadowToPlain(TcoPneumatics.FuseChannelExtended source)
		{
			base.CopyShadowToPlain(source);
			_ChnEx.CopyShadowToPlain(source._ChnEx);
		}

		public void CopyShadowToPlain(TcoPneumatics.IShadowFuseChannelExtended source)
		{
			this.CopyShadowToPlain((TcoPneumatics.FuseChannelExtended)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainFuseChannelExtended()
		{
			__ChnEx = new PlainEL922x_ChannelExtended();
		}
	}
}