using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;
using PlcAppExamplesConnector.Properties;

[assembly: Vortex.Connector.Attributes.AssemblyPlcCounterPart("{\r\n  \"Types\": [\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"MAIN\",\r\n      \"Namespace\": \"PlcAppExamples\",\r\n      \"TypeMetaInfo\": 3\r\n    }\r\n  ],\r\n  \"Name\": \"PlcAppExamples\",\r\n  \"Namespace\": \"PlcAppExamples\"\r\n}")]
#pragma warning disable SA1402, CS1591, CS0108, CS0067
namespace PlcAppExamples
{
	public partial class PlcAppExamplesTwinController : Vortex.Connector.ITwinController, IPlcAppExamplesTwinController, IShadowPlcAppExamplesTwinController
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
				return PlcAppExamplesTwinController.Translator.Translate(_humanReadable).Interpolate(this);
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

		IMAIN IPlcAppExamplesTwinController.MAIN
		{
			get
			{
				return MAIN;
			}
		}

		IShadowMAIN IShadowPlcAppExamplesTwinController.MAIN
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

		public PlainPlcAppExamplesTwinController CreatePlainerType()
		{
			var cloned = new PlainPlcAppExamplesTwinController();
			cloned.MAIN = MAIN.CreatePlainerType();
			return cloned;
		}

		protected PlainPlcAppExamplesTwinController CreatePlainerType(PlainPlcAppExamplesTwinController cloned)
		{
			cloned.MAIN = MAIN.CreatePlainerType();
			return cloned;
		}

		public System.String AttributeName
		{
			get
			{
				return PlcAppExamplesTwinController.Translator.Translate(_AttributeName).Interpolate(this);
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

		public IPlcAppExamplesTwinController Online
		{
			get
			{
				return (IPlcAppExamplesTwinController)this;
			}
		}

		public IShadowPlcAppExamplesTwinController Shadow
		{
			get
			{
				return (IShadowPlcAppExamplesTwinController)this;
			}
		}

		public Vortex.Connector.IConnector Connector
		{
			get;
			set;
		}

		public PlcAppExamplesTwinController()
		{
			var adapter = new Vortex.Connector.ConnectorAdapter(typeof (DummyConnectorFactory));
			this.Connector = adapter.GetConnector(new object[]{});
			_MAIN = new MAIN(this.Connector, "", "MAIN");
		}

		public PlcAppExamplesTwinController(Vortex.Connector.ConnectorAdapter adapter, object[] parameters)
		{
			this.Connector = adapter.GetConnector(parameters);
			_MAIN = new MAIN(this.Connector, "", "MAIN");
		}

		public PlcAppExamplesTwinController(Vortex.Connector.ConnectorAdapter adapter)
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

	public partial interface IPlcAppExamplesTwinController
	{
		IMAIN MAIN
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		PlcAppExamples.PlainPlcAppExamplesTwinController CreatePlainerType();
	}

	public partial interface IShadowPlcAppExamplesTwinController
	{
		IShadowMAIN MAIN
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		PlcAppExamples.PlainPlcAppExamplesTwinController CreatePlainerType();
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainPlcAppExamplesTwinController : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
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

		public void CopyPlainToCyclic(PlcAppExamples.PlcAppExamplesTwinController target)
		{
			MAIN.CopyPlainToCyclic(target.MAIN);
		}

		public void CopyPlainToCyclic(PlcAppExamples.IPlcAppExamplesTwinController target)
		{
			this.CopyPlainToCyclic((PlcAppExamples.PlcAppExamplesTwinController)target);
		}

		public void CopyPlainToShadow(PlcAppExamples.PlcAppExamplesTwinController target)
		{
			MAIN.CopyPlainToShadow(target.MAIN);
		}

		public void CopyPlainToShadow(PlcAppExamples.IShadowPlcAppExamplesTwinController target)
		{
			this.CopyPlainToShadow((PlcAppExamples.PlcAppExamplesTwinController)target);
		}

		public void CopyCyclicToPlain(PlcAppExamples.PlcAppExamplesTwinController source)
		{
			MAIN.CopyCyclicToPlain(source.MAIN);
		}

		public void CopyCyclicToPlain(PlcAppExamples.IPlcAppExamplesTwinController source)
		{
			this.CopyCyclicToPlain((PlcAppExamples.PlcAppExamplesTwinController)source);
		}

		public void CopyShadowToPlain(PlcAppExamples.PlcAppExamplesTwinController source)
		{
			MAIN.CopyShadowToPlain(source.MAIN);
		}

		public void CopyShadowToPlain(PlcAppExamples.IShadowPlcAppExamplesTwinController source)
		{
			this.CopyShadowToPlain((PlcAppExamples.PlcAppExamplesTwinController)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainPlcAppExamplesTwinController()
		{
			_MAIN = new PlainMAIN();
		}
	}
}