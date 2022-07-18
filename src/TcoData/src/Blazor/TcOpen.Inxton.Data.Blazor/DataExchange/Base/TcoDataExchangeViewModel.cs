using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Vortex.Presentation;

namespace TcoData
{

   
    public class TcoDataExchangeViewModel : RenderableViewModelBase
    {
        public TcoDataExchangeViewModel() : base()
        {

        }

        public IDataViewModel DataViewModel
        {
            get;
            private set;
        }

        protected virtual void UpdateAvailability()
        {
            
            DataViewModel.CancelEditCommandAvailable = true;
            DataViewModel.DeleteCommandAvailable = true;
            DataViewModel.EditCommandAvailable = true;
            DataViewModel.ExportCommandAvailable = true;
            DataViewModel.ImportCommandAvailable = true;
            DataViewModel.LoadFromPlcCommandAvailable = true;
            DataViewModel.SendToPlcCommandAvailable = true;
            DataViewModel.StartCreateCopyOfExistingAvailable = true;
            DataViewModel.StartCreateNewCommandAvailable = true;
            DataViewModel.UpdateCommandAvailable = true;
          

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

                    DataViewModel = (IDataViewModel)generic.Invoke(null, new object[] { dataExchangeObject.GetRepository(), dataExchangeObject });

                }
                catch 
                {
                    throw ;//new BrowsableObjectCreationException("Unable to create browsable object for the view. For details see inner exception.", ex);
                }

            
        }
    }
}
