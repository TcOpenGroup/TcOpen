using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "fbContext", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class fbContext : TcoCore.TcoContext, Vortex.Connector.IVortexObject, IfbContext, IShadowfbContext, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		public new void LazyOnlineToShadow()
		{
			base.LazyOnlineToShadow();
		}

		public new void LazyShadowToOnline()
		{
			base.LazyShadowToOnline();
		}

		public new PlainfbContext CreatePlainerType()
		{
			var cloned = new PlainfbContext();
			base.CreatePlainerType(cloned);
			return cloned;
		}

		protected PlainfbContext CreatePlainerType(PlainfbContext cloned)
		{
			base.CreatePlainerType(cloned);
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
		partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexConstructorParameterless();
		public void FlushPlainToOnline(TcoCoreTests.PlainfbContext source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainfbContext source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainfbContext source)
		{
			this.Read();
			source.CopyCyclicToPlain(this);
		}

		public fbContext(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail): base (parent, readableTail, symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
		}

		public fbContext(): base ()
		{
			PexPreConstructorParameterless();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcfbContext : TcoCore.TcoContext.PlcTcoContext
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcfbContext()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IfbContext : Vortex.Connector.IVortexOnlineObject, TcoCore.ITcoContext
	{
		new TcoCoreTests.PlainfbContext CreatePlainerType();
		new void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainfbContext source);
		void FlushOnlineToPlain(TcoCoreTests.PlainfbContext source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowfbContext : Vortex.Connector.IVortexShadowObject, TcoCore.IShadowTcoContext
	{
		new TcoCoreTests.PlainfbContext CreatePlainerType();
		new void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainfbContext source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainfbContext : TcoCore.PlainTcoContext
	{
		public void CopyPlainToCyclic(TcoCoreTests.fbContext target)
		{
			base.CopyPlainToCyclic(target);
		}

		public void CopyPlainToCyclic(TcoCoreTests.IfbContext target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.fbContext)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.fbContext target)
		{
			base.CopyPlainToShadow(target);
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowfbContext target)
		{
			this.CopyPlainToShadow((TcoCoreTests.fbContext)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.fbContext source)
		{
			base.CopyCyclicToPlain(source);
		}

		public void CopyCyclicToPlain(TcoCoreTests.IfbContext source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.fbContext)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.fbContext source)
		{
			base.CopyShadowToPlain(source);
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowfbContext source)
		{
			this.CopyShadowToPlain((TcoCoreTests.fbContext)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainfbContext()
		{
		}
	}
}