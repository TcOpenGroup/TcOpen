using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoTask_DownCounter", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class TcoTask_DownCounter : Vortex.Connector.IVortexObject, ITcoTask_DownCounter, IShadowTcoTask_DownCounter, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		Vortex.Connector.ValueTypes.OnlinerLInt __SetUpValue;
		public Vortex.Connector.ValueTypes.OnlinerLInt _SetUpValue
		{
			get
			{
				return __SetUpValue;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt ITcoTask_DownCounter._SetUpValue
		{
			get
			{
				return _SetUpValue;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt IShadowTcoTask_DownCounter._SetUpValue
		{
			get
			{
				return _SetUpValue;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLInt __CounterValue;
		public Vortex.Connector.ValueTypes.OnlinerLInt _CounterValue
		{
			get
			{
				return __CounterValue;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt ITcoTask_DownCounter._CounterValue
		{
			get
			{
				return _CounterValue;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt IShadowTcoTask_DownCounter._CounterValue
		{
			get
			{
				return _CounterValue;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLInt __InvokeCounter;
		public Vortex.Connector.ValueTypes.OnlinerLInt _InvokeCounter
		{
			get
			{
				return __InvokeCounter;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt ITcoTask_DownCounter._InvokeCounter
		{
			get
			{
				return _InvokeCounter;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt IShadowTcoTask_DownCounter._InvokeCounter
		{
			get
			{
				return _InvokeCounter;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLInt __DoneCounter;
		public Vortex.Connector.ValueTypes.OnlinerLInt _DoneCounter
		{
			get
			{
				return __DoneCounter;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt ITcoTask_DownCounter._DoneCounter
		{
			get
			{
				return _DoneCounter;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt IShadowTcoTask_DownCounter._DoneCounter
		{
			get
			{
				return _DoneCounter;
			}
		}

		public void LazyOnlineToShadow()
		{
			_SetUpValue.Shadow = _SetUpValue.LastValue;
			_CounterValue.Shadow = _CounterValue.LastValue;
			_InvokeCounter.Shadow = _InvokeCounter.LastValue;
			_DoneCounter.Shadow = _DoneCounter.LastValue;
		}

		public void LazyShadowToOnline()
		{
			_SetUpValue.Cyclic = _SetUpValue.Shadow;
			_CounterValue.Cyclic = _CounterValue.Shadow;
			_InvokeCounter.Cyclic = _InvokeCounter.Shadow;
			_DoneCounter.Cyclic = _DoneCounter.Shadow;
		}

		public PlainTcoTask_DownCounter CreatePlainerType()
		{
			var cloned = new PlainTcoTask_DownCounter();
			return cloned;
		}

		protected PlainTcoTask_DownCounter CreatePlainerType(PlainTcoTask_DownCounter cloned)
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

		public void FlushPlainToOnline(TcoCoreTests.PlainTcoTask_DownCounter source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainTcoTask_DownCounter source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainTcoTask_DownCounter source)
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

		public TcoTask_DownCounter(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			__SetUpValue = @Connector.Online.Adapter.CreateLINT(this, "", "_SetUpValue");
			__CounterValue = @Connector.Online.Adapter.CreateLINT(this, "", "_CounterValue");
			__InvokeCounter = @Connector.Online.Adapter.CreateLINT(this, "", "_InvokeCounter");
			__DoneCounter = @Connector.Online.Adapter.CreateLINT(this, "", "_DoneCounter");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public TcoTask_DownCounter()
		{
			PexPreConstructorParameterless();
			__SetUpValue = Vortex.Connector.IConnectorFactory.CreateLINT();
			__CounterValue = Vortex.Connector.IConnectorFactory.CreateLINT();
			__InvokeCounter = Vortex.Connector.IConnectorFactory.CreateLINT();
			__DoneCounter = Vortex.Connector.IConnectorFactory.CreateLINT();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoTask_DownCounter
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoTask_DownCounter()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoTask_DownCounter : Vortex.Connector.IVortexOnlineObject
	{
		Vortex.Connector.ValueTypes.Online.IOnlineLInt _SetUpValue
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt _CounterValue
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt _InvokeCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt _DoneCounter
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoTask_DownCounter CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainTcoTask_DownCounter source);
		void FlushOnlineToPlain(TcoCoreTests.PlainTcoTask_DownCounter source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoTask_DownCounter : Vortex.Connector.IVortexShadowObject
	{
		Vortex.Connector.ValueTypes.Shadows.IShadowLInt _SetUpValue
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt _CounterValue
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt _InvokeCounter
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt _DoneCounter
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoTask_DownCounter CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainTcoTask_DownCounter source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoTask_DownCounter : Vortex.Connector.IPlain
	{
		System.Int64 __SetUpValue;
		public System.Int64 _SetUpValue
		{
			get
			{
				return __SetUpValue;
			}

			set
			{
				if (__SetUpValue != value)
				{
					__SetUpValue = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_SetUpValue)));
				}
			}
		}

		System.Int64 __CounterValue;
		public System.Int64 _CounterValue
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

		System.Int64 __InvokeCounter;
		public System.Int64 _InvokeCounter
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

		System.Int64 __DoneCounter;
		public System.Int64 _DoneCounter
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

		public void CopyPlainToCyclic(TcoCoreTests.TcoTask_DownCounter target)
		{
			target._SetUpValue.Cyclic = _SetUpValue;
			target._CounterValue.Cyclic = _CounterValue;
			target._InvokeCounter.Cyclic = _InvokeCounter;
			target._DoneCounter.Cyclic = _DoneCounter;
		}

		public void CopyPlainToCyclic(TcoCoreTests.ITcoTask_DownCounter target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.TcoTask_DownCounter)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.TcoTask_DownCounter target)
		{
			target._SetUpValue.Shadow = _SetUpValue;
			target._CounterValue.Shadow = _CounterValue;
			target._InvokeCounter.Shadow = _InvokeCounter;
			target._DoneCounter.Shadow = _DoneCounter;
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowTcoTask_DownCounter target)
		{
			this.CopyPlainToShadow((TcoCoreTests.TcoTask_DownCounter)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.TcoTask_DownCounter source)
		{
			_SetUpValue = source._SetUpValue.LastValue;
			_CounterValue = source._CounterValue.LastValue;
			_InvokeCounter = source._InvokeCounter.LastValue;
			_DoneCounter = source._DoneCounter.LastValue;
		}

		public void CopyCyclicToPlain(TcoCoreTests.ITcoTask_DownCounter source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.TcoTask_DownCounter)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.TcoTask_DownCounter source)
		{
			_SetUpValue = source._SetUpValue.Shadow;
			_CounterValue = source._CounterValue.Shadow;
			_InvokeCounter = source._InvokeCounter.Shadow;
			_DoneCounter = source._DoneCounter.Shadow;
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowTcoTask_DownCounter source)
		{
			this.CopyShadowToPlain((TcoCoreTests.TcoTask_DownCounter)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoTask_DownCounter()
		{
		}
	}
}