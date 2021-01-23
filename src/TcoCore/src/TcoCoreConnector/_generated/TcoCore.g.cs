using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;
using TcoCoreConnector.Properties;

[assembly: Vortex.Connector.Attributes.AssemblyPlcCounterPart("{\r\n  \"Types\": [\r\n    {\r\n      \"TypeAttributes\": \"\",\r\n      \"TypeName\": \"eMessageCategory\",\r\n      \"Namespace\": \"TcoCore\",\r\n      \"TypeMetaInfo\": 5\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\",\r\n      \"TypeName\": \"eTaskState\",\r\n      \"Namespace\": \"TcoCore\",\r\n      \"TypeMetaInfo\": 5\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"TcoComponent\",\r\n      \"Namespace\": \"TcoCore\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"TcoMessenger\",\r\n      \"Namespace\": \"TcoCore\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"TcoContext\",\r\n      \"Namespace\": \"TcoCore\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"TcoObject\",\r\n      \"Namespace\": \"TcoCore\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"TcoState\",\r\n      \"Namespace\": \"TcoCore\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"TcoTask\",\r\n      \"Namespace\": \"TcoCore\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"TcoMessage\",\r\n      \"Namespace\": \"TcoCore\",\r\n      \"TypeMetaInfo\": 1\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"MAIN\",\r\n      \"Namespace\": \"TcoCore\",\r\n      \"TypeMetaInfo\": 3\r\n    }\r\n  ],\r\n  \"Name\": \"TcoCore\",\r\n  \"Namespace\": \"TcoCore\"\r\n}")]
namespace TcoCore
{
	public partial class TcoCoreTwinController : Vortex.Connector.ITwinController, ITcoCoreTwinController, IShadowTcoCoreTwinController
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
				return TcoCoreTwinController.Translator.Translate(_humanReadable).Interpolate(this);
			}

			protected set
			{
				_humanReadable = value;
			}
		}

		protected string _humanReadable;
		MAIN _MAIN;
		public MAIN MAIN
		{
			get
			{
				return _MAIN;
			}
		}

		IMAIN ITcoCoreTwinController.MAIN
		{
			get
			{
				return MAIN;
			}
		}

		IShadowMAIN IShadowTcoCoreTwinController.MAIN
		{
			get
			{
				return MAIN;
			}
		}

		public void LazyOnlineToShadow()
		{
			MAIN.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			MAIN.LazyShadowToOnline();
		}

		public PlainTcoCoreTwinController CreatePlainerType()
		{
			var cloned = new PlainTcoCoreTwinController();
			cloned.MAIN = MAIN.CreatePlainerType();
			return cloned;
		}

		protected PlainTcoCoreTwinController CreatePlainerType(PlainTcoCoreTwinController cloned)
		{
			cloned.MAIN = MAIN.CreatePlainerType();
			return cloned;
		}

		public System.String AttributeName
		{
			get
			{
				return TcoCoreTwinController.Translator.Translate(_AttributeName).Interpolate(this);
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

		public ITcoCoreTwinController Online
		{
			get
			{
				return (ITcoCoreTwinController)this;
			}
		}

		public IShadowTcoCoreTwinController Shadow
		{
			get
			{
				return (IShadowTcoCoreTwinController)this;
			}
		}

		public Vortex.Connector.IConnector Connector
		{
			get;
			set;
		}

		public TcoCoreTwinController()
		{
			var adapter = new Vortex.Connector.ConnectorAdapter(typeof (DummyConnectorFactory));
			this.Connector = adapter.GetConnector(new object[]{});
			_MAIN = new MAIN(this.Connector, "", "MAIN");
		}

		public TcoCoreTwinController(Vortex.Connector.ConnectorAdapter adapter, object[] parameters)
		{
			this.Connector = adapter.GetConnector(parameters);
			_MAIN = new MAIN(this.Connector, "", "MAIN");
		}

		public TcoCoreTwinController(Vortex.Connector.ConnectorAdapter adapter)
		{
			this.Connector = adapter.GetConnector(adapter.Parameters);
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

	public partial interface ITcoCoreTwinController
	{
		IMAIN MAIN
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCore.PlainTcoCoreTwinController CreatePlainerType();
	}

	public partial interface IShadowTcoCoreTwinController
	{
		IShadowMAIN MAIN
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCore.PlainTcoCoreTwinController CreatePlainerType();
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoCoreTwinController : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
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

		public void CopyPlainToCyclic(TcoCore.TcoCoreTwinController target)
		{
			MAIN.CopyPlainToCyclic(target.MAIN);
		}

		public void CopyPlainToCyclic(TcoCore.ITcoCoreTwinController target)
		{
			this.CopyPlainToCyclic((TcoCore.TcoCoreTwinController)target);
		}

		public void CopyPlainToShadow(TcoCore.TcoCoreTwinController target)
		{
			MAIN.CopyPlainToShadow(target.MAIN);
		}

		public void CopyPlainToShadow(TcoCore.IShadowTcoCoreTwinController target)
		{
			this.CopyPlainToShadow((TcoCore.TcoCoreTwinController)target);
		}

		public void CopyCyclicToPlain(TcoCore.TcoCoreTwinController source)
		{
			MAIN.CopyCyclicToPlain(source.MAIN);
		}

		public void CopyCyclicToPlain(TcoCore.ITcoCoreTwinController source)
		{
			this.CopyCyclicToPlain((TcoCore.TcoCoreTwinController)source);
		}

		public void CopyShadowToPlain(TcoCore.TcoCoreTwinController source)
		{
			MAIN.CopyShadowToPlain(source.MAIN);
		}

		public void CopyShadowToPlain(TcoCore.IShadowTcoCoreTwinController source)
		{
			this.CopyShadowToPlain((TcoCore.TcoCoreTwinController)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoCoreTwinController()
		{
			_MAIN = new PlainMAIN();
		}
	}
}