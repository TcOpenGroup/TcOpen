using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoTaskTest", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class TcoTaskTest : Vortex.Connector.IVortexObject, ITcoTaskTest, IShadowTcoTaskTest, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		Vortex.Connector.ValueTypes.OnlinerBool __IsBusy;
		public Vortex.Connector.ValueTypes.OnlinerBool _IsBusy
		{
			get
			{
				return __IsBusy;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoTaskTest._IsBusy
		{
			get
			{
				return _IsBusy;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoTaskTest._IsBusy
		{
			get
			{
				return _IsBusy;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool __IsError;
		public Vortex.Connector.ValueTypes.OnlinerBool _IsError
		{
			get
			{
				return __IsError;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoTaskTest._IsError
		{
			get
			{
				return _IsError;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoTaskTest._IsError
		{
			get
			{
				return _IsError;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool __IsDone;
		public Vortex.Connector.ValueTypes.OnlinerBool _IsDone
		{
			get
			{
				return __IsDone;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoTaskTest._IsDone
		{
			get
			{
				return _IsDone;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoTaskTest._IsDone
		{
			get
			{
				return _IsDone;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __CounterSetValue;
		public Vortex.Connector.ValueTypes.OnlinerULInt _CounterSetValue
		{
			get
			{
				return __CounterSetValue;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoTaskTest._CounterSetValue
		{
			get
			{
				return _CounterSetValue;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoTaskTest._CounterSetValue
		{
			get
			{
				return _CounterSetValue;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __CounterValue;
		public Vortex.Connector.ValueTypes.OnlinerULInt _CounterValue
		{
			get
			{
				return __CounterValue;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoTaskTest._CounterValue
		{
			get
			{
				return _CounterValue;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoTaskTest._CounterValue
		{
			get
			{
				return _CounterValue;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __InvokeCounter;
		public Vortex.Connector.ValueTypes.OnlinerULInt _InvokeCounter
		{
			get
			{
				return __InvokeCounter;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoTaskTest._InvokeCounter
		{
			get
			{
				return _InvokeCounter;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoTaskTest._InvokeCounter
		{
			get
			{
				return _InvokeCounter;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __ExecuteCounter;
		public Vortex.Connector.ValueTypes.OnlinerULInt _ExecuteCounter
		{
			get
			{
				return __ExecuteCounter;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoTaskTest._ExecuteCounter
		{
			get
			{
				return _ExecuteCounter;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoTaskTest._ExecuteCounter
		{
			get
			{
				return _ExecuteCounter;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __DoneCounter;
		public Vortex.Connector.ValueTypes.OnlinerULInt _DoneCounter
		{
			get
			{
				return __DoneCounter;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoTaskTest._DoneCounter
		{
			get
			{
				return _DoneCounter;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoTaskTest._DoneCounter
		{
			get
			{
				return _DoneCounter;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __InvokeRisingEdgeCounter;
		public Vortex.Connector.ValueTypes.OnlinerULInt _InvokeRisingEdgeCounter
		{
			get
			{
				return __InvokeRisingEdgeCounter;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoTaskTest._InvokeRisingEdgeCounter
		{
			get
			{
				return _InvokeRisingEdgeCounter;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoTaskTest._InvokeRisingEdgeCounter
		{
			get
			{
				return _InvokeRisingEdgeCounter;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __ExecuteRisingEdgeCounter;
		public Vortex.Connector.ValueTypes.OnlinerULInt _ExecuteRisingEdgeCounter
		{
			get
			{
				return __ExecuteRisingEdgeCounter;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoTaskTest._ExecuteRisingEdgeCounter
		{
			get
			{
				return _ExecuteRisingEdgeCounter;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoTaskTest._ExecuteRisingEdgeCounter
		{
			get
			{
				return _ExecuteRisingEdgeCounter;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __DoneRisingEdgeCounter;
		public Vortex.Connector.ValueTypes.OnlinerULInt _DoneRisingEdgeCounter
		{
			get
			{
				return __DoneRisingEdgeCounter;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoTaskTest._DoneRisingEdgeCounter
		{
			get
			{
				return _DoneRisingEdgeCounter;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoTaskTest._DoneRisingEdgeCounter
		{
			get
			{
				return _DoneRisingEdgeCounter;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __CounterValuePV;
		public Vortex.Connector.ValueTypes.OnlinerULInt _CounterValuePV
		{
			get
			{
				return __CounterValuePV;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoTaskTest._CounterValuePV
		{
			get
			{
				return _CounterValuePV;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoTaskTest._CounterValuePV
		{
			get
			{
				return _CounterValuePV;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __MyIdentity;
		public Vortex.Connector.ValueTypes.OnlinerULInt _MyIdentity
		{
			get
			{
				return __MyIdentity;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoTaskTest._MyIdentity
		{
			get
			{
				return _MyIdentity;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoTaskTest._MyIdentity
		{
			get
			{
				return _MyIdentity;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __MyContextIdentity;
		public Vortex.Connector.ValueTypes.OnlinerULInt _MyContextIdentity
		{
			get
			{
				return __MyContextIdentity;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoTaskTest._MyContextIdentity
		{
			get
			{
				return _MyContextIdentity;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoTaskTest._MyContextIdentity
		{
			get
			{
				return _MyContextIdentity;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __MyContextStartCount;
		public Vortex.Connector.ValueTypes.OnlinerULInt _MyContextStartCount
		{
			get
			{
				return __MyContextStartCount;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoTaskTest._MyContextStartCount
		{
			get
			{
				return _MyContextStartCount;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoTaskTest._MyContextStartCount
		{
			get
			{
				return _MyContextStartCount;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __MyContextEndCount;
		public Vortex.Connector.ValueTypes.OnlinerULInt _MyContextEndCount
		{
			get
			{
				return __MyContextEndCount;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoTaskTest._MyContextEndCount
		{
			get
			{
				return _MyContextEndCount;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoTaskTest._MyContextEndCount
		{
			get
			{
				return _MyContextEndCount;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __MyParentIdentity;
		public Vortex.Connector.ValueTypes.OnlinerULInt _MyParentIdentity
		{
			get
			{
				return __MyParentIdentity;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoTaskTest._MyParentIdentity
		{
			get
			{
				return _MyParentIdentity;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoTaskTest._MyParentIdentity
		{
			get
			{
				return _MyParentIdentity;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool __AutoRestoreByMyParentEnabled;
		public Vortex.Connector.ValueTypes.OnlinerBool _AutoRestoreByMyParentEnabled
		{
			get
			{
				return __AutoRestoreByMyParentEnabled;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoTaskTest._AutoRestoreByMyParentEnabled
		{
			get
			{
				return _AutoRestoreByMyParentEnabled;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoTaskTest._AutoRestoreByMyParentEnabled
		{
			get
			{
				return _AutoRestoreByMyParentEnabled;
			}
		}

		public void LazyOnlineToShadow()
		{
			_IsBusy.Shadow = _IsBusy.LastValue;
			_IsError.Shadow = _IsError.LastValue;
			_IsDone.Shadow = _IsDone.LastValue;
			_CounterSetValue.Shadow = _CounterSetValue.LastValue;
			_CounterValue.Shadow = _CounterValue.LastValue;
			_InvokeCounter.Shadow = _InvokeCounter.LastValue;
			_ExecuteCounter.Shadow = _ExecuteCounter.LastValue;
			_DoneCounter.Shadow = _DoneCounter.LastValue;
			_InvokeRisingEdgeCounter.Shadow = _InvokeRisingEdgeCounter.LastValue;
			_ExecuteRisingEdgeCounter.Shadow = _ExecuteRisingEdgeCounter.LastValue;
			_DoneRisingEdgeCounter.Shadow = _DoneRisingEdgeCounter.LastValue;
			_CounterValuePV.Shadow = _CounterValuePV.LastValue;
			_MyIdentity.Shadow = _MyIdentity.LastValue;
			_MyContextIdentity.Shadow = _MyContextIdentity.LastValue;
			_MyContextStartCount.Shadow = _MyContextStartCount.LastValue;
			_MyContextEndCount.Shadow = _MyContextEndCount.LastValue;
			_MyParentIdentity.Shadow = _MyParentIdentity.LastValue;
			_AutoRestoreByMyParentEnabled.Shadow = _AutoRestoreByMyParentEnabled.LastValue;
		}

		public void LazyShadowToOnline()
		{
			_IsBusy.Cyclic = _IsBusy.Shadow;
			_IsError.Cyclic = _IsError.Shadow;
			_IsDone.Cyclic = _IsDone.Shadow;
			_CounterSetValue.Cyclic = _CounterSetValue.Shadow;
			_CounterValue.Cyclic = _CounterValue.Shadow;
			_InvokeCounter.Cyclic = _InvokeCounter.Shadow;
			_ExecuteCounter.Cyclic = _ExecuteCounter.Shadow;
			_DoneCounter.Cyclic = _DoneCounter.Shadow;
			_InvokeRisingEdgeCounter.Cyclic = _InvokeRisingEdgeCounter.Shadow;
			_ExecuteRisingEdgeCounter.Cyclic = _ExecuteRisingEdgeCounter.Shadow;
			_DoneRisingEdgeCounter.Cyclic = _DoneRisingEdgeCounter.Shadow;
			_CounterValuePV.Cyclic = _CounterValuePV.Shadow;
			_MyIdentity.Cyclic = _MyIdentity.Shadow;
			_MyContextIdentity.Cyclic = _MyContextIdentity.Shadow;
			_MyContextStartCount.Cyclic = _MyContextStartCount.Shadow;
			_MyContextEndCount.Cyclic = _MyContextEndCount.Shadow;
			_MyParentIdentity.Cyclic = _MyParentIdentity.Shadow;
			_AutoRestoreByMyParentEnabled.Cyclic = _AutoRestoreByMyParentEnabled.Shadow;
		}

		public PlainTcoTaskTest CreatePlainerType()
		{
			var cloned = new PlainTcoTaskTest();
			return cloned;
		}

		protected PlainTcoTaskTest CreatePlainerType(PlainTcoTaskTest cloned)
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

		public void FlushPlainToOnline(TcoCoreTests.PlainTcoTaskTest source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainTcoTaskTest source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainTcoTaskTest source)
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

		public TcoTaskTest(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			__IsBusy = @Connector.Online.Adapter.CreateBOOL(this, "", "_IsBusy");
			__IsError = @Connector.Online.Adapter.CreateBOOL(this, "", "_IsError");
			__IsDone = @Connector.Online.Adapter.CreateBOOL(this, "", "_IsDone");
			__CounterSetValue = @Connector.Online.Adapter.CreateULINT(this, "", "_CounterSetValue");
			__CounterValue = @Connector.Online.Adapter.CreateULINT(this, "", "_CounterValue");
			__InvokeCounter = @Connector.Online.Adapter.CreateULINT(this, "", "_InvokeCounter");
			__ExecuteCounter = @Connector.Online.Adapter.CreateULINT(this, "", "_ExecuteCounter");
			__DoneCounter = @Connector.Online.Adapter.CreateULINT(this, "", "_DoneCounter");
			__InvokeRisingEdgeCounter = @Connector.Online.Adapter.CreateULINT(this, "", "_InvokeRisingEdgeCounter");
			__ExecuteRisingEdgeCounter = @Connector.Online.Adapter.CreateULINT(this, "", "_ExecuteRisingEdgeCounter");
			__DoneRisingEdgeCounter = @Connector.Online.Adapter.CreateULINT(this, "", "_DoneRisingEdgeCounter");
			__CounterValuePV = @Connector.Online.Adapter.CreateULINT(this, "", "_CounterValuePV");
			__MyIdentity = @Connector.Online.Adapter.CreateULINT(this, "", "_MyIdentity");
			__MyContextIdentity = @Connector.Online.Adapter.CreateULINT(this, "", "_MyContextIdentity");
			__MyContextStartCount = @Connector.Online.Adapter.CreateULINT(this, "", "_MyContextStartCount");
			__MyContextEndCount = @Connector.Online.Adapter.CreateULINT(this, "", "_MyContextEndCount");
			__MyParentIdentity = @Connector.Online.Adapter.CreateULINT(this, "", "_MyParentIdentity");
			__AutoRestoreByMyParentEnabled = @Connector.Online.Adapter.CreateBOOL(this, "", "_AutoRestoreByMyParentEnabled");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public TcoTaskTest()
		{
			PexPreConstructorParameterless();
			__IsBusy = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__IsError = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__IsDone = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__CounterSetValue = Vortex.Connector.IConnectorFactory.CreateULINT();
			__CounterValue = Vortex.Connector.IConnectorFactory.CreateULINT();
			__InvokeCounter = Vortex.Connector.IConnectorFactory.CreateULINT();
			__ExecuteCounter = Vortex.Connector.IConnectorFactory.CreateULINT();
			__DoneCounter = Vortex.Connector.IConnectorFactory.CreateULINT();
			__InvokeRisingEdgeCounter = Vortex.Connector.IConnectorFactory.CreateULINT();
			__ExecuteRisingEdgeCounter = Vortex.Connector.IConnectorFactory.CreateULINT();
			__DoneRisingEdgeCounter = Vortex.Connector.IConnectorFactory.CreateULINT();
			__CounterValuePV = Vortex.Connector.IConnectorFactory.CreateULINT();
			__MyIdentity = Vortex.Connector.IConnectorFactory.CreateULINT();
			__MyContextIdentity = Vortex.Connector.IConnectorFactory.CreateULINT();
			__MyContextStartCount = Vortex.Connector.IConnectorFactory.CreateULINT();
			__MyContextEndCount = Vortex.Connector.IConnectorFactory.CreateULINT();
			__MyParentIdentity = Vortex.Connector.IConnectorFactory.CreateULINT();
			__AutoRestoreByMyParentEnabled = Vortex.Connector.IConnectorFactory.CreateBOOL();
			AttributeName = "";
			PexConstructorParameterless();
		}

		public void ExecutionBody()
		{
			Connector.InvokeRpc(this.Symbol, "ExecutionBody", new object[]{});
		}

		public System.String GetMessage()
		{
			return (System.String)Connector.InvokeRpc(this.Symbol, "GetMessage", new object[]{});
		}

		public void PostMessage(System.String Message)
		{
			Connector.InvokeRpc(this.Symbol, "PostMessage", new object[]{Message});
		}

		public void ReadOutAutoRestoreProperties()
		{
			Connector.InvokeRpc(this.Symbol, "ReadOutAutoRestoreProperties", new object[]{});
		}

		public void ReadOutIdentities()
		{
			Connector.InvokeRpc(this.Symbol, "ReadOutIdentities", new object[]{});
		}

		public void ReadOutState()
		{
			Connector.InvokeRpc(this.Symbol, "ReadOutState", new object[]{});
		}

		public void SetPreviousStateToIdle()
		{
			Connector.InvokeRpc(this.Symbol, "SetPreviousStateToIdle", new object[]{});
		}

		public void TriggerAbort()
		{
			Connector.InvokeRpc(this.Symbol, "TriggerAbort", new object[]{});
		}

		public void TriggerInvoke()
		{
			Connector.InvokeRpc(this.Symbol, "TriggerInvoke", new object[]{});
		}

		public void TriggerRestore()
		{
			Connector.InvokeRpc(this.Symbol, "TriggerRestore", new object[]{});
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoTaskTest
		{
			public object _IsBusy;
			public object _IsError;
			public object _IsDone;
			public object _CounterSetValue;
			public object _CounterValue;
			public object _InvokeCounter;
			public object _ExecuteCounter;
			public object _DoneCounter;
			public object _InvokeRisingEdgeCounter;
			public object _ExecuteRisingEdgeCounter;
			public object _DoneRisingEdgeCounter;
			public object _PreviousState;
			public object _CounterValuePV;
			public object _MyIdentity;
			public object _MyContextIdentity;
			public object _MyContextStartCount;
			public object _MyContextEndCount;
			public object _MyParentIdentity;
			public object _AutoRestoreByMyParentEnabled;
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoTaskTest()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoTaskTest : Vortex.Connector.IVortexOnlineObject
	{
		Vortex.Connector.ValueTypes.Online.IOnlineBool _IsBusy
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool _IsError
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool _IsDone
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _CounterSetValue
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _CounterValue
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _InvokeCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _ExecuteCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _DoneCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _InvokeRisingEdgeCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _ExecuteRisingEdgeCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _DoneRisingEdgeCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _CounterValuePV
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _MyIdentity
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _MyContextIdentity
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _MyContextStartCount
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _MyContextEndCount
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _MyParentIdentity
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool _AutoRestoreByMyParentEnabled
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoTaskTest CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainTcoTaskTest source);
		void FlushOnlineToPlain(TcoCoreTests.PlainTcoTaskTest source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoTaskTest : Vortex.Connector.IVortexShadowObject
	{
		Vortex.Connector.ValueTypes.Shadows.IShadowBool _IsBusy
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool _IsError
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool _IsDone
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _CounterSetValue
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _CounterValue
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _InvokeCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _ExecuteCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _DoneCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _InvokeRisingEdgeCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _ExecuteRisingEdgeCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _DoneRisingEdgeCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _CounterValuePV
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _MyIdentity
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _MyContextIdentity
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _MyContextStartCount
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _MyContextEndCount
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _MyParentIdentity
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool _AutoRestoreByMyParentEnabled
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoTaskTest CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainTcoTaskTest source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoTaskTest : Vortex.Connector.IPlain
	{
		System.Boolean __IsBusy;
		public System.Boolean _IsBusy
		{
			get
			{
				return __IsBusy;
			}

			set
			{
				if (__IsBusy != value)
				{
					__IsBusy = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_IsBusy)));
				}
			}
		}

		System.Boolean __IsError;
		public System.Boolean _IsError
		{
			get
			{
				return __IsError;
			}

			set
			{
				if (__IsError != value)
				{
					__IsError = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_IsError)));
				}
			}
		}

		System.Boolean __IsDone;
		public System.Boolean _IsDone
		{
			get
			{
				return __IsDone;
			}

			set
			{
				if (__IsDone != value)
				{
					__IsDone = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_IsDone)));
				}
			}
		}

		System.UInt64 __CounterSetValue;
		public System.UInt64 _CounterSetValue
		{
			get
			{
				return __CounterSetValue;
			}

			set
			{
				if (__CounterSetValue != value)
				{
					__CounterSetValue = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_CounterSetValue)));
				}
			}
		}

		System.UInt64 __CounterValue;
		public System.UInt64 _CounterValue
		{
			get
			{
				return __CounterValue;
			}

			set
			{
				if (__CounterValue != value)
				{
					__CounterValue = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_CounterValue)));
				}
			}
		}

		System.UInt64 __InvokeCounter;
		public System.UInt64 _InvokeCounter
		{
			get
			{
				return __InvokeCounter;
			}

			set
			{
				if (__InvokeCounter != value)
				{
					__InvokeCounter = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_InvokeCounter)));
				}
			}
		}

		System.UInt64 __ExecuteCounter;
		public System.UInt64 _ExecuteCounter
		{
			get
			{
				return __ExecuteCounter;
			}

			set
			{
				if (__ExecuteCounter != value)
				{
					__ExecuteCounter = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_ExecuteCounter)));
				}
			}
		}

		System.UInt64 __DoneCounter;
		public System.UInt64 _DoneCounter
		{
			get
			{
				return __DoneCounter;
			}

			set
			{
				if (__DoneCounter != value)
				{
					__DoneCounter = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_DoneCounter)));
				}
			}
		}

		System.UInt64 __InvokeRisingEdgeCounter;
		public System.UInt64 _InvokeRisingEdgeCounter
		{
			get
			{
				return __InvokeRisingEdgeCounter;
			}

			set
			{
				if (__InvokeRisingEdgeCounter != value)
				{
					__InvokeRisingEdgeCounter = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_InvokeRisingEdgeCounter)));
				}
			}
		}

		System.UInt64 __ExecuteRisingEdgeCounter;
		public System.UInt64 _ExecuteRisingEdgeCounter
		{
			get
			{
				return __ExecuteRisingEdgeCounter;
			}

			set
			{
				if (__ExecuteRisingEdgeCounter != value)
				{
					__ExecuteRisingEdgeCounter = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_ExecuteRisingEdgeCounter)));
				}
			}
		}

		System.UInt64 __DoneRisingEdgeCounter;
		public System.UInt64 _DoneRisingEdgeCounter
		{
			get
			{
				return __DoneRisingEdgeCounter;
			}

			set
			{
				if (__DoneRisingEdgeCounter != value)
				{
					__DoneRisingEdgeCounter = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_DoneRisingEdgeCounter)));
				}
			}
		}

		System.UInt64 __CounterValuePV;
		public System.UInt64 _CounterValuePV
		{
			get
			{
				return __CounterValuePV;
			}

			set
			{
				if (__CounterValuePV != value)
				{
					__CounterValuePV = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_CounterValuePV)));
				}
			}
		}

		System.UInt64 __MyIdentity;
		public System.UInt64 _MyIdentity
		{
			get
			{
				return __MyIdentity;
			}

			set
			{
				if (__MyIdentity != value)
				{
					__MyIdentity = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_MyIdentity)));
				}
			}
		}

		System.UInt64 __MyContextIdentity;
		public System.UInt64 _MyContextIdentity
		{
			get
			{
				return __MyContextIdentity;
			}

			set
			{
				if (__MyContextIdentity != value)
				{
					__MyContextIdentity = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_MyContextIdentity)));
				}
			}
		}

		System.UInt64 __MyContextStartCount;
		public System.UInt64 _MyContextStartCount
		{
			get
			{
				return __MyContextStartCount;
			}

			set
			{
				if (__MyContextStartCount != value)
				{
					__MyContextStartCount = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_MyContextStartCount)));
				}
			}
		}

		System.UInt64 __MyContextEndCount;
		public System.UInt64 _MyContextEndCount
		{
			get
			{
				return __MyContextEndCount;
			}

			set
			{
				if (__MyContextEndCount != value)
				{
					__MyContextEndCount = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_MyContextEndCount)));
				}
			}
		}

		System.UInt64 __MyParentIdentity;
		public System.UInt64 _MyParentIdentity
		{
			get
			{
				return __MyParentIdentity;
			}

			set
			{
				if (__MyParentIdentity != value)
				{
					__MyParentIdentity = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_MyParentIdentity)));
				}
			}
		}

		System.Boolean __AutoRestoreByMyParentEnabled;
		public System.Boolean _AutoRestoreByMyParentEnabled
		{
			get
			{
				return __AutoRestoreByMyParentEnabled;
			}

			set
			{
				if (__AutoRestoreByMyParentEnabled != value)
				{
					__AutoRestoreByMyParentEnabled = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_AutoRestoreByMyParentEnabled)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCoreTests.TcoTaskTest target)
		{
			target._IsBusy.Cyclic = _IsBusy;
			target._IsError.Cyclic = _IsError;
			target._IsDone.Cyclic = _IsDone;
			target._CounterSetValue.Cyclic = _CounterSetValue;
			target._CounterValue.Cyclic = _CounterValue;
			target._InvokeCounter.Cyclic = _InvokeCounter;
			target._ExecuteCounter.Cyclic = _ExecuteCounter;
			target._DoneCounter.Cyclic = _DoneCounter;
			target._InvokeRisingEdgeCounter.Cyclic = _InvokeRisingEdgeCounter;
			target._ExecuteRisingEdgeCounter.Cyclic = _ExecuteRisingEdgeCounter;
			target._DoneRisingEdgeCounter.Cyclic = _DoneRisingEdgeCounter;
			target._CounterValuePV.Cyclic = _CounterValuePV;
			target._MyIdentity.Cyclic = _MyIdentity;
			target._MyContextIdentity.Cyclic = _MyContextIdentity;
			target._MyContextStartCount.Cyclic = _MyContextStartCount;
			target._MyContextEndCount.Cyclic = _MyContextEndCount;
			target._MyParentIdentity.Cyclic = _MyParentIdentity;
			target._AutoRestoreByMyParentEnabled.Cyclic = _AutoRestoreByMyParentEnabled;
		}

		public void CopyPlainToCyclic(TcoCoreTests.ITcoTaskTest target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.TcoTaskTest)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.TcoTaskTest target)
		{
			target._IsBusy.Shadow = _IsBusy;
			target._IsError.Shadow = _IsError;
			target._IsDone.Shadow = _IsDone;
			target._CounterSetValue.Shadow = _CounterSetValue;
			target._CounterValue.Shadow = _CounterValue;
			target._InvokeCounter.Shadow = _InvokeCounter;
			target._ExecuteCounter.Shadow = _ExecuteCounter;
			target._DoneCounter.Shadow = _DoneCounter;
			target._InvokeRisingEdgeCounter.Shadow = _InvokeRisingEdgeCounter;
			target._ExecuteRisingEdgeCounter.Shadow = _ExecuteRisingEdgeCounter;
			target._DoneRisingEdgeCounter.Shadow = _DoneRisingEdgeCounter;
			target._CounterValuePV.Shadow = _CounterValuePV;
			target._MyIdentity.Shadow = _MyIdentity;
			target._MyContextIdentity.Shadow = _MyContextIdentity;
			target._MyContextStartCount.Shadow = _MyContextStartCount;
			target._MyContextEndCount.Shadow = _MyContextEndCount;
			target._MyParentIdentity.Shadow = _MyParentIdentity;
			target._AutoRestoreByMyParentEnabled.Shadow = _AutoRestoreByMyParentEnabled;
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowTcoTaskTest target)
		{
			this.CopyPlainToShadow((TcoCoreTests.TcoTaskTest)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.TcoTaskTest source)
		{
			_IsBusy = source._IsBusy.LastValue;
			_IsError = source._IsError.LastValue;
			_IsDone = source._IsDone.LastValue;
			_CounterSetValue = source._CounterSetValue.LastValue;
			_CounterValue = source._CounterValue.LastValue;
			_InvokeCounter = source._InvokeCounter.LastValue;
			_ExecuteCounter = source._ExecuteCounter.LastValue;
			_DoneCounter = source._DoneCounter.LastValue;
			_InvokeRisingEdgeCounter = source._InvokeRisingEdgeCounter.LastValue;
			_ExecuteRisingEdgeCounter = source._ExecuteRisingEdgeCounter.LastValue;
			_DoneRisingEdgeCounter = source._DoneRisingEdgeCounter.LastValue;
			_CounterValuePV = source._CounterValuePV.LastValue;
			_MyIdentity = source._MyIdentity.LastValue;
			_MyContextIdentity = source._MyContextIdentity.LastValue;
			_MyContextStartCount = source._MyContextStartCount.LastValue;
			_MyContextEndCount = source._MyContextEndCount.LastValue;
			_MyParentIdentity = source._MyParentIdentity.LastValue;
			_AutoRestoreByMyParentEnabled = source._AutoRestoreByMyParentEnabled.LastValue;
		}

		public void CopyCyclicToPlain(TcoCoreTests.ITcoTaskTest source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.TcoTaskTest)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.TcoTaskTest source)
		{
			_IsBusy = source._IsBusy.Shadow;
			_IsError = source._IsError.Shadow;
			_IsDone = source._IsDone.Shadow;
			_CounterSetValue = source._CounterSetValue.Shadow;
			_CounterValue = source._CounterValue.Shadow;
			_InvokeCounter = source._InvokeCounter.Shadow;
			_ExecuteCounter = source._ExecuteCounter.Shadow;
			_DoneCounter = source._DoneCounter.Shadow;
			_InvokeRisingEdgeCounter = source._InvokeRisingEdgeCounter.Shadow;
			_ExecuteRisingEdgeCounter = source._ExecuteRisingEdgeCounter.Shadow;
			_DoneRisingEdgeCounter = source._DoneRisingEdgeCounter.Shadow;
			_CounterValuePV = source._CounterValuePV.Shadow;
			_MyIdentity = source._MyIdentity.Shadow;
			_MyContextIdentity = source._MyContextIdentity.Shadow;
			_MyContextStartCount = source._MyContextStartCount.Shadow;
			_MyContextEndCount = source._MyContextEndCount.Shadow;
			_MyParentIdentity = source._MyParentIdentity.Shadow;
			_AutoRestoreByMyParentEnabled = source._AutoRestoreByMyParentEnabled.Shadow;
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowTcoTaskTest source)
		{
			this.CopyShadowToPlain((TcoCoreTests.TcoTaskTest)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoTaskTest()
		{
		}
	}
}