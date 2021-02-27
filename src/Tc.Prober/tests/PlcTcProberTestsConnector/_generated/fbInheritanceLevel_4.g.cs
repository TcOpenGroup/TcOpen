using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace PlcTcProberTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "fbInheritanceLevel_4", "PlcTcProberTests", TypeComplexityEnum.Complex)]
	public partial class fbInheritanceLevel_4 : fbInheritanceLevel_3, Vortex.Connector.IVortexObject, IfbInheritanceLevel_4, IShadowfbInheritanceLevel_4, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		stAllTypes _level_4;
		public stAllTypes level_4
		{
			get
			{
				return _level_4;
			}
		}

		IstAllTypes IfbInheritanceLevel_4.level_4
		{
			get
			{
				return level_4;
			}
		}

		IShadowstAllTypes IShadowfbInheritanceLevel_4.level_4
		{
			get
			{
				return level_4;
			}
		}

		public new void LazyOnlineToShadow()
		{
			base.LazyOnlineToShadow();
			level_4.LazyOnlineToShadow();
		}

		public new void LazyShadowToOnline()
		{
			base.LazyShadowToOnline();
			level_4.LazyShadowToOnline();
		}

		public new PlainfbInheritanceLevel_4 CreatePlainerType()
		{
			var cloned = new PlainfbInheritanceLevel_4();
			base.CreatePlainerType(cloned);
			cloned.level_4 = level_4.CreatePlainerType();
			return cloned;
		}

		protected PlainfbInheritanceLevel_4 CreatePlainerType(PlainfbInheritanceLevel_4 cloned)
		{
			base.CreatePlainerType(cloned);
			cloned.level_4 = level_4.CreatePlainerType();
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
		partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexConstructorParameterless();
		public void FlushPlainToOnline(PlcTcProberTests.PlainfbInheritanceLevel_4 source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(PlcTcProberTests.PlainfbInheritanceLevel_4 source)
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

		public void FlushOnlineToPlain(PlcTcProberTests.PlainfbInheritanceLevel_4 source)
		{
			this.Read();
			source.CopyCyclicToPlain(this);
		}

		public fbInheritanceLevel_4(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail): base (parent, readableTail, symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			_level_4 = new stAllTypes(this, "LEVEL 4", "level_4");
			_level_4.AttributeName = "LEVEL 4";
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
		}

		public fbInheritanceLevel_4(): base ()
		{
			PexPreConstructorParameterless();
			_level_4 = new stAllTypes();
			_level_4.AttributeName = "LEVEL 4";
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcfbInheritanceLevel_4 : PlcTcProberTests.fbInheritanceLevel_3.PlcfbInheritanceLevel_3
		{
			public PlainstAllTypes level_4;
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcfbInheritanceLevel_4()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IfbInheritanceLevel_4 : Vortex.Connector.IVortexOnlineObject, PlcTcProberTests.IfbInheritanceLevel_3
	{
		IstAllTypes level_4
		{
			get;
		}

		new PlcTcProberTests.PlainfbInheritanceLevel_4 CreatePlainerType();
		new void FlushOnlineToShadow();
		void FlushPlainToOnline(PlcTcProberTests.PlainfbInheritanceLevel_4 source);
		void FlushOnlineToPlain(PlcTcProberTests.PlainfbInheritanceLevel_4 source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowfbInheritanceLevel_4 : Vortex.Connector.IVortexShadowObject, PlcTcProberTests.IShadowfbInheritanceLevel_3
	{
		IShadowstAllTypes level_4
		{
			get;
		}

		new PlcTcProberTests.PlainfbInheritanceLevel_4 CreatePlainerType();
		new void FlushShadowToOnline();
		void CopyPlainToShadow(PlcTcProberTests.PlainfbInheritanceLevel_4 source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainfbInheritanceLevel_4 : PlcTcProberTests.PlainfbInheritanceLevel_3
	{
		PlainstAllTypes _level_4;
		public PlainstAllTypes level_4
		{
			get
			{
				return _level_4;
			}

			set
			{
				if (_level_4 != value)
				{
					_level_4 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(level_4)));
				}
			}
		}

		public void CopyPlainToCyclic(PlcTcProberTests.fbInheritanceLevel_4 target)
		{
			base.CopyPlainToCyclic(target);
			level_4.CopyPlainToCyclic(target.level_4);
		}

		public void CopyPlainToCyclic(PlcTcProberTests.IfbInheritanceLevel_4 target)
		{
			this.CopyPlainToCyclic((PlcTcProberTests.fbInheritanceLevel_4)target);
		}

		public void CopyPlainToShadow(PlcTcProberTests.fbInheritanceLevel_4 target)
		{
			base.CopyPlainToShadow(target);
			level_4.CopyPlainToShadow(target.level_4);
		}

		public void CopyPlainToShadow(PlcTcProberTests.IShadowfbInheritanceLevel_4 target)
		{
			this.CopyPlainToShadow((PlcTcProberTests.fbInheritanceLevel_4)target);
		}

		public void CopyCyclicToPlain(PlcTcProberTests.fbInheritanceLevel_4 source)
		{
			base.CopyCyclicToPlain(source);
			level_4.CopyCyclicToPlain(source.level_4);
		}

		public void CopyCyclicToPlain(PlcTcProberTests.IfbInheritanceLevel_4 source)
		{
			this.CopyCyclicToPlain((PlcTcProberTests.fbInheritanceLevel_4)source);
		}

		public void CopyShadowToPlain(PlcTcProberTests.fbInheritanceLevel_4 source)
		{
			base.CopyShadowToPlain(source);
			level_4.CopyShadowToPlain(source.level_4);
		}

		public void CopyShadowToPlain(PlcTcProberTests.IShadowfbInheritanceLevel_4 source)
		{
			this.CopyShadowToPlain((PlcTcProberTests.fbInheritanceLevel_4)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainfbInheritanceLevel_4()
		{
			_level_4 = new PlainstAllTypes();
		}
	}
}