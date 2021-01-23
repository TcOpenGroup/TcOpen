using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoObject_Waveform", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class TcoObject_Waveform : Vortex.Connector.IVortexObject, ITcoObject_Waveform, IShadowTcoObject_Waveform, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		TcoState_WaveformSequence __TcoState_WaveformSequence_1;
		public TcoState_WaveformSequence _TcoState_WaveformSequence_1
		{
			get
			{
				return __TcoState_WaveformSequence_1;
			}
		}

		ITcoState_WaveformSequence ITcoObject_Waveform._TcoState_WaveformSequence_1
		{
			get
			{
				return _TcoState_WaveformSequence_1;
			}
		}

		IShadowTcoState_WaveformSequence IShadowTcoObject_Waveform._TcoState_WaveformSequence_1
		{
			get
			{
				return _TcoState_WaveformSequence_1;
			}
		}

		TcoState_WaveformSequence __TcoState_WaveformSequence_2;
		public TcoState_WaveformSequence _TcoState_WaveformSequence_2
		{
			get
			{
				return __TcoState_WaveformSequence_2;
			}
		}

		ITcoState_WaveformSequence ITcoObject_Waveform._TcoState_WaveformSequence_2
		{
			get
			{
				return _TcoState_WaveformSequence_2;
			}
		}

		IShadowTcoState_WaveformSequence IShadowTcoObject_Waveform._TcoState_WaveformSequence_2
		{
			get
			{
				return _TcoState_WaveformSequence_2;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLReal __Value1;
		public Vortex.Connector.ValueTypes.OnlinerLReal _Value1
		{
			get
			{
				return __Value1;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal ITcoObject_Waveform._Value1
		{
			get
			{
				return _Value1;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal IShadowTcoObject_Waveform._Value1
		{
			get
			{
				return _Value1;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLReal __Value2;
		public Vortex.Connector.ValueTypes.OnlinerLReal _Value2
		{
			get
			{
				return __Value2;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal ITcoObject_Waveform._Value2
		{
			get
			{
				return _Value2;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal IShadowTcoObject_Waveform._Value2
		{
			get
			{
				return _Value2;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLReal __Value;
		public Vortex.Connector.ValueTypes.OnlinerLReal _Value
		{
			get
			{
				return __Value;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal ITcoObject_Waveform._Value
		{
			get
			{
				return _Value;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal IShadowTcoObject_Waveform._Value
		{
			get
			{
				return _Value;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __X;
		public Vortex.Connector.ValueTypes.OnlinerULInt _X
		{
			get
			{
				return __X;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoObject_Waveform._X
		{
			get
			{
				return _X;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoObject_Waveform._X
		{
			get
			{
				return _X;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __PlcCycleOffset;
		public Vortex.Connector.ValueTypes.OnlinerULInt _PlcCycleOffset
		{
			get
			{
				return __PlcCycleOffset;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoObject_Waveform._PlcCycleOffset
		{
			get
			{
				return _PlcCycleOffset;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoObject_Waveform._PlcCycleOffset
		{
			get
			{
				return _PlcCycleOffset;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __PlcCycle;
		public Vortex.Connector.ValueTypes.OnlinerULInt _PlcCycle
		{
			get
			{
				return __PlcCycle;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoObject_Waveform._PlcCycle
		{
			get
			{
				return _PlcCycle;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoObject_Waveform._PlcCycle
		{
			get
			{
				return _PlcCycle;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool __Reset;
		public Vortex.Connector.ValueTypes.OnlinerBool _Reset
		{
			get
			{
				return __Reset;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoObject_Waveform._Reset
		{
			get
			{
				return _Reset;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoObject_Waveform._Reset
		{
			get
			{
				return _Reset;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool __Start;
		public Vortex.Connector.ValueTypes.OnlinerBool _Start
		{
			get
			{
				return __Start;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoObject_Waveform._Start
		{
			get
			{
				return _Start;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoObject_Waveform._Start
		{
			get
			{
				return _Start;
			}
		}

		public void LazyOnlineToShadow()
		{
			_TcoState_WaveformSequence_1.LazyOnlineToShadow();
			_TcoState_WaveformSequence_2.LazyOnlineToShadow();
			_Value1.Shadow = _Value1.LastValue;
			_Value2.Shadow = _Value2.LastValue;
			_Value.Shadow = _Value.LastValue;
			_X.Shadow = _X.LastValue;
			_PlcCycleOffset.Shadow = _PlcCycleOffset.LastValue;
			_PlcCycle.Shadow = _PlcCycle.LastValue;
			_Reset.Shadow = _Reset.LastValue;
			_Start.Shadow = _Start.LastValue;
		}

		public void LazyShadowToOnline()
		{
			_TcoState_WaveformSequence_1.LazyShadowToOnline();
			_TcoState_WaveformSequence_2.LazyShadowToOnline();
			_Value1.Cyclic = _Value1.Shadow;
			_Value2.Cyclic = _Value2.Shadow;
			_Value.Cyclic = _Value.Shadow;
			_X.Cyclic = _X.Shadow;
			_PlcCycleOffset.Cyclic = _PlcCycleOffset.Shadow;
			_PlcCycle.Cyclic = _PlcCycle.Shadow;
			_Reset.Cyclic = _Reset.Shadow;
			_Start.Cyclic = _Start.Shadow;
		}

		public PlainTcoObject_Waveform CreatePlainerType()
		{
			var cloned = new PlainTcoObject_Waveform();
			cloned._TcoState_WaveformSequence_1 = _TcoState_WaveformSequence_1.CreatePlainerType();
			cloned._TcoState_WaveformSequence_2 = _TcoState_WaveformSequence_2.CreatePlainerType();
			return cloned;
		}

		protected PlainTcoObject_Waveform CreatePlainerType(PlainTcoObject_Waveform cloned)
		{
			cloned._TcoState_WaveformSequence_1 = _TcoState_WaveformSequence_1.CreatePlainerType();
			cloned._TcoState_WaveformSequence_2 = _TcoState_WaveformSequence_2.CreatePlainerType();
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

		public void FlushPlainToOnline(TcoCoreTests.PlainTcoObject_Waveform source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainTcoObject_Waveform source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainTcoObject_Waveform source)
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

		public TcoObject_Waveform(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@ValueTags = new System.Collections.Generic.List<Vortex.Connector.IValueTag>();
			this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			__TcoState_WaveformSequence_1 = new TcoState_WaveformSequence(this, "", "_TcoState_WaveformSequence_1");
			__TcoState_WaveformSequence_2 = new TcoState_WaveformSequence(this, "", "_TcoState_WaveformSequence_2");
			__Value1 = @Connector.Online.Adapter.CreateLREAL(this, "", "_Value1");
			__Value2 = @Connector.Online.Adapter.CreateLREAL(this, "", "_Value2");
			__Value = @Connector.Online.Adapter.CreateLREAL(this, "", "_Value");
			__X = @Connector.Online.Adapter.CreateULINT(this, "", "_X");
			__PlcCycleOffset = @Connector.Online.Adapter.CreateULINT(this, "", "_PlcCycleOffset");
			__PlcCycle = @Connector.Online.Adapter.CreateULINT(this, "", "_PlcCycle");
			__Reset = @Connector.Online.Adapter.CreateBOOL(this, "", "_Reset");
			__Start = @Connector.Online.Adapter.CreateBOOL(this, "", "_Start");
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
			parent.AddChild(this);
		}

		public TcoObject_Waveform()
		{
			PexPreConstructorParameterless();
			__TcoState_WaveformSequence_1 = new TcoState_WaveformSequence();
			__TcoState_WaveformSequence_2 = new TcoState_WaveformSequence();
			__Value1 = Vortex.Connector.IConnectorFactory.CreateLREAL();
			__Value2 = Vortex.Connector.IConnectorFactory.CreateLREAL();
			__Value = Vortex.Connector.IConnectorFactory.CreateLREAL();
			__X = Vortex.Connector.IConnectorFactory.CreateULINT();
			__PlcCycleOffset = Vortex.Connector.IConnectorFactory.CreateULINT();
			__PlcCycle = Vortex.Connector.IConnectorFactory.CreateULINT();
			__Reset = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__Start = Vortex.Connector.IConnectorFactory.CreateBOOL();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoObject_Waveform
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoObject_Waveform()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoObject_Waveform : Vortex.Connector.IVortexOnlineObject
	{
		ITcoState_WaveformSequence _TcoState_WaveformSequence_1
		{
			get;
		}

		ITcoState_WaveformSequence _TcoState_WaveformSequence_2
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal _Value1
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal _Value2
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal _Value
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _X
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _PlcCycleOffset
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _PlcCycle
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool _Reset
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool _Start
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoObject_Waveform CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainTcoObject_Waveform source);
		void FlushOnlineToPlain(TcoCoreTests.PlainTcoObject_Waveform source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoObject_Waveform : Vortex.Connector.IVortexShadowObject
	{
		IShadowTcoState_WaveformSequence _TcoState_WaveformSequence_1
		{
			get;
		}

		IShadowTcoState_WaveformSequence _TcoState_WaveformSequence_2
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal _Value1
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal _Value2
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal _Value
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _X
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _PlcCycleOffset
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _PlcCycle
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool _Reset
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool _Start
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoObject_Waveform CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainTcoObject_Waveform source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoObject_Waveform : Vortex.Connector.IPlain
	{
		PlainTcoState_WaveformSequence __TcoState_WaveformSequence_1;
		public PlainTcoState_WaveformSequence _TcoState_WaveformSequence_1
		{
			get
			{
				return __TcoState_WaveformSequence_1;
			}

			set
			{
				if (__TcoState_WaveformSequence_1 != value)
				{
					__TcoState_WaveformSequence_1 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_TcoState_WaveformSequence_1)));
				}
			}
		}

		PlainTcoState_WaveformSequence __TcoState_WaveformSequence_2;
		public PlainTcoState_WaveformSequence _TcoState_WaveformSequence_2
		{
			get
			{
				return __TcoState_WaveformSequence_2;
			}

			set
			{
				if (__TcoState_WaveformSequence_2 != value)
				{
					__TcoState_WaveformSequence_2 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_TcoState_WaveformSequence_2)));
				}
			}
		}

		System.Double __Value1;
		public System.Double _Value1
		{
			get
			{
				return __Value1;
			}

			set
			{
				if (__Value1 != value)
				{
					__Value1 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_Value1)));
				}
			}
		}

		System.Double __Value2;
		public System.Double _Value2
		{
			get
			{
				return __Value2;
			}

			set
			{
				if (__Value2 != value)
				{
					__Value2 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_Value2)));
				}
			}
		}

		System.Double __Value;
		public System.Double _Value
		{
			get
			{
				return __Value;
			}

			set
			{
				if (__Value != value)
				{
					__Value = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_Value)));
				}
			}
		}

		System.UInt64 __X;
		public System.UInt64 _X
		{
			get
			{
				return __X;
			}

			set
			{
				if (__X != value)
				{
					__X = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_X)));
				}
			}
		}

		System.UInt64 __PlcCycleOffset;
		public System.UInt64 _PlcCycleOffset
		{
			get
			{
				return __PlcCycleOffset;
			}

			set
			{
				if (__PlcCycleOffset != value)
				{
					__PlcCycleOffset = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_PlcCycleOffset)));
				}
			}
		}

		System.UInt64 __PlcCycle;
		public System.UInt64 _PlcCycle
		{
			get
			{
				return __PlcCycle;
			}

			set
			{
				if (__PlcCycle != value)
				{
					__PlcCycle = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_PlcCycle)));
				}
			}
		}

		System.Boolean __Reset;
		public System.Boolean _Reset
		{
			get
			{
				return __Reset;
			}

			set
			{
				if (__Reset != value)
				{
					__Reset = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_Reset)));
				}
			}
		}

		System.Boolean __Start;
		public System.Boolean _Start
		{
			get
			{
				return __Start;
			}

			set
			{
				if (__Start != value)
				{
					__Start = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_Start)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCoreTests.TcoObject_Waveform target)
		{
			_TcoState_WaveformSequence_1.CopyPlainToCyclic(target._TcoState_WaveformSequence_1);
			_TcoState_WaveformSequence_2.CopyPlainToCyclic(target._TcoState_WaveformSequence_2);
			target._Value1.Cyclic = _Value1;
			target._Value2.Cyclic = _Value2;
			target._Value.Cyclic = _Value;
			target._X.Cyclic = _X;
			target._PlcCycleOffset.Cyclic = _PlcCycleOffset;
			target._PlcCycle.Cyclic = _PlcCycle;
			target._Reset.Cyclic = _Reset;
			target._Start.Cyclic = _Start;
		}

		public void CopyPlainToCyclic(TcoCoreTests.ITcoObject_Waveform target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.TcoObject_Waveform)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.TcoObject_Waveform target)
		{
			_TcoState_WaveformSequence_1.CopyPlainToShadow(target._TcoState_WaveformSequence_1);
			_TcoState_WaveformSequence_2.CopyPlainToShadow(target._TcoState_WaveformSequence_2);
			target._Value1.Shadow = _Value1;
			target._Value2.Shadow = _Value2;
			target._Value.Shadow = _Value;
			target._X.Shadow = _X;
			target._PlcCycleOffset.Shadow = _PlcCycleOffset;
			target._PlcCycle.Shadow = _PlcCycle;
			target._Reset.Shadow = _Reset;
			target._Start.Shadow = _Start;
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowTcoObject_Waveform target)
		{
			this.CopyPlainToShadow((TcoCoreTests.TcoObject_Waveform)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.TcoObject_Waveform source)
		{
			_TcoState_WaveformSequence_1.CopyCyclicToPlain(source._TcoState_WaveformSequence_1);
			_TcoState_WaveformSequence_2.CopyCyclicToPlain(source._TcoState_WaveformSequence_2);
			_Value1 = source._Value1.LastValue;
			_Value2 = source._Value2.LastValue;
			_Value = source._Value.LastValue;
			_X = source._X.LastValue;
			_PlcCycleOffset = source._PlcCycleOffset.LastValue;
			_PlcCycle = source._PlcCycle.LastValue;
			_Reset = source._Reset.LastValue;
			_Start = source._Start.LastValue;
		}

		public void CopyCyclicToPlain(TcoCoreTests.ITcoObject_Waveform source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.TcoObject_Waveform)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.TcoObject_Waveform source)
		{
			_TcoState_WaveformSequence_1.CopyShadowToPlain(source._TcoState_WaveformSequence_1);
			_TcoState_WaveformSequence_2.CopyShadowToPlain(source._TcoState_WaveformSequence_2);
			_Value1 = source._Value1.Shadow;
			_Value2 = source._Value2.Shadow;
			_Value = source._Value.Shadow;
			_X = source._X.Shadow;
			_PlcCycleOffset = source._PlcCycleOffset.Shadow;
			_PlcCycle = source._PlcCycle.Shadow;
			_Reset = source._Reset.Shadow;
			_Start = source._Start.Shadow;
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowTcoObject_Waveform source)
		{
			this.CopyShadowToPlain((TcoCoreTests.TcoObject_Waveform)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoObject_Waveform()
		{
			__TcoState_WaveformSequence_1 = new PlainTcoState_WaveformSequence();
			__TcoState_WaveformSequence_2 = new PlainTcoState_WaveformSequence();
		}
	}
}