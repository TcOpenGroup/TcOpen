using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
	
///		<summary>
///			Provides basic state controller. 
///		</summary>		
///<seealso cref="PlcTcoState"/>
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoState", "TcoCore", TypeComplexityEnum.Complex)]
	public partial class TcoState : TcoObject, Vortex.Connector.IVortexObject, ITcoState, IShadowTcoState, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		public new void LazyOnlineToShadow()
		{
			base.LazyOnlineToShadow();
		}

		public new void LazyShadowToOnline()
		{
			base.LazyShadowToOnline();
		}

		public new PlainTcoState CreatePlainerType()
		{
			var cloned = new PlainTcoState();
			base.CreatePlainerType(cloned);
			return cloned;
		}

		protected PlainTcoState CreatePlainerType(PlainTcoState cloned)
		{
			base.CreatePlainerType(cloned);
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
		partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexConstructorParameterless();
		public void FlushPlainToOnline(TcoCore.PlainTcoState source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCore.PlainTcoState source)
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

		public void FlushOnlineToPlain(TcoCore.PlainTcoState source)
		{
			this.Read();
			source.CopyCyclicToPlain(this);
		}

		public TcoState(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail): base (parent, readableTail, symbolTail)
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

		public TcoState(): base ()
		{
			PexPreConstructorParameterless();
			AttributeName = "";
			PexConstructorParameterless();
		}

		
///		<summary>
///			Provides basic state controller. 
///		</summary>		

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoState : TcoCore.TcoObject.PlcTcoObject
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoState()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoState : Vortex.Connector.IVortexOnlineObject, TcoCore.ITcoObject
	{
		new TcoCore.PlainTcoState CreatePlainerType();
		new void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCore.PlainTcoState source);
		void FlushOnlineToPlain(TcoCore.PlainTcoState source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoState : Vortex.Connector.IVortexShadowObject, TcoCore.IShadowTcoObject
	{
		new TcoCore.PlainTcoState CreatePlainerType();
		new void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCore.PlainTcoState source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoState : TcoCore.PlainTcoObject
	{
		public void CopyPlainToCyclic(TcoCore.TcoState target)
		{
			base.CopyPlainToCyclic(target);
		}

		public void CopyPlainToCyclic(TcoCore.ITcoState target)
		{
			this.CopyPlainToCyclic((TcoCore.TcoState)target);
		}

		public void CopyPlainToShadow(TcoCore.TcoState target)
		{
			base.CopyPlainToShadow(target);
		}

		public void CopyPlainToShadow(TcoCore.IShadowTcoState target)
		{
			this.CopyPlainToShadow((TcoCore.TcoState)target);
		}

		public void CopyCyclicToPlain(TcoCore.TcoState source)
		{
			base.CopyCyclicToPlain(source);
		}

		public void CopyCyclicToPlain(TcoCore.ITcoState source)
		{
			this.CopyCyclicToPlain((TcoCore.TcoState)source);
		}

		public void CopyShadowToPlain(TcoCore.TcoState source)
		{
			base.CopyShadowToPlain(source);
		}

		public void CopyShadowToPlain(TcoCore.IShadowTcoState source)
		{
			this.CopyShadowToPlain((TcoCore.TcoState)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoState()
		{
		}
	}
}