using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace PlcTcProberTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "fbRootLevelStruct", "PlcTcProberTests", TypeComplexityEnum.Complex)]
	public partial class fbRootLevelStruct : Vortex.Connector.IVortexObject, IfbRootLevelStruct, IShadowfbRootLevelStruct, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		stAllTypes _level_0;
		public stAllTypes level_0
		{
			get
			{
				return _level_0;
			}
		}

		IstAllTypes IfbRootLevelStruct.level_0
		{
			get
			{
				return level_0;
			}
		}

		IShadowstAllTypes IShadowfbRootLevelStruct.level_0
		{
			get
			{
				return level_0;
			}
		}

		public const System.Int16 __constlevel = 5;
		Vortex.Connector.ValueTypes.OnlinerInt _level;
		public Vortex.Connector.ValueTypes.OnlinerInt level
		{
			get
			{
				return _level;
			}
		}

		public void LazyOnlineToShadow()
		{
			level_0.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			level_0.LazyShadowToOnline();
		}

		public PlainfbRootLevelStruct CreatePlainerType()
		{
			var cloned = new PlainfbRootLevelStruct();
			cloned.level_0 = level_0.CreatePlainerType();
			return cloned;
		}

		protected PlainfbRootLevelStruct CreatePlainerType(PlainfbRootLevelStruct cloned)
		{
			cloned.level_0 = level_0.CreatePlainerType();
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

		public void FlushPlainToOnline(PlcTcProberTests.PlainfbRootLevelStruct source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(PlcTcProberTests.PlainfbRootLevelStruct source)
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

		public void FlushOnlineToPlain(PlcTcProberTests.PlainfbRootLevelStruct source)
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

		public fbRootLevelStruct(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			_level_0 = new stAllTypes(this, "LEVEL 0", "level_0");
			_level_0.AttributeName = "LEVEL 0";
			_level = @Connector.Online.Adapter.CreateINT(this, "", "level");
			level.MakeReadOnly();
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public fbRootLevelStruct()
		{
			PexPreConstructorParameterless();
			_level_0 = new stAllTypes();
			_level_0.AttributeName = "LEVEL 0";
			_level = Vortex.Connector.IConnectorFactory.CreateINT();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcfbRootLevelStruct
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcfbRootLevelStruct()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IfbRootLevelStruct : Vortex.Connector.IVortexOnlineObject
	{
		IstAllTypes level_0
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		PlcTcProberTests.PlainfbRootLevelStruct CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(PlcTcProberTests.PlainfbRootLevelStruct source);
		void FlushOnlineToPlain(PlcTcProberTests.PlainfbRootLevelStruct source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowfbRootLevelStruct : Vortex.Connector.IVortexShadowObject
	{
		IShadowstAllTypes level_0
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		PlcTcProberTests.PlainfbRootLevelStruct CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(PlcTcProberTests.PlainfbRootLevelStruct source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainfbRootLevelStruct : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		PlainstAllTypes _level_0;
		public PlainstAllTypes level_0
		{
			get
			{
				return _level_0;
			}

			set
			{
				if (_level_0 != value)
				{
					_level_0 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(level_0)));
				}
			}
		}

		public const System.Int16 __constlevel = 5;
		System.Int16 _level;
		public System.Int16 level
		{
			get
			{
				return _level;
			}

			set
			{
				if (_level != value)
				{
					_level = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(level)));
				}
			}
		}

		public void CopyPlainToCyclic(PlcTcProberTests.fbRootLevelStruct target)
		{
			level_0.CopyPlainToCyclic(target.level_0);
			target.level.Cyclic = level;
		}

		public void CopyPlainToCyclic(PlcTcProberTests.IfbRootLevelStruct target)
		{
			this.CopyPlainToCyclic((PlcTcProberTests.fbRootLevelStruct)target);
		}

		public void CopyPlainToShadow(PlcTcProberTests.fbRootLevelStruct target)
		{
			level_0.CopyPlainToShadow(target.level_0);
			target.level.Shadow = level;
		}

		public void CopyPlainToShadow(PlcTcProberTests.IShadowfbRootLevelStruct target)
		{
			this.CopyPlainToShadow((PlcTcProberTests.fbRootLevelStruct)target);
		}

		public void CopyCyclicToPlain(PlcTcProberTests.fbRootLevelStruct source)
		{
			level_0.CopyCyclicToPlain(source.level_0);
			level = source.level.LastValue;
		}

		public void CopyCyclicToPlain(PlcTcProberTests.IfbRootLevelStruct source)
		{
			this.CopyCyclicToPlain((PlcTcProberTests.fbRootLevelStruct)source);
		}

		public void CopyShadowToPlain(PlcTcProberTests.fbRootLevelStruct source)
		{
			level_0.CopyShadowToPlain(source.level_0);
			level = source.level.Shadow;
		}

		public void CopyShadowToPlain(PlcTcProberTests.IShadowfbRootLevelStruct source)
		{
			this.CopyShadowToPlain((PlcTcProberTests.fbRootLevelStruct)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainfbRootLevelStruct()
		{
			_level_0 = new PlainstAllTypes();
		}
	}
}