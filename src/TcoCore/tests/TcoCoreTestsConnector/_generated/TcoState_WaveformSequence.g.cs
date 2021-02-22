using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoState_WaveformSequence", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class TcoState_WaveformSequence : Vortex.Connector.IVortexObject, ITcoState_WaveformSequence, IShadowTcoState_WaveformSequence, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		TcoTask_Transition __TcoTask_Transition_1;
		public TcoTask_Transition _TcoTask_Transition_1
		{
			get
			{
				return __TcoTask_Transition_1;
			}
		}

		ITcoTask_Transition ITcoState_WaveformSequence._TcoTask_Transition_1
		{
			get
			{
				return _TcoTask_Transition_1;
			}
		}

		IShadowTcoTask_Transition IShadowTcoState_WaveformSequence._TcoTask_Transition_1
		{
			get
			{
				return _TcoTask_Transition_1;
			}
		}

		TcoTask_Transition __TcoTask_Transition_2;
		public TcoTask_Transition _TcoTask_Transition_2
		{
			get
			{
				return __TcoTask_Transition_2;
			}
		}

		ITcoTask_Transition ITcoState_WaveformSequence._TcoTask_Transition_2
		{
			get
			{
				return _TcoTask_Transition_2;
			}
		}

		IShadowTcoTask_Transition IShadowTcoState_WaveformSequence._TcoTask_Transition_2
		{
			get
			{
				return _TcoTask_Transition_2;
			}
		}

		public WaveformPoint[] _WaveformPointTable
		{
			get;
			set;
		}

		IWaveformPoint[] ITcoState_WaveformSequence._WaveformPointTable
		{
			get
			{
				return _WaveformPointTable;
			}

			set
			{
				_WaveformPointTable = (WaveformPoint[])value;
			}
		}

		IShadowWaveformPoint[] IShadowTcoState_WaveformSequence._WaveformPointTable
		{
			get
			{
				return _WaveformPointTable;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerUInt __WaveformPointsCount;
		public Vortex.Connector.ValueTypes.OnlinerUInt _WaveformPointsCount
		{
			get
			{
				return __WaveformPointsCount;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineUInt ITcoState_WaveformSequence._WaveformPointsCount
		{
			get
			{
				return _WaveformPointsCount;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowUInt IShadowTcoState_WaveformSequence._WaveformPointsCount
		{
			get
			{
				return _WaveformPointsCount;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerUInt __ActualTransitionNumber;
		public Vortex.Connector.ValueTypes.OnlinerUInt _ActualTransitionNumber
		{
			get
			{
				return __ActualTransitionNumber;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineUInt ITcoState_WaveformSequence._ActualTransitionNumber
		{
			get
			{
				return _ActualTransitionNumber;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowUInt IShadowTcoState_WaveformSequence._ActualTransitionNumber
		{
			get
			{
				return _ActualTransitionNumber;
			}
		}

		public void LazyOnlineToShadow()
		{
			_TcoTask_Transition_1.LazyOnlineToShadow();
			_TcoTask_Transition_2.LazyOnlineToShadow();
			Vortex.Connector.BuilderHelpers.Arrays.CopyCyclicToShadowComplex<WaveformPoint>(_WaveformPointTable);
			_WaveformPointsCount.Shadow = _WaveformPointsCount.LastValue;
			_ActualTransitionNumber.Shadow = _ActualTransitionNumber.LastValue;
		}

		public void LazyShadowToOnline()
		{
			_TcoTask_Transition_1.LazyShadowToOnline();
			_TcoTask_Transition_2.LazyShadowToOnline();
			Vortex.Connector.BuilderHelpers.Arrays.CopyShadowToCyclicComplex<WaveformPoint>(_WaveformPointTable);
			_WaveformPointsCount.Cyclic = _WaveformPointsCount.Shadow;
			_ActualTransitionNumber.Cyclic = _ActualTransitionNumber.Shadow;
		}

		public PlainTcoState_WaveformSequence CreatePlainerType()
		{
			var cloned = new PlainTcoState_WaveformSequence();
			cloned._TcoTask_Transition_1 = _TcoTask_Transition_1.CreatePlainerType();
			cloned._TcoTask_Transition_2 = _TcoTask_Transition_2.CreatePlainerType();
			cloned._WaveformPointTable = new PlainWaveformPoint[11];
			Vortex.Connector.BuilderHelpers.Arrays.CreatePlainerType<PlainWaveformPoint>(cloned._WaveformPointTable);
			return cloned;
		}

		protected PlainTcoState_WaveformSequence CreatePlainerType(PlainTcoState_WaveformSequence cloned)
		{
			cloned._TcoTask_Transition_1 = _TcoTask_Transition_1.CreatePlainerType();
			cloned._TcoTask_Transition_2 = _TcoTask_Transition_2.CreatePlainerType();
			cloned._WaveformPointTable = new PlainWaveformPoint[11];
			Vortex.Connector.BuilderHelpers.Arrays.CreatePlainerType<PlainWaveformPoint>(cloned._WaveformPointTable);
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

		public void FlushPlainToOnline(TcoCoreTests.PlainTcoState_WaveformSequence source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainTcoState_WaveformSequence source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainTcoState_WaveformSequence source)
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

		public TcoState_WaveformSequence(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			__TcoTask_Transition_1 = new TcoTask_Transition(this, "", "_TcoTask_Transition_1");
			__TcoTask_Transition_2 = new TcoTask_Transition(this, "", "_TcoTask_Transition_2");
			_WaveformPointTable = new WaveformPoint[11];
			Vortex.Connector.BuilderHelpers.Arrays.InstantiateArray(_WaveformPointTable, this, "", "_WaveformPointTable", (p, rt, st) => new WaveformPoint(p, rt, st));
			__WaveformPointsCount = @Connector.Online.Adapter.CreateUINT(this, "", "_WaveformPointsCount");
			__ActualTransitionNumber = @Connector.Online.Adapter.CreateUINT(this, "", "_ActualTransitionNumber");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public TcoState_WaveformSequence()
		{
			PexPreConstructorParameterless();
			__TcoTask_Transition_1 = new TcoTask_Transition();
			__TcoTask_Transition_2 = new TcoTask_Transition();
			_WaveformPointTable = new WaveformPoint[11];
			Vortex.Connector.BuilderHelpers.Arrays.InstantiateArray(_WaveformPointTable, () => new WaveformPoint());
			__WaveformPointsCount = Vortex.Connector.IConnectorFactory.CreateUINT();
			__ActualTransitionNumber = Vortex.Connector.IConnectorFactory.CreateUINT();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoState_WaveformSequence
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoState_WaveformSequence()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoState_WaveformSequence : Vortex.Connector.IVortexOnlineObject
	{
		ITcoTask_Transition _TcoTask_Transition_1
		{
			get;
		}

		ITcoTask_Transition _TcoTask_Transition_2
		{
			get;
		}

		IWaveformPoint[] _WaveformPointTable
		{
			get;
			set;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineUInt _WaveformPointsCount
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineUInt _ActualTransitionNumber
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoState_WaveformSequence CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainTcoState_WaveformSequence source);
		void FlushOnlineToPlain(TcoCoreTests.PlainTcoState_WaveformSequence source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoState_WaveformSequence : Vortex.Connector.IVortexShadowObject
	{
		IShadowTcoTask_Transition _TcoTask_Transition_1
		{
			get;
		}

		IShadowTcoTask_Transition _TcoTask_Transition_2
		{
			get;
		}

		IShadowWaveformPoint[] _WaveformPointTable
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowUInt _WaveformPointsCount
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowUInt _ActualTransitionNumber
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoState_WaveformSequence CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainTcoState_WaveformSequence source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoState_WaveformSequence : Vortex.Connector.IPlain
	{
		PlainTcoTask_Transition __TcoTask_Transition_1;
		public PlainTcoTask_Transition _TcoTask_Transition_1
		{
			get
			{
				return __TcoTask_Transition_1;
			}

			set
			{
				if (__TcoTask_Transition_1 != value)
				{
					__TcoTask_Transition_1 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_TcoTask_Transition_1)));
				}
			}
		}

		PlainTcoTask_Transition __TcoTask_Transition_2;
		public PlainTcoTask_Transition _TcoTask_Transition_2
		{
			get
			{
				return __TcoTask_Transition_2;
			}

			set
			{
				if (__TcoTask_Transition_2 != value)
				{
					__TcoTask_Transition_2 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_TcoTask_Transition_2)));
				}
			}
		}

		public PlainWaveformPoint[] _WaveformPointTable
		{
			get;
			set;
		}

		System.UInt16 __WaveformPointsCount;
		public System.UInt16 _WaveformPointsCount
		{
			get
			{
				return __WaveformPointsCount;
			}

			set
			{
				if (__WaveformPointsCount != value)
				{
					__WaveformPointsCount = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_WaveformPointsCount)));
				}
			}
		}

		System.UInt16 __ActualTransitionNumber;
		public System.UInt16 _ActualTransitionNumber
		{
			get
			{
				return __ActualTransitionNumber;
			}

			set
			{
				if (__ActualTransitionNumber != value)
				{
					__ActualTransitionNumber = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_ActualTransitionNumber)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCoreTests.TcoState_WaveformSequence target)
		{
			_TcoTask_Transition_1.CopyPlainToCyclic(target._TcoTask_Transition_1);
			_TcoTask_Transition_2.CopyPlainToCyclic(target._TcoTask_Transition_2);
			Vortex.Connector.BuilderHelpers.Arrays.CopyPlainToOnline<PlainWaveformPoint, WaveformPoint>(_WaveformPointTable, target._WaveformPointTable);
			target._WaveformPointsCount.Cyclic = _WaveformPointsCount;
			target._ActualTransitionNumber.Cyclic = _ActualTransitionNumber;
		}

		public void CopyPlainToCyclic(TcoCoreTests.ITcoState_WaveformSequence target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.TcoState_WaveformSequence)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.TcoState_WaveformSequence target)
		{
			_TcoTask_Transition_1.CopyPlainToShadow(target._TcoTask_Transition_1);
			_TcoTask_Transition_2.CopyPlainToShadow(target._TcoTask_Transition_2);
			Vortex.Connector.BuilderHelpers.Arrays.CopyPlainToShadow<PlainWaveformPoint, WaveformPoint>(_WaveformPointTable, target._WaveformPointTable);
			target._WaveformPointsCount.Shadow = _WaveformPointsCount;
			target._ActualTransitionNumber.Shadow = _ActualTransitionNumber;
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowTcoState_WaveformSequence target)
		{
			this.CopyPlainToShadow((TcoCoreTests.TcoState_WaveformSequence)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.TcoState_WaveformSequence source)
		{
			_TcoTask_Transition_1.CopyCyclicToPlain(source._TcoTask_Transition_1);
			_TcoTask_Transition_2.CopyCyclicToPlain(source._TcoTask_Transition_2);
			Vortex.Connector.BuilderHelpers.Arrays.CopyOnlineToPlain<WaveformPoint, PlainWaveformPoint>(source._WaveformPointTable, _WaveformPointTable);
			_WaveformPointsCount = source._WaveformPointsCount.LastValue;
			_ActualTransitionNumber = source._ActualTransitionNumber.LastValue;
		}

		public void CopyCyclicToPlain(TcoCoreTests.ITcoState_WaveformSequence source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.TcoState_WaveformSequence)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.TcoState_WaveformSequence source)
		{
			_TcoTask_Transition_1.CopyShadowToPlain(source._TcoTask_Transition_1);
			_TcoTask_Transition_2.CopyShadowToPlain(source._TcoTask_Transition_2);
			Vortex.Connector.BuilderHelpers.Arrays.CopyShadowToPlain<WaveformPoint, PlainWaveformPoint>(source._WaveformPointTable, _WaveformPointTable);
			_WaveformPointsCount = source._WaveformPointsCount.Shadow;
			_ActualTransitionNumber = source._ActualTransitionNumber.Shadow;
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowTcoState_WaveformSequence source)
		{
			this.CopyShadowToPlain((TcoCoreTests.TcoState_WaveformSequence)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoState_WaveformSequence()
		{
			__TcoTask_Transition_1 = new PlainTcoTask_Transition();
			__TcoTask_Transition_2 = new PlainTcoTask_Transition();
			_WaveformPointTable = new PlainWaveformPoint[11];
			Vortex.Connector.BuilderHelpers.Arrays.InstantiatePlainerType<PlainWaveformPoint>(_WaveformPointTable);
		}
	}
}