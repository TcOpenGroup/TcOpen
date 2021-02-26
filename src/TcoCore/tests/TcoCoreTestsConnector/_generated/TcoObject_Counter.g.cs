using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoObject_Counter", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class TcoObject_Counter : Vortex.Connector.IVortexObject, ITcoObject_Counter, IShadowTcoObject_Counter, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		Vortex.Connector.ValueTypes.OnlinerLInt _CounterValue;
		public Vortex.Connector.ValueTypes.OnlinerLInt CounterValue
		{
			get
			{
				return _CounterValue;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt ITcoObject_Counter.CounterValue
		{
			get
			{
				return CounterValue;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt IShadowTcoObject_Counter.CounterValue
		{
			get
			{
				return CounterValue;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLInt _UpperLimit;
		public Vortex.Connector.ValueTypes.OnlinerLInt UpperLimit
		{
			get
			{
				return _UpperLimit;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt ITcoObject_Counter.UpperLimit
		{
			get
			{
				return UpperLimit;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt IShadowTcoObject_Counter.UpperLimit
		{
			get
			{
				return UpperLimit;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLInt _LowerLimit;
		public Vortex.Connector.ValueTypes.OnlinerLInt LowerLimit
		{
			get
			{
				return _LowerLimit;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt ITcoObject_Counter.LowerLimit
		{
			get
			{
				return LowerLimit;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt IShadowTcoObject_Counter.LowerLimit
		{
			get
			{
				return LowerLimit;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLInt _CountUp_InvokeTrigCounter;
		public Vortex.Connector.ValueTypes.OnlinerLInt CountUp_InvokeTrigCounter
		{
			get
			{
				return _CountUp_InvokeTrigCounter;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt ITcoObject_Counter.CountUp_InvokeTrigCounter
		{
			get
			{
				return CountUp_InvokeTrigCounter;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt IShadowTcoObject_Counter.CountUp_InvokeTrigCounter
		{
			get
			{
				return CountUp_InvokeTrigCounter;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLInt _CountUp_DoneTrigCounter;
		public Vortex.Connector.ValueTypes.OnlinerLInt CountUp_DoneTrigCounter
		{
			get
			{
				return _CountUp_DoneTrigCounter;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt ITcoObject_Counter.CountUp_DoneTrigCounter
		{
			get
			{
				return CountUp_DoneTrigCounter;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt IShadowTcoObject_Counter.CountUp_DoneTrigCounter
		{
			get
			{
				return CountUp_DoneTrigCounter;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLInt _CountDown_InvokeTrigCounter;
		public Vortex.Connector.ValueTypes.OnlinerLInt CountDown_InvokeTrigCounter
		{
			get
			{
				return _CountDown_InvokeTrigCounter;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt ITcoObject_Counter.CountDown_InvokeTrigCounter
		{
			get
			{
				return CountDown_InvokeTrigCounter;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt IShadowTcoObject_Counter.CountDown_InvokeTrigCounter
		{
			get
			{
				return CountDown_InvokeTrigCounter;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLInt _CountDown_DoneTrigCounter;
		public Vortex.Connector.ValueTypes.OnlinerLInt CountDown_DoneTrigCounter
		{
			get
			{
				return _CountDown_DoneTrigCounter;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt ITcoObject_Counter.CountDown_DoneTrigCounter
		{
			get
			{
				return CountDown_DoneTrigCounter;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt IShadowTcoObject_Counter.CountDown_DoneTrigCounter
		{
			get
			{
				return CountDown_DoneTrigCounter;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLInt _ResetToZero_InvokeTrigCounter;
		public Vortex.Connector.ValueTypes.OnlinerLInt ResetToZero_InvokeTrigCounter
		{
			get
			{
				return _ResetToZero_InvokeTrigCounter;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt ITcoObject_Counter.ResetToZero_InvokeTrigCounter
		{
			get
			{
				return ResetToZero_InvokeTrigCounter;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt IShadowTcoObject_Counter.ResetToZero_InvokeTrigCounter
		{
			get
			{
				return ResetToZero_InvokeTrigCounter;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLInt _ResetToZero_DoneTrigCounter;
		public Vortex.Connector.ValueTypes.OnlinerLInt ResetToZero_DoneTrigCounter
		{
			get
			{
				return _ResetToZero_DoneTrigCounter;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt ITcoObject_Counter.ResetToZero_DoneTrigCounter
		{
			get
			{
				return ResetToZero_DoneTrigCounter;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt IShadowTcoObject_Counter.ResetToZero_DoneTrigCounter
		{
			get
			{
				return ResetToZero_DoneTrigCounter;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool _CountUp_Execute;
		public Vortex.Connector.ValueTypes.OnlinerBool CountUp_Execute
		{
			get
			{
				return _CountUp_Execute;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoObject_Counter.CountUp_Execute
		{
			get
			{
				return CountUp_Execute;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoObject_Counter.CountUp_Execute
		{
			get
			{
				return CountUp_Execute;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool _CountDown_Execute;
		public Vortex.Connector.ValueTypes.OnlinerBool CountDown_Execute
		{
			get
			{
				return _CountDown_Execute;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoObject_Counter.CountDown_Execute
		{
			get
			{
				return CountDown_Execute;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoObject_Counter.CountDown_Execute
		{
			get
			{
				return CountDown_Execute;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool _ResetToZero_Execute;
		public Vortex.Connector.ValueTypes.OnlinerBool ResetToZero_Execute
		{
			get
			{
				return _ResetToZero_Execute;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoObject_Counter.ResetToZero_Execute
		{
			get
			{
				return ResetToZero_Execute;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoObject_Counter.ResetToZero_Execute
		{
			get
			{
				return ResetToZero_Execute;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool _CountUp_Busy;
		public Vortex.Connector.ValueTypes.OnlinerBool CountUp_Busy
		{
			get
			{
				return _CountUp_Busy;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoObject_Counter.CountUp_Busy
		{
			get
			{
				return CountUp_Busy;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoObject_Counter.CountUp_Busy
		{
			get
			{
				return CountUp_Busy;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool _CountDown_Busy;
		public Vortex.Connector.ValueTypes.OnlinerBool CountDown_Busy
		{
			get
			{
				return _CountDown_Busy;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoObject_Counter.CountDown_Busy
		{
			get
			{
				return CountDown_Busy;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoObject_Counter.CountDown_Busy
		{
			get
			{
				return CountDown_Busy;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool _ResetToZero_Busy;
		public Vortex.Connector.ValueTypes.OnlinerBool ResetToZero_Busy
		{
			get
			{
				return _ResetToZero_Busy;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoObject_Counter.ResetToZero_Busy
		{
			get
			{
				return ResetToZero_Busy;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoObject_Counter.ResetToZero_Busy
		{
			get
			{
				return ResetToZero_Busy;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool _CountUp_Done;
		public Vortex.Connector.ValueTypes.OnlinerBool CountUp_Done
		{
			get
			{
				return _CountUp_Done;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoObject_Counter.CountUp_Done
		{
			get
			{
				return CountUp_Done;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoObject_Counter.CountUp_Done
		{
			get
			{
				return CountUp_Done;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool _CountDown_Done;
		public Vortex.Connector.ValueTypes.OnlinerBool CountDown_Done
		{
			get
			{
				return _CountDown_Done;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoObject_Counter.CountDown_Done
		{
			get
			{
				return CountDown_Done;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoObject_Counter.CountDown_Done
		{
			get
			{
				return CountDown_Done;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool _ResetToZero_Done;
		public Vortex.Connector.ValueTypes.OnlinerBool ResetToZero_Done
		{
			get
			{
				return _ResetToZero_Done;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoObject_Counter.ResetToZero_Done
		{
			get
			{
				return ResetToZero_Done;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoObject_Counter.ResetToZero_Done
		{
			get
			{
				return ResetToZero_Done;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLInt _Cycle;
		public Vortex.Connector.ValueTypes.OnlinerLInt Cycle
		{
			get
			{
				return _Cycle;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt ITcoObject_Counter.Cycle
		{
			get
			{
				return Cycle;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt IShadowTcoObject_Counter.Cycle
		{
			get
			{
				return Cycle;
			}
		}

		public void LazyOnlineToShadow()
		{
			CounterValue.Shadow = CounterValue.LastValue;
			UpperLimit.Shadow = UpperLimit.LastValue;
			LowerLimit.Shadow = LowerLimit.LastValue;
			CountUp_InvokeTrigCounter.Shadow = CountUp_InvokeTrigCounter.LastValue;
			CountUp_DoneTrigCounter.Shadow = CountUp_DoneTrigCounter.LastValue;
			CountDown_InvokeTrigCounter.Shadow = CountDown_InvokeTrigCounter.LastValue;
			CountDown_DoneTrigCounter.Shadow = CountDown_DoneTrigCounter.LastValue;
			ResetToZero_InvokeTrigCounter.Shadow = ResetToZero_InvokeTrigCounter.LastValue;
			ResetToZero_DoneTrigCounter.Shadow = ResetToZero_DoneTrigCounter.LastValue;
			CountUp_Execute.Shadow = CountUp_Execute.LastValue;
			CountDown_Execute.Shadow = CountDown_Execute.LastValue;
			ResetToZero_Execute.Shadow = ResetToZero_Execute.LastValue;
			CountUp_Busy.Shadow = CountUp_Busy.LastValue;
			CountDown_Busy.Shadow = CountDown_Busy.LastValue;
			ResetToZero_Busy.Shadow = ResetToZero_Busy.LastValue;
			CountUp_Done.Shadow = CountUp_Done.LastValue;
			CountDown_Done.Shadow = CountDown_Done.LastValue;
			ResetToZero_Done.Shadow = ResetToZero_Done.LastValue;
			Cycle.Shadow = Cycle.LastValue;
		}

		public void LazyShadowToOnline()
		{
			CounterValue.Cyclic = CounterValue.Shadow;
			UpperLimit.Cyclic = UpperLimit.Shadow;
			LowerLimit.Cyclic = LowerLimit.Shadow;
			CountUp_InvokeTrigCounter.Cyclic = CountUp_InvokeTrigCounter.Shadow;
			CountUp_DoneTrigCounter.Cyclic = CountUp_DoneTrigCounter.Shadow;
			CountDown_InvokeTrigCounter.Cyclic = CountDown_InvokeTrigCounter.Shadow;
			CountDown_DoneTrigCounter.Cyclic = CountDown_DoneTrigCounter.Shadow;
			ResetToZero_InvokeTrigCounter.Cyclic = ResetToZero_InvokeTrigCounter.Shadow;
			ResetToZero_DoneTrigCounter.Cyclic = ResetToZero_DoneTrigCounter.Shadow;
			CountUp_Execute.Cyclic = CountUp_Execute.Shadow;
			CountDown_Execute.Cyclic = CountDown_Execute.Shadow;
			ResetToZero_Execute.Cyclic = ResetToZero_Execute.Shadow;
			CountUp_Busy.Cyclic = CountUp_Busy.Shadow;
			CountDown_Busy.Cyclic = CountDown_Busy.Shadow;
			ResetToZero_Busy.Cyclic = ResetToZero_Busy.Shadow;
			CountUp_Done.Cyclic = CountUp_Done.Shadow;
			CountDown_Done.Cyclic = CountDown_Done.Shadow;
			ResetToZero_Done.Cyclic = ResetToZero_Done.Shadow;
			Cycle.Cyclic = Cycle.Shadow;
		}

		public PlainTcoObject_Counter CreatePlainerType()
		{
			var cloned = new PlainTcoObject_Counter();
			return cloned;
		}

		protected PlainTcoObject_Counter CreatePlainerType(PlainTcoObject_Counter cloned)
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

		public void FlushPlainToOnline(TcoCoreTests.PlainTcoObject_Counter source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainTcoObject_Counter source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainTcoObject_Counter source)
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

		public TcoObject_Counter(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			_CounterValue = @Connector.Online.Adapter.CreateLINT(this, "", "CounterValue");
			_UpperLimit = @Connector.Online.Adapter.CreateLINT(this, "", "UpperLimit");
			_LowerLimit = @Connector.Online.Adapter.CreateLINT(this, "", "LowerLimit");
			_CountUp_InvokeTrigCounter = @Connector.Online.Adapter.CreateLINT(this, "", "CountUp_InvokeTrigCounter");
			_CountUp_DoneTrigCounter = @Connector.Online.Adapter.CreateLINT(this, "", "CountUp_DoneTrigCounter");
			_CountDown_InvokeTrigCounter = @Connector.Online.Adapter.CreateLINT(this, "", "CountDown_InvokeTrigCounter");
			_CountDown_DoneTrigCounter = @Connector.Online.Adapter.CreateLINT(this, "", "CountDown_DoneTrigCounter");
			_ResetToZero_InvokeTrigCounter = @Connector.Online.Adapter.CreateLINT(this, "", "ResetToZero_InvokeTrigCounter");
			_ResetToZero_DoneTrigCounter = @Connector.Online.Adapter.CreateLINT(this, "", "ResetToZero_DoneTrigCounter");
			_CountUp_Execute = @Connector.Online.Adapter.CreateBOOL(this, "", "CountUp_Execute");
			_CountDown_Execute = @Connector.Online.Adapter.CreateBOOL(this, "", "CountDown_Execute");
			_ResetToZero_Execute = @Connector.Online.Adapter.CreateBOOL(this, "", "ResetToZero_Execute");
			_CountUp_Busy = @Connector.Online.Adapter.CreateBOOL(this, "", "CountUp_Busy");
			_CountDown_Busy = @Connector.Online.Adapter.CreateBOOL(this, "", "CountDown_Busy");
			_ResetToZero_Busy = @Connector.Online.Adapter.CreateBOOL(this, "", "ResetToZero_Busy");
			_CountUp_Done = @Connector.Online.Adapter.CreateBOOL(this, "", "CountUp_Done");
			_CountDown_Done = @Connector.Online.Adapter.CreateBOOL(this, "", "CountDown_Done");
			_ResetToZero_Done = @Connector.Online.Adapter.CreateBOOL(this, "", "ResetToZero_Done");
			_Cycle = @Connector.Online.Adapter.CreateLINT(this, "", "Cycle");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public TcoObject_Counter()
		{
			PexPreConstructorParameterless();
			_CounterValue = Vortex.Connector.IConnectorFactory.CreateLINT();
			_UpperLimit = Vortex.Connector.IConnectorFactory.CreateLINT();
			_LowerLimit = Vortex.Connector.IConnectorFactory.CreateLINT();
			_CountUp_InvokeTrigCounter = Vortex.Connector.IConnectorFactory.CreateLINT();
			_CountUp_DoneTrigCounter = Vortex.Connector.IConnectorFactory.CreateLINT();
			_CountDown_InvokeTrigCounter = Vortex.Connector.IConnectorFactory.CreateLINT();
			_CountDown_DoneTrigCounter = Vortex.Connector.IConnectorFactory.CreateLINT();
			_ResetToZero_InvokeTrigCounter = Vortex.Connector.IConnectorFactory.CreateLINT();
			_ResetToZero_DoneTrigCounter = Vortex.Connector.IConnectorFactory.CreateLINT();
			_CountUp_Execute = Vortex.Connector.IConnectorFactory.CreateBOOL();
			_CountDown_Execute = Vortex.Connector.IConnectorFactory.CreateBOOL();
			_ResetToZero_Execute = Vortex.Connector.IConnectorFactory.CreateBOOL();
			_CountUp_Busy = Vortex.Connector.IConnectorFactory.CreateBOOL();
			_CountDown_Busy = Vortex.Connector.IConnectorFactory.CreateBOOL();
			_ResetToZero_Busy = Vortex.Connector.IConnectorFactory.CreateBOOL();
			_CountUp_Done = Vortex.Connector.IConnectorFactory.CreateBOOL();
			_CountDown_Done = Vortex.Connector.IConnectorFactory.CreateBOOL();
			_ResetToZero_Done = Vortex.Connector.IConnectorFactory.CreateBOOL();
			_Cycle = Vortex.Connector.IConnectorFactory.CreateLINT();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoObject_Counter
		{
			public object CounterValue;
			public object UpperLimit;
			public object LowerLimit;
			public object TcoTask_CountUp;
			public object TcoTask_CountDown;
			public object TcoTask_ResetToZero;
			public object CountUp_InvokeTrigCounter;
			public object CountUp_DoneTrigCounter;
			public object CountDown_InvokeTrigCounter;
			public object CountDown_DoneTrigCounter;
			public object ResetToZero_InvokeTrigCounter;
			public object ResetToZero_DoneTrigCounter;
			public object CountUp_PreviousState;
			public object CountDown_PreviousState;
			public object ResetToZero_PreviousState;
			public object CountUp_Execute;
			public object CountDown_Execute;
			public object ResetToZero_Execute;
			public object CountUp_Busy;
			public object CountDown_Busy;
			public object ResetToZero_Busy;
			public object CountUp_Done;
			public object CountDown_Done;
			public object ResetToZero_Done;
			public object Cycle;
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoObject_Counter()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoObject_Counter : Vortex.Connector.IVortexOnlineObject
	{
		Vortex.Connector.ValueTypes.Online.IOnlineLInt CounterValue
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt UpperLimit
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt LowerLimit
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt CountUp_InvokeTrigCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt CountUp_DoneTrigCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt CountDown_InvokeTrigCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt CountDown_DoneTrigCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt ResetToZero_InvokeTrigCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt ResetToZero_DoneTrigCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool CountUp_Execute
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool CountDown_Execute
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ResetToZero_Execute
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool CountUp_Busy
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool CountDown_Busy
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ResetToZero_Busy
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool CountUp_Done
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool CountDown_Done
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ResetToZero_Done
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt Cycle
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoObject_Counter CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainTcoObject_Counter source);
		void FlushOnlineToPlain(TcoCoreTests.PlainTcoObject_Counter source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoObject_Counter : Vortex.Connector.IVortexShadowObject
	{
		Vortex.Connector.ValueTypes.Shadows.IShadowLInt CounterValue
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt UpperLimit
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt LowerLimit
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt CountUp_InvokeTrigCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt CountUp_DoneTrigCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt CountDown_InvokeTrigCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt CountDown_DoneTrigCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt ResetToZero_InvokeTrigCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt ResetToZero_DoneTrigCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool CountUp_Execute
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool CountDown_Execute
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool ResetToZero_Execute
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool CountUp_Busy
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool CountDown_Busy
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool ResetToZero_Busy
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool CountUp_Done
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool CountDown_Done
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool ResetToZero_Done
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt Cycle
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoObject_Counter CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainTcoObject_Counter source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoObject_Counter : Vortex.Connector.IPlain
	{
		System.Int64 _CounterValue;
		public System.Int64 CounterValue
		{
			get
			{
				return _CounterValue;
			}

			set
			{
				if (_CounterValue != value)
				{
					_CounterValue = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(CounterValue)));
				}
			}
		}

		System.Int64 _UpperLimit;
		public System.Int64 UpperLimit
		{
			get
			{
				return _UpperLimit;
			}

			set
			{
				if (_UpperLimit != value)
				{
					_UpperLimit = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(UpperLimit)));
				}
			}
		}

		System.Int64 _LowerLimit;
		public System.Int64 LowerLimit
		{
			get
			{
				return _LowerLimit;
			}

			set
			{
				if (_LowerLimit != value)
				{
					_LowerLimit = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(LowerLimit)));
				}
			}
		}

		System.Int64 _CountUp_InvokeTrigCounter;
		public System.Int64 CountUp_InvokeTrigCounter
		{
			get
			{
				return _CountUp_InvokeTrigCounter;
			}

			set
			{
				if (_CountUp_InvokeTrigCounter != value)
				{
					_CountUp_InvokeTrigCounter = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(CountUp_InvokeTrigCounter)));
				}
			}
		}

		System.Int64 _CountUp_DoneTrigCounter;
		public System.Int64 CountUp_DoneTrigCounter
		{
			get
			{
				return _CountUp_DoneTrigCounter;
			}

			set
			{
				if (_CountUp_DoneTrigCounter != value)
				{
					_CountUp_DoneTrigCounter = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(CountUp_DoneTrigCounter)));
				}
			}
		}

		System.Int64 _CountDown_InvokeTrigCounter;
		public System.Int64 CountDown_InvokeTrigCounter
		{
			get
			{
				return _CountDown_InvokeTrigCounter;
			}

			set
			{
				if (_CountDown_InvokeTrigCounter != value)
				{
					_CountDown_InvokeTrigCounter = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(CountDown_InvokeTrigCounter)));
				}
			}
		}

		System.Int64 _CountDown_DoneTrigCounter;
		public System.Int64 CountDown_DoneTrigCounter
		{
			get
			{
				return _CountDown_DoneTrigCounter;
			}

			set
			{
				if (_CountDown_DoneTrigCounter != value)
				{
					_CountDown_DoneTrigCounter = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(CountDown_DoneTrigCounter)));
				}
			}
		}

		System.Int64 _ResetToZero_InvokeTrigCounter;
		public System.Int64 ResetToZero_InvokeTrigCounter
		{
			get
			{
				return _ResetToZero_InvokeTrigCounter;
			}

			set
			{
				if (_ResetToZero_InvokeTrigCounter != value)
				{
					_ResetToZero_InvokeTrigCounter = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(ResetToZero_InvokeTrigCounter)));
				}
			}
		}

		System.Int64 _ResetToZero_DoneTrigCounter;
		public System.Int64 ResetToZero_DoneTrigCounter
		{
			get
			{
				return _ResetToZero_DoneTrigCounter;
			}

			set
			{
				if (_ResetToZero_DoneTrigCounter != value)
				{
					_ResetToZero_DoneTrigCounter = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(ResetToZero_DoneTrigCounter)));
				}
			}
		}

		System.Boolean _CountUp_Execute;
		public System.Boolean CountUp_Execute
		{
			get
			{
				return _CountUp_Execute;
			}

			set
			{
				if (_CountUp_Execute != value)
				{
					_CountUp_Execute = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(CountUp_Execute)));
				}
			}
		}

		System.Boolean _CountDown_Execute;
		public System.Boolean CountDown_Execute
		{
			get
			{
				return _CountDown_Execute;
			}

			set
			{
				if (_CountDown_Execute != value)
				{
					_CountDown_Execute = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(CountDown_Execute)));
				}
			}
		}

		System.Boolean _ResetToZero_Execute;
		public System.Boolean ResetToZero_Execute
		{
			get
			{
				return _ResetToZero_Execute;
			}

			set
			{
				if (_ResetToZero_Execute != value)
				{
					_ResetToZero_Execute = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(ResetToZero_Execute)));
				}
			}
		}

		System.Boolean _CountUp_Busy;
		public System.Boolean CountUp_Busy
		{
			get
			{
				return _CountUp_Busy;
			}

			set
			{
				if (_CountUp_Busy != value)
				{
					_CountUp_Busy = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(CountUp_Busy)));
				}
			}
		}

		System.Boolean _CountDown_Busy;
		public System.Boolean CountDown_Busy
		{
			get
			{
				return _CountDown_Busy;
			}

			set
			{
				if (_CountDown_Busy != value)
				{
					_CountDown_Busy = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(CountDown_Busy)));
				}
			}
		}

		System.Boolean _ResetToZero_Busy;
		public System.Boolean ResetToZero_Busy
		{
			get
			{
				return _ResetToZero_Busy;
			}

			set
			{
				if (_ResetToZero_Busy != value)
				{
					_ResetToZero_Busy = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(ResetToZero_Busy)));
				}
			}
		}

		System.Boolean _CountUp_Done;
		public System.Boolean CountUp_Done
		{
			get
			{
				return _CountUp_Done;
			}

			set
			{
				if (_CountUp_Done != value)
				{
					_CountUp_Done = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(CountUp_Done)));
				}
			}
		}

		System.Boolean _CountDown_Done;
		public System.Boolean CountDown_Done
		{
			get
			{
				return _CountDown_Done;
			}

			set
			{
				if (_CountDown_Done != value)
				{
					_CountDown_Done = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(CountDown_Done)));
				}
			}
		}

		System.Boolean _ResetToZero_Done;
		public System.Boolean ResetToZero_Done
		{
			get
			{
				return _ResetToZero_Done;
			}

			set
			{
				if (_ResetToZero_Done != value)
				{
					_ResetToZero_Done = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(ResetToZero_Done)));
				}
			}
		}

		System.Int64 _Cycle;
		public System.Int64 Cycle
		{
			get
			{
				return _Cycle;
			}

			set
			{
				if (_Cycle != value)
				{
					_Cycle = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Cycle)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCoreTests.TcoObject_Counter target)
		{
			target.CounterValue.Cyclic = CounterValue;
			target.UpperLimit.Cyclic = UpperLimit;
			target.LowerLimit.Cyclic = LowerLimit;
			target.CountUp_InvokeTrigCounter.Cyclic = CountUp_InvokeTrigCounter;
			target.CountUp_DoneTrigCounter.Cyclic = CountUp_DoneTrigCounter;
			target.CountDown_InvokeTrigCounter.Cyclic = CountDown_InvokeTrigCounter;
			target.CountDown_DoneTrigCounter.Cyclic = CountDown_DoneTrigCounter;
			target.ResetToZero_InvokeTrigCounter.Cyclic = ResetToZero_InvokeTrigCounter;
			target.ResetToZero_DoneTrigCounter.Cyclic = ResetToZero_DoneTrigCounter;
			target.CountUp_Execute.Cyclic = CountUp_Execute;
			target.CountDown_Execute.Cyclic = CountDown_Execute;
			target.ResetToZero_Execute.Cyclic = ResetToZero_Execute;
			target.CountUp_Busy.Cyclic = CountUp_Busy;
			target.CountDown_Busy.Cyclic = CountDown_Busy;
			target.ResetToZero_Busy.Cyclic = ResetToZero_Busy;
			target.CountUp_Done.Cyclic = CountUp_Done;
			target.CountDown_Done.Cyclic = CountDown_Done;
			target.ResetToZero_Done.Cyclic = ResetToZero_Done;
			target.Cycle.Cyclic = Cycle;
		}

		public void CopyPlainToCyclic(TcoCoreTests.ITcoObject_Counter target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.TcoObject_Counter)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.TcoObject_Counter target)
		{
			target.CounterValue.Shadow = CounterValue;
			target.UpperLimit.Shadow = UpperLimit;
			target.LowerLimit.Shadow = LowerLimit;
			target.CountUp_InvokeTrigCounter.Shadow = CountUp_InvokeTrigCounter;
			target.CountUp_DoneTrigCounter.Shadow = CountUp_DoneTrigCounter;
			target.CountDown_InvokeTrigCounter.Shadow = CountDown_InvokeTrigCounter;
			target.CountDown_DoneTrigCounter.Shadow = CountDown_DoneTrigCounter;
			target.ResetToZero_InvokeTrigCounter.Shadow = ResetToZero_InvokeTrigCounter;
			target.ResetToZero_DoneTrigCounter.Shadow = ResetToZero_DoneTrigCounter;
			target.CountUp_Execute.Shadow = CountUp_Execute;
			target.CountDown_Execute.Shadow = CountDown_Execute;
			target.ResetToZero_Execute.Shadow = ResetToZero_Execute;
			target.CountUp_Busy.Shadow = CountUp_Busy;
			target.CountDown_Busy.Shadow = CountDown_Busy;
			target.ResetToZero_Busy.Shadow = ResetToZero_Busy;
			target.CountUp_Done.Shadow = CountUp_Done;
			target.CountDown_Done.Shadow = CountDown_Done;
			target.ResetToZero_Done.Shadow = ResetToZero_Done;
			target.Cycle.Shadow = Cycle;
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowTcoObject_Counter target)
		{
			this.CopyPlainToShadow((TcoCoreTests.TcoObject_Counter)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.TcoObject_Counter source)
		{
			CounterValue = source.CounterValue.LastValue;
			UpperLimit = source.UpperLimit.LastValue;
			LowerLimit = source.LowerLimit.LastValue;
			CountUp_InvokeTrigCounter = source.CountUp_InvokeTrigCounter.LastValue;
			CountUp_DoneTrigCounter = source.CountUp_DoneTrigCounter.LastValue;
			CountDown_InvokeTrigCounter = source.CountDown_InvokeTrigCounter.LastValue;
			CountDown_DoneTrigCounter = source.CountDown_DoneTrigCounter.LastValue;
			ResetToZero_InvokeTrigCounter = source.ResetToZero_InvokeTrigCounter.LastValue;
			ResetToZero_DoneTrigCounter = source.ResetToZero_DoneTrigCounter.LastValue;
			CountUp_Execute = source.CountUp_Execute.LastValue;
			CountDown_Execute = source.CountDown_Execute.LastValue;
			ResetToZero_Execute = source.ResetToZero_Execute.LastValue;
			CountUp_Busy = source.CountUp_Busy.LastValue;
			CountDown_Busy = source.CountDown_Busy.LastValue;
			ResetToZero_Busy = source.ResetToZero_Busy.LastValue;
			CountUp_Done = source.CountUp_Done.LastValue;
			CountDown_Done = source.CountDown_Done.LastValue;
			ResetToZero_Done = source.ResetToZero_Done.LastValue;
			Cycle = source.Cycle.LastValue;
		}

		public void CopyCyclicToPlain(TcoCoreTests.ITcoObject_Counter source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.TcoObject_Counter)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.TcoObject_Counter source)
		{
			CounterValue = source.CounterValue.Shadow;
			UpperLimit = source.UpperLimit.Shadow;
			LowerLimit = source.LowerLimit.Shadow;
			CountUp_InvokeTrigCounter = source.CountUp_InvokeTrigCounter.Shadow;
			CountUp_DoneTrigCounter = source.CountUp_DoneTrigCounter.Shadow;
			CountDown_InvokeTrigCounter = source.CountDown_InvokeTrigCounter.Shadow;
			CountDown_DoneTrigCounter = source.CountDown_DoneTrigCounter.Shadow;
			ResetToZero_InvokeTrigCounter = source.ResetToZero_InvokeTrigCounter.Shadow;
			ResetToZero_DoneTrigCounter = source.ResetToZero_DoneTrigCounter.Shadow;
			CountUp_Execute = source.CountUp_Execute.Shadow;
			CountDown_Execute = source.CountDown_Execute.Shadow;
			ResetToZero_Execute = source.ResetToZero_Execute.Shadow;
			CountUp_Busy = source.CountUp_Busy.Shadow;
			CountDown_Busy = source.CountDown_Busy.Shadow;
			ResetToZero_Busy = source.ResetToZero_Busy.Shadow;
			CountUp_Done = source.CountUp_Done.Shadow;
			CountDown_Done = source.CountDown_Done.Shadow;
			ResetToZero_Done = source.ResetToZero_Done.Shadow;
			Cycle = source.Cycle.Shadow;
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowTcoObject_Counter source)
		{
			this.CopyShadowToPlain((TcoCoreTests.TcoObject_Counter)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoObject_Counter()
		{
		}
	}
}