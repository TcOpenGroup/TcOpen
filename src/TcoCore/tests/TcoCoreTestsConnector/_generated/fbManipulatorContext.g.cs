using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "fbManipulatorContext", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class fbManipulatorContext : TcoCore.TcoContext, Vortex.Connector.IVortexObject, IfbManipulatorContext, IShadowfbManipulatorContext, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		fbManipulatorAutomat __automat;
		public fbManipulatorAutomat _automat
		{
			get
			{
				return __automat;
			}
		}

		IfbManipulatorAutomat IfbManipulatorContext._automat
		{
			get
			{
				return _automat;
			}
		}

		IShadowfbManipulatorAutomat IShadowfbManipulatorContext._automat
		{
			get
			{
				return _automat;
			}
		}

		public new void LazyOnlineToShadow()
		{
			base.LazyOnlineToShadow();
			_automat.LazyOnlineToShadow();
		}

		public new void LazyShadowToOnline()
		{
			base.LazyShadowToOnline();
			_automat.LazyShadowToOnline();
		}

		public new PlainfbManipulatorContext CreatePlainerType()
		{
			var cloned = new PlainfbManipulatorContext();
			base.CreatePlainerType(cloned);
			cloned._automat = _automat.CreatePlainerType();
			return cloned;
		}

		protected PlainfbManipulatorContext CreatePlainerType(PlainfbManipulatorContext cloned)
		{
			base.CreatePlainerType(cloned);
			cloned._automat = _automat.CreatePlainerType();
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
		partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexConstructorParameterless();
		public void FlushPlainToOnline(TcoCoreTests.PlainfbManipulatorContext source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainfbManipulatorContext source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainfbManipulatorContext source)
		{
			this.Read();
			source.CopyCyclicToPlain(this);
		}

		public fbManipulatorContext(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail): base (parent, readableTail, symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			__automat = new fbManipulatorAutomat(this, "", "_automat");
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
		}

		public fbManipulatorContext(): base ()
		{
			PexPreConstructorParameterless();
			__automat = new fbManipulatorAutomat();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcfbManipulatorContext : TcoCore.TcoContext.PlcTcoContext
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcfbManipulatorContext()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IfbManipulatorContext : Vortex.Connector.IVortexOnlineObject, TcoCore.ITcoContext
	{
		IfbManipulatorAutomat _automat
		{
			get;
		}

		new TcoCoreTests.PlainfbManipulatorContext CreatePlainerType();
		new void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainfbManipulatorContext source);
		void FlushOnlineToPlain(TcoCoreTests.PlainfbManipulatorContext source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowfbManipulatorContext : Vortex.Connector.IVortexShadowObject, TcoCore.IShadowTcoContext
	{
		IShadowfbManipulatorAutomat _automat
		{
			get;
		}

		new TcoCoreTests.PlainfbManipulatorContext CreatePlainerType();
		new void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainfbManipulatorContext source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainfbManipulatorContext : TcoCore.PlainTcoContext
	{
		PlainfbManipulatorAutomat __automat;
		public PlainfbManipulatorAutomat _automat
		{
			get
			{
				return __automat;
			}

			set
			{
				if (__automat != value)
				{
					__automat = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_automat)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCoreTests.fbManipulatorContext target)
		{
			base.CopyPlainToCyclic(target);
			_automat.CopyPlainToCyclic(target._automat);
		}

		public void CopyPlainToCyclic(TcoCoreTests.IfbManipulatorContext target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.fbManipulatorContext)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.fbManipulatorContext target)
		{
			base.CopyPlainToShadow(target);
			_automat.CopyPlainToShadow(target._automat);
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowfbManipulatorContext target)
		{
			this.CopyPlainToShadow((TcoCoreTests.fbManipulatorContext)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.fbManipulatorContext source)
		{
			base.CopyCyclicToPlain(source);
			_automat.CopyCyclicToPlain(source._automat);
		}

		public void CopyCyclicToPlain(TcoCoreTests.IfbManipulatorContext source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.fbManipulatorContext)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.fbManipulatorContext source)
		{
			base.CopyShadowToPlain(source);
			_automat.CopyShadowToPlain(source._automat);
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowfbManipulatorContext source)
		{
			this.CopyShadowToPlain((TcoCoreTests.fbManipulatorContext)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainfbManipulatorContext()
		{
			__automat = new PlainfbManipulatorAutomat();
		}
	}
}