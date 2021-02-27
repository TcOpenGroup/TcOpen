using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace PlcTcProberTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "fbBasicRunnerTests", "PlcTcProberTests", TypeComplexityEnum.Complex)]
	public partial class fbBasicRunnerTests : Vortex.Connector.IVortexObject, IfbBasicRunnerTests, IShadowfbBasicRunnerTests, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		Vortex.Connector.ValueTypes.OnlinerUInt _runs;
		public Vortex.Connector.ValueTypes.OnlinerUInt runs
		{
			get
			{
				return _runs;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineUInt IfbBasicRunnerTests.runs
		{
			get
			{
				return runs;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowUInt IShadowfbBasicRunnerTests.runs
		{
			get
			{
				return runs;
			}
		}

		public void LazyOnlineToShadow()
		{
			runs.Shadow = runs.LastValue;
		}

		public void LazyShadowToOnline()
		{
			runs.Cyclic = runs.Shadow;
		}

		public PlainfbBasicRunnerTests CreatePlainerType()
		{
			var cloned = new PlainfbBasicRunnerTests();
			return cloned;
		}

		protected PlainfbBasicRunnerTests CreatePlainerType(PlainfbBasicRunnerTests cloned)
		{
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

		public void FlushPlainToOnline(PlcTcProberTests.PlainfbBasicRunnerTests source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(PlcTcProberTests.PlainfbBasicRunnerTests source)
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

		public void FlushOnlineToPlain(PlcTcProberTests.PlainfbBasicRunnerTests source)
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

		public fbBasicRunnerTests(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			_runs = @Connector.Online.Adapter.CreateUINT(this, "", "runs");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public fbBasicRunnerTests()
		{
			PexPreConstructorParameterless();
			_runs = Vortex.Connector.IConnectorFactory.CreateUINT();
			AttributeName = "";
			PexConstructorParameterless();
		}

		public System.UInt16 ResetCounter()
		{
			return (System.UInt16)Connector.InvokeRpc(this.Symbol, "ResetCounter", new object[]{});
		}

		public System.UInt16 RunCount()
		{
			return (System.UInt16)Connector.InvokeRpc(this.Symbol, "RunCount", new object[]{});
		}

		public System.Boolean RunUntilReturnsTrue(System.Boolean _m)
		{
			return (System.Boolean)Connector.InvokeRpc(this.Symbol, "RunUntilReturnsTrue", new object[]{_m});
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcfbBasicRunnerTests
		{
			public object runs;
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcfbBasicRunnerTests()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IfbBasicRunnerTests : Vortex.Connector.IVortexOnlineObject
	{
		Vortex.Connector.ValueTypes.Online.IOnlineUInt runs
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		PlcTcProberTests.PlainfbBasicRunnerTests CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(PlcTcProberTests.PlainfbBasicRunnerTests source);
		void FlushOnlineToPlain(PlcTcProberTests.PlainfbBasicRunnerTests source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowfbBasicRunnerTests : Vortex.Connector.IVortexShadowObject
	{
		Vortex.Connector.ValueTypes.Shadows.IShadowUInt runs
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		PlcTcProberTests.PlainfbBasicRunnerTests CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(PlcTcProberTests.PlainfbBasicRunnerTests source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainfbBasicRunnerTests : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		System.UInt16 _runs;
		public System.UInt16 runs
		{
			get
			{
				return _runs;
			}

			set
			{
				if (_runs != value)
				{
					_runs = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(runs)));
				}
			}
		}

		public void CopyPlainToCyclic(PlcTcProberTests.fbBasicRunnerTests target)
		{
			target.runs.Cyclic = runs;
		}

		public void CopyPlainToCyclic(PlcTcProberTests.IfbBasicRunnerTests target)
		{
			this.CopyPlainToCyclic((PlcTcProberTests.fbBasicRunnerTests)target);
		}

		public void CopyPlainToShadow(PlcTcProberTests.fbBasicRunnerTests target)
		{
			target.runs.Shadow = runs;
		}

		public void CopyPlainToShadow(PlcTcProberTests.IShadowfbBasicRunnerTests target)
		{
			this.CopyPlainToShadow((PlcTcProberTests.fbBasicRunnerTests)target);
		}

		public void CopyCyclicToPlain(PlcTcProberTests.fbBasicRunnerTests source)
		{
			runs = source.runs.LastValue;
		}

		public void CopyCyclicToPlain(PlcTcProberTests.IfbBasicRunnerTests source)
		{
			this.CopyCyclicToPlain((PlcTcProberTests.fbBasicRunnerTests)source);
		}

		public void CopyShadowToPlain(PlcTcProberTests.fbBasicRunnerTests source)
		{
			runs = source.runs.Shadow;
		}

		public void CopyShadowToPlain(PlcTcProberTests.IShadowfbBasicRunnerTests source)
		{
			this.CopyShadowToPlain((PlcTcProberTests.fbBasicRunnerTests)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainfbBasicRunnerTests()
		{
		}
	}
}