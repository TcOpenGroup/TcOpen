using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoSequencer", "TcoCore", TypeComplexityEnum.Complex)]
	public partial class TcoSequencer : TcoState, Vortex.Connector.IVortexObject, ITcoSequencer, IShadowTcoSequencer, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		StepDetails __currentStep;
		public StepDetails _currentStep
		{
			get
			{
				return __currentStep;
			}
		}

		IStepDetails ITcoSequencer._currentStep
		{
			get
			{
				return _currentStep;
			}
		}

		IShadowStepDetails IShadowTcoSequencer._currentStep
		{
			get
			{
				return _currentStep;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool __sequencerHasError;
		[ReadOnly()]
		public Vortex.Connector.ValueTypes.OnlinerBool _sequencerHasError
		{
			get
			{
				return __sequencerHasError;
			}
		}

		[ReadOnly()]
		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoSequencer._sequencerHasError
		{
			get
			{
				return _sequencerHasError;
			}
		}

		[ReadOnly()]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoSequencer._sequencerHasError
		{
			get
			{
				return _sequencerHasError;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerInt __sequencerErrorId;
		[ReadOnly(), Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eSequencerError))]
		public Vortex.Connector.ValueTypes.OnlinerInt _sequencerErrorId
		{
			get
			{
				return __sequencerErrorId;
			}
		}

		[ReadOnly(), Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eSequencerError))]
		Vortex.Connector.ValueTypes.Online.IOnlineInt ITcoSequencer._sequencerErrorId
		{
			get
			{
				return _sequencerErrorId;
			}
		}

		[ReadOnly(), Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eSequencerError))]
		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadowTcoSequencer._sequencerErrorId
		{
			get
			{
				return _sequencerErrorId;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerTime __SequenceElapsedTime;
		public Vortex.Connector.ValueTypes.OnlinerTime _SequenceElapsedTime
		{
			get
			{
				return __SequenceElapsedTime;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineTime ITcoSequencer._SequenceElapsedTime
		{
			get
			{
				return _SequenceElapsedTime;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowTime IShadowTcoSequencer._SequenceElapsedTime
		{
			get
			{
				return _SequenceElapsedTime;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerTime __StepElapsedTime;
		public Vortex.Connector.ValueTypes.OnlinerTime _StepElapsedTime
		{
			get
			{
				return __StepElapsedTime;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineTime ITcoSequencer._StepElapsedTime
		{
			get
			{
				return _StepElapsedTime;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowTime IShadowTcoSequencer._StepElapsedTime
		{
			get
			{
				return _StepElapsedTime;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerTime __LastCycleTime;
		public Vortex.Connector.ValueTypes.OnlinerTime _LastCycleTime
		{
			get
			{
				return __LastCycleTime;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineTime ITcoSequencer._LastCycleTime
		{
			get
			{
				return _LastCycleTime;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowTime IShadowTcoSequencer._LastCycleTime
		{
			get
			{
				return _LastCycleTime;
			}
		}

		public new void LazyOnlineToShadow()
		{
			base.LazyOnlineToShadow();
			_currentStep.LazyOnlineToShadow();
			_sequencerHasError.Shadow = _sequencerHasError.LastValue;
			_sequencerErrorId.Shadow = _sequencerErrorId.LastValue;
			_SequenceElapsedTime.Shadow = _SequenceElapsedTime.LastValue;
			_StepElapsedTime.Shadow = _StepElapsedTime.LastValue;
			_LastCycleTime.Shadow = _LastCycleTime.LastValue;
		}

		public new void LazyShadowToOnline()
		{
			base.LazyShadowToOnline();
			_currentStep.LazyShadowToOnline();
			_sequencerHasError.Cyclic = _sequencerHasError.Shadow;
			_sequencerErrorId.Cyclic = _sequencerErrorId.Shadow;
			_SequenceElapsedTime.Cyclic = _SequenceElapsedTime.Shadow;
			_StepElapsedTime.Cyclic = _StepElapsedTime.Shadow;
			_LastCycleTime.Cyclic = _LastCycleTime.Shadow;
		}

		public new PlainTcoSequencer CreatePlainerType()
		{
			var cloned = new PlainTcoSequencer();
			base.CreatePlainerType(cloned);
			cloned._currentStep = _currentStep.CreatePlainerType();
			return cloned;
		}

		protected PlainTcoSequencer CreatePlainerType(PlainTcoSequencer cloned)
		{
			base.CreatePlainerType(cloned);
			cloned._currentStep = _currentStep.CreatePlainerType();
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
		partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexConstructorParameterless();
		public void FlushPlainToOnline(TcoCore.PlainTcoSequencer source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCore.PlainTcoSequencer source)
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

		public void FlushOnlineToPlain(TcoCore.PlainTcoSequencer source)
		{
			this.Read();
			source.CopyCyclicToPlain(this);
		}

		public TcoSequencer(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail): base (parent, readableTail, symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			__currentStep = new StepDetails(this, "<#Current step#>", "_currentStep");
			__currentStep.AttributeName = "<#Current step#>";
			__sequencerHasError = @Connector.Online.Adapter.CreateBOOL(this, "<#Sequencer error#>", "_sequencerHasError");
			_sequencerHasError.MakeReadOnly();
			_sequencerHasError.AttributeName = "<#Sequencer error#>";
			__sequencerErrorId = @Connector.Online.Adapter.CreateINT(this, "<#Sequencer error ID#>", "_sequencerErrorId");
			_sequencerErrorId.MakeReadOnly();
			_sequencerErrorId.AttributeName = "<#Sequencer error ID#>";
			__SequenceElapsedTime = @Connector.Online.Adapter.CreateTIME(this, "", "_SequenceElapsedTime");
			__StepElapsedTime = @Connector.Online.Adapter.CreateTIME(this, "", "_StepElapsedTime");
			__LastCycleTime = @Connector.Online.Adapter.CreateTIME(this, "", "_LastCycleTime");
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
		}

		public TcoSequencer(): base ()
		{
			PexPreConstructorParameterless();
			__currentStep = new StepDetails();
			__currentStep.AttributeName = "<#Current step#>";
			__sequencerHasError = Vortex.Connector.IConnectorFactory.CreateBOOL();
			_sequencerHasError.AttributeName = "<#Sequencer error#>";
			__sequencerErrorId = Vortex.Connector.IConnectorFactory.CreateINT();
			_sequencerErrorId.AttributeName = "<#Sequencer error ID#>";
			__SequenceElapsedTime = Vortex.Connector.IConnectorFactory.CreateTIME();
			__StepElapsedTime = Vortex.Connector.IConnectorFactory.CreateTIME();
			__LastCycleTime = Vortex.Connector.IConnectorFactory.CreateTIME();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoSequencer : TcoCore.TcoState.PlcTcoState
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoSequencer()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoSequencer : Vortex.Connector.IVortexOnlineObject, TcoCore.ITcoState
	{
		IStepDetails _currentStep
		{
			get;
		}

		[ReadOnly()]
		Vortex.Connector.ValueTypes.Online.IOnlineBool _sequencerHasError
		{
			get;
		}

		[ReadOnly(), Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eSequencerError))]
		Vortex.Connector.ValueTypes.Online.IOnlineInt _sequencerErrorId
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineTime _SequenceElapsedTime
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineTime _StepElapsedTime
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineTime _LastCycleTime
		{
			get;
		}

		new TcoCore.PlainTcoSequencer CreatePlainerType();
		new void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCore.PlainTcoSequencer source);
		void FlushOnlineToPlain(TcoCore.PlainTcoSequencer source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoSequencer : Vortex.Connector.IVortexShadowObject, TcoCore.IShadowTcoState
	{
		IShadowStepDetails _currentStep
		{
			get;
		}

		[ReadOnly()]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool _sequencerHasError
		{
			get;
		}

		[ReadOnly(), Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eSequencerError))]
		Vortex.Connector.ValueTypes.Shadows.IShadowInt _sequencerErrorId
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowTime _SequenceElapsedTime
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowTime _StepElapsedTime
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowTime _LastCycleTime
		{
			get;
		}

		new TcoCore.PlainTcoSequencer CreatePlainerType();
		new void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCore.PlainTcoSequencer source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoSequencer : TcoCore.PlainTcoState
	{
		PlainStepDetails __currentStep;
		public PlainStepDetails _currentStep
		{
			get
			{
				return __currentStep;
			}

			set
			{
				if (__currentStep != value)
				{
					__currentStep = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_currentStep)));
				}
			}
		}

		System.Boolean __sequencerHasError;
		[ReadOnly()]
		public System.Boolean _sequencerHasError
		{
			get
			{
				return __sequencerHasError;
			}

			set
			{
				if (__sequencerHasError != value)
				{
					__sequencerHasError = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_sequencerHasError)));
				}
			}
		}

		System.Int16 __sequencerErrorId;
		[ReadOnly(), Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eSequencerError))]
		public System.Int16 _sequencerErrorId
		{
			get
			{
				return __sequencerErrorId;
			}

			set
			{
				if (__sequencerErrorId != value)
				{
					__sequencerErrorId = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_sequencerErrorId)));
				}
			}
		}

		System.TimeSpan __SequenceElapsedTime;
		public System.TimeSpan _SequenceElapsedTime
		{
			get
			{
				return __SequenceElapsedTime;
			}

			set
			{
				if (__SequenceElapsedTime != value)
				{
					__SequenceElapsedTime = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_SequenceElapsedTime)));
				}
			}
		}

		System.TimeSpan __StepElapsedTime;
		public System.TimeSpan _StepElapsedTime
		{
			get
			{
				return __StepElapsedTime;
			}

			set
			{
				if (__StepElapsedTime != value)
				{
					__StepElapsedTime = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_StepElapsedTime)));
				}
			}
		}

		System.TimeSpan __LastCycleTime;
		public System.TimeSpan _LastCycleTime
		{
			get
			{
				return __LastCycleTime;
			}

			set
			{
				if (__LastCycleTime != value)
				{
					__LastCycleTime = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_LastCycleTime)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCore.TcoSequencer target)
		{
			base.CopyPlainToCyclic(target);
			_currentStep.CopyPlainToCyclic(target._currentStep);
			target._sequencerHasError.Cyclic = _sequencerHasError;
			target._sequencerErrorId.Cyclic = _sequencerErrorId;
			target._SequenceElapsedTime.Cyclic = _SequenceElapsedTime;
			target._StepElapsedTime.Cyclic = _StepElapsedTime;
			target._LastCycleTime.Cyclic = _LastCycleTime;
		}

		public void CopyPlainToCyclic(TcoCore.ITcoSequencer target)
		{
			this.CopyPlainToCyclic((TcoCore.TcoSequencer)target);
		}

		public void CopyPlainToShadow(TcoCore.TcoSequencer target)
		{
			base.CopyPlainToShadow(target);
			_currentStep.CopyPlainToShadow(target._currentStep);
			target._sequencerHasError.Shadow = _sequencerHasError;
			target._sequencerErrorId.Shadow = _sequencerErrorId;
			target._SequenceElapsedTime.Shadow = _SequenceElapsedTime;
			target._StepElapsedTime.Shadow = _StepElapsedTime;
			target._LastCycleTime.Shadow = _LastCycleTime;
		}

		public void CopyPlainToShadow(TcoCore.IShadowTcoSequencer target)
		{
			this.CopyPlainToShadow((TcoCore.TcoSequencer)target);
		}

		public void CopyCyclicToPlain(TcoCore.TcoSequencer source)
		{
			base.CopyCyclicToPlain(source);
			_currentStep.CopyCyclicToPlain(source._currentStep);
			_sequencerHasError = source._sequencerHasError.LastValue;
			_sequencerErrorId = source._sequencerErrorId.LastValue;
			_SequenceElapsedTime = source._SequenceElapsedTime.LastValue;
			_StepElapsedTime = source._StepElapsedTime.LastValue;
			_LastCycleTime = source._LastCycleTime.LastValue;
		}

		public void CopyCyclicToPlain(TcoCore.ITcoSequencer source)
		{
			this.CopyCyclicToPlain((TcoCore.TcoSequencer)source);
		}

		public void CopyShadowToPlain(TcoCore.TcoSequencer source)
		{
			base.CopyShadowToPlain(source);
			_currentStep.CopyShadowToPlain(source._currentStep);
			_sequencerHasError = source._sequencerHasError.Shadow;
			_sequencerErrorId = source._sequencerErrorId.Shadow;
			_SequenceElapsedTime = source._SequenceElapsedTime.Shadow;
			_StepElapsedTime = source._StepElapsedTime.Shadow;
			_LastCycleTime = source._LastCycleTime.Shadow;
		}

		public void CopyShadowToPlain(TcoCore.IShadowTcoSequencer source)
		{
			this.CopyShadowToPlain((TcoCore.TcoSequencer)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoSequencer()
		{
			__currentStep = new PlainStepDetails();
		}
	}
}