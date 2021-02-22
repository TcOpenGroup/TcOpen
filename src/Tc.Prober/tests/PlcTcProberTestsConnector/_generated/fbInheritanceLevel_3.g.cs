using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace PlcTcProberTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "fbInheritanceLevel_3", "PlcTcProberTests", TypeComplexityEnum.Complex)]
	public partial class fbInheritanceLevel_3 : fbInheritanceLevel_2, Vortex.Connector.IVortexObject, IfbInheritanceLevel_3, IShadowfbInheritanceLevel_3, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		stAllTypes _level_3;
		public stAllTypes level_3
		{
			get
			{
				return _level_3;
			}
		}

		IstAllTypes IfbInheritanceLevel_3.level_3
		{
			get
			{
				return level_3;
			}
		}

		IShadowstAllTypes IShadowfbInheritanceLevel_3.level_3
		{
			get
			{
				return level_3;
			}
		}

		public new void LazyOnlineToShadow()
		{
			base.LazyOnlineToShadow();
			level_3.LazyOnlineToShadow();
		}

		public new void LazyShadowToOnline()
		{
			base.LazyShadowToOnline();
			level_3.LazyShadowToOnline();
		}

		public new PlainfbInheritanceLevel_3 CreatePlainerType()
		{
			var cloned = new PlainfbInheritanceLevel_3();
			base.CreatePlainerType(cloned);
			cloned.level_3 = level_3.CreatePlainerType();
			return cloned;
		}

		protected PlainfbInheritanceLevel_3 CreatePlainerType(PlainfbInheritanceLevel_3 cloned)
		{
			base.CreatePlainerType(cloned);
			cloned.level_3 = level_3.CreatePlainerType();
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
		partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexConstructorParameterless();
		public void FlushPlainToOnline(PlcTcProberTests.PlainfbInheritanceLevel_3 source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(PlcTcProberTests.PlainfbInheritanceLevel_3 source)
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

		public void FlushOnlineToPlain(PlcTcProberTests.PlainfbInheritanceLevel_3 source)
		{
			this.Read();
			source.CopyCyclicToPlain(this);
		}

		public fbInheritanceLevel_3(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail): base (parent, readableTail, symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			_level_3 = new stAllTypes(this, "LEVEL 3", "level_3");
			_level_3.AttributeName = "LEVEL 3";
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
		}

		public fbInheritanceLevel_3(): base ()
		{
			PexPreConstructorParameterless();
			_level_3 = new stAllTypes();
			_level_3.AttributeName = "LEVEL 3";
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcfbInheritanceLevel_3 : PlcTcProberTests.fbInheritanceLevel_2.PlcfbInheritanceLevel_2
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcfbInheritanceLevel_3()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IfbInheritanceLevel_3 : Vortex.Connector.IVortexOnlineObject, PlcTcProberTests.IfbInheritanceLevel_2
	{
		IstAllTypes level_3
		{
			get;
		}

		new PlcTcProberTests.PlainfbInheritanceLevel_3 CreatePlainerType();
		new void FlushOnlineToShadow();
		void FlushPlainToOnline(PlcTcProberTests.PlainfbInheritanceLevel_3 source);
		void FlushOnlineToPlain(PlcTcProberTests.PlainfbInheritanceLevel_3 source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowfbInheritanceLevel_3 : Vortex.Connector.IVortexShadowObject, PlcTcProberTests.IShadowfbInheritanceLevel_2
	{
		IShadowstAllTypes level_3
		{
			get;
		}

		new PlcTcProberTests.PlainfbInheritanceLevel_3 CreatePlainerType();
		new void FlushShadowToOnline();
		void CopyPlainToShadow(PlcTcProberTests.PlainfbInheritanceLevel_3 source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainfbInheritanceLevel_3 : PlcTcProberTests.PlainfbInheritanceLevel_2
	{
		PlainstAllTypes _level_3;
		public PlainstAllTypes level_3
		{
			get
			{
				return _level_3;
			}

			set
			{
				if (_level_3 != value)
				{
					_level_3 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(level_3)));
				}
			}
		}

		public void CopyPlainToCyclic(PlcTcProberTests.fbInheritanceLevel_3 target)
		{
			base.CopyPlainToCyclic(target);
			level_3.CopyPlainToCyclic(target.level_3);
		}

		public void CopyPlainToCyclic(PlcTcProberTests.IfbInheritanceLevel_3 target)
		{
			this.CopyPlainToCyclic((PlcTcProberTests.fbInheritanceLevel_3)target);
		}

		public void CopyPlainToShadow(PlcTcProberTests.fbInheritanceLevel_3 target)
		{
			base.CopyPlainToShadow(target);
			level_3.CopyPlainToShadow(target.level_3);
		}

		public void CopyPlainToShadow(PlcTcProberTests.IShadowfbInheritanceLevel_3 target)
		{
			this.CopyPlainToShadow((PlcTcProberTests.fbInheritanceLevel_3)target);
		}

		public void CopyCyclicToPlain(PlcTcProberTests.fbInheritanceLevel_3 source)
		{
			base.CopyCyclicToPlain(source);
			level_3.CopyCyclicToPlain(source.level_3);
		}

		public void CopyCyclicToPlain(PlcTcProberTests.IfbInheritanceLevel_3 source)
		{
			this.CopyCyclicToPlain((PlcTcProberTests.fbInheritanceLevel_3)source);
		}

		public void CopyShadowToPlain(PlcTcProberTests.fbInheritanceLevel_3 source)
		{
			base.CopyShadowToPlain(source);
			level_3.CopyShadowToPlain(source.level_3);
		}

		public void CopyShadowToPlain(PlcTcProberTests.IShadowfbInheritanceLevel_3 source)
		{
			this.CopyShadowToPlain((PlcTcProberTests.fbInheritanceLevel_3)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainfbInheritanceLevel_3()
		{
			_level_3 = new PlainstAllTypes();
		}
	}
}