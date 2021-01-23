using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoIoBeckhoff
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "EL922x_ChannelBasic", "TcoIoBeckhoff", TypeComplexityEnum.Complex)]
	public partial class EL922x_ChannelBasic : Vortex.Connector.IVortexObject, IEL922x_ChannelBasic, IShadowEL922x_ChannelBasic, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		StatusStructBasic _Status;
		[IoLinkable("Inputs")]
		public StatusStructBasic Status
		{
			get
			{
				return _Status;
			}
		}

		[IoLinkable("Inputs")]
		IStatusStructBasic IEL922x_ChannelBasic.Status
		{
			get
			{
				return Status;
			}
		}

		[IoLinkable("Inputs")]
		IShadowStatusStructBasic IShadowEL922x_ChannelBasic.Status
		{
			get
			{
				return Status;
			}
		}

		OutputStruct _Control;
		[IoLinkable("")]
		public OutputStruct Control
		{
			get
			{
				return _Control;
			}
		}

		[IoLinkable("")]
		IOutputStruct IEL922x_ChannelBasic.Control
		{
			get
			{
				return Control;
			}
		}

		[IoLinkable("")]
		IShadowOutputStruct IShadowEL922x_ChannelBasic.Control
		{
			get
			{
				return Control;
			}
		}

		public void LazyOnlineToShadow()
		{
			Status.LazyOnlineToShadow();
			Control.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			Status.LazyShadowToOnline();
			Control.LazyShadowToOnline();
		}

		public PlainEL922x_ChannelBasic CreatePlainerType()
		{
			var cloned = new PlainEL922x_ChannelBasic();
			cloned.Status = Status.CreatePlainerType();
			cloned.Control = Control.CreatePlainerType();
			return cloned;
		}

		protected PlainEL922x_ChannelBasic CreatePlainerType(PlainEL922x_ChannelBasic cloned)
		{
			cloned.Status = Status.CreatePlainerType();
			cloned.Control = Control.CreatePlainerType();
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

		public void FlushPlainToOnline(TcoIoBeckhoff.PlainEL922x_ChannelBasic source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.PlainEL922x_ChannelBasic source)
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

		public void FlushOnlineToPlain(TcoIoBeckhoff.PlainEL922x_ChannelBasic source)
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

		public EL922x_ChannelBasic(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@ValueTags = new System.Collections.Generic.List<Vortex.Connector.IValueTag>();
			this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			_Status = new StatusStructBasic(this, "", "Status");
			_Control = new OutputStruct(this, "", "Control");
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
			parent.AddChild(this);
		}

		public EL922x_ChannelBasic()
		{
			PexPreConstructorParameterless();
			_Status = new StatusStructBasic();
			_Control = new OutputStruct();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcEL922x_ChannelBasic
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcEL922x_ChannelBasic()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IEL922x_ChannelBasic : Vortex.Connector.IVortexOnlineObject
	{
		[IoLinkable("Inputs")]
		IStatusStructBasic Status
		{
			get;
		}

		[IoLinkable("")]
		IOutputStruct Control
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainEL922x_ChannelBasic CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoIoBeckhoff.PlainEL922x_ChannelBasic source);
		void FlushOnlineToPlain(TcoIoBeckhoff.PlainEL922x_ChannelBasic source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowEL922x_ChannelBasic : Vortex.Connector.IVortexShadowObject
	{
		[IoLinkable("Inputs")]
		IShadowStatusStructBasic Status
		{
			get;
		}

		[IoLinkable("")]
		IShadowOutputStruct Control
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainEL922x_ChannelBasic CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoIoBeckhoff.PlainEL922x_ChannelBasic source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainEL922x_ChannelBasic : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		PlainStatusStructBasic _Status;
		[IoLinkable("Inputs")]
		public PlainStatusStructBasic Status
		{
			get
			{
				return _Status;
			}

			set
			{
				if (_Status != value)
				{
					_Status = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Status)));
				}
			}
		}

		PlainOutputStruct _Control;
		[IoLinkable("")]
		public PlainOutputStruct Control
		{
			get
			{
				return _Control;
			}

			set
			{
				if (_Control != value)
				{
					_Control = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Control)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoIoBeckhoff.EL922x_ChannelBasic target)
		{
			Status.CopyPlainToCyclic(target.Status);
			Control.CopyPlainToCyclic(target.Control);
		}

		public void CopyPlainToCyclic(TcoIoBeckhoff.IEL922x_ChannelBasic target)
		{
			this.CopyPlainToCyclic((TcoIoBeckhoff.EL922x_ChannelBasic)target);
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.EL922x_ChannelBasic target)
		{
			Status.CopyPlainToShadow(target.Status);
			Control.CopyPlainToShadow(target.Control);
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.IShadowEL922x_ChannelBasic target)
		{
			this.CopyPlainToShadow((TcoIoBeckhoff.EL922x_ChannelBasic)target);
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.EL922x_ChannelBasic source)
		{
			Status.CopyCyclicToPlain(source.Status);
			Control.CopyCyclicToPlain(source.Control);
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.IEL922x_ChannelBasic source)
		{
			this.CopyCyclicToPlain((TcoIoBeckhoff.EL922x_ChannelBasic)source);
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.EL922x_ChannelBasic source)
		{
			Status.CopyShadowToPlain(source.Status);
			Control.CopyShadowToPlain(source.Control);
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.IShadowEL922x_ChannelBasic source)
		{
			this.CopyShadowToPlain((TcoIoBeckhoff.EL922x_ChannelBasic)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainEL922x_ChannelBasic()
		{
			_Status = new PlainStatusStructBasic();
			_Control = new PlainOutputStruct();
		}
	}
}