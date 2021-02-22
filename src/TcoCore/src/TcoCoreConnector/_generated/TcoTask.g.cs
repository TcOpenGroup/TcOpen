using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoTask", "TcoCore", TypeComplexityEnum.Complex)]
	public partial class TcoTask : TcoObject, Vortex.Connector.IVortexObject, ITcoTask, IShadowTcoTask, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		Vortex.Connector.ValueTypes.OnlinerInt __taskState;
		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTaskState))]
		public Vortex.Connector.ValueTypes.OnlinerInt _taskState
		{
			get
			{
				return __taskState;
			}
		}

		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTaskState))]
		Vortex.Connector.ValueTypes.Online.IOnlineInt ITcoTask._taskState
		{
			get
			{
				return _taskState;
			}
		}

		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTaskState))]
		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadowTcoTask._taskState
		{
			get
			{
				return _taskState;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerULInt __nextExpectedCycle;
		[RenderIgnore(), ReadOnly()]
		public Vortex.Connector.ValueTypes.OnlinerULInt _nextExpectedCycle
		{
			get
			{
				return __nextExpectedCycle;
			}
		}

		[RenderIgnore(), ReadOnly()]
		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoTask._nextExpectedCycle
		{
			get
			{
				return _nextExpectedCycle;
			}
		}

		[RenderIgnore(), ReadOnly()]
		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoTask._nextExpectedCycle
		{
			get
			{
				return _nextExpectedCycle;
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

		ITcoMessenger ITcoTask._messenger
		{
			get
			{
				return _messenger;
			}
		}

		IShadowTcoMessenger IShadowTcoTask._messenger
		{
			get
			{
				return _messenger;
			}
		}

		public new void LazyOnlineToShadow()
		{
			base.LazyOnlineToShadow();
			_taskState.Shadow = _taskState.LastValue;
			_nextExpectedCycle.Shadow = _nextExpectedCycle.LastValue;
			_messenger.LazyOnlineToShadow();
		}

		public new void LazyShadowToOnline()
		{
			base.LazyShadowToOnline();
			_taskState.Cyclic = _taskState.Shadow;
			_nextExpectedCycle.Cyclic = _nextExpectedCycle.Shadow;
			_messenger.LazyShadowToOnline();
		}

		public new PlainTcoTask CreatePlainerType()
		{
			var cloned = new PlainTcoTask();
			base.CreatePlainerType(cloned);
			cloned._messenger = _messenger.CreatePlainerType();
			return cloned;
		}

		protected PlainTcoTask CreatePlainerType(PlainTcoTask cloned)
		{
			base.CreatePlainerType(cloned);
			cloned._messenger = _messenger.CreatePlainerType();
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
		partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexConstructorParameterless();
		public void FlushPlainToOnline(TcoCore.PlainTcoTask source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCore.PlainTcoTask source)
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

		public void FlushOnlineToPlain(TcoCore.PlainTcoTask source)
		{
			this.Read();
			source.CopyCyclicToPlain(this);
		}

		public TcoTask(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail): base (parent, readableTail, symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			__taskState = @Connector.Online.Adapter.CreateINT(this, "", "_taskState");
			__nextExpectedCycle = @Connector.Online.Adapter.CreateULINT(this, "", "_nextExpectedCycle");
			_nextExpectedCycle.MakeReadOnly();
			__messenger = new TcoMessenger(this, "", "_messenger");
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
		}

		public TcoTask(): base ()
		{
			PexPreConstructorParameterless();
			__taskState = Vortex.Connector.IConnectorFactory.CreateINT();
			__nextExpectedCycle = Vortex.Connector.IConnectorFactory.CreateULINT();
			__messenger = new TcoMessenger();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoTask : TcoCore.TcoObject.PlcTcoObject
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoTask()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoTask : Vortex.Connector.IVortexOnlineObject, TcoCore.ITcoObject
	{
		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTaskState))]
		Vortex.Connector.ValueTypes.Online.IOnlineInt _taskState
		{
			get;
		}

		[RenderIgnore(), ReadOnly()]
		Vortex.Connector.ValueTypes.Online.IOnlineULInt _nextExpectedCycle
		{
			get;
		}

		ITcoMessenger _messenger
		{
			get;
		}

		new TcoCore.PlainTcoTask CreatePlainerType();
		new void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCore.PlainTcoTask source);
		void FlushOnlineToPlain(TcoCore.PlainTcoTask source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoTask : Vortex.Connector.IVortexShadowObject, TcoCore.IShadowTcoObject
	{
		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTaskState))]
		Vortex.Connector.ValueTypes.Shadows.IShadowInt _taskState
		{
			get;
		}

		[RenderIgnore(), ReadOnly()]
		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _nextExpectedCycle
		{
			get;
		}

		IShadowTcoMessenger _messenger
		{
			get;
		}

		new TcoCore.PlainTcoTask CreatePlainerType();
		new void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCore.PlainTcoTask source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoTask : TcoCore.PlainTcoObject
	{
		System.Int16 __taskState;
		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eTaskState))]
		public System.Int16 _taskState
		{
			get
			{
				return __taskState;
			}

			set
			{
				if (__taskState != value)
				{
					__taskState = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_taskState)));
				}
			}
		}

		System.UInt64 __nextExpectedCycle;
		[RenderIgnore(), ReadOnly()]
		public System.UInt64 _nextExpectedCycle
		{
			get
			{
				return __nextExpectedCycle;
			}

			set
			{
				if (__nextExpectedCycle != value)
				{
					__nextExpectedCycle = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_nextExpectedCycle)));
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

		public void CopyPlainToCyclic(TcoCore.TcoTask target)
		{
			base.CopyPlainToCyclic(target);
			target._taskState.Cyclic = _taskState;
			target._nextExpectedCycle.Cyclic = _nextExpectedCycle;
			_messenger.CopyPlainToCyclic(target._messenger);
		}

		public void CopyPlainToCyclic(TcoCore.ITcoTask target)
		{
			this.CopyPlainToCyclic((TcoCore.TcoTask)target);
		}

		public void CopyPlainToShadow(TcoCore.TcoTask target)
		{
			base.CopyPlainToShadow(target);
			target._taskState.Shadow = _taskState;
			target._nextExpectedCycle.Shadow = _nextExpectedCycle;
			_messenger.CopyPlainToShadow(target._messenger);
		}

		public void CopyPlainToShadow(TcoCore.IShadowTcoTask target)
		{
			this.CopyPlainToShadow((TcoCore.TcoTask)target);
		}

		public void CopyCyclicToPlain(TcoCore.TcoTask source)
		{
			base.CopyCyclicToPlain(source);
			_taskState = source._taskState.LastValue;
			_nextExpectedCycle = source._nextExpectedCycle.LastValue;
			_messenger.CopyCyclicToPlain(source._messenger);
		}

		public void CopyCyclicToPlain(TcoCore.ITcoTask source)
		{
			this.CopyCyclicToPlain((TcoCore.TcoTask)source);
		}

		public void CopyShadowToPlain(TcoCore.TcoTask source)
		{
			base.CopyShadowToPlain(source);
			_taskState = source._taskState.Shadow;
			_nextExpectedCycle = source._nextExpectedCycle.Shadow;
			_messenger.CopyShadowToPlain(source._messenger);
		}

		public void CopyShadowToPlain(TcoCore.IShadowTcoTask source)
		{
			this.CopyShadowToPlain((TcoCore.TcoTask)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoTask()
		{
			__messenger = new PlainTcoMessenger();
		}
	}
}