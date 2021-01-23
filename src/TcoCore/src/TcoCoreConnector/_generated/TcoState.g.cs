using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoState", "TcoCore", TypeComplexityEnum.Complex)]
	public partial class TcoState : TcoObject, Vortex.Connector.IVortexObject, ITcoState, IShadowTcoState, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		Vortex.Connector.ValueTypes.OnlinerInt __State;
		public Vortex.Connector.ValueTypes.OnlinerInt _State
		{
			get
			{
				return __State;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineInt ITcoState._State
		{
			get
			{
				return _State;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadowTcoState._State
		{
			get
			{
				return _State;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool __enableAutoRestore;
		[RenderIgnore(), ReadOnly()]
		public Vortex.Connector.ValueTypes.OnlinerBool _enableAutoRestore
		{
			get
			{
				return __enableAutoRestore;
			}
		}

		[RenderIgnore(), ReadOnly()]
		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoState._enableAutoRestore
		{
			get
			{
				return _enableAutoRestore;
			}
		}

		[RenderIgnore(), ReadOnly()]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoState._enableAutoRestore
		{
			get
			{
				return _enableAutoRestore;
			}
		}

		TcoMessenger __messenger;
		public TcoMessenger _messenger
		{
			get
			{
				return __messenger;
			}
		}

		ITcoMessenger ITcoState._messenger
		{
			get
			{
				return _messenger;
			}
		}

		IShadowTcoMessenger IShadowTcoState._messenger
		{
			get
			{
				return _messenger;
			}
		}

		public new void LazyOnlineToShadow()
		{
			base.LazyOnlineToShadow();
			_State.Shadow = _State.LastValue;
			_enableAutoRestore.Shadow = _enableAutoRestore.LastValue;
			_messenger.LazyOnlineToShadow();
		}

		public new void LazyShadowToOnline()
		{
			base.LazyShadowToOnline();
			_State.Cyclic = _State.Shadow;
			_enableAutoRestore.Cyclic = _enableAutoRestore.Shadow;
			_messenger.LazyShadowToOnline();
		}

		public new PlainTcoState CreatePlainerType()
		{
			var cloned = new PlainTcoState();
			base.CreatePlainerType(cloned);
			cloned._messenger = _messenger.CreatePlainerType();
			return cloned;
		}

		protected PlainTcoState CreatePlainerType(PlainTcoState cloned)
		{
			base.CreatePlainerType(cloned);
			cloned._messenger = _messenger.CreatePlainerType();
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
		partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexConstructorParameterless();
		public void FlushPlainToOnline(TcoCore.PlainTcoState source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCore.PlainTcoState source)
		{
			source.CopyPlainToShadow(this);
		}

		public new void FlushShadowToOnline()
		{
			this.LazyShadowToOnline();
			this.Write();
		}

		public new void FlushOnlineToShadow()
		{
			this.Read();
			this.LazyOnlineToShadow();
		}

		public void FlushOnlineToPlain(TcoCore.PlainTcoState source)
		{
			this.Read();
			source.CopyCyclicToPlain(this);
		}

		public TcoState(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail): base (parent, readableTail, symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			__State = @Connector.Online.Adapter.CreateINT(this, "", "_State");
			__enableAutoRestore = @Connector.Online.Adapter.CreateBOOL(this, "", "_enableAutoRestore");
			_enableAutoRestore.MakeReadOnly();
			__messenger = new TcoMessenger(this, "", "_messenger");
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
		}

		public TcoState(): base ()
		{
			PexPreConstructorParameterless();
			__State = Vortex.Connector.IConnectorFactory.CreateINT();
			__enableAutoRestore = Vortex.Connector.IConnectorFactory.CreateBOOL();
			__messenger = new TcoMessenger();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoState : TcoCore.TcoObject.PlcTcoObject
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoState()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoState : Vortex.Connector.IVortexOnlineObject, TcoCore.ITcoObject
	{
		Vortex.Connector.ValueTypes.Online.IOnlineInt _State
		{
			get;
		}

		[RenderIgnore(), ReadOnly()]
		Vortex.Connector.ValueTypes.Online.IOnlineBool _enableAutoRestore
		{
			get;
		}

		ITcoMessenger _messenger
		{
			get;
		}

		new TcoCore.PlainTcoState CreatePlainerType();
		new void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCore.PlainTcoState source);
		void FlushOnlineToPlain(TcoCore.PlainTcoState source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoState : Vortex.Connector.IVortexShadowObject, TcoCore.IShadowTcoObject
	{
		Vortex.Connector.ValueTypes.Shadows.IShadowInt _State
		{
			get;
		}

		[RenderIgnore(), ReadOnly()]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool _enableAutoRestore
		{
			get;
		}

		IShadowTcoMessenger _messenger
		{
			get;
		}

		new TcoCore.PlainTcoState CreatePlainerType();
		new void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCore.PlainTcoState source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoState : TcoCore.PlainTcoObject
	{
		System.Int16 __State;
		public System.Int16 _State
		{
			get
			{
				return __State;
			}

			set
			{
				if (__State != value)
				{
					__State = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_State)));
				}
			}
		}

		System.Boolean __enableAutoRestore;
		[RenderIgnore(), ReadOnly()]
		public System.Boolean _enableAutoRestore
		{
			get
			{
				return __enableAutoRestore;
			}

			set
			{
				if (__enableAutoRestore != value)
				{
					__enableAutoRestore = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_enableAutoRestore)));
				}
			}
		}

		PlainTcoMessenger __messenger;
		public PlainTcoMessenger _messenger
		{
			get
			{
				return __messenger;
			}

			set
			{
				if (__messenger != value)
				{
					__messenger = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_messenger)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCore.TcoState target)
		{
			base.CopyPlainToCyclic(target);
			target._State.Cyclic = _State;
			target._enableAutoRestore.Cyclic = _enableAutoRestore;
			_messenger.CopyPlainToCyclic(target._messenger);
		}

		public void CopyPlainToCyclic(TcoCore.ITcoState target)
		{
			this.CopyPlainToCyclic((TcoCore.TcoState)target);
		}

		public void CopyPlainToShadow(TcoCore.TcoState target)
		{
			base.CopyPlainToShadow(target);
			target._State.Shadow = _State;
			target._enableAutoRestore.Shadow = _enableAutoRestore;
			_messenger.CopyPlainToShadow(target._messenger);
		}

		public void CopyPlainToShadow(TcoCore.IShadowTcoState target)
		{
			this.CopyPlainToShadow((TcoCore.TcoState)target);
		}

		public void CopyCyclicToPlain(TcoCore.TcoState source)
		{
			base.CopyCyclicToPlain(source);
			_State = source._State.LastValue;
			_enableAutoRestore = source._enableAutoRestore.LastValue;
			_messenger.CopyCyclicToPlain(source._messenger);
		}

		public void CopyCyclicToPlain(TcoCore.ITcoState source)
		{
			this.CopyCyclicToPlain((TcoCore.TcoState)source);
		}

		public void CopyShadowToPlain(TcoCore.TcoState source)
		{
			base.CopyShadowToPlain(source);
			_State = source._State.Shadow;
			_enableAutoRestore = source._enableAutoRestore.Shadow;
			_messenger.CopyShadowToPlain(source._messenger);
		}

		public void CopyShadowToPlain(TcoCore.IShadowTcoState source)
		{
			this.CopyShadowToPlain((TcoCore.TcoState)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoState()
		{
			__messenger = new PlainTcoMessenger();
		}
	}
}