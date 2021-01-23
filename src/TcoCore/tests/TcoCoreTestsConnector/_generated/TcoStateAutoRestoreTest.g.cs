using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoStateAutoRestoreTest", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class TcoStateAutoRestoreTest : Vortex.Connector.IVortexObject, ITcoStateAutoRestoreTest, IShadowTcoStateAutoRestoreTest, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		Vortex.Connector.ValueTypes.OnlinerInt __MyState;
		public Vortex.Connector.ValueTypes.OnlinerInt _MyState
		{
			get
			{
				return __MyState;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineInt ITcoStateAutoRestoreTest._MyState
		{
			get
			{
				return _MyState;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadowTcoStateAutoRestoreTest._MyState
		{
			get
			{
				return _MyState;
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

		Vortex.Connector.ValueTypes.Online.IOnlineInt ITcoStateAutoRestoreTest._OnStateChangeCounter
		{
			get
			{
				return _OnStateChangeCounter;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadowTcoStateAutoRestoreTest._OnStateChangeCounter
		{
			get
			{
				return _OnStateChangeCounter;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerInt __RestoreCounter;
		public Vortex.Connector.ValueTypes.OnlinerInt _RestoreCounter
		{
			get
			{
				return __RestoreCounter;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineInt ITcoStateAutoRestoreTest._RestoreCounter
		{
			get
			{
				return _RestoreCounter;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadowTcoStateAutoRestoreTest._RestoreCounter
		{
			get
			{
				return _RestoreCounter;
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

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoStateAutoRestoreTest._AutoRestoreToMyChildsEnabled
		{
			get
			{
				return _AutoRestoreToMyChildsEnabled;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoStateAutoRestoreTest._AutoRestoreToMyChildsEnabled
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

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoStateAutoRestoreTest._AutoRestoreByMyParentEnabled
		{
			get
			{
				return _AutoRestoreByMyParentEnabled;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoStateAutoRestoreTest._AutoRestoreByMyParentEnabled
		{
			get
			{
				return _AutoRestoreByMyParentEnabled;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __CountsPerStep;
		public Vortex.Connector.ValueTypes.OnlinerULInt _CountsPerStep
		{
			get
			{
				return __CountsPerStep;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoStateAutoRestoreTest._CountsPerStep
		{
			get
			{
				return _CountsPerStep;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoStateAutoRestoreTest._CountsPerStep
		{
			get
			{
				return _CountsPerStep;
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

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoStateAutoRestoreTest._CounterValue
		{
			get
			{
				return _CounterValue;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoStateAutoRestoreTest._CounterValue
		{
			get
			{
				return _CounterValue;
			}
		}

		public void LazyOnlineToShadow()
		{
			_MyState.Shadow = _MyState.LastValue;
			_OnStateChangeCounter.Shadow = _OnStateChangeCounter.LastValue;
			_RestoreCounter.Shadow = _RestoreCounter.LastValue;
			_AutoRestoreToMyChildsEnabled.Shadow = _AutoRestoreToMyChildsEnabled.LastValue;
			_AutoRestoreByMyParentEnabled.Shadow = _AutoRestoreByMyParentEnabled.LastValue;
			_CountsPerStep.Shadow = _CountsPerStep.LastValue;
			_CounterValue.Shadow = _CounterValue.LastValue;
		}

		public void LazyShadowToOnline()
		{
			_MyState.Cyclic = _MyState.Shadow;
			_OnStateChangeCounter.Cyclic = _OnStateChangeCounter.Shadow;
			_RestoreCounter.Cyclic = _RestoreCounter.Shadow;
			_AutoRestoreToMyChildsEnabled.Cyclic = _AutoRestoreToMyChildsEnabled.Shadow;
			_AutoRestoreByMyParentEnabled.Cyclic = _AutoRestoreByMyParentEnabled.Shadow;
			_CountsPerStep.Cyclic = _CountsPerStep.Shadow;
			_CounterValue.Cyclic = _CounterValue.Shadow;
		}

		public PlainTcoStateAutoRestoreTest CreatePlainerType()
		{
			var cloned = new PlainTcoStateAutoRestoreTest();
			return cloned;
		}

		protected PlainTcoStateAutoRestoreTest CreatePlainerType(PlainTcoStateAutoRestoreTest cloned)
		{
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
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

		public void FlushPlainToOnline(TcoCoreTests.PlainTcoStateAutoRestoreTest source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainTcoStateAutoRestoreTest source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainTcoStateAutoRestoreTest source)
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

		public TcoStateAutoRestoreTest(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@ValueTags = new System.Collections.Generic.List<Vortex.Connector.IValueTag>();
			this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			__MyState = @Connector.Online.Adapter.CreateINT(this, "", "_MyState");
			__OnStateChangeCounter = @Connector.Online.Adapter.CreateINT(this, "", "_OnStateChangeCounter");
			__RestoreCounter = @Connector.Online.Adapter.CreateINT(this, "", "_RestoreCounter");
			__AutoRestoreToMyChildsEnabled = @Connector.Online.Adapter.CreateBOOL(this, "", "_AutoRestoreToMyChildsEnabled");
			__AutoRestoreByMyParentEnabled = @Connector.Online.Adapter.CreateBOOL(this, "", "_AutoRestoreByMyParentEnabled");
			__CountsPerStep = @Connector.Online.Adapter.CreateULINT(this, "", "_CountsPerStep");
			__CounterValue = @Connector.Online.Adapter.CreateULINT(this, "", "_CounterValue");
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
			parent.AddChild(this);
		}

		public TcoStateAutoRestoreTest()
		{
			PexPreConstructorParameterless();
			__MyState = Vortex.Connector.IConnectorFactory.CreateINT();
			__OnStateChangeCounter = Vortex.Connector.IConnectorFactory.CreateINT();
			__RestoreCounter = Vortex.Connector.IConnectorFactory.CreateINT();
			__AutoRestoreToMyChildsEnabled = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__AutoRestoreByMyParentEnabled = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__CountsPerStep = Vortex.Connector.IConnectorFactory.CreateULINT();
			__CounterValue = Vortex.Connector.IConnectorFactory.CreateULINT();
			AttributeName = "";
			PexConstructorParameterless();
		}

		public void CleanUp()
		{
			Connector.InvokeRpc(this.Symbol, "CleanUp", new object[]{});
		}

		public void ExecutionBody()
		{
			Connector.InvokeRpc(this.Symbol, "ExecutionBody", new object[]{});
		}

		public void ReadOutAutoRestoreProperties()
		{
			Connector.InvokeRpc(this.Symbol, "ReadOutAutoRestoreProperties", new object[]{});
		}

		public void TriggerRestore()
		{
			Connector.InvokeRpc(this.Symbol, "TriggerRestore", new object[]{});
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoStateAutoRestoreTest
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoStateAutoRestoreTest()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoStateAutoRestoreTest : Vortex.Connector.IVortexOnlineObject
	{
		Vortex.Connector.ValueTypes.Online.IOnlineInt _MyState
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineInt _OnStateChangeCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineInt _RestoreCounter
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

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _CountsPerStep
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _CounterValue
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoStateAutoRestoreTest CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainTcoStateAutoRestoreTest source);
		void FlushOnlineToPlain(TcoCoreTests.PlainTcoStateAutoRestoreTest source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoStateAutoRestoreTest : Vortex.Connector.IVortexShadowObject
	{
		Vortex.Connector.ValueTypes.Shadows.IShadowInt _MyState
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowInt _OnStateChangeCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowInt _RestoreCounter
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

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _CountsPerStep
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _CounterValue
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoStateAutoRestoreTest CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainTcoStateAutoRestoreTest source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoStateAutoRestoreTest : Vortex.Connector.IPlain
	{
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

		System.Int16 __RestoreCounter;
		public System.Int16 _RestoreCounter
		{
			get
			{
				return __RestoreCounter;
			}

			set
			{
				if (__RestoreCounter != value)
				{
					__RestoreCounter = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_RestoreCounter)));
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

		System.UInt64 __CountsPerStep;
		public System.UInt64 _CountsPerStep
		{
			get
			{
				return __CountsPerStep;
			}

			set
			{
				if (__CountsPerStep != value)
				{
					__CountsPerStep = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_CountsPerStep)));
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

		public void CopyPlainToCyclic(TcoCoreTests.TcoStateAutoRestoreTest target)
		{
			target._MyState.Cyclic = _MyState;
			target._OnStateChangeCounter.Cyclic = _OnStateChangeCounter;
			target._RestoreCounter.Cyclic = _RestoreCounter;
			target._AutoRestoreToMyChildsEnabled.Cyclic = _AutoRestoreToMyChildsEnabled;
			target._AutoRestoreByMyParentEnabled.Cyclic = _AutoRestoreByMyParentEnabled;
			target._CountsPerStep.Cyclic = _CountsPerStep;
			target._CounterValue.Cyclic = _CounterValue;
		}

		public void CopyPlainToCyclic(TcoCoreTests.ITcoStateAutoRestoreTest target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.TcoStateAutoRestoreTest)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.TcoStateAutoRestoreTest target)
		{
			target._MyState.Shadow = _MyState;
			target._OnStateChangeCounter.Shadow = _OnStateChangeCounter;
			target._RestoreCounter.Shadow = _RestoreCounter;
			target._AutoRestoreToMyChildsEnabled.Shadow = _AutoRestoreToMyChildsEnabled;
			target._AutoRestoreByMyParentEnabled.Shadow = _AutoRestoreByMyParentEnabled;
			target._CountsPerStep.Shadow = _CountsPerStep;
			target._CounterValue.Shadow = _CounterValue;
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowTcoStateAutoRestoreTest target)
		{
			this.CopyPlainToShadow((TcoCoreTests.TcoStateAutoRestoreTest)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.TcoStateAutoRestoreTest source)
		{
			_MyState = source._MyState.LastValue;
			_OnStateChangeCounter = source._OnStateChangeCounter.LastValue;
			_RestoreCounter = source._RestoreCounter.LastValue;
			_AutoRestoreToMyChildsEnabled = source._AutoRestoreToMyChildsEnabled.LastValue;
			_AutoRestoreByMyParentEnabled = source._AutoRestoreByMyParentEnabled.LastValue;
			_CountsPerStep = source._CountsPerStep.LastValue;
			_CounterValue = source._CounterValue.LastValue;
		}

		public void CopyCyclicToPlain(TcoCoreTests.ITcoStateAutoRestoreTest source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.TcoStateAutoRestoreTest)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.TcoStateAutoRestoreTest source)
		{
			_MyState = source._MyState.Shadow;
			_OnStateChangeCounter = source._OnStateChangeCounter.Shadow;
			_RestoreCounter = source._RestoreCounter.Shadow;
			_AutoRestoreToMyChildsEnabled = source._AutoRestoreToMyChildsEnabled.Shadow;
			_AutoRestoreByMyParentEnabled = source._AutoRestoreByMyParentEnabled.Shadow;
			_CountsPerStep = source._CountsPerStep.Shadow;
			_CounterValue = source._CounterValue.Shadow;
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowTcoStateAutoRestoreTest source)
		{
			this.CopyShadowToPlain((TcoCoreTests.TcoStateAutoRestoreTest)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoStateAutoRestoreTest()
		{
		}
	}
}