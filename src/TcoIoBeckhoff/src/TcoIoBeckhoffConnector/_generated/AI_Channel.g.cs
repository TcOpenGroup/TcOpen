using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoIoBeckhoff
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "AI_Channel", "TcoIoBeckhoff", TypeComplexityEnum.Complex)]
	public partial class AI_Channel : Vortex.Connector.IVortexObject, IAI_Channel, IShadowAI_Channel, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		Vortex.Connector.ValueTypes.OnlinerInt _Value;
		[IoLinkable("Inputs")]
		public Vortex.Connector.ValueTypes.OnlinerInt Value
		{
			get
			{
				return _Value;
			}
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineInt IAI_Channel.Value
		{
			get
			{
				return Value;
			}
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadowAI_Channel.Value
		{
			get
			{
				return Value;
			}
		}

		AI_ChannelStatus _Status;
		[IoLinkable("Inputs")]
		public AI_ChannelStatus Status
		{
			get
			{
				return _Status;
			}
		}

		[IoLinkable("Inputs")]
		IAI_ChannelStatus IAI_Channel.Status
		{
			get
			{
				return Status;
			}
		}

		[IoLinkable("Inputs")]
		IShadowAI_ChannelStatus IShadowAI_Channel.Status
		{
			get
			{
				return Status;
			}
		}

		public void LazyOnlineToShadow()
		{
			Value.Shadow = Value.LastValue;
			Status.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			Value.Cyclic = Value.Shadow;
			Status.LazyShadowToOnline();
		}

		public PlainAI_Channel CreatePlainerType()
		{
			var cloned = new PlainAI_Channel();
			cloned.Status = Status.CreatePlainerType();
			return cloned;
		}

		protected PlainAI_Channel CreatePlainerType(PlainAI_Channel cloned)
		{
			cloned.Status = Status.CreatePlainerType();
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

		public void FlushPlainToOnline(TcoIoBeckhoff.PlainAI_Channel source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.PlainAI_Channel source)
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

		public void FlushOnlineToPlain(TcoIoBeckhoff.PlainAI_Channel source)
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

		public AI_Channel(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@ValueTags = new System.Collections.Generic.List<Vortex.Connector.IValueTag>();
			this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			_Value = @Connector.Online.Adapter.CreateINT(this, "Raw value", "Value");
			Value.AttributeName = "Raw value";
			_Status = new AI_ChannelStatus(this, "", "Status");
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
			parent.AddChild(this);
		}

		public AI_Channel()
		{
			PexPreConstructorParameterless();
			_Value = Vortex.Connector.IConnectorFactory.CreateINT();
			Value.AttributeName = "Raw value";
			_Status = new AI_ChannelStatus();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcAI_Channel
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcAI_Channel()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IAI_Channel : Vortex.Connector.IVortexOnlineObject
	{
		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineInt Value
		{
			get;
		}

		[IoLinkable("Inputs")]
		IAI_ChannelStatus Status
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainAI_Channel CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoIoBeckhoff.PlainAI_Channel source);
		void FlushOnlineToPlain(TcoIoBeckhoff.PlainAI_Channel source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowAI_Channel : Vortex.Connector.IVortexShadowObject
	{
		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowInt Value
		{
			get;
		}

		[IoLinkable("Inputs")]
		IShadowAI_ChannelStatus Status
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainAI_Channel CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoIoBeckhoff.PlainAI_Channel source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainAI_Channel : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		System.Int16 _Value;
		[IoLinkable("Inputs")]
		public System.Int16 Value
		{
			get
			{
				return _Value;
			}

			set
			{
				if (_Value != value)
				{
					_Value = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Value)));
				}
			}
		}

		PlainAI_ChannelStatus _Status;
		[IoLinkable("Inputs")]
		public PlainAI_ChannelStatus Status
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

		public void CopyPlainToCyclic(TcoIoBeckhoff.AI_Channel target)
		{
			target.Value.Cyclic = Value;
			Status.CopyPlainToCyclic(target.Status);
		}

		public void CopyPlainToCyclic(TcoIoBeckhoff.IAI_Channel target)
		{
			this.CopyPlainToCyclic((TcoIoBeckhoff.AI_Channel)target);
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.AI_Channel target)
		{
			target.Value.Shadow = Value;
			Status.CopyPlainToShadow(target.Status);
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.IShadowAI_Channel target)
		{
			this.CopyPlainToShadow((TcoIoBeckhoff.AI_Channel)target);
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.AI_Channel source)
		{
			Value = source.Value.LastValue;
			Status.CopyCyclicToPlain(source.Status);
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.IAI_Channel source)
		{
			this.CopyCyclicToPlain((TcoIoBeckhoff.AI_Channel)source);
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.AI_Channel source)
		{
			Value = source.Value.Shadow;
			Status.CopyShadowToPlain(source.Status);
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.IShadowAI_Channel source)
		{
			this.CopyShadowToPlain((TcoIoBeckhoff.AI_Channel)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainAI_Channel()
		{
			_Status = new PlainAI_ChannelStatus();
		}
	}
}