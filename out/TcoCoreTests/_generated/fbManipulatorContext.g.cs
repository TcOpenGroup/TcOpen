using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "fbManipulatorContext", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class fbManipulatorContext : Vortex.Connector.IVortexObject, IfbManipulatorContext, IShadowfbManipulatorContext, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		fbManipulatorAutomat __automat;
		public fbManipulatorAutomat _automat
		{
			get
			{
				return __automat;
			}
		}

		IfbManipulatorAutomat IfbManipulatorContext._automat
		{
			get
			{
				return _automat;
			}
		}

		IShadowfbManipulatorAutomat IShadowfbManipulatorContext._automat
		{
			get
			{
				return _automat;
			}
		}

		public void LazyOnlineToShadow()
		{
			_automat.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			_automat.LazyShadowToOnline();
		}

		public PlainfbManipulatorContext CreatePlainerType()
		{
			var cloned = new PlainfbManipulatorContext();
			cloned._automat = _automat.CreatePlainerType();
			return cloned;
		}

		protected PlainfbManipulatorContext CreatePlainerType(PlainfbManipulatorContext cloned)
		{
			cloned._automat = _automat.CreatePlainerType();
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

		public void FlushPlainToOnline(TcoCoreTests.PlainfbManipulatorContext source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainfbManipulatorContext source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainfbManipulatorContext source)
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

		public fbManipulatorContext(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			__automat = new fbManipulatorAutomat(this, "", "_automat");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public fbManipulatorContext()
		{
			PexPreConstructorParameterless();
			__automat = new fbManipulatorAutomat();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcfbManipulatorContext
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcfbManipulatorContext()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IfbManipulatorContext : Vortex.Connector.IVortexOnlineObject
	{
		IfbManipulatorAutomat _automat
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainfbManipulatorContext CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainfbManipulatorContext source);
		void FlushOnlineToPlain(TcoCoreTests.PlainfbManipulatorContext source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowfbManipulatorContext : Vortex.Connector.IVortexShadowObject
	{
		IShadowfbManipulatorAutomat _automat
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainfbManipulatorContext CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainfbManipulatorContext source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainfbManipulatorContext : Vortex.Connector.IPlain
	{
		PlainfbManipulatorAutomat __automat;
		public PlainfbManipulatorAutomat _automat
		{
			get
			{
				return __automat;
			}

			set
			{
				if (__automat != value)
				{
					__automat = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_automat)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCoreTests.fbManipulatorContext target)
		{
			_automat.CopyPlainToCyclic(target._automat);
		}

		public void CopyPlainToCyclic(TcoCoreTests.IfbManipulatorContext target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.fbManipulatorContext)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.fbManipulatorContext target)
		{
			_automat.CopyPlainToShadow(target._automat);
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowfbManipulatorContext target)
		{
			this.CopyPlainToShadow((TcoCoreTests.fbManipulatorContext)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.fbManipulatorContext source)
		{
			_automat.CopyCyclicToPlain(source._automat);
		}

		public void CopyCyclicToPlain(TcoCoreTests.IfbManipulatorContext source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.fbManipulatorContext)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.fbManipulatorContext source)
		{
			_automat.CopyShadowToPlain(source._automat);
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowfbManipulatorContext source)
		{
			this.CopyShadowToPlain((TcoCoreTests.fbManipulatorContext)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainfbManipulatorContext()
		{
			__automat = new PlainfbManipulatorAutomat();
		}
	}
}