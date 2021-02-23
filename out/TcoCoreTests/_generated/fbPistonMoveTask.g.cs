using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "fbPistonMoveTask", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class fbPistonMoveTask : Vortex.Connector.IVortexObject, IfbPistonMoveTask, IShadowfbPistonMoveTask, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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

		public void LazyOnlineToShadow()
		{
			PositionSensor.Shadow = PositionSensor.LastValue;
		}

		public void LazyShadowToOnline()
		{
			PositionSensor.Cyclic = PositionSensor.Shadow;
		}

		public PlainfbPistonMoveTask CreatePlainerType()
		{
			var cloned = new PlainfbPistonMoveTask();
			return cloned;
		}

		protected PlainfbPistonMoveTask CreatePlainerType(PlainfbPistonMoveTask cloned)
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

		public void FlushPlainToOnline(TcoCoreTests.PlainfbPistonMoveTask source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainfbPistonMoveTask source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainfbPistonMoveTask source)
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

		public fbPistonMoveTask(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			_PositionSensor = @Connector.Online.Adapter.CreateBOOL(this, "", "PositionSensor");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public fbPistonMoveTask()
		{
			PexPreConstructorParameterless();
			_PositionSensor = Vortex.Connector.IConnectorFactory.CreateBOOL();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcfbPistonMoveTask
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
	public partial interface IfbPistonMoveTask : Vortex.Connector.IVortexOnlineObject
	{
		Vortex.Connector.ValueTypes.Online.IOnlineBool PositionSensor
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainfbPistonMoveTask CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainfbPistonMoveTask source);
		void FlushOnlineToPlain(TcoCoreTests.PlainfbPistonMoveTask source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowfbPistonMoveTask : Vortex.Connector.IVortexShadowObject
	{
		Vortex.Connector.ValueTypes.Shadows.IShadowBool PositionSensor
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainfbPistonMoveTask CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainfbPistonMoveTask source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainfbPistonMoveTask : Vortex.Connector.IPlain
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
			target.PositionSensor.Cyclic = PositionSensor;
		}

		public void CopyPlainToCyclic(TcoCoreTests.IfbPistonMoveTask target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.fbPistonMoveTask)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.fbPistonMoveTask target)
		{
			target.PositionSensor.Shadow = PositionSensor;
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowfbPistonMoveTask target)
		{
			this.CopyPlainToShadow((TcoCoreTests.fbPistonMoveTask)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.fbPistonMoveTask source)
		{
			PositionSensor = source.PositionSensor.LastValue;
		}

		public void CopyCyclicToPlain(TcoCoreTests.IfbPistonMoveTask source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.fbPistonMoveTask)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.fbPistonMoveTask source)
		{
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