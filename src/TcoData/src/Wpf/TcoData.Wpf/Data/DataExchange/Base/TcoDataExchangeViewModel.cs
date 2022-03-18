﻿// #define WITH_SEC

using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using Vortex.Presentation.Wpf;

namespace TcoData
{
    public class TcoDataExchangeViewModel : RenderableViewModel
    {

        public TcoDataExchangeViewModel() : base()
        {
          
        }

        public dynamic DataViewModel
        {
            get;
            private set;
        }
        
        protected virtual void UpdateAvailability()
        {
            try
            {
                ((FunctionAvailability)this.DataViewModel).CancelEditCommandAvailable = true;
                ((FunctionAvailability)this.DataViewModel).DeleteCommandAvailable = true;
                ((FunctionAvailability)this.DataViewModel).EditCommandAvailable = true;
                ((FunctionAvailability)this.DataViewModel).ExportCommandAvailable = true;
                ((FunctionAvailability)this.DataViewModel).ImportCommandAvailable = true;
                ((FunctionAvailability)this.DataViewModel).LoadFromPlcCommandAvailable = true;
                ((FunctionAvailability)this.DataViewModel).SendToPlcCommandAvailable = true;
                ((FunctionAvailability)this.DataViewModel).StartCreateCopyOfExistingAvailable = true;
                ((FunctionAvailability)this.DataViewModel).StartCreateNewCommandAvailable = true;
                ((FunctionAvailability)this.DataViewModel).UpdateCommandAvailable = true;
            }
            catch (Exception)
            {
                if(!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                    throw;
            }
            
        }

        public override object Model
        {
            get => this.DataViewModel.DataExchange;
            set
            {
                CreateBrowsable((TcoDataExchange)value);
                UpdateAvailability();
            }
        }
       

        private void CreateBrowsable(TcoDataExchange dataExchangeObject)
        {
            if (!PresentationHelper.IsDesignMode())
            {
                try
                {                   
                    var DataPropertyInfo = dataExchangeObject.GetType().GetProperty("_data");

                    if (DataPropertyInfo == null)
                    {
                        DataPropertyInfo = dataExchangeObject.GetType().GetProperty("_data", BindingFlags.NonPublic);
                    }


                    if (DataPropertyInfo == null)
                    {
                        DataPropertyInfo = dataExchangeObject.GetType().GetProperty("_data", BindingFlags.NonPublic | BindingFlags.Instance);
                    }

                    if (DataPropertyInfo == null)
                    {
                        throw new Exception($"{dataExchangeObject.GetType().ToString()} must implement member '_data' that inherits from {nameof(TcoEntity)}.");
                    }

                    var dataOfType = DataPropertyInfo.PropertyType.Name;
                    var dataNameSpace = DataPropertyInfo.PropertyType.Namespace;


                    MethodInfo method = typeof(DataViewModel).GetMethod("Create");
                    var genericTypeName = $"{dataNameSpace}.Plain{dataOfType}, {dataNameSpace}Connector";
                    var genericType = Type.GetType(genericTypeName);

                    if (genericType == null)
                    {
                        throw new Exception($"Could not retrieve {genericTypeName} when creating browsable object.");
                    }

                    MethodInfo generic = method.MakeGenericMethod(genericType);

                    DataViewModel = generic.Invoke(null, new object[] { dataExchangeObject.GetRepository(), dataExchangeObject });                    

                }
                catch (Exception ex)
                {
                    throw new BrowsableObjectCreationException("Unable to create browsable object for the view. For details see inner exception.", ex);
                }
                
            }
        }
       
    }    
}
