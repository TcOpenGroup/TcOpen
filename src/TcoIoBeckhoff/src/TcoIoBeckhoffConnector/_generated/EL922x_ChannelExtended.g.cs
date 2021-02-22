using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoIoBeckhoff
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "EL922x_ChannelExtended", "TcoIoBeckhoff", TypeComplexityEnum.Complex)]
	public partial class EL922x_ChannelExtended : Vortex.Connector.IVortexObject, IEL922x_ChannelExtended, IShadowEL922x_ChannelExtended, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		StatusStructExtended _Status;
		[IoLinkable("Inputs")]
		public StatusStructExtended Status
		{
			get
			{
				return _Status;
			}
		}

		[IoLinkable("Inputs")]
		IStatusStructExtended IEL922x_ChannelExtended.Status
		{
			get
			{
				return Status;
			}
		}

		[IoLinkable("Inputs")]
		IShadowStatusStructExtended IShadowEL922x_ChannelExtended.Status
		{
			get
			{
				return Status;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerUInt _Load;
		[IoLinkable("Inputs")]
		public Vortex.Connector.ValueTypes.OnlinerUInt Load
		{
			get
			{
				return _Load;
			}
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineUInt IEL922x_ChannelExtended.Load
		{
			get
			{
				return Load;
			}
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowUInt IShadowEL922x_ChannelExtended.Load
		{
			get
			{
				return Load;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerUInt _Current;
		[IoLinkable("Inputs")]
		public Vortex.Connector.ValueTypes.OnlinerUInt Current
		{
			get
			{
				return _Current;
			}
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineUInt IEL922x_ChannelExtended.Current
		{
			get
			{
				return Current;
			}
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowUInt IShadowEL922x_ChannelExtended.Current
		{
			get
			{
				return Current;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerUInt _VoltageIn;
		[IoLinkable("Inputs")]
		public Vortex.Connector.ValueTypes.OnlinerUInt VoltageIn
		{
			get
			{
				return _VoltageIn;
			}
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineUInt IEL922x_ChannelExtended.VoltageIn
		{
			get
			{
				return VoltageIn;
			}
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowUInt IShadowEL922x_ChannelExtended.VoltageIn
		{
			get
			{
				return VoltageIn;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerUInt _VoltageOut;
		[IoLinkable("Inputs")]
		public Vortex.Connector.ValueTypes.OnlinerUInt VoltageOut
		{
			get
			{
				return _VoltageOut;
			}
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineUInt IEL922x_ChannelExtended.VoltageOut
		{
			get
			{
				return VoltageOut;
			}
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowUInt IShadowEL922x_ChannelExtended.VoltageOut
		{
			get
			{
				return VoltageOut;
			}
		}

		OutputStruct _Control;
		[IoLinkable("")]
		public OutputStruct Control
		{
			get
			{
				return _Control;
			}
		}

		[IoLinkable("")]
		IOutputStruct IEL922x_ChannelExtended.Control
		{
			get
			{
				return Control;
			}
		}

		[IoLinkable("")]
		IShadowOutputStruct IShadowEL922x_ChannelExtended.Control
		{
			get
			{
				return Control;
			}
		}

		public void LazyOnlineToShadow()
		{
			Status.LazyOnlineToShadow();
			Load.Shadow = Load.LastValue;
			Current.Shadow = Current.LastValue;
			VoltageIn.Shadow = VoltageIn.LastValue;
			VoltageOut.Shadow = VoltageOut.LastValue;
			Control.LazyOnlineToShadow();
		}

		public void LazyShadowToOnline()
		{
			Status.LazyShadowToOnline();
			Load.Cyclic = Load.Shadow;
			Current.Cyclic = Current.Shadow;
			VoltageIn.Cyclic = VoltageIn.Shadow;
			VoltageOut.Cyclic = VoltageOut.Shadow;
			Control.LazyShadowToOnline();
		}

		public PlainEL922x_ChannelExtended CreatePlainerType()
		{
			var cloned = new PlainEL922x_ChannelExtended();
			cloned.Status = Status.CreatePlainerType();
			cloned.Control = Control.CreatePlainerType();
			return cloned;
		}

		protected PlainEL922x_ChannelExtended CreatePlainerType(PlainEL922x_ChannelExtended cloned)
		{
			cloned.Status = Status.CreatePlainerType();
			cloned.Control = Control.CreatePlainerType();
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

		public void FlushPlainToOnline(TcoIoBeckhoff.PlainEL922x_ChannelExtended source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.PlainEL922x_ChannelExtended source)
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

		public void FlushOnlineToPlain(TcoIoBeckhoff.PlainEL922x_ChannelExtended source)
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

		public EL922x_ChannelExtended(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			_Status = new StatusStructExtended(this, "", "Status");
			_Load = @Connector.Online.Adapter.CreateUINT(this, "", "Load");
			_Current = @Connector.Online.Adapter.CreateUINT(this, "", "Current");
			_VoltageIn = @Connector.Online.Adapter.CreateUINT(this, "", "VoltageIn");
			_VoltageOut = @Connector.Online.Adapter.CreateUINT(this, "", "VoltageOut");
			_Control = new OutputStruct(this, "", "Control");
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public EL922x_ChannelExtended()
		{
			PexPreConstructorParameterless();
			_Status = new StatusStructExtended();
			_Load = Vortex.Connector.IConnectorFactory.CreateUINT();
			_Current = Vortex.Connector.IConnectorFactory.CreateUINT();
			_VoltageIn = Vortex.Connector.IConnectorFactory.CreateUINT();
			_VoltageOut = Vortex.Connector.IConnectorFactory.CreateUINT();
			_Control = new OutputStruct();
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcEL922x_ChannelExtended
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcEL922x_ChannelExtended()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IEL922x_ChannelExtended : Vortex.Connector.IVortexOnlineObject
	{
		[IoLinkable("Inputs")]
		IStatusStructExtended Status
		{
			get;
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineUInt Load
		{
			get;
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineUInt Current
		{
			get;
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineUInt VoltageIn
		{
			get;
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Online.IOnlineUInt VoltageOut
		{
			get;
		}

		[IoLinkable("")]
		IOutputStruct Control
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainEL922x_ChannelExtended CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoIoBeckhoff.PlainEL922x_ChannelExtended source);
		void FlushOnlineToPlain(TcoIoBeckhoff.PlainEL922x_ChannelExtended source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowEL922x_ChannelExtended : Vortex.Connector.IVortexShadowObject
	{
		[IoLinkable("Inputs")]
		IShadowStatusStructExtended Status
		{
			get;
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowUInt Load
		{
			get;
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowUInt Current
		{
			get;
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowUInt VoltageIn
		{
			get;
		}

		[IoLinkable("Inputs")]
		Vortex.Connector.ValueTypes.Shadows.IShadowUInt VoltageOut
		{
			get;
		}

		[IoLinkable("")]
		IShadowOutputStruct Control
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoIoBeckhoff.PlainEL922x_ChannelExtended CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoIoBeckhoff.PlainEL922x_ChannelExtended source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainEL922x_ChannelExtended : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		PlainStatusStructExtended _Status;
		[IoLinkable("Inputs")]
		public PlainStatusStructExtended Status
		{
			get
			{
				return _Status;
			}

			set
			{
				if (_Status != value)
				{
					_Status = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Status)));
				}
			}
		}

		System.UInt16 _Load;
		[IoLinkable("Inputs")]
		public System.UInt16 Load
		{
			get
			{
				return _Load;
			}

			set
			{
				if (_Load != value)
				{
					_Load = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Load)));
				}
			}
		}

		System.UInt16 _Current;
		[IoLinkable("Inputs")]
		public System.UInt16 Current
		{
			get
			{
				return _Current;
			}

			set
			{
				if (_Current != value)
				{
					_Current = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Current)));
				}
			}
		}

		System.UInt16 _VoltageIn;
		[IoLinkable("Inputs")]
		public System.UInt16 VoltageIn
		{
			get
			{
				return _VoltageIn;
			}

			set
			{
				if (_VoltageIn != value)
				{
					_VoltageIn = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(VoltageIn)));
				}
			}
		}

		System.UInt16 _VoltageOut;
		[IoLinkable("Inputs")]
		public System.UInt16 VoltageOut
		{
			get
			{
				return _VoltageOut;
			}

			set
			{
				if (_VoltageOut != value)
				{
					_VoltageOut = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(VoltageOut)));
				}
			}
		}

		PlainOutputStruct _Control;
		[IoLinkable("")]
		public PlainOutputStruct Control
		{
			get
			{
				return _Control;
			}

			set
			{
				if (_Control != value)
				{
					_Control = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Control)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoIoBeckhoff.EL922x_ChannelExtended target)
		{
			Status.CopyPlainToCyclic(target.Status);
			target.Load.Cyclic = Load;
			target.Current.Cyclic = Current;
			target.VoltageIn.Cyclic = VoltageIn;
			target.VoltageOut.Cyclic = VoltageOut;
			Control.CopyPlainToCyclic(target.Control);
		}

		public void CopyPlainToCyclic(TcoIoBeckhoff.IEL922x_ChannelExtended target)
		{
			this.CopyPlainToCyclic((TcoIoBeckhoff.EL922x_ChannelExtended)target);
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.EL922x_ChannelExtended target)
		{
			Status.CopyPlainToShadow(target.Status);
			target.Load.Shadow = Load;
			target.Current.Shadow = Current;
			target.VoltageIn.Shadow = VoltageIn;
			target.VoltageOut.Shadow = VoltageOut;
			Control.CopyPlainToShadow(target.Control);
		}

		public void CopyPlainToShadow(TcoIoBeckhoff.IShadowEL922x_ChannelExtended target)
		{
			this.CopyPlainToShadow((TcoIoBeckhoff.EL922x_ChannelExtended)target);
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.EL922x_ChannelExtended source)
		{
			Status.CopyCyclicToPlain(source.Status);
			Load = source.Load.LastValue;
			Current = source.Current.LastValue;
			VoltageIn = source.VoltageIn.LastValue;
			VoltageOut = source.VoltageOut.LastValue;
			Control.CopyCyclicToPlain(source.Control);
		}

		public void CopyCyclicToPlain(TcoIoBeckhoff.IEL922x_ChannelExtended source)
		{
			this.CopyCyclicToPlain((TcoIoBeckhoff.EL922x_ChannelExtended)source);
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.EL922x_ChannelExtended source)
		{
			Status.CopyShadowToPlain(source.Status);
			Load = source.Load.Shadow;
			Current = source.Current.Shadow;
			VoltageIn = source.VoltageIn.Shadow;
			VoltageOut = source.VoltageOut.Shadow;
			Control.CopyShadowToPlain(source.Control);
		}

		public void CopyShadowToPlain(TcoIoBeckhoff.IShadowEL922x_ChannelExtended source)
		{
			this.CopyShadowToPlain((TcoIoBeckhoff.EL922x_ChannelExtended)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainEL922x_ChannelExtended()
		{
			_Status = new PlainStatusStructExtended();
			_Control = new PlainOutputStruct();
		}
	}
}