using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoPneumatics
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "fbComponent", "TcoPneumatics", TypeComplexityEnum.Complex)]
	public partial class fbComponent : Vortex.Connector.IVortexObject, IfbComponent, IShadowfbComponent, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		Vortex.Connector.ValueTypes.OnlinerString __message;
		[ReadOnly()]
		public Vortex.Connector.ValueTypes.OnlinerString _message
		{
			get
			{
				return __message;
			}
		}

		[ReadOnly()]
		Vortex.Connector.ValueTypes.Online.IOnlineString IfbComponent._message
		{
			get
			{
				return _message;
			}
		}

		[ReadOnly()]
		Vortex.Connector.ValueTypes.Shadows.IShadowString IShadowfbComponent._message
		{
			get
			{
				return _message;
			}
		}

		public void LazyOnlineToShadow()
		{
			_message.Shadow = _message.LastValue;
		}

		public void LazyShadowToOnline()
		{
			_message.Cyclic = _message.Shadow;
		}

		public PlainfbComponent CreatePlainerType()
		{
			var cloned = new PlainfbComponent();
			return cloned;
		}

		protected PlainfbComponent CreatePlainerType(PlainfbComponent cloned)
		{
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

		public void FlushPlainToOnline(TcoPneumatics.PlainfbComponent source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoPneumatics.PlainfbComponent source)
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

		public void FlushOnlineToPlain(TcoPneumatics.PlainfbComponent source)
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

		public fbComponent(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@ValueTags = new System.Collections.Generic.List<Vortex.Connector.IValueTag>();
			this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			__message = @Connector.Online.Adapter.CreateSTRING(this, "Components' message", "_message");
			_message.MakeReadOnly();
			_message.AttributeName = "Components' message";
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
			parent.AddChild(this);
		}

		public fbComponent()
		{
			PexPreConstructorParameterless();
			__message = Vortex.Connector.IConnectorFactory.CreateSTRING();
			_message.AttributeName = "Components' message";
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcfbComponent
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcfbComponent()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IfbComponent : Vortex.Connector.IVortexOnlineObject
	{
		[ReadOnly()]
		Vortex.Connector.ValueTypes.Online.IOnlineString _message
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoPneumatics.PlainfbComponent CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoPneumatics.PlainfbComponent source);
		void FlushOnlineToPlain(TcoPneumatics.PlainfbComponent source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowfbComponent : Vortex.Connector.IVortexShadowObject
	{
		[ReadOnly()]
		Vortex.Connector.ValueTypes.Shadows.IShadowString _message
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoPneumatics.PlainfbComponent CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoPneumatics.PlainfbComponent source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainfbComponent : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		System.String __message;
		[ReadOnly()]
		public System.String _message
		{
			get
			{
				return __message;
			}

			set
			{
				if (__message != value)
				{
					__message = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_message)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoPneumatics.fbComponent target)
		{
			target._message.Cyclic = _message;
		}

		public void CopyPlainToCyclic(TcoPneumatics.IfbComponent target)
		{
			this.CopyPlainToCyclic((TcoPneumatics.fbComponent)target);
		}

		public void CopyPlainToShadow(TcoPneumatics.fbComponent target)
		{
			target._message.Shadow = _message;
		}

		public void CopyPlainToShadow(TcoPneumatics.IShadowfbComponent target)
		{
			this.CopyPlainToShadow((TcoPneumatics.fbComponent)target);
		}

		public void CopyCyclicToPlain(TcoPneumatics.fbComponent source)
		{
			_message = source._message.LastValue;
		}

		public void CopyCyclicToPlain(TcoPneumatics.IfbComponent source)
		{
			this.CopyCyclicToPlain((TcoPneumatics.fbComponent)source);
		}

		public void CopyShadowToPlain(TcoPneumatics.fbComponent source)
		{
			_message = source._message.Shadow;
		}

		public void CopyShadowToPlain(TcoPneumatics.IShadowfbComponent source)
		{
			this.CopyShadowToPlain((TcoPneumatics.fbComponent)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainfbComponent()
		{
		}
	}
}