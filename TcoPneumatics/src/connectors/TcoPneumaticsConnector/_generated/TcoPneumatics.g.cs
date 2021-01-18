using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;
using TcoPneumaticsConnector.Properties;

[assembly: Vortex.Connector.Attributes.AssemblyPlcCounterPart("{\r\n  \"Types\": [\r\n    {\r\n      \"TypeAttributes\": \"\",\r\n      \"TypeName\": \"eTaskState\",\r\n      \"Namespace\": \"TcoPneumatics\",\r\n      \"TypeMetaInfo\": 5\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"fbComponent\",\r\n      \"Namespace\": \"TcoPneumatics\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"fbCylinder\",\r\n      \"Namespace\": \"TcoPneumatics\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"FuseChannelBasic\",\r\n      \"Namespace\": \"TcoPneumatics\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"FuseChannelExtended\",\r\n      \"Namespace\": \"TcoPneumatics\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"FuseModuleEL922x\",\r\n      \"Namespace\": \"TcoPneumatics\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"fbTask\",\r\n      \"Namespace\": \"TcoPneumatics\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"fbTaskTests\",\r\n      \"Namespace\": \"TcoPneumatics\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"EL922x_ChannelBasic\",\r\n      \"Namespace\": \"TcoPneumatics\",\r\n      \"TypeMetaInfo\": 1\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"EL922x_ChannelExtended\",\r\n      \"Namespace\": \"TcoPneumatics\",\r\n      \"TypeMetaInfo\": 1\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"OutputStruct\",\r\n      \"Namespace\": \"TcoPneumatics\",\r\n      \"TypeMetaInfo\": 1\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"StatusStructBasic\",\r\n      \"Namespace\": \"TcoPneumatics\",\r\n      \"TypeMetaInfo\": 1\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"StatusStructExtended\",\r\n      \"Namespace\": \"TcoPneumatics\",\r\n      \"TypeMetaInfo\": 1\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"IO\",\r\n      \"Namespace\": \"TcoPneumatics\",\r\n      \"TypeMetaInfo\": 0\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"Global_Version\",\r\n      \"Namespace\": \"TcoPneumatics\",\r\n      \"TypeMetaInfo\": 0\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"MAIN\",\r\n      \"Namespace\": \"TcoPneumatics\",\r\n      \"TypeMetaInfo\": 3\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"MAIN_TESTS\",\r\n      \"Namespace\": \"TcoPneumatics\",\r\n      \"TypeMetaInfo\": 3\r\n    }\r\n  ],\r\n  \"Name\": \"TcoPneumatics\",\r\n  \"Namespace\": \"TcoPneumatics\"\r\n}")]
namespace TcoPneumatics
{
	public partial class TcoPneumaticsTwinController : Vortex.Connector.ITwinController, ITcoPneumaticsTwinController, IShadowTcoPneumaticsTwinController
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
		IO _IO;
		public IO IO
		{
			get
			{
				return _IO;
			}
		}

		IIO ITcoPneumaticsTwinController.IO
		{
			get
			{
				return IO;
			}
		}

		IShadowIO IShadowTcoPneumaticsTwinController.IO
		{
			get
			{
				return IO;
			}
		}

		Global_Version _Global_Version;
		public Global_Version Global_Version
		{
			get
			{
				return _Global_Version;
			}
		}

		IGlobal_Version ITcoPneumaticsTwinController.Global_Version
		{
			get
			{
				return Global_Version;
			}
		}

