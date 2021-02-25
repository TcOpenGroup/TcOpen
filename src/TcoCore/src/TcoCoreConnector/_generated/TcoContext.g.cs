using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
	
///		<summary>
///			Basic construction container encapsulating TcoObjects, provides possibility to detect if its child member is called cyclically.
///			This function block is abstract so it cannot be instantiated and must be extended. The <see cref="TcoContext.PlcTcoContext.Run()"/> of the extended instance must be 
///			called only once per plc cycle inside one Twincat PlcTask.
///			Several TcoContext instances could be called inside one Twincat PlcTask, but only once per plc cycle. One instance must not be called inside several Twincat PlcTask.
///			The TcoContext child members could ask theirs parents TcoContext for the values of the properties <see cref="TcoContext.PlcTcoContext.StartCycleCount()"/> and <see cref="TcoContext.PlcTcoContext.EndCycleCount()"/>. 
///			Comparing with theirs internal values they could determine if they were called in the previous plc cycle. Depending on their settings they could provide AutoRestorable mechanism on themselves or on theirs child members.
///			<note type="tip">
///				For example, one TcoContext could contain all objects, components and PLC logic of one independent station of the machine, while the second TcoContext could contain all the objects of the second station of the same machine.
///				In this way, stations can be separated to prevent access from one station to another.
///			</note>
///			<remarks>			
///				<note type="caution">
///					Do not confuse Twincat PlcTask with <see cref="TcoTask.PlcTcoTask()"/>.
///				</note>
///			</remarks>			
///		</summary>			
///<seealso cref="PlcTcoContext"/>
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoContext", "TcoCore", TypeComplexityEnum.Complex)]
	public partial class TcoContext : Vortex.Connector.IVortexObject, ITcoContext, IShadowTcoContext, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		
