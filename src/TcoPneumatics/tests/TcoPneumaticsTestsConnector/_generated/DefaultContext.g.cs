using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoPneumaticsTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "DefaultContext", "TcoPneumaticsTests", TypeComplexityEnum.Complex)]
	public partial class DefaultContext : TcoCore.TcoContext, Vortex.Connector.IVortexObject, IDefaultContext, IShadowDefaultContext, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		TcoPneumatics.fbCylinder __wpfCyclinder;
		public TcoPneumatics.fbCylinder _wpfCyclinder
		{
			get
			{
				return __wpfCyclinder;
			}
		}

		TcoPneumatics.IfbCylinder IDefaultContext._wpfCyclinder
		{
			get
			{
				return _wpfCyclinder;
			}
		}

		TcoPneumatics.IShadowfbCylinder IShadowDefaultContext._wpfCyclinder
		{
			get
			{
				return _wpfCyclinder;
			}
		}

		TcoPneumatics.fbCylinder __wpfCyclinder_1;
		public TcoPneumatics.fbCylinder _wpfCyclinder_1
		{
			get
			{
				return __wpfCyclinder_1;
			}
		}

		TcoPneumatics.IfbCylinder IDefaultContext._wpfCyclinder_1
		{
			get
			{
				return _wpfCyclinder_1;
			}
		}

		TcoPneumatics.IShadowfbCylinder IShadowDefaultContext._wpfCyclinder_1
		{
			get
			{
				return _wpfCyclinder_1;
			}
		}

		TcoPneumatics.fbCylinder __wpfCyclinder_2;
		public TcoPneumatics.fbCylinder _wpfCyclinder_2
		{
			get
			{
				return __wpfCyclinder_2;
			}
		}

		TcoPneumatics.IfbCylinder IDefaultContext._wpfCyclinder_2
		{
			get
			{
				return _wpfCyclinder_2;
			}
		}

		TcoPneumatics.IShadowfbCylinder IShadowDefaultContext._wpfCyclinder_2
		{
			get
			{
				return _wpfCyclinder_2;
			}
		}

		TcoPneumatics.fbCylinder __wpfCyclinder_3;
		public TcoPneumatics.fbCylinder _wpfCyclinder_3
		{
			get
			{
				return __wpfCyclinder_3;
			}
		}

		TcoPneumatics.IfbCylinder IDefaultContext._wpfCyclinder_3
		{
			get
			{
				return _wpfCyclinder_3;
			}
		}

		TcoPneumatics.IShadowfbCylinder IShadowDefaultContext._wpfCyclinder_3
		{
			get
			{
				return _wpfCyclinder_3;
			}
		}

		public new void LazyOnlineToShadow()
		{
			base.LazyOnlineToShadow();
			_wpfCyclinder.LazyOnlineToShadow();
			_wpfCyclinder_1.LazyOnlineToShadow();
			_wpfCyclinder_2.LazyOnlineToShadow();
			_wpfCyclinder_3.LazyOnlineToShadow();
		}

		public new void LazyShadowToOnline()
		{
			base.LazyShadowToOnline();
			_wpfCyclinder.LazyShadowToOnline();
			_wpfCyclinder_1.LazyShadowToOnline();
			_wpfCyclinder_2.LazyShadowToOnline();
			_wpfCyclinder_3.LazyShadowToOnline();
		}

		public new PlainDefaultContext CreatePlainerType()
		{
			var cloned = new PlainDefaultContext();
			base.CreatePlainerType(cloned);
			cloned._wpfCyclinder = _wpfCyclinder.CreatePlainerType();
			cloned._wpfCyclinder_1 = _wpfCyclinder_1.CreatePlainerType();
			cloned._wpfCyclinder_2 = _wpfCyclinder_2.CreatePlainerType();
			cloned._wpfCyclinder_3 = _wpfCyclinder_3.CreatePlainerType();
			return cloned;
		}

		protected PlainDefaultContext CreatePlainerType(PlainDefaultContext cloned)
		{
			base.CreatePlainerType(cloned);
			cloned._wpfCyclinder = _wpfCyclinder.CreatePlainerType();
			cloned._wpfCyclinder_1 = _wpfCyclinder_1.CreatePlainerType();
			cloned._wpfCyclinder_2 = _wpfCyclinder_2.CreatePlainerType();
			cloned._wpfCyclinder_3 = _wpfCyclinder_3.CreatePlainerType();
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
		partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexConstructorParameterless();
		public void FlushPlainToOnline(TcoPneumaticsTests.PlainDefaultContext source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoPneumaticsTests.PlainDefaultContext source)
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

		public void FlushOnlineToPlain(TcoPneumaticsTests.PlainDefaultContext source)
		{
			this.Read();
			source.CopyCyclicToPlain(this);
		}

		public DefaultContext(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail): base (parent, readableTail, symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			__wpfCyclinder = new TcoPneumatics.fbCylinder(this, "", "_wpfCyclinder");
			__wpfCyclinder_1 = new TcoPneumatics.fbCylinder(this, "", "_wpfCyclinder_1");
			__wpfCyclinder_2 = new TcoPneumatics.fbCylinder(this, "", "_wpfCyclinder_2");
			__wpfCyclinder_3 = new TcoPneumatics.fbCylinder(this, "", "_wpfCyclinder_3");
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
		}

		public DefaultContext(): base ()
		{
			PexPreConstructorParameterless();
			__wpfCyclinder = new TcoPneumatics.fbCylinder();
			__wpfCyclinder_1 = new TcoPneumatics.fbCylinder();
			__wpfCyclinder_2 = new TcoPneumatics.fbCylinder();
			__wpfCyclinder_3 = new TcoPneumatics.fbCylinder();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcDefaultContext : TcoCore.TcoContext.PlcTcoContext
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcDefaultContext()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IDefaultContext : Vortex.Connector.IVortexOnlineObject, TcoCore.ITcoContext
	{
		TcoPneumatics.IfbCylinder _wpfCyclinder
		{
			get;
		}

		TcoPneumatics.IfbCylinder _wpfCyclinder_1
		{
			get;
		}

		TcoPneumatics.IfbCylinder _wpfCyclinder_2
		{
			get;
		}

		TcoPneumatics.IfbCylinder _wpfCyclinder_3
		{
			get;
		}

		new TcoPneumaticsTests.PlainDefaultContext CreatePlainerType();
		new void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoPneumaticsTests.PlainDefaultContext source);
		void FlushOnlineToPlain(TcoPneumaticsTests.PlainDefaultContext source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowDefaultContext : Vortex.Connector.IVortexShadowObject, TcoCore.IShadowTcoContext
	{
		TcoPneumatics.IShadowfbCylinder _wpfCyclinder
		{
			get;
		}

		TcoPneumatics.IShadowfbCylinder _wpfCyclinder_1
		{
			get;
		}

		TcoPneumatics.IShadowfbCylinder _wpfCyclinder_2
		{
			get;
		}

		TcoPneumatics.IShadowfbCylinder _wpfCyclinder_3
		{
			get;
		}

		new TcoPneumaticsTests.PlainDefaultContext CreatePlainerType();
		new void FlushShadowToOnline();
		void CopyPlainToShadow(TcoPneumaticsTests.PlainDefaultContext source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainDefaultContext : TcoCore.PlainTcoContext
	{
		TcoPneumatics.PlainfbCylinder __wpfCyclinder;
		public TcoPneumatics.PlainfbCylinder _wpfCyclinder
		{
			get
			{
				return __wpfCyclinder;
			}

			set
			{
				if (__wpfCyclinder != value)
				{
					__wpfCyclinder = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_wpfCyclinder)));
				}
			}
		}

		TcoPneumatics.PlainfbCylinder __wpfCyclinder_1;
		public TcoPneumatics.PlainfbCylinder _wpfCyclinder_1
		{
			get
			{
				return __wpfCyclinder_1;
			}

			set
			{
				if (__wpfCyclinder_1 != value)
				{
					__wpfCyclinder_1 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_wpfCyclinder_1)));
				}
			}
		}

		TcoPneumatics.PlainfbCylinder __wpfCyclinder_2;
		public TcoPneumatics.PlainfbCylinder _wpfCyclinder_2
		{
			get
			{
				return __wpfCyclinder_2;
			}

			set
			{
				if (__wpfCyclinder_2 != value)
				{
					__wpfCyclinder_2 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_wpfCyclinder_2)));
				}
			}
		}

		TcoPneumatics.PlainfbCylinder __wpfCyclinder_3;
		public TcoPneumatics.PlainfbCylinder _wpfCyclinder_3
		{
			get
			{
				return __wpfCyclinder_3;
			}

			set
			{
				if (__wpfCyclinder_3 != value)
				{
					__wpfCyclinder_3 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_wpfCyclinder_3)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoPneumaticsTests.DefaultContext target)
		{
			base.CopyPlainToCyclic(target);
			_wpfCyclinder.CopyPlainToCyclic(target._wpfCyclinder);
			_wpfCyclinder_1.CopyPlainToCyclic(target._wpfCyclinder_1);
			_wpfCyclinder_2.CopyPlainToCyclic(target._wpfCyclinder_2);
			_wpfCyclinder_3.CopyPlainToCyclic(target._wpfCyclinder_3);
		}

		public void CopyPlainToCyclic(TcoPneumaticsTests.IDefaultContext target)
		{
			this.CopyPlainToCyclic((TcoPneumaticsTests.DefaultContext)target);
		}

		public void CopyPlainToShadow(TcoPneumaticsTests.DefaultContext target)
		{
			base.CopyPlainToShadow(target);
			_wpfCyclinder.CopyPlainToShadow(target._wpfCyclinder);
			_wpfCyclinder_1.CopyPlainToShadow(target._wpfCyclinder_1);
			_wpfCyclinder_2.CopyPlainToShadow(target._wpfCyclinder_2);
			_wpfCyclinder_3.CopyPlainToShadow(target._wpfCyclinder_3);
		}

		public void CopyPlainToShadow(TcoPneumaticsTests.IShadowDefaultContext target)
		{
			this.CopyPlainToShadow((TcoPneumaticsTests.DefaultContext)target);
		}

		public void CopyCyclicToPlain(TcoPneumaticsTests.DefaultContext source)
		{
			base.CopyCyclicToPlain(source);
			_wpfCyclinder.CopyCyclicToPlain(source._wpfCyclinder);
			_wpfCyclinder_1.CopyCyclicToPlain(source._wpfCyclinder_1);
			_wpfCyclinder_2.CopyCyclicToPlain(source._wpfCyclinder_2);
			_wpfCyclinder_3.CopyCyclicToPlain(source._wpfCyclinder_3);
		}

		public void CopyCyclicToPlain(TcoPneumaticsTests.IDefaultContext source)
		{
			this.CopyCyclicToPlain((TcoPneumaticsTests.DefaultContext)source);
		}

		public void CopyShadowToPlain(TcoPneumaticsTests.DefaultContext source)
		{
			base.CopyShadowToPlain(source);
			_wpfCyclinder.CopyShadowToPlain(source._wpfCyclinder);
			_wpfCyclinder_1.CopyShadowToPlain(source._wpfCyclinder_1);
			_wpfCyclinder_2.CopyShadowToPlain(source._wpfCyclinder_2);
			_wpfCyclinder_3.CopyShadowToPlain(source._wpfCyclinder_3);
		}

		public void CopyShadowToPlain(TcoPneumaticsTests.IShadowDefaultContext source)
		{
			this.CopyShadowToPlain((TcoPneumaticsTests.DefaultContext)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainDefaultContext()
		{
			__wpfCyclinder = new TcoPneumatics.PlainfbCylinder();
			__wpfCyclinder_1 = new TcoPneumatics.PlainfbCylinder();
			__wpfCyclinder_2 = new TcoPneumatics.PlainfbCylinder();
			__wpfCyclinder_3 = new TcoPneumatics.PlainfbCylinder();
		}
	}
}