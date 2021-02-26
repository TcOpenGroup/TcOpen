using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoIoBeckhoff
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "AI_ChannelStatus", "TcoIoBeckhoff", TypeComplexityEnum.Complex)]
	public partial class AI_ChannelStatus : Vortex.Connector.IVortexObject, IAI_ChannelStatus, IShadowAI_ChannelStatus, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		public void LazyOnlineToShadow()
		{
		}

		public void LazyShadowToOnline()
		{
		}

		public PlainAI_ChannelStatus CreatePlainerType()
		{
			var cloned = new PlainAI_ChannelStatus();
			return cloned;
		}

		protected PlainAI_ChannelStatus CreatePlainerType(PlainAI_ChannelStatus cloned)
		{
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
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

		private System.Collections.Generic.List<Vortex.Connector.IVortexElement> Kids
		{
			get;
			set;
		}

		public System.Collections.Generic.IEnumerable<Vortex.Connector.IVortexElement> GetKids()
		{
			return this.Kids;
		}

		public void AddKid(Vortex.Connector.IVortexElement vortexElement)
		{
			this.Kids.Add(vortexElement);
		}

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

		public void FlushPlainToOnline(TcoIoBeckhoff.PlainAI_ChannelStatus source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.PlainAI_ChannelStatus source)
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

		public void FlushOnlineToPlain(TcoIoBeckhoff.PlainAI_ChannelStatus source)
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

		public AI_ChannelStatus(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@ValueTags = new System.Collections.Generic.List<Vortex.Connector.IValueTag>();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			this.Kids = new System.Collections.Generic.List<Vortex.Connector.IVortexElement>();
			this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public AI_ChannelStatus()
		{
			PexPreConstructorParameterless();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcAI_ChannelStatus
		{
			public object UnderRange;
			public object OverRange;
			public object Limit_1_Greater;
			public object Limit_1_Smaller;
			public object Limit_2_Greater;
			public object Limit_2_Smaller;
			public object Error;
			public object Reserved_1_7;
			public object Reserved_2_0;
			public object Reserved_2_1;
			public object Reserved_2_2;
			public object Reserved_2_3;
			public object Reserved_2_4;
			public object Reserved_2_5;
			public object TxPdoState;
			public object TxPdoToggle;
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcAI_ChannelStatus()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IAI_ChannelStatus : Vortex.Connector.IVortexOnlineObject
	{
		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainAI_ChannelStatus CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoIoBeckhoff.PlainAI_ChannelStatus source);
		void FlushOnlineToPlain(TcoIoBeckhoff.PlainAI_ChannelStatus source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowAI_ChannelStatus : Vortex.Connector.IVortexShadowObject
	{
		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainAI_ChannelStatus CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoIoBeckhoff.PlainAI_ChannelStatus source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainAI_ChannelStatus : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		public void CopyPlainToCyclic(TcoIoBeckhoff.AI_ChannelStatus target)
		{
		}

		public void CopyPlainToCyclic(TcoIoBeckhoff.IAI_ChannelStatus target)
		{
			this.CopyPlainToCyclic((TcoIoBeckhoff.AI_ChannelStatus)target);
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.AI_ChannelStatus target)
		{
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.IShadowAI_ChannelStatus target)
		{
			this.CopyPlainToShadow((TcoIoBeckhoff.AI_ChannelStatus)target);
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.AI_ChannelStatus source)
		{
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.IAI_ChannelStatus source)
		{
			this.CopyCyclicToPlain((TcoIoBeckhoff.AI_ChannelStatus)source);
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.AI_ChannelStatus source)
		{
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.IShadowAI_ChannelStatus source)
		{
			this.CopyShadowToPlain((TcoIoBeckhoff.AI_ChannelStatus)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainAI_ChannelStatus()
		{
		}
	}
}