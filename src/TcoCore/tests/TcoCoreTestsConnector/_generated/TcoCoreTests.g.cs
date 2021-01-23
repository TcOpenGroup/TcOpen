using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;
using TcoCoreTestsConnector.Properties;

[assembly: Vortex.Connector.Attributes.AssemblyPlcCounterPart("{\r\n  \"Types\": [\r\n    {\r\n      \"TypeAttributes\": \"\",\r\n      \"TypeName\": \"eTransitionType\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 5\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"fbContext\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"TcoContext_App_1\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"TcoObject_Counter\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"TcoContext_App_2\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"TcoTask_DownCounter\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"fbPiston\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"fbPistonMoveTask\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"fbManipulatorAutomat\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"fbManipulatorContext\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"TcoContext_Waveform\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"TcoObject_Waveform\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"TcoState_WaveformSequence\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"TcoTask_Transition\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"TcoContextTest\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"TcoObjectTest\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"TcoStateTest\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"TcoStateAutoRestoreTest\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"TcoTaskTest\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"WaveformPoint\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 1\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"gMANIPULATOR_IO\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 0\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"EXAMPLES_PRG\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 3\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"MANIPULATOR\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 3\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"MAIN\",\r\n      \"Namespace\": \"TcoCoreTests\",\r\n      \"TypeMetaInfo\": 3\r\n    }\r\n  ],\r\n  \"Name\": \"TcoCoreTests\",\r\n  \"Namespace\": \"TcoCoreTests\"\r\n}")]
namespace TcoCoreTests
{
	public partial class TcoCoreTestsTwinController : Vortex.Connector.ITwinController, ITcoCoreTestsTwinController, IShadowTcoCoreTestsTwinController
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
		gMANIPULATOR_IO _gMANIPULATOR_IO;
		public gMANIPULATOR_IO gMANIPULATOR_IO
		{
			get
			{
				return _gMANIPULATOR_IO;
			}
		}

		IgMANIPULATOR_IO ITcoCoreTestsTwinController.gMANIPULATOR_IO
		{
			get
			{
				return gMANIPULATOR_IO;
			}
		}

		IShadowgMANIPULATOR_IO IShadowTcoCoreTestsTwinController.gMANIPULATOR_IO
		{
			get
			{
				return gMANIPULATOR_IO;
			}
		}

		EXAMPLES_PRG _EXAMPLES_PRG;
		public EXAMPLES_PRG EXAMPLES_PRG
		{
			get
			{
				return _EXAMPLES_PRG;
			}
		}

		IEXAMPLES_PRG ITcoCoreTestsTwinController.EXAMPLES_PRG
		{
			get
			{
				return EXAMPLES_PRG;
			}
		}

		IShadowEXAMPLES_PRG IShadowTcoCoreTestsTwinController.EXAMPLES_PRG
		{
			get
			{
				return EXAMPLES_PRG;
			}
		}

		MANIPULATOR _MANIPULATOR;
		public MANIPULATOR MANIPULATOR
		{
			get
			{
				return _MANIPULATOR;
			}
		}

		IMANIPULATOR ITcoCoreTestsTwinController.MANIPULATOR
		{
			get
			{
				return MANIPULATOR;
			}
		}

