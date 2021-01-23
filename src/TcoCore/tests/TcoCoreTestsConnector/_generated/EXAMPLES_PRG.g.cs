using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "EXAMPLES_PRG", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class EXAMPLES_PRG : Vortex.Connector.IVortexObject, IEXAMPLES_PRG, IShadowEXAMPLES_PRG, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		fbContext __context;
		public fbContext _context
		{
			get
			{
				return __context;
			}
		}

		IfbContext IEXAMPLES_PRG._context
		{
			get
			{
				return _context;
			}
		}

		IShadowfbContext IShadowEXAMPLES_PRG._context
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

		public PlainEXAMPLES_PRG CreatePlainerType()
		{
			var cloned = new PlainEXAMPLES_PRG();
			cloned._context = _context.CreatePlainerType();
			return cloned;
		}

		protected PlainEXAMPLES_PRG CreatePlainerType(PlainEXAMPLES_PRG cloned)
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

		public void FlushPlainToOnline(TcoCoreTests.PlainEXAMPLES_PRG source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainEXAMPLES_PRG source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainEXAMPLES_PRG source)
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

		public EXAMPLES_PRG(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@ValueTags = new System.Collections.Generic.List<Vortex.Connector.IValueTag>();
			this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			__context = new fbContext(this, "", "_context");
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
			parent.AddChild(this);
		}

		public EXAMPLES_PRG()
		{
			PexPreConstructorParameterless();
			__context = new fbContext();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcEXAMPLES_PRG
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcEXAMPLES_PRG()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IEXAMPLES_PRG : Vortex.Connector.IVortexOnlineObject
	{
		IfbContext _context
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainEXAMPLES_PRG CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainEXAMPLES_PRG source);
		void FlushOnlineToPlain(TcoCoreTests.PlainEXAMPLES_PRG source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowEXAMPLES_PRG : Vortex.Connector.IVortexShadowObject
	{
		IShadowfbContext _context
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainEXAMPLES_PRG CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainEXAMPLES_PRG source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainEXAMPLES_PRG : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		PlainfbContext __context;
		public PlainfbContext _context
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

		public void CopyPlainToCyclic(TcoCoreTests.EXAMPLES_PRG target)
		{
			_context.CopyPlainToCyclic(target._context);
		}

		public void CopyPlainToCyclic(TcoCoreTests.IEXAMPLES_PRG target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.EXAMPLES_PRG)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.EXAMPLES_PRG target)
		{
			_context.CopyPlainToShadow(target._context);
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowEXAMPLES_PRG target)
		{
			this.CopyPlainToShadow((TcoCoreTests.EXAMPLES_PRG)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.EXAMPLES_PRG source)
		{
			_context.CopyCyclicToPlain(source._context);
		}

		public void CopyCyclicToPlain(TcoCoreTests.IEXAMPLES_PRG source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.EXAMPLES_PRG)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.EXAMPLES_PRG source)
		{
			_context.CopyShadowToPlain(source._context);
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowEXAMPLES_PRG source)
		{
			this.CopyShadowToPlain((TcoCoreTests.EXAMPLES_PRG)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainEXAMPLES_PRG()
		{
			__context = new PlainfbContext();
		}
	}
}