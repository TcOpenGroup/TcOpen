using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace PlcTcProberTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "fbInheritanceLevel_5", "PlcTcProberTests", TypeComplexityEnum.Complex)]
	public partial class fbInheritanceLevel_5 : fbInheritanceLevel_4, Vortex.Connector.IVortexObject, IfbInheritanceLevel_5, IShadowfbInheritanceLevel_5, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
	{
		stAllTypes _level_5;
		public stAllTypes level_5
		{
			get
			{
				return _level_5;
			}
		}

		IstAllTypes IfbInheritanceLevel_5.level_5
		{
			get
			{
				return level_5;
			}
		}

		IShadowstAllTypes IShadowfbInheritanceLevel_5.level_5
		{
			get
			{
				return level_5;
			}
		}

		public new void LazyOnlineToShadow()
		{
			base.LazyOnlineToShadow();
			level_5.LazyOnlineToShadow();
		}

		public new void LazyShadowToOnline()
		{
			base.LazyShadowToOnline();
			level_5.LazyShadowToOnline();
		}

		public new PlainfbInheritanceLevel_5 CreatePlainerType()
		{
			var cloned = new PlainfbInheritanceLevel_5();
			base.CreatePlainerType(cloned);
			cloned.level_5 = level_5.CreatePlainerType();
			return cloned;
		}

		protected PlainfbInheritanceLevel_5 CreatePlainerType(PlainfbInheritanceLevel_5 cloned)
		{
			base.CreatePlainerType(cloned);
			cloned.level_5 = level_5.CreatePlainerType();
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
		partial void PexConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexConstructorParameterless();
		public void FlushPlainToOnline(PlcTcProberTests.PlainfbInheritanceLevel_5 source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(PlcTcProberTests.PlainfbInheritanceLevel_5 source)
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

		public void FlushOnlineToPlain(PlcTcProberTests.PlainfbInheritanceLevel_5 source)
		{
			this.Read();
			source.CopyCyclicToPlain(this);
		}

		public fbInheritanceLevel_5(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail): base (parent, readableTail, symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			_level_5 = new stAllTypes(this, "LEVEL 5", "level_5");
			_level_5.AttributeName = "LEVEL 5";
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
		}

		public fbInheritanceLevel_5(): base ()
		{
			PexPreConstructorParameterless();
			_level_5 = new stAllTypes();
			_level_5.AttributeName = "LEVEL 5";
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcfbInheritanceLevel_5 : PlcTcProberTests.fbInheritanceLevel_4.PlcfbInheritanceLevel_4
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcfbInheritanceLevel_5()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IfbInheritanceLevel_5 : Vortex.Connector.IVortexOnlineObject, PlcTcProberTests.IfbInheritanceLevel_4
	{
		IstAllTypes level_5
		{
			get;
		}

		new PlcTcProberTests.PlainfbInheritanceLevel_5 CreatePlainerType();
		new void FlushOnlineToShadow();
		void FlushPlainToOnline(PlcTcProberTests.PlainfbInheritanceLevel_5 source);
		void FlushOnlineToPlain(PlcTcProberTests.PlainfbInheritanceLevel_5 source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowfbInheritanceLevel_5 : Vortex.Connector.IVortexShadowObject, PlcTcProberTests.IShadowfbInheritanceLevel_4
	{
		IShadowstAllTypes level_5
		{
			get;
		}

		new PlcTcProberTests.PlainfbInheritanceLevel_5 CreatePlainerType();
		new void FlushShadowToOnline();
		void CopyPlainToShadow(PlcTcProberTests.PlainfbInheritanceLevel_5 source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainfbInheritanceLevel_5 : PlcTcProberTests.PlainfbInheritanceLevel_4
	{
		PlainstAllTypes _level_5;
		public PlainstAllTypes level_5
		{
			get
			{
				return _level_5;
			}

			set
			{
				if (_level_5 != value)
				{
					_level_5 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(level_5)));
				}
			}
		}

		public void CopyPlainToCyclic(PlcTcProberTests.fbInheritanceLevel_5 target)
		{
			base.CopyPlainToCyclic(target);
			level_5.CopyPlainToCyclic(target.level_5);
		}

		public void CopyPlainToCyclic(PlcTcProberTests.IfbInheritanceLevel_5 target)
		{
			this.CopyPlainToCyclic((PlcTcProberTests.fbInheritanceLevel_5)target);
		}

		public void CopyPlainToShadow(PlcTcProberTests.fbInheritanceLevel_5 target)
		{
			base.CopyPlainToShadow(target);
			level_5.CopyPlainToShadow(target.level_5);
		}

		public void CopyPlainToShadow(PlcTcProberTests.IShadowfbInheritanceLevel_5 target)
		{
			this.CopyPlainToShadow((PlcTcProberTests.fbInheritanceLevel_5)target);
		}

		public void CopyCyclicToPlain(PlcTcProberTests.fbInheritanceLevel_5 source)
		{
			base.CopyCyclicToPlain(source);
			level_5.CopyCyclicToPlain(source.level_5);
		}

		public void CopyCyclicToPlain(PlcTcProberTests.IfbInheritanceLevel_5 source)
		{
			this.CopyCyclicToPlain((PlcTcProberTests.fbInheritanceLevel_5)source);
		}

		public void CopyShadowToPlain(PlcTcProberTests.fbInheritanceLevel_5 source)
		{
			base.CopyShadowToPlain(source);
			level_5.CopyShadowToPlain(source.level_5);
		}

		public void CopyShadowToPlain(PlcTcProberTests.IShadowfbInheritanceLevel_5 source)
		{
			this.CopyShadowToPlain((PlcTcProberTests.fbInheritanceLevel_5)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainfbInheritanceLevel_5()
		{
			_level_5 = new PlainstAllTypes();
		}
	}
}