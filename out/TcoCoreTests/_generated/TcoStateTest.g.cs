using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoStateTest", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class TcoStateTest : Vortex.Connector.IVortexObject, ITcoStateTest, IShadowTcoStateTest, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		Vortex.Connector.ValueTypes.OnlinerULInt __MyIdentity;
		public Vortex.Connector.ValueTypes.OnlinerULInt _MyIdentity
		{
			get
			{
				return __MyIdentity;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoStateTest._MyIdentity
		{
			get
			{
				return _MyIdentity;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoStateTest._MyIdentity
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

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoStateTest._MyContextIdentity
		{
			get
			{
				return _MyContextIdentity;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoStateTest._MyContextIdentity
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

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoStateTest._MyContextStartCount
		{
			get
			{
				return _MyContextStartCount;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoStateTest._MyContextStartCount
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

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoStateTest._MyContextEndCount
		{
			get
			{
				return _MyContextEndCount;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoStateTest._MyContextEndCount
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

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoStateTest._MyParentIdentity
		{
			get
			{
				return _MyParentIdentity;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoStateTest._MyParentIdentity
		{
			get
			{
				return _MyParentIdentity;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerInt __MyState;
		public Vortex.Connector.ValueTypes.OnlinerInt _MyState
		{
			get
			{
				return __MyState;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineInt ITcoStateTest._MyState
		{
			get
			{
				return _MyState;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadowTcoStateTest._MyState
		{
			get
			{
				return _MyState;
			}
		}

		TcoTaskTest __TcoTaskTest_A;
		public TcoTaskTest _TcoTaskTest_A
		{
			get
			{
				return __TcoTaskTest_A;
			}
		}

		ITcoTaskTest ITcoStateTest._TcoTaskTest_A
		{
			get
			{
				return _TcoTaskTest_A;
			}
		}

		IShadowTcoTaskTest IShadowTcoStateTest._TcoTaskTest_A
		{
			get
			{
				return _TcoTaskTest_A;
			}
		}

		TcoTaskTest __TcoTaskTest_B;
		public TcoTaskTest _TcoTaskTest_B
		{
			get
			{
				return __TcoTaskTest_B;
			}
		}

		ITcoTaskTest ITcoStateTest._TcoTaskTest_B
		{
			get
			{
				return _TcoTaskTest_B;
			}
		}

		IShadowTcoTaskTest IShadowTcoStateTest._TcoTaskTest_B
		{
			get
			{
				return _TcoTaskTest_B;
			}
		}

		TcoStateAutoRestoreTest __TcoStateTest_A;
		public TcoStateAutoRestoreTest _TcoStateTest_A
		{
			get
			{
				return __TcoStateTest_A;
			}
		}

		ITcoStateAutoRestoreTest ITcoStateTest._TcoStateTest_A
		{
			get
			{
				return _TcoStateTest_A;
			}
		}

		IShadowTcoStateAutoRestoreTest IShadowTcoStateTest._TcoStateTest_A
		{
			get
			{
				return _TcoStateTest_A;
			}
		}

		TcoStateAutoRestoreTest __TcoStateTest_B;
		public TcoStateAutoRestoreTest _TcoStateTest_B
		{
			get
			{
				return __TcoStateTest_B;
			}
		}

		ITcoStateAutoRestoreTest ITcoStateTest._TcoStateTest_B
		{
			get
			{
				return _TcoStateTest_B;
			}
		}

		IShadowTcoStateAutoRestoreTest IShadowTcoStateTest._TcoStateTest_B
		{
			get
			{
				return _TcoStateTest_B;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerInt __OnStateChangeCounter;
		public Vortex.Connector.ValueTypes.OnlinerInt _OnStateChangeCounter
		{
			get
			{
				return __OnStateChangeCounter;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineInt ITcoStateTest._OnStateChangeCounter
		{
			get
			{
				return _OnStateChangeCounter;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadowTcoStateTest._OnStateChangeCounter
		{
			get
			{
				return _OnStateChangeCounter;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool __AutoRestoreToMyChildsEnabled;
		public Vortex.Connector.ValueTypes.OnlinerBool _AutoRestoreToMyChildsEnabled
		{
			get
			{
				return __AutoRestoreToMyChildsEnabled;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoStateTest._AutoRestoreToMyChildsEnabled
		{
			get
			{
				return _AutoRestoreToMyChildsEnabled;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoStateTest._AutoRestoreToMyChildsEnabled
		{
			get
			{
				return _AutoRestoreToMyChildsEnabled;
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

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoStateTest._AutoRestoreByMyParentEnabled
		{
			get
			{
				return _AutoRestoreByMyParentEnabled;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoStateTest._AutoRestoreByMyParentEnabled
		{
			get
			{
				return _AutoRestoreByMyParentEnabled;
			}
		}

		public void LazyOnlineToShadow()
		{
			_MyIdentity.Shadow = _MyIdentity.LastValue;
			_MyContextIdentity.Shadow = _MyContextIdentity.LastValue;
			_MyContextStartCount.Shadow = _MyContextStartCount.LastValue;
			_MyContextEndCount.Shadow = _MyContextEndCount.LastValue;
			_MyParentIdentity.Shadow = _MyParentIdentity.LastValue;
			_MyState.Shadow = _MyState.LastValue;
			_TcoTaskTest_A.LazyOnlineToShadow();
			_TcoTaskTest_B.LazyOnlineToShadow();
			_TcoStateTest_A.LazyOnlineToShadow();
			_TcoStateTest_B.LazyOnlineToShadow();
			_OnStateChangeCounter.Shadow = _OnStateChangeCounter.LastValue;
			_AutoRestoreToMyChildsEnabled.Shadow = _AutoRestoreToMyChildsEnabled.LastValue;
			_AutoRestoreByMyParentEnabled.Shadow = _AutoRestoreByMyParentEnabled.LastValue;
		}

		public void LazyShadowToOnline()
		{
			_MyIdentity.Cyclic = _MyIdentity.Shadow;
			_MyContextIdentity.Cyclic = _MyContextIdentity.Shadow;
			_MyContextStartCount.Cyclic = _MyContextStartCount.Shadow;
			_MyContextEndCount.Cyclic = _MyContextEndCount.Shadow;
			_MyParentIdentity.Cyclic = _MyParentIdentity.Shadow;
			_MyState.Cyclic = _MyState.Shadow;
			_TcoTaskTest_A.LazyShadowToOnline();
			_TcoTaskTest_B.LazyShadowToOnline();
			_TcoStateTest_A.LazyShadowToOnline();
			_TcoStateTest_B.LazyShadowToOnline();
			_OnStateChangeCounter.Cyclic = _OnStateChangeCounter.Shadow;
			_AutoRestoreToMyChildsEnabled.Cyclic = _AutoRestoreToMyChildsEnabled.Shadow;
			_AutoRestoreByMyParentEnabled.Cyclic = _AutoRestoreByMyParentEnabled.Shadow;
		}

		public PlainTcoStateTest CreatePlainerType()
		{
			var cloned = new PlainTcoStateTest();
			cloned._TcoTaskTest_A = _TcoTaskTest_A.CreatePlainerType();
			cloned._TcoTaskTest_B = _TcoTaskTest_B.CreatePlainerType();
			cloned._TcoStateTest_A = _TcoStateTest_A.CreatePlainerType();
			cloned._TcoStateTest_B = _TcoStateTest_B.CreatePlainerType();
			return cloned;
		}

		protected PlainTcoStateTest CreatePlainerType(PlainTcoStateTest cloned)
		{
			cloned._TcoTaskTest_A = _TcoTaskTest_A.CreatePlainerType();
			cloned._TcoTaskTest_B = _TcoTaskTest_B.CreatePlainerType();
			cloned._TcoStateTest_A = _TcoStateTest_A.CreatePlainerType();
			cloned._TcoStateTest_B = _TcoStateTest_B.CreatePlainerType();
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

		public void FlushPlainToOnline(TcoCoreTests.PlainTcoStateTest source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainTcoStateTest source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainTcoStateTest source)
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

		public TcoStateTest(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			__MyIdentity = @Connector.Online.Adapter.CreateULINT(this, "", "_MyIdentity");
			__MyContextIdentity = @Connector.Online.Adapter.CreateULINT(this, "", "_MyContextIdentity");
			__MyContextStartCount = @Connector.Online.Adapter.CreateULINT(this, "", "_MyContextStartCount");
			__MyContextEndCount = @Connector.Online.Adapter.CreateULINT(this, "", "_MyContextEndCount");
			__MyParentIdentity = @Connector.Online.Adapter.CreateULINT(this, "", "_MyParentIdentity");
			__MyState = @Connector.Online.Adapter.CreateINT(this, "", "_MyState");
			__TcoTaskTest_A = new TcoTaskTest(this, "", "_TcoTaskTest_A");
			__TcoTaskTest_B = new TcoTaskTest(this, "", "_TcoTaskTest_B");
			__TcoStateTest_A = new TcoStateAutoRestoreTest(this, "", "_TcoStateTest_A");
			__TcoStateTest_B = new TcoStateAutoRestoreTest(this, "", "_TcoStateTest_B");
			__OnStateChangeCounter = @Connector.Online.Adapter.CreateINT(this, "", "_OnStateChangeCounter");
			__AutoRestoreToMyChildsEnabled = @Connector.Online.Adapter.CreateBOOL(this, "", "_AutoRestoreToMyChildsEnabled");
			__AutoRestoreByMyParentEnabled = @Connector.Online.Adapter.CreateBOOL(this, "", "_AutoRestoreByMyParentEnabled");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public TcoStateTest()
		{
			PexPreConstructorParameterless();
			__MyIdentity = Vortex.Connector.IConnectorFactory.CreateULINT();
			__MyContextIdentity = Vortex.Connector.IConnectorFactory.CreateULINT();
			__MyContextStartCount = Vortex.Connector.IConnectorFactory.CreateULINT();
			__MyContextEndCount = Vortex.Connector.IConnectorFactory.CreateULINT();
			__MyParentIdentity = Vortex.Connector.IConnectorFactory.CreateULINT();
			__MyState = Vortex.Connector.IConnectorFactory.CreateINT();
			__TcoTaskTest_A = new TcoTaskTest();
			__TcoTaskTest_B = new TcoTaskTest();
			__TcoStateTest_A = new TcoStateAutoRestoreTest();
			__TcoStateTest_B = new TcoStateAutoRestoreTest();
			__OnStateChangeCounter = Vortex.Connector.IConnectorFactory.CreateINT();
			__AutoRestoreToMyChildsEnabled = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__AutoRestoreByMyParentEnabled = Vortex.Connector.IConnectorFactory.CreateBOOL();
			AttributeName = "";
			PexConstructorParameterless();
		}

		public void CallTaskInstancies()
		{
			Connector.InvokeRpc(this.Symbol, "CallTaskInstancies", new object[]{});
		}

		public void CleanUp()
		{
			Connector.InvokeRpc(this.Symbol, "CleanUp", new object[]{});
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

		public void TriggerChangeState(System.Int16 NewState)
		{
			Connector.InvokeRpc(this.Symbol, "TriggerChangeState", new object[]{NewState});
		}

		public void TriggerChangeStateWithObjectRestore(System.Int16 NewState)
		{
			Connector.InvokeRpc(this.Symbol, "TriggerChangeStateWithObjectRestore", new object[]{NewState});
		}

		public void TriggerRestore()
		{
			Connector.InvokeRpc(this.Symbol, "TriggerRestore", new object[]{});
		}

		public void TriggerTaskInvoke()
		{
			Connector.InvokeRpc(this.Symbol, "TriggerTaskInvoke", new object[]{});
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoStateTest
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoStateTest()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoStateTest : Vortex.Connector.IVortexOnlineObject
	{
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

		Vortex.Connector.ValueTypes.Online.IOnlineInt _MyState
		{
			get;
		}

		ITcoTaskTest _TcoTaskTest_A
		{
			get;
		}

		ITcoTaskTest _TcoTaskTest_B
		{
			get;
		}

		ITcoStateAutoRestoreTest _TcoStateTest_A
		{
			get;
		}

		ITcoStateAutoRestoreTest _TcoStateTest_B
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineInt _OnStateChangeCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool _AutoRestoreToMyChildsEnabled
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

		TcoCoreTests.PlainTcoStateTest CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainTcoStateTest source);
		void FlushOnlineToPlain(TcoCoreTests.PlainTcoStateTest source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoStateTest : Vortex.Connector.IVortexShadowObject
	{
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

		Vortex.Connector.ValueTypes.Shadows.IShadowInt _MyState
		{
			get;
		}

		IShadowTcoTaskTest _TcoTaskTest_A
		{
			get;
		}

		IShadowTcoTaskTest _TcoTaskTest_B
		{
			get;
		}

		IShadowTcoStateAutoRestoreTest _TcoStateTest_A
		{
			get;
		}

		IShadowTcoStateAutoRestoreTest _TcoStateTest_B
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowInt _OnStateChangeCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool _AutoRestoreToMyChildsEnabled
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

		TcoCoreTests.PlainTcoStateTest CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainTcoStateTest source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoStateTest : Vortex.Connector.IPlain
	{
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

		System.Int16 __MyState;
		public System.Int16 _MyState
		{
			get
			{
				return __MyState;
			}

			set
			{
				if (__MyState != value)
				{
					__MyState = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_MyState)));
				}
			}
		}

		PlainTcoTaskTest __TcoTaskTest_A;
		public PlainTcoTaskTest _TcoTaskTest_A
		{
			get
			{
				return __TcoTaskTest_A;
			}

			set
			{
				if (__TcoTaskTest_A != value)
				{
					__TcoTaskTest_A = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_TcoTaskTest_A)));
				}
			}
		}

		PlainTcoTaskTest __TcoTaskTest_B;
		public PlainTcoTaskTest _TcoTaskTest_B
		{
			get
			{
				return __TcoTaskTest_B;
			}

			set
			{
				if (__TcoTaskTest_B != value)
				{
					__TcoTaskTest_B = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_TcoTaskTest_B)));
				}
			}
		}

		PlainTcoStateAutoRestoreTest __TcoStateTest_A;
		public PlainTcoStateAutoRestoreTest _TcoStateTest_A
		{
			get
			{
				return __TcoStateTest_A;
			}

			set
			{
				if (__TcoStateTest_A != value)
				{
					__TcoStateTest_A = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_TcoStateTest_A)));
				}
			}
		}

		PlainTcoStateAutoRestoreTest __TcoStateTest_B;
		public PlainTcoStateAutoRestoreTest _TcoStateTest_B
		{
			get
			{
				return __TcoStateTest_B;
			}

			set
			{
				if (__TcoStateTest_B != value)
				{
					__TcoStateTest_B = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_TcoStateTest_B)));
				}
			}
		}

		System.Int16 __OnStateChangeCounter;
		public System.Int16 _OnStateChangeCounter
		{
			get
			{
				return __OnStateChangeCounter;
			}

			set
			{
				if (__OnStateChangeCounter != value)
				{
					__OnStateChangeCounter = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_OnStateChangeCounter)));
				}
			}
		}

		System.Boolean __AutoRestoreToMyChildsEnabled;
		public System.Boolean _AutoRestoreToMyChildsEnabled
		{
			get
			{
				return __AutoRestoreToMyChildsEnabled;
			}

			set
			{
				if (__AutoRestoreToMyChildsEnabled != value)
				{
					__AutoRestoreToMyChildsEnabled = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_AutoRestoreToMyChildsEnabled)));
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

		public void CopyPlainToCyclic(TcoCoreTests.TcoStateTest target)
		{
			target._MyIdentity.Cyclic = _MyIdentity;
			target._MyContextIdentity.Cyclic = _MyContextIdentity;
			target._MyContextStartCount.Cyclic = _MyContextStartCount;
			target._MyContextEndCount.Cyclic = _MyContextEndCount;
			target._MyParentIdentity.Cyclic = _MyParentIdentity;
			target._MyState.Cyclic = _MyState;
			_TcoTaskTest_A.CopyPlainToCyclic(target._TcoTaskTest_A);
			_TcoTaskTest_B.CopyPlainToCyclic(target._TcoTaskTest_B);
			_TcoStateTest_A.CopyPlainToCyclic(target._TcoStateTest_A);
			_TcoStateTest_B.CopyPlainToCyclic(target._TcoStateTest_B);
			target._OnStateChangeCounter.Cyclic = _OnStateChangeCounter;
			target._AutoRestoreToMyChildsEnabled.Cyclic = _AutoRestoreToMyChildsEnabled;
			target._AutoRestoreByMyParentEnabled.Cyclic = _AutoRestoreByMyParentEnabled;
		}

		public void CopyPlainToCyclic(TcoCoreTests.ITcoStateTest target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.TcoStateTest)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.TcoStateTest target)
		{
			target._MyIdentity.Shadow = _MyIdentity;
			target._MyContextIdentity.Shadow = _MyContextIdentity;
			target._MyContextStartCount.Shadow = _MyContextStartCount;
			target._MyContextEndCount.Shadow = _MyContextEndCount;
			target._MyParentIdentity.Shadow = _MyParentIdentity;
			target._MyState.Shadow = _MyState;
			_TcoTaskTest_A.CopyPlainToShadow(target._TcoTaskTest_A);
			_TcoTaskTest_B.CopyPlainToShadow(target._TcoTaskTest_B);
			_TcoStateTest_A.CopyPlainToShadow(target._TcoStateTest_A);
			_TcoStateTest_B.CopyPlainToShadow(target._TcoStateTest_B);
			target._OnStateChangeCounter.Shadow = _OnStateChangeCounter;
			target._AutoRestoreToMyChildsEnabled.Shadow = _AutoRestoreToMyChildsEnabled;
			target._AutoRestoreByMyParentEnabled.Shadow = _AutoRestoreByMyParentEnabled;
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowTcoStateTest target)
		{
			this.CopyPlainToShadow((TcoCoreTests.TcoStateTest)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.TcoStateTest source)
		{
			_MyIdentity = source._MyIdentity.LastValue;
			_MyContextIdentity = source._MyContextIdentity.LastValue;
			_MyContextStartCount = source._MyContextStartCount.LastValue;
			_MyContextEndCount = source._MyContextEndCount.LastValue;
			_MyParentIdentity = source._MyParentIdentity.LastValue;
			_MyState = source._MyState.LastValue;
			_TcoTaskTest_A.CopyCyclicToPlain(source._TcoTaskTest_A);
			_TcoTaskTest_B.CopyCyclicToPlain(source._TcoTaskTest_B);
			_TcoStateTest_A.CopyCyclicToPlain(source._TcoStateTest_A);
			_TcoStateTest_B.CopyCyclicToPlain(source._TcoStateTest_B);
			_OnStateChangeCounter = source._OnStateChangeCounter.LastValue;
			_AutoRestoreToMyChildsEnabled = source._AutoRestoreToMyChildsEnabled.LastValue;
			_AutoRestoreByMyParentEnabled = source._AutoRestoreByMyParentEnabled.LastValue;
		}

		public void CopyCyclicToPlain(TcoCoreTests.ITcoStateTest source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.TcoStateTest)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.TcoStateTest source)
		{
			_MyIdentity = source._MyIdentity.Shadow;
			_MyContextIdentity = source._MyContextIdentity.Shadow;
			_MyContextStartCount = source._MyContextStartCount.Shadow;
			_MyContextEndCount = source._MyContextEndCount.Shadow;
			_MyParentIdentity = source._MyParentIdentity.Shadow;
			_MyState = source._MyState.Shadow;
			_TcoTaskTest_A.CopyShadowToPlain(source._TcoTaskTest_A);
			_TcoTaskTest_B.CopyShadowToPlain(source._TcoTaskTest_B);
			_TcoStateTest_A.CopyShadowToPlain(source._TcoStateTest_A);
			_TcoStateTest_B.CopyShadowToPlain(source._TcoStateTest_B);
			_OnStateChangeCounter = source._OnStateChangeCounter.Shadow;
			_AutoRestoreToMyChildsEnabled = source._AutoRestoreToMyChildsEnabled.Shadow;
			_AutoRestoreByMyParentEnabled = source._AutoRestoreByMyParentEnabled.Shadow;
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowTcoStateTest source)
		{
			this.CopyShadowToPlain((TcoCoreTests.TcoStateTest)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoStateTest()
		{
			__TcoTaskTest_A = new PlainTcoTaskTest();
			__TcoTaskTest_B = new PlainTcoTaskTest();
			__TcoStateTest_A = new PlainTcoStateAutoRestoreTest();
			__TcoStateTest_B = new PlainTcoStateAutoRestoreTest();
		}
	}
}