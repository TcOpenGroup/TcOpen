using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace PlcTcProberTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "stRecorder", "PlcTcProberTests", TypeComplexityEnum.Complex)]
	public partial class stRecorder : Vortex.Connector.IVortexObject, IstRecorder, IShadowstRecorder, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		Vortex.Connector.ValueTypes.OnlinerInt _counter;
		public Vortex.Connector.ValueTypes.OnlinerInt counter
		{
			get
			{
				return _counter;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineInt IstRecorder.counter
		{
			get
			{
				return counter;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadowstRecorder.counter
		{
			get
			{
				return counter;
			}
		}

		public void LazyOnlineToShadow()
		{
			counter.Shadow = counter.LastValue;
		}

		public void LazyShadowToOnline()
		{
			counter.Cyclic = counter.Shadow;
		}

		public PlainstRecorder CreatePlainerType()
		{
			var cloned = new PlainstRecorder();
			return cloned;
		}

		protected PlainstRecorder CreatePlainerType(PlainstRecorder cloned)
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

		public void FlushPlainToOnline(PlcTcProberTests.PlainstRecorder source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(PlcTcProberTests.PlainstRecorder source)
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

		public void FlushOnlineToPlain(PlcTcProberTests.PlainstRecorder source)
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

		public stRecorder(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			_counter = @Connector.Online.Adapter.CreateINT(this, "", "counter");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public stRecorder()
		{
			PexPreConstructorParameterless();
			_counter = Vortex.Connector.IConnectorFactory.CreateINT();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcstRecorder
		{
			public object counter;
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcstRecorder()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IstRecorder : Vortex.Connector.IVortexOnlineObject
	{
		Vortex.Connector.ValueTypes.Online.IOnlineInt counter
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		PlcTcProberTests.PlainstRecorder CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(PlcTcProberTests.PlainstRecorder source);
		void FlushOnlineToPlain(PlcTcProberTests.PlainstRecorder source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowstRecorder : Vortex.Connector.IVortexShadowObject
	{
		Vortex.Connector.ValueTypes.Shadows.IShadowInt counter
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		PlcTcProberTests.PlainstRecorder CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(PlcTcProberTests.PlainstRecorder source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainstRecorder : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		System.Int16 _counter;
		public System.Int16 counter
		{
			get
			{
				return _counter;
			}

			set
			{
				if (_counter != value)
				{
					_counter = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(counter)));
				}
			}
		}

		public void CopyPlainToCyclic(PlcTcProberTests.stRecorder target)
		{
			target.counter.Cyclic = counter;
		}

		public void CopyPlainToCyclic(PlcTcProberTests.IstRecorder target)
		{
			this.CopyPlainToCyclic((PlcTcProberTests.stRecorder)target);
		}

		public void CopyPlainToShadow(PlcTcProberTests.stRecorder target)
		{
			target.counter.Shadow = counter;
		}

		public void CopyPlainToShadow(PlcTcProberTests.IShadowstRecorder target)
		{
			this.CopyPlainToShadow((PlcTcProberTests.stRecorder)target);
		}

		public void CopyCyclicToPlain(PlcTcProberTests.stRecorder source)
		{
			counter = source.counter.LastValue;
		}

		public void CopyCyclicToPlain(PlcTcProberTests.IstRecorder source)
		{
			this.CopyCyclicToPlain((PlcTcProberTests.stRecorder)source);
		}

		public void CopyShadowToPlain(PlcTcProberTests.stRecorder source)
		{
			counter = source.counter.Shadow;
		}

		public void CopyShadowToPlain(PlcTcProberTests.IShadowstRecorder source)
		{
			this.CopyShadowToPlain((PlcTcProberTests.stRecorder)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainstRecorder()
		{
		}
	}
}