		IShadowGlobal_Version IShadowTcoPneumaticsTwinController.Global_Version
		{
			get
			{
				return Global_Version;
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

		IMAIN ITcoPneumaticsTwinController.MAIN
		{
			get
			{
				return MAIN;
			}
		}

		IShadowMAIN IShadowTcoPneumaticsTwinController.MAIN
		{
			get
			{
				return MAIN;
			}
		}

		MAIN_TESTS _MAIN_TESTS;
		internal MAIN_TESTS MAIN_TESTS
		{
			get
			{
				return _MAIN_TESTS;
			}
		}

		public void LazyOnlineToShadow()
		{
			IO.LazyOnlineToShadow();
			Global_Version.LazyOnlineToShadow();
			MAIN.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			IO.LazyShadowToOnline();
			Global_Version.LazyShadowToOnline();
			MAIN.LazyShadowToOnline();
		}

		public PlainTcoPneumaticsTwinController CreatePlainerType()
		{
			var cloned = new PlainTcoPneumaticsTwinController();
			cloned.IO = IO.CreatePlainerType();
			cloned.Global_Version = Global_Version.CreatePlainerType();
			cloned.MAIN = MAIN.CreatePlainerType();
			cloned.MAIN_TESTS = MAIN_TESTS.CreatePlainerType();
			return cloned;
		}

		protected PlainTcoPneumaticsTwinController CreatePlainerType(PlainTcoPneumaticsTwinController cloned)
		{
			cloned.IO = IO.CreatePlainerType();
			cloned.Global_Version = Global_Version.CreatePlainerType();
			cloned.MAIN = MAIN.CreatePlainerType();
			cloned.MAIN_TESTS = MAIN_TESTS.CreatePlainerType();
			return cloned;
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

		public ITcoPneumaticsTwinController Online
		{
			get
			{
				return (ITcoPneumaticsTwinController)this;
			}
		}

		public IShadowTcoPneumaticsTwinController Shadow
		{
			get
			{
				return (IShadowTcoPneumaticsTwinController)this;
			}
		}

		public Vortex.Connector.IConnector Connector
		{
			get;
			set;
		}

		public TcoPneumaticsTwinController()
		{
			var adapter = new Vortex.Connector.ConnectorAdapter(typeof (DummyConnectorFactory));
			this.Connector = adapter.GetConnector(new object[]{});
			_IO = new IO(this.Connector, "", "IO");
			_Global_Version = new Global_Version(this.Connector, "", "Global_Version");
			_MAIN = new MAIN(this.Connector, "", "MAIN");
			_MAIN_TESTS = new MAIN_TESTS(this.Connector, "", "MAIN_TESTS");
		}

		public TcoPneumaticsTwinController(Vortex.Connector.ConnectorAdapter adapter, object[] parameters)
		{
			this.Connector = adapter.GetConnector(parameters);
			_IO = new IO(this.Connector, "", "IO");
			_Global_Version = new Global_Version(this.Connector, "", "Global_Version");
			_MAIN = new MAIN(this.Connector, "", "MAIN");
			_MAIN_TESTS = new MAIN_TESTS(this.Connector, "", "MAIN_TESTS");
		}

		public TcoPneumaticsTwinController(Vortex.Connector.ConnectorAdapter adapter)
		{
			this.Connector = adapter.GetConnector(adapter.Parameters);
			_IO = new IO(this.Connector, "", "IO");
			_Global_Version = new Global_Version(this.Connector, "", "Global_Version");
			_MAIN = new MAIN(this.Connector, "", "MAIN");
			_MAIN_TESTS = new MAIN_TESTS(this.Connector, "", "MAIN_TESTS");
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

	public partial interface ITcoPneumaticsTwinController
	{
		IIO IO
		{
			get;
		}

		IGlobal_Version Global_Version
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

		TcoPneumatics.PlainTcoPneumaticsTwinController CreatePlainerType();
	}

	public partial interface IShadowTcoPneumaticsTwinController
	{
		IShadowIO IO
		{
			get;
		}

		IShadowGlobal_Version Global_Version
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

		TcoPneumatics.PlainTcoPneumaticsTwinController CreatePlainerType();
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoPneumaticsTwinController : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		PlainIO _IO;
		public PlainIO IO
		{
			get
			{
				return _IO;
			}

			set
			{
				if (_IO != value)
				{
					_IO = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(IO)));
				}
			}
		}

		PlainGlobal_Version _Global_Version;
		public PlainGlobal_Version Global_Version
		{
			get
			{
				return _Global_Version;
			}

			set
			{
				if (_Global_Version != value)
				{
					_Global_Version = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Global_Version)));
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

		PlainMAIN_TESTS _MAIN_TESTS;
		internal PlainMAIN_TESTS MAIN_TESTS
		{
			get
			{
				return _MAIN_TESTS;
			}

			set
			{
				if (_MAIN_TESTS != value)
				{
					_MAIN_TESTS = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(MAIN_TESTS)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoPneumatics.TcoPneumaticsTwinController target)
		{
			IO.CopyPlainToCyclic(target.IO);
			Global_Version.CopyPlainToCyclic(target.Global_Version);
			MAIN.CopyPlainToCyclic(target.MAIN);
			MAIN_TESTS.CopyPlainToCyclic(target.MAIN_TESTS);
		}

		public void CopyPlainToCyclic(TcoPneumatics.ITcoPneumaticsTwinController target)
		{
			this.CopyPlainToCyclic((TcoPneumatics.TcoPneumaticsTwinController)target);
		}

		public void CopyPlainToShadow(TcoPneumatics.TcoPneumaticsTwinController target)
		{
			IO.CopyPlainToShadow(target.IO);
			Global_Version.CopyPlainToShadow(target.Global_Version);
			MAIN.CopyPlainToShadow(target.MAIN);
			MAIN_TESTS.CopyPlainToShadow(target.MAIN_TESTS);
		}

		public void CopyPlainToShadow(TcoPneumatics.IShadowTcoPneumaticsTwinController target)
		{
			this.CopyPlainToShadow((TcoPneumatics.TcoPneumaticsTwinController)target);
		}

		public void CopyCyclicToPlain(TcoPneumatics.TcoPneumaticsTwinController source)
		{
			IO.CopyCyclicToPlain(source.IO);
			Global_Version.CopyCyclicToPlain(source.Global_Version);
			MAIN.CopyCyclicToPlain(source.MAIN);
			MAIN_TESTS.CopyCyclicToPlain(source.MAIN_TESTS);
		}

		public void CopyCyclicToPlain(TcoPneumatics.ITcoPneumaticsTwinController source)
		{
			this.CopyCyclicToPlain((TcoPneumatics.TcoPneumaticsTwinController)source);
		}

		public void CopyShadowToPlain(TcoPneumatics.TcoPneumaticsTwinController source)
		{
			IO.CopyShadowToPlain(source.IO);
			Global_Version.CopyShadowToPlain(source.Global_Version);
			MAIN.CopyShadowToPlain(source.MAIN);
			MAIN_TESTS.CopyShadowToPlain(source.MAIN_TESTS);
		}

		public void CopyShadowToPlain(TcoPneumatics.IShadowTcoPneumaticsTwinController source)
		{
			this.CopyShadowToPlain((TcoPneumatics.TcoPneumaticsTwinController)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoPneumaticsTwinController()
		{
			_IO = new PlainIO();
			_Global_Version = new PlainGlobal_Version();
			_MAIN = new PlainMAIN();
			_MAIN_TESTS = new PlainMAIN_TESTS();
		}
	}
}