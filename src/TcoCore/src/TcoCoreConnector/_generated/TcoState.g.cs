using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
	
///		<summary>
///			Provides basic state controller. It could be implemented using <c>IF</c>, <c>IF</c> <c>ELSIF</c> or <c>CASE</c> statement. The transition to the different state needs to be perfromed by calling 
///			<see cref="TcoState.PlcTcoState.ChangeState(Object)"/> method. This method ensures also calling the <see cref="TcoState.PlcTcoState.OnStateChange(Object,Object)"/> every time that state is changed. The 
///		  	<c>OnStateChange()</c>could be overloaded and some custom code that needs to be executed on each change of the state could be placed here. The TcoState could be used like this:
///			<para>
///				<example>
///				<note type="Example 1">
///				<para>
///					<c>_myTask()</c> is not a member of this <c>TcoState()</c>, it is restored manually to be able to trigger it in the next state.
///				</para>
///					<code>
///						IF State = 10 THEN
///							Main._myTask.Invoke();
///							Main._myTask.Execute();
///							IF Main._myTask.Done THEN
///								ChangeState(State + 10);
///								Main._myTask.Restore();
///							END_IF
///						END_IF
///						IF State = 20 THEN
///							Main._myTask.Invoke();
///							Main._myTask.Execute();
///							IF Main._myTask.Done THEN
///								ChangeState(State + 10);
///								Main._myTask.Restore();
///							END_IF
///						END_IF
///					</code>
///				 </note>
///				</example>
///			</para>		
///			<para>
///				<example>
///				<note type="Example 2">
///				<para>
///					<c>_myTask()</c> is a member of this <c>TcoState()</c>, it is restored manually using the fluent syntax to be able to trigger it in the next state.
///				</para>
///					<code>
///						CASE State OF
///							10:	
///							_myTask.Invoke();
///							_myTask.Execute();
///							IF _myTask.Done THEN
///								ChangeState(State + 10).RestoreObject(_myTask);
///							END_IF
///							20:
///							_myTask.Invoke();
///							_myTask.Execute();
///							IF _myTask.Done THEN
///								ChangeState(State + 10).RestoreObject(_myTask);
///							END_IF	
///						END_CASE
///					</code>
///				 </note>
///				</example>
///				<para>
///					See <see cref="TcoState.PlcTcoState.RestoreObject(Object)"/> for more details.
///				</para>
///			</para>			
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
///			Provides basic state controller. It could be implemented using <c>IF</c>, <c>IF</c> <c>ELSIF</c> or <c>CASE</c> statement. The transition to the different state needs to be perfromed by calling 
///			<see cref="TcoState.PlcTcoState.ChangeState(Object)"/> method. This method ensures also calling the <see cref="TcoState.PlcTcoState.OnStateChange(Object,Object)"/> every time that state is changed. The 
///		  	<c>OnStateChange()</c>could be overloaded and some custom code that needs to be executed on each change of the state could be placed here. The TcoState could be used like this:
///			<para>
///				<example>
///				<note type="Example 1">
///				<para>
///					<c>_myTask()</c> is not a member of this <c>TcoState()</c>, it is restored manually to be able to trigger it in the next state.
///				</para>
///					<code>
///						IF State = 10 THEN
///							Main._myTask.Invoke();
///							Main._myTask.Execute();
///							IF Main._myTask.Done THEN
///								ChangeState(State + 10);
///								Main._myTask.Restore();
///							END_IF
///						END_IF
///						IF State = 20 THEN
///							Main._myTask.Invoke();
///							Main._myTask.Execute();
///							IF Main._myTask.Done THEN
///								ChangeState(State + 10);
///								Main._myTask.Restore();
///							END_IF
///						END_IF
///					</code>
///				 </note>
///				</example>
///			</para>		
///			<para>
///				<example>
///				<note type="Example 2">
///				<para>
///					<c>_myTask()</c> is a member of this <c>TcoState()</c>, it is restored manually using the fluent syntax to be able to trigger it in the next state.
///				</para>
///					<code>
///						CASE State OF
///							10:	
///							_myTask.Invoke();
///							_myTask.Execute();
///							IF _myTask.Done THEN
///								ChangeState(State + 10).RestoreObject(_myTask);
///							END_IF
///							20:
///							_myTask.Invoke();
///							_myTask.Execute();
///							IF _myTask.Done THEN
///								ChangeState(State + 10).RestoreObject(_myTask);
///							END_IF	
///						END_CASE
///					</code>
///				 </note>
///				</example>
///				<para>
///					See <see cref="TcoState.PlcTcoState.RestoreObject(Object)"/> for more details.
///				</para>
///			</para>			
///		</summary>		

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoState : TcoCore.TcoObject.PlcTcoObject
		{
			
///		<summary>
///			Private method called each time that State property is asked for. If this instance is auto restorable, this method ensures calling the <see cref="TcoState.PlcTcoState.Restore()"/> method if:
///			<para>
///				1.)	Parent object of this instance changes its state in this Plc cycle.
///			</para>
///			<para>
///				2.)	Parent object of this instance changes its state no mather when and this instance has not yet process this change.
///			</para>
///			<para>
///				See also <see cref="TcoState.PlcTcoState.AutoRestorable()"/>
///			</para>
///			<para>
///				See also <see cref="TcoState.PlcTcoState.EnableAutoRestore()"/>
///			</para>
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute()]
			private void AutoRestore()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			Use to change the State property of this instance to the new value. This method is final, so it cannot be overloaded. However there is a <see cref="TcoState.PlcTcoState.OnStateChange(Object,Object)"/> 
