using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using TcOpen.Inxton.RepositoryDataSet;

namespace TcoDrivesBeckhoff
{
    public class RepositoryHandler : INotifyPropertyChanged
    {
        public RepositoryHandler(RepositoryDataSetHandler<PositioningParamItem> dataSet)
        {
            DataHandler = dataSet;
        }

          
        public ObservableCollection<string> ListOfDataSets { 
            get
            {
                var items = DataHandler.Repository.Queryable.Select(p => p._EntityId).OrderBy(i => i).ToList();
                return  new ObservableCollection<string>(items);
            }
        }
       

        /// <summary>
        /// Gets current set.
        /// </summary>
        public EntitySet<PositioningParamItem> CurrentSet { get; set; } = new EntitySet<PositioningParamItem>();

        /// <summary>
        /// Gets production of this 
        /// </summary>
        protected RepositoryDataSetHandler<PositioningParamItem> DataHandler { get; }

        /// <summary>
        /// Loads items set from the repository to this controller.
        /// </summary>
        /// <param name="setid">set id.</param>
        public void LoadDataSet(string setid)
        {
            try
            {
                var result = DataHandler.Repository.Queryable.FirstOrDefault(p => p._EntityId == setid);

                if (result != null)
                    CurrentSet = DataHandler.Read(setid);


                OnPropertyChanged(nameof(CurrentSet));
            }
            catch (Exception)
            {

                //todo
            }
          
        }

        /// <summary>
        /// Saves items set from this controller to the repository.
        /// </summary>
        /// <param name="setId">Instrucion set id.</param>
        public void SaveDataSet(string setId)
        {
            try
            {
                if (!DataHandler.Repository.Queryable.Where(p => p._EntityId == setId).Any())
                {
                    DataHandler.Create(setId, CurrentSet);
                }
                DataHandler.Update(setId, CurrentSet);
                OnPropertyChanged(nameof(CurrentSet));
                OnPropertyChanged(nameof(ListOfDataSets));
            }
            catch (Exception)
            {

               //todo
            }
         


        }


        /// <summary>
        /// Delete set from the repository.
        /// </summary>
        /// <param name="setId">Set id.</param>
        public void DeleteDataSet(string setId)
        {
            try
            {
                if (DataHandler.Repository.Queryable.Where(p => p._EntityId == setId).Any())
                {
                    DataHandler.Repository.Delete(setId);
                }
 
                OnPropertyChanged(nameof(CurrentSet));
                OnPropertyChanged(nameof(ListOfDataSets));
            }
            catch (Exception)
            {

                //todo
            }



        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




    }

}
