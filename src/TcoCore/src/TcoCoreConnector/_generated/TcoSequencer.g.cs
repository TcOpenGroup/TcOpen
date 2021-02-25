using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
	
///			<summary>
///				Provides basic sequential control, including step mode, which allows to run the sequence step-by step, step forward and step backward.
///			</summary>
///<seealso cref="PlcTcoSequencer"/>
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoSequencer", "TcoCore", TypeComplexityEnum.Complex)]
	public partial class TcoSequencer : TcoState, Vortex.Connector.IVortexObject, ITcoSequencer, IShadowTcoSequencer, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		StepDetails __currentStep;
		
///		<summary>
///			Holds the status and internal variables of the current step.
///		</summary>			
///		<remarks>			
///			<para>
///				See <see cref="StepDetails"/> for detailed description.
///			</para>
///		</remarks>		

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
		
///		<summary>
///			True if an error occurs in the sequence. Detailed cause of this error is described by the _sequencerErrorId.		
///		</summary>				
///		<remarks>			
///			<para>
///				See <see cref="TcoSequencer.PlcTcoSequencer._sequencerErrorId"/> for detailed description.
///			</para>
///		</remarks>	

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
		
///		<summary>
///			Describes cause of the sequencer error.
///		</summary>		
///		<remarks>			
///			<para>
///				See <see cref="eSequencerError"/> for detailed description.
///			</para>
///		</remarks>			

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

		
///			<summary>
///				Provides basic sequential control, including step mode, which allows to run the sequence step-by step, step forward and step backward.
///			</summary>

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoSequencer : TcoCore.TcoState.PlcTcoState
		{
			
///		<summary>
///			Allows to set required mode to sequencer.
///			Also it returns the actual selected mode.
///			<remarks>			
///				<para>
///					See <see cref="eSequencerMode"/> for detailed description.
///				</para>
///			</remarks>		
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
			protected PlcTcoSequencer()
			{
			}

			
///		<summary>
///			Performs the control of the StepId of the currently executed step.
///			<remarks>			
///				<note type="important">
///					The StepId of each step in the sequence must be unique throughout the complete sequence, and it must not be changed.
///				</note>
///			</remarks>			
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<param name="inStepID">
///<para>Plc type : INT [VAR_INPUT]; Twin type : <see cref="Vortex.Connector.ValueTypes.OnlinerInt"/></para>
///<para></para>
///</param>

///<param name="inStepModeActive">
///<para>Plc type : BOOL [VAR_INPUT]; Twin type : <see cref="Vortex.Connector.ValueTypes.OnlinerBool"/></para>
///<para></para>
///</param>

///<param name="inStepInRunning">
///<para>Plc type : BOOL [VAR_INPUT]; Twin type : <see cref="Vortex.Connector.ValueTypes.OnlinerBool"/></para>
///<para></para>
///</param>

///<returns>Plc type BOOL; Twin type: <see cref="Vortex.Connector.ValueTypes.OnlinerBool"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			private dynamic CheckStepId(dynamic inStepID, dynamic inStepModeActive, dynamic inStepInRunning)
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			This method is called at the end of the sequence. Implicit calling of this method is ensured by calling the method Run().
///			<remarks>			
///				<note type="important">
///					Do not call this method explicitly.
///				</note>
///			</remarks>
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			protected void Close()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			Performs the control of the uniqueness of each StepId in the whole sequence.
///			<remarks>			
///				<note type="important">
///					The StepId of each step in the sequence must be unique throughout the complete sequence, and it must not be changed.
///				</note>
///			</remarks>			
///		</summary>				
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<param name="inStepID">
///<para>Plc type : INT [VAR_INPUT]; Twin type : <see cref="Vortex.Connector.ValueTypes.OnlinerInt"/></para>
///<para>
///		<summary>
///			StepId to be checked for uniqueness.
///		</summary>				
///	</para>
///</param>

///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			private void IsStepIdUnique(dynamic inStepID)
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			Main method of the sequener. Custom code needs to be placed here, calling of the methods Open() at the beggining and Close() at the end is ensured by calling the InstanceName.Run() method.
///			This method is abstract, so each derived type has to implement its own implementation of this method.
///		</summary>
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type BOOL; Twin type: <see cref="Vortex.Connector.ValueTypes.OnlinerBool"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public dynamic Main()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			Ensures that after each download, StepId uniqueness control is going to be performed again. 
///			<remarks>			
///				<note type="important">
///					Do not call this method explicitly.
///				</note>
///			</remarks>		
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			internal void OnlineChange()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			This method is called at the beginning of the sequence. Implicit calling of this method is ensured by calling the method Run().
///			<remarks>			
///				<note type="important">
///					Do not call this method explicitly.
///				</note>
///			</remarks>
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			protected void Open()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			This method is called at the end of the sequence. It is public, so it should be overloaded and customized.
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public void PostSequenceComplete()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			This method is called at the end of each step. It is public, so it should be overloaded and customized.
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public void PostStepComplete()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			Finishes the currently executed step and initiates to start the step with StepId equals to the value of the inRequestedStepID.
///			In case that the order of the requested step is higher than the order of the currently finished step (the requested step is "after" the current one)
///			the requested step is started in the same PLC cycle.
///			In case that the order of the requested step is lower than the order of the currently finished step (the requested step is "before" the current one)
///			the requested step is started in the next PLC cycle.
///			If the requested step is not found even in the next PLC cycle, the sequencer returns the error StepWithRequestedIdDoesNotExists.
///			<para>
///				See <see cref="eSequencerError"/> for detailed description.
///			</para>
///		</summary>
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<param name="inRequestedStepID">
///<para>Plc type : UINT [VAR_INPUT]; Twin type : <see cref="Vortex.Connector.ValueTypes.OnlinerUInt"/></para>
///<para>
///			<summary>
///				StepId of the step to be executed.
///			</summary>
///		</para>
///</param>

///<returns>Plc type ITcoSequencer; Twin type: <see cref="ITcoSequencer"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public dynamic RequestStep(dynamic inRequestedStepID)
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			<para>
///				This method resets the sequencer. 
///				Method is typically called before starting the sequence or after error has been occured in the sequence.
///			</para>
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public void Reset()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			<para>
///				Ensures calling the Open(), Main() and Close() methods in the desired order.
///				This method is final, so it cannot be overloaded. The InstanceName.Run() needs to be called cyclically inside the appropriate context.
///			</para>
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public void Run()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			<para>
///				Complete the sequencer.
///				Method is typically called inside the last step of the sequence.
///			</para>
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public void SequenceComplete()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///				Basic step of the sequence. 
///		</summary>			
///		<returns>
///			True in case if step with given StepId is in order of the execution, the step is enabled and the sequencer is in the cyclic mode.
///			True in case if step with given StepId is in order of the execution, the step is enabled, the sequencer is in the step mode and StepIn() method was triggered.  
///			False in all the other cases.
///		</returns>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<param name="inStepID">
///<para>Plc type : INT [VAR_INPUT]; Twin type : <see cref="Vortex.Connector.ValueTypes.OnlinerInt"/></para>
///<para>
///		<summary>
///			StepId of the current step.
///			<remarks>			
///				<note type="important">
///					This number must be unique throughout the complete sequence.
///				</note>
///			</remarks>
///		</summary>		
///	</para>
///</param>

///<param name="inEnabled">
///<para>Plc type : BOOL [VAR_INPUT]; Twin type : <see cref="Vortex.Connector.ValueTypes.OnlinerBool"/></para>
///<para>
///		<summary>
///			If this value is false, step body is not executed and execution is moved to the next enabled step.
///		</summary>				
///	</para>
///</param>

///<param name="inStepDescription">
///<para>Plc type : STRING [VAR_INPUT]; Twin type : <see cref="Vortex.Connector.ValueTypes.OnlinerString"/></para>
///<para>
///		<summary>
///			Step description text.
///		</summary>				
///	</para>
///</param>

///<returns>Plc type BOOL; Twin type: <see cref="Vortex.Connector.ValueTypes.OnlinerBool"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public dynamic Step(dynamic inStepID, dynamic inEnabled, dynamic inStepDescription)
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			This method triggers StepBackward task, that decrement current step (variable: <c> TcoSequencer._theOrderOfTheCurrentlyExecutedStep</c> ), in case the sequencer is in step mode, and the current step is greather than zero.
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public void StepBackward()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			This method is called at each change of the current step and changes the sequencer property State derived from the TcoState function block to the StepId of the current step. 
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<param name="newState">
///<para>Plc type : INT [VAR_INPUT]; Twin type : <see cref="Vortex.Connector.ValueTypes.OnlinerInt"/></para>
///<para></para>
///</param>

///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			private void StepChanged(dynamic newState)
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			Complete the actually running step, in the case the inCondition is met.
///			Method is typically called inside the step as the transition method.
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<param name="inCondition">
///<para>Plc type : BOOL [VAR_INPUT]; Twin type : <see cref="Vortex.Connector.ValueTypes.OnlinerBool"/></para>
///<para>
///		<summary>
///			The condition under which the step is completed.
///		</summary>			
///	</para>
///</param>

///<returns>Plc type ITcoSequencer; Twin type: <see cref="ITcoSequencer"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public dynamic StepCompleteWhen(dynamic inCondition)
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			This method triggers StepForward task, that increment current step (variable: <c>TcoSequencer._theOrderOfTheCurrentlyExecutedStep</c> ), 
///			in case the sequencer is in step mode, and the current step is lower than number of steps in th sequence (variable: <c>TcoSequencer._numberOfStepsInSequence</c> ).
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public void StepForward()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			Triggers StepIn task, that changes the current step status from ReadyToRun to Running. 
///			This causes starting the execution of the body of the current step.
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public void StepIn()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
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