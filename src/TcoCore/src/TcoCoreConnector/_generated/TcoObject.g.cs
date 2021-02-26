using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
	
///		<summary>
///			Basic construction block from which all the others block except TcoContext are derived. This function block is abstract so it cannot be instantiated and must be extended.
///			It must be assigned to some TcoContext. Each TcoObject can have only one context. This context is assigned in the declaration part using implicit method <c>FB_init()</c>.
///			The TcoObject provides this context to all its members, so all objects in the TcoContext has the same context.
///			<para>
///				<example>
///				<note type="Example">
///				<para>
///					All the variables _myContext, _ContextTcoObjectA, _ContextTcoObjectB, _ContextChildTcoObject should have all the same value.
///				</para>
///				<para>
///					All the variables _myIdentity, _TcoObjectAidentity, _TcoObjectBidentity, _ChildTcoObjectidentity should have uniques values.
///				</para>
///					<code>
///	//Declaration of the myTcoObject 
///	FUNCTION_BLOCK myTcoObject EXTENDS TcoObject
///	VAR
///		  _myChildTcoObject	:	myChildTcoObject(THIS^);
///	END_VAR
///	//Declaration of the myTcoContext 
///	FUNCTION_BLOCK myTcoContext EXTENDS TcoContext
///	VAR
///		  _myTcoObjectA		:	myTcoObject(THIS^);					
///		  _myTcoObjectB		:	myTcoObject(_myTcoObjectA.Context); 
///		  _myContext		:	ITcoContext;
///		  _ContextTcoObjectA	:	ITcoContext;
///		  _ContextTcoObjectB	:	ITcoContext;
///		  _ContextChildTcoObject	:	ITcoContext;
///		  _myIdentity		:	ULINT;
///		  _TcoObjectAidentity	: 	ULINT;
///		  _TcoObjectBidentity	: 	ULINT;
///		  _ChildTcoObjectidentity	: 	ULINT;							
///	END_VAR
///	//Plc code of the myTcoContext 
///	_myContext		:= 		THIS^.Context;
///	_ContextTcoObjectA	:=		_myTcoObjectA.Context;
///	_ContextTcoObjectB	:=		_myTcoObjectB.Context;
///	_ContextChildTcoObject	:=		_myTcoObjectB._myChildTcoObject.Context;
///	_myIdentity		:=		THIS^.Identity;
///	_TcoObjectAidentity	:=		_myTcoObjectA.Identity;
///	_TcoObjectBidentity	:=		_myTcoObjectB.Identity;
///	_ChildTcoObjectidentity	:=		_myTcoObjectB._myChildTcoObject.Identity;
///					</code>
///					 <note type="Explanation">
///						<para>
///							The context of the myTcoContext instance is assigned to itself. As the _myTcoObjectA context is assigned to this instance of the myTcoContext, it is assigned to the same context,
///							and the value of the _ContextTcoObjectA will be the same as the value of the _myContext.
///						</para>					
///						<para>
///							The context of the _myTcoObjectB instance is assigned to the _myTcoObjectA, that has already assigned context to this instance of the myTcoContext.
///							So the value of the _ContextTcoObjectB will be the same as the values of the _ContextTcoObjectA and _myContext.
///						</para>					
///						<para>
///							The context of the _myTcoObjectB._myChildTcoObject instance is assigned to the _myTcoObjectB, that has already assigned context to _myTcoObjectA, 
///							that has already assigned its context to this instance of the myTcoContext.
///							So the value of the _ContextChildTcoObject wile be the same as the values _ContextTcoObjectB,_ContextTcoObjectA and _myContext.
///						</para>
///						<para>
///							As the Identities of all objects points to the themselves, all identities will have different values, as all objects are unique.
///						</para>					
///					 </note>
///				 </note>
///				</example>
///			</para>
/// 		</summary>			
///<seealso cref="PlcTcoObject"/>
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoObject", "TcoCore", TypeComplexityEnum.Complex)]
	public partial class TcoObject : Vortex.Connector.IVortexObject, ITcoObject, IShadowTcoObject, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		Vortex.Connector.ValueTypes.OnlinerULInt __Identity;
		[RenderIgnore(), ReadOnly()]
		public Vortex.Connector.ValueTypes.OnlinerULInt _Identity
		{
			get
			{
				return __Identity;
			}
		}

		[RenderIgnore(), ReadOnly()]
		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoObject._Identity
		{
			get
			{
				return _Identity;
			}
		}

		[RenderIgnore(), ReadOnly()]
		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoObject._Identity
		{
			get
			{
				return _Identity;
			}
		}

		public void LazyOnlineToShadow()
		{
			_Identity.Shadow = _Identity.LastValue;
		}

		public void LazyShadowToOnline()
		{
			_Identity.Cyclic = _Identity.Shadow;
		}

		public PlainTcoObject CreatePlainerType()
		{
			var cloned = new PlainTcoObject();
			return cloned;
		}

		protected PlainTcoObject CreatePlainerType(PlainTcoObject cloned)
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

		public void FlushPlainToOnline(TcoCore.PlainTcoObject source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCore.PlainTcoObject source)
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

		public void FlushOnlineToPlain(TcoCore.PlainTcoObject source)
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

		public TcoObject(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			__Identity = @Connector.Online.Adapter.CreateULINT(this, "", "_Identity");
			_Identity.MakeReadOnly();
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public TcoObject()
		{
			PexPreConstructorParameterless();
			__Identity = Vortex.Connector.IConnectorFactory.CreateULINT();
			AttributeName = "";
			PexConstructorParameterless();
		}

		
///		<summary>
///			Basic construction block from which all the others block except TcoContext are derived. This function block is abstract so it cannot be instantiated and must be extended.
///			It must be assigned to some TcoContext. Each TcoObject can have only one context. This context is assigned in the declaration part using implicit method <c>FB_init()</c>.
///			The TcoObject provides this context to all its members, so all objects in the TcoContext has the same context.
///			<para>
///				<example>
///				<note type="Example">
///				<para>
///					All the variables _myContext, _ContextTcoObjectA, _ContextTcoObjectB, _ContextChildTcoObject should have all the same value.
///				</para>
///				<para>
///					All the variables _myIdentity, _TcoObjectAidentity, _TcoObjectBidentity, _ChildTcoObjectidentity should have uniques values.
///				</para>
///					<code>
///	//Declaration of the myTcoObject 
///	FUNCTION_BLOCK myTcoObject EXTENDS TcoObject
///	VAR
///		  _myChildTcoObject	:	myChildTcoObject(THIS^);
///	END_VAR
///	//Declaration of the myTcoContext 
///	FUNCTION_BLOCK myTcoContext EXTENDS TcoContext
///	VAR
///		  _myTcoObjectA		:	myTcoObject(THIS^);					
///		  _myTcoObjectB		:	myTcoObject(_myTcoObjectA.Context); 
///		  _myContext		:	ITcoContext;
///		  _ContextTcoObjectA	:	ITcoContext;
///		  _ContextTcoObjectB	:	ITcoContext;
///		  _ContextChildTcoObject	:	ITcoContext;
///		  _myIdentity		:	ULINT;
///		  _TcoObjectAidentity	: 	ULINT;
///		  _TcoObjectBidentity	: 	ULINT;
///		  _ChildTcoObjectidentity	: 	ULINT;							
///	END_VAR
///	//Plc code of the myTcoContext 
///	_myContext		:= 		THIS^.Context;
///	_ContextTcoObjectA	:=		_myTcoObjectA.Context;
///	_ContextTcoObjectB	:=		_myTcoObjectB.Context;
///	_ContextChildTcoObject	:=		_myTcoObjectB._myChildTcoObject.Context;
///	_myIdentity		:=		THIS^.Identity;
///	_TcoObjectAidentity	:=		_myTcoObjectA.Identity;
///	_TcoObjectBidentity	:=		_myTcoObjectB.Identity;
///	_ChildTcoObjectidentity	:=		_myTcoObjectB._myChildTcoObject.Identity;
///					</code>
///					 <note type="Explanation">
///						<para>
///							The context of the myTcoContext instance is assigned to itself. As the _myTcoObjectA context is assigned to this instance of the myTcoContext, it is assigned to the same context,
///							and the value of the _ContextTcoObjectA will be the same as the value of the _myContext.
///						</para>					
///						<para>
///							The context of the _myTcoObjectB instance is assigned to the _myTcoObjectA, that has already assigned context to this instance of the myTcoContext.
///							So the value of the _ContextTcoObjectB will be the same as the values of the _ContextTcoObjectA and _myContext.
///						</para>					
///						<para>
///							The context of the _myTcoObjectB._myChildTcoObject instance is assigned to the _myTcoObjectB, that has already assigned context to _myTcoObjectA, 
///							that has already assigned its context to this instance of the myTcoContext.
///							So the value of the _ContextChildTcoObject wile be the same as the values _ContextTcoObjectB,_ContextTcoObjectA and _myContext.
///						</para>
///						<para>
///							As the Identities of all objects points to the themselves, all identities will have different values, as all objects are unique.
///						</para>					
///					 </note>
///				 </note>
///				</example>
///			</para>
/// 		</summary>			

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoObject
		{
			
///		<summary>
///			Returns the context of the parent object, that this object is assigned to.
///			This context is given by declaration, its value is assigned after download by calling the implicit method <c>FB_init()</c> and cannot be changed during runtime.
///		</summary>			
///<summary><note type="note">This is PLC property. This method is accessible only from the PLC code.</note></summary>
///<returns>Plc type ITcoContext; Twin type: <see cref="ITcoContext"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute()]
			public dynamic Context
			{
				get
				{
					throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
				}
			}

			
///		<summary>
///			Returns the own identity of the <see cref ="TcoObject.PlcTcoObject()"/>. This value is assigned after download by calling the implicit method <c>FB_init()</c> and cannot be changed during runtime.
///			This variable is used in the higher level packages.  
///		</summary>			
///<summary><note type="note">This is PLC property. This method is accessible only from the PLC code.</note></summary>
///<returns>Plc type ULINT; Twin type: <see cref="Vortex.Connector.ValueTypes.OnlinerULInt"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute()]
			public dynamic Identity
			{
				get
				{
					throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
				}
			}

			
///			<summary>
///				Returns current messenger that is part of this object.
///				See <see cref="TcoMessenger.PlcTcoMessenger()"/> for more details.
///			</summary>			
///<summary><note type="note">This is PLC property. This method is accessible only from the PLC code.</note></summary>
///<returns>Plc type ITcoMessenger; Twin type: <see cref="ITcoMessenger"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute()]
			public dynamic Messenger
			{
				get
				{
					throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
				}
			}

			public object _Identity;
			public object _Parent;
			public object _messenger;
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoObject()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoObject : Vortex.Connector.IVortexOnlineObject
	{
		[RenderIgnore(), ReadOnly()]
		Vortex.Connector.ValueTypes.Online.IOnlineULInt _Identity
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCore.PlainTcoObject CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCore.PlainTcoObject source);
		void FlushOnlineToPlain(TcoCore.PlainTcoObject source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoObject : Vortex.Connector.IVortexShadowObject
	{
		[RenderIgnore(), ReadOnly()]
		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _Identity
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCore.PlainTcoObject CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCore.PlainTcoObject source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoObject : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		System.UInt64 __Identity;
		[RenderIgnore(), ReadOnly()]
		public System.UInt64 _Identity
		{
			get
			{
				return __Identity;
			}

			set
			{
				if (__Identity != value)
				{
					__Identity = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_Identity)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCore.TcoObject target)
		{
			target._Identity.Cyclic = _Identity;
		}

		public void CopyPlainToCyclic(TcoCore.ITcoObject target)
		{
			this.CopyPlainToCyclic((TcoCore.TcoObject)target);
		}

		public void CopyPlainToShadow(TcoCore.TcoObject target)
		{
			target._Identity.Shadow = _Identity;
		}

		public void CopyPlainToShadow(TcoCore.IShadowTcoObject target)
		{
			this.CopyPlainToShadow((TcoCore.TcoObject)target);
		}

		public void CopyCyclicToPlain(TcoCore.TcoObject source)
		{
			_Identity = source._Identity.LastValue;
		}

		public void CopyCyclicToPlain(TcoCore.ITcoObject source)
		{
			this.CopyCyclicToPlain((TcoCore.TcoObject)source);
		}

		public void CopyShadowToPlain(TcoCore.TcoObject source)
		{
			_Identity = source._Identity.Shadow;
		}

		public void CopyShadowToPlain(TcoCore.IShadowTcoObject source)
		{
			this.CopyShadowToPlain((TcoCore.TcoObject)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoObject()
		{
		}
	}
}