using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoComponent", "TcoCore", TypeComplexityEnum.Complex)]
	public partial class TcoComponent : TcoObject, Vortex.Connector.IVortexObject, ITcoComponent, IShadowTcoComponent, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		public new void LazyOnlineToShadow()
		{
			base.LazyOnlineToShadow();
		}

		public new void LazyShadowToOnline()
		{
			base.LazyShadowToOnline();
		}

		public new PlainTcoComponent CreatePlainerType()
		{
			var cloned = new PlainTcoComponent();
			base.CreatePlainerType(cloned);
			return cloned;
		}

		protected PlainTcoComponent CreatePlainerType(PlainTcoComponent cloned)
		{
			base.CreatePlainerType(cloned);
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
		partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexConstructorParameterless();
		public void FlushPlainToOnline(TcoCore.PlainTcoComponent source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCore.PlainTcoComponent source)
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

		public void FlushOnlineToPlain(TcoCore.PlainTcoComponent source)
		{
			this.Read();
			source.CopyCyclicToPlain(this);
		}

		public TcoComponent(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail): base (parent, readableTail, symbolTail)
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

		public TcoComponent(): base ()
		{
			PexPreConstructorParameterless();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoComponent : TcoCore.TcoObject.PlcTcoObject
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoComponent()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoComponent : Vortex.Connector.IVortexOnlineObject, TcoCore.ITcoObject
	{
		new TcoCore.PlainTcoComponent CreatePlainerType();
		new void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCore.PlainTcoComponent source);
		void FlushOnlineToPlain(TcoCore.PlainTcoComponent source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoComponent : Vortex.Connector.IVortexShadowObject, TcoCore.IShadowTcoObject
	{
		new TcoCore.PlainTcoComponent CreatePlainerType();
		new void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCore.PlainTcoComponent source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoComponent : TcoCore.PlainTcoObject
	{
		public void CopyPlainToCyclic(TcoCore.TcoComponent target)
		{
			base.CopyPlainToCyclic(target);
		}

		public void CopyPlainToCyclic(TcoCore.ITcoComponent target)
		{
			this.CopyPlainToCyclic((TcoCore.TcoComponent)target);
		}

		public void CopyPlainToShadow(TcoCore.TcoComponent target)
		{
			base.CopyPlainToShadow(target);
		}

		public void CopyPlainToShadow(TcoCore.IShadowTcoComponent target)
		{
			this.CopyPlainToShadow((TcoCore.TcoComponent)target);
		}

		public void CopyCyclicToPlain(TcoCore.TcoComponent source)
		{
			base.CopyCyclicToPlain(source);
		}

		public void CopyCyclicToPlain(TcoCore.ITcoComponent source)
		{
			this.CopyCyclicToPlain((TcoCore.TcoComponent)source);
		}

		public void CopyShadowToPlain(TcoCore.TcoComponent source)
		{
			base.CopyShadowToPlain(source);
		}

		public void CopyShadowToPlain(TcoCore.IShadowTcoComponent source)
		{
			this.CopyShadowToPlain((TcoCore.TcoComponent)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoComponent()
		{
		}
	}
}