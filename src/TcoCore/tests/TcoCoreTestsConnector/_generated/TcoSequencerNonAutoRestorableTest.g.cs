using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoSequencerNonAutoRestorableTest", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class TcoSequencerNonAutoRestorableTest : Vortex.Connector.IVortexObject, ITcoSequencerNonAutoRestorableTest, IShadowTcoSequencerNonAutoRestorableTest, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
				return TcoCoreTestsTwinController.Translator.Translate(_humanReadable).Interpolate(this);
			}

			protected set
			{
				_humanReadable = value;
			}
		}

		protected string _humanReadable;
		_TcoSequencer __Sequencer;
		public _TcoSequencer _Sequencer
		{
			get
			{
				return __Sequencer;
			}
		}

		I_TcoSequencer ITcoSequencerNonAutoRestorableTest._Sequencer
		{
			get
			{
				return _Sequencer;
			}
		}

		IShadow_TcoSequencer IShadowTcoSequencerNonAutoRestorableTest._Sequencer
		{
			get
			{
				return _Sequencer;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool __RunPLCinstanceOnce;
		public Vortex.Connector.ValueTypes.OnlinerBool _RunPLCinstanceOnce
		{
			get
			{
				return __RunPLCinstanceOnce;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoSequencerNonAutoRestorableTest._RunPLCinstanceOnce
		{
			get
			{
				return _RunPLCinstanceOnce;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoSequencerNonAutoRestorableTest._RunPLCinstanceOnce
		{
			get
			{
				return _RunPLCinstanceOnce;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool __RunPLCinstanceCyclicaly;
		public Vortex.Connector.ValueTypes.OnlinerBool _RunPLCinstanceCyclicaly
		{
			get
			{
				return __RunPLCinstanceCyclicaly;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoSequencerNonAutoRestorableTest._RunPLCinstanceCyclicaly
		{
			get
			{
				return _RunPLCinstanceCyclicaly;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoSequencerNonAutoRestorableTest._RunPLCinstanceCyclicaly
		{
			get
			{
				return _RunPLCinstanceCyclicaly;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerInt __StepID;
		public Vortex.Connector.ValueTypes.OnlinerInt _StepID
		{
			get
			{
				return __StepID;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineInt ITcoSequencerNonAutoRestorableTest._StepID
		{
			get
			{
				return _StepID;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadowTcoSequencerNonAutoRestorableTest._StepID
		{
			get
			{
				return _StepID;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool __Enabled;
		public Vortex.Connector.ValueTypes.OnlinerBool _Enabled
		{
			get
			{
				return __Enabled;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoSequencerNonAutoRestorableTest._Enabled
		{
			get
			{
				return _Enabled;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoSequencerNonAutoRestorableTest._Enabled
		{
			get
			{
				return _Enabled;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerString __StepDescription;
		public Vortex.Connector.ValueTypes.OnlinerString _StepDescription
		{
			get
			{
				return __StepDescription;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineString ITcoSequencerNonAutoRestorableTest._StepDescription
		{
			get
			{
				return _StepDescription;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowString IShadowTcoSequencerNonAutoRestorableTest._StepDescription
		{
			get
			{
				return _StepDescription;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool __RunOneStep;
		public Vortex.Connector.ValueTypes.OnlinerBool _RunOneStep
		{
			get
			{
				return __RunOneStep;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoSequencerNonAutoRestorableTest._RunOneStep
		{
			get
			{
				return _RunOneStep;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoSequencerNonAutoRestorableTest._RunOneStep
		{
			get
			{
				return _RunOneStep;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool __RunAllSteps;
		public Vortex.Connector.ValueTypes.OnlinerBool _RunAllSteps
		{
			get
			{
				return __RunAllSteps;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoSequencerNonAutoRestorableTest._RunAllSteps
		{
			get
			{
				return _RunAllSteps;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoSequencerNonAutoRestorableTest._RunAllSteps
		{
			get
			{
				return _RunAllSteps;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool __FinishStep;
		public Vortex.Connector.ValueTypes.OnlinerBool _FinishStep
		{
			get
			{
				return __FinishStep;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoSequencerNonAutoRestorableTest._FinishStep
		{
			get
			{
				return _FinishStep;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoSequencerNonAutoRestorableTest._FinishStep
		{
			get
			{
				return _FinishStep;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool __Reset;
		public Vortex.Connector.ValueTypes.OnlinerBool _Reset
		{
			get
			{
				return __Reset;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoSequencerNonAutoRestorableTest._Reset
		{
			get
			{
				return _Reset;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoSequencerNonAutoRestorableTest._Reset
		{
			get
			{
				return _Reset;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerInt __CycleCount;
		public Vortex.Connector.ValueTypes.OnlinerInt _CycleCount
		{
			get
			{
				return __CycleCount;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineInt ITcoSequencerNonAutoRestorableTest._CycleCount
		{
			get
			{
				return _CycleCount;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadowTcoSequencerNonAutoRestorableTest._CycleCount
		{
			get
			{
				return _CycleCount;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerInt __ResetCycleCount;
		public Vortex.Connector.ValueTypes.OnlinerInt _ResetCycleCount
		{
			get
			{
				return __ResetCycleCount;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineInt ITcoSequencerNonAutoRestorableTest._ResetCycleCount
		{
			get
			{
				return _ResetCycleCount;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadowTcoSequencerNonAutoRestorableTest._ResetCycleCount
		{
			get
			{
				return _ResetCycleCount;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool __Init;
		public Vortex.Connector.ValueTypes.OnlinerBool _Init
		{
			get
			{
				return __Init;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoSequencerNonAutoRestorableTest._Init
		{
			get
			{
				return _Init;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoSequencerNonAutoRestorableTest._Init
		{
			get
			{
				return _Init;
			}
		}

		public void LazyOnlineToShadow()
		{
			_Sequencer.LazyOnlineToShadow();
			_RunPLCinstanceOnce.Shadow = _RunPLCinstanceOnce.LastValue;
			_RunPLCinstanceCyclicaly.Shadow = _RunPLCinstanceCyclicaly.LastValue;
			_StepID.Shadow = _StepID.LastValue;
			_Enabled.Shadow = _Enabled.LastValue;
			_StepDescription.Shadow = _StepDescription.LastValue;
			_RunOneStep.Shadow = _RunOneStep.LastValue;
			_RunAllSteps.Shadow = _RunAllSteps.LastValue;
			_FinishStep.Shadow = _FinishStep.LastValue;
			_Reset.Shadow = _Reset.LastValue;
			_CycleCount.Shadow = _CycleCount.LastValue;
			_ResetCycleCount.Shadow = _ResetCycleCount.LastValue;
			_Init.Shadow = _Init.LastValue;
		}

		public void LazyShadowToOnline()
		{
			_Sequencer.LazyShadowToOnline();
			_RunPLCinstanceOnce.Cyclic = _RunPLCinstanceOnce.Shadow;
			_RunPLCinstanceCyclicaly.Cyclic = _RunPLCinstanceCyclicaly.Shadow;
			_StepID.Cyclic = _StepID.Shadow;
			_Enabled.Cyclic = _Enabled.Shadow;
			_StepDescription.Cyclic = _StepDescription.Shadow;
			_RunOneStep.Cyclic = _RunOneStep.Shadow;
			_RunAllSteps.Cyclic = _RunAllSteps.Shadow;
			_FinishStep.Cyclic = _FinishStep.Shadow;
			_Reset.Cyclic = _Reset.Shadow;
			_CycleCount.Cyclic = _CycleCount.Shadow;
			_ResetCycleCount.Cyclic = _ResetCycleCount.Shadow;
			_Init.Cyclic = _Init.Shadow;
		}

		public PlainTcoSequencerNonAutoRestorableTest CreatePlainerType()
		{
			var cloned = new PlainTcoSequencerNonAutoRestorableTest();
			cloned._Sequencer = _Sequencer.CreatePlainerType();
			return cloned;
		}

		protected PlainTcoSequencerNonAutoRestorableTest CreatePlainerType(PlainTcoSequencerNonAutoRestorableTest cloned)
		{
			cloned._Sequencer = _Sequencer.CreatePlainerType();
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

		public void FlushPlainToOnline(TcoCoreTests.PlainTcoSequencerNonAutoRestorableTest source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainTcoSequencerNonAutoRestorableTest source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainTcoSequencerNonAutoRestorableTest source)
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
				return TcoCoreTestsTwinController.Translator.Translate(_AttributeName).Interpolate(this);
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

		public TcoSequencerNonAutoRestorableTest(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			__Sequencer = new _TcoSequencer(this, "", "_Sequencer");
			__RunPLCinstanceOnce = @Connector.Online.Adapter.CreateBOOL(this, "", "_RunPLCinstanceOnce");
			__RunPLCinstanceCyclicaly = @Connector.Online.Adapter.CreateBOOL(this, "", "_RunPLCinstanceCyclicaly");
			__StepID = @Connector.Online.Adapter.CreateINT(this, "", "_StepID");
			__Enabled = @Connector.Online.Adapter.CreateBOOL(this, "", "_Enabled");
			__StepDescription = @Connector.Online.Adapter.CreateSTRING(this, "", "_StepDescription");
			__RunOneStep = @Connector.Online.Adapter.CreateBOOL(this, "", "_RunOneStep");
			__RunAllSteps = @Connector.Online.Adapter.CreateBOOL(this, "", "_RunAllSteps");
			__FinishStep = @Connector.Online.Adapter.CreateBOOL(this, "", "_FinishStep");
			__Reset = @Connector.Online.Adapter.CreateBOOL(this, "", "_Reset");
			__CycleCount = @Connector.Online.Adapter.CreateINT(this, "", "_CycleCount");
			__ResetCycleCount = @Connector.Online.Adapter.CreateINT(this, "", "_ResetCycleCount");
			__Init = @Connector.Online.Adapter.CreateBOOL(this, "", "_Init");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public TcoSequencerNonAutoRestorableTest()
		{
			PexPreConstructorParameterless();
			__Sequencer = new _TcoSequencer();
			__RunPLCinstanceOnce = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__RunPLCinstanceCyclicaly = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__StepID = Vortex.Connector.IConnectorFactory.CreateINT();
			__Enabled = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__StepDescription = Vortex.Connector.IConnectorFactory.CreateSTRING();
			__RunOneStep = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__RunAllSteps = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__FinishStep = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__Reset = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__CycleCount = Vortex.Connector.IConnectorFactory.CreateINT();
			__ResetCycleCount = Vortex.Connector.IConnectorFactory.CreateINT();
			__Init = Vortex.Connector.IConnectorFactory.CreateBOOL();
			AttributeName = "";
			PexConstructorParameterless();
		}

		public System.Boolean ChildHasAutoRestoreEnabled()
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "ChildHasAutoRestoreEnabled", new object[]{});
		}

		public System.Boolean ChildIsAutoRestorable()
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "ChildIsAutoRestorable", new object[]{});
		}

		public void CleanupPointers()
		{
			Connector.InvokeRpc(this.Symbol, "CleanupPointers", new object[]{});
		}

		public void ClearNumberOfSteps()
		{
			Connector.InvokeRpc(this.Symbol, "ClearNumberOfSteps", new object[]{});
		}

		public void ContextClose()
		{
			Connector.InvokeRpc(this.Symbol, "ContextClose", new object[]{});
		}

		public void ContextOpen()
		{
			Connector.InvokeRpc(this.Symbol, "ContextOpen", new object[]{});
		}

		public System.Int16 GetChildState()
		{
			return (System.Int16)Connector.InvokeRpc(this.Symbol, "GetChildState", new object[]{});
		}

		public System.UInt16 GetCurrentStepOrder()
		{
			return (System.UInt16)Connector.InvokeRpc(this.Symbol, "GetCurrentStepOrder", new object[]{});
		}

		public System.UInt16 GetNumberOfStepsInSequence()
		{
			return (System.UInt16)Connector.InvokeRpc(this.Symbol, "GetNumberOfStepsInSequence", new object[]{});
		}

		public System.UInt16 GetOrderOfTheCurrentlyEvaluatedStep()
		{
			return (System.UInt16)Connector.InvokeRpc(this.Symbol, "GetOrderOfTheCurrentlyEvaluatedStep", new object[]{});
		}

		public System.UInt16 GetOrderOfTheCurrentlyExecutedStep()
		{
			return (System.UInt16)Connector.InvokeRpc(this.Symbol, "GetOrderOfTheCurrentlyExecutedStep", new object[]{});
		}

		public System.UInt16 GetPreviousNumberOfStepsInSequence()
		{
			return (System.UInt16)Connector.InvokeRpc(this.Symbol, "GetPreviousNumberOfStepsInSequence", new object[]{});
		}

		public System.UInt16 GetRequestStepCycle()
		{
			return (System.UInt16)Connector.InvokeRpc(this.Symbol, "GetRequestStepCycle", new object[]{});
		}

		public System.Int16 GetSequencerErrorId()
		{
			return (System.Int16)Connector.InvokeRpc(this.Symbol, "GetSequencerErrorId", new object[]{});
		}

		public System.String GetTextOfTheMostImportantMessage()
		{
			return (System.String)Connector.InvokeRpc(this.Symbol, "GetTextOfTheMostImportantMessage", new object[]{});
		}

		public System.Boolean HasAutoRestoreEnabled()
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "HasAutoRestoreEnabled", new object[]{});
		}

		public System.Boolean IsAutoRestorable()
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "IsAutoRestorable", new object[]{});
		}

		public System.Boolean IsFirstCycle()
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "IsFirstCycle", new object[]{});
		}

		public System.Boolean IsNewTier()
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "IsNewTier", new object[]{});
		}

		public void PLCinstanceRun(System.UInt16 inStepId, System.Boolean inEnabled, System.String inStepDescription)
		{
			Connector.InvokeRpc(this.Symbol, "PLCinstanceRun", new object[]{inStepId, inEnabled, inStepDescription});
		}

		public System.Boolean ProbeIsNewTier()
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "ProbeIsNewTier", new object[]{});
		}

		public System.Boolean ProbeRealNewTier()
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "ProbeRealNewTier", new object[]{});
		}

		public void RequestStep(System.Int16 inRequestedStepId)
		{
			Connector.InvokeRpc(this.Symbol, "RequestStep", new object[]{inRequestedStepId});
		}

		public void Reset()
		{
			Connector.InvokeRpc(this.Symbol, "Reset", new object[]{});
		}

		public void SequenceComplete()
		{
			Connector.InvokeRpc(this.Symbol, "SequenceComplete", new object[]{});
		}

		public void SequencerClose()
		{
			Connector.InvokeRpc(this.Symbol, "SequencerClose", new object[]{});
		}

		public System.Boolean SequencerHasError()
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "SequencerHasError", new object[]{});
		}

		public void SequencerOpen()
		{
			Connector.InvokeRpc(this.Symbol, "SequencerOpen", new object[]{});
		}

		public void SetChildState(System.Int16 inState)
		{
			Connector.InvokeRpc(this.Symbol, "SetChildState", new object[]{inState});
		}

		public void SetCurrentStep(System.Int16 inStepID, System.String inStepDescription)
		{
			Connector.InvokeRpc(this.Symbol, "SetCurrentStep", new object[]{inStepID, inStepDescription});
		}

		public System.Boolean SetCyclicMode()
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "SetCyclicMode", new object[]{});
		}

		public System.UInt16 SetNumberOfSteps(System.UInt16 inNumberOfSteps)
		{
			return (System.UInt16)Connector.InvokeRpc(this.Symbol, "SetNumberOfSteps", new object[]{inNumberOfSteps});
		}

		public System.UInt16 SetRequestStepCycle(System.UInt16 Value)
		{
			return (System.UInt16)Connector.InvokeRpc(this.Symbol, "SetRequestStepCycle", new object[]{Value});
		}

		public System.Boolean SetSequenceAsChecked()
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "SetSequenceAsChecked", new object[]{});
		}

		public System.Boolean SetSequenceAsNotChecked()
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "SetSequenceAsNotChecked", new object[]{});
		}

		public System.Boolean SetStepMode()
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "SetStepMode", new object[]{});
		}

		public System.Boolean Step(System.Int16 inStepId, System.Boolean inEnabled, System.String inStepDescription)
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "Step", new object[]{inStepId, inEnabled, inStepDescription});
		}

		public void StepBackward()
		{
			Connector.InvokeRpc(this.Symbol, "StepBackward", new object[]{});
		}

		public void StepCompleteWhen(System.Boolean inCondition)
		{
			Connector.InvokeRpc(this.Symbol, "StepCompleteWhen", new object[]{inCondition});
		}

		public void StepForward()
		{
			Connector.InvokeRpc(this.Symbol, "StepForward", new object[]{});
		}

		public void StepIn()
		{
			Connector.InvokeRpc(this.Symbol, "StepIn", new object[]{});
		}

		public void UpdateCurrentStepDetails()
		{
			Connector.InvokeRpc(this.Symbol, "UpdateCurrentStepDetails", new object[]{});
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoSequencerNonAutoRestorableTest
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoSequencerNonAutoRestorableTest()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoSequencerNonAutoRestorableTest : Vortex.Connector.IVortexOnlineObject
	{
		I_TcoSequencer _Sequencer
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool _RunPLCinstanceOnce
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool _RunPLCinstanceCyclicaly
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineInt _StepID
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool _Enabled
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineString _StepDescription
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool _RunOneStep
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool _RunAllSteps
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool _FinishStep
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool _Reset
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineInt _CycleCount
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineInt _ResetCycleCount
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool _Init
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoSequencerNonAutoRestorableTest CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainTcoSequencerNonAutoRestorableTest source);
		void FlushOnlineToPlain(TcoCoreTests.PlainTcoSequencerNonAutoRestorableTest source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoSequencerNonAutoRestorableTest : Vortex.Connector.IVortexShadowObject
	{
		IShadow_TcoSequencer _Sequencer
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool _RunPLCinstanceOnce
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool _RunPLCinstanceCyclicaly
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowInt _StepID
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool _Enabled
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowString _StepDescription
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool _RunOneStep
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool _RunAllSteps
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool _FinishStep
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool _Reset
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowInt _CycleCount
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowInt _ResetCycleCount
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool _Init
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoSequencerNonAutoRestorableTest CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainTcoSequencerNonAutoRestorableTest source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoSequencerNonAutoRestorableTest : Vortex.Connector.IPlain
	{
		Plain_TcoSequencer __Sequencer;
		public Plain_TcoSequencer _Sequencer
		{
			get
			{
				return __Sequencer;
			}

			set
			{
				if (__Sequencer != value)
				{
					__Sequencer = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_Sequencer)));
				}
			}
		}

		System.Boolean __RunPLCinstanceOnce;
		public System.Boolean _RunPLCinstanceOnce
		{
			get
			{
				return __RunPLCinstanceOnce;
			}

			set
			{
				if (__RunPLCinstanceOnce != value)
				{
					__RunPLCinstanceOnce = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_RunPLCinstanceOnce)));
				}
			}
		}

		System.Boolean __RunPLCinstanceCyclicaly;
		public System.Boolean _RunPLCinstanceCyclicaly
		{
			get
			{
				return __RunPLCinstanceCyclicaly;
			}

			set
			{
				if (__RunPLCinstanceCyclicaly != value)
				{
					__RunPLCinstanceCyclicaly = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_RunPLCinstanceCyclicaly)));
				}
			}
		}

		System.Int16 __StepID;
		public System.Int16 _StepID
		{
			get
			{
				return __StepID;
			}

			set
			{
				if (__StepID != value)
				{
					__StepID = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_StepID)));
				}
			}
		}

		System.Boolean __Enabled;
		public System.Boolean _Enabled
		{
			get
			{
				return __Enabled;
			}

			set
			{
				if (__Enabled != value)
				{
					__Enabled = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_Enabled)));
				}
			}
		}

		System.String __StepDescription;
		public System.String _StepDescription
		{
			get
			{
				return __StepDescription;
			}

			set
			{
				if (__StepDescription != value)
				{
					__StepDescription = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_StepDescription)));
				}
			}
		}

		System.Boolean __RunOneStep;
		public System.Boolean _RunOneStep
		{
			get
			{
				return __RunOneStep;
			}

			set
			{
				if (__RunOneStep != value)
				{
					__RunOneStep = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_RunOneStep)));
				}
			}
		}

		System.Boolean __RunAllSteps;
		public System.Boolean _RunAllSteps
		{
			get
			{
				return __RunAllSteps;
			}

			set
			{
				if (__RunAllSteps != value)
				{
					__RunAllSteps = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_RunAllSteps)));
				}
			}
		}

		System.Boolean __FinishStep;
		public System.Boolean _FinishStep
		{
			get
			{
				return __FinishStep;
			}

			set
			{
				if (__FinishStep != value)
				{
					__FinishStep = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_FinishStep)));
				}
			}
		}

		System.Boolean __Reset;
		public System.Boolean _Reset
		{
			get
			{
				return __Reset;
			}

			set
			{
				if (__Reset != value)
				{
					__Reset = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_Reset)));
				}
			}
		}

		System.Int16 __CycleCount;
		public System.Int16 _CycleCount
		{
			get
			{
				return __CycleCount;
			}

			set
			{
				if (__CycleCount != value)
				{
					__CycleCount = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_CycleCount)));
				}
			}
		}

		System.Int16 __ResetCycleCount;
		public System.Int16 _ResetCycleCount
		{
			get
			{
				return __ResetCycleCount;
			}

			set
			{
				if (__ResetCycleCount != value)
				{
					__ResetCycleCount = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_ResetCycleCount)));
				}
			}
		}

		System.Boolean __Init;
		public System.Boolean _Init
		{
			get
			{
				return __Init;
			}

			set
			{
				if (__Init != value)
				{
					__Init = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_Init)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCoreTests.TcoSequencerNonAutoRestorableTest target)
		{
			_Sequencer.CopyPlainToCyclic(target._Sequencer);
			target._RunPLCinstanceOnce.Cyclic = _RunPLCinstanceOnce;
			target._RunPLCinstanceCyclicaly.Cyclic = _RunPLCinstanceCyclicaly;
			target._StepID.Cyclic = _StepID;
			target._Enabled.Cyclic = _Enabled;
			target._StepDescription.Cyclic = _StepDescription;
			target._RunOneStep.Cyclic = _RunOneStep;
			target._RunAllSteps.Cyclic = _RunAllSteps;
			target._FinishStep.Cyclic = _FinishStep;
			target._Reset.Cyclic = _Reset;
			target._CycleCount.Cyclic = _CycleCount;
			target._ResetCycleCount.Cyclic = _ResetCycleCount;
			target._Init.Cyclic = _Init;
		}

		public void CopyPlainToCyclic(TcoCoreTests.ITcoSequencerNonAutoRestorableTest target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.TcoSequencerNonAutoRestorableTest)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.TcoSequencerNonAutoRestorableTest target)
		{
			_Sequencer.CopyPlainToShadow(target._Sequencer);
			target._RunPLCinstanceOnce.Shadow = _RunPLCinstanceOnce;
			target._RunPLCinstanceCyclicaly.Shadow = _RunPLCinstanceCyclicaly;
			target._StepID.Shadow = _StepID;
			target._Enabled.Shadow = _Enabled;
			target._StepDescription.Shadow = _StepDescription;
			target._RunOneStep.Shadow = _RunOneStep;
			target._RunAllSteps.Shadow = _RunAllSteps;
			target._FinishStep.Shadow = _FinishStep;
			target._Reset.Shadow = _Reset;
			target._CycleCount.Shadow = _CycleCount;
			target._ResetCycleCount.Shadow = _ResetCycleCount;
			target._Init.Shadow = _Init;
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowTcoSequencerNonAutoRestorableTest target)
		{
			this.CopyPlainToShadow((TcoCoreTests.TcoSequencerNonAutoRestorableTest)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.TcoSequencerNonAutoRestorableTest source)
		{
			_Sequencer.CopyCyclicToPlain(source._Sequencer);
			_RunPLCinstanceOnce = source._RunPLCinstanceOnce.LastValue;
			_RunPLCinstanceCyclicaly = source._RunPLCinstanceCyclicaly.LastValue;
			_StepID = source._StepID.LastValue;
			_Enabled = source._Enabled.LastValue;
			_StepDescription = source._StepDescription.LastValue;
			_RunOneStep = source._RunOneStep.LastValue;
			_RunAllSteps = source._RunAllSteps.LastValue;
			_FinishStep = source._FinishStep.LastValue;
			_Reset = source._Reset.LastValue;
			_CycleCount = source._CycleCount.LastValue;
			_ResetCycleCount = source._ResetCycleCount.LastValue;
			_Init = source._Init.LastValue;
		}

		public void CopyCyclicToPlain(TcoCoreTests.ITcoSequencerNonAutoRestorableTest source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.TcoSequencerNonAutoRestorableTest)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.TcoSequencerNonAutoRestorableTest source)
		{
			_Sequencer.CopyShadowToPlain(source._Sequencer);
			_RunPLCinstanceOnce = source._RunPLCinstanceOnce.Shadow;
			_RunPLCinstanceCyclicaly = source._RunPLCinstanceCyclicaly.Shadow;
			_StepID = source._StepID.Shadow;
			_Enabled = source._Enabled.Shadow;
			_StepDescription = source._StepDescription.Shadow;
			_RunOneStep = source._RunOneStep.Shadow;
			_RunAllSteps = source._RunAllSteps.Shadow;
			_FinishStep = source._FinishStep.Shadow;
			_Reset = source._Reset.Shadow;
			_CycleCount = source._CycleCount.Shadow;
			_ResetCycleCount = source._ResetCycleCount.Shadow;
			_Init = source._Init.Shadow;
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowTcoSequencerNonAutoRestorableTest source)
		{
			this.CopyShadowToPlain((TcoCoreTests.TcoSequencerNonAutoRestorableTest)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoSequencerNonAutoRestorableTest()
		{
			__Sequencer = new Plain_TcoSequencer();
		}
	}
}