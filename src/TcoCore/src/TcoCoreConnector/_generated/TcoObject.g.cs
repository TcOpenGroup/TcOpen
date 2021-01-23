using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoObject", "TcoCore", TypeComplexityEnum.Complex)]
	public partial class TcoObject : Vortex.Connector.IVortexObject, ITcoObject, IShadowTcoObject, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
				return TcoCoreTwinController.Translator.Translate(_humanReadable).Interpolate(this);
			}

			protected set
			{
				_humanReadable = value;
			}
		}

		protected string _humanReadable;
		Vortex.Connector.ValueTypes.OnlinerULInt __Identity;
		[RenderIgnore(), ReadOnly()]
		public Vortex.Connector.ValueTypes.OnlinerULInt _Identity
		{
			get
			{
				return __Identity;
			}
		}

		[RenderIgnore(), ReadOnly()]
		Vortex.Connector.ValueTypes.Online.IOnlineULInt ITcoObject._Identity
		{
			get
			{
				return _Identity;
			}
		}

		[RenderIgnore(), ReadOnly()]
		Vortex.Connector.ValueTypes.Shadows.IShadowULInt IShadowTcoObject._Identity
		{
			get
			{
				return _Identity;
			}
		}

		public void LazyOnlineToShadow()
		{
			_Identity.Shadow = _Identity.LastValue;
		}

		public void LazyShadowToOnline()
		{
			_Identity.Cyclic = _Identity.Shadow;
		}

		public PlainTcoObject CreatePlainerType()
		{
			var cloned = new PlainTcoObject();
			return cloned;
		}

		protected PlainTcoObject CreatePlainerType(PlainTcoObject cloned)
		{
			return cloned;
		}

		partial void PexPreConstructor(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail);
		partial void PexPreConstructorParameterless();
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

		public void FlushPlainToOnline(TcoCore.PlainTcoObject source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCore.PlainTcoObject source)
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

		public void FlushOnlineToPlain(TcoCore.PlainTcoObject source)
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
				return TcoCoreTwinController.Translator.Translate(_AttributeName).Interpolate(this);
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

		public TcoObject(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@ValueTags = new System.Collections.Generic.List<Vortex.Connector.IValueTag>();
			this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			__Identity = @Connector.Online.Adapter.CreateULINT(this, "", "_Identity");
			_Identity.MakeReadOnly();
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
			parent.AddChild(this);
		}

		public TcoObject()
		{
			PexPreConstructorParameterless();
			__Identity = Vortex.Connector.IConnectorFactory.CreateULINT();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoObject
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoObject()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoObject : Vortex.Connector.IVortexOnlineObject
	{
		[RenderIgnore(), ReadOnly()]
		Vortex.Connector.ValueTypes.Online.IOnlineULInt _Identity
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCore.PlainTcoObject CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCore.PlainTcoObject source);
		void FlushOnlineToPlain(TcoCore.PlainTcoObject source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoObject : Vortex.Connector.IVortexShadowObject
	{
		[RenderIgnore(), ReadOnly()]
		Vortex.Connector.ValueTypes.Shadows.IShadowULInt _Identity
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCore.PlainTcoObject CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCore.PlainTcoObject source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoObject : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		System.UInt64 __Identity;
		[RenderIgnore(), ReadOnly()]
		public System.UInt64 _Identity
		{
			get
			{
				return __Identity;
			}

			set
			{
				if (__Identity != value)
				{
					__Identity = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(_Identity)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCore.TcoObject target)
		{
			target._Identity.Cyclic = _Identity;
		}

		public void CopyPlainToCyclic(TcoCore.ITcoObject target)
		{
			this.CopyPlainToCyclic((TcoCore.TcoObject)target);
		}

		public void CopyPlainToShadow(TcoCore.TcoObject target)
		{
			target._Identity.Shadow = _Identity;
		}

		public void CopyPlainToShadow(TcoCore.IShadowTcoObject target)
		{
			this.CopyPlainToShadow((TcoCore.TcoObject)target);
		}

		public void CopyCyclicToPlain(TcoCore.TcoObject source)
		{
			_Identity = source._Identity.LastValue;
		}

		public void CopyCyclicToPlain(TcoCore.ITcoObject source)
		{
			this.CopyCyclicToPlain((TcoCore.TcoObject)source);
		}

		public void CopyShadowToPlain(TcoCore.TcoObject source)
		{
			_Identity = source._Identity.Shadow;
		}

		public void CopyShadowToPlain(TcoCore.IShadowTcoObject source)
		{
			this.CopyShadowToPlain((TcoCore.TcoObject)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoObject()
		{
		}
	}
}