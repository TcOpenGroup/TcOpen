using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace PlcTcProberTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "fbInheritanceLevel_1", "PlcTcProberTests", TypeComplexityEnum.Complex)]
	public partial class fbInheritanceLevel_1 : fbRootLevelStruct, Vortex.Connector.IVortexObject, IfbInheritanceLevel_1, IShadowfbInheritanceLevel_1, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		stAllTypes _level_1;
		public stAllTypes level_1
		{
			get
			{
				return _level_1;
			}
		}

		IstAllTypes IfbInheritanceLevel_1.level_1
		{
			get
			{
				return level_1;
			}
		}

		IShadowstAllTypes IShadowfbInheritanceLevel_1.level_1
		{
			get
			{
				return level_1;
			}
		}

		public new void LazyOnlineToShadow()
		{
			base.LazyOnlineToShadow();
			level_1.LazyOnlineToShadow();
		}

		public new void LazyShadowToOnline()
		{
			base.LazyShadowToOnline();
			level_1.LazyShadowToOnline();
		}

		public new PlainfbInheritanceLevel_1 CreatePlainerType()
		{
			var cloned = new PlainfbInheritanceLevel_1();
			base.CreatePlainerType(cloned);
			cloned.level_1 = level_1.CreatePlainerType();
			return cloned;
		}

		protected PlainfbInheritanceLevel_1 CreatePlainerType(PlainfbInheritanceLevel_1 cloned)
		{
			base.CreatePlainerType(cloned);
			cloned.level_1 = level_1.CreatePlainerType();
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
		partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexConstructorParameterless();
		public void FlushPlainToOnline(PlcTcProberTests.PlainfbInheritanceLevel_1 source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(PlcTcProberTests.PlainfbInheritanceLevel_1 source)
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

		public void FlushOnlineToPlain(PlcTcProberTests.PlainfbInheritanceLevel_1 source)
		{
			this.Read();
			source.CopyCyclicToPlain(this);
		}

		public fbInheritanceLevel_1(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail): base (parent, readableTail, symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			_level_1 = new stAllTypes(this, "LEVEL 1", "level_1");
			_level_1.AttributeName = "LEVEL 1";
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
		}

		public fbInheritanceLevel_1(): base ()
		{
			PexPreConstructorParameterless();
			_level_1 = new stAllTypes();
			_level_1.AttributeName = "LEVEL 1";
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcfbInheritanceLevel_1 : PlcTcProberTests.fbRootLevelStruct.PlcfbRootLevelStruct
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcfbInheritanceLevel_1()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IfbInheritanceLevel_1 : Vortex.Connector.IVortexOnlineObject, PlcTcProberTests.IfbRootLevelStruct
	{
		IstAllTypes level_1
		{
			get;
		}

		new PlcTcProberTests.PlainfbInheritanceLevel_1 CreatePlainerType();
		new void FlushOnlineToShadow();
		void FlushPlainToOnline(PlcTcProberTests.PlainfbInheritanceLevel_1 source);
		void FlushOnlineToPlain(PlcTcProberTests.PlainfbInheritanceLevel_1 source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowfbInheritanceLevel_1 : Vortex.Connector.IVortexShadowObject, PlcTcProberTests.IShadowfbRootLevelStruct
	{
		IShadowstAllTypes level_1
		{
			get;
		}

		new PlcTcProberTests.PlainfbInheritanceLevel_1 CreatePlainerType();
		new void FlushShadowToOnline();
		void CopyPlainToShadow(PlcTcProberTests.PlainfbInheritanceLevel_1 source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainfbInheritanceLevel_1 : PlcTcProberTests.PlainfbRootLevelStruct
	{
		PlainstAllTypes _level_1;
		public PlainstAllTypes level_1
		{
			get
			{
				return _level_1;
			}

			set
			{
				if (_level_1 != value)
				{
					_level_1 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(level_1)));
				}
			}
		}

		public void CopyPlainToCyclic(PlcTcProberTests.fbInheritanceLevel_1 target)
		{
			base.CopyPlainToCyclic(target);
			level_1.CopyPlainToCyclic(target.level_1);
		}

		public void CopyPlainToCyclic(PlcTcProberTests.IfbInheritanceLevel_1 target)
		{
			this.CopyPlainToCyclic((PlcTcProberTests.fbInheritanceLevel_1)target);
		}

		public void CopyPlainToShadow(PlcTcProberTests.fbInheritanceLevel_1 target)
		{
			base.CopyPlainToShadow(target);
			level_1.CopyPlainToShadow(target.level_1);
		}

		public void CopyPlainToShadow(PlcTcProberTests.IShadowfbInheritanceLevel_1 target)
		{
			this.CopyPlainToShadow((PlcTcProberTests.fbInheritanceLevel_1)target);
		}

		public void CopyCyclicToPlain(PlcTcProberTests.fbInheritanceLevel_1 source)
		{
			base.CopyCyclicToPlain(source);
			level_1.CopyCyclicToPlain(source.level_1);
		}

		public void CopyCyclicToPlain(PlcTcProberTests.IfbInheritanceLevel_1 source)
		{
			this.CopyCyclicToPlain((PlcTcProberTests.fbInheritanceLevel_1)source);
		}

		public void CopyShadowToPlain(PlcTcProberTests.fbInheritanceLevel_1 source)
		{
			base.CopyShadowToPlain(source);
			level_1.CopyShadowToPlain(source.level_1);
		}

		public void CopyShadowToPlain(PlcTcProberTests.IShadowfbInheritanceLevel_1 source)
		{
			this.CopyShadowToPlain((PlcTcProberTests.fbInheritanceLevel_1)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainfbInheritanceLevel_1()
		{
			_level_1 = new PlainstAllTypes();
		}
	}
}