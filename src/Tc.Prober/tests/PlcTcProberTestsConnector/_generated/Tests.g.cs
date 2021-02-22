using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace PlcTcProberTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "Tests", "PlcTcProberTests", TypeComplexityEnum.Complex)]
	public partial class Tests : Vortex.Connector.IVortexObject, ITests, IShadowTests, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
				return PlcTcProberTestsTwinController.Translator.Translate(_humanReadable).Interpolate(this);
			}

			protected set
			{
				_humanReadable = value;
			}
		}

		protected string _humanReadable;
		fbBasicRunnerTests __basicRunnerTests;
		public fbBasicRunnerTests _basicRunnerTests
		{
			get
			{
				return __basicRunnerTests;
			}
		}

		IfbBasicRunnerTests ITests._basicRunnerTests
		{
			get
			{
				return _basicRunnerTests;
			}
		}

		IShadowfbBasicRunnerTests IShadowTests._basicRunnerTests
		{
			get
			{
				return _basicRunnerTests;
			}
		}

		fbRecorderRunnerTests __recorderRunnerTests;
		public fbRecorderRunnerTests _recorderRunnerTests
		{
			get
			{
				return __recorderRunnerTests;
			}
		}

		IfbRecorderRunnerTests ITests._recorderRunnerTests
		{
			get
			{
				return _recorderRunnerTests;
			}
		}

		IShadowfbRecorderRunnerTests IShadowTests._recorderRunnerTests
		{
			get
			{
				return _recorderRunnerTests;
			}
		}

		public void LazyOnlineToShadow()
		{
			_basicRunnerTests.LazyOnlineToShadow();
			_recorderRunnerTests.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			_basicRunnerTests.LazyShadowToOnline();
			_recorderRunnerTests.LazyShadowToOnline();
		}

		public PlainTests CreatePlainerType()
		{
			var cloned = new PlainTests();
			cloned._basicRunnerTests = _basicRunnerTests.CreatePlainerType();
			cloned._recorderRunnerTests = _recorderRunnerTests.CreatePlainerType();
			return cloned;
		}

		protected PlainTests CreatePlainerType(PlainTests cloned)
		{
			cloned._basicRunnerTests = _basicRunnerTests.CreatePlainerType();
			cloned._recorderRunnerTests = _recorderRunnerTests.CreatePlainerType();
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

		public void FlushPlainToOnline(PlcTcProberTests.PlainTests source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(PlcTcProberTests.PlainTests source)
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

		public void FlushOnlineToPlain(PlcTcProberTests.PlainTests source)
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
				return PlcTcProberTestsTwinController.Translator.Translate(_AttributeName).Interpolate(this);
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

		public Tests(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			__basicRunnerTests = new fbBasicRunnerTests(this, "", "_basicRunnerTests");
			__recorderRunnerTests = new fbRecorderRunnerTests(this, "", "_recorderRunnerTests");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public Tests()
		{
			PexPreConstructorParameterless();
			__basicRunnerTests = new fbBasicRunnerTests();
			__recorderRunnerTests = new fbRecorderRunnerTests();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTests
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTests()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITests : Vortex.Connector.IVortexOnlineObject
	{
		IfbBasicRunnerTests _basicRunnerTests
		{
			get;
		}

		IfbRecorderRunnerTests _recorderRunnerTests
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		PlcTcProberTests.PlainTests CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(PlcTcProberTests.PlainTests source);
		void FlushOnlineToPlain(PlcTcProberTests.PlainTests source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTests : Vortex.Connector.IVortexShadowObject
	{
		IShadowfbBasicRunnerTests _basicRunnerTests
		{
			get;
		}

		IShadowfbRecorderRunnerTests _recorderRunnerTests
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		PlcTcProberTests.PlainTests CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(PlcTcProberTests.PlainTests source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTests : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		PlainfbBasicRunnerTests __basicRunnerTests;
		public PlainfbBasicRunnerTests _basicRunnerTests
		{
			get
			{
				return __basicRunnerTests;
			}

			set
			{
				if (__basicRunnerTests != value)
				{
					__basicRunnerTests = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_basicRunnerTests)));
				}
			}
		}

		PlainfbRecorderRunnerTests __recorderRunnerTests;
		public PlainfbRecorderRunnerTests _recorderRunnerTests
		{
			get
			{
				return __recorderRunnerTests;
			}

			set
			{
				if (__recorderRunnerTests != value)
				{
					__recorderRunnerTests = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_recorderRunnerTests)));
				}
			}
		}

		public void CopyPlainToCyclic(PlcTcProberTests.Tests target)
		{
			_basicRunnerTests.CopyPlainToCyclic(target._basicRunnerTests);
			_recorderRunnerTests.CopyPlainToCyclic(target._recorderRunnerTests);
		}

		public void CopyPlainToCyclic(PlcTcProberTests.ITests target)
		{
			this.CopyPlainToCyclic((PlcTcProberTests.Tests)target);
		}

		public void CopyPlainToShadow(PlcTcProberTests.Tests target)
		{
			_basicRunnerTests.CopyPlainToShadow(target._basicRunnerTests);
			_recorderRunnerTests.CopyPlainToShadow(target._recorderRunnerTests);
		}

		public void CopyPlainToShadow(PlcTcProberTests.IShadowTests target)
		{
			this.CopyPlainToShadow((PlcTcProberTests.Tests)target);
		}

		public void CopyCyclicToPlain(PlcTcProberTests.Tests source)
		{
			_basicRunnerTests.CopyCyclicToPlain(source._basicRunnerTests);
			_recorderRunnerTests.CopyCyclicToPlain(source._recorderRunnerTests);
		}

		public void CopyCyclicToPlain(PlcTcProberTests.ITests source)
		{
			this.CopyCyclicToPlain((PlcTcProberTests.Tests)source);
		}

		public void CopyShadowToPlain(PlcTcProberTests.Tests source)
		{
			_basicRunnerTests.CopyShadowToPlain(source._basicRunnerTests);
			_recorderRunnerTests.CopyShadowToPlain(source._recorderRunnerTests);
		}

		public void CopyShadowToPlain(PlcTcProberTests.IShadowTests source)
		{
			this.CopyShadowToPlain((PlcTcProberTests.Tests)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTests()
		{
			__basicRunnerTests = new PlainfbBasicRunnerTests();
			__recorderRunnerTests = new PlainfbRecorderRunnerTests();
		}
	}
}