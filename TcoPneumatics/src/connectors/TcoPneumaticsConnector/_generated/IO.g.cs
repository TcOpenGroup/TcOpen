using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoPneumatics
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "IO", "TcoPneumatics", TypeComplexityEnum.Complex)]
	public partial class IO : Vortex.Connector.IVortexObject, IIO, IShadowIO, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		[IoLinkable("Inputs")]
		public Vortex.Connector.ValueTypes.OnlinerBool[] A1
		{
			get;
			set;
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineBool[] IIO.A1
		{
			get
			{
				return A1;
			}

			set
			{
				A1 = (Vortex.Connector.ValueTypes.OnlinerBool[])value;
			}
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool[] IShadowIO.A1
		{
			get
			{
				return A1;
			}
		}

		[IoLinkable("Outputs")]
		public Vortex.Connector.ValueTypes.OnlinerBool[] A2
		{
			get;
			set;
		}

		[IoLinkable("Outputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineBool[] IIO.A2
		{
			get
			{
				return A2;
			}

			set
			{
				A2 = (Vortex.Connector.ValueTypes.OnlinerBool[])value;
			}
		}

		[IoLinkable("Outputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool[] IShadowIO.A2
		{
			get
			{
				return A2;
			}
		}

		public void LazyOnlineToShadow()
		{
			Vortex.Connector.BuilderHelpers.Arrays.CopyCyclicToShadowPrimitive<Vortex.Connector.ValueTypes.OnlinerBool>(A1);
			Vortex.Connector.BuilderHelpers.Arrays.CopyCyclicToShadowPrimitive<Vortex.Connector.ValueTypes.OnlinerBool>(A2);
		}

		public void LazyShadowToOnline()
		{
			Vortex.Connector.BuilderHelpers.Arrays.CopyShadowToCyclicPrimitive<Vortex.Connector.ValueTypes.OnlinerBool>(A1);
			Vortex.Connector.BuilderHelpers.Arrays.CopyShadowToCyclicPrimitive<Vortex.Connector.ValueTypes.OnlinerBool>(A2);
		}

		public PlainIO CreatePlainerType()
		{
			var cloned = new PlainIO();
			cloned.A1 = new System.Boolean[8];
			cloned.A2 = new System.Boolean[8];
			return cloned;
		}

		protected PlainIO CreatePlainerType(PlainIO cloned)
		{
			cloned.A1 = new System.Boolean[8];
			cloned.A2 = new System.Boolean[8];
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

		public void FlushPlainToOnline(TcoPneumatics.PlainIO source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoPneumatics.PlainIO source)
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

		public void FlushOnlineToPlain(TcoPneumatics.PlainIO source)
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

		public IO(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@ValueTags = new System.Collections.Generic.List<Vortex.Connector.IValueTag>();
			this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			A1 = new Vortex.Connector.ValueTypes.OnlinerBool[8];
			Vortex.Connector.BuilderHelpers.Arrays.InstantiateArray(A1, this, "", "A1", (p, rt, st) => @Connector.Online.Adapter.CreateBOOL(p, rt, st));
			A2 = new Vortex.Connector.ValueTypes.OnlinerBool[8];
			Vortex.Connector.BuilderHelpers.Arrays.InstantiateArray(A2, this, "", "A2", (p, rt, st) => @Connector.Online.Adapter.CreateBOOL(p, rt, st));
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
			parent.AddChild(this);
		}

		public IO()
		{
			PexPreConstructorParameterless();
			A1 = new Vortex.Connector.ValueTypes.OnlinerBool[8];
			Vortex.Connector.BuilderHelpers.Arrays.InstantiateArray(A1, () => Vortex.Connector.IConnectorFactory.CreateBOOL());
			A2 = new Vortex.Connector.ValueTypes.OnlinerBool[8];
			Vortex.Connector.BuilderHelpers.Arrays.InstantiateArray(A2, () => Vortex.Connector.IConnectorFactory.CreateBOOL());
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcIO
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcIO()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IIO : Vortex.Connector.IVortexOnlineObject
	{
		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineBool[] A1
		{
			get;
			set;
		}

		[IoLinkable("Outputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineBool[] A2
		{
			get;
			set;
		}

		System.String AttributeName
		{
			get;
		}

		TcoPneumatics.PlainIO CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoPneumatics.PlainIO source);
		void FlushOnlineToPlain(TcoPneumatics.PlainIO source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowIO : Vortex.Connector.IVortexShadowObject
	{
		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool[] A1
		{
			get;
		}

		[IoLinkable("Outputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool[] A2
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoPneumatics.PlainIO CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoPneumatics.PlainIO source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainIO : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		public System.Boolean[] A1
		{
			get;
			set;
		}

		public System.Boolean[] A2
		{
			get;
			set;
		}

		public void CopyPlainToCyclic(TcoPneumatics.IO target)
		{
			Vortex.Connector.BuilderHelpers.Arrays.CopyPlainToOnline<System.Boolean, Vortex.Connector.ValueTypes.OnlinerBool>(A1, target.A1);
			Vortex.Connector.BuilderHelpers.Arrays.CopyPlainToOnline<System.Boolean, Vortex.Connector.ValueTypes.OnlinerBool>(A2, target.A2);
		}

		public void CopyPlainToCyclic(TcoPneumatics.IIO target)
		{
			this.CopyPlainToCyclic((TcoPneumatics.IO)target);
		}

		public void CopyPlainToShadow(TcoPneumatics.IO target)
		{
			Vortex.Connector.BuilderHelpers.Arrays.CopyPlainToShadow<System.Boolean, Vortex.Connector.ValueTypes.OnlinerBool>(A1, target.A1);
			Vortex.Connector.BuilderHelpers.Arrays.CopyPlainToShadow<System.Boolean, Vortex.Connector.ValueTypes.OnlinerBool>(A2, target.A2);
		}

		public void CopyPlainToShadow(TcoPneumatics.IShadowIO target)
		{
			this.CopyPlainToShadow((TcoPneumatics.IO)target);
		}

		public void CopyCyclicToPlain(TcoPneumatics.IO source)
		{
			Vortex.Connector.BuilderHelpers.Arrays.CopyOnlineToPlain<Vortex.Connector.ValueTypes.OnlinerBool, System.Boolean>(source.A1, A1);
			Vortex.Connector.BuilderHelpers.Arrays.CopyOnlineToPlain<Vortex.Connector.ValueTypes.OnlinerBool, System.Boolean>(source.A2, A2);
		}

		public void CopyCyclicToPlain(TcoPneumatics.IIO source)
		{
			this.CopyCyclicToPlain((TcoPneumatics.IO)source);
		}

		public void CopyShadowToPlain(TcoPneumatics.IO source)
		{
			Vortex.Connector.BuilderHelpers.Arrays.CopyShadowToPlain<Vortex.Connector.ValueTypes.OnlinerBool, System.Boolean>(source.A1, A1);
			Vortex.Connector.BuilderHelpers.Arrays.CopyShadowToPlain<Vortex.Connector.ValueTypes.OnlinerBool, System.Boolean>(source.A2, A2);
		}

		public void CopyShadowToPlain(TcoPneumatics.IShadowIO source)
		{
			this.CopyShadowToPlain((TcoPneumatics.IO)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainIO()
		{
			A1 = new System.Boolean[8];
			A2 = new System.Boolean[8];
		}
	}
}