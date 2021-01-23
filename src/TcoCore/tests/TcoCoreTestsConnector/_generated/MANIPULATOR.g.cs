using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "MANIPULATOR", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class MANIPULATOR : Vortex.Connector.IVortexObject, IMANIPULATOR, IShadowMANIPULATOR, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		fbManipulatorContext __context;
		public fbManipulatorContext _context
		{
			get
			{
				return __context;
			}
		}

		IfbManipulatorContext IMANIPULATOR._context
		{
			get
			{
				return _context;
			}
		}

		IShadowfbManipulatorContext IShadowMANIPULATOR._context
		{
			get
			{
				return _context;
			}
		}

		public void LazyOnlineToShadow()
		{
			_context.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			_context.LazyShadowToOnline();
		}

		public PlainMANIPULATOR CreatePlainerType()
		{
			var cloned = new PlainMANIPULATOR();
			cloned._context = _context.CreatePlainerType();
			return cloned;
		}

		protected PlainMANIPULATOR CreatePlainerType(PlainMANIPULATOR cloned)
		{
			cloned._context = _context.CreatePlainerType();
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

		public void FlushPlainToOnline(TcoCoreTests.PlainMANIPULATOR source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainMANIPULATOR source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainMANIPULATOR source)
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

		public MANIPULATOR(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@ValueTags = new System.Collections.Generic.List<Vortex.Connector.IValueTag>();
			this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			__context = new fbManipulatorContext(this, "", "_context");
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
			parent.AddChild(this);
		}

		public MANIPULATOR()
		{
			PexPreConstructorParameterless();
			__context = new fbManipulatorContext();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcMANIPULATOR
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcMANIPULATOR()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IMANIPULATOR : Vortex.Connector.IVortexOnlineObject
	{
		IfbManipulatorContext _context
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainMANIPULATOR CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainMANIPULATOR source);
		void FlushOnlineToPlain(TcoCoreTests.PlainMANIPULATOR source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowMANIPULATOR : Vortex.Connector.IVortexShadowObject
	{
		IShadowfbManipulatorContext _context
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainMANIPULATOR CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainMANIPULATOR source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainMANIPULATOR : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		PlainfbManipulatorContext __context;
		public PlainfbManipulatorContext _context
		{
			get
			{
				return __context;
			}

			set
			{
				if (__context != value)
				{
					__context = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_context)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCoreTests.MANIPULATOR target)
		{
			_context.CopyPlainToCyclic(target._context);
		}

		public void CopyPlainToCyclic(TcoCoreTests.IMANIPULATOR target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.MANIPULATOR)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.MANIPULATOR target)
		{
			_context.CopyPlainToShadow(target._context);
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowMANIPULATOR target)
		{
			this.CopyPlainToShadow((TcoCoreTests.MANIPULATOR)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.MANIPULATOR source)
		{
			_context.CopyCyclicToPlain(source._context);
		}

		public void CopyCyclicToPlain(TcoCoreTests.IMANIPULATOR source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.MANIPULATOR)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.MANIPULATOR source)
		{
			_context.CopyShadowToPlain(source._context);
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowMANIPULATOR source)
		{
			this.CopyShadowToPlain((TcoCoreTests.MANIPULATOR)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainMANIPULATOR()
		{
			__context = new PlainfbManipulatorContext();
		}
	}
}