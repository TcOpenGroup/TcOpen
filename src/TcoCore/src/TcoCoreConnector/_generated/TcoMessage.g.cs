using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
	
///		<summary>
///			Complete message details including time stamp, message text, message category, identity of the message provider, PLC cycle in which the message was posted.
///		</summary>				
///<seealso cref="PlcTcoMessage"/>
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoMessage", "TcoCore", TypeComplexityEnum.Complex)]
	public partial class TcoMessage : Vortex.Connector.IVortexObject, ITcoMessage, IShadowTcoMessage, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		Vortex.Connector.ValueTypes.OnlinerDateTime _TimeStamp;
		
///		<summary>
///			Time when the message was posted.
///		</summary>				

		[ReadOnly()]
		public Vortex.Connector.ValueTypes.OnlinerDateTime TimeStamp
		{
			get
			{
				return _TimeStamp;
			}
		}

		[ReadOnly()]
		Vortex.Connector.ValueTypes.Online.IOnlineDateTime ITcoMessage.TimeStamp
		{
			get
			{
				return TimeStamp;
			}
		}

		[ReadOnly()]
		Vortex.Connector.ValueTypes.Shadows.IShadowDateTime IShadowTcoMessage.TimeStamp
		{
			get
			{
				return TimeStamp;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerString _Text;
		
///		<summary>
///			Text of the message.
///		</summary>				

		[ReadOnly()]
		public Vortex.Connector.ValueTypes.OnlinerString Text
		{
			get
			{
				return _Text;
			}
		}

		[ReadOnly()]
		Vortex.Connector.ValueTypes.Online.IOnlineString ITcoMessage.Text
		{
			get
			{
				return Text;
			}
		}

		[ReadOnly()]
		Vortex.Connector.ValueTypes.Shadows.IShadowString IShadowTcoMessage.Text
		{
			get
			{
				return Text;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt _Identity;
		
///		<summary>
///			Identity of the TcoObject that posted this message. 
///		</summary>				

		[ReadOnly()]
		public Vortex.Connector.ValueTypes.OnlinerULInt Identity
		{
			get
			{
				return _Identity;
			}
		}

		[ReadOnly()]
		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoMessage.Identity
		{
			get
			{
				return Identity;
			}
		}

		[ReadOnly()]
		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoMessage.Identity
		{
			get
			{
				return Identity;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerInt _Category;
		
///		<summary>
///			Message category of this message. See <see cref="eMessageCategory"/> for detailed informations. 
///		</summary>				

		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eMessageCategory))]
		public Vortex.Connector.ValueTypes.OnlinerInt Category
		{
			get
			{
				return _Category;
			}
		}

		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eMessageCategory))]
		Vortex.Connector.ValueTypes.Online.IOnlineInt ITcoMessage.Category
		{
			get
			{
				return Category;
			}
		}

		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eMessageCategory))]
		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadowTcoMessage.Category
		{
			get
			{
				return Category;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt _Cycle;
		
///		<summary>
///			Number of the PLC cycle in which the message was posted.
///		</summary>				

		[ReadOnly()]
		public Vortex.Connector.ValueTypes.OnlinerULInt Cycle
		{
			get
			{
				return _Cycle;
			}
		}

		[ReadOnly()]
		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoMessage.Cycle
		{
			get
			{
				return Cycle;
			}
		}

		[ReadOnly()]
		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoMessage.Cycle
		{
			get
			{
				return Cycle;
			}
		}

		public void LazyOnlineToShadow()
		{
			TimeStamp.Shadow = TimeStamp.LastValue;
			Text.Shadow = Text.LastValue;
			Identity.Shadow = Identity.LastValue;
			Category.Shadow = Category.LastValue;
			Cycle.Shadow = Cycle.LastValue;
		}

		public void LazyShadowToOnline()
		{
			TimeStamp.Cyclic = TimeStamp.Shadow;
			Text.Cyclic = Text.Shadow;
			Identity.Cyclic = Identity.Shadow;
			Category.Cyclic = Category.Shadow;
			Cycle.Cyclic = Cycle.Shadow;
		}

		public PlainTcoMessage CreatePlainerType()
		{
			var cloned = new PlainTcoMessage();
			return cloned;
		}

		protected PlainTcoMessage CreatePlainerType(PlainTcoMessage cloned)
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

		public void FlushPlainToOnline(TcoCore.PlainTcoMessage source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCore.PlainTcoMessage source)
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

		public void FlushOnlineToPlain(TcoCore.PlainTcoMessage source)
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

		public TcoMessage(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			_TimeStamp = @Connector.Online.Adapter.CreateDATE_TIME(this, "", "TimeStamp");
			TimeStamp.MakeReadOnly();
			_Text = @Connector.Online.Adapter.CreateSTRING(this, "", "Text");
			Text.MakeReadOnly();
			_Identity = @Connector.Online.Adapter.CreateULINT(this, "", "Identity");
			Identity.MakeReadOnly();
			_Category = @Connector.Online.Adapter.CreateINT(this, "", "Category");
			_Cycle = @Connector.Online.Adapter.CreateULINT(this, "", "Cycle");
			Cycle.MakeReadOnly();
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public TcoMessage()
		{
			PexPreConstructorParameterless();
			_TimeStamp = Vortex.Connector.IConnectorFactory.CreateDATE_TIME();
			_Text = Vortex.Connector.IConnectorFactory.CreateSTRING();
			_Identity = Vortex.Connector.IConnectorFactory.CreateULINT();
			_Category = Vortex.Connector.IConnectorFactory.CreateINT();
			_Cycle = Vortex.Connector.IConnectorFactory.CreateULINT();
			AttributeName = "";
			PexConstructorParameterless();
		}

		
///		<summary>
///			Complete message details including time stamp, message text, message category, identity of the message provider, PLC cycle in which the message was posted.
///		</summary>				

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoMessage
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoMessage()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoMessage : Vortex.Connector.IVortexOnlineObject
	{
		[ReadOnly()]
		Vortex.Connector.ValueTypes.Online.IOnlineDateTime TimeStamp
		{
			get;
		}

		[ReadOnly()]
		Vortex.Connector.ValueTypes.Online.IOnlineString Text
		{
			get;
		}

		[ReadOnly()]
		Vortex.Connector.ValueTypes.Online.IOnlineULInt Identity
		{
			get;
		}

		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eMessageCategory))]
		Vortex.Connector.ValueTypes.Online.IOnlineInt Category
		{
			get;
		}

		[ReadOnly()]
		Vortex.Connector.ValueTypes.Online.IOnlineULInt Cycle
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCore.PlainTcoMessage CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCore.PlainTcoMessage source);
		void FlushOnlineToPlain(TcoCore.PlainTcoMessage source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoMessage : Vortex.Connector.IVortexShadowObject
	{
		[ReadOnly()]
		Vortex.Connector.ValueTypes.Shadows.IShadowDateTime TimeStamp
		{
			get;
		}

		[ReadOnly()]
		Vortex.Connector.ValueTypes.Shadows.IShadowString Text
		{
			get;
		}

		[ReadOnly()]
		Vortex.Connector.ValueTypes.Shadows.IShadowULInt Identity
		{
			get;
		}

		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eMessageCategory))]
		Vortex.Connector.ValueTypes.Shadows.IShadowInt Category
		{
			get;
		}

		[ReadOnly()]
		Vortex.Connector.ValueTypes.Shadows.IShadowULInt Cycle
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCore.PlainTcoMessage CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCore.PlainTcoMessage source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoMessage : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		System.DateTime _TimeStamp;
		[ReadOnly()]
		public System.DateTime TimeStamp
		{
			get
			{
				return _TimeStamp;
			}

			set
			{
				if (_TimeStamp != value)
				{
					_TimeStamp = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(TimeStamp)));
				}
			}
		}

		System.String _Text;
		[ReadOnly()]
		public System.String Text
		{
			get
			{
				return _Text;
			}

			set
			{
				if (_Text != value)
				{
					_Text = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Text)));
				}
			}
		}

		System.UInt64 _Identity;
		[ReadOnly()]
		public System.UInt64 Identity
		{
			get
			{
				return _Identity;
			}

			set
			{
				if (_Identity != value)
				{
					_Identity = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Identity)));
				}
			}
		}

		System.Int16 _Category;
		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eMessageCategory))]
		public System.Int16 Category
		{
			get
			{
				return _Category;
			}

			set
			{
				if (_Category != value)
				{
					_Category = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Category)));
				}
			}
		}

		System.UInt64 _Cycle;
		[ReadOnly()]
		public System.UInt64 Cycle
		{
			get
			{
				return _Cycle;
			}

			set
			{
				if (_Cycle != value)
				{
					_Cycle = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Cycle)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCore.TcoMessage target)
		{
			target.TimeStamp.Cyclic = TimeStamp;
			target.Text.Cyclic = Text;
			target.Identity.Cyclic = Identity;
			target.Category.Cyclic = Category;
			target.Cycle.Cyclic = Cycle;
		}

		public void CopyPlainToCyclic(TcoCore.ITcoMessage target)
		{
			this.CopyPlainToCyclic((TcoCore.TcoMessage)target);
		}

		public void CopyPlainToShadow(TcoCore.TcoMessage target)
		{
			target.TimeStamp.Shadow = TimeStamp;
			target.Text.Shadow = Text;
			target.Identity.Shadow = Identity;
			target.Category.Shadow = Category;
			target.Cycle.Shadow = Cycle;
		}

		public void CopyPlainToShadow(TcoCore.IShadowTcoMessage target)
		{
			this.CopyPlainToShadow((TcoCore.TcoMessage)target);
		}

		public void CopyCyclicToPlain(TcoCore.TcoMessage source)
		{
			TimeStamp = source.TimeStamp.LastValue;
			Text = source.Text.LastValue;
			Identity = source.Identity.LastValue;
			Category = source.Category.LastValue;
			Cycle = source.Cycle.LastValue;
		}

		public void CopyCyclicToPlain(TcoCore.ITcoMessage source)
		{
			this.CopyCyclicToPlain((TcoCore.TcoMessage)source);
		}

		public void CopyShadowToPlain(TcoCore.TcoMessage source)
		{
			TimeStamp = source.TimeStamp.Shadow;
			Text = source.Text.Shadow;
			Identity = source.Identity.Shadow;
			Category = source.Category.Shadow;
			Cycle = source.Cycle.Shadow;
		}

		public void CopyShadowToPlain(TcoCore.IShadowTcoMessage source)
		{
			this.CopyShadowToPlain((TcoCore.TcoMessage)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoMessage()
		{
		}
	}
}