///			method that is called on each change of the <c>State</c> and could be overloaded.
///			<para>
///				<example>
///				<note type="Example">
///				<para>
///					Possibility to use the fluent syntax.
///				</para>
///					<code>ChangeState(newState).RestoreObject(myChildObject);</code>
///				 </note>
///				</example>
///			</para>		
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<param name="NewState">
///<para>Plc type : INT [VAR_INPUT]; Twin type : <see cref="Vortex.Connector.ValueTypes.OnlinerInt"/></para>
///<para></para>
///</param>

///<returns>Plc type ITcoState; Twin type: <see cref="ITcoState"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute()]
			public dynamic ChangeState(dynamic NewState)
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			This method ensures reading out the parent's <see cref="TcoState.PlcTcoState.EnablAutoRestore()/> property and writing it into the <see cref="TcoState.PlcTcoState.AutoRestorable()"/> of this instance.
///			Calling this method is ensured by calling the implicit method <c>FB_init()</c> after download, or after online change.
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute()]
			private void CheckIfAutoRestoreEnabledByParent()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			Private method called each time that State property is asked for. If this instance is auto restorable, this method ensures calling the <see cref="TcoState.PlcTcoState.Restore()"/> method if:
///			<para>
///				This instance has not been called for one or more cycles and starts to be called again. (By calling the instance means asking for its property State).
///			</para>
///			<para>
///				See also <see cref="TcoState.PlcTcoState.AutoRestorable()"/>
///			</para>
///			<para>
///				See also <see cref="TcoState.PlcTcoState.EnableAutoRestore()"/>
///			</para>
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute()]
			private void CheckIfCalledCyclically()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			This method is called on each change of the <c>State</c>. This method could be overloaded and custom code should be placed here.	
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<param name="PreviousState">
///<para>Plc type : INT [VAR_INPUT]; Twin type : <see cref="Vortex.Connector.ValueTypes.OnlinerInt"/></para>
///<para></para>
///</param>

///<param name="NewState">
///<para>Plc type : INT [VAR_INPUT]; Twin type : <see cref="Vortex.Connector.ValueTypes.OnlinerInt"/></para>
///<para></para>
///</param>

///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute()]
			public void OnStateChange(dynamic PreviousState, dynamic NewState)
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			Restore this instance to the state -1. Could be called explicitly so as using auto restore mechanism.
///			<para>
///				<example>
///					<note type="Example">
///						<para>
///							Possibility to use the fluent syntax.
///						</para>
///						<code>
///							Restore().RestoreObject(myFirstChildObject).RestoreObject(mySecondChildObject);
///						</code>
///					 </note>
///				</example>
///			</para>				
///			<para>
///				See also <see cref="TcoState.PlcTcoState.AutoRestorable()"/>
///			</para>
///			<para>
///				See also <see cref="TcoState.PlcTcoState.EnableAutoRestore()"/>
///			</para>
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type ITcoRestorable; Twin type: <see cref="ITcoRestorable"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute()]
			public dynamic Restore()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			Ensures calling the <c>Restore()</c> method on the object entered.	
///			<para>
///				<example>
///				<note type="Example">
///				<para>
///					Possibility to use the fluent syntax.
///				</para>
///					<code>ChangeState(newState).RestoreObject(myFirstChildObject).RestoreObject(mySecondChildObject);</code>
///				 </note>
///				</example>
///			</para>		
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<param name="Obj">
///<para>Plc type : ITcoRestorable [VAR_INPUT]; Twin type : <see cref="ITcoRestorable"/></para>
///<para></para>
///</param>

