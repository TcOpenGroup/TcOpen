using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "fbPiston", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class fbPiston : Vortex.Connector.IVortexObject, IfbPiston, IShadowfbPiston, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		Vortex.Connector.ValueTypes.OnlinerBool _iHomePositionSensor;
		public Vortex.Connector.ValueTypes.OnlinerBool iHomePositionSensor
		{
			get
			{
				return _iHomePositionSensor;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool IfbPiston.iHomePositionSensor
		{
			get
			{
				return iHomePositionSensor;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowfbPiston.iHomePositionSensor
		{
			get
			{
				return iHomePositionSensor;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool _iWorkPositionSensor;
		public Vortex.Connector.ValueTypes.OnlinerBool iWorkPositionSensor
		{
			get
			{
				return _iWorkPositionSensor;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool IfbPiston.iWorkPositionSensor
		{
			get
			{
				return iWorkPositionSensor;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowfbPiston.iWorkPositionSensor
		{
			get
			{
				return iWorkPositionSensor;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool _qHomePositionActuator;
		public Vortex.Connector.ValueTypes.OnlinerBool qHomePositionActuator
		{
			get
			{
				return _qHomePositionActuator;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool IfbPiston.qHomePositionActuator
		{
			get
			{
				return qHomePositionActuator;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowfbPiston.qHomePositionActuator
		{
			get
			{
				return qHomePositionActuator;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool _qWorkPositionActuator;
		public Vortex.Connector.ValueTypes.OnlinerBool qWorkPositionActuator
		{
			get
			{
				return _qWorkPositionActuator;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool IfbPiston.qWorkPositionActuator
		{
			get
			{
				return qWorkPositionActuator;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowfbPiston.qWorkPositionActuator
		{
			get
			{
				return qWorkPositionActuator;
			}
		}

		fbPistonMoveTask __moveHomeTask;
		public fbPistonMoveTask _moveHomeTask
		{
			get
			{
				return __moveHomeTask;
			}
		}

		IfbPistonMoveTask IfbPiston._moveHomeTask
		{
			get
			{
				return _moveHomeTask;
			}
		}

		IShadowfbPistonMoveTask IShadowfbPiston._moveHomeTask
		{
			get
			{
				return _moveHomeTask;
			}
		}

		fbPistonMoveTask __moveWorkTask;
		public fbPistonMoveTask _moveWorkTask
		{
			get
			{
				return __moveWorkTask;
			}
		}

		IfbPistonMoveTask IfbPiston._moveWorkTask
		{
			get
			{
				return _moveWorkTask;
			}
		}

		IShadowfbPistonMoveTask IShadowfbPiston._moveWorkTask
		{
			get
			{
				return _moveWorkTask;
			}
		}

		public void LazyOnlineToShadow()
		{
			iHomePositionSensor.Shadow = iHomePositionSensor.LastValue;
			iWorkPositionSensor.Shadow = iWorkPositionSensor.LastValue;
			qHomePositionActuator.Shadow = qHomePositionActuator.LastValue;
			qWorkPositionActuator.Shadow = qWorkPositionActuator.LastValue;
			_moveHomeTask.LazyOnlineToShadow();
			_moveWorkTask.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			iHomePositionSensor.Cyclic = iHomePositionSensor.Shadow;
			iWorkPositionSensor.Cyclic = iWorkPositionSensor.Shadow;
			qHomePositionActuator.Cyclic = qHomePositionActuator.Shadow;
			qWorkPositionActuator.Cyclic = qWorkPositionActuator.Shadow;
			_moveHomeTask.LazyShadowToOnline();
			_moveWorkTask.LazyShadowToOnline();
		}

		public PlainfbPiston CreatePlainerType()
		{
			var cloned = new PlainfbPiston();
			cloned._moveHomeTask = _moveHomeTask.CreatePlainerType();
			cloned._moveWorkTask = _moveWorkTask.CreatePlainerType();
			return cloned;
		}

		protected PlainfbPiston CreatePlainerType(PlainfbPiston cloned)
		{
			cloned._moveHomeTask = _moveHomeTask.CreatePlainerType();
			cloned._moveWorkTask = _moveWorkTask.CreatePlainerType();
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

		public void FlushPlainToOnline(TcoCoreTests.PlainfbPiston source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainfbPiston source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainfbPiston source)
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

		public fbPiston(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			_iHomePositionSensor = @Connector.Online.Adapter.CreateBOOL(this, "", "iHomePositionSensor");
			_iWorkPositionSensor = @Connector.Online.Adapter.CreateBOOL(this, "", "iWorkPositionSensor");
			_qHomePositionActuator = @Connector.Online.Adapter.CreateBOOL(this, "", "qHomePositionActuator");
			_qWorkPositionActuator = @Connector.Online.Adapter.CreateBOOL(this, "", "qWorkPositionActuator");
			__moveHomeTask = new fbPistonMoveTask(this, "", "_moveHomeTask");
			__moveWorkTask = new fbPistonMoveTask(this, "", "_moveWorkTask");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public fbPiston()
		{
			PexPreConstructorParameterless();
			_iHomePositionSensor = Vortex.Connector.IConnectorFactory.CreateBOOL();
			_iWorkPositionSensor = Vortex.Connector.IConnectorFactory.CreateBOOL();
			_qHomePositionActuator = Vortex.Connector.IConnectorFactory.CreateBOOL();
			_qWorkPositionActuator = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__moveHomeTask = new fbPistonMoveTask();
			__moveWorkTask = new fbPistonMoveTask();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcfbPiston
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcfbPiston()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IfbPiston : Vortex.Connector.IVortexOnlineObject
	{
		Vortex.Connector.ValueTypes.Online.IOnlineBool iHomePositionSensor
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool iWorkPositionSensor
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool qHomePositionActuator
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool qWorkPositionActuator
		{
			get;
		}

		IfbPistonMoveTask _moveHomeTask
		{
			get;
		}

		IfbPistonMoveTask _moveWorkTask
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainfbPiston CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainfbPiston source);
		void FlushOnlineToPlain(TcoCoreTests.PlainfbPiston source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowfbPiston : Vortex.Connector.IVortexShadowObject
	{
		Vortex.Connector.ValueTypes.Shadows.IShadowBool iHomePositionSensor
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool iWorkPositionSensor
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool qHomePositionActuator
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool qWorkPositionActuator
		{
			get;
		}

		IShadowfbPistonMoveTask _moveHomeTask
		{
			get;
		}

		IShadowfbPistonMoveTask _moveWorkTask
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainfbPiston CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainfbPiston source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainfbPiston : Vortex.Connector.IPlain
	{
		System.Boolean _iHomePositionSensor;
		public System.Boolean iHomePositionSensor
		{
			get
			{
				return _iHomePositionSensor;
			}

			set
			{
				if (_iHomePositionSensor != value)
				{
					_iHomePositionSensor = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(iHomePositionSensor)));
				}
			}
		}

		System.Boolean _iWorkPositionSensor;
		public System.Boolean iWorkPositionSensor
		{
			get
			{
				return _iWorkPositionSensor;
			}

			set
			{
				if (_iWorkPositionSensor != value)
				{
					_iWorkPositionSensor = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(iWorkPositionSensor)));
				}
			}
		}

		System.Boolean _qHomePositionActuator;
		public System.Boolean qHomePositionActuator
		{
			get
			{
				return _qHomePositionActuator;
			}

			set
			{
				if (_qHomePositionActuator != value)
				{
					_qHomePositionActuator = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(qHomePositionActuator)));
				}
			}
		}

		System.Boolean _qWorkPositionActuator;
		public System.Boolean qWorkPositionActuator
		{
			get
			{
				return _qWorkPositionActuator;
			}

			set
			{
				if (_qWorkPositionActuator != value)
				{
					_qWorkPositionActuator = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(qWorkPositionActuator)));
				}
			}
		}

		PlainfbPistonMoveTask __moveHomeTask;
		public PlainfbPistonMoveTask _moveHomeTask
		{
			get
			{
				return __moveHomeTask;
			}

			set
			{
				if (__moveHomeTask != value)
				{
					__moveHomeTask = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_moveHomeTask)));
				}
			}
		}

		PlainfbPistonMoveTask __moveWorkTask;
		public PlainfbPistonMoveTask _moveWorkTask
		{
			get
			{
				return __moveWorkTask;
			}

			set
			{
				if (__moveWorkTask != value)
				{
					__moveWorkTask = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_moveWorkTask)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCoreTests.fbPiston target)
		{
			target.iHomePositionSensor.Cyclic = iHomePositionSensor;
			target.iWorkPositionSensor.Cyclic = iWorkPositionSensor;
			target.qHomePositionActuator.Cyclic = qHomePositionActuator;
			target.qWorkPositionActuator.Cyclic = qWorkPositionActuator;
			_moveHomeTask.CopyPlainToCyclic(target._moveHomeTask);
			_moveWorkTask.CopyPlainToCyclic(target._moveWorkTask);
		}

		public void CopyPlainToCyclic(TcoCoreTests.IfbPiston target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.fbPiston)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.fbPiston target)
		{
			target.iHomePositionSensor.Shadow = iHomePositionSensor;
			target.iWorkPositionSensor.Shadow = iWorkPositionSensor;
			target.qHomePositionActuator.Shadow = qHomePositionActuator;
			target.qWorkPositionActuator.Shadow = qWorkPositionActuator;
			_moveHomeTask.CopyPlainToShadow(target._moveHomeTask);
			_moveWorkTask.CopyPlainToShadow(target._moveWorkTask);
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowfbPiston target)
		{
			this.CopyPlainToShadow((TcoCoreTests.fbPiston)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.fbPiston source)
		{
			iHomePositionSensor = source.iHomePositionSensor.LastValue;
			iWorkPositionSensor = source.iWorkPositionSensor.LastValue;
			qHomePositionActuator = source.qHomePositionActuator.LastValue;
			qWorkPositionActuator = source.qWorkPositionActuator.LastValue;
			_moveHomeTask.CopyCyclicToPlain(source._moveHomeTask);
			_moveWorkTask.CopyCyclicToPlain(source._moveWorkTask);
		}

		public void CopyCyclicToPlain(TcoCoreTests.IfbPiston source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.fbPiston)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.fbPiston source)
		{
			iHomePositionSensor = source.iHomePositionSensor.Shadow;
			iWorkPositionSensor = source.iWorkPositionSensor.Shadow;
			qHomePositionActuator = source.qHomePositionActuator.Shadow;
			qWorkPositionActuator = source.qWorkPositionActuator.Shadow;
			_moveHomeTask.CopyShadowToPlain(source._moveHomeTask);
			_moveWorkTask.CopyShadowToPlain(source._moveWorkTask);
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowfbPiston source)
		{
			this.CopyShadowToPlain((TcoCoreTests.fbPiston)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainfbPiston()
		{
			__moveHomeTask = new PlainfbPistonMoveTask();
			__moveWorkTask = new PlainfbPistonMoveTask();
		}
	}
}