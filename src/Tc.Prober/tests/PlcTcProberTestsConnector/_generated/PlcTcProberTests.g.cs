using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;
using PlcTcProberTestsConnector.Properties;

[assembly: Vortex.Connector.Attributes.AssemblyPlcCounterPart("{\r\n  \"Types\": [\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"fbBasicRunnerTests\",\r\n      \"Namespace\": \"PlcTcProberTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"fbRecorderRunnerTests\",\r\n      \"Namespace\": \"PlcTcProberTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"fbInheritanceLevel_1\",\r\n      \"Namespace\": \"PlcTcProberTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"fbInheritanceLevel_2\",\r\n      \"Namespace\": \"PlcTcProberTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"fbInheritanceLevel_3\",\r\n      \"Namespace\": \"PlcTcProberTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"fbInheritanceLevel_4\",\r\n      \"Namespace\": \"PlcTcProberTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"fbInheritanceLevel_5\",\r\n      \"Namespace\": \"PlcTcProberTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"fbRootLevelStruct\",\r\n      \"Namespace\": \"PlcTcProberTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"stRecorder\",\r\n      \"Namespace\": \"PlcTcProberTests\",\r\n      \"TypeMetaInfo\": 1\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Interpolated \\\"|[[2]AttributeName]|-|[[1]AttributeName]|-|[AttributeName]||[BOOL_val.AttributeName]|\\\" }\\n{attribute addProperty Name \\\"ALL BASE TYPES\\\" }\",\r\n      \"TypeName\": \"stAllTypes\",\r\n      \"Namespace\": \"PlcTcProberTests\",\r\n      \"TypeMetaInfo\": 1\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"Tests\",\r\n      \"Namespace\": \"PlcTcProberTests\",\r\n      \"TypeMetaInfo\": 0\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"MAIN\",\r\n      \"Namespace\": \"PlcTcProberTests\",\r\n      \"TypeMetaInfo\": 3\r\n    }\r\n  ],\r\n  \"Name\": \"PlcTcProberTests\",\r\n  \"Namespace\": \"PlcTcProberTests\"\r\n}")]
#pragma warning disable SA1402, CS1591, CS0108, CS0067
namespace PlcTcProberTests
{
	public partial class PlcTcProberTestsTwinController : Vortex.Connector.ITwinController, IPlcTcProberTestsTwinController, IShadowPlcTcProberTestsTwinController
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
		Tests _Tests;
		public Tests Tests
		{
			get
			{
				return _Tests;
			}
		}

		ITests IPlcTcProberTestsTwinController.Tests
		{
			get
			{
				return Tests;
			}
		}

		IShadowTests IShadowPlcTcProberTestsTwinController.Tests
		{
			get
			{
				return Tests;
			}
		}

		MAIN _MAIN;
		public MAIN MAIN
		{
			get
			{
				return _MAIN;
			}
		}

		IMAIN IPlcTcProberTestsTwinController.MAIN
		{
			get
			{
				return MAIN;
			}
		}

		IShadowMAIN IShadowPlcTcProberTestsTwinController.MAIN
		{
			get
			{
				return MAIN;
			}
		}

		public void LazyOnlineToShadow()
		{
			Tests.LazyOnlineToShadow();
			MAIN.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			Tests.LazyShadowToOnline();
			MAIN.LazyShadowToOnline();
		}

		public PlainPlcTcProberTestsTwinController CreatePlainerType()
		{
			var cloned = new PlainPlcTcProberTestsTwinController();
			cloned.Tests = Tests.CreatePlainerType();
			cloned.MAIN = MAIN.CreatePlainerType();
			return cloned;
		}

