using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoPneumatics
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "fbTaskTests", "TcoPneumatics", TypeComplexityEnum.Complex)]
	internal partial class fbTaskTests : fbComponent, Vortex.Connector.IVortexObject, IfbTaskTests, IShadowfbTaskTests, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		Vortex.Connector.ValueTypes.OnlinerUInt __task_index;
		public Vortex.Connector.ValueTypes.OnlinerUInt _task_index
		{
			get
			{
				return __task_index;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineUInt IfbTaskTests._task_index
		{
			get
			{
				return _task_index;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowUInt IShadowfbTaskTests._task_index
		{
			get
			{
				return _task_index;
			}
		}

		public fbTask[] _tasks
		{
			get;
			set;
		}

		IfbTask[] IfbTaskTests._tasks
		{
			get
			{
				return _tasks;
			}

			set
			{
				_tasks = (fbTask[])value;
			}
		}

		IShadowfbTask[] IShadowfbTaskTests._tasks
		{
			get
			{
				return _tasks;
			}
		}

		public Vortex.Connector.ValueTypes.OnlinerUInt[] _task_lock_group
		{
			get;
			set;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineUInt[] IfbTaskTests._task_lock_group
		{
			get
			{
				return _task_lock_group;
			}

			set
			{
				_task_lock_group = (Vortex.Connector.ValueTypes.OnlinerUInt[])value;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowUInt[] IShadowfbTaskTests._task_lock_group
		{
			get
			{
				return _task_lock_group;
			}
		}

		public Vortex.Connector.ValueTypes.OnlinerBool[] _task_done_condition
		{
			get;
			set;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool[] IfbTaskTests._task_done_condition
		{
			get
			{
				return _task_done_condition;
			}

			set
			{
				_task_done_condition = (Vortex.Connector.ValueTypes.OnlinerBool[])value;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool[] IShadowfbTaskTests._task_done_condition
		{
			get
			{
				return _task_done_condition;
			}
		}

		public Vortex.Connector.ValueTypes.OnlinerBool[] _task_fail_condition
		{
			get;
			set;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool[] IfbTaskTests._task_fail_condition
		{
			get
			{
				return _task_fail_condition;
			}

			set
			{
				_task_fail_condition = (Vortex.Connector.ValueTypes.OnlinerBool[])value;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool[] IShadowfbTaskTests._task_fail_condition
		{
			get
			{
				return _task_fail_condition;
			}
		}

		public const System.UInt16 __constNUMBER_OF_TASKS = 4;
		Vortex.Connector.ValueTypes.OnlinerUInt _NUMBER_OF_TASKS;
		public Vortex.Connector.ValueTypes.OnlinerUInt NUMBER_OF_TASKS
		{
			get
			{
				return _NUMBER_OF_TASKS;
			}
		}

		public new void LazyOnlineToShadow()
		{
			base.LazyOnlineToShadow();
			_task_index.Shadow = _task_index.LastValue;
			Vortex.Connector.BuilderHelpers.Arrays.CopyCyclicToShadowComplex<fbTask>(_tasks);
			Vortex.Connector.BuilderHelpers.Arrays.CopyCyclicToShadowPrimitive<Vortex.Connector.ValueTypes.OnlinerUInt>(_task_lock_group);
			Vortex.Connector.BuilderHelpers.Arrays.CopyCyclicToShadowPrimitive<Vortex.Connector.ValueTypes.OnlinerBool>(_task_done_condition);
			Vortex.Connector.BuilderHelpers.Arrays.CopyCyclicToShadowPrimitive<Vortex.Connector.ValueTypes.OnlinerBool>(_task_fail_condition);
		}

		public new void LazyShadowToOnline()
		{
			base.LazyShadowToOnline();
			_task_index.Cyclic = _task_index.Shadow;
			Vortex.Connector.BuilderHelpers.Arrays.CopyShadowToCyclicComplex<fbTask>(_tasks);
			Vortex.Connector.BuilderHelpers.Arrays.CopyShadowToCyclicPrimitive<Vortex.Connector.ValueTypes.OnlinerUInt>(_task_lock_group);
			Vortex.Connector.BuilderHelpers.Arrays.CopyShadowToCyclicPrimitive<Vortex.Connector.ValueTypes.OnlinerBool>(_task_done_condition);
			Vortex.Connector.BuilderHelpers.Arrays.CopyShadowToCyclicPrimitive<Vortex.Connector.ValueTypes.OnlinerBool>(_task_fail_condition);
		}

		public new PlainfbTaskTests CreatePlainerType()
		{
			var cloned = new PlainfbTaskTests();
			base.CreatePlainerType(cloned);
			cloned._tasks = new PlainfbTask[__constNUMBER_OF_TASKS + 1];
			Vortex.Connector.BuilderHelpers.Arrays.CreatePlainerType<PlainfbTask>(cloned._tasks);
			cloned._task_lock_group = new System.UInt16[__constNUMBER_OF_TASKS + 1];
			cloned._task_done_condition = new System.Boolean[__constNUMBER_OF_TASKS + 1];
			cloned._task_fail_condition = new System.Boolean[__constNUMBER_OF_TASKS + 1];
			return cloned;
		}

		protected PlainfbTaskTests CreatePlainerType(PlainfbTaskTests cloned)
		{
			base.CreatePlainerType(cloned);
			cloned._tasks = new PlainfbTask[__constNUMBER_OF_TASKS + 1];
			Vortex.Connector.BuilderHelpers.Arrays.CreatePlainerType<PlainfbTask>(cloned._tasks);
			cloned._task_lock_group = new System.UInt16[__constNUMBER_OF_TASKS + 1];
			cloned._task_done_condition = new System.Boolean[__constNUMBER_OF_TASKS + 1];
			cloned._task_fail_condition = new System.Boolean[__constNUMBER_OF_TASKS + 1];
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
		partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexConstructorParameterless();
		public void FlushPlainToOnline(TcoPneumatics.PlainfbTaskTests source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoPneumatics.PlainfbTaskTests source)
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

		public void FlushOnlineToPlain(TcoPneumatics.PlainfbTaskTests source)
		{
			this.Read();
			source.CopyCyclicToPlain(this);
		}

		internal fbTaskTests(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail): base (parent, readableTail, symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			__task_index = @Connector.Online.Adapter.CreateUINT(this, "", "_task_index");
			_tasks = new fbTask[__constNUMBER_OF_TASKS + 1];
			Vortex.Connector.BuilderHelpers.Arrays.InstantiateArray(_tasks, this, "", "_tasks", (p, rt, st) => new fbTask(p, rt, st));
			_task_lock_group = new Vortex.Connector.ValueTypes.OnlinerUInt[__constNUMBER_OF_TASKS + 1];
			Vortex.Connector.BuilderHelpers.Arrays.InstantiateArray(_task_lock_group, this, "", "_task_lock_group", (p, rt, st) => @Connector.Online.Adapter.CreateUINT(p, rt, st));
			_task_done_condition = new Vortex.Connector.ValueTypes.OnlinerBool[__constNUMBER_OF_TASKS + 1];
			Vortex.Connector.BuilderHelpers.Arrays.InstantiateArray(_task_done_condition, this, "", "_task_done_condition", (p, rt, st) => @Connector.Online.Adapter.CreateBOOL(p, rt, st));
			_task_fail_condition = new Vortex.Connector.ValueTypes.OnlinerBool[__constNUMBER_OF_TASKS + 1];
			Vortex.Connector.BuilderHelpers.Arrays.InstantiateArray(_task_fail_condition, this, "", "_task_fail_condition", (p, rt, st) => @Connector.Online.Adapter.CreateBOOL(p, rt, st));
			_NUMBER_OF_TASKS = @Connector.Online.Adapter.CreateUINT(this, "", "NUMBER_OF_TASKS");
			NUMBER_OF_TASKS.MakeReadOnly();
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
		}

		internal fbTaskTests(): base ()
		{
			PexPreConstructorParameterless();
			__task_index = Vortex.Connector.IConnectorFactory.CreateUINT();
			_tasks = new fbTask[__constNUMBER_OF_TASKS + 1];
			Vortex.Connector.BuilderHelpers.Arrays.InstantiateArray(_tasks, () => new fbTask());
			_task_lock_group = new Vortex.Connector.ValueTypes.OnlinerUInt[__constNUMBER_OF_TASKS + 1];
			Vortex.Connector.BuilderHelpers.Arrays.InstantiateArray(_task_lock_group, () => Vortex.Connector.IConnectorFactory.CreateUINT());
			_task_done_condition = new Vortex.Connector.ValueTypes.OnlinerBool[__constNUMBER_OF_TASKS + 1];
			Vortex.Connector.BuilderHelpers.Arrays.InstantiateArray(_task_done_condition, () => Vortex.Connector.IConnectorFactory.CreateBOOL());
			_task_fail_condition = new Vortex.Connector.ValueTypes.OnlinerBool[__constNUMBER_OF_TASKS + 1];
			Vortex.Connector.BuilderHelpers.Arrays.InstantiateArray(_task_fail_condition, () => Vortex.Connector.IConnectorFactory.CreateBOOL());
			_NUMBER_OF_TASKS = Vortex.Connector.IConnectorFactory.CreateUINT();
			AttributeName = "";
			PexConstructorParameterless();
		}

		public void AbortTest(System.UInt16 taskIndex)
		{
			Connector.InvokeRpc(this.Symbol, "AbortTest", new object[]{taskIndex});
		}

		public System.Boolean GetBusyTest(System.UInt16 taskIndex)
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "GetBusyTest", new object[]{taskIndex});
		}

		public System.Boolean GetDoneTest(System.UInt16 taskIndex)
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "GetDoneTest", new object[]{taskIndex});
		}

		public System.Boolean GetErrorTest(System.UInt16 taskIndex)
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "GetErrorTest", new object[]{taskIndex});
		}

		public void InvokeTaskTest(System.UInt16 taskIndex)
		{
			Connector.InvokeRpc(this.Symbol, "InvokeTaskTest", new object[]{taskIndex});
		}

		public System.Boolean InvokeTaskTests_with_done(System.UInt16 taskIndex)
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "InvokeTaskTests_with_done", new object[]{taskIndex});
		}

		public void ResetComponent()
		{
			Connector.InvokeRpc(this.Symbol, "ResetComponent", new object[]{});
		}

		public void ResetTask(System.UInt16 taskIndex)
		{
			Connector.InvokeRpc(this.Symbol, "ResetTask", new object[]{taskIndex});
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcfbTaskTests : TcoPneumatics.fbComponent.PlcfbComponent
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcfbTaskTests()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	internal partial interface IfbTaskTests : Vortex.Connector.IVortexOnlineObject, TcoPneumatics.IfbComponent
	{
		Vortex.Connector.ValueTypes.Online.IOnlineUInt _task_index
		{
			get;
		}

		IfbTask[] _tasks
		{
			get;
			set;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineUInt[] _task_lock_group
		{
			get;
			set;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool[] _task_done_condition
		{
			get;
			set;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool[] _task_fail_condition
		{
			get;
			set;
		}

		new TcoPneumatics.PlainfbTaskTests CreatePlainerType();
		new void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoPneumatics.PlainfbTaskTests source);
		void FlushOnlineToPlain(TcoPneumatics.PlainfbTaskTests source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	internal partial interface IShadowfbTaskTests : Vortex.Connector.IVortexShadowObject, TcoPneumatics.IShadowfbComponent
	{
		Vortex.Connector.ValueTypes.Shadows.IShadowUInt _task_index
		{
			get;
		}

		IShadowfbTask[] _tasks
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowUInt[] _task_lock_group
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool[] _task_done_condition
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool[] _task_fail_condition
		{
			get;
		}

		new TcoPneumatics.PlainfbTaskTests CreatePlainerType();
		new void FlushShadowToOnline();
		void CopyPlainToShadow(TcoPneumatics.PlainfbTaskTests source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	internal partial class PlainfbTaskTests : TcoPneumatics.PlainfbComponent
	{
		System.UInt16 __task_index;
		public System.UInt16 _task_index
		{
			get
			{
				return __task_index;
			}

			set
			{
				if (__task_index != value)
				{
					__task_index = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_task_index)));
				}
			}
		}

		public PlainfbTask[] _tasks
		{
			get;
			set;
		}

		public System.UInt16[] _task_lock_group
		{
			get;
			set;
		}

		public System.Boolean[] _task_done_condition
		{
			get;
			set;
		}

		public System.Boolean[] _task_fail_condition
		{
			get;
			set;
		}

		public const System.UInt16 __constNUMBER_OF_TASKS = 4;
		System.UInt16 _NUMBER_OF_TASKS;
		public System.UInt16 NUMBER_OF_TASKS
		{
			get
			{
				return _NUMBER_OF_TASKS;
			}

			set
			{
				if (_NUMBER_OF_TASKS != value)
				{
					_NUMBER_OF_TASKS = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(NUMBER_OF_TASKS)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoPneumatics.fbTaskTests target)
		{
			base.CopyPlainToCyclic(target);
			target._task_index.Cyclic = _task_index;
			Vortex.Connector.BuilderHelpers.Arrays.CopyPlainToOnline<PlainfbTask, fbTask>(_tasks, target._tasks);
			Vortex.Connector.BuilderHelpers.Arrays.CopyPlainToOnline<System.UInt16, Vortex.Connector.ValueTypes.OnlinerUInt>(_task_lock_group, target._task_lock_group);
			Vortex.Connector.BuilderHelpers.Arrays.CopyPlainToOnline<System.Boolean, Vortex.Connector.ValueTypes.OnlinerBool>(_task_done_condition, target._task_done_condition);
			Vortex.Connector.BuilderHelpers.Arrays.CopyPlainToOnline<System.Boolean, Vortex.Connector.ValueTypes.OnlinerBool>(_task_fail_condition, target._task_fail_condition);
			target.NUMBER_OF_TASKS.Cyclic = NUMBER_OF_TASKS;
		}

		public void CopyPlainToCyclic(TcoPneumatics.IfbTaskTests target)
		{
			this.CopyPlainToCyclic((TcoPneumatics.fbTaskTests)target);
		}

		public void CopyPlainToShadow(TcoPneumatics.fbTaskTests target)
		{
			base.CopyPlainToShadow(target);
			target._task_index.Shadow = _task_index;
			Vortex.Connector.BuilderHelpers.Arrays.CopyPlainToShadow<PlainfbTask, fbTask>(_tasks, target._tasks);
			Vortex.Connector.BuilderHelpers.Arrays.CopyPlainToShadow<System.UInt16, Vortex.Connector.ValueTypes.OnlinerUInt>(_task_lock_group, target._task_lock_group);
			Vortex.Connector.BuilderHelpers.Arrays.CopyPlainToShadow<System.Boolean, Vortex.Connector.ValueTypes.OnlinerBool>(_task_done_condition, target._task_done_condition);
			Vortex.Connector.BuilderHelpers.Arrays.CopyPlainToShadow<System.Boolean, Vortex.Connector.ValueTypes.OnlinerBool>(_task_fail_condition, target._task_fail_condition);
			target.NUMBER_OF_TASKS.Shadow = NUMBER_OF_TASKS;
		}

		public void CopyPlainToShadow(TcoPneumatics.IShadowfbTaskTests target)
		{
			this.CopyPlainToShadow((TcoPneumatics.fbTaskTests)target);
		}

		public void CopyCyclicToPlain(TcoPneumatics.fbTaskTests source)
		{
			base.CopyCyclicToPlain(source);
			_task_index = source._task_index.LastValue;
			Vortex.Connector.BuilderHelpers.Arrays.CopyOnlineToPlain<fbTask, PlainfbTask>(source._tasks, _tasks);
			Vortex.Connector.BuilderHelpers.Arrays.CopyOnlineToPlain<Vortex.Connector.ValueTypes.OnlinerUInt, System.UInt16>(source._task_lock_group, _task_lock_group);
			Vortex.Connector.BuilderHelpers.Arrays.CopyOnlineToPlain<Vortex.Connector.ValueTypes.OnlinerBool, System.Boolean>(source._task_done_condition, _task_done_condition);
			Vortex.Connector.BuilderHelpers.Arrays.CopyOnlineToPlain<Vortex.Connector.ValueTypes.OnlinerBool, System.Boolean>(source._task_fail_condition, _task_fail_condition);
			NUMBER_OF_TASKS = source.NUMBER_OF_TASKS.LastValue;
		}

		public void CopyCyclicToPlain(TcoPneumatics.IfbTaskTests source)
		{
			this.CopyCyclicToPlain((TcoPneumatics.fbTaskTests)source);
		}

		public void CopyShadowToPlain(TcoPneumatics.fbTaskTests source)
		{
			base.CopyShadowToPlain(source);
			_task_index = source._task_index.Shadow;
			Vortex.Connector.BuilderHelpers.Arrays.CopyShadowToPlain<fbTask, PlainfbTask>(source._tasks, _tasks);
			Vortex.Connector.BuilderHelpers.Arrays.CopyShadowToPlain<Vortex.Connector.ValueTypes.OnlinerUInt, System.UInt16>(source._task_lock_group, _task_lock_group);
			Vortex.Connector.BuilderHelpers.Arrays.CopyShadowToPlain<Vortex.Connector.ValueTypes.OnlinerBool, System.Boolean>(source._task_done_condition, _task_done_condition);
			Vortex.Connector.BuilderHelpers.Arrays.CopyShadowToPlain<Vortex.Connector.ValueTypes.OnlinerBool, System.Boolean>(source._task_fail_condition, _task_fail_condition);
			NUMBER_OF_TASKS = source.NUMBER_OF_TASKS.Shadow;
		}

		public void CopyShadowToPlain(TcoPneumatics.IShadowfbTaskTests source)
		{
			this.CopyShadowToPlain((TcoPneumatics.fbTaskTests)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainfbTaskTests()
		{
			_tasks = new PlainfbTask[__constNUMBER_OF_TASKS + 1];
			Vortex.Connector.BuilderHelpers.Arrays.InstantiatePlainerType<PlainfbTask>(_tasks);
			_task_lock_group = new System.UInt16[__constNUMBER_OF_TASKS + 1];
			_task_done_condition = new System.Boolean[__constNUMBER_OF_TASKS + 1];
			_task_fail_condition = new System.Boolean[__constNUMBER_OF_TASKS + 1];
		}
	}
}