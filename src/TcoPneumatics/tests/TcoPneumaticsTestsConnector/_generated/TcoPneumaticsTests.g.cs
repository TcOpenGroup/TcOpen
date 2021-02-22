using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;
using TcoPneumaticsTestsConnector.Properties;

[assembly: Vortex.Connector.Attributes.AssemblyPlcCounterPart("{\r\n  \"Types\": [\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"DefaultContext\",\r\n      \"Namespace\": \"TcoPneumaticsTests\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"IO\",\r\n      \"Namespace\": \"TcoPneumaticsTests\",\r\n      \"TypeMetaInfo\": 0\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"MAIN\",\r\n      \"Namespace\": \"TcoPneumaticsTests\",\r\n      \"TypeMetaInfo\": 3\r\n    }\r\n  ],\r\n  \"Name\": \"TcoPneumaticsTests\",\r\n  \"Namespace\": \"TcoPneumaticsTests\"\r\n}")]
#pragma warning disable SA1402, CS1591, CS0108, CS0067
namespace TcoPneumaticsTests
{
	public partial class TcoPneumaticsTestsTwinController : Vortex.Connector.ITwinController, ITcoPneumaticsTestsTwinController, IShadowTcoPneumaticsTestsTwinController
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
				return TcoPneumaticsTestsTwinController.Translator.Translate(_humanReadable).Interpolate(this);
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

		IIO ITcoPneumaticsTestsTwinController.IO
		{
			get
			{
				return IO;
			}
		}

		IShadowIO IShadowTcoPneumaticsTestsTwinController.IO
		{
			get
			{
				return IO;
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

		IMAIN ITcoPneumaticsTestsTwinController.MAIN
		{
			get
			{
				return MAIN;
			}
		}

		IShadowMAIN IShadowTcoPneumaticsTestsTwinController.MAIN
		{
			get
			{
				return MAIN;
			}
		}

		public void LazyOnlineToShadow()
		{
			IO.LazyOnlineToShadow();
			MAIN.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			IO.LazyShadowToOnline();
			MAIN.LazyShadowToOnline();
		}

		public PlainTcoPneumaticsTestsTwinController CreatePlainerType()
		{
			var cloned = new PlainTcoPneumaticsTestsTwinController();
			cloned.IO = IO.CreatePlainerType();
			cloned.MAIN = MAIN.CreatePlainerType();
			return cloned;
		}

		protected PlainTcoPneumaticsTestsTwinController CreatePlainerType(PlainTcoPneumaticsTestsTwinController cloned)
		{
			cloned.IO = IO.CreatePlainerType();
			cloned.MAIN = MAIN.CreatePlainerType();
			return cloned;
		}

		public System.String AttributeName
		{
			get
			{
				return TcoPneumaticsTestsTwinController.Translator.Translate(_AttributeName).Interpolate(this);
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

		public ITcoPneumaticsTestsTwinController Online
		{
			get
			{
				return (ITcoPneumaticsTestsTwinController)this;
			}
		}

		public IShadowTcoPneumaticsTestsTwinController Shadow
		{
			get
			{
				return (IShadowTcoPneumaticsTestsTwinController)this;
			}
		}

		public Vortex.Connector.IConnector Connector
		{
			get;
			set;
		}

		public TcoPneumaticsTestsTwinController()
		{
			var adapter = new Vortex.Connector.ConnectorAdapter(typeof (DummyConnectorFactory));
			this.Connector = adapter.GetConnector(new object[]{});
			_IO = new IO(this.Connector, "", "IO");
			_MAIN = new MAIN(this.Connector, "", "MAIN");
		}

		public TcoPneumaticsTestsTwinController(Vortex.Connector.ConnectorAdapter adapter, object[] parameters)
		{
			this.Connector = adapter.GetConnector(parameters);
			_IO = new IO(this.Connector, "", "IO");
			_MAIN = new MAIN(this.Connector, "", "MAIN");
		}

		public TcoPneumaticsTestsTwinController(Vortex.Connector.ConnectorAdapter adapter)
		{
			this.Connector = adapter.GetConnector(adapter.Parameters);
			_IO = new IO(this.Connector, "", "IO");
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

	public partial interface ITcoPneumaticsTestsTwinController
	{
		IIO IO
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

		TcoPneumaticsTests.PlainTcoPneumaticsTestsTwinController CreatePlainerType();
	}

	public partial interface IShadowTcoPneumaticsTestsTwinController
	{
		IShadowIO IO
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

		TcoPneumaticsTests.PlainTcoPneumaticsTestsTwinController CreatePlainerType();
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoPneumaticsTestsTwinController : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
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

		public void CopyPlainToCyclic(TcoPneumaticsTests.TcoPneumaticsTestsTwinController target)
		{
			IO.CopyPlainToCyclic(target.IO);
			MAIN.CopyPlainToCyclic(target.MAIN);
		}

		public void CopyPlainToCyclic(TcoPneumaticsTests.ITcoPneumaticsTestsTwinController target)
		{
			this.CopyPlainToCyclic((TcoPneumaticsTests.TcoPneumaticsTestsTwinController)target);
		}

		public void CopyPlainToShadow(TcoPneumaticsTests.TcoPneumaticsTestsTwinController target)
		{
			IO.CopyPlainToShadow(target.IO);
			MAIN.CopyPlainToShadow(target.MAIN);
		}

		public void CopyPlainToShadow(TcoPneumaticsTests.IShadowTcoPneumaticsTestsTwinController target)
		{
			this.CopyPlainToShadow((TcoPneumaticsTests.TcoPneumaticsTestsTwinController)target);
		}

		public void CopyCyclicToPlain(TcoPneumaticsTests.TcoPneumaticsTestsTwinController source)
		{
			IO.CopyCyclicToPlain(source.IO);
			MAIN.CopyCyclicToPlain(source.MAIN);
		}

		public void CopyCyclicToPlain(TcoPneumaticsTests.ITcoPneumaticsTestsTwinController source)
		{
			this.CopyCyclicToPlain((TcoPneumaticsTests.TcoPneumaticsTestsTwinController)source);
		}

		public void CopyShadowToPlain(TcoPneumaticsTests.TcoPneumaticsTestsTwinController source)
		{
			IO.CopyShadowToPlain(source.IO);
			MAIN.CopyShadowToPlain(source.MAIN);
		}

		public void CopyShadowToPlain(TcoPneumaticsTests.IShadowTcoPneumaticsTestsTwinController source)
		{
			this.CopyShadowToPlain((TcoPneumaticsTests.TcoPneumaticsTestsTwinController)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoPneumaticsTestsTwinController()
		{
			_IO = new PlainIO();
			_MAIN = new PlainMAIN();
		}
	}
}