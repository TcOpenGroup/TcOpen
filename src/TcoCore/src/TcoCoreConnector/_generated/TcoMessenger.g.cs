using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
	
///			<summary>
///				Provides mechanism for delivering messages to the HMI.				 
///			</summary>			
///<seealso cref="PlcTcoMessenger"/>
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoMessenger", "TcoCore", TypeComplexityEnum.Complex)]
	public partial class TcoMessenger : Vortex.Connector.IVortexObject, ITcoMessenger, IShadowTcoMessenger, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		TcoMessage __mime;
		
///			<summary>
///				Most important message of this instance of messenger.			 
///			</summary>			

		public TcoMessage _mime
		{
			get
			{
				return __mime;
			}
		}

		ITcoMessage ITcoMessenger._mime
		{
			get
			{
				return _mime;
			}
		}

		IShadowTcoMessage IShadowTcoMessenger._mime
		{
			get
			{
				return _mime;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __contextIdentity;
		public Vortex.Connector.ValueTypes.OnlinerULInt _contextIdentity
		{
			get
			{
				return __contextIdentity;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoMessenger._contextIdentity
		{
			get
			{
				return _contextIdentity;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoMessenger._contextIdentity
		{
			get
			{
				return _contextIdentity;
			}
		}

		public void LazyOnlineToShadow()
		{
			_mime.LazyOnlineToShadow();
			_contextIdentity.Shadow = _contextIdentity.LastValue;
		}

		public void LazyShadowToOnline()
		{
			_mime.LazyShadowToOnline();
			_contextIdentity.Cyclic = _contextIdentity.Shadow;
		}

		public PlainTcoMessenger CreatePlainerType()
		{
			var cloned = new PlainTcoMessenger();
			cloned._mime = _mime.CreatePlainerType();
			return cloned;
		}

		protected PlainTcoMessenger CreatePlainerType(PlainTcoMessenger cloned)
		{
			cloned._mime = _mime.CreatePlainerType();
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

		public void FlushPlainToOnline(TcoCore.PlainTcoMessenger source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCore.PlainTcoMessenger source)
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

		public void FlushOnlineToPlain(TcoCore.PlainTcoMessenger source)
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

		public TcoMessenger(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@ValueTags = new System.Collections.Generic.List<Vortex.Connector.IValueTag>();
			this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			__mime = new TcoMessage(this, "", "_mime");
			__contextIdentity = @Connector.Online.Adapter.CreateULINT(this, "", "_contextIdentity");
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
			parent.AddChild(this);
		}

		public TcoMessenger()
		{
			PexPreConstructorParameterless();
			__mime = new TcoMessage();
			__contextIdentity = Vortex.Connector.IConnectorFactory.CreateULINT();
			AttributeName = "";
			PexConstructorParameterless();
		}

		
///			<summary>
///				Provides mechanism for delivering messages to the HMI.				 
///			</summary>			

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoMessenger
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoMessenger()
			{
			}

			
///			<summary>
///				Adds message of 'debug' category to the message queue.				 
///			</summary>		
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<param name="Message">
///<para>Plc type : STRING [VAR_INPUT]; Twin type : <see cref="Vortex.Connector.ValueTypes.OnlinerString"/></para>
///<para>
///			<summary>
///				Arbitrary message string.			 
///			</summary>			
///		</para>
///</param>

///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public void Debug(dynamic Message)
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///			<summary>
///				Adds message of 'error' category to the message queue.				 
///			</summary>						
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<param name="Message">
///<para>Plc type : STRING [VAR_INPUT]; Twin type : <see cref="Vortex.Connector.ValueTypes.OnlinerString"/></para>
///<para>
///			<summary>
///				Arbitrary message string.			 
///			</summary>			
///		</para>
///</param>

///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public void Error(dynamic Message)
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///			<summary>
///				Adds message of 'information' category to the message queue.				 
///			</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<param name="Message">
///<para>Plc type : STRING [VAR_INPUT]; Twin type : <see cref="Vortex.Connector.ValueTypes.OnlinerString"/></para>
///<para>
///			<summary>
///				Arbitrary message string.			 
///			</summary>			
///		</para>
///</param>

///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public void Info(dynamic Message)
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///			<summary>
///				Adds message of 'notification' category to the message queue.				 
///			</summary>				
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<param name="Message">
///<para>Plc type : STRING [VAR_INPUT]; Twin type : <see cref="Vortex.Connector.ValueTypes.OnlinerString"/></para>
///<para>
///			<summary>
///				Arbitrary message string.			 
///			</summary>			
///		</para>
///</param>

///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public void Notify(dynamic Message)
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///			<summary>
///				Adds message of given category to the message queue.				 
///			</summary>	
///			<returns>Message index.</returns>		
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<param name="Message">
///<para>Plc type : STRING [VAR_INPUT]; Twin type : <see cref="Vortex.Connector.ValueTypes.OnlinerString"/></para>
///<para>
///			<summary>
///				Arbitrary message string.			 
///			</summary>			
///		</para>
///</param>

///<param name="Category">
///<para>Plc type : eMessageCategory [VAR_INPUT]; Twin type : <see cref="eMessageCategory"/></para>
///<para>
///			<summary>
///				Message category.			 
///			</summary>			
///		</para>
///</param>

///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public void Post(dynamic Message, dynamic Category)
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///			<summary>
///				Adds message of 'programming error' category to the message queue.				 
///			</summary>				
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<param name="Message">
///<para>Plc type : STRING [VAR_INPUT]; Twin type : <see cref="Vortex.Connector.ValueTypes.OnlinerString"/></para>
///<para>
///			<summary>
///				Arbitrary message string.			 
///			</summary>			
///		</para>
///</param>

///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public void Programming(dynamic Message)
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///			<summary>
///				Adds message of 'trace' category to the message queue.				 
///			</summary>					
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<param name="Message">
///<para>Plc type : STRING [VAR_INPUT]; Twin type : <see cref="Vortex.Connector.ValueTypes.OnlinerString"/></para>
///<para>
///			<summary>
///				Arbitrary message string.			 
///			</summary>			
///		</para>
///</param>

///<returns>Plc type INT; Twin type: <see cref="Vortex.Connector.ValueTypes.OnlinerInt"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public dynamic Trace(dynamic Message)
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///			<summary>
///				Adds message of 'warning' category to the message queue.				 
///			</summary>					
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<param name="Message">
///<para>Plc type : STRING [VAR_INPUT]; Twin type : <see cref="Vortex.Connector.ValueTypes.OnlinerString"/></para>
///<para>
///			<summary>
///				Arbitrary message string.			 
///			</summary>			
///		</para>
///</param>

///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public void Warning(dynamic Message)
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoMessenger : Vortex.Connector.IVortexOnlineObject
	{
		ITcoMessage _mime
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineULInt _contextIdentity
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCore.PlainTcoMessenger CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCore.PlainTcoMessenger source);
		void FlushOnlineToPlain(TcoCore.PlainTcoMessenger source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoMessenger : Vortex.Connector.IVortexShadowObject
	{
		IShadowTcoMessage _mime
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _contextIdentity
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCore.PlainTcoMessenger CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCore.PlainTcoMessenger source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoMessenger : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		PlainTcoMessage __mime;
		public PlainTcoMessage _mime
		{
			get
			{
				return __mime;
			}

			set
			{
				if (__mime != value)
				{
					__mime = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_mime)));
				}
			}
		}

		System.UInt64 __contextIdentity;
		public System.UInt64 _contextIdentity
		{
			get
			{
				return __contextIdentity;
			}

			set
			{
				if (__contextIdentity != value)
				{
					__contextIdentity = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_contextIdentity)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCore.TcoMessenger target)
		{
			_mime.CopyPlainToCyclic(target._mime);
			target._contextIdentity.Cyclic = _contextIdentity;
		}

		public void CopyPlainToCyclic(TcoCore.ITcoMessenger target)
		{
			this.CopyPlainToCyclic((TcoCore.TcoMessenger)target);
		}

		public void CopyPlainToShadow(TcoCore.TcoMessenger target)
		{
			_mime.CopyPlainToShadow(target._mime);
			target._contextIdentity.Shadow = _contextIdentity;
		}

		public void CopyPlainToShadow(TcoCore.IShadowTcoMessenger target)
		{
			this.CopyPlainToShadow((TcoCore.TcoMessenger)target);
		}

		public void CopyCyclicToPlain(TcoCore.TcoMessenger source)
		{
			_mime.CopyCyclicToPlain(source._mime);
			_contextIdentity = source._contextIdentity.LastValue;
		}

		public void CopyCyclicToPlain(TcoCore.ITcoMessenger source)
		{
			this.CopyCyclicToPlain((TcoCore.TcoMessenger)source);
		}

		public void CopyShadowToPlain(TcoCore.TcoMessenger source)
		{
			_mime.CopyShadowToPlain(source._mime);
			_contextIdentity = source._contextIdentity.Shadow;
		}

		public void CopyShadowToPlain(TcoCore.IShadowTcoMessenger source)
		{
			this.CopyShadowToPlain((TcoCore.TcoMessenger)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoMessenger()
		{
			__mime = new PlainTcoMessage();
		}
	}
}