		IShadowMANIPULATOR IShadowTcoCoreTestsTwinController.MANIPULATOR
		{
			get
			{
				return MANIPULATOR;
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

		IMAIN ITcoCoreTestsTwinController.MAIN
		{
			get
			{
				return MAIN;
			}
		}

		IShadowMAIN IShadowTcoCoreTestsTwinController.MAIN
		{
			get
			{
				return MAIN;
			}
		}

		public void LazyOnlineToShadow()
		{
			gMANIPULATOR_IO.LazyOnlineToShadow();
			EXAMPLES_PRG.LazyOnlineToShadow();
			MANIPULATOR.LazyOnlineToShadow();
			MAIN.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			gMANIPULATOR_IO.LazyShadowToOnline();
			EXAMPLES_PRG.LazyShadowToOnline();
			MANIPULATOR.LazyShadowToOnline();
			MAIN.LazyShadowToOnline();
		}

		public PlainTcoCoreTestsTwinController CreatePlainerType()
		{
			var cloned = new PlainTcoCoreTestsTwinController();
			cloned.gMANIPULATOR_IO = gMANIPULATOR_IO.CreatePlainerType();
			cloned.EXAMPLES_PRG = EXAMPLES_PRG.CreatePlainerType();
			cloned.MANIPULATOR = MANIPULATOR.CreatePlainerType();
			cloned.MAIN = MAIN.CreatePlainerType();
			return cloned;
		}

		protected PlainTcoCoreTestsTwinController CreatePlainerType(PlainTcoCoreTestsTwinController cloned)
		{
			cloned.gMANIPULATOR_IO = gMANIPULATOR_IO.CreatePlainerType();
			cloned.EXAMPLES_PRG = EXAMPLES_PRG.CreatePlainerType();
			cloned.MANIPULATOR = MANIPULATOR.CreatePlainerType();
			cloned.MAIN = MAIN.CreatePlainerType();
			return cloned;
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

		public ITcoCoreTestsTwinController Online
		{
			get
			{
				return (ITcoCoreTestsTwinController)this;
			}
		}

		public IShadowTcoCoreTestsTwinController Shadow
		{
			get
			{
				return (IShadowTcoCoreTestsTwinController)this;
			}
		}

		public Vortex.Connector.IConnector Connector
		{
			get;
			set;
		}

		public TcoCoreTestsTwinController()
		{
			var adapter = new Vortex.Connector.ConnectorAdapter(typeof (DummyConnectorFactory));
			this.Connector = adapter.GetConnector(new object[]{});
			_gMANIPULATOR_IO = new gMANIPULATOR_IO(this.Connector, "", "gMANIPULATOR_IO");
			_EXAMPLES_PRG = new EXAMPLES_PRG(this.Connector, "", "EXAMPLES_PRG");
			_MANIPULATOR = new MANIPULATOR(this.Connector, "", "MANIPULATOR");
			_MAIN = new MAIN(this.Connector, "", "MAIN");
		}

		public TcoCoreTestsTwinController(Vortex.Connector.ConnectorAdapter adapter, object[] parameters)
		{
			this.Connector = adapter.GetConnector(parameters);
			_gMANIPULATOR_IO = new gMANIPULATOR_IO(this.Connector, "", "gMANIPULATOR_IO");
			_EXAMPLES_PRG = new EXAMPLES_PRG(this.Connector, "", "EXAMPLES_PRG");
			_MANIPULATOR = new MANIPULATOR(this.Connector, "", "MANIPULATOR");
			_MAIN = new MAIN(this.Connector, "", "MAIN");
		}

		public TcoCoreTestsTwinController(Vortex.Connector.ConnectorAdapter adapter)
		{
			this.Connector = adapter.GetConnector(adapter.Parameters);
			_gMANIPULATOR_IO = new gMANIPULATOR_IO(this.Connector, "", "gMANIPULATOR_IO");
			_EXAMPLES_PRG = new EXAMPLES_PRG(this.Connector, "", "EXAMPLES_PRG");
			_MANIPULATOR = new MANIPULATOR(this.Connector, "", "MANIPULATOR");
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

	public partial interface ITcoCoreTestsTwinController
	{
		IgMANIPULATOR_IO gMANIPULATOR_IO
		{
			get;
		}

		IEXAMPLES_PRG EXAMPLES_PRG
		{
			get;
		}

		IMANIPULATOR MANIPULATOR
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

		TcoCoreTests.PlainTcoCoreTestsTwinController CreatePlainerType();
	}

	public partial interface IShadowTcoCoreTestsTwinController
	{
		IShadowgMANIPULATOR_IO gMANIPULATOR_IO
		{
			get;
		}

		IShadowEXAMPLES_PRG EXAMPLES_PRG
		{
			get;
		}

		IShadowMANIPULATOR MANIPULATOR
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

		TcoCoreTests.PlainTcoCoreTestsTwinController CreatePlainerType();
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoCoreTestsTwinController : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		PlaingMANIPULATOR_IO _gMANIPULATOR_IO;
		public PlaingMANIPULATOR_IO gMANIPULATOR_IO
		{
			get
			{
				return _gMANIPULATOR_IO;
			}

			set
			{
				if (_gMANIPULATOR_IO != value)
				{
					_gMANIPULATOR_IO = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(gMANIPULATOR_IO)));
				}
			}
		}

		PlainEXAMPLES_PRG _EXAMPLES_PRG;
		public PlainEXAMPLES_PRG EXAMPLES_PRG
		{
			get
			{
				return _EXAMPLES_PRG;
			}

			set
			{
				if (_EXAMPLES_PRG != value)
				{
					_EXAMPLES_PRG = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(EXAMPLES_PRG)));
				}
			}
		}

		PlainMANIPULATOR _MANIPULATOR;
		public PlainMANIPULATOR MANIPULATOR
		{
			get
			{
				return _MANIPULATOR;
			}

			set
			{
				if (_MANIPULATOR != value)
				{
					_MANIPULATOR = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(MANIPULATOR)));
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

		public void CopyPlainToCyclic(TcoCoreTests.TcoCoreTestsTwinController target)
		{
			gMANIPULATOR_IO.CopyPlainToCyclic(target.gMANIPULATOR_IO);
			EXAMPLES_PRG.CopyPlainToCyclic(target.EXAMPLES_PRG);
			MANIPULATOR.CopyPlainToCyclic(target.MANIPULATOR);
			MAIN.CopyPlainToCyclic(target.MAIN);
		}

		public void CopyPlainToCyclic(TcoCoreTests.ITcoCoreTestsTwinController target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.TcoCoreTestsTwinController)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.TcoCoreTestsTwinController target)
		{
			gMANIPULATOR_IO.CopyPlainToShadow(target.gMANIPULATOR_IO);
			EXAMPLES_PRG.CopyPlainToShadow(target.EXAMPLES_PRG);
			MANIPULATOR.CopyPlainToShadow(target.MANIPULATOR);
			MAIN.CopyPlainToShadow(target.MAIN);
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowTcoCoreTestsTwinController target)
		{
			this.CopyPlainToShadow((TcoCoreTests.TcoCoreTestsTwinController)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.TcoCoreTestsTwinController source)
		{
			gMANIPULATOR_IO.CopyCyclicToPlain(source.gMANIPULATOR_IO);
			EXAMPLES_PRG.CopyCyclicToPlain(source.EXAMPLES_PRG);
			MANIPULATOR.CopyCyclicToPlain(source.MANIPULATOR);
			MAIN.CopyCyclicToPlain(source.MAIN);
		}

		public void CopyCyclicToPlain(TcoCoreTests.ITcoCoreTestsTwinController source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.TcoCoreTestsTwinController)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.TcoCoreTestsTwinController source)
		{
			gMANIPULATOR_IO.CopyShadowToPlain(source.gMANIPULATOR_IO);
			EXAMPLES_PRG.CopyShadowToPlain(source.EXAMPLES_PRG);
			MANIPULATOR.CopyShadowToPlain(source.MANIPULATOR);
			MAIN.CopyShadowToPlain(source.MAIN);
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowTcoCoreTestsTwinController source)
		{
			this.CopyShadowToPlain((TcoCoreTests.TcoCoreTestsTwinController)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoCoreTestsTwinController()
		{
			_gMANIPULATOR_IO = new PlaingMANIPULATOR_IO();
			_EXAMPLES_PRG = new PlainEXAMPLES_PRG();
			_MANIPULATOR = new PlainMANIPULATOR();
			_MAIN = new PlainMAIN();
		}
	}
}