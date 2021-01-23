using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "TcoContext_App_2", "TcoCoreTests", TypeComplexityEnum.Complex)]
	public partial class TcoContext_App_2 : Vortex.Connector.IVortexObject, ITcoContext_App_2, IShadowTcoContext_App_2, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		Vortex.Connector.ValueTypes.OnlinerBool _ResetToZero;
		public Vortex.Connector.ValueTypes.OnlinerBool ResetToZero
		{
			get
			{
				return _ResetToZero;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool ITcoContext_App_2.ResetToZero
		{
			get
			{
				return ResetToZero;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowTcoContext_App_2.ResetToZero
		{
			get
			{
				return ResetToZero;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLInt _C1;
		public Vortex.Connector.ValueTypes.OnlinerLInt C1
		{
			get
			{
				return _C1;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt ITcoContext_App_2.C1
		{
			get
			{
				return C1;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt IShadowTcoContext_App_2.C1
		{
			get
			{
				return C1;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerLInt _C2;
		public Vortex.Connector.ValueTypes.OnlinerLInt C2
		{
			get
			{
				return _C2;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt ITcoContext_App_2.C2
		{
			get
			{
				return C2;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt IShadowTcoContext_App_2.C2
		{
			get
			{
				return C2;
			}
		}

		TcoObject_Counter _TcoObject_Counter_1;
		public TcoObject_Counter TcoObject_Counter_1
		{
			get
			{
				return _TcoObject_Counter_1;
			}
		}

		ITcoObject_Counter ITcoContext_App_2.TcoObject_Counter_1
		{
			get
			{
				return TcoObject_Counter_1;
			}
		}

		IShadowTcoObject_Counter IShadowTcoContext_App_2.TcoObject_Counter_1
		{
			get
			{
				return TcoObject_Counter_1;
			}
		}

		TcoObject_Counter _TcoObject_Counter_2;
		public TcoObject_Counter TcoObject_Counter_2
		{
			get
			{
				return _TcoObject_Counter_2;
			}
		}

		ITcoObject_Counter ITcoContext_App_2.TcoObject_Counter_2
		{
			get
			{
				return TcoObject_Counter_2;
			}
		}

		IShadowTcoObject_Counter IShadowTcoContext_App_2.TcoObject_Counter_2
		{
			get
			{
				return TcoObject_Counter_2;
			}
		}

		public void LazyOnlineToShadow()
		{
			ResetToZero.Shadow = ResetToZero.LastValue;
			C1.Shadow = C1.LastValue;
			C2.Shadow = C2.LastValue;
			TcoObject_Counter_1.LazyOnlineToShadow();
			TcoObject_Counter_2.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			ResetToZero.Cyclic = ResetToZero.Shadow;
			C1.Cyclic = C1.Shadow;
			C2.Cyclic = C2.Shadow;
			TcoObject_Counter_1.LazyShadowToOnline();
			TcoObject_Counter_2.LazyShadowToOnline();
		}

		public PlainTcoContext_App_2 CreatePlainerType()
		{
			var cloned = new PlainTcoContext_App_2();
			cloned.TcoObject_Counter_1 = TcoObject_Counter_1.CreatePlainerType();
			cloned.TcoObject_Counter_2 = TcoObject_Counter_2.CreatePlainerType();
			return cloned;
		}

		protected PlainTcoContext_App_2 CreatePlainerType(PlainTcoContext_App_2 cloned)
		{
			cloned.TcoObject_Counter_1 = TcoObject_Counter_1.CreatePlainerType();
			cloned.TcoObject_Counter_2 = TcoObject_Counter_2.CreatePlainerType();
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

		public void FlushPlainToOnline(TcoCoreTests.PlainTcoContext_App_2 source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCoreTests.PlainTcoContext_App_2 source)
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

		public void FlushOnlineToPlain(TcoCoreTests.PlainTcoContext_App_2 source)
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

		public TcoContext_App_2(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
		{
			this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@ValueTags = new System.Collections.Generic.List<Vortex.Connector.IValueTag>();
			this.@Children = new System.Collections.Generic.List<Vortex.Connector.IVortexObject>();
			this.@Parent = parent;
			_humanReadable = Vortex.Connector.IConnector.CreateSymbol(parent.HumanReadable, readableTail);
			PexPreConstructor(parent, readableTail, symbolTail);
			Symbol = Vortex.Connector.IConnector.CreateSymbol(parent.Symbol, symbolTail);
			_ResetToZero = @Connector.Online.Adapter.CreateBOOL(this, "", "ResetToZero");
			_C1 = @Connector.Online.Adapter.CreateLINT(this, "", "C1");
			_C2 = @Connector.Online.Adapter.CreateLINT(this, "", "C2");
			_TcoObject_Counter_1 = new TcoObject_Counter(this, "", "TcoObject_Counter_1");
			_TcoObject_Counter_2 = new TcoObject_Counter(this, "", "TcoObject_Counter_2");
			AttributeName = "";
			PexConstructor(parent, readableTail, symbolTail);
			parent.AddChild(this);
		}

		public TcoContext_App_2()
		{
			PexPreConstructorParameterless();
			_ResetToZero = Vortex.Connector.IConnectorFactory.CreateBOOL();
			_C1 = Vortex.Connector.IConnectorFactory.CreateLINT();
			_C2 = Vortex.Connector.IConnectorFactory.CreateLINT();
			_TcoObject_Counter_1 = new TcoObject_Counter();
			_TcoObject_Counter_2 = new TcoObject_Counter();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcTcoContext_App_2
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcTcoContext_App_2()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface ITcoContext_App_2 : Vortex.Connector.IVortexOnlineObject
	{
		Vortex.Connector.ValueTypes.Online.IOnlineBool ResetToZero
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt C1
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineLInt C2
		{
			get;
		}

		ITcoObject_Counter TcoObject_Counter_1
		{
			get;
		}

		ITcoObject_Counter TcoObject_Counter_2
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoContext_App_2 CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCoreTests.PlainTcoContext_App_2 source);
		void FlushOnlineToPlain(TcoCoreTests.PlainTcoContext_App_2 source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowTcoContext_App_2 : Vortex.Connector.IVortexShadowObject
	{
		Vortex.Connector.ValueTypes.Shadows.IShadowBool ResetToZero
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt C1
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowLInt C2
		{
			get;
		}

		IShadowTcoObject_Counter TcoObject_Counter_1
		{
			get;
		}

		IShadowTcoObject_Counter TcoObject_Counter_2
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCoreTests.PlainTcoContext_App_2 CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCoreTests.PlainTcoContext_App_2 source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainTcoContext_App_2 : Vortex.Connector.IPlain
	{
		System.Boolean _ResetToZero;
		public System.Boolean ResetToZero
		{
			get
			{
				return _ResetToZero;
			}

			set
			{
				if (_ResetToZero != value)
				{
					_ResetToZero = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(ResetToZero)));
				}
			}
		}

		System.Int64 _C1;
		public System.Int64 C1
		{
			get
			{
				return _C1;
			}

			set
			{
				if (_C1 != value)
				{
					_C1 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(C1)));
				}
			}
		}

		System.Int64 _C2;
		public System.Int64 C2
		{
			get
			{
				return _C2;
			}

			set
			{
				if (_C2 != value)
				{
					_C2 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(C2)));
				}
			}
		}

		PlainTcoObject_Counter _TcoObject_Counter_1;
		public PlainTcoObject_Counter TcoObject_Counter_1
		{
			get
			{
				return _TcoObject_Counter_1;
			}

			set
			{
				if (_TcoObject_Counter_1 != value)
				{
					_TcoObject_Counter_1 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(TcoObject_Counter_1)));
				}
			}
		}

		PlainTcoObject_Counter _TcoObject_Counter_2;
		public PlainTcoObject_Counter TcoObject_Counter_2
		{
			get
			{
				return _TcoObject_Counter_2;
			}

			set
			{
				if (_TcoObject_Counter_2 != value)
				{
					_TcoObject_Counter_2 = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(TcoObject_Counter_2)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCoreTests.TcoContext_App_2 target)
		{
			target.ResetToZero.Cyclic = ResetToZero;
			target.C1.Cyclic = C1;
			target.C2.Cyclic = C2;
			TcoObject_Counter_1.CopyPlainToCyclic(target.TcoObject_Counter_1);
			TcoObject_Counter_2.CopyPlainToCyclic(target.TcoObject_Counter_2);
		}

		public void CopyPlainToCyclic(TcoCoreTests.ITcoContext_App_2 target)
		{
			this.CopyPlainToCyclic((TcoCoreTests.TcoContext_App_2)target);
		}

		public void CopyPlainToShadow(TcoCoreTests.TcoContext_App_2 target)
		{
			target.ResetToZero.Shadow = ResetToZero;
			target.C1.Shadow = C1;
			target.C2.Shadow = C2;
			TcoObject_Counter_1.CopyPlainToShadow(target.TcoObject_Counter_1);
			TcoObject_Counter_2.CopyPlainToShadow(target.TcoObject_Counter_2);
		}

		public void CopyPlainToShadow(TcoCoreTests.IShadowTcoContext_App_2 target)
		{
			this.CopyPlainToShadow((TcoCoreTests.TcoContext_App_2)target);
		}

		public void CopyCyclicToPlain(TcoCoreTests.TcoContext_App_2 source)
		{
			ResetToZero = source.ResetToZero.LastValue;
			C1 = source.C1.LastValue;
			C2 = source.C2.LastValue;
			TcoObject_Counter_1.CopyCyclicToPlain(source.TcoObject_Counter_1);
			TcoObject_Counter_2.CopyCyclicToPlain(source.TcoObject_Counter_2);
		}

		public void CopyCyclicToPlain(TcoCoreTests.ITcoContext_App_2 source)
		{
			this.CopyCyclicToPlain((TcoCoreTests.TcoContext_App_2)source);
		}

		public void CopyShadowToPlain(TcoCoreTests.TcoContext_App_2 source)
		{
			ResetToZero = source.ResetToZero.Shadow;
			C1 = source.C1.Shadow;
			C2 = source.C2.Shadow;
			TcoObject_Counter_1.CopyShadowToPlain(source.TcoObject_Counter_1);
			TcoObject_Counter_2.CopyShadowToPlain(source.TcoObject_Counter_2);
		}

		public void CopyShadowToPlain(TcoCoreTests.IShadowTcoContext_App_2 source)
		{
			this.CopyShadowToPlain((TcoCoreTests.TcoContext_App_2)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainTcoContext_App_2()
		{
			_TcoObject_Counter_1 = new PlainTcoObject_Counter();
			_TcoObject_Counter_2 = new PlainTcoObject_Counter();
		}
	}
}