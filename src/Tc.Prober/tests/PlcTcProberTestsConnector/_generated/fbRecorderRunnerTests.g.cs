using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace PlcTcProberTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "fbRecorderRunnerTests", "PlcTcProberTests", TypeComplexityEnum.Complex)]
	public partial class fbRecorderRunnerTests : Vortex.Connector.IVortexObject, IfbRecorderRunnerTests, IShadowfbRecorderRunnerTests, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		stRecorder __recorder;
		public stRecorder _recorder
		{
			get
			{
				return __recorder;
			}
		}

		IstRecorder IfbRecorderRunnerTests._recorder
		{
			get
			{
				return _recorder;
			}
		}

		IShadowstRecorder IShadowfbRecorderRunnerTests._recorder
		{
			get
			{
				return _recorder;
			}
		}

		public void LazyOnlineToShadow()
		{
			_recorder.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			_recorder.LazyShadowToOnline();
		}

		public PlainfbRecorderRunnerTests CreatePlainerType()
		{
			var cloned = new PlainfbRecorderRunnerTests();
			cloned._recorder = _recorder.CreatePlainerType();
			return cloned;
		}

		protected PlainfbRecorderRunnerTests CreatePlainerType(PlainfbRecorderRunnerTests cloned)
		{
			cloned._recorder = _recorder.CreatePlainerType();
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

		public void FlushPlainToOnline(PlcTcProberTests.PlainfbRecorderRunnerTests source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(PlcTcProberTests.PlainfbRecorderRunnerTests source)
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

		public void FlushOnlineToPlain(PlcTcProberTests.PlainfbRecorderRunnerTests source)
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

		public fbRecorderRunnerTests(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			__recorder = new stRecorder(this, "", "_recorder");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public fbRecorderRunnerTests()
		{
			PexPreConstructorParameterless();
			__recorder = new stRecorder();
			AttributeName = "";
			PexConstructorParameterless();
		}

		public System.UInt16 ResetCounter()
		{
			return (System.UInt16)Connector.InvokeRpc(this.Symbol, "ResetCounter", new object[]{});
		}

		public System.Int16 RunWithRecorder()
		{
			return (System.Int16)Connector.InvokeRpc(this.Symbol, "RunWithRecorder", new object[]{});
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcfbRecorderRunnerTests
		{
			public PlainstRecorder _recorder;
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcfbRecorderRunnerTests()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IfbRecorderRunnerTests : Vortex.Connector.IVortexOnlineObject
	{
		IstRecorder _recorder
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		PlcTcProberTests.PlainfbRecorderRunnerTests CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(PlcTcProberTests.PlainfbRecorderRunnerTests source);
		void FlushOnlineToPlain(PlcTcProberTests.PlainfbRecorderRunnerTests source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowfbRecorderRunnerTests : Vortex.Connector.IVortexShadowObject
	{
		IShadowstRecorder _recorder
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		PlcTcProberTests.PlainfbRecorderRunnerTests CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(PlcTcProberTests.PlainfbRecorderRunnerTests source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainfbRecorderRunnerTests : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		PlainstRecorder __recorder;
		public PlainstRecorder _recorder
		{
			get
			{
				return __recorder;
			}

			set
			{
				if (__recorder != value)
				{
					__recorder = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_recorder)));
				}
			}
		}

		public void CopyPlainToCyclic(PlcTcProberTests.fbRecorderRunnerTests target)
		{
			_recorder.CopyPlainToCyclic(target._recorder);
		}

		public void CopyPlainToCyclic(PlcTcProberTests.IfbRecorderRunnerTests target)
		{
			this.CopyPlainToCyclic((PlcTcProberTests.fbRecorderRunnerTests)target);
		}

		public void CopyPlainToShadow(PlcTcProberTests.fbRecorderRunnerTests target)
		{
			_recorder.CopyPlainToShadow(target._recorder);
		}

		public void CopyPlainToShadow(PlcTcProberTests.IShadowfbRecorderRunnerTests target)
		{
			this.CopyPlainToShadow((PlcTcProberTests.fbRecorderRunnerTests)target);
		}

		public void CopyCyclicToPlain(PlcTcProberTests.fbRecorderRunnerTests source)
		{
			_recorder.CopyCyclicToPlain(source._recorder);
		}

		public void CopyCyclicToPlain(PlcTcProberTests.IfbRecorderRunnerTests source)
		{
			this.CopyCyclicToPlain((PlcTcProberTests.fbRecorderRunnerTests)source);
		}

		public void CopyShadowToPlain(PlcTcProberTests.fbRecorderRunnerTests source)
		{
			_recorder.CopyShadowToPlain(source._recorder);
		}

		public void CopyShadowToPlain(PlcTcProberTests.IShadowfbRecorderRunnerTests source)
		{
			this.CopyShadowToPlain((PlcTcProberTests.fbRecorderRunnerTests)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainfbRecorderRunnerTests()
		{
			__recorder = new PlainstRecorder();
		}
	}
}