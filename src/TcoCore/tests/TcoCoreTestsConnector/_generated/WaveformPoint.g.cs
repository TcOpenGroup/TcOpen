using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "WaveformPoint", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class WaveformPoint : Vortex.Connector.IVortexObject, IWaveformPoint, IShadowWaveformPoint, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		Vortex.Connector.ValueTypes.OnlinerInt _TransitionType;
		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTransitionType))]
		public Vortex.Connector.ValueTypes.OnlinerInt TransitionType
		{
			get
			{
				return _TransitionType;
			}
		}

		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTransitionType))]
		Vortex.Connector.ValueTypes.Online.IOnlineInt IWaveformPoint.TransitionType
		{
			get
			{
				return TransitionType;
			}
		}

		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTransitionType))]
		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadowWaveformPoint.TransitionType
		{
			get
			{
				return TransitionType;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLReal _TargetValue;
		public Vortex.Connector.ValueTypes.OnlinerLReal TargetValue
		{
			get
			{
				return _TargetValue;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal IWaveformPoint.TargetValue
		{
			get
			{
				return TargetValue;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal IShadowWaveformPoint.TargetValue
		{
			get
			{
				return TargetValue;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerUInt _Duration;
		public Vortex.Connector.ValueTypes.OnlinerUInt Duration
		{
			get
			{
				return _Duration;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineUInt IWaveformPoint.Duration
		{
			get
			{
				return Duration;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowUInt IShadowWaveformPoint.Duration
		{
			get
			{
				return Duration;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLReal _Shape;
		public Vortex.Connector.ValueTypes.OnlinerLReal Shape
		{
			get
			{
				return _Shape;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal IWaveformPoint.Shape
		{
			get
			{
				return Shape;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal IShadowWaveformPoint.Shape
		{
			get
			{
				return Shape;
			}
		}

		public void LazyOnlineToShadow()
		{
			TransitionType.Shadow = TransitionType.LastValue;
			TargetValue.Shadow = TargetValue.LastValue;
			Duration.Shadow = Duration.LastValue;
			Shape.Shadow = Shape.LastValue;
		}

		public void LazyShadowToOnline()
		{
			TransitionType.Cyclic = TransitionType.Shadow;
			TargetValue.Cyclic = TargetValue.Shadow;
			Duration.Cyclic = Duration.Shadow;
			Shape.Cyclic = Shape.Shadow;
		}

		public PlainWaveformPoint CreatePlainerType()
		{
			var cloned = new PlainWaveformPoint();
			return cloned;
		}

		protected PlainWaveformPoint CreatePlainerType(PlainWaveformPoint cloned)
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

		public void FlushPlainToOnline(TcoCoreTests.PlainWaveformPoint source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainWaveformPoint source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainWaveformPoint source)
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

		public WaveformPoint(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@ValueTags = new System.Collections.Generic.List<Vortex.Connector.IValueTag>();
			this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			_TransitionType = @Connector.Online.Adapter.CreateINT(this, "", "TransitionType");
			_TargetValue = @Connector.Online.Adapter.CreateLREAL(this, "", "TargetValue");
			_Duration = @Connector.Online.Adapter.CreateUINT(this, "", "Duration");
			_Shape = @Connector.Online.Adapter.CreateLREAL(this, "", "Shape");
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
			parent.AddChild(this);
		}

		public WaveformPoint()
		{
			PexPreConstructorParameterless();
			_TransitionType = Vortex.Connector.IConnectorFactory.CreateINT();
			_TargetValue = Vortex.Connector.IConnectorFactory.CreateLREAL();
			_Duration = Vortex.Connector.IConnectorFactory.CreateUINT();
			_Shape = Vortex.Connector.IConnectorFactory.CreateLREAL();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcWaveformPoint
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcWaveformPoint()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IWaveformPoint : Vortex.Connector.IVortexOnlineObject
	{
		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTransitionType))]
		Vortex.Connector.ValueTypes.Online.IOnlineInt TransitionType
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal TargetValue
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineUInt Duration
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLReal Shape
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainWaveformPoint CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainWaveformPoint source);
		void FlushOnlineToPlain(TcoCoreTests.PlainWaveformPoint source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowWaveformPoint : Vortex.Connector.IVortexShadowObject
	{
		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTransitionType))]
		Vortex.Connector.ValueTypes.Shadows.IShadowInt TransitionType
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal TargetValue
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowUInt Duration
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLReal Shape
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainWaveformPoint CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainWaveformPoint source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainWaveformPoint : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		System.Int16 _TransitionType;
		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTransitionType))]
		public System.Int16 TransitionType
		{
			get
			{
				return _TransitionType;
			}

			set
			{
				if (_TransitionType != value)
				{
					_TransitionType = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(TransitionType)));
				}
			}
		}

		System.Double _TargetValue;
		public System.Double TargetValue
		{
			get
			{
				return _TargetValue;
			}

			set
			{
				if (_TargetValue != value)
				{
					_TargetValue = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(TargetValue)));
				}
			}
		}

		System.UInt16 _Duration;
		public System.UInt16 Duration
		{
			get
			{
				return _Duration;
			}

			set
			{
				if (_Duration != value)
				{
					_Duration = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Duration)));
				}
			}
		}

		System.Double _Shape;
		public System.Double Shape
		{
			get
			{
				return _Shape;
			}

			set
			{
				if (_Shape != value)
				{
					_Shape = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Shape)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCoreTests.WaveformPoint target)
		{
			target.TransitionType.Cyclic = TransitionType;
			target.TargetValue.Cyclic = TargetValue;
			target.Duration.Cyclic = Duration;
			target.Shape.Cyclic = Shape;
		}

		public void CopyPlainToCyclic(TcoCoreTests.IWaveformPoint target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.WaveformPoint)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.WaveformPoint target)
		{
			target.TransitionType.Shadow = TransitionType;
			target.TargetValue.Shadow = TargetValue;
			target.Duration.Shadow = Duration;
			target.Shape.Shadow = Shape;
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowWaveformPoint target)
		{
			this.CopyPlainToShadow((TcoCoreTests.WaveformPoint)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.WaveformPoint source)
		{
			TransitionType = source.TransitionType.LastValue;
			TargetValue = source.TargetValue.LastValue;
			Duration = source.Duration.LastValue;
			Shape = source.Shape.LastValue;
		}

		public void CopyCyclicToPlain(TcoCoreTests.IWaveformPoint source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.WaveformPoint)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.WaveformPoint source)
		{
			TransitionType = source.TransitionType.Shadow;
			TargetValue = source.TargetValue.Shadow;
			Duration = source.Duration.Shadow;
			Shape = source.Shape.Shadow;
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowWaveformPoint source)
		{
			this.CopyShadowToPlain((TcoCoreTests.WaveformPoint)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainWaveformPoint()
		{
		}
	}
}