using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace PlcTcProberTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "MAIN", "PlcTcProberTests", TypeComplexityEnum.Complex)]
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
				return PlcTcProberTestsTwinController.Translator.Translate(_humanReadable).Interpolate(this);
			}

			protected set
			{
				_humanReadable = value;
			}
		}

		protected string _humanReadable;
		fbInheritanceLevel_5 _InheritanceRw;
		public fbInheritanceLevel_5 InheritanceRw
		{
			get
			{
				return _InheritanceRw;
			}
		}

		IfbInheritanceLevel_5 IMAIN.InheritanceRw
		{
			get
			{
				return InheritanceRw;
			}
		}

		IShadowfbInheritanceLevel_5 IShadowMAIN.InheritanceRw
		{
			get
			{
				return InheritanceRw;
			}
		}

		public void LazyOnlineToShadow()
		{
			InheritanceRw.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			InheritanceRw.LazyShadowToOnline();
		}

		public PlainMAIN CreatePlainerType()
		{
			var cloned = new PlainMAIN();
			cloned.InheritanceRw = InheritanceRw.CreatePlainerType();
			return cloned;
		}

		protected PlainMAIN CreatePlainerType(PlainMAIN cloned)
		{
			cloned.InheritanceRw = InheritanceRw.CreatePlainerType();
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

		public void FlushPlainToOnline(PlcTcProberTests.PlainMAIN source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(PlcTcProberTests.PlainMAIN source)
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

		public void FlushOnlineToPlain(PlcTcProberTests.PlainMAIN source)
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
				return PlcTcProberTestsTwinController.Translator.Translate(_AttributeName).Interpolate(this);
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
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			this.Kids = new System.Collections.Generic.List<Vortex.Connector.IVortexElement>();
			this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			_InheritanceRw = new fbInheritanceLevel_5(this, "", "InheritanceRw");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public MAIN()
		{
			PexPreConstructorParameterless();
			_InheritanceRw = new fbInheritanceLevel_5();
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
		IfbInheritanceLevel_5 InheritanceRw
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		PlcTcProberTests.PlainMAIN CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(PlcTcProberTests.PlainMAIN source);
		void FlushOnlineToPlain(PlcTcProberTests.PlainMAIN source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowMAIN : Vortex.Connector.IVortexShadowObject
	{
		IShadowfbInheritanceLevel_5 InheritanceRw
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		PlcTcProberTests.PlainMAIN CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(PlcTcProberTests.PlainMAIN source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainMAIN : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		PlainfbInheritanceLevel_5 _InheritanceRw;
		public PlainfbInheritanceLevel_5 InheritanceRw
		{
			get
			{
				return _InheritanceRw;
			}

			set
			{
				if (_InheritanceRw != value)
				{
					_InheritanceRw = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(InheritanceRw)));
				}
			}
		}

		public void CopyPlainToCyclic(PlcTcProberTests.MAIN target)
		{
			InheritanceRw.CopyPlainToCyclic(target.InheritanceRw);
		}

		public void CopyPlainToCyclic(PlcTcProberTests.IMAIN target)
		{
			this.CopyPlainToCyclic((PlcTcProberTests.MAIN)target);
		}

		public void CopyPlainToShadow(PlcTcProberTests.MAIN target)
		{
			InheritanceRw.CopyPlainToShadow(target.InheritanceRw);
		}

		public void CopyPlainToShadow(PlcTcProberTests.IShadowMAIN target)
		{
			this.CopyPlainToShadow((PlcTcProberTests.MAIN)target);
		}

		public void CopyCyclicToPlain(PlcTcProberTests.MAIN source)
		{
			InheritanceRw.CopyCyclicToPlain(source.InheritanceRw);
		}

		public void CopyCyclicToPlain(PlcTcProberTests.IMAIN source)
		{
			this.CopyCyclicToPlain((PlcTcProberTests.MAIN)source);
		}

		public void CopyShadowToPlain(PlcTcProberTests.MAIN source)
		{
			InheritanceRw.CopyShadowToPlain(source.InheritanceRw);
		}

		public void CopyShadowToPlain(PlcTcProberTests.IShadowMAIN source)
		{
			this.CopyShadowToPlain((PlcTcProberTests.MAIN)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainMAIN()
		{
			_InheritanceRw = new PlainfbInheritanceLevel_5();
		}
	}
}