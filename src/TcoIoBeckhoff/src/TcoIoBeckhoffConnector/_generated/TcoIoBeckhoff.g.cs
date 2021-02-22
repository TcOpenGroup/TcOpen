using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;
using TcoIoBeckhoffConnector.Properties;

[assembly: Vortex.Connector.Attributes.AssemblyPlcCounterPart("{\r\n  \"Types\": [\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"AI_1_Module\",\r\n      \"Namespace\": \"TcoIoBeckhoff\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"AI_ChannelStatus\",\r\n      \"Namespace\": \"TcoIoBeckhoff\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"AI_Channel\",\r\n      \"Namespace\": \"TcoIoBeckhoff\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"InfoData\",\r\n      \"Namespace\": \"TcoIoBeckhoff\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"WcState\",\r\n      \"Namespace\": \"TcoIoBeckhoff\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"FuseChannelBasic\",\r\n      \"Namespace\": \"TcoIoBeckhoff\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"FuseChannelExtended\",\r\n      \"Namespace\": \"TcoIoBeckhoff\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"FuseModuleEL922x\",\r\n      \"Namespace\": \"TcoIoBeckhoff\",\r\n      \"TypeMetaInfo\": 4\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"EL922x_ChannelBasic\",\r\n      \"Namespace\": \"TcoIoBeckhoff\",\r\n      \"TypeMetaInfo\": 1\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"EL922x_ChannelExtended\",\r\n      \"Namespace\": \"TcoIoBeckhoff\",\r\n      \"TypeMetaInfo\": 1\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"OutputStruct\",\r\n      \"Namespace\": \"TcoIoBeckhoff\",\r\n      \"TypeMetaInfo\": 1\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"StatusStructBasic\",\r\n      \"Namespace\": \"TcoIoBeckhoff\",\r\n      \"TypeMetaInfo\": 1\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"StatusStructExtended\",\r\n      \"Namespace\": \"TcoIoBeckhoff\",\r\n      \"TypeMetaInfo\": 1\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"IO\",\r\n      \"Namespace\": \"TcoIoBeckhoff\",\r\n      \"TypeMetaInfo\": 0\r\n    },\r\n    {\r\n      \"TypeAttributes\": \"\\n{attribute addProperty Name \\\"\\\" }\",\r\n      \"TypeName\": \"Global_Version\",\r\n      \"Namespace\": \"TcoIoBeckhoff\",\r\n      \"TypeMetaInfo\": 0\r\n    }\r\n  ],\r\n  \"Name\": \"TcoIoBeckhoff\",\r\n  \"Namespace\": \"TcoIoBeckhoff\"\r\n}")]
#pragma warning disable SA1402, CS1591, CS0108, CS0067
namespace TcoIoBeckhoff
{
	public partial class TcoIoBeckhoffTwinController : Vortex.Connector.ITwinController, ITcoIoBeckhoffTwinController, IShadowTcoIoBeckhoffTwinController
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
				return TcoIoBeckhoffTwinController.Translator.Translate(_humanReadable).Interpolate(this);
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

		IIO ITcoIoBeckhoffTwinController.IO
		{
			get
			{
				return IO;
			}
		}

		IShadowIO IShadowTcoIoBeckhoffTwinController.IO
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

		IGlobal_Version ITcoIoBeckhoffTwinController.Global_Version
		{
			get
			{
				return Global_Version;
			}
		}

		IShadowGlobal_Version IShadowTcoIoBeckhoffTwinController.Global_Version
		{
			get
			{
				return Global_Version;
			}
		}

		public void LazyOnlineToShadow()
		{
			IO.LazyOnlineToShadow();
			Global_Version.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			IO.LazyShadowToOnline();
			Global_Version.LazyShadowToOnline();
		}

		public PlainTcoIoBeckhoffTwinController CreatePlainerType()
		{
			var cloned = new PlainTcoIoBeckhoffTwinController();
			cloned.IO = IO.CreatePlainerType();
			cloned.Global_Version = Global_Version.CreatePlainerType();
			return cloned;
		}

