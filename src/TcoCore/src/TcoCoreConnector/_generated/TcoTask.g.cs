using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoTask", "TcoCore", TypeComplexityEnum.Complex)]
	public partial class TcoTask : TcoObject, Vortex.Connector.IVortexObject, ITcoTask, IShadowTcoTask, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		public new void LazyOnlineToShadow()
		{
			base.LazyOnlineToShadow();
		}

		public new void LazyShadowToOnline()
		{
			base.LazyShadowToOnline();
		}

		public new PlainTcoTask CreatePlainerType()
		{
			var cloned = new PlainTcoTask();
			base.CreatePlainerType(cloned);
			return cloned;
		}

		protected PlainTcoTask CreatePlainerType(PlainTcoTask cloned)
		{
			base.CreatePlainerType(cloned);
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
		partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexConstructorParameterless();
		public void FlushPlainToOnline(TcoCore.PlainTcoTask source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCore.PlainTcoTask source)
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

		public void FlushOnlineToPlain(TcoCore.PlainTcoTask source)
		{
			this.Read();
			source.CopyCyclicToPlain(this);
		}

		public TcoTask(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail): base (parent, readableTail, symbolTail)
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

		public TcoTask(): base ()
		{
			PexPreConstructorParameterless();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoTask : TcoCore.TcoObject.PlcTcoObject
		{
			
///		<summary>
///			Returns the context of the parent object, that this object is assigned to.
///			This context is given by declaration, its value is assiged after download by calling the implicit method <c>FB_init()</c> and cannot be changed during runtime.
///		</summary>			
///<summary><note type="note">This is PLC property. This method is accessible only from the PLC code.</note></summary>
///<returns>Plc type ITcoContext; Twin type: <see cref="ITcoContext"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public dynamic Context
			{
				get
				{
					throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
				}
			}

			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoTask()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoTask : Vortex.Connector.IVortexOnlineObject, TcoCore.ITcoObject
	{
		new TcoCore.PlainTcoTask CreatePlainerType();
		new void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCore.PlainTcoTask source);
		void FlushOnlineToPlain(TcoCore.PlainTcoTask source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoTask : Vortex.Connector.IVortexShadowObject, TcoCore.IShadowTcoObject
	{
		new TcoCore.PlainTcoTask CreatePlainerType();
		new void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCore.PlainTcoTask source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoTask : TcoCore.PlainTcoObject
	{
		public void CopyPlainToCyclic(TcoCore.TcoTask target)
		{
			base.CopyPlainToCyclic(target);
		}

		public void CopyPlainToCyclic(TcoCore.ITcoTask target)
		{
			this.CopyPlainToCyclic((TcoCore.TcoTask)target);
		}

		public void CopyPlainToShadow(TcoCore.TcoTask target)
		{
			base.CopyPlainToShadow(target);
		}

		public void CopyPlainToShadow(TcoCore.IShadowTcoTask target)
		{
			this.CopyPlainToShadow((TcoCore.TcoTask)target);
		}

		public void CopyCyclicToPlain(TcoCore.TcoTask source)
		{
			base.CopyCyclicToPlain(source);
		}

		public void CopyCyclicToPlain(TcoCore.ITcoTask source)
		{
			this.CopyCyclicToPlain((TcoCore.TcoTask)source);
		}

		public void CopyShadowToPlain(TcoCore.TcoTask source)
		{
			base.CopyShadowToPlain(source);
		}

		public void CopyShadowToPlain(TcoCore.IShadowTcoTask source)
		{
			this.CopyShadowToPlain((TcoCore.TcoTask)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoTask()
		{
		}
	}
}