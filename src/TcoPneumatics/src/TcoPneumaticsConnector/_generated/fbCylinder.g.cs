using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoPneumatics
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "fbCylinder", "TcoPneumatics", TypeComplexityEnum.Complex)]
	public partial class fbCylinder : TcoCore.TcoComponent, Vortex.Connector.IVortexObject, IfbCylinder, IShadowfbCylinder, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		Vortex.Connector.ValueTypes.OnlinerBool _inAtHomePos;
		
///			<summary>
///				Home position sensor.
///			</summary>	

		[Container(Layout.UniformGrid), Group(Layout.GroupBox, "Inputs")]
		public Vortex.Connector.ValueTypes.OnlinerBool inAtHomePos
		{
			get
			{
				return _inAtHomePos;
			}
		}

		[Container(Layout.UniformGrid), Group(Layout.GroupBox, "Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineBool IfbCylinder.inAtHomePos
		{
			get
			{
				return inAtHomePos;
			}
		}

		[Container(Layout.UniformGrid), Group(Layout.GroupBox, "Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowfbCylinder.inAtHomePos
		{
			get
			{
				return inAtHomePos;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool _inAtWorkPos;
		
///			<summary>
///				Working position sensor.
///			</summary>	

		public Vortex.Connector.ValueTypes.OnlinerBool inAtWorkPos
		{
			get
			{
				return _inAtWorkPos;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool IfbCylinder.inAtWorkPos
		{
			get
			{
				return inAtWorkPos;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowfbCylinder.inAtWorkPos
		{
			get
			{
				return inAtWorkPos;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool _outToHomePos;
		
///			<summary>
///				Move to working home position signal.
///			</summary>	

		[Container(Layout.UniformGrid), Group(Layout.GroupBox, "Outputs")]
		public Vortex.Connector.ValueTypes.OnlinerBool outToHomePos
		{
			get
			{
				return _outToHomePos;
			}
		}

		[Container(Layout.UniformGrid), Group(Layout.GroupBox, "Outputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineBool IfbCylinder.outToHomePos
		{
			get
			{
				return outToHomePos;
			}
		}

		[Container(Layout.UniformGrid), Group(Layout.GroupBox, "Outputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowfbCylinder.outToHomePos
		{
			get
			{
				return outToHomePos;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool _outToWorkPos;
		
///			<summary>
///				Move to working posistion signal.
///			</summary>	

		public Vortex.Connector.ValueTypes.OnlinerBool outToWorkPos
		{
			get
			{
				return _outToWorkPos;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool IfbCylinder.outToWorkPos
		{
			get
			{
				return outToWorkPos;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowfbCylinder.outToWorkPos
		{
			get
			{
				return outToWorkPos;
			}
		}

		TcoCore.TcoTask __moveHomeTask;
		[Container(Layout.UniformGrid)]
		public TcoCore.TcoTask _moveHomeTask
		{
			get
			{
				return __moveHomeTask;
			}
		}

		[Container(Layout.UniformGrid)]
		TcoCore.ITcoTask IfbCylinder._moveHomeTask
		{
			get
			{
				return _moveHomeTask;
			}
		}

		[Container(Layout.UniformGrid)]
		TcoCore.IShadowTcoTask IShadowfbCylinder._moveHomeTask
		{
			get
			{
				return _moveHomeTask;
			}
		}

		TcoCore.TcoTask __moveWorkTask;
		public TcoCore.TcoTask _moveWorkTask
		{
			get
			{
				return __moveWorkTask;
			}
		}

		TcoCore.ITcoTask IfbCylinder._moveWorkTask
		{
			get
			{
				return _moveWorkTask;
			}
		}

		TcoCore.IShadowTcoTask IShadowfbCylinder._moveWorkTask
		{
			get
			{
				return _moveWorkTask;
			}
		}

		TcoCore.TcoTask __stopTask;
		public TcoCore.TcoTask _stopTask
		{
			get
			{
				return __stopTask;
			}
		}

		TcoCore.ITcoTask IfbCylinder._stopTask
		{
			get
			{
				return _stopTask;
			}
		}

		TcoCore.IShadowTcoTask IShadowfbCylinder._stopTask
		{
			get
			{
				return _stopTask;
			}
		}

		public new void LazyOnlineToShadow()
		{
			base.LazyOnlineToShadow();
			inAtHomePos.Shadow = inAtHomePos.LastValue;
			inAtWorkPos.Shadow = inAtWorkPos.LastValue;
			outToHomePos.Shadow = outToHomePos.LastValue;
			outToWorkPos.Shadow = outToWorkPos.LastValue;
			_moveHomeTask.LazyOnlineToShadow();
			_moveWorkTask.LazyOnlineToShadow();
			_stopTask.LazyOnlineToShadow();
		}

		public new void LazyShadowToOnline()
		{
			base.LazyShadowToOnline();
			inAtHomePos.Cyclic = inAtHomePos.Shadow;
			inAtWorkPos.Cyclic = inAtWorkPos.Shadow;
			outToHomePos.Cyclic = outToHomePos.Shadow;
			outToWorkPos.Cyclic = outToWorkPos.Shadow;
			_moveHomeTask.LazyShadowToOnline();
			_moveWorkTask.LazyShadowToOnline();
			_stopTask.LazyShadowToOnline();
		}

		public new PlainfbCylinder CreatePlainerType()
		{
			var cloned = new PlainfbCylinder();
			base.CreatePlainerType(cloned);
			cloned._moveHomeTask = _moveHomeTask.CreatePlainerType();
			cloned._moveWorkTask = _moveWorkTask.CreatePlainerType();
			cloned._stopTask = _stopTask.CreatePlainerType();
			return cloned;
		}

		protected PlainfbCylinder CreatePlainerType(PlainfbCylinder cloned)
		{
			base.CreatePlainerType(cloned);
			cloned._moveHomeTask = _moveHomeTask.CreatePlainerType();
			cloned._moveWorkTask = _moveWorkTask.CreatePlainerType();
			cloned._stopTask = _stopTask.CreatePlainerType();
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
		partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexConstructorParameterless();
		public void FlushPlainToOnline(TcoPneumatics.PlainfbCylinder source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoPneumatics.PlainfbCylinder source)
		{
			source.CopyPlainToShadow(this);
		}

		public new void FlushShadowToOnline()
		{
			this.LazyShadowToOnline();
			this.Write();
		}

		public new void FlushOnlineToShadow()
		{
			this.Read();
			this.LazyOnlineToShadow();
		}

		public void FlushOnlineToPlain(TcoPneumatics.PlainfbCylinder source)
		{
			this.Read();
			source.CopyCyclicToPlain(this);
		}

		public fbCylinder(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail): base (parent, readableTail, symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			_inAtHomePos = @Connector.Online.Adapter.CreateBOOL(this, "Home position sensor", "inAtHomePos");
			inAtHomePos.AttributeName = "Home position sensor";
			_inAtWorkPos = @Connector.Online.Adapter.CreateBOOL(this, "Work position sensor", "inAtWorkPos");
			inAtWorkPos.AttributeName = "Work position sensor";
			_outToHomePos = @Connector.Online.Adapter.CreateBOOL(this, "Move to home position actuator", "outToHomePos");
			outToHomePos.AttributeName = "Move to home position actuator";
			_outToWorkPos = @Connector.Online.Adapter.CreateBOOL(this, "Move to work position actuator", "outToWorkPos");
			outToWorkPos.AttributeName = "Move to work position actuator";
			__moveHomeTask = new TcoCore.TcoTask(this, "Request move to Home position", "_moveHomeTask");
			__moveHomeTask.AttributeName = "Request move to Home position";
			__moveWorkTask = new TcoCore.TcoTask(this, "Request move to Work position", "_moveWorkTask");
			__moveWorkTask.AttributeName = "Request move to Work position";
			__stopTask = new TcoCore.TcoTask(this, "Request Stop", "_stopTask");
			__stopTask.AttributeName = "Request Stop";
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
		}

		public fbCylinder(): base ()
		{
			PexPreConstructorParameterless();
			_inAtHomePos = Vortex.Connector.IConnectorFactory.CreateBOOL();
			inAtHomePos.AttributeName = "Home position sensor";
			_inAtWorkPos = Vortex.Connector.IConnectorFactory.CreateBOOL();
			inAtWorkPos.AttributeName = "Work position sensor";
			_outToHomePos = Vortex.Connector.IConnectorFactory.CreateBOOL();
			outToHomePos.AttributeName = "Move to home position actuator";
			_outToWorkPos = Vortex.Connector.IConnectorFactory.CreateBOOL();
			outToWorkPos.AttributeName = "Move to work position actuator";
			__moveHomeTask = new TcoCore.TcoTask();
			__moveHomeTask.AttributeName = "Request move to Home position";
			__moveWorkTask = new TcoCore.TcoTask();
			__moveWorkTask.AttributeName = "Request move to Work position";
			__stopTask = new TcoCore.TcoTask();
			__stopTask.AttributeName = "Request Stop";
			AttributeName = "";
			PexConstructorParameterless();
		}

		public System.Boolean _MoveToHomeTest()
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "_MoveToHomeTest", new object[]{});
		}

		public System.Boolean _MoveToWorkTest()
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "_MoveToWorkTest", new object[]{});
		}

		public System.Boolean _StopTest()
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "_StopTest", new object[]{});
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcfbCylinder : TcoCore.TcoComponent.PlcTcoComponent
		{
			
///		<summary>
///			Moves the piston into home position.
///		</summary>
///		<returns>True when home position sensor is reached.</returns>
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type ITcoTask; Twin type: <see cref="ITcoTask"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute()]
			public dynamic MoveToHome()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			Moves the piston into working position.
///		</summary>
///		<returns>True when working position sensor is reached.</returns>
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type ITcoTask; Twin type: <see cref="ITcoTask"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute()]
			public dynamic MoveToWork()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///			<summary>
///				Home position sensor.
///			</summary>	

			public object inAtHomePos;
			
///			<summary>
///				Working position sensor.
///			</summary>	

			public object inAtWorkPos;
			
///			<summary>
///				Move to working home position signal.
///			</summary>	

			public object outToHomePos;
			
///			<summary>
///				Move to working posistion signal.
///			</summary>	

			public object outToWorkPos;
			public TcoCore.PlainTcoTask _moveHomeTask;
			public TcoCore.PlainTcoTask _moveWorkTask;
			public TcoCore.PlainTcoTask _stopTask;
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcfbCylinder()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IfbCylinder : Vortex.Connector.IVortexOnlineObject, TcoCore.ITcoComponent
	{
		[Container(Layout.UniformGrid), Group(Layout.GroupBox, "Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineBool inAtHomePos
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool inAtWorkPos
		{
			get;
		}

		[Container(Layout.UniformGrid), Group(Layout.GroupBox, "Outputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineBool outToHomePos
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool outToWorkPos
		{
			get;
		}

		[Container(Layout.UniformGrid)]
		TcoCore.ITcoTask _moveHomeTask
		{
			get;
		}

		TcoCore.ITcoTask _moveWorkTask
		{
			get;
		}

		TcoCore.ITcoTask _stopTask
		{
			get;
		}

		new TcoPneumatics.PlainfbCylinder CreatePlainerType();
		new void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoPneumatics.PlainfbCylinder source);
		void FlushOnlineToPlain(TcoPneumatics.PlainfbCylinder source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowfbCylinder : Vortex.Connector.IVortexShadowObject, TcoCore.IShadowTcoComponent
	{
		[Container(Layout.UniformGrid), Group(Layout.GroupBox, "Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool inAtHomePos
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool inAtWorkPos
		{
			get;
		}

		[Container(Layout.UniformGrid), Group(Layout.GroupBox, "Outputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool outToHomePos
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool outToWorkPos
		{
			get;
		}

		[Container(Layout.UniformGrid)]
		TcoCore.IShadowTcoTask _moveHomeTask
		{
			get;
		}

		TcoCore.IShadowTcoTask _moveWorkTask
		{
			get;
		}

		TcoCore.IShadowTcoTask _stopTask
		{
			get;
		}

		new TcoPneumatics.PlainfbCylinder CreatePlainerType();
		new void FlushShadowToOnline();
		void CopyPlainToShadow(TcoPneumatics.PlainfbCylinder source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainfbCylinder : TcoCore.PlainTcoComponent
	{
		System.Boolean _inAtHomePos;
		[Container(Layout.UniformGrid), Group(Layout.GroupBox, "Inputs")]
		public System.Boolean inAtHomePos
		{
			get
			{
				return _inAtHomePos;
			}

			set
			{
				if (_inAtHomePos != value)
				{
					_inAtHomePos = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(inAtHomePos)));
				}
			}
		}

		System.Boolean _inAtWorkPos;
		public System.Boolean inAtWorkPos
		{
			get
			{
				return _inAtWorkPos;
			}

			set
			{
				if (_inAtWorkPos != value)
				{
					_inAtWorkPos = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(inAtWorkPos)));
				}
			}
		}

		System.Boolean _outToHomePos;
		[Container(Layout.UniformGrid), Group(Layout.GroupBox, "Outputs")]
		public System.Boolean outToHomePos
		{
			get
			{
				return _outToHomePos;
			}

			set
			{
				if (_outToHomePos != value)
				{
					_outToHomePos = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(outToHomePos)));
				}
			}
		}

		System.Boolean _outToWorkPos;
		public System.Boolean outToWorkPos
		{
			get
			{
				return _outToWorkPos;
			}

			set
			{
				if (_outToWorkPos != value)
				{
					_outToWorkPos = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(outToWorkPos)));
				}
			}
		}

		TcoCore.PlainTcoTask __moveHomeTask;
		[Container(Layout.UniformGrid)]
		public TcoCore.PlainTcoTask _moveHomeTask
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

		TcoCore.PlainTcoTask __moveWorkTask;
		public TcoCore.PlainTcoTask _moveWorkTask
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

		TcoCore.PlainTcoTask __stopTask;
		public TcoCore.PlainTcoTask _stopTask
		{
			get
			{
				return __stopTask;
			}

			set
			{
				if (__stopTask != value)
				{
					__stopTask = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_stopTask)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoPneumatics.fbCylinder target)
		{
			base.CopyPlainToCyclic(target);
			target.inAtHomePos.Cyclic = inAtHomePos;
			target.inAtWorkPos.Cyclic = inAtWorkPos;
			target.outToHomePos.Cyclic = outToHomePos;
			target.outToWorkPos.Cyclic = outToWorkPos;
			_moveHomeTask.CopyPlainToCyclic(target._moveHomeTask);
			_moveWorkTask.CopyPlainToCyclic(target._moveWorkTask);
			_stopTask.CopyPlainToCyclic(target._stopTask);
		}

		public void CopyPlainToCyclic(TcoPneumatics.IfbCylinder target)
		{
			this.CopyPlainToCyclic((TcoPneumatics.fbCylinder)target);
		}

		public void CopyPlainToShadow(TcoPneumatics.fbCylinder target)
		{
			base.CopyPlainToShadow(target);
			target.inAtHomePos.Shadow = inAtHomePos;
			target.inAtWorkPos.Shadow = inAtWorkPos;
			target.outToHomePos.Shadow = outToHomePos;
			target.outToWorkPos.Shadow = outToWorkPos;
			_moveHomeTask.CopyPlainToShadow(target._moveHomeTask);
			_moveWorkTask.CopyPlainToShadow(target._moveWorkTask);
			_stopTask.CopyPlainToShadow(target._stopTask);
		}

		public void CopyPlainToShadow(TcoPneumatics.IShadowfbCylinder target)
		{
			this.CopyPlainToShadow((TcoPneumatics.fbCylinder)target);
		}

		public void CopyCyclicToPlain(TcoPneumatics.fbCylinder source)
		{
			base.CopyCyclicToPlain(source);
			inAtHomePos = source.inAtHomePos.LastValue;
			inAtWorkPos = source.inAtWorkPos.LastValue;
			outToHomePos = source.outToHomePos.LastValue;
			outToWorkPos = source.outToWorkPos.LastValue;
			_moveHomeTask.CopyCyclicToPlain(source._moveHomeTask);
			_moveWorkTask.CopyCyclicToPlain(source._moveWorkTask);
			_stopTask.CopyCyclicToPlain(source._stopTask);
		}

		public void CopyCyclicToPlain(TcoPneumatics.IfbCylinder source)
		{
			this.CopyCyclicToPlain((TcoPneumatics.fbCylinder)source);
		}

		public void CopyShadowToPlain(TcoPneumatics.fbCylinder source)
		{
			base.CopyShadowToPlain(source);
			inAtHomePos = source.inAtHomePos.Shadow;
			inAtWorkPos = source.inAtWorkPos.Shadow;
			outToHomePos = source.outToHomePos.Shadow;
			outToWorkPos = source.outToWorkPos.Shadow;
			_moveHomeTask.CopyShadowToPlain(source._moveHomeTask);
			_moveWorkTask.CopyShadowToPlain(source._moveWorkTask);
			_stopTask.CopyShadowToPlain(source._stopTask);
		}

		public void CopyShadowToPlain(TcoPneumatics.IShadowfbCylinder source)
		{
			this.CopyShadowToPlain((TcoPneumatics.fbCylinder)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainfbCylinder()
		{
			__moveHomeTask = new TcoCore.PlainTcoTask();
			__moveWorkTask = new TcoCore.PlainTcoTask();
			__stopTask = new TcoCore.PlainTcoTask();
		}
	}
}