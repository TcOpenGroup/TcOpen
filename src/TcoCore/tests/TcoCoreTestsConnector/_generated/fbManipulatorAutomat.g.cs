using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "fbManipulatorAutomat", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class fbManipulatorAutomat : TcoCore.TcoState, Vortex.Connector.IVortexObject, IfbManipulatorAutomat, IShadowfbManipulatorAutomat, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		fbPiston __horizontalPiston;
		public fbPiston _horizontalPiston
		{
			get
			{
				return __horizontalPiston;
			}
		}

		IfbPiston IfbManipulatorAutomat._horizontalPiston
		{
			get
			{
				return _horizontalPiston;
			}
		}

		IShadowfbPiston IShadowfbManipulatorAutomat._horizontalPiston
		{
			get
			{
				return _horizontalPiston;
			}
		}

		fbPiston __verticalPiston;
		public fbPiston _verticalPiston
		{
			get
			{
				return __verticalPiston;
			}
		}

		IfbPiston IfbManipulatorAutomat._verticalPiston
		{
			get
			{
				return _verticalPiston;
			}
		}

		IShadowfbPiston IShadowfbManipulatorAutomat._verticalPiston
		{
			get
			{
				return _verticalPiston;
			}
		}

		fbPiston __gripperPiston;
		public fbPiston _gripperPiston
		{
			get
			{
				return __gripperPiston;
			}
		}

		IfbPiston IfbManipulatorAutomat._gripperPiston
		{
			get
			{
				return _gripperPiston;
			}
		}

		IShadowfbPiston IShadowfbManipulatorAutomat._gripperPiston
		{
			get
			{
				return _gripperPiston;
			}
		}

		public new void LazyOnlineToShadow()
		{
			base.LazyOnlineToShadow();
			_horizontalPiston.LazyOnlineToShadow();
			_verticalPiston.LazyOnlineToShadow();
			_gripperPiston.LazyOnlineToShadow();
		}

		public new void LazyShadowToOnline()
		{
			base.LazyShadowToOnline();
			_horizontalPiston.LazyShadowToOnline();
			_verticalPiston.LazyShadowToOnline();
			_gripperPiston.LazyShadowToOnline();
		}

		public new PlainfbManipulatorAutomat CreatePlainerType()
		{
			var cloned = new PlainfbManipulatorAutomat();
			base.CreatePlainerType(cloned);
			cloned._horizontalPiston = _horizontalPiston.CreatePlainerType();
			cloned._verticalPiston = _verticalPiston.CreatePlainerType();
			cloned._gripperPiston = _gripperPiston.CreatePlainerType();
			return cloned;
		}

		protected PlainfbManipulatorAutomat CreatePlainerType(PlainfbManipulatorAutomat cloned)
		{
			base.CreatePlainerType(cloned);
			cloned._horizontalPiston = _horizontalPiston.CreatePlainerType();
			cloned._verticalPiston = _verticalPiston.CreatePlainerType();
			cloned._gripperPiston = _gripperPiston.CreatePlainerType();
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
		partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexConstructorParameterless();
		public void FlushPlainToOnline(TcoCoreTests.PlainfbManipulatorAutomat source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainfbManipulatorAutomat source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainfbManipulatorAutomat source)
		{
			this.Read();
			source.CopyCyclicToPlain(this);
		}

		public fbManipulatorAutomat(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail): base (parent, readableTail, symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			__horizontalPiston = new fbPiston(this, "", "_horizontalPiston");
			__verticalPiston = new fbPiston(this, "", "_verticalPiston");
			__gripperPiston = new fbPiston(this, "", "_gripperPiston");
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
		}

		public fbManipulatorAutomat(): base ()
		{
			PexPreConstructorParameterless();
			__horizontalPiston = new fbPiston();
			__verticalPiston = new fbPiston();
			__gripperPiston = new fbPiston();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcfbManipulatorAutomat : TcoCore.TcoState.PlcTcoState
		{
			public PlainfbPiston _horizontalPiston;
			public PlainfbPiston _verticalPiston;
			public PlainfbPiston _gripperPiston;
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcfbManipulatorAutomat()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IfbManipulatorAutomat : Vortex.Connector.IVortexOnlineObject, TcoCore.ITcoState
	{
		IfbPiston _horizontalPiston
		{
			get;
		}

		IfbPiston _verticalPiston
		{
			get;
		}

		IfbPiston _gripperPiston
		{
			get;
		}

		new TcoCoreTests.PlainfbManipulatorAutomat CreatePlainerType();
		new void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainfbManipulatorAutomat source);
		void FlushOnlineToPlain(TcoCoreTests.PlainfbManipulatorAutomat source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowfbManipulatorAutomat : Vortex.Connector.IVortexShadowObject, TcoCore.IShadowTcoState
	{
		IShadowfbPiston _horizontalPiston
		{
			get;
		}

		IShadowfbPiston _verticalPiston
		{
			get;
		}

		IShadowfbPiston _gripperPiston
		{
			get;
		}

		new TcoCoreTests.PlainfbManipulatorAutomat CreatePlainerType();
		new void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainfbManipulatorAutomat source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainfbManipulatorAutomat : TcoCore.PlainTcoState
	{
		PlainfbPiston __horizontalPiston;
		public PlainfbPiston _horizontalPiston
		{
			get
			{
				return __horizontalPiston;
			}

			set
			{
				if (__horizontalPiston != value)
				{
					__horizontalPiston = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_horizontalPiston)));
				}
			}
		}

		PlainfbPiston __verticalPiston;
		public PlainfbPiston _verticalPiston
		{
			get
			{
				return __verticalPiston;
			}

			set
			{
				if (__verticalPiston != value)
				{
					__verticalPiston = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_verticalPiston)));
				}
			}
		}

		PlainfbPiston __gripperPiston;
		public PlainfbPiston _gripperPiston
		{
			get
			{
				return __gripperPiston;
			}

			set
			{
				if (__gripperPiston != value)
				{
					__gripperPiston = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_gripperPiston)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCoreTests.fbManipulatorAutomat target)
		{
			base.CopyPlainToCyclic(target);
			_horizontalPiston.CopyPlainToCyclic(target._horizontalPiston);
			_verticalPiston.CopyPlainToCyclic(target._verticalPiston);
			_gripperPiston.CopyPlainToCyclic(target._gripperPiston);
		}

		public void CopyPlainToCyclic(TcoCoreTests.IfbManipulatorAutomat target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.fbManipulatorAutomat)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.fbManipulatorAutomat target)
		{
			base.CopyPlainToShadow(target);
			_horizontalPiston.CopyPlainToShadow(target._horizontalPiston);
			_verticalPiston.CopyPlainToShadow(target._verticalPiston);
			_gripperPiston.CopyPlainToShadow(target._gripperPiston);
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowfbManipulatorAutomat target)
		{
			this.CopyPlainToShadow((TcoCoreTests.fbManipulatorAutomat)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.fbManipulatorAutomat source)
		{
			base.CopyCyclicToPlain(source);
			_horizontalPiston.CopyCyclicToPlain(source._horizontalPiston);
			_verticalPiston.CopyCyclicToPlain(source._verticalPiston);
			_gripperPiston.CopyCyclicToPlain(source._gripperPiston);
		}

		public void CopyCyclicToPlain(TcoCoreTests.IfbManipulatorAutomat source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.fbManipulatorAutomat)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.fbManipulatorAutomat source)
		{
			base.CopyShadowToPlain(source);
			_horizontalPiston.CopyShadowToPlain(source._horizontalPiston);
			_verticalPiston.CopyShadowToPlain(source._verticalPiston);
			_gripperPiston.CopyShadowToPlain(source._gripperPiston);
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowfbManipulatorAutomat source)
		{
			this.CopyShadowToPlain((TcoCoreTests.fbManipulatorAutomat)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainfbManipulatorAutomat()
		{
			__horizontalPiston = new PlainfbPiston();
			__verticalPiston = new PlainfbPiston();
			__gripperPiston = new PlainfbPiston();
		}
	}
}