using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
#pragma warning disable SA1402, CS1591, CS0108, CS0067
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("{attribute addProperty Name \"\" }", "StepDetails", "TcoCore", TypeComplexityEnum.Complex)]
	public partial class StepDetails : Vortex.Connector.IVortexObject, IStepDetails, IShadowStepDetails, Vortex.Connector.IVortexOnlineObject, Vortex.Connector.IVortexShadowObject
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
		Vortex.Connector.ValueTypes.OnlinerInt _ID;
		public Vortex.Connector.ValueTypes.OnlinerInt ID
		{
			get
			{
				return _ID;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineInt IStepDetails.ID
		{
			get
			{
				return ID;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadowStepDetails.ID
		{
			get
			{
				return ID;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerUInt _Order;
		public Vortex.Connector.ValueTypes.OnlinerUInt Order
		{
			get
			{
				return _Order;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineUInt IStepDetails.Order
		{
			get
			{
				return Order;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowUInt IShadowStepDetails.Order
		{
			get
			{
				return Order;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerBool _Enabled;
		public Vortex.Connector.ValueTypes.OnlinerBool Enabled
		{
			get
			{
				return _Enabled;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool IStepDetails.Enabled
		{
			get
			{
				return Enabled;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool IShadowStepDetails.Enabled
		{
			get
			{
				return Enabled;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerString _Description;
		public Vortex.Connector.ValueTypes.OnlinerString Description
		{
			get
			{
				return _Description;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineString IStepDetails.Description
		{
			get
			{
				return Description;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowString IShadowStepDetails.Description
		{
			get
			{
				return Description;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerInt _Status;
		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eStepStatus))]
		public Vortex.Connector.ValueTypes.OnlinerInt Status
		{
			get
			{
				return _Status;
			}
		}

		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eStepStatus))]
		Vortex.Connector.ValueTypes.Online.IOnlineInt IStepDetails.Status
		{
			get
			{
				return Status;
			}
		}

		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eStepStatus))]
		Vortex.Connector.ValueTypes.Shadows.IShadowInt IShadowStepDetails.Status
		{
			get
			{
				return Status;
			}
		}

		Vortex.Connector.ValueTypes.OnlinerTime _Duration;
		public Vortex.Connector.ValueTypes.OnlinerTime Duration
		{
			get
			{
				return _Duration;
			}
		}

		Vortex.Connector.ValueTypes.Online.IOnlineTime IStepDetails.Duration
		{
			get
			{
				return Duration;
			}
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowTime IShadowStepDetails.Duration
		{
			get
			{
				return Duration;
			}
		}

		public void LazyOnlineToShadow()
		{
			ID.Shadow = ID.LastValue;
			Order.Shadow = Order.LastValue;
			Enabled.Shadow = Enabled.LastValue;
			Description.Shadow = Description.LastValue;
			Status.Shadow = Status.LastValue;
			Duration.Shadow = Duration.LastValue;
		}

		public void LazyShadowToOnline()
		{
			ID.Cyclic = ID.Shadow;
			Order.Cyclic = Order.Shadow;
			Enabled.Cyclic = Enabled.Shadow;
			Description.Cyclic = Description.Shadow;
			Status.Cyclic = Status.Shadow;
			Duration.Cyclic = Duration.Shadow;
		}

		public PlainStepDetails CreatePlainerType()
		{
			var cloned = new PlainStepDetails();
			return cloned;
		}

		protected PlainStepDetails CreatePlainerType(PlainStepDetails cloned)
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

		public void FlushPlainToOnline(TcoCore.PlainStepDetails source)
		{
			source.CopyPlainToCyclic(this);
			this.Write();
		}

		public void CopyPlainToShadow(TcoCore.PlainStepDetails source)
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

		public void FlushOnlineToPlain(TcoCore.PlainStepDetails source)
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

		public StepDetails(Vortex.Connector.IVortexObject parent, string readableTail, string symbolTail)
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
			_ID = @Connector.Online.Adapter.CreateINT(this, "<#Step ID#>", "ID");
			ID.AttributeName = "<#Step ID#>";
			_Order = @Connector.Online.Adapter.CreateUINT(this, "<#Step Order#>", "Order");
			Order.AttributeName = "<#Step Order#>";
			_Enabled = @Connector.Online.Adapter.CreateBOOL(this, "<#Enabled#>", "Enabled");
			Enabled.AttributeName = "<#Enabled#>";
			_Description = @Connector.Online.Adapter.CreateSTRING(this, "<#Step description#>", "Description");
			Description.AttributeName = "<#Step description#>";
			_Status = @Connector.Online.Adapter.CreateINT(this, "<#Step status#>", "Status");
			Status.AttributeName = "<#Step status#>";
			_Duration = @Connector.Online.Adapter.CreateTIME(this, "<#Step duration#>", "Duration");
			Duration.AttributeName = "<#Step duration#>";
			AttributeName = "";
			parent.AddChild(this);
			parent.AddKid(this);
			PexConstructor(parent, readableTail, symbolTail);
		}

		public StepDetails()
		{
			PexPreConstructorParameterless();
			_ID = Vortex.Connector.IConnectorFactory.CreateINT();
			ID.AttributeName = "<#Step ID#>";
			_Order = Vortex.Connector.IConnectorFactory.CreateUINT();
			Order.AttributeName = "<#Step Order#>";
			_Enabled = Vortex.Connector.IConnectorFactory.CreateBOOL();
			Enabled.AttributeName = "<#Enabled#>";
			_Description = Vortex.Connector.IConnectorFactory.CreateSTRING();
			Description.AttributeName = "<#Step description#>";
			_Status = Vortex.Connector.IConnectorFactory.CreateINT();
			Status.AttributeName = "<#Step status#>";
			_Duration = Vortex.Connector.IConnectorFactory.CreateTIME();
			Duration.AttributeName = "<#Step duration#>";
			AttributeName = "";
			PexConstructorParameterless();
		}

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
		protected abstract class PlcStepDetails
		{
			///<summary>Prevents creating instance of this class via public constructor</summary><exclude/>
			protected PlcStepDetails()
			{
			}
		}
	}

	
            /// <summary>
            /// This is onliner interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IStepDetails : Vortex.Connector.IVortexOnlineObject
	{
		Vortex.Connector.ValueTypes.Online.IOnlineInt ID
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineUInt Order
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineBool Enabled
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineString Description
		{
			get;
		}

		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eStepStatus))]
		Vortex.Connector.ValueTypes.Online.IOnlineInt Status
		{
			get;
		}

		Vortex.Connector.ValueTypes.Online.IOnlineTime Duration
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCore.PlainStepDetails CreatePlainerType();
		void FlushOnlineToShadow();
		void FlushPlainToOnline(TcoCore.PlainStepDetails source);
		void FlushOnlineToPlain(TcoCore.PlainStepDetails source);
	}

	
            /// <summary>
            /// This is shadow interface for its respective class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial interface IShadowStepDetails : Vortex.Connector.IVortexShadowObject
	{
		Vortex.Connector.ValueTypes.Shadows.IShadowInt ID
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowUInt Order
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowBool Enabled
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowString Description
		{
			get;
		}

		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eStepStatus))]
		Vortex.Connector.ValueTypes.Shadows.IShadowInt Status
		{
			get;
		}

		Vortex.Connector.ValueTypes.Shadows.IShadowTime Duration
		{
			get;
		}

		System.String AttributeName
		{
			get;
		}

		TcoCore.PlainStepDetails CreatePlainerType();
		void FlushShadowToOnline();
		void CopyPlainToShadow(TcoCore.PlainStepDetails source);
	}

	
            /// <summary>
            /// This is POCO object for its respective onliner class. For documentation of this type see the onliner class.
            /// </summary>
            /// <exclude />
	public partial class PlainStepDetails : System.ComponentModel.INotifyPropertyChanged, Vortex.Connector.IPlain
	{
		System.Int16 _ID;
		public System.Int16 ID
		{
			get
			{
				return _ID;
			}

			set
			{
				if (_ID != value)
				{
					_ID = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(ID)));
				}
			}
		}

		System.UInt16 _Order;
		public System.UInt16 Order
		{
			get
			{
				return _Order;
			}

			set
			{
				if (_Order != value)
				{
					_Order = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Order)));
				}
			}
		}

		System.Boolean _Enabled;
		public System.Boolean Enabled
		{
			get
			{
				return _Enabled;
			}

			set
			{
				if (_Enabled != value)
				{
					_Enabled = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Enabled)));
				}
			}
		}

		System.String _Description;
		public System.String Description
		{
			get
			{
				return _Description;
			}

			set
			{
				if (_Description != value)
				{
					_Description = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Description)));
				}
			}
		}

		System.Int16 _Status;
		[Vortex.Connector.EnumeratorDiscriminatorAttribute(typeof (eStepStatus))]
		public System.Int16 Status
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

		System.TimeSpan _Duration;
		public System.TimeSpan Duration
		{
			get
			{
				return _Duration;
			}

			set
			{
				if (_Duration != value)
				{
					_Duration = value;
					PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Duration)));
				}
			}
		}

		public void CopyPlainToCyclic(TcoCore.StepDetails target)
		{
			target.ID.Cyclic = ID;
			target.Order.Cyclic = Order;
			target.Enabled.Cyclic = Enabled;
			target.Description.Cyclic = Description;
			target.Status.Cyclic = Status;
			target.Duration.Cyclic = Duration;
		}

		public void CopyPlainToCyclic(TcoCore.IStepDetails target)
		{
			this.CopyPlainToCyclic((TcoCore.StepDetails)target);
		}

		public void CopyPlainToShadow(TcoCore.StepDetails target)
		{
			target.ID.Shadow = ID;
			target.Order.Shadow = Order;
			target.Enabled.Shadow = Enabled;
			target.Description.Shadow = Description;
			target.Status.Shadow = Status;
			target.Duration.Shadow = Duration;
		}

		public void CopyPlainToShadow(TcoCore.IShadowStepDetails target)
		{
			this.CopyPlainToShadow((TcoCore.StepDetails)target);
		}

		public void CopyCyclicToPlain(TcoCore.StepDetails source)
		{
			ID = source.ID.LastValue;
			Order = source.Order.LastValue;
			Enabled = source.Enabled.LastValue;
			Description = source.Description.LastValue;
			Status = source.Status.LastValue;
			Duration = source.Duration.LastValue;
		}

		public void CopyCyclicToPlain(TcoCore.IStepDetails source)
		{
			this.CopyCyclicToPlain((TcoCore.StepDetails)source);
		}

		public void CopyShadowToPlain(TcoCore.StepDetails source)
		{
			ID = source.ID.Shadow;
			Order = source.Order.Shadow;
			Enabled = source.Enabled.Shadow;
			Description = source.Description.Shadow;
			Status = source.Status.Shadow;
			Duration = source.Duration.Shadow;
		}

		public void CopyShadowToPlain(TcoCore.IShadowStepDetails source)
		{
			this.CopyShadowToPlain((TcoCore.StepDetails)source);
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public PlainStepDetails()
		{
		}
	}
}