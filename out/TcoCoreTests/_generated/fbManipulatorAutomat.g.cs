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
	public partial class fbManipulatorAutomat : Vortex.Connector.IVortexObject, IfbManipulatorAutomat, IShadowfbManipulatorAutomat, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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

		public void LazyOnlineToShadow()
		{
			_horizontalPiston.LazyOnlineToShadow();
			_verticalPiston.LazyOnlineToShadow();
			_gripperPiston.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			_horizontalPiston.LazyShadowToOnline();
			_verticalPiston.LazyShadowToOnline();
			_gripperPiston.LazyShadowToOnline();
		}

		public PlainfbManipulatorAutomat CreatePlainerType()
		{
			var cloned = new PlainfbManipulatorAutomat();
			cloned._horizontalPiston = _horizontalPiston.CreatePlainerType();
			cloned._verticalPiston = _verticalPiston.CreatePlainerType();
			cloned._gripperPiston = _gripperPiston.CreatePlainerType();
			return cloned;
		}

		protected PlainfbManipulatorAutomat CreatePlainerType(PlainfbManipulatorAutomat cloned)
		{
			cloned._horizontalPiston = _horizontalPiston.CreatePlainerType();
			cloned._verticalPiston = _verticalPiston.CreatePlainerType();
			cloned._gripperPiston = _gripperPiston.CreatePlainerType();
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

		public void FlushPlainToOnline(TcoCoreTests.PlainfbManipulatorAutomat source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainfbManipulatorAutomat source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainfbManipulatorAutomat source)
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

		public fbManipulatorAutomat(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			__horizontalPiston = new fbPiston(this, "", "_horizontalPiston");
			__verticalPiston = new fbPiston(this, "", "_verticalPiston");
			__gripperPiston = new fbPiston(this, "", "_gripperPiston");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public fbManipulatorAutomat()
		{
			PexPreConstructorParameterless();
			__horizontalPiston = new fbPiston();
			__verticalPiston = new fbPiston();
			__gripperPiston = new fbPiston();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcfbManipulatorAutomat
		{
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
	public partial interface IfbManipulatorAutomat : Vortex.Connector.IVortexOnlineObject
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

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainfbManipulatorAutomat CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainfbManipulatorAutomat source);
		void FlushOnlineToPlain(TcoCoreTests.PlainfbManipulatorAutomat source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowfbManipulatorAutomat : Vortex.Connector.IVortexShadowObject
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

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainfbManipulatorAutomat CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainfbManipulatorAutomat source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainfbManipulatorAutomat : Vortex.Connector.IPlain
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