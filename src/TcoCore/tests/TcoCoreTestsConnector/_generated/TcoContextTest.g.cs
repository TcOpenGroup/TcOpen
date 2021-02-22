using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoContextTest", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class TcoContextTest : Vortex.Connector.IVortexObject, ITcoContextTest, IShadowTcoContextTest, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		Vortex.Connector.ValueTypes.OnlinerBool __CallMyPlcInstance;
		public Vortex.Connector.ValueTypes.OnlinerBool _CallMyPlcInstance
		{
			get
			{
				return __CallMyPlcInstance;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoContextTest._CallMyPlcInstance
		{
			get
			{
				return _CallMyPlcInstance;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoContextTest._CallMyPlcInstance
		{
			get
			{
				return _CallMyPlcInstance;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __startCycles;
		public Vortex.Connector.ValueTypes.OnlinerULInt _startCycles
		{
			get
			{
				return __startCycles;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoContextTest._startCycles
		{
			get
			{
				return _startCycles;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoContextTest._startCycles
		{
			get
			{
				return _startCycles;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __endCycles;
		public Vortex.Connector.ValueTypes.OnlinerULInt _endCycles
		{
			get
			{
				return __endCycles;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoContextTest._endCycles
		{
			get
			{
				return _endCycles;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoContextTest._endCycles
		{
			get
			{
				return _endCycles;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __mainCycles;
		public Vortex.Connector.ValueTypes.OnlinerULInt _mainCycles
		{
			get
			{
				return __mainCycles;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoContextTest._mainCycles
		{
			get
			{
				return _mainCycles;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoContextTest._mainCycles
		{
			get
			{
				return _mainCycles;
			}
		}

		TcoObjectTest __TcoObjectTest_A;
		public TcoObjectTest _TcoObjectTest_A
		{
			get
			{
				return __TcoObjectTest_A;
			}
		}

		ITcoObjectTest ITcoContextTest._TcoObjectTest_A
		{
			get
			{
				return _TcoObjectTest_A;
			}
		}

		IShadowTcoObjectTest IShadowTcoContextTest._TcoObjectTest_A
		{
			get
			{
				return _TcoObjectTest_A;
			}
		}

		TcoObjectTest __TcoObjectTest_B;
		public TcoObjectTest _TcoObjectTest_B
		{
			get
			{
				return __TcoObjectTest_B;
			}
		}

		ITcoObjectTest ITcoContextTest._TcoObjectTest_B
		{
			get
			{
				return _TcoObjectTest_B;
			}
		}

		IShadowTcoObjectTest IShadowTcoContextTest._TcoObjectTest_B
		{
			get
			{
				return _TcoObjectTest_B;
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

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoContextTest._MyIdentity
		{
			get
			{
				return _MyIdentity;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoContextTest._MyIdentity
		{
			get
			{
				return _MyIdentity;
			}
		}

		public void LazyOnlineToShadow()
		{
			_CallMyPlcInstance.Shadow = _CallMyPlcInstance.LastValue;
			_startCycles.Shadow = _startCycles.LastValue;
			_endCycles.Shadow = _endCycles.LastValue;
			_mainCycles.Shadow = _mainCycles.LastValue;
			_TcoObjectTest_A.LazyOnlineToShadow();
			_TcoObjectTest_B.LazyOnlineToShadow();
			_MyIdentity.Shadow = _MyIdentity.LastValue;
		}

		public void LazyShadowToOnline()
		{
			_CallMyPlcInstance.Cyclic = _CallMyPlcInstance.Shadow;
			_startCycles.Cyclic = _startCycles.Shadow;
			_endCycles.Cyclic = _endCycles.Shadow;
			_mainCycles.Cyclic = _mainCycles.Shadow;
			_TcoObjectTest_A.LazyShadowToOnline();
			_TcoObjectTest_B.LazyShadowToOnline();
			_MyIdentity.Cyclic = _MyIdentity.Shadow;
		}

		public PlainTcoContextTest CreatePlainerType()
		{
			var cloned = new PlainTcoContextTest();
			cloned._TcoObjectTest_A = _TcoObjectTest_A.CreatePlainerType();
			cloned._TcoObjectTest_B = _TcoObjectTest_B.CreatePlainerType();
			return cloned;
		}

		protected PlainTcoContextTest CreatePlainerType(PlainTcoContextTest cloned)
		{
			cloned._TcoObjectTest_A = _TcoObjectTest_A.CreatePlainerType();
			cloned._TcoObjectTest_B = _TcoObjectTest_B.CreatePlainerType();
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

		public void FlushPlainToOnline(TcoCoreTests.PlainTcoContextTest source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainTcoContextTest source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainTcoContextTest source)
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

		public TcoContextTest(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			__CallMyPlcInstance = @Connector.Online.Adapter.CreateBOOL(this, "", "_CallMyPlcInstance");
			__startCycles = @Connector.Online.Adapter.CreateULINT(this, "", "_startCycles");
			__endCycles = @Connector.Online.Adapter.CreateULINT(this, "", "_endCycles");
			__mainCycles = @Connector.Online.Adapter.CreateULINT(this, "", "_mainCycles");
			__TcoObjectTest_A = new TcoObjectTest(this, "", "_TcoObjectTest_A");
			__TcoObjectTest_B = new TcoObjectTest(this, "", "_TcoObjectTest_B");
			__MyIdentity = @Connector.Online.Adapter.CreateULINT(this, "", "_MyIdentity");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public TcoContextTest()
		{
			PexPreConstructorParameterless();
			__CallMyPlcInstance = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__startCycles = Vortex.Connector.IConnectorFactory.CreateULINT();
			__endCycles = Vortex.Connector.IConnectorFactory.CreateULINT();
			__mainCycles = Vortex.Connector.IConnectorFactory.CreateULINT();
			__TcoObjectTest_A = new TcoObjectTest();
			__TcoObjectTest_B = new TcoObjectTest();
			__MyIdentity = Vortex.Connector.IConnectorFactory.CreateULINT();
			AttributeName = "";
			PexConstructorParameterless();
		}

		public void CallMainFromUnitTest()
		{
			Connector.InvokeRpc(this.Symbol, "CallMainFromUnitTest", new object[]{});
		}

		public void CallRunFromUnitTest()
		{
			Connector.InvokeRpc(this.Symbol, "CallRunFromUnitTest", new object[]{});
		}

		public void ContextClose()
		{
			Connector.InvokeRpc(this.Symbol, "ContextClose", new object[]{});
		}

		public void ContextOpen()
		{
			Connector.InvokeRpc(this.Symbol, "ContextOpen", new object[]{});
		}

		public void ReadOutCycleCounters()
		{
			Connector.InvokeRpc(this.Symbol, "ReadOutCycleCounters", new object[]{});
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoContextTest
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoContextTest()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoContextTest : Vortex.Connector.IVortexOnlineObject
	{
		Vortex.Connector.ValueTypes.Online.IOnlineBool _CallMyPlcInstance
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _startCycles
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _endCycles
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _mainCycles
		{
			get;
		}

		ITcoObjectTest _TcoObjectTest_A
		{
			get;
		}

		ITcoObjectTest _TcoObjectTest_B
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _MyIdentity
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoContextTest CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainTcoContextTest source);
		void FlushOnlineToPlain(TcoCoreTests.PlainTcoContextTest source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoContextTest : Vortex.Connector.IVortexShadowObject
	{
		Vortex.Connector.ValueTypes.Shadows.IShadowBool _CallMyPlcInstance
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _startCycles
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _endCycles
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _mainCycles
		{
			get;
		}

		IShadowTcoObjectTest _TcoObjectTest_A
		{
			get;
		}

		IShadowTcoObjectTest _TcoObjectTest_B
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _MyIdentity
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoContextTest CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainTcoContextTest source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoContextTest : Vortex.Connector.IPlain
	{
		System.Boolean __CallMyPlcInstance;
		public System.Boolean _CallMyPlcInstance
		{
			get
			{
				return __CallMyPlcInstance;
			}

			set
			{
				if (__CallMyPlcInstance != value)
				{
					__CallMyPlcInstance = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_CallMyPlcInstance)));
				}
			}
		}

		System.UInt64 __startCycles;
		public System.UInt64 _startCycles
		{
			get
			{
				return __startCycles;
			}

			set
			{
				if (__startCycles != value)
				{
					__startCycles = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_startCycles)));
				}
			}
		}

		System.UInt64 __endCycles;
		public System.UInt64 _endCycles
		{
			get
			{
				return __endCycles;
			}

			set
			{
				if (__endCycles != value)
				{
					__endCycles = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_endCycles)));
				}
			}
		}

		System.UInt64 __mainCycles;
		public System.UInt64 _mainCycles
		{
			get
			{
				return __mainCycles;
			}

			set
			{
				if (__mainCycles != value)
				{
					__mainCycles = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_mainCycles)));
				}
			}
		}

		PlainTcoObjectTest __TcoObjectTest_A;
		public PlainTcoObjectTest _TcoObjectTest_A
		{
			get
			{
				return __TcoObjectTest_A;
			}

			set
			{
				if (__TcoObjectTest_A != value)
				{
					__TcoObjectTest_A = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_TcoObjectTest_A)));
				}
			}
		}

		PlainTcoObjectTest __TcoObjectTest_B;
		public PlainTcoObjectTest _TcoObjectTest_B
		{
			get
			{
				return __TcoObjectTest_B;
			}

			set
			{
				if (__TcoObjectTest_B != value)
				{
					__TcoObjectTest_B = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_TcoObjectTest_B)));
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

		public void CopyPlainToCyclic(TcoCoreTests.TcoContextTest target)
		{
			target._CallMyPlcInstance.Cyclic = _CallMyPlcInstance;
			target._startCycles.Cyclic = _startCycles;
			target._endCycles.Cyclic = _endCycles;
			target._mainCycles.Cyclic = _mainCycles;
			_TcoObjectTest_A.CopyPlainToCyclic(target._TcoObjectTest_A);
			_TcoObjectTest_B.CopyPlainToCyclic(target._TcoObjectTest_B);
			target._MyIdentity.Cyclic = _MyIdentity;
		}

		public void CopyPlainToCyclic(TcoCoreTests.ITcoContextTest target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.TcoContextTest)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.TcoContextTest target)
		{
			target._CallMyPlcInstance.Shadow = _CallMyPlcInstance;
			target._startCycles.Shadow = _startCycles;
			target._endCycles.Shadow = _endCycles;
			target._mainCycles.Shadow = _mainCycles;
			_TcoObjectTest_A.CopyPlainToShadow(target._TcoObjectTest_A);
			_TcoObjectTest_B.CopyPlainToShadow(target._TcoObjectTest_B);
			target._MyIdentity.Shadow = _MyIdentity;
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowTcoContextTest target)
		{
			this.CopyPlainToShadow((TcoCoreTests.TcoContextTest)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.TcoContextTest source)
		{
			_CallMyPlcInstance = source._CallMyPlcInstance.LastValue;
			_startCycles = source._startCycles.LastValue;
			_endCycles = source._endCycles.LastValue;
			_mainCycles = source._mainCycles.LastValue;
			_TcoObjectTest_A.CopyCyclicToPlain(source._TcoObjectTest_A);
			_TcoObjectTest_B.CopyCyclicToPlain(source._TcoObjectTest_B);
			_MyIdentity = source._MyIdentity.LastValue;
		}

		public void CopyCyclicToPlain(TcoCoreTests.ITcoContextTest source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.TcoContextTest)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.TcoContextTest source)
		{
			_CallMyPlcInstance = source._CallMyPlcInstance.Shadow;
			_startCycles = source._startCycles.Shadow;
			_endCycles = source._endCycles.Shadow;
			_mainCycles = source._mainCycles.Shadow;
			_TcoObjectTest_A.CopyShadowToPlain(source._TcoObjectTest_A);
			_TcoObjectTest_B.CopyShadowToPlain(source._TcoObjectTest_B);
			_MyIdentity = source._MyIdentity.Shadow;
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowTcoContextTest source)
		{
			this.CopyShadowToPlain((TcoCoreTests.TcoContextTest)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoContextTest()
		{
			__TcoObjectTest_A = new PlainTcoObjectTest();
			__TcoObjectTest_B = new PlainTcoObjectTest();
		}
	}
}