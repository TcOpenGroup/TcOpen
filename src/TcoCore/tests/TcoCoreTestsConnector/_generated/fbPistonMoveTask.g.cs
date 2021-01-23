using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "fbPistonMoveTask", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class fbPistonMoveTask : TcoCore.TcoTask, Vortex.Connector.IVortexObject, IfbPistonMoveTask, IShadowfbPistonMoveTask, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		Vortex.Connector.ValueTypes.OnlinerBool _PositionSensor;
		public Vortex.Connector.ValueTypes.OnlinerBool PositionSensor
		{
			get
			{
				return _PositionSensor;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool IfbPistonMoveTask.PositionSensor
		{
			get
			{
				return PositionSensor;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowfbPistonMoveTask.PositionSensor
		{
			get
			{
				return PositionSensor;
			}
		}

		public new void LazyOnlineToShadow()
		{
			base.LazyOnlineToShadow();
			PositionSensor.Shadow = PositionSensor.LastValue;
		}

		public new void LazyShadowToOnline()
		{
			base.LazyShadowToOnline();
			PositionSensor.Cyclic = PositionSensor.Shadow;
		}

		public new PlainfbPistonMoveTask CreatePlainerType()
		{
			var cloned = new PlainfbPistonMoveTask();
			base.CreatePlainerType(cloned);
			return cloned;
		}

		protected PlainfbPistonMoveTask CreatePlainerType(PlainfbPistonMoveTask cloned)
		{
			base.CreatePlainerType(cloned);
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
		partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexConstructorParameterless();
		public void FlushPlainToOnline(TcoCoreTests.PlainfbPistonMoveTask source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainfbPistonMoveTask source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainfbPistonMoveTask source)
		{
			this.Read();
			source.CopyCyclicToPlain(this);
		}

		public fbPistonMoveTask(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail): base (parent, readableTail, symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			_PositionSensor = @Connector.Online.Adapter.CreateBOOL(this, "", "PositionSensor");
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
		}

		public fbPistonMoveTask(): base ()
		{
			PexPreConstructorParameterless();
			_PositionSensor = Vortex.Connector.IConnectorFactory.CreateBOOL();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcfbPistonMoveTask : TcoCore.TcoTask.PlcTcoTask
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcfbPistonMoveTask()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IfbPistonMoveTask : Vortex.Connector.IVortexOnlineObject, TcoCore.ITcoTask
	{
		Vortex.Connector.ValueTypes.Online.IOnlineBool PositionSensor
		{
			get;
		}

		new TcoCoreTests.PlainfbPistonMoveTask CreatePlainerType();
		new void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainfbPistonMoveTask source);
		void FlushOnlineToPlain(TcoCoreTests.PlainfbPistonMoveTask source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowfbPistonMoveTask : Vortex.Connector.IVortexShadowObject, TcoCore.IShadowTcoTask
	{
		Vortex.Connector.ValueTypes.Shadows.IShadowBool PositionSensor
		{
			get;
		}

		new TcoCoreTests.PlainfbPistonMoveTask CreatePlainerType();
		new void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainfbPistonMoveTask source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainfbPistonMoveTask : TcoCore.PlainTcoTask
	{
		System.Boolean _PositionSensor;
		public System.Boolean PositionSensor
		{
			get
			{
				return _PositionSensor;
			}

			set
			{
				if (_PositionSensor != value)
				{
					_PositionSensor = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(PositionSensor)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCoreTests.fbPistonMoveTask target)
		{
			base.CopyPlainToCyclic(target);
			target.PositionSensor.Cyclic = PositionSensor;
		}

		public void CopyPlainToCyclic(TcoCoreTests.IfbPistonMoveTask target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.fbPistonMoveTask)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.fbPistonMoveTask target)
		{
			base.CopyPlainToShadow(target);
			target.PositionSensor.Shadow = PositionSensor;
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowfbPistonMoveTask target)
		{
			this.CopyPlainToShadow((TcoCoreTests.fbPistonMoveTask)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.fbPistonMoveTask source)
		{
			base.CopyCyclicToPlain(source);
			PositionSensor = source.PositionSensor.LastValue;
		}

		public void CopyCyclicToPlain(TcoCoreTests.IfbPistonMoveTask source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.fbPistonMoveTask)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.fbPistonMoveTask source)
		{
			base.CopyShadowToPlain(source);
			PositionSensor = source.PositionSensor.Shadow;
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowfbPistonMoveTask source)
		{
			this.CopyShadowToPlain((TcoCoreTests.fbPistonMoveTask)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainfbPistonMoveTask()
		{
		}
	}
}