		protected PlainTcoIoBeckhoffTwinController CreatePlainerType(PlainTcoIoBeckhoffTwinController cloned)
		{
			cloned.IO = IO.CreatePlainerType();
			cloned.Global_Version = Global_Version.CreatePlainerType();
			return cloned;
		}

		public System.String AttributeName
		{
			get
			{
				return TcoIoBeckhoffTwinController.Translator.Translate(_AttributeName).Interpolate(this);
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

		public ITcoIoBeckhoffTwinController Online
		{
			get
			{
				return (ITcoIoBeckhoffTwinController)this;
			}
		}

		public IShadowTcoIoBeckhoffTwinController Shadow
		{
			get
			{
				return (IShadowTcoIoBeckhoffTwinController)this;
			}
		}

		public Vortex.Connector.IConnector Connector
		{
			get;
			set;
		}

		public TcoIoBeckhoffTwinController()
		{
			var adapter = new Vortex.Connector.ConnectorAdapter(typeof (DummyConnectorFactory));
			this.Connector = adapter.GetConnector(new object[]{});
			_IO = new IO(this.Connector, "", "IO");
			_Global_Version = new Global_Version(this.Connector, "", "Global_Version");
		}

		public TcoIoBeckhoffTwinController(Vortex.Connector.ConnectorAdapter adapter, object[] parameters)
		{
			this.Connector = adapter.GetConnector(parameters);
			_IO = new IO(this.Connector, "", "IO");
			_Global_Version = new Global_Version(this.Connector, "", "Global_Version");
		}

		public TcoIoBeckhoffTwinController(Vortex.Connector.ConnectorAdapter adapter)
		{
			this.Connector = adapter.GetConnector(adapter.Parameters);
			_IO = new IO(this.Connector, "", "IO");
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

	public partial interface ITcoIoBeckhoffTwinController
	{
		IIO IO
		{
			get;
		}

		IGlobal_Version Global_Version
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainTcoIoBeckhoffTwinController CreatePlainerType();
	}

	public partial interface IShadowTcoIoBeckhoffTwinController
	{
		IShadowIO IO
		{
			get;
		}

		IShadowGlobal_Version Global_Version
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainTcoIoBeckhoffTwinController CreatePlainerType();
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoIoBeckhoffTwinController : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
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

		public void CopyPlainToCyclic(TcoIoBeckhoff.TcoIoBeckhoffTwinController target)
		{
			IO.CopyPlainToCyclic(target.IO);
			Global_Version.CopyPlainToCyclic(target.Global_Version);
		}

		public void CopyPlainToCyclic(TcoIoBeckhoff.ITcoIoBeckhoffTwinController target)
		{
			this.CopyPlainToCyclic((TcoIoBeckhoff.TcoIoBeckhoffTwinController)target);
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.TcoIoBeckhoffTwinController target)
		{
			IO.CopyPlainToShadow(target.IO);
			Global_Version.CopyPlainToShadow(target.Global_Version);
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.IShadowTcoIoBeckhoffTwinController target)
		{
			this.CopyPlainToShadow((TcoIoBeckhoff.TcoIoBeckhoffTwinController)target);
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.TcoIoBeckhoffTwinController source)
		{
			IO.CopyCyclicToPlain(source.IO);
			Global_Version.CopyCyclicToPlain(source.Global_Version);
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.ITcoIoBeckhoffTwinController source)
		{
			this.CopyCyclicToPlain((TcoIoBeckhoff.TcoIoBeckhoffTwinController)source);
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.TcoIoBeckhoffTwinController source)
		{
			IO.CopyShadowToPlain(source.IO);
			Global_Version.CopyShadowToPlain(source.Global_Version);
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.IShadowTcoIoBeckhoffTwinController source)
		{
			this.CopyShadowToPlain((TcoIoBeckhoff.TcoIoBeckhoffTwinController)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoIoBeckhoffTwinController()
		{
			_IO = new PlainIO();
			_Global_Version = new PlainGlobal_Version();
		}
	}
}