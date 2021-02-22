using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "_TcoSequencer", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class _TcoSequencer : Vortex.Connector.IVortexObject, I_TcoSequencer, IShadow_TcoSequencer, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		Vortex.Connector.ValueTypes.OnlinerBool __RunOneStep;
		public Vortex.Connector.ValueTypes.OnlinerBool _RunOneStep
		{
			get
			{
				return __RunOneStep;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool I_TcoSequencer._RunOneStep
		{
			get
			{
				return _RunOneStep;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadow_TcoSequencer._RunOneStep
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

		Vortex.Connector.ValueTypes.Online.IOnlineBool I_TcoSequencer._RunAllSteps
		{
			get
			{
				return _RunAllSteps;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadow_TcoSequencer._RunAllSteps
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

		Vortex.Connector.ValueTypes.Online.IOnlineBool I_TcoSequencer._FinishStep
		{
			get
			{
				return _FinishStep;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadow_TcoSequencer._FinishStep
		{
			get
			{
				return _FinishStep;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerInt __ID;
		public Vortex.Connector.ValueTypes.OnlinerInt _ID
		{
			get
			{
				return __ID;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineInt I_TcoSequencer._ID
		{
			get
			{
				return _ID;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadow_TcoSequencer._ID
		{
			get
			{
				return _ID;
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

		Vortex.Connector.ValueTypes.Online.IOnlineBool I_TcoSequencer._Enabled
		{
			get
			{
				return _Enabled;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadow_TcoSequencer._Enabled
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

		Vortex.Connector.ValueTypes.Online.IOnlineString I_TcoSequencer._StepDescription
		{
			get
			{
				return _StepDescription;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowString IShadow_TcoSequencer._StepDescription
		{
			get
			{
				return _StepDescription;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerInt __currentStepId;
		public Vortex.Connector.ValueTypes.OnlinerInt _currentStepId
		{
			get
			{
				return __currentStepId;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineInt I_TcoSequencer._currentStepId
		{
			get
			{
				return _currentStepId;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadow_TcoSequencer._currentStepId
		{
			get
			{
				return _currentStepId;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerUInt __currentStepOrder;
		public Vortex.Connector.ValueTypes.OnlinerUInt _currentStepOrder
		{
			get
			{
				return __currentStepOrder;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineUInt I_TcoSequencer._currentStepOrder
		{
			get
			{
				return _currentStepOrder;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowUInt IShadow_TcoSequencer._currentStepOrder
		{
			get
			{
				return _currentStepOrder;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool __currentStepEnabled;
		public Vortex.Connector.ValueTypes.OnlinerBool _currentStepEnabled
		{
			get
			{
				return __currentStepEnabled;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool I_TcoSequencer._currentStepEnabled
		{
			get
			{
				return _currentStepEnabled;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadow_TcoSequencer._currentStepEnabled
		{
			get
			{
				return _currentStepEnabled;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerString __currentStepDescription;
		public Vortex.Connector.ValueTypes.OnlinerString _currentStepDescription
		{
			get
			{
				return __currentStepDescription;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineString I_TcoSequencer._currentStepDescription
		{
			get
			{
				return _currentStepDescription;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowString IShadow_TcoSequencer._currentStepDescription
		{
			get
			{
				return _currentStepDescription;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerInt __currentStepStatus;
		public Vortex.Connector.ValueTypes.OnlinerInt _currentStepStatus
		{
			get
			{
				return __currentStepStatus;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineInt I_TcoSequencer._currentStepStatus
		{
			get
			{
				return _currentStepStatus;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadow_TcoSequencer._currentStepStatus
		{
			get
			{
				return _currentStepStatus;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerTime __currentStepDuration;
		public Vortex.Connector.ValueTypes.OnlinerTime _currentStepDuration
		{
			get
			{
				return __currentStepDuration;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineTime I_TcoSequencer._currentStepDuration
		{
			get
			{
				return _currentStepDuration;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowTime IShadow_TcoSequencer._currentStepDuration
		{
			get
			{
				return _currentStepDuration;
			}
		}

		public void LazyOnlineToShadow()
		{
			_RunOneStep.Shadow = _RunOneStep.LastValue;
			_RunAllSteps.Shadow = _RunAllSteps.LastValue;
			_FinishStep.Shadow = _FinishStep.LastValue;
			_ID.Shadow = _ID.LastValue;
			_Enabled.Shadow = _Enabled.LastValue;
			_StepDescription.Shadow = _StepDescription.LastValue;
			_currentStepId.Shadow = _currentStepId.LastValue;
			_currentStepOrder.Shadow = _currentStepOrder.LastValue;
			_currentStepEnabled.Shadow = _currentStepEnabled.LastValue;
			_currentStepDescription.Shadow = _currentStepDescription.LastValue;
			_currentStepStatus.Shadow = _currentStepStatus.LastValue;
			_currentStepDuration.Shadow = _currentStepDuration.LastValue;
		}

		public void LazyShadowToOnline()
		{
			_RunOneStep.Cyclic = _RunOneStep.Shadow;
			_RunAllSteps.Cyclic = _RunAllSteps.Shadow;
			_FinishStep.Cyclic = _FinishStep.Shadow;
			_ID.Cyclic = _ID.Shadow;
			_Enabled.Cyclic = _Enabled.Shadow;
			_StepDescription.Cyclic = _StepDescription.Shadow;
			_currentStepId.Cyclic = _currentStepId.Shadow;
			_currentStepOrder.Cyclic = _currentStepOrder.Shadow;
			_currentStepEnabled.Cyclic = _currentStepEnabled.Shadow;
			_currentStepDescription.Cyclic = _currentStepDescription.Shadow;
			_currentStepStatus.Cyclic = _currentStepStatus.Shadow;
			_currentStepDuration.Cyclic = _currentStepDuration.Shadow;
		}

		public Plain_TcoSequencer CreatePlainerType()
		{
			var cloned = new Plain_TcoSequencer();
			return cloned;
		}

		protected Plain_TcoSequencer CreatePlainerType(Plain_TcoSequencer cloned)
		{
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

		public void FlushPlainToOnline(TcoCoreTests.Plain_TcoSequencer source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.Plain_TcoSequencer source)
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

		public void FlushOnlineToPlain(TcoCoreTests.Plain_TcoSequencer source)
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

		public _TcoSequencer(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			__RunOneStep = @Connector.Online.Adapter.CreateBOOL(this, "", "_RunOneStep");
			__RunAllSteps = @Connector.Online.Adapter.CreateBOOL(this, "", "_RunAllSteps");
			__FinishStep = @Connector.Online.Adapter.CreateBOOL(this, "", "_FinishStep");
			__ID = @Connector.Online.Adapter.CreateINT(this, "", "_ID");
			__Enabled = @Connector.Online.Adapter.CreateBOOL(this, "", "_Enabled");
			__StepDescription = @Connector.Online.Adapter.CreateSTRING(this, "", "_StepDescription");
			__currentStepId = @Connector.Online.Adapter.CreateINT(this, "", "_currentStepId");
			__currentStepOrder = @Connector.Online.Adapter.CreateUINT(this, "", "_currentStepOrder");
			__currentStepEnabled = @Connector.Online.Adapter.CreateBOOL(this, "", "_currentStepEnabled");
			__currentStepDescription = @Connector.Online.Adapter.CreateSTRING(this, "", "_currentStepDescription");
			__currentStepStatus = @Connector.Online.Adapter.CreateINT(this, "", "_currentStepStatus");
			__currentStepDuration = @Connector.Online.Adapter.CreateTIME(this, "", "_currentStepDuration");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public _TcoSequencer()
		{
			PexPreConstructorParameterless();
			__RunOneStep = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__RunAllSteps = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__FinishStep = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__ID = Vortex.Connector.IConnectorFactory.CreateINT();
			__Enabled = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__StepDescription = Vortex.Connector.IConnectorFactory.CreateSTRING();
			__currentStepId = Vortex.Connector.IConnectorFactory.CreateINT();
			__currentStepOrder = Vortex.Connector.IConnectorFactory.CreateUINT();
			__currentStepEnabled = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__currentStepDescription = Vortex.Connector.IConnectorFactory.CreateSTRING();
			__currentStepStatus = Vortex.Connector.IConnectorFactory.CreateINT();
			__currentStepDuration = Vortex.Connector.IConnectorFactory.CreateTIME();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class Plc_TcoSequencer
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected Plc_TcoSequencer()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface I_TcoSequencer : Vortex.Connector.IVortexOnlineObject
	{
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

		Vortex.Connector.ValueTypes.Online.IOnlineInt _ID
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

		Vortex.Connector.ValueTypes.Online.IOnlineInt _currentStepId
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineUInt _currentStepOrder
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool _currentStepEnabled
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineString _currentStepDescription
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineInt _currentStepStatus
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineTime _currentStepDuration
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.Plain_TcoSequencer CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.Plain_TcoSequencer source);
		void FlushOnlineToPlain(TcoCoreTests.Plain_TcoSequencer source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadow_TcoSequencer : Vortex.Connector.IVortexShadowObject
	{
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

		Vortex.Connector.ValueTypes.Shadows.IShadowInt _ID
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

		Vortex.Connector.ValueTypes.Shadows.IShadowInt _currentStepId
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowUInt _currentStepOrder
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool _currentStepEnabled
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowString _currentStepDescription
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowInt _currentStepStatus
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowTime _currentStepDuration
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.Plain_TcoSequencer CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.Plain_TcoSequencer source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class Plain_TcoSequencer : Vortex.Connector.IPlain
	{
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

		System.Int16 __ID;
		public System.Int16 _ID
		{
			get
			{
				return __ID;
			}

			set
			{
				if (__ID != value)
				{
					__ID = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_ID)));
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

		System.Int16 __currentStepId;
		public System.Int16 _currentStepId
		{
			get
			{
				return __currentStepId;
			}

			set
			{
				if (__currentStepId != value)
				{
					__currentStepId = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_currentStepId)));
				}
			}
		}

		System.UInt16 __currentStepOrder;
		public System.UInt16 _currentStepOrder
		{
			get
			{
				return __currentStepOrder;
			}

			set
			{
				if (__currentStepOrder != value)
				{
					__currentStepOrder = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_currentStepOrder)));
				}
			}
		}

		System.Boolean __currentStepEnabled;
		public System.Boolean _currentStepEnabled
		{
			get
			{
				return __currentStepEnabled;
			}

			set
			{
				if (__currentStepEnabled != value)
				{
					__currentStepEnabled = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_currentStepEnabled)));
				}
			}
		}

		System.String __currentStepDescription;
		public System.String _currentStepDescription
		{
			get
			{
				return __currentStepDescription;
			}

			set
			{
				if (__currentStepDescription != value)
				{
					__currentStepDescription = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_currentStepDescription)));
				}
			}
		}

		System.Int16 __currentStepStatus;
		public System.Int16 _currentStepStatus
		{
			get
			{
				return __currentStepStatus;
			}

			set
			{
				if (__currentStepStatus != value)
				{
					__currentStepStatus = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_currentStepStatus)));
				}
			}
		}

		System.TimeSpan __currentStepDuration;
		public System.TimeSpan _currentStepDuration
		{
			get
			{
				return __currentStepDuration;
			}

			set
			{
				if (__currentStepDuration != value)
				{
					__currentStepDuration = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_currentStepDuration)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCoreTests._TcoSequencer target)
		{
			target._RunOneStep.Cyclic = _RunOneStep;
			target._RunAllSteps.Cyclic = _RunAllSteps;
			target._FinishStep.Cyclic = _FinishStep;
			target._ID.Cyclic = _ID;
			target._Enabled.Cyclic = _Enabled;
			target._StepDescription.Cyclic = _StepDescription;
			target._currentStepId.Cyclic = _currentStepId;
			target._currentStepOrder.Cyclic = _currentStepOrder;
			target._currentStepEnabled.Cyclic = _currentStepEnabled;
			target._currentStepDescription.Cyclic = _currentStepDescription;
			target._currentStepStatus.Cyclic = _currentStepStatus;
			target._currentStepDuration.Cyclic = _currentStepDuration;
		}

		public void CopyPlainToCyclic(TcoCoreTests.I_TcoSequencer target)
		{
			this.CopyPlainToCyclic((TcoCoreTests._TcoSequencer)target);
		}

		public void CopyPlainToShadow(TcoCoreTests._TcoSequencer target)
		{
			target._RunOneStep.Shadow = _RunOneStep;
			target._RunAllSteps.Shadow = _RunAllSteps;
			target._FinishStep.Shadow = _FinishStep;
			target._ID.Shadow = _ID;
			target._Enabled.Shadow = _Enabled;
			target._StepDescription.Shadow = _StepDescription;
			target._currentStepId.Shadow = _currentStepId;
			target._currentStepOrder.Shadow = _currentStepOrder;
			target._currentStepEnabled.Shadow = _currentStepEnabled;
			target._currentStepDescription.Shadow = _currentStepDescription;
			target._currentStepStatus.Shadow = _currentStepStatus;
			target._currentStepDuration.Shadow = _currentStepDuration;
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadow_TcoSequencer target)
		{
			this.CopyPlainToShadow((TcoCoreTests._TcoSequencer)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests._TcoSequencer source)
		{
			_RunOneStep = source._RunOneStep.LastValue;
			_RunAllSteps = source._RunAllSteps.LastValue;
			_FinishStep = source._FinishStep.LastValue;
			_ID = source._ID.LastValue;
			_Enabled = source._Enabled.LastValue;
			_StepDescription = source._StepDescription.LastValue;
			_currentStepId = source._currentStepId.LastValue;
			_currentStepOrder = source._currentStepOrder.LastValue;
			_currentStepEnabled = source._currentStepEnabled.LastValue;
			_currentStepDescription = source._currentStepDescription.LastValue;
			_currentStepStatus = source._currentStepStatus.LastValue;
			_currentStepDuration = source._currentStepDuration.LastValue;
		}

		public void CopyCyclicToPlain(TcoCoreTests.I_TcoSequencer source)
		{
			this.CopyCyclicToPlain((TcoCoreTests._TcoSequencer)source);
		}

		public void CopyShadowToPlain(TcoCoreTests._TcoSequencer source)
		{
			_RunOneStep = source._RunOneStep.Shadow;
			_RunAllSteps = source._RunAllSteps.Shadow;
			_FinishStep = source._FinishStep.Shadow;
			_ID = source._ID.Shadow;
			_Enabled = source._Enabled.Shadow;
			_StepDescription = source._StepDescription.Shadow;
			_currentStepId = source._currentStepId.Shadow;
			_currentStepOrder = source._currentStepOrder.Shadow;
			_currentStepEnabled = source._currentStepEnabled.Shadow;
			_currentStepDescription = source._currentStepDescription.Shadow;
			_currentStepStatus = source._currentStepStatus.Shadow;
			_currentStepDuration = source._currentStepDuration.Shadow;
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadow_TcoSequencer source)
		{
			this.CopyShadowToPlain((TcoCoreTests._TcoSequencer)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public Plain_TcoSequencer()
		{
		}
	}
}