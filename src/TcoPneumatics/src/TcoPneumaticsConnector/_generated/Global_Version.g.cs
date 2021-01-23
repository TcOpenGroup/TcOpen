using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoPneumatics
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "Global_Version", "TcoPneumatics", TypeComplexityEnum.Complex)]
	public partial class Global_Version : Vortex.Connector.IVortexObject, IGlobal_Version, IShadowGlobal_Version, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
				return TcoPneumaticsTwinController.Translator.Translate(_humanReadable).Interpolate(this);
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

		public PlainGlobal_Version CreatePlainerType()
		{
			var cloned = new PlainGlobal_Version();
			return cloned;
		}

		protected PlainGlobal_Version CreatePlainerType(PlainGlobal_Version cloned)
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

		public void FlushPlainToOnline(TcoPneumatics.PlainGlobal_Version source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoPneumatics.PlainGlobal_Version source)
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

		public void FlushOnlineToPlain(TcoPneumatics.PlainGlobal_Version source)
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
				return TcoPneumaticsTwinController.Translator.Translate(_AttributeName).Interpolate(this);
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

		public Global_Version(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@ValueTags = new System.Collections.Generic.List<Vortex.Connector.IValueTag>();
			this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
			parent.AddChild(this);
		}

		public Global_Version()
		{
			PexPreConstructorParameterless();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcGlobal_Version
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcGlobal_Version()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IGlobal_Version : Vortex.Connector.IVortexOnlineObject
	{
		System.String AttributeName
		{
			get;
		}

		TcoPneumatics.PlainGlobal_Version CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoPneumatics.PlainGlobal_Version source);
		void FlushOnlineToPlain(TcoPneumatics.PlainGlobal_Version source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowGlobal_Version : Vortex.Connector.IVortexShadowObject
	{
		System.String AttributeName
		{
			get;
		}

		TcoPneumatics.PlainGlobal_Version CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoPneumatics.PlainGlobal_Version source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainGlobal_Version : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		public void CopyPlainToCyclic(TcoPneumatics.Global_Version target)
		{
		}

		public void CopyPlainToCyclic(TcoPneumatics.IGlobal_Version target)
		{
			this.CopyPlainToCyclic((TcoPneumatics.Global_Version)target);
		}

		public void CopyPlainToShadow(TcoPneumatics.Global_Version target)
		{
		}

		public void CopyPlainToShadow(TcoPneumatics.IShadowGlobal_Version target)
		{
			this.CopyPlainToShadow((TcoPneumatics.Global_Version)target);
		}

		public void CopyCyclicToPlain(TcoPneumatics.Global_Version source)
		{
		}

		public void CopyCyclicToPlain(TcoPneumatics.IGlobal_Version source)
		{
			this.CopyCyclicToPlain((TcoPneumatics.Global_Version)source);
		}

		public void CopyShadowToPlain(TcoPneumatics.Global_Version source)
		{
		}

		public void CopyShadowToPlain(TcoPneumatics.IShadowGlobal_Version source)
		{
			this.CopyShadowToPlain((TcoPneumatics.Global_Version)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainGlobal_Version()
		{
		}
	}
}