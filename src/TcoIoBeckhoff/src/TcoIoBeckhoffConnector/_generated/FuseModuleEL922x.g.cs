using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoIoBeckhoff
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "FuseModuleEL922x", "TcoIoBeckhoff", TypeComplexityEnum.Complex)]
	public partial class FuseModuleEL922x : Vortex.Connector.IVortexObject, IFuseModuleEL922x, IShadowFuseModuleEL922x, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
				return TcoIoBeckhoffTwinController.Translator.Translate(_humanReadable).Interpolate(this);
			}

			protected set
			{
				_humanReadable = value;
			}
		}

		protected string _humanReadable;
		public void LazyOnlineToShadow()
		{
		}

		public void LazyShadowToOnline()
		{
		}

		public PlainFuseModuleEL922x CreatePlainerType()
		{
			var cloned = new PlainFuseModuleEL922x();
			return cloned;
		}

		protected PlainFuseModuleEL922x CreatePlainerType(PlainFuseModuleEL922x cloned)
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

		public void FlushPlainToOnline(TcoIoBeckhoff.PlainFuseModuleEL922x source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.PlainFuseModuleEL922x source)
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

		public void FlushOnlineToPlain(TcoIoBeckhoff.PlainFuseModuleEL922x source)
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
				return TcoIoBeckhoffTwinController.Translator.Translate(_AttributeName).Interpolate(this);
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

		public FuseModuleEL922x(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public FuseModuleEL922x()
		{
			PexPreConstructorParameterless();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcFuseModuleEL922x
		{
			public object _chn1Basic;
			public object _chn2Basic;
			public object _chn1Extended;
			public object _chn2Extended;
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcFuseModuleEL922x()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IFuseModuleEL922x : Vortex.Connector.IVortexOnlineObject
	{
		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainFuseModuleEL922x CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoIoBeckhoff.PlainFuseModuleEL922x source);
		void FlushOnlineToPlain(TcoIoBeckhoff.PlainFuseModuleEL922x source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowFuseModuleEL922x : Vortex.Connector.IVortexShadowObject
	{
		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainFuseModuleEL922x CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoIoBeckhoff.PlainFuseModuleEL922x source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainFuseModuleEL922x : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		public void CopyPlainToCyclic(TcoIoBeckhoff.FuseModuleEL922x target)
		{
		}

		public void CopyPlainToCyclic(TcoIoBeckhoff.IFuseModuleEL922x target)
		{
			this.CopyPlainToCyclic((TcoIoBeckhoff.FuseModuleEL922x)target);
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.FuseModuleEL922x target)
		{
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.IShadowFuseModuleEL922x target)
		{
			this.CopyPlainToShadow((TcoIoBeckhoff.FuseModuleEL922x)target);
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.FuseModuleEL922x source)
		{
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.IFuseModuleEL922x source)
		{
			this.CopyCyclicToPlain((TcoIoBeckhoff.FuseModuleEL922x)source);
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.FuseModuleEL922x source)
		{
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.IShadowFuseModuleEL922x source)
		{
			this.CopyShadowToPlain((TcoIoBeckhoff.FuseModuleEL922x)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainFuseModuleEL922x()
		{
		}
	}
}