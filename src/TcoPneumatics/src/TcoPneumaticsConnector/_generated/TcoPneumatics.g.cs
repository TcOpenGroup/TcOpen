using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;
using TcoPneumaticsConnector.Properties;

[assembly: Vortex.Connector.Attributes.AssemblyPlcCounterPart("{\r\n  \"Types\": [\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"fbCylinder\",\r\n      \"Namespace\": \"TcoPneumatics\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"Global_Version\",\r\n      \"Namespace\": \"TcoPneumatics\",\r\n      \"TypeMetaInfo\": 0\r\n    }\r\n  ],\r\n  \"Name\": \"TcoPneumatics\",\r\n  \"Namespace\": \"TcoPneumatics\"\r\n}")]
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

		public void LazyOnlineToShadow()
		{
			Global_Version.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			Global_Version.LazyShadowToOnline();
		}

		public PlainTcoPneumaticsTwinController CreatePlainerType()
		{
			var cloned = new PlainTcoPneumaticsTwinController();
			cloned.Global_Version = Global_Version.CreatePlainerType();
			return cloned;
		}

		protected PlainTcoPneumaticsTwinController CreatePlainerType(PlainTcoPneumaticsTwinController cloned)
		{
			cloned.Global_Version = Global_Version.CreatePlainerType();
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
			_Global_Version = new Global_Version(this.Connector, "", "Global_Version");
		}

		public TcoPneumaticsTwinController(Vortex.Connector.ConnectorAdapter adapter, object[] parameters)
		{
			this.Connector = adapter.GetConnector(parameters);
			_Global_Version = new Global_Version(this.Connector, "", "Global_Version");
		}

		public TcoPneumaticsTwinController(Vortex.Connector.ConnectorAdapter adapter)
		{
			this.Connector = adapter.GetConnector(adapter.Parameters);
			_Global_Version = new Global_Version(this.Connector, "", "Global_Version");
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
		IGlobal_Version Global_Version
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
		IShadowGlobal_Version Global_Version
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

		public void CopyPlainToCyclic(TcoPneumatics.TcoPneumaticsTwinController target)
		{
			Global_Version.CopyPlainToCyclic(target.Global_Version);
		}

		public void CopyPlainToCyclic(TcoPneumatics.ITcoPneumaticsTwinController target)
		{
			this.CopyPlainToCyclic((TcoPneumatics.TcoPneumaticsTwinController)target);
		}

		public void CopyPlainToShadow(TcoPneumatics.TcoPneumaticsTwinController target)
		{
			Global_Version.CopyPlainToShadow(target.Global_Version);
		}

		public void CopyPlainToShadow(TcoPneumatics.IShadowTcoPneumaticsTwinController target)
		{
			this.CopyPlainToShadow((TcoPneumatics.TcoPneumaticsTwinController)target);
		}

		public void CopyCyclicToPlain(TcoPneumatics.TcoPneumaticsTwinController source)
		{
			Global_Version.CopyCyclicToPlain(source.Global_Version);
		}

		public void CopyCyclicToPlain(TcoPneumatics.ITcoPneumaticsTwinController source)
		{
			this.CopyCyclicToPlain((TcoPneumatics.TcoPneumaticsTwinController)source);
		}

		public void CopyShadowToPlain(TcoPneumatics.TcoPneumaticsTwinController source)
		{
			Global_Version.CopyShadowToPlain(source.Global_Version);
		}

		public void CopyShadowToPlain(TcoPneumatics.IShadowTcoPneumaticsTwinController source)
		{
			this.CopyShadowToPlain((TcoPneumatics.TcoPneumaticsTwinController)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoPneumaticsTwinController()
		{
			_Global_Version = new PlainGlobal_Version();
		}
	}
}