		protected PlainPlcTcProberTestsTwinController CreatePlainerType(PlainPlcTcProberTestsTwinController cloned)
		{
			cloned.Tests = Tests.CreatePlainerType();
			cloned.MAIN = MAIN.CreatePlainerType();
			return cloned;
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

		public IPlcTcProberTestsTwinController Online
		{
			get
			{
				return (IPlcTcProberTestsTwinController)this;
			}
		}

		public IShadowPlcTcProberTestsTwinController Shadow
		{
			get
			{
				return (IShadowPlcTcProberTestsTwinController)this;
			}
		}

		public Vortex.Connector.IConnector Connector
		{
			get;
			set;
		}

		public PlcTcProberTestsTwinController()
		{
			var adapter = new Vortex.Connector.ConnectorAdapter(typeof (DummyConnectorFactory));
			this.Connector = adapter.GetConnector(new object[]{});
			_Tests = new Tests(this.Connector, "", "Tests");
			_MAIN = new MAIN(this.Connector, "", "MAIN");
		}

		public PlcTcProberTestsTwinController(Vortex.Connector.ConnectorAdapter adapter, object[] parameters)
		{
			this.Connector = adapter.GetConnector(parameters);
			_Tests = new Tests(this.Connector, "", "Tests");
			_MAIN = new MAIN(this.Connector, "", "MAIN");
		}

		public PlcTcProberTestsTwinController(Vortex.Connector.ConnectorAdapter adapter)
		{
			this.Connector = adapter.GetConnector(adapter.Parameters);
			_Tests = new Tests(this.Connector, "", "Tests");
			_MAIN = new MAIN(this.Connector, "", "MAIN");
		}

		public static string LocalizationDirectory
		{
			get;
			set;
		}

		private static Vortex.Localizations.Abstractions.ITranslator _translator
		{
			get;
			set;
		}

		internal static Vortex.Localizations.Abstractions.ITranslator Translator
		{
			get
			{
				if (_translator == null)
				{
					_translator = Vortex.Localizations.Abstractions.ITranslator.Get(typeof (Localizations));
				} return  _translator ; 

			}
		}
	}

	public partial interface IPlcTcProberTestsTwinController
	{
		ITests Tests
		{
			get;
		}

		IMAIN MAIN
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		PlcTcProberTests.PlainPlcTcProberTestsTwinController CreatePlainerType();
	}

	public partial interface IShadowPlcTcProberTestsTwinController
	{
		IShadowTests Tests
		{
			get;
		}

		IShadowMAIN MAIN
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		PlcTcProberTests.PlainPlcTcProberTestsTwinController CreatePlainerType();
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainPlcTcProberTestsTwinController : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		PlainTests _Tests;
		public PlainTests Tests
		{
			get
			{
				return _Tests;
			}

			set
			{
				if (_Tests != value)
				{
					_Tests = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Tests)));
				}
			}
		}

		PlainMAIN _MAIN;
		public PlainMAIN MAIN
		{
			get
			{
				return _MAIN;
			}

			set
			{
				if (_MAIN != value)
				{
					_MAIN = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(MAIN)));
				}
			}
		}

		public void CopyPlainToCyclic(PlcTcProberTests.PlcTcProberTestsTwinController target)
		{
			Tests.CopyPlainToCyclic(target.Tests);
			MAIN.CopyPlainToCyclic(target.MAIN);
		}

		public void CopyPlainToCyclic(PlcTcProberTests.IPlcTcProberTestsTwinController target)
		{
			this.CopyPlainToCyclic((PlcTcProberTests.PlcTcProberTestsTwinController)target);
		}

		public void CopyPlainToShadow(PlcTcProberTests.PlcTcProberTestsTwinController target)
		{
			Tests.CopyPlainToShadow(target.Tests);
			MAIN.CopyPlainToShadow(target.MAIN);
		}

		public void CopyPlainToShadow(PlcTcProberTests.IShadowPlcTcProberTestsTwinController target)
		{
			this.CopyPlainToShadow((PlcTcProberTests.PlcTcProberTestsTwinController)target);
		}

		public void CopyCyclicToPlain(PlcTcProberTests.PlcTcProberTestsTwinController source)
		{
			Tests.CopyCyclicToPlain(source.Tests);
			MAIN.CopyCyclicToPlain(source.MAIN);
		}

		public void CopyCyclicToPlain(PlcTcProberTests.IPlcTcProberTestsTwinController source)
		{
			this.CopyCyclicToPlain((PlcTcProberTests.PlcTcProberTestsTwinController)source);
		}

		public void CopyShadowToPlain(PlcTcProberTests.PlcTcProberTestsTwinController source)
		{
			Tests.CopyShadowToPlain(source.Tests);
			MAIN.CopyShadowToPlain(source.MAIN);
		}

		public void CopyShadowToPlain(PlcTcProberTests.IShadowPlcTcProberTestsTwinController source)
		{
			this.CopyShadowToPlain((PlcTcProberTests.PlcTcProberTestsTwinController)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainPlcTcProberTestsTwinController()
		{
			_Tests = new PlainTests();
			_MAIN = new PlainMAIN();
		}
	}
}