using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoTask_Transition", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class TcoTask_Transition : Vortex.Connector.IVortexObject, ITcoTask_Transition, IShadowTcoTask_Transition, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		Vortex.Connector.ValueTypes.OnlinerInt __TransitionType;
		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTransitionType))]
		public Vortex.Connector.ValueTypes.OnlinerInt _TransitionType
		{
			get
			{
				return __TransitionType;
			}
		}

		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTransitionType))]
		Vortex.Connector.ValueTypes.Online.IOnlineInt ITcoTask_Transition._TransitionType
		{
			get
			{
				return _TransitionType;
			}
		}

		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTransitionType))]
		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadowTcoTask_Transition._TransitionType
		{
			get
			{
				return _TransitionType;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLReal __k0;
		public Vortex.Connector.ValueTypes.OnlinerLReal _k0
		{
			get
			{
				return __k0;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal ITcoTask_Transition._k0
		{
			get
			{
				return _k0;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal IShadowTcoTask_Transition._k0
		{
			get
			{
				return _k0;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLReal __StartValue;
		public Vortex.Connector.ValueTypes.OnlinerLReal _StartValue
		{
			get
			{
				return __StartValue;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal ITcoTask_Transition._StartValue
		{
			get
			{
				return _StartValue;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal IShadowTcoTask_Transition._StartValue
		{
			get
			{
				return _StartValue;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLReal __TargetValue;
		public Vortex.Connector.ValueTypes.OnlinerLReal _TargetValue
		{
			get
			{
				return __TargetValue;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal ITcoTask_Transition._TargetValue
		{
			get
			{
				return _TargetValue;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal IShadowTcoTask_Transition._TargetValue
		{
			get
			{
				return _TargetValue;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerUInt __Elapsed;
		public Vortex.Connector.ValueTypes.OnlinerUInt _Elapsed
		{
			get
			{
				return __Elapsed;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineUInt ITcoTask_Transition._Elapsed
		{
			get
			{
				return _Elapsed;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowUInt IShadowTcoTask_Transition._Elapsed
		{
			get
			{
				return _Elapsed;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerUInt __TransitionDuration;
		public Vortex.Connector.ValueTypes.OnlinerUInt _TransitionDuration
		{
			get
			{
				return __TransitionDuration;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineUInt ITcoTask_Transition._TransitionDuration
		{
			get
			{
				return _TransitionDuration;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowUInt IShadowTcoTask_Transition._TransitionDuration
		{
			get
			{
				return _TransitionDuration;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLReal __t0;
		public Vortex.Connector.ValueTypes.OnlinerLReal _t0
		{
			get
			{
				return __t0;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal ITcoTask_Transition._t0
		{
			get
			{
				return _t0;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal IShadowTcoTask_Transition._t0
		{
			get
			{
				return _t0;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLReal __x0;
		public Vortex.Connector.ValueTypes.OnlinerLReal _x0
		{
			get
			{
				return __x0;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal ITcoTask_Transition._x0
		{
			get
			{
				return _x0;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal IShadowTcoTask_Transition._x0
		{
			get
			{
				return _x0;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLReal __k1;
		public Vortex.Connector.ValueTypes.OnlinerLReal _k1
		{
			get
			{
				return __k1;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal ITcoTask_Transition._k1
		{
			get
			{
				return _k1;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal IShadowTcoTask_Transition._k1
		{
			get
			{
				return _k1;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLReal __k2;
		public Vortex.Connector.ValueTypes.OnlinerLReal _k2
		{
			get
			{
				return __k2;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal ITcoTask_Transition._k2
		{
			get
			{
				return _k2;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal IShadowTcoTask_Transition._k2
		{
			get
			{
				return _k2;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLReal __k3;
		public Vortex.Connector.ValueTypes.OnlinerLReal _k3
		{
			get
			{
				return __k3;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal ITcoTask_Transition._k3
		{
			get
			{
				return _k3;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal IShadowTcoTask_Transition._k3
		{
			get
			{
				return _k3;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLReal __k4;
		public Vortex.Connector.ValueTypes.OnlinerLReal _k4
		{
			get
			{
				return __k4;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal ITcoTask_Transition._k4
		{
			get
			{
				return _k4;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal IShadowTcoTask_Transition._k4
		{
			get
			{
				return _k4;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool __ImmediateChange;
		public Vortex.Connector.ValueTypes.OnlinerBool _ImmediateChange
		{
			get
			{
				return __ImmediateChange;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoTask_Transition._ImmediateChange
		{
			get
			{
				return _ImmediateChange;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoTask_Transition._ImmediateChange
		{
			get
			{
				return _ImmediateChange;
			}
		}

		public void LazyOnlineToShadow()
		{
			_TransitionType.Shadow = _TransitionType.LastValue;
			_k0.Shadow = _k0.LastValue;
			_StartValue.Shadow = _StartValue.LastValue;
			_TargetValue.Shadow = _TargetValue.LastValue;
			_Elapsed.Shadow = _Elapsed.LastValue;
			_TransitionDuration.Shadow = _TransitionDuration.LastValue;
			_t0.Shadow = _t0.LastValue;
			_x0.Shadow = _x0.LastValue;
			_k1.Shadow = _k1.LastValue;
			_k2.Shadow = _k2.LastValue;
			_k3.Shadow = _k3.LastValue;
			_k4.Shadow = _k4.LastValue;
			_ImmediateChange.Shadow = _ImmediateChange.LastValue;
		}

		public void LazyShadowToOnline()
		{
			_TransitionType.Cyclic = _TransitionType.Shadow;
			_k0.Cyclic = _k0.Shadow;
			_StartValue.Cyclic = _StartValue.Shadow;
			_TargetValue.Cyclic = _TargetValue.Shadow;
			_Elapsed.Cyclic = _Elapsed.Shadow;
			_TransitionDuration.Cyclic = _TransitionDuration.Shadow;
			_t0.Cyclic = _t0.Shadow;
			_x0.Cyclic = _x0.Shadow;
			_k1.Cyclic = _k1.Shadow;
			_k2.Cyclic = _k2.Shadow;
			_k3.Cyclic = _k3.Shadow;
			_k4.Cyclic = _k4.Shadow;
			_ImmediateChange.Cyclic = _ImmediateChange.Shadow;
		}

		public PlainTcoTask_Transition CreatePlainerType()
		{
			var cloned = new PlainTcoTask_Transition();
			return cloned;
		}

		protected PlainTcoTask_Transition CreatePlainerType(PlainTcoTask_Transition cloned)
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

		public void FlushPlainToOnline(TcoCoreTests.PlainTcoTask_Transition source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainTcoTask_Transition source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainTcoTask_Transition source)
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

		public TcoTask_Transition(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			__TransitionType = @Connector.Online.Adapter.CreateINT(this, "", "_TransitionType");
			__k0 = @Connector.Online.Adapter.CreateLREAL(this, "", "_k0");
			__StartValue = @Connector.Online.Adapter.CreateLREAL(this, "", "_StartValue");
			__TargetValue = @Connector.Online.Adapter.CreateLREAL(this, "", "_TargetValue");
			__Elapsed = @Connector.Online.Adapter.CreateUINT(this, "", "_Elapsed");
			__TransitionDuration = @Connector.Online.Adapter.CreateUINT(this, "", "_TransitionDuration");
			__t0 = @Connector.Online.Adapter.CreateLREAL(this, "", "_t0");
			__x0 = @Connector.Online.Adapter.CreateLREAL(this, "", "_x0");
			__k1 = @Connector.Online.Adapter.CreateLREAL(this, "", "_k1");
			__k2 = @Connector.Online.Adapter.CreateLREAL(this, "", "_k2");
			__k3 = @Connector.Online.Adapter.CreateLREAL(this, "", "_k3");
			__k4 = @Connector.Online.Adapter.CreateLREAL(this, "", "_k4");
			__ImmediateChange = @Connector.Online.Adapter.CreateBOOL(this, "", "_ImmediateChange");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public TcoTask_Transition()
		{
			PexPreConstructorParameterless();
			__TransitionType = Vortex.Connector.IConnectorFactory.CreateINT();
			__k0 = Vortex.Connector.IConnectorFactory.CreateLREAL();
			__StartValue = Vortex.Connector.IConnectorFactory.CreateLREAL();
			__TargetValue = Vortex.Connector.IConnectorFactory.CreateLREAL();
			__Elapsed = Vortex.Connector.IConnectorFactory.CreateUINT();
			__TransitionDuration = Vortex.Connector.IConnectorFactory.CreateUINT();
			__t0 = Vortex.Connector.IConnectorFactory.CreateLREAL();
			__x0 = Vortex.Connector.IConnectorFactory.CreateLREAL();
			__k1 = Vortex.Connector.IConnectorFactory.CreateLREAL();
			__k2 = Vortex.Connector.IConnectorFactory.CreateLREAL();
			__k3 = Vortex.Connector.IConnectorFactory.CreateLREAL();
			__k4 = Vortex.Connector.IConnectorFactory.CreateLREAL();
			__ImmediateChange = Vortex.Connector.IConnectorFactory.CreateBOOL();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoTask_Transition
		{
			public System.Int16 _TransitionType;
			public object _k0;
			public object _StartValue;
			public object _TargetValue;
			public object _Elapsed;
			public object _TransitionDuration;
			public object _t0;
			public object _x0;
			public object _k1;
			public object _k2;
			public object _k3;
			public object _k4;
			public object _ImmediateChange;
			public object Value;
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoTask_Transition()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoTask_Transition : Vortex.Connector.IVortexOnlineObject
	{
		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTransitionType))]
		Vortex.Connector.ValueTypes.Online.IOnlineInt _TransitionType
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal _k0
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal _StartValue
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal _TargetValue
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineUInt _Elapsed
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineUInt _TransitionDuration
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal _t0
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal _x0
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal _k1
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal _k2
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal _k3
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal _k4
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool _ImmediateChange
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoTask_Transition CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainTcoTask_Transition source);
		void FlushOnlineToPlain(TcoCoreTests.PlainTcoTask_Transition source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoTask_Transition : Vortex.Connector.IVortexShadowObject
	{
		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTransitionType))]
		Vortex.Connector.ValueTypes.Shadows.IShadowInt _TransitionType
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal _k0
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal _StartValue
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal _TargetValue
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowUInt _Elapsed
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowUInt _TransitionDuration
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal _t0
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal _x0
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal _k1
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal _k2
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal _k3
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal _k4
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool _ImmediateChange
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoTask_Transition CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainTcoTask_Transition source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoTask_Transition : Vortex.Connector.IPlain
	{
		System.Int16 __TransitionType;
		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTransitionType))]
		public System.Int16 _TransitionType
		{
			get
			{
				return __TransitionType;
			}

			set
			{
				if (__TransitionType != value)
				{
					__TransitionType = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_TransitionType)));
				}
			}
		}

		System.Double __k0;
		public System.Double _k0
		{
			get
			{
				return __k0;
			}

			set
			{
				if (__k0 != value)
				{
					__k0 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_k0)));
				}
			}
		}

		System.Double __StartValue;
		public System.Double _StartValue
		{
			get
			{
				return __StartValue;
			}

			set
			{
				if (__StartValue != value)
				{
					__StartValue = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_StartValue)));
				}
			}
		}

		System.Double __TargetValue;
		public System.Double _TargetValue
		{
			get
			{
				return __TargetValue;
			}

			set
			{
				if (__TargetValue != value)
				{
					__TargetValue = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_TargetValue)));
				}
			}
		}

		System.UInt16 __Elapsed;
		public System.UInt16 _Elapsed
		{
			get
			{
				return __Elapsed;
			}

			set
			{
				if (__Elapsed != value)
				{
					__Elapsed = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_Elapsed)));
				}
			}
		}

		System.UInt16 __TransitionDuration;
		public System.UInt16 _TransitionDuration
		{
			get
			{
				return __TransitionDuration;
			}

			set
			{
				if (__TransitionDuration != value)
				{
					__TransitionDuration = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_TransitionDuration)));
				}
			}
		}

		System.Double __t0;
		public System.Double _t0
		{
			get
			{
				return __t0;
			}

			set
			{
				if (__t0 != value)
				{
					__t0 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_t0)));
				}
			}
		}

		System.Double __x0;
		public System.Double _x0
		{
			get
			{
				return __x0;
			}

			set
			{
				if (__x0 != value)
				{
					__x0 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_x0)));
				}
			}
		}

		System.Double __k1;
		public System.Double _k1
		{
			get
			{
				return __k1;
			}

			set
			{
				if (__k1 != value)
				{
					__k1 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_k1)));
				}
			}
		}

		System.Double __k2;
		public System.Double _k2
		{
			get
			{
				return __k2;
			}

			set
			{
				if (__k2 != value)
				{
					__k2 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_k2)));
				}
			}
		}

		System.Double __k3;
		public System.Double _k3
		{
			get
			{
				return __k3;
			}

			set
			{
				if (__k3 != value)
				{
					__k3 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_k3)));
				}
			}
		}

		System.Double __k4;
		public System.Double _k4
		{
			get
			{
				return __k4;
			}

			set
			{
				if (__k4 != value)
				{
					__k4 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_k4)));
				}
			}
		}

		System.Boolean __ImmediateChange;
		public System.Boolean _ImmediateChange
		{
			get
			{
				return __ImmediateChange;
			}

			set
			{
				if (__ImmediateChange != value)
				{
					__ImmediateChange = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_ImmediateChange)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCoreTests.TcoTask_Transition target)
		{
			target._TransitionType.Cyclic = _TransitionType;
			target._k0.Cyclic = _k0;
			target._StartValue.Cyclic = _StartValue;
			target._TargetValue.Cyclic = _TargetValue;
			target._Elapsed.Cyclic = _Elapsed;
			target._TransitionDuration.Cyclic = _TransitionDuration;
			target._t0.Cyclic = _t0;
			target._x0.Cyclic = _x0;
			target._k1.Cyclic = _k1;
			target._k2.Cyclic = _k2;
			target._k3.Cyclic = _k3;
			target._k4.Cyclic = _k4;
			target._ImmediateChange.Cyclic = _ImmediateChange;
		}

		public void CopyPlainToCyclic(TcoCoreTests.ITcoTask_Transition target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.TcoTask_Transition)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.TcoTask_Transition target)
		{
			target._TransitionType.Shadow = _TransitionType;
			target._k0.Shadow = _k0;
			target._StartValue.Shadow = _StartValue;
			target._TargetValue.Shadow = _TargetValue;
			target._Elapsed.Shadow = _Elapsed;
			target._TransitionDuration.Shadow = _TransitionDuration;
			target._t0.Shadow = _t0;
			target._x0.Shadow = _x0;
			target._k1.Shadow = _k1;
			target._k2.Shadow = _k2;
			target._k3.Shadow = _k3;
			target._k4.Shadow = _k4;
			target._ImmediateChange.Shadow = _ImmediateChange;
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowTcoTask_Transition target)
		{
			this.CopyPlainToShadow((TcoCoreTests.TcoTask_Transition)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.TcoTask_Transition source)
		{
			_TransitionType = source._TransitionType.LastValue;
			_k0 = source._k0.LastValue;
			_StartValue = source._StartValue.LastValue;
			_TargetValue = source._TargetValue.LastValue;
			_Elapsed = source._Elapsed.LastValue;
			_TransitionDuration = source._TransitionDuration.LastValue;
			_t0 = source._t0.LastValue;
			_x0 = source._x0.LastValue;
			_k1 = source._k1.LastValue;
			_k2 = source._k2.LastValue;
			_k3 = source._k3.LastValue;
			_k4 = source._k4.LastValue;
			_ImmediateChange = source._ImmediateChange.LastValue;
		}

		public void CopyCyclicToPlain(TcoCoreTests.ITcoTask_Transition source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.TcoTask_Transition)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.TcoTask_Transition source)
		{
			_TransitionType = source._TransitionType.Shadow;
			_k0 = source._k0.Shadow;
			_StartValue = source._StartValue.Shadow;
			_TargetValue = source._TargetValue.Shadow;
			_Elapsed = source._Elapsed.Shadow;
			_TransitionDuration = source._TransitionDuration.Shadow;
			_t0 = source._t0.Shadow;
			_x0 = source._x0.Shadow;
			_k1 = source._k1.Shadow;
			_k2 = source._k2.Shadow;
			_k3 = source._k3.Shadow;
			_k4 = source._k4.Shadow;
			_ImmediateChange = source._ImmediateChange.Shadow;
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowTcoTask_Transition source)
		{
			this.CopyShadowToPlain((TcoCoreTests.TcoTask_Transition)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoTask_Transition()
		{
		}
	}
}