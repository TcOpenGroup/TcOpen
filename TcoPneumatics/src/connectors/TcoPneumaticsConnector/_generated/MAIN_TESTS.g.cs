using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoPneumatics
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "MAIN_TESTS", "TcoPneumatics", TypeComplexityEnum.Complex)]
	internal partial class MAIN_TESTS : Vortex.Connector.IVortexObject, IMAIN_TESTS, IShadowMAIN_TESTS, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
				return TcoPneumaticsTwinController.Translator.Translate(_humanReadable).Interpolate(this);
			}

			protected set
			{
				_humanReadable = value;
			}
		}

		protected string _humanReadable;
		fbTaskTests __taskTests;
		internal fbTaskTests _taskTests
		{
			get
			{
				return __taskTests;
			}
		}

		public void LazyOnlineToShadow()
		{
		}

		public void LazyShadowToOnline()
		{
		}

		public PlainMAIN_TESTS CreatePlainerType()
		{
			var cloned = new PlainMAIN_TESTS();
			cloned._taskTests = _taskTests.CreatePlainerType();
			return cloned;
		}

		protected PlainMAIN_TESTS CreatePlainerType(PlainMAIN_TESTS cloned)
		{
			cloned._taskTests = _taskTests.CreatePlainerType();
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

		public void FlushPlainToOnline(TcoPneumatics.PlainMAIN_TESTS source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoPneumatics.PlainMAIN_TESTS source)
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

		public void FlushOnlineToPlain(TcoPneumatics.PlainMAIN_TESTS source)
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
				return TcoPneumaticsTwinController.Translator.Translate(_AttributeName).Interpolate(this);
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

		internal MAIN_TESTS(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@ValueTags = new System.Collections.Generic.List<Vortex.Connector.IValueTag>();
			this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			__taskTests = new fbTaskTests(this, "", "_taskTests");
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
			parent.AddChild(this);
		}

		internal MAIN_TESTS()
		{
			PexPreConstructorParameterless();
			__taskTests = new fbTaskTests();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcMAIN_TESTS
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcMAIN_TESTS()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	internal partial interface IMAIN_TESTS : Vortex.Connector.IVortexOnlineObject
	{
		System.String AttributeName
		{
			get;
		}

		TcoPneumatics.PlainMAIN_TESTS CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoPneumatics.PlainMAIN_TESTS source);
		void FlushOnlineToPlain(TcoPneumatics.PlainMAIN_TESTS source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	internal partial interface IShadowMAIN_TESTS : Vortex.Connector.IVortexShadowObject
	{
		System.String AttributeName
		{
			get;
		}

		TcoPneumatics.PlainMAIN_TESTS CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoPneumatics.PlainMAIN_TESTS source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	internal partial class PlainMAIN_TESTS : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		PlainfbTaskTests __taskTests;
		internal PlainfbTaskTests _taskTests
		{
			get
			{
				return __taskTests;
			}

			set
			{
				if (__taskTests != value)
				{
					__taskTests = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_taskTests)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoPneumatics.MAIN_TESTS target)
		{
			_taskTests.CopyPlainToCyclic(target._taskTests);
		}

		public void CopyPlainToCyclic(TcoPneumatics.IMAIN_TESTS target)
		{
			this.CopyPlainToCyclic((TcoPneumatics.MAIN_TESTS)target);
		}

		public void CopyPlainToShadow(TcoPneumatics.MAIN_TESTS target)
		{
			_taskTests.CopyPlainToShadow(target._taskTests);
		}

		public void CopyPlainToShadow(TcoPneumatics.IShadowMAIN_TESTS target)
		{
			this.CopyPlainToShadow((TcoPneumatics.MAIN_TESTS)target);
		}

		public void CopyCyclicToPlain(TcoPneumatics.MAIN_TESTS source)
		{
			_taskTests.CopyCyclicToPlain(source._taskTests);
		}

		public void CopyCyclicToPlain(TcoPneumatics.IMAIN_TESTS source)
		{
			this.CopyCyclicToPlain((TcoPneumatics.MAIN_TESTS)source);
		}

		public void CopyShadowToPlain(TcoPneumatics.MAIN_TESTS source)
		{
			_taskTests.CopyShadowToPlain(source._taskTests);
		}

		public void CopyShadowToPlain(TcoPneumatics.IShadowMAIN_TESTS source)
		{
			this.CopyShadowToPlain((TcoPneumatics.MAIN_TESTS)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainMAIN_TESTS()
		{
			__taskTests = new PlainfbTaskTests();
		}
	}
}