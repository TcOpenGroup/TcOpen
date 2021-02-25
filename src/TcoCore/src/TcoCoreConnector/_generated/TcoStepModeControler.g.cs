using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
	
///			<summary>
///				Provides switching between sequencer modes so as handling stepping tasks in th step mode.
///			</summary>
///<seealso cref="PlcTcoStepModeControler"/>
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoStepModeControler", "TcoCore", TypeComplexityEnum.Complex)]
	public partial class TcoStepModeControler : TcoState, Vortex.Connector.IVortexObject, ITcoStepModeControler, IShadowTcoStepModeControler, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		Vortex.Connector.ValueTypes.OnlinerBool _inCurrentStepRunning;
		public Vortex.Connector.ValueTypes.OnlinerBool inCurrentStepRunning
		{
			get
			{
				return _inCurrentStepRunning;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoStepModeControler.inCurrentStepRunning
		{
			get
			{
				return inCurrentStepRunning;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoStepModeControler.inCurrentStepRunning
		{
			get
			{
				return inCurrentStepRunning;
			}
		}

		public new void LazyOnlineToShadow()
		{
			base.LazyOnlineToShadow();
			inCurrentStepRunning.Shadow = inCurrentStepRunning.LastValue;
		}

		public new void LazyShadowToOnline()
		{
			base.LazyShadowToOnline();
			inCurrentStepRunning.Cyclic = inCurrentStepRunning.Shadow;
		}

		public new PlainTcoStepModeControler CreatePlainerType()
		{
			var cloned = new PlainTcoStepModeControler();
			base.CreatePlainerType(cloned);
			return cloned;
		}

		protected PlainTcoStepModeControler CreatePlainerType(PlainTcoStepModeControler cloned)
		{
			base.CreatePlainerType(cloned);
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
		partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexConstructorParameterless();
		public void FlushPlainToOnline(TcoCore.PlainTcoStepModeControler source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCore.PlainTcoStepModeControler source)
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

		public void FlushOnlineToPlain(TcoCore.PlainTcoStepModeControler source)
		{
			this.Read();
			source.CopyCyclicToPlain(this);
		}

		public TcoStepModeControler(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail): base (parent, readableTail, symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			_inCurrentStepRunning = @Connector.Online.Adapter.CreateBOOL(this, "", "inCurrentStepRunning");
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
		}

		public TcoStepModeControler(): base ()
		{
			PexPreConstructorParameterless();
			_inCurrentStepRunning = Vortex.Connector.IConnectorFactory.CreateBOOL();
			AttributeName = "";
			PexConstructorParameterless();
		}

		
///			<summary>
///				Provides switching between sequencer modes so as handling stepping tasks in th step mode.
///			</summary>

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoStepModeControler : TcoCore.TcoState.PlcTcoState
		{
			
///		<summary>
///			Provides access to the mode of the sequencer. 
///		</summary>		
///<summary><note type="note">This is PLC property. This method is accessible only from the PLC code.</note></summary>
///<returns>Plc type eSequencerMode; Twin type: <see cref="eSequencerMode"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public dynamic Mode
			{
				get
				{
					throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
				}
			}

			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoStepModeControler()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoStepModeControler : Vortex.Connector.IVortexOnlineObject, TcoCore.ITcoState
	{
		Vortex.Connector.ValueTypes.Online.IOnlineBool inCurrentStepRunning
		{
			get;
		}

		new TcoCore.PlainTcoStepModeControler CreatePlainerType();
		new void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCore.PlainTcoStepModeControler source);
		void FlushOnlineToPlain(TcoCore.PlainTcoStepModeControler source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoStepModeControler : Vortex.Connector.IVortexShadowObject, TcoCore.IShadowTcoState
	{
		Vortex.Connector.ValueTypes.Shadows.IShadowBool inCurrentStepRunning
		{
			get;
		}

		new TcoCore.PlainTcoStepModeControler CreatePlainerType();
		new void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCore.PlainTcoStepModeControler source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoStepModeControler : TcoCore.PlainTcoState
	{
		System.Boolean _inCurrentStepRunning;
		public System.Boolean inCurrentStepRunning
		{
			get
			{
				return _inCurrentStepRunning;
			}

			set
			{
				if (_inCurrentStepRunning != value)
				{
					_inCurrentStepRunning = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(inCurrentStepRunning)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCore.TcoStepModeControler target)
		{
			base.CopyPlainToCyclic(target);
			target.inCurrentStepRunning.Cyclic = inCurrentStepRunning;
		}

		public void CopyPlainToCyclic(TcoCore.ITcoStepModeControler target)
		{
			this.CopyPlainToCyclic((TcoCore.TcoStepModeControler)target);
		}

		public void CopyPlainToShadow(TcoCore.TcoStepModeControler target)
		{
			base.CopyPlainToShadow(target);
			target.inCurrentStepRunning.Shadow = inCurrentStepRunning;
		}

		public void CopyPlainToShadow(TcoCore.IShadowTcoStepModeControler target)
		{
			this.CopyPlainToShadow((TcoCore.TcoStepModeControler)target);
		}

		public void CopyCyclicToPlain(TcoCore.TcoStepModeControler source)
		{
			base.CopyCyclicToPlain(source);
			inCurrentStepRunning = source.inCurrentStepRunning.LastValue;
		}

		public void CopyCyclicToPlain(TcoCore.ITcoStepModeControler source)
		{
			this.CopyCyclicToPlain((TcoCore.TcoStepModeControler)source);
		}

		public void CopyShadowToPlain(TcoCore.TcoStepModeControler source)
		{
			base.CopyShadowToPlain(source);
			inCurrentStepRunning = source.inCurrentStepRunning.Shadow;
		}

		public void CopyShadowToPlain(TcoCore.IShadowTcoStepModeControler source)
		{
			this.CopyShadowToPlain((TcoCore.TcoStepModeControler)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoStepModeControler()
		{
		}
	}
}