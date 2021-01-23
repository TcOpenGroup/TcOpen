using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoIoBeckhoff
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "IO", "TcoIoBeckhoff", TypeComplexityEnum.Complex)]
	public partial class IO : Vortex.Connector.IVortexObject, IIO, IShadowIO, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		AI_1_Module __EL3001;
		public AI_1_Module _EL3001
		{
			get
			{
				return __EL3001;
			}
		}

		IAI_1_Module IIO._EL3001
		{
			get
			{
				return _EL3001;
			}
		}

		IShadowAI_1_Module IShadowIO._EL3001
		{
			get
			{
				return _EL3001;
			}
		}

		FuseChannelBasic __channel_1;
		public FuseChannelBasic _channel_1
		{
			get
			{
				return __channel_1;
			}
		}

		IFuseChannelBasic IIO._channel_1
		{
			get
			{
				return _channel_1;
			}
		}

		IShadowFuseChannelBasic IShadowIO._channel_1
		{
			get
			{
				return _channel_1;
			}
		}

		FuseChannelBasic __channel_2;
		public FuseChannelBasic _channel_2
		{
			get
			{
				return __channel_2;
			}
		}

		IFuseChannelBasic IIO._channel_2
		{
			get
			{
				return _channel_2;
			}
		}

		IShadowFuseChannelBasic IShadowIO._channel_2
		{
			get
			{
				return _channel_2;
			}
		}

		FuseModuleEL922x __EL922x;
		public FuseModuleEL922x _EL922x
		{
			get
			{
				return __EL922x;
			}
		}

		IFuseModuleEL922x IIO._EL922x
		{
			get
			{
				return _EL922x;
			}
		}

		IShadowFuseModuleEL922x IShadowIO._EL922x
		{
			get
			{
				return _EL922x;
			}
		}

		public void LazyOnlineToShadow()
		{
			_EL3001.LazyOnlineToShadow();
			_channel_1.LazyOnlineToShadow();
			_channel_2.LazyOnlineToShadow();
			_EL922x.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			_EL3001.LazyShadowToOnline();
			_channel_1.LazyShadowToOnline();
			_channel_2.LazyShadowToOnline();
			_EL922x.LazyShadowToOnline();
		}

		public PlainIO CreatePlainerType()
		{
			var cloned = new PlainIO();
			cloned._EL3001 = _EL3001.CreatePlainerType();
			cloned._channel_1 = _channel_1.CreatePlainerType();
			cloned._channel_2 = _channel_2.CreatePlainerType();
			cloned._EL922x = _EL922x.CreatePlainerType();
			return cloned;
		}

		protected PlainIO CreatePlainerType(PlainIO cloned)
		{
			cloned._EL3001 = _EL3001.CreatePlainerType();
			cloned._channel_1 = _channel_1.CreatePlainerType();
			cloned._channel_2 = _channel_2.CreatePlainerType();
			cloned._EL922x = _EL922x.CreatePlainerType();
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

		public void FlushPlainToOnline(TcoIoBeckhoff.PlainIO source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.PlainIO source)
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

		public void FlushOnlineToPlain(TcoIoBeckhoff.PlainIO source)
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

		public IO(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@ValueTags = new System.Collections.Generic.List<Vortex.Connector.IValueTag>();
			this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			__EL3001 = new AI_1_Module(this, "", "_EL3001");
			__channel_1 = new FuseChannelBasic(this, "", "_channel_1");
			__channel_2 = new FuseChannelBasic(this, "", "_channel_2");
			__EL922x = new FuseModuleEL922x(this, "", "_EL922x");
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
			parent.AddChild(this);
		}

		public IO()
		{
			PexPreConstructorParameterless();
			__EL3001 = new AI_1_Module();
			__channel_1 = new FuseChannelBasic();
			__channel_2 = new FuseChannelBasic();
			__EL922x = new FuseModuleEL922x();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcIO
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcIO()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IIO : Vortex.Connector.IVortexOnlineObject
	{
		IAI_1_Module _EL3001
		{
			get;
		}

		IFuseChannelBasic _channel_1
		{
			get;
		}

		IFuseChannelBasic _channel_2
		{
			get;
		}

		IFuseModuleEL922x _EL922x
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainIO CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoIoBeckhoff.PlainIO source);
		void FlushOnlineToPlain(TcoIoBeckhoff.PlainIO source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowIO : Vortex.Connector.IVortexShadowObject
	{
		IShadowAI_1_Module _EL3001
		{
			get;
		}

		IShadowFuseChannelBasic _channel_1
		{
			get;
		}

		IShadowFuseChannelBasic _channel_2
		{
			get;
		}

		IShadowFuseModuleEL922x _EL922x
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainIO CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoIoBeckhoff.PlainIO source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainIO : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		PlainAI_1_Module __EL3001;
		public PlainAI_1_Module _EL3001
		{
			get
			{
				return __EL3001;
			}

			set
			{
				if (__EL3001 != value)
				{
					__EL3001 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_EL3001)));
				}
			}
		}

		PlainFuseChannelBasic __channel_1;
		public PlainFuseChannelBasic _channel_1
		{
			get
			{
				return __channel_1;
			}

			set
			{
				if (__channel_1 != value)
				{
					__channel_1 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_channel_1)));
				}
			}
		}

		PlainFuseChannelBasic __channel_2;
		public PlainFuseChannelBasic _channel_2
		{
			get
			{
				return __channel_2;
			}

			set
			{
				if (__channel_2 != value)
				{
					__channel_2 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_channel_2)));
				}
			}
		}

		PlainFuseModuleEL922x __EL922x;
		public PlainFuseModuleEL922x _EL922x
		{
			get
			{
				return __EL922x;
			}

			set
			{
				if (__EL922x != value)
				{
					__EL922x = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_EL922x)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoIoBeckhoff.IO target)
		{
			_EL3001.CopyPlainToCyclic(target._EL3001);
			_channel_1.CopyPlainToCyclic(target._channel_1);
			_channel_2.CopyPlainToCyclic(target._channel_2);
			_EL922x.CopyPlainToCyclic(target._EL922x);
		}

		public void CopyPlainToCyclic(TcoIoBeckhoff.IIO target)
		{
			this.CopyPlainToCyclic((TcoIoBeckhoff.IO)target);
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.IO target)
		{
			_EL3001.CopyPlainToShadow(target._EL3001);
			_channel_1.CopyPlainToShadow(target._channel_1);
			_channel_2.CopyPlainToShadow(target._channel_2);
			_EL922x.CopyPlainToShadow(target._EL922x);
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.IShadowIO target)
		{
			this.CopyPlainToShadow((TcoIoBeckhoff.IO)target);
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.IO source)
		{
			_EL3001.CopyCyclicToPlain(source._EL3001);
			_channel_1.CopyCyclicToPlain(source._channel_1);
			_channel_2.CopyCyclicToPlain(source._channel_2);
			_EL922x.CopyCyclicToPlain(source._EL922x);
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.IIO source)
		{
			this.CopyCyclicToPlain((TcoIoBeckhoff.IO)source);
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.IO source)
		{
			_EL3001.CopyShadowToPlain(source._EL3001);
			_channel_1.CopyShadowToPlain(source._channel_1);
			_channel_2.CopyShadowToPlain(source._channel_2);
			_EL922x.CopyShadowToPlain(source._EL922x);
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.IShadowIO source)
		{
			this.CopyShadowToPlain((TcoIoBeckhoff.IO)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainIO()
		{
			__EL3001 = new PlainAI_1_Module();
			__channel_1 = new PlainFuseChannelBasic();
			__channel_2 = new PlainFuseChannelBasic();
			__EL922x = new PlainFuseModuleEL922x();
		}
	}
}