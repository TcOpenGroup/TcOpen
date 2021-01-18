using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoPneumatics
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "MAIN", "TcoPneumatics", TypeComplexityEnum.Complex)]
	public partial class MAIN : Vortex.Connector.IVortexObject, IMAIN, IShadowMAIN, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		fbCylinder __wpfCyclinder;
		[Container(Layout.Stack)]
		public fbCylinder _wpfCyclinder
		{
			get
			{
				return __wpfCyclinder;
			}
		}

		[Container(Layout.Stack)]
		IfbCylinder IMAIN._wpfCyclinder
		{
			get
			{
				return _wpfCyclinder;
			}
		}

		[Container(Layout.Stack)]
		IShadowfbCylinder IShadowMAIN._wpfCyclinder
		{
			get
			{
				return _wpfCyclinder;
			}
		}

		fbCylinder __wpfCyclinder_1;
		[Container(Layout.Stack)]
		public fbCylinder _wpfCyclinder_1
		{
			get
			{
				return __wpfCyclinder_1;
			}
		}

		[Container(Layout.Stack)]
		IfbCylinder IMAIN._wpfCyclinder_1
		{
			get
			{
				return _wpfCyclinder_1;
			}
		}

		[Container(Layout.Stack)]
		IShadowfbCylinder IShadowMAIN._wpfCyclinder_1
		{
			get
			{
				return _wpfCyclinder_1;
			}
		}

		fbCylinder __wpfCyclinder_2;
		[Container(Layout.Stack)]
		public fbCylinder _wpfCyclinder_2
		{
			get
			{
				return __wpfCyclinder_2;
			}
		}

		[Container(Layout.Stack)]
		IfbCylinder IMAIN._wpfCyclinder_2
		{
			get
			{
				return _wpfCyclinder_2;
			}
		}

		[Container(Layout.Stack)]
		IShadowfbCylinder IShadowMAIN._wpfCyclinder_2
		{
			get
			{
				return _wpfCyclinder_2;
			}
		}

		fbCylinder __wpfCyclinder_3;
		[Container(Layout.Stack)]
		public fbCylinder _wpfCyclinder_3
		{
			get
			{
				return __wpfCyclinder_3;
			}
		}

		[Container(Layout.Stack)]
		IfbCylinder IMAIN._wpfCyclinder_3
		{
			get
			{
				return _wpfCyclinder_3;
			}
		}

		[Container(Layout.Stack)]
		IShadowfbCylinder IShadowMAIN._wpfCyclinder_3
		{
			get
			{
				return _wpfCyclinder_3;
			}
		}

		public void LazyOnlineToShadow()
		{
			_wpfCyclinder.LazyOnlineToShadow();
			_wpfCyclinder_1.LazyOnlineToShadow();
			_wpfCyclinder_2.LazyOnlineToShadow();
			_wpfCyclinder_3.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			_wpfCyclinder.LazyShadowToOnline();
			_wpfCyclinder_1.LazyShadowToOnline();
			_wpfCyclinder_2.LazyShadowToOnline();
			_wpfCyclinder_3.LazyShadowToOnline();
		}

		public PlainMAIN CreatePlainerType()
		{
			var cloned = new PlainMAIN();
			cloned._wpfCyclinder = _wpfCyclinder.CreatePlainerType();
			cloned._wpfCyclinder_1 = _wpfCyclinder_1.CreatePlainerType();
			cloned._wpfCyclinder_2 = _wpfCyclinder_2.CreatePlainerType();
			cloned._wpfCyclinder_3 = _wpfCyclinder_3.CreatePlainerType();
			return cloned;
		}

		protected PlainMAIN CreatePlainerType(PlainMAIN cloned)
		{
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

		public void FlushPlainToOnline(TcoPneumatics.PlainMAIN source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoPneumatics.PlainMAIN source)
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

		public void FlushOnlineToPlain(TcoPneumatics.PlainMAIN source)
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

		public MAIN(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@ValueTags = new System.Collections.Generic.List<Vortex.Connector.IValueTag>();
			this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			__wpfCyclinder = new fbCylinder(this, "WPF cyclinder", "_wpfCyclinder");
			__wpfCyclinder.AttributeName = "WPF cyclinder";
			__wpfCyclinder_1 = new fbCylinder(this, "WPF cyclinder 1", "_wpfCyclinder_1");
			__wpfCyclinder_1.AttributeName = "WPF cyclinder 1";
			__wpfCyclinder_2 = new fbCylinder(this, "WPF cyclinder 2", "_wpfCyclinder_2");
			__wpfCyclinder_2.AttributeName = "WPF cyclinder 2";
			__wpfCyclinder_3 = new fbCylinder(this, "WPF cyclinder 3", "_wpfCyclinder_3");
			__wpfCyclinder_3.AttributeName = "WPF cyclinder 3";
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
			parent.AddChild(this);
		}

		public MAIN()
		{
			PexPreConstructorParameterless();
			__wpfCyclinder = new fbCylinder();
			__wpfCyclinder.AttributeName = "WPF cyclinder";
			__wpfCyclinder_1 = new fbCylinder();
			__wpfCyclinder_1.AttributeName = "WPF cyclinder 1";
			__wpfCyclinder_2 = new fbCylinder();
			__wpfCyclinder_2.AttributeName = "WPF cyclinder 2";
			__wpfCyclinder_3 = new fbCylinder();
			__wpfCyclinder_3.AttributeName = "WPF cyclinder 3";
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcMAIN
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcMAIN()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IMAIN : Vortex.Connector.IVortexOnlineObject
	{
		[Container(Layout.Stack)]
		IfbCylinder _wpfCyclinder
		{
			get;
		}

		[Container(Layout.Stack)]
		IfbCylinder _wpfCyclinder_1
		{
			get;
		}

		[Container(Layout.Stack)]
		IfbCylinder _wpfCyclinder_2
		{
			get;
		}

		[Container(Layout.Stack)]
		IfbCylinder _wpfCyclinder_3
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoPneumatics.PlainMAIN CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoPneumatics.PlainMAIN source);
		void FlushOnlineToPlain(TcoPneumatics.PlainMAIN source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowMAIN : Vortex.Connector.IVortexShadowObject
	{
		[Container(Layout.Stack)]
		IShadowfbCylinder _wpfCyclinder
		{
			get;
		}

		[Container(Layout.Stack)]
		IShadowfbCylinder _wpfCyclinder_1
		{
			get;
		}

		[Container(Layout.Stack)]
		IShadowfbCylinder _wpfCyclinder_2
		{
			get;
		}

		[Container(Layout.Stack)]
		IShadowfbCylinder _wpfCyclinder_3
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoPneumatics.PlainMAIN CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoPneumatics.PlainMAIN source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainMAIN : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		PlainfbCylinder __wpfCyclinder;
		[Container(Layout.Stack)]
		public PlainfbCylinder _wpfCyclinder
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

		PlainfbCylinder __wpfCyclinder_1;
		[Container(Layout.Stack)]
		public PlainfbCylinder _wpfCyclinder_1
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

		PlainfbCylinder __wpfCyclinder_2;
		[Container(Layout.Stack)]
		public PlainfbCylinder _wpfCyclinder_2
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

		PlainfbCylinder __wpfCyclinder_3;
		[Container(Layout.Stack)]
		public PlainfbCylinder _wpfCyclinder_3
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

		public void CopyPlainToCyclic(TcoPneumatics.MAIN target)
		{
			_wpfCyclinder.CopyPlainToCyclic(target._wpfCyclinder);
			_wpfCyclinder_1.CopyPlainToCyclic(target._wpfCyclinder_1);
			_wpfCyclinder_2.CopyPlainToCyclic(target._wpfCyclinder_2);
			_wpfCyclinder_3.CopyPlainToCyclic(target._wpfCyclinder_3);
		}

		public void CopyPlainToCyclic(TcoPneumatics.IMAIN target)
		{
			this.CopyPlainToCyclic((TcoPneumatics.MAIN)target);
		}

		public void CopyPlainToShadow(TcoPneumatics.MAIN target)
		{
			_wpfCyclinder.CopyPlainToShadow(target._wpfCyclinder);
			_wpfCyclinder_1.CopyPlainToShadow(target._wpfCyclinder_1);
			_wpfCyclinder_2.CopyPlainToShadow(target._wpfCyclinder_2);
			_wpfCyclinder_3.CopyPlainToShadow(target._wpfCyclinder_3);
		}

		public void CopyPlainToShadow(TcoPneumatics.IShadowMAIN target)
		{
			this.CopyPlainToShadow((TcoPneumatics.MAIN)target);
		}

		public void CopyCyclicToPlain(TcoPneumatics.MAIN source)
		{
			_wpfCyclinder.CopyCyclicToPlain(source._wpfCyclinder);
			_wpfCyclinder_1.CopyCyclicToPlain(source._wpfCyclinder_1);
			_wpfCyclinder_2.CopyCyclicToPlain(source._wpfCyclinder_2);
			_wpfCyclinder_3.CopyCyclicToPlain(source._wpfCyclinder_3);
		}

		public void CopyCyclicToPlain(TcoPneumatics.IMAIN source)
		{
			this.CopyCyclicToPlain((TcoPneumatics.MAIN)source);
		}

		public void CopyShadowToPlain(TcoPneumatics.MAIN source)
		{
			_wpfCyclinder.CopyShadowToPlain(source._wpfCyclinder);
			_wpfCyclinder_1.CopyShadowToPlain(source._wpfCyclinder_1);
			_wpfCyclinder_2.CopyShadowToPlain(source._wpfCyclinder_2);
			_wpfCyclinder_3.CopyShadowToPlain(source._wpfCyclinder_3);
		}

		public void CopyShadowToPlain(TcoPneumatics.IShadowMAIN source)
		{
			this.CopyShadowToPlain((TcoPneumatics.MAIN)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainMAIN()
		{
			__wpfCyclinder = new PlainfbCylinder();
			__wpfCyclinder_1 = new PlainfbCylinder();
			__wpfCyclinder_2 = new PlainfbCylinder();
			__wpfCyclinder_3 = new PlainfbCylinder();
		}
	}
}