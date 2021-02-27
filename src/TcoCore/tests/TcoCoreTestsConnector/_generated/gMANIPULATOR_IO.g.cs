using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "gMANIPULATOR_IO", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class gMANIPULATOR_IO : Vortex.Connector.IVortexObject, IgMANIPULATOR_IO, IShadowgMANIPULATOR_IO, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		[IoLinkable("Inputs")]
		public Vortex.Connector.ValueTypes.OnlinerBool[] Inputs
		{
			get;
			set;
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineBool[] IgMANIPULATOR_IO.Inputs
		{
			get
			{
				return Inputs;
			}

			set
			{
				Inputs = (Vortex.Connector.ValueTypes.OnlinerBool[])value;
			}
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool[] IShadowgMANIPULATOR_IO.Inputs
		{
			get
			{
				return Inputs;
			}
		}

		[IoLinkable("Outputs")]
		public Vortex.Connector.ValueTypes.OnlinerBool[] Outputs
		{
			get;
			set;
		}

		[IoLinkable("Outputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineBool[] IgMANIPULATOR_IO.Outputs
		{
			get
			{
				return Outputs;
			}

			set
			{
				Outputs = (Vortex.Connector.ValueTypes.OnlinerBool[])value;
			}
		}

		[IoLinkable("Outputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool[] IShadowgMANIPULATOR_IO.Outputs
		{
			get
			{
				return Outputs;
			}
		}

		public void LazyOnlineToShadow()
		{
			Vortex.Connector.BuilderHelpers.Arrays.CopyCyclicToShadowPrimitive<Vortex.Connector.ValueTypes.OnlinerBool>(Inputs);
			Vortex.Connector.BuilderHelpers.Arrays.CopyCyclicToShadowPrimitive<Vortex.Connector.ValueTypes.OnlinerBool>(Outputs);
		}

		public void LazyShadowToOnline()
		{
			Vortex.Connector.BuilderHelpers.Arrays.CopyShadowToCyclicPrimitive<Vortex.Connector.ValueTypes.OnlinerBool>(Inputs);
			Vortex.Connector.BuilderHelpers.Arrays.CopyShadowToCyclicPrimitive<Vortex.Connector.ValueTypes.OnlinerBool>(Outputs);
		}

		public PlaingMANIPULATOR_IO CreatePlainerType()
		{
			var cloned = new PlaingMANIPULATOR_IO();
			cloned.Inputs = new System.Boolean[8];
			cloned.Outputs = new System.Boolean[8];
			return cloned;
		}

		protected PlaingMANIPULATOR_IO CreatePlainerType(PlaingMANIPULATOR_IO cloned)
		{
			cloned.Inputs = new System.Boolean[8];
			cloned.Outputs = new System.Boolean[8];
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

		public void FlushPlainToOnline(TcoCoreTests.PlaingMANIPULATOR_IO source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlaingMANIPULATOR_IO source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlaingMANIPULATOR_IO source)
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

		public gMANIPULATOR_IO(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			Inputs = new Vortex.Connector.ValueTypes.OnlinerBool[8];
			Vortex.Connector.BuilderHelpers.Arrays.InstantiateArray(Inputs, this, "", "Inputs", (p, rt, st) => @Connector.Online.Adapter.CreateBOOL(p, rt, st));
			Outputs = new Vortex.Connector.ValueTypes.OnlinerBool[8];
			Vortex.Connector.BuilderHelpers.Arrays.InstantiateArray(Outputs, this, "", "Outputs", (p, rt, st) => @Connector.Online.Adapter.CreateBOOL(p, rt, st));
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public gMANIPULATOR_IO()
		{
			PexPreConstructorParameterless();
			Inputs = new Vortex.Connector.ValueTypes.OnlinerBool[8];
			Vortex.Connector.BuilderHelpers.Arrays.InstantiateArray(Inputs, () => Vortex.Connector.IConnectorFactory.CreateBOOL());
			Outputs = new Vortex.Connector.ValueTypes.OnlinerBool[8];
			Vortex.Connector.BuilderHelpers.Arrays.InstantiateArray(Outputs, () => Vortex.Connector.IConnectorFactory.CreateBOOL());
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcgMANIPULATOR_IO
		{
			public object Inputs;
			public object Outputs;
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcgMANIPULATOR_IO()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IgMANIPULATOR_IO : Vortex.Connector.IVortexOnlineObject
	{
		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineBool[] Inputs
		{
			get;
			set;
		}

		[IoLinkable("Outputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineBool[] Outputs
		{
			get;
			set;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlaingMANIPULATOR_IO CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlaingMANIPULATOR_IO source);
		void FlushOnlineToPlain(TcoCoreTests.PlaingMANIPULATOR_IO source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowgMANIPULATOR_IO : Vortex.Connector.IVortexShadowObject
	{
		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool[] Inputs
		{
			get;
		}

		[IoLinkable("Outputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowBool[] Outputs
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlaingMANIPULATOR_IO CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlaingMANIPULATOR_IO source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlaingMANIPULATOR_IO : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		public System.Boolean[] Inputs
		{
			get;
			set;
		}

		public System.Boolean[] Outputs
		{
			get;
			set;
		}

		public void CopyPlainToCyclic(TcoCoreTests.gMANIPULATOR_IO target)
		{
			Vortex.Connector.BuilderHelpers.Arrays.CopyPlainToOnline<System.Boolean, Vortex.Connector.ValueTypes.OnlinerBool>(Inputs, target.Inputs);
			Vortex.Connector.BuilderHelpers.Arrays.CopyPlainToOnline<System.Boolean, Vortex.Connector.ValueTypes.OnlinerBool>(Outputs, target.Outputs);
		}

		public void CopyPlainToCyclic(TcoCoreTests.IgMANIPULATOR_IO target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.gMANIPULATOR_IO)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.gMANIPULATOR_IO target)
		{
			Vortex.Connector.BuilderHelpers.Arrays.CopyPlainToShadow<System.Boolean, Vortex.Connector.ValueTypes.OnlinerBool>(Inputs, target.Inputs);
			Vortex.Connector.BuilderHelpers.Arrays.CopyPlainToShadow<System.Boolean, Vortex.Connector.ValueTypes.OnlinerBool>(Outputs, target.Outputs);
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowgMANIPULATOR_IO target)
		{
			this.CopyPlainToShadow((TcoCoreTests.gMANIPULATOR_IO)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.gMANIPULATOR_IO source)
		{
			Vortex.Connector.BuilderHelpers.Arrays.CopyOnlineToPlain<Vortex.Connector.ValueTypes.OnlinerBool, System.Boolean>(source.Inputs, Inputs);
			Vortex.Connector.BuilderHelpers.Arrays.CopyOnlineToPlain<Vortex.Connector.ValueTypes.OnlinerBool, System.Boolean>(source.Outputs, Outputs);
		}

		public void CopyCyclicToPlain(TcoCoreTests.IgMANIPULATOR_IO source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.gMANIPULATOR_IO)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.gMANIPULATOR_IO source)
		{
			Vortex.Connector.BuilderHelpers.Arrays.CopyShadowToPlain<Vortex.Connector.ValueTypes.OnlinerBool, System.Boolean>(source.Inputs, Inputs);
			Vortex.Connector.BuilderHelpers.Arrays.CopyShadowToPlain<Vortex.Connector.ValueTypes.OnlinerBool, System.Boolean>(source.Outputs, Outputs);
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowgMANIPULATOR_IO source)
		{
			this.CopyShadowToPlain((TcoCoreTests.gMANIPULATOR_IO)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlaingMANIPULATOR_IO()
		{
			Inputs = new System.Boolean[8];
			Outputs = new System.Boolean[8];
		}
	}
}