///		<summary>
///			The own identity of the TcoContext.
///			This variable is used in the higher level packages.  
///		</summary>			

		[RenderIgnore(), ReadOnly()]
		public Vortex.Connector.ValueTypes.OnlinerULInt _Identity
		{
			get
			{
				return __Identity;
			}
		}

		[RenderIgnore(), ReadOnly()]
		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoContext._Identity
		{
			get
			{
				return _Identity;
			}
		}

		[RenderIgnore(), ReadOnly()]
		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoContext._Identity
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

		public PlainTcoContext CreatePlainerType()
		{
			var cloned = new PlainTcoContext();
			return cloned;
		}

		protected PlainTcoContext CreatePlainerType(PlainTcoContext cloned)
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

		public void FlushPlainToOnline(TcoCore.PlainTcoContext source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCore.PlainTcoContext source)
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

		public void FlushOnlineToPlain(TcoCore.PlainTcoContext source)
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

		public TcoContext(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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

		public TcoContext()
		{
			PexPreConstructorParameterless();
			__Identity = Vortex.Connector.IConnectorFactory.CreateULINT();
			AttributeName = "";
			PexConstructorParameterless();
		}

		
///		<summary>
///			Basic construction container encapsulating TcoObjects, provides possibility to detect if its child member is called cyclically.
///			This function block is abstract so it cannot be instantiated and must be extended. The <see cref="TcoContext.PlcTcoContext.Run()"/> of the extended instance must be 
///			called only once per plc cycle inside one Twincat PlcTask.
///			Several TcoContext instances could be called inside one Twincat PlcTask, but only once per plc cycle. One instance must not be called inside several Twincat PlcTask.
///			The TcoContext child members could ask theirs parents TcoContext for the values of the properties <see cref="TcoContext.PlcTcoContext.StartCycleCount()"/> and <see cref="TcoContext.PlcTcoContext.EndCycleCount()"/>. 
///			Comparing with theirs internal values they could determine if they were called in the previous plc cycle. Depending on their settings they could provide AutoRestorable mechanism on themselves or on theirs child members.
///			<note type="tip">
///				For example, one TcoContext could contain all objects, components and PLC logic of one independent station of the machine, while the second TcoContext could contain all the objects of the second station of the same machine.
///				In this way, stations can be separated to prevent access from one station to another.
///			</note>
///			<remarks>			
///				<note type="caution">
///					Do not confuse Twincat PlcTask with <see cref="TcoTask.PlcTcoTask()"/>.
///				</note>
///			</remarks>			
///		</summary>			

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoContext
		{
			
///		<summary>
///			Returns the reference to this instance of the <see cref="TcoContext.PlcTcoContext()"/>.
///		</summary>			
///<summary><note type="note">This is PLC property. This method is accessible only from the PLC code.</note></summary>
///<returns>Plc type ITcoContext; Twin type: <see cref="ITcoContext"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public dynamic Context
			{
				get
				{
					throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
				}
			}

			
///		<summary>
///			Value of this property is set at the end of the <see cref="TcoContext.PlcTcoContext()"/>, in the method <see cref="TcoContext.PlcTcoContext.Close()"/> to the value of <see cref="TcoContext.PlcTcoContext.StartCycleCount"/> property.
///		</summary>			
///<summary><note type="note">This is PLC property. This method is accessible only from the PLC code.</note></summary>
///<returns>Plc type ULINT; Twin type: <see cref="Vortex.Connector.ValueTypes.OnlinerULInt"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public dynamic EndCycleCount
			{
				get
				{
					throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
				}
			}

			
///		<summary>
///			Returns the own identity of the <see cref ="TcoContext.PlcTcoContext()"/>. This value is assiged after download by calling the implicit method <c>FB_init()</c> and cannot be changed during runtime.
///			This variable is used in the higher level packages.  
///		</summary>			
///<summary><note type="note">This is PLC property. This method is accessible only from the PLC code.</note></summary>
///<returns>Plc type ULINT; Twin type: <see cref="Vortex.Connector.ValueTypes.OnlinerULInt"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public dynamic Identity
			{
				get
				{
					throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
				}
			}

			
///		<summary>
///			Value of this property is incremented at the beginning of the <see cref="TcoContext.PlcTcoContext()"/>, in the method <see cref="TcoContext.PlcTcoContext.Open()"/>.
///			By comparing this value with the internal values of the child members they are able to determine if they were called in the previous plc cycle. 
///			Depending on their settings they could provide AutoRestorable mechanism on themselves or on theirs child members.
///		</summary>			
///<summary><note type="note">This is PLC property. This method is accessible only from the PLC code.</note></summary>
///<returns>Plc type ULINT; Twin type: <see cref="Vortex.Connector.ValueTypes.OnlinerULInt"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public dynamic StartCycleCount
			{
				get
				{
					throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
				}
			}

			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoContext()
			{
			}

			
///		<summary>
///			This method is called at the end of the TcoContext. Implicit calling of this method is ensured by calling the method <see cref="TcoContext.PlcTcoContext.Run()"/>.
///			<remarks>			
///				<note type="important">
///					Do not call this method explicitly.
///				</note>
///			</remarks>
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			protected void Close()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			Main method of the TcoContext. Custom code needs to be placed here, calling of the methods <see cref="TcoContext.PlcTcoContext.Open()"/> at the beggining 
///			and <see cref="TcoContext.PlcTcoContext.Close()"/> at the end is ensured by calling the <c>InstanceName.Run()</c> method.
///			This method is abstract, so each derived type has to implement its own implementation of this method.
///		</summary>
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			protected void Main()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			This method is called at the beginning of the TcoContext. Implicit calling of this method is ensured by calling the method <see cref="TcoContext.PlcTcoContext.Run()"/>.
///			<remarks>			
///				<note type="important">
///					Do not call this method explicitly.
///				</note>
///			</remarks>
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			protected void Open()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}

			
///		<summary>
///			<para>
///				Ensures calling the <see cref="TcoContext.PlcTcoContext.Open()"/>, <see cref="TcoContext.PlcTcoContext.Main()"/> and <see cref="TcoContext.PlcTcoContext.Close()"/> methods in the desired order.
///				This method is final, so it cannot be overloaded. The <c>InstanceName.Run()</c> needs to be called cyclically.
///			</para>
///		</summary>			
///<summary><note type="note">This is PLC method. This method is invokable only from the PLC code.</note></summary>
///<returns>Plc type VOID; Twin type: <see cref="void"/></returns>

			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced), Vortex.Connector.IgnoreReflectionAttribute(), RenderIgnore()]
			public void Run()
			{
				throw new NotImplementedException("This is PLC member; not invokable form the PC side.");
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoContext : Vortex.Connector.IVortexOnlineObject
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

		TcoCore.PlainTcoContext CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCore.PlainTcoContext source);
		void FlushOnlineToPlain(TcoCore.PlainTcoContext source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoContext : Vortex.Connector.IVortexShadowObject
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

		TcoCore.PlainTcoContext CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCore.PlainTcoContext source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoContext : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
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

		public void CopyPlainToCyclic(TcoCore.TcoContext target)
		{
			target._Identity.Cyclic = _Identity;
		}

		public void CopyPlainToCyclic(TcoCore.ITcoContext target)
		{
			this.CopyPlainToCyclic((TcoCore.TcoContext)target);
		}

		public void CopyPlainToShadow(TcoCore.TcoContext target)
		{
			target._Identity.Shadow = _Identity;
		}

		public void CopyPlainToShadow(TcoCore.IShadowTcoContext target)
		{
			this.CopyPlainToShadow((TcoCore.TcoContext)target);
		}

		public void CopyCyclicToPlain(TcoCore.TcoContext source)
		{
			_Identity = source._Identity.LastValue;
		}

		public void CopyCyclicToPlain(TcoCore.ITcoContext source)
		{
			this.CopyCyclicToPlain((TcoCore.TcoContext)source);
		}

		public void CopyShadowToPlain(TcoCore.TcoContext source)
		{
			_Identity = source._Identity.Shadow;
		}

		public void CopyShadowToPlain(TcoCore.IShadowTcoContext source)
		{
			this.CopyShadowToPlain((TcoCore.TcoContext)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoContext()
		{
		}
	}
}