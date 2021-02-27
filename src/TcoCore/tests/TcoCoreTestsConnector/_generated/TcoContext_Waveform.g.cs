using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoContext_Waveform", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class TcoContext_Waveform : Vortex.Connector.IVortexObject, ITcoContext_Waveform, IShadowTcoContext_Waveform, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
				return TcoCoreTestsTwinController.Translator.Translate(_humanReadable).Interpolate(this);
			}

			protected set
			{
				_humanReadable = value;
			}
		}

		protected string _humanReadable;
		TcoObject_Waveform __TcoObject_Waveform;
		public TcoObject_Waveform _TcoObject_Waveform
		{
			get
			{
				return __TcoObject_Waveform;
			}
		}

		ITcoObject_Waveform ITcoContext_Waveform._TcoObject_Waveform
		{
			get
			{
				return _TcoObject_Waveform;
			}
		}

		IShadowTcoObject_Waveform IShadowTcoContext_Waveform._TcoObject_Waveform
		{
			get
			{
				return _TcoObject_Waveform;
			}
		}

		public void LazyOnlineToShadow()
		{
			_TcoObject_Waveform.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			_TcoObject_Waveform.LazyShadowToOnline();
		}

		public PlainTcoContext_Waveform CreatePlainerType()
		{
			var cloned = new PlainTcoContext_Waveform();
			cloned._TcoObject_Waveform = _TcoObject_Waveform.CreatePlainerType();
			return cloned;
		}

		protected PlainTcoContext_Waveform CreatePlainerType(PlainTcoContext_Waveform cloned)
		{
			cloned._TcoObject_Waveform = _TcoObject_Waveform.CreatePlainerType();
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

		public void FlushPlainToOnline(TcoCoreTests.PlainTcoContext_Waveform source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainTcoContext_Waveform source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainTcoContext_Waveform source)
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
				return TcoCoreTestsTwinController.Translator.Translate(_AttributeName).Interpolate(this);
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

		public TcoContext_Waveform(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			__TcoObject_Waveform = new TcoObject_Waveform(this, "", "_TcoObject_Waveform");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public TcoContext_Waveform()
		{
			PexPreConstructorParameterless();
			__TcoObject_Waveform = new TcoObject_Waveform();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoContext_Waveform
		{
			public PlainTcoObject_Waveform _TcoObject_Waveform;
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoContext_Waveform()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoContext_Waveform : Vortex.Connector.IVortexOnlineObject
	{
		ITcoObject_Waveform _TcoObject_Waveform
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoContext_Waveform CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainTcoContext_Waveform source);
		void FlushOnlineToPlain(TcoCoreTests.PlainTcoContext_Waveform source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoContext_Waveform : Vortex.Connector.IVortexShadowObject
	{
		IShadowTcoObject_Waveform _TcoObject_Waveform
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoContext_Waveform CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainTcoContext_Waveform source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoContext_Waveform : Vortex.Connector.IPlain
	{
		PlainTcoObject_Waveform __TcoObject_Waveform;
		public PlainTcoObject_Waveform _TcoObject_Waveform
		{
			get
			{
				return __TcoObject_Waveform;
			}

			set
			{
				if (__TcoObject_Waveform != value)
				{
					__TcoObject_Waveform = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_TcoObject_Waveform)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCoreTests.TcoContext_Waveform target)
		{
			_TcoObject_Waveform.CopyPlainToCyclic(target._TcoObject_Waveform);
		}

		public void CopyPlainToCyclic(TcoCoreTests.ITcoContext_Waveform target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.TcoContext_Waveform)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.TcoContext_Waveform target)
		{
			_TcoObject_Waveform.CopyPlainToShadow(target._TcoObject_Waveform);
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowTcoContext_Waveform target)
		{
			this.CopyPlainToShadow((TcoCoreTests.TcoContext_Waveform)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.TcoContext_Waveform source)
		{
			_TcoObject_Waveform.CopyCyclicToPlain(source._TcoObject_Waveform);
		}

		public void CopyCyclicToPlain(TcoCoreTests.ITcoContext_Waveform source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.TcoContext_Waveform)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.TcoContext_Waveform source)
		{
			_TcoObject_Waveform.CopyShadowToPlain(source._TcoObject_Waveform);
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowTcoContext_Waveform source)
		{
			this.CopyShadowToPlain((TcoCoreTests.TcoContext_Waveform)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoContext_Waveform()
		{
			__TcoObject_Waveform = new PlainTcoObject_Waveform();
		}
	}
}