///<returns>Plc type ITcoState; Twin type: <see cref="ITcoState"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute()]
			public dynamic RestoreObject(dynamic Obj)
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			Returns if this instance is auto restorable. This is 'dependency property'. Value of this property is given by the parent of this object. 
///			This value is assigned after download by calling the implicit method <c>FB_init()</c> of the parent object and cannot be changed during runtime.
///			The <c>AutoRestorable</c> property of this objects is retrieved from the <c>EnableAutoRestore</c> property of the parent object.
///			This is done by calling the <c>CheckIfAutoRestoreEnabledByParent()</c> method inside the implicit method <c>FB_init()</c> after download.
///			If this instance is the auto restorable it will call the <see cref="TcoState.PlcTcoState.Restore()"/> method if:
///			<para>
///			1.) This instance has not been called for one or more cycles and starts to be called again. (By calling the instance means asking for its property <c>State</c>).
///			</para>
///			<para>
///			2.)	Parent object of this instance changes its state.
///			</para>
///			<para>
///				See also <see cref="TcoState.PlcTcoState.EnableAutoRestore()"/>
///			</para>
///		</summary>			
///<summary><note type="note">This is PLC property. This method is accessible only from the PLC code.</note></summary>
///<returns>Plc type BOOL; Twin type: <see cref="Vortex.Connector.ValueTypes.OnlinerBool"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute()]
			public dynamic AutoRestorable
			{
				get
				{
					throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
				}
			}

			
///		<summary>
///			Returns number of the Plc cycle during which this instance changes its <c>State</c> last time.
///			The child objects ask for this value to ensure auto restore mechanism working.  	
///		</summary>			
///<summary><note type="note">This is PLC property. This method is accessible only from the PLC code.</note></summary>
///<returns>Plc type ULINT; Twin type: <see cref="Vortex.Connector.ValueTypes.OnlinerULInt"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute()]
			public dynamic ChangeStateCycle
			{
				get
				{
					throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
				}
			}

			
///		<summary>
///			Returns the context of the parent object, that this object is assigned to.
///			This context is given by declaration, its value is assigned after download by calling the implicit method <c>FB_init()</c> and cannot be changed during runtime.
///		</summary>			
///<summary><note type="note">This is PLC property. This method is accessible only from the PLC code.</note></summary>
///<returns>Plc type ITcoContext; Twin type: <see cref="ITcoContext"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute()]
			public dynamic Context
			{
				get
				{
					throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
				}
			}

			
///		<summary>
///			Returns if the auto restore mechanism is enabled for this instance. This property does not affect this instance, but it affects all members of this instance.
///			If the auto restore is enabled, the child member call its <c>Restore()</c> method if:
///			<para>
///			1.) Its instance has not been called for one or more cycles.
///			</para>
///			<para>
///			2.)	Its parent object (THIS^ instance) has changed its state.
///			</para>
///			This value is assigned after download by calling the implicit method <c>FB_init()</c> and cannot be changed during runtime.
///			<para>
///				See also <see cref="TcoState.PlcTcoState.AutoRestorable()"/>
///			</para>
///		</summary>			
///<summary><note type="note">This is PLC property. This method is accessible only from the PLC code.</note></summary>
///<returns>Plc type BOOL; Twin type: <see cref="Vortex.Connector.ValueTypes.OnlinerBool"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute()]
			public dynamic EnableAutoRestore
			{
				get
				{
					throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
				}
			}

			
///		<summary>
///			Returns the own identity of the <see cref ="TcoState.PlcTcoState()"/>. This value is assigned after download by calling the implicit method <c>FB_init()</c> and cannot be changed during runtime.
///			This variable is used in the higher level packages.  
///		</summary>			
///<summary><note type="note">This is PLC property. This method is accessible only from the PLC code.</note></summary>
///<returns>Plc type ULINT; Twin type: <see cref="Vortex.Connector.ValueTypes.OnlinerULInt"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute()]
			public dynamic Identity
			{
				get
				{
					throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
				}
			}

			
///		<summary>
///			Returns last <c>State</c> into which this instance has been changed lasttime.
///			The child objects ask for this value to ensure auto restore mechanism working.  	
///		</summary>			
///<summary><note type="note">This is PLC property. This method is accessible only from the PLC code.</note></summary>
///<returns>Plc type INT; Twin type: <see cref="Vortex.Connector.ValueTypes.OnlinerInt"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute()]
			public dynamic LastChangedState
			{
				get
				{
					throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
				}
			}

			
///		<summary>
///			Return actual state of this instance. Needs to be asked for cyclically in case of this instance is auto restorable.
///			<para>
///				See also <see cref="TcoState.PlcTcoState.AutoRestorable()"/>
///			</para>
///		</summary>			
///<summary><note type="note">This is PLC property. This method is accessible only from the PLC code.</note></summary>
///<returns>Plc type INT; Twin type: <see cref="Vortex.Connector.ValueTypes.OnlinerInt"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute()]
			public dynamic State
			{
				get
				{
					throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
				}
			}

			public object _State;
			public object _enableAutoRestore;
			public object _AutoRestorable;
			public object _StartCycleCount;
			public object _MyParentsChangeStateCycle;
			public object _ChangeStateCycle;
			public object _LastChangedState;
			public object _MyParentsLastChangedState;
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