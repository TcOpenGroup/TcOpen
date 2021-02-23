using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoObjectTest", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class TcoObjectTest : Vortex.Connector.IVortexObject, ITcoObjectTest, IShadowTcoObjectTest, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoObjectTest._MyIdentity
		{
			get
			{
				return _MyIdentity;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoObjectTest._MyIdentity
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

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoObjectTest._MyContextIdentity
		{
			get
			{
				return _MyContextIdentity;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoObjectTest._MyContextIdentity
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

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoObjectTest._MyContextStartCount
		{
			get
			{
				return _MyContextStartCount;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoObjectTest._MyContextStartCount
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

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoObjectTest._MyContextEndCount
		{
			get
			{
				return _MyContextEndCount;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoObjectTest._MyContextEndCount
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

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoObjectTest._MyParentIdentity
		{
			get
			{
				return _MyParentIdentity;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoObjectTest._MyParentIdentity
		{
			get
			{
				return _MyParentIdentity;
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

		ITcoTaskTest ITcoObjectTest._TcoTaskTest_A
		{
			get
			{
				return _TcoTaskTest_A;
			}
		}

		IShadowTcoTaskTest IShadowTcoObjectTest._TcoTaskTest_A
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

		ITcoTaskTest ITcoObjectTest._TcoTaskTest_B
		{
			get
			{
				return _TcoTaskTest_B;
			}
		}

		IShadowTcoTaskTest IShadowTcoObjectTest._TcoTaskTest_B
		{
			get
			{
				return _TcoTaskTest_B;
			}
		}

		TcoStateTest __TcoStateTest_A;
		public TcoStateTest _TcoStateTest_A
		{
			get
			{
				return __TcoStateTest_A;
			}
		}

		ITcoStateTest ITcoObjectTest._TcoStateTest_A
		{
			get
			{
				return _TcoStateTest_A;
			}
		}

		IShadowTcoStateTest IShadowTcoObjectTest._TcoStateTest_A
		{
			get
			{
				return _TcoStateTest_A;
			}
		}

		TcoStateTest __TcoStateTest_B;
		public TcoStateTest _TcoStateTest_B
		{
			get
			{
				return __TcoStateTest_B;
			}
		}

		ITcoStateTest ITcoObjectTest._TcoStateTest_B
		{
			get
			{
				return _TcoStateTest_B;
			}
		}

		IShadowTcoStateTest IShadowTcoObjectTest._TcoStateTest_B
		{
			get
			{
				return _TcoStateTest_B;
			}
		}

		public void LazyOnlineToShadow()
		{
			_MyIdentity.Shadow = _MyIdentity.LastValue;
			_MyContextIdentity.Shadow = _MyContextIdentity.LastValue;
			_MyContextStartCount.Shadow = _MyContextStartCount.LastValue;
			_MyContextEndCount.Shadow = _MyContextEndCount.LastValue;
			_MyParentIdentity.Shadow = _MyParentIdentity.LastValue;
			_TcoTaskTest_A.LazyOnlineToShadow();
			_TcoTaskTest_B.LazyOnlineToShadow();
			_TcoStateTest_A.LazyOnlineToShadow();
			_TcoStateTest_B.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			_MyIdentity.Cyclic = _MyIdentity.Shadow;
			_MyContextIdentity.Cyclic = _MyContextIdentity.Shadow;
			_MyContextStartCount.Cyclic = _MyContextStartCount.Shadow;
			_MyContextEndCount.Cyclic = _MyContextEndCount.Shadow;
			_MyParentIdentity.Cyclic = _MyParentIdentity.Shadow;
			_TcoTaskTest_A.LazyShadowToOnline();
			_TcoTaskTest_B.LazyShadowToOnline();
			_TcoStateTest_A.LazyShadowToOnline();
			_TcoStateTest_B.LazyShadowToOnline();
		}

		public PlainTcoObjectTest CreatePlainerType()
		{
			var cloned = new PlainTcoObjectTest();
			cloned._TcoTaskTest_A = _TcoTaskTest_A.CreatePlainerType();
			cloned._TcoTaskTest_B = _TcoTaskTest_B.CreatePlainerType();
			cloned._TcoStateTest_A = _TcoStateTest_A.CreatePlainerType();
			cloned._TcoStateTest_B = _TcoStateTest_B.CreatePlainerType();
			return cloned;
		}

		protected PlainTcoObjectTest CreatePlainerType(PlainTcoObjectTest cloned)
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

		public void FlushPlainToOnline(TcoCoreTests.PlainTcoObjectTest source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainTcoObjectTest source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainTcoObjectTest source)
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

		public TcoObjectTest(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			__TcoTaskTest_A = new TcoTaskTest(this, "", "_TcoTaskTest_A");
			__TcoTaskTest_B = new TcoTaskTest(this, "", "_TcoTaskTest_B");
			__TcoStateTest_A = new TcoStateTest(this, "", "_TcoStateTest_A");
			__TcoStateTest_B = new TcoStateTest(this, "", "_TcoStateTest_B");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public TcoObjectTest()
		{
			PexPreConstructorParameterless();
			__MyIdentity = Vortex.Connector.IConnectorFactory.CreateULINT();
			__MyContextIdentity = Vortex.Connector.IConnectorFactory.CreateULINT();
			__MyContextStartCount = Vortex.Connector.IConnectorFactory.CreateULINT();
			__MyContextEndCount = Vortex.Connector.IConnectorFactory.CreateULINT();
			__MyParentIdentity = Vortex.Connector.IConnectorFactory.CreateULINT();
			__TcoTaskTest_A = new TcoTaskTest();
			__TcoTaskTest_B = new TcoTaskTest();
			__TcoStateTest_A = new TcoStateTest();
			__TcoStateTest_B = new TcoStateTest();
			AttributeName = "";
			PexConstructorParameterless();
		}

		public void CallTaskInstancies()
		{
			Connector.InvokeRpc(this.Symbol, "CallTaskInstancies", new object[]{});
		}

		public void ReadOutIdentities()
		{
			Connector.InvokeRpc(this.Symbol, "ReadOutIdentities", new object[]{});
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoObjectTest
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoObjectTest()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoObjectTest : Vortex.Connector.IVortexOnlineObject
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

		ITcoTaskTest _TcoTaskTest_A
		{
			get;
		}

		ITcoTaskTest _TcoTaskTest_B
		{
			get;
		}

		ITcoStateTest _TcoStateTest_A
		{
			get;
		}

		ITcoStateTest _TcoStateTest_B
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoObjectTest CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainTcoObjectTest source);
		void FlushOnlineToPlain(TcoCoreTests.PlainTcoObjectTest source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoObjectTest : Vortex.Connector.IVortexShadowObject
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

		IShadowTcoTaskTest _TcoTaskTest_A
		{
			get;
		}

		IShadowTcoTaskTest _TcoTaskTest_B
		{
			get;
		}

		IShadowTcoStateTest _TcoStateTest_A
		{
			get;
		}

		IShadowTcoStateTest _TcoStateTest_B
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoObjectTest CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainTcoObjectTest source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoObjectTest : Vortex.Connector.IPlain
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

		PlainTcoStateTest __TcoStateTest_A;
		public PlainTcoStateTest _TcoStateTest_A
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

		PlainTcoStateTest __TcoStateTest_B;
		public PlainTcoStateTest _TcoStateTest_B
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

		public void CopyPlainToCyclic(TcoCoreTests.TcoObjectTest target)
		{
			target._MyIdentity.Cyclic = _MyIdentity;
			target._MyContextIdentity.Cyclic = _MyContextIdentity;
			target._MyContextStartCount.Cyclic = _MyContextStartCount;
			target._MyContextEndCount.Cyclic = _MyContextEndCount;
			target._MyParentIdentity.Cyclic = _MyParentIdentity;
			_TcoTaskTest_A.CopyPlainToCyclic(target._TcoTaskTest_A);
			_TcoTaskTest_B.CopyPlainToCyclic(target._TcoTaskTest_B);
			_TcoStateTest_A.CopyPlainToCyclic(target._TcoStateTest_A);
			_TcoStateTest_B.CopyPlainToCyclic(target._TcoStateTest_B);
		}

		public void CopyPlainToCyclic(TcoCoreTests.ITcoObjectTest target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.TcoObjectTest)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.TcoObjectTest target)
		{
			target._MyIdentity.Shadow = _MyIdentity;
			target._MyContextIdentity.Shadow = _MyContextIdentity;
			target._MyContextStartCount.Shadow = _MyContextStartCount;
			target._MyContextEndCount.Shadow = _MyContextEndCount;
			target._MyParentIdentity.Shadow = _MyParentIdentity;
			_TcoTaskTest_A.CopyPlainToShadow(target._TcoTaskTest_A);
			_TcoTaskTest_B.CopyPlainToShadow(target._TcoTaskTest_B);
			_TcoStateTest_A.CopyPlainToShadow(target._TcoStateTest_A);
			_TcoStateTest_B.CopyPlainToShadow(target._TcoStateTest_B);
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowTcoObjectTest target)
		{
			this.CopyPlainToShadow((TcoCoreTests.TcoObjectTest)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.TcoObjectTest source)
		{
			_MyIdentity = source._MyIdentity.LastValue;
			_MyContextIdentity = source._MyContextIdentity.LastValue;
			_MyContextStartCount = source._MyContextStartCount.LastValue;
			_MyContextEndCount = source._MyContextEndCount.LastValue;
			_MyParentIdentity = source._MyParentIdentity.LastValue;
			_TcoTaskTest_A.CopyCyclicToPlain(source._TcoTaskTest_A);
			_TcoTaskTest_B.CopyCyclicToPlain(source._TcoTaskTest_B);
			_TcoStateTest_A.CopyCyclicToPlain(source._TcoStateTest_A);
			_TcoStateTest_B.CopyCyclicToPlain(source._TcoStateTest_B);
		}

		public void CopyCyclicToPlain(TcoCoreTests.ITcoObjectTest source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.TcoObjectTest)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.TcoObjectTest source)
		{
			_MyIdentity = source._MyIdentity.Shadow;
			_MyContextIdentity = source._MyContextIdentity.Shadow;
			_MyContextStartCount = source._MyContextStartCount.Shadow;
			_MyContextEndCount = source._MyContextEndCount.Shadow;
			_MyParentIdentity = source._MyParentIdentity.Shadow;
			_TcoTaskTest_A.CopyShadowToPlain(source._TcoTaskTest_A);
			_TcoTaskTest_B.CopyShadowToPlain(source._TcoTaskTest_B);
			_TcoStateTest_A.CopyShadowToPlain(source._TcoStateTest_A);
			_TcoStateTest_B.CopyShadowToPlain(source._TcoStateTest_B);
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowTcoObjectTest source)
		{
			this.CopyShadowToPlain((TcoCoreTests.TcoObjectTest)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoObjectTest()
		{
			__TcoTaskTest_A = new PlainTcoTaskTest();
			__TcoTaskTest_B = new PlainTcoTaskTest();
			__TcoStateTest_A = new PlainTcoStateTest();
			__TcoStateTest_B = new PlainTcoStateTest();
		}
	}
}