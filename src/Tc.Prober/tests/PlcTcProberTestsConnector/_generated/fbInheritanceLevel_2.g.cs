using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace PlcTcProberTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "fbInheritanceLevel_2", "PlcTcProberTests", TypeComplexityEnum.Complex)]
	public partial class fbInheritanceLevel_2 : fbInheritanceLevel_1, Vortex.Connector.IVortexObject, IfbInheritanceLevel_2, IShadowfbInheritanceLevel_2, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		stAllTypes _level_2;
		public stAllTypes level_2
		{
			get
			{
				return _level_2;
			}
		}

		IstAllTypes IfbInheritanceLevel_2.level_2
		{
			get
			{
				return level_2;
			}
		}

		IShadowstAllTypes IShadowfbInheritanceLevel_2.level_2
		{
			get
			{
				return level_2;
			}
		}

		public new void LazyOnlineToShadow()
		{
			base.LazyOnlineToShadow();
			level_2.LazyOnlineToShadow();
		}

		public new void LazyShadowToOnline()
		{
			base.LazyShadowToOnline();
			level_2.LazyShadowToOnline();
		}

		public new PlainfbInheritanceLevel_2 CreatePlainerType()
		{
			var cloned = new PlainfbInheritanceLevel_2();
			base.CreatePlainerType(cloned);
			cloned.level_2 = level_2.CreatePlainerType();
			return cloned;
		}

		protected PlainfbInheritanceLevel_2 CreatePlainerType(PlainfbInheritanceLevel_2 cloned)
		{
			base.CreatePlainerType(cloned);
			cloned.level_2 = level_2.CreatePlainerType();
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
		partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexConstructorParameterless();
		public void FlushPlainToOnline(PlcTcProberTests.PlainfbInheritanceLevel_2 source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(PlcTcProberTests.PlainfbInheritanceLevel_2 source)
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

		public void FlushOnlineToPlain(PlcTcProberTests.PlainfbInheritanceLevel_2 source)
		{
			this.Read();
			source.CopyCyclicToPlain(this);
		}

		public fbInheritanceLevel_2(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail): base (parent, readableTail, symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			_level_2 = new stAllTypes(this, "LEVEL 2", "level_2");
			_level_2.AttributeName = "LEVEL 2";
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
		}

		public fbInheritanceLevel_2(): base ()
		{
			PexPreConstructorParameterless();
			_level_2 = new stAllTypes();
			_level_2.AttributeName = "LEVEL 2";
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcfbInheritanceLevel_2 : PlcTcProberTests.fbInheritanceLevel_1.PlcfbInheritanceLevel_1
		{
			public PlainstAllTypes level_2;
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcfbInheritanceLevel_2()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IfbInheritanceLevel_2 : Vortex.Connector.IVortexOnlineObject, PlcTcProberTests.IfbInheritanceLevel_1
	{
		IstAllTypes level_2
		{
			get;
		}

		new PlcTcProberTests.PlainfbInheritanceLevel_2 CreatePlainerType();
		new void FlushOnlineToShadow();
		void FlushPlainToOnline(PlcTcProberTests.PlainfbInheritanceLevel_2 source);
		void FlushOnlineToPlain(PlcTcProberTests.PlainfbInheritanceLevel_2 source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowfbInheritanceLevel_2 : Vortex.Connector.IVortexShadowObject, PlcTcProberTests.IShadowfbInheritanceLevel_1
	{
		IShadowstAllTypes level_2
		{
			get;
		}

		new PlcTcProberTests.PlainfbInheritanceLevel_2 CreatePlainerType();
		new void FlushShadowToOnline();
		void CopyPlainToShadow(PlcTcProberTests.PlainfbInheritanceLevel_2 source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainfbInheritanceLevel_2 : PlcTcProberTests.PlainfbInheritanceLevel_1
	{
		PlainstAllTypes _level_2;
		public PlainstAllTypes level_2
		{
			get
			{
				return _level_2;
			}

			set
			{
				if (_level_2 != value)
				{
					_level_2 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(level_2)));
				}
			}
		}

		public void CopyPlainToCyclic(PlcTcProberTests.fbInheritanceLevel_2 target)
		{
			base.CopyPlainToCyclic(target);
			level_2.CopyPlainToCyclic(target.level_2);
		}

		public void CopyPlainToCyclic(PlcTcProberTests.IfbInheritanceLevel_2 target)
		{
			this.CopyPlainToCyclic((PlcTcProberTests.fbInheritanceLevel_2)target);
		}

		public void CopyPlainToShadow(PlcTcProberTests.fbInheritanceLevel_2 target)
		{
			base.CopyPlainToShadow(target);
			level_2.CopyPlainToShadow(target.level_2);
		}

		public void CopyPlainToShadow(PlcTcProberTests.IShadowfbInheritanceLevel_2 target)
		{
			this.CopyPlainToShadow((PlcTcProberTests.fbInheritanceLevel_2)target);
		}

		public void CopyCyclicToPlain(PlcTcProberTests.fbInheritanceLevel_2 source)
		{
			base.CopyCyclicToPlain(source);
			level_2.CopyCyclicToPlain(source.level_2);
		}

		public void CopyCyclicToPlain(PlcTcProberTests.IfbInheritanceLevel_2 source)
		{
			this.CopyCyclicToPlain((PlcTcProberTests.fbInheritanceLevel_2)source);
		}

		public void CopyShadowToPlain(PlcTcProberTests.fbInheritanceLevel_2 source)
		{
			base.CopyShadowToPlain(source);
			level_2.CopyShadowToPlain(source.level_2);
		}

		public void CopyShadowToPlain(PlcTcProberTests.IShadowfbInheritanceLevel_2 source)
		{
			this.CopyShadowToPlain((PlcTcProberTests.fbInheritanceLevel_2)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainfbInheritanceLevel_2()
		{
			_level_2 = new PlainstAllTypes();
		}
	}
}