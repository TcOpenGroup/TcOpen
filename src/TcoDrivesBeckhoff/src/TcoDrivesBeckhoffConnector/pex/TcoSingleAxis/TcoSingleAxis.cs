using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Swift;
using Vortex.Connector;
using TcoCore;
using System.Text.RegularExpressions;
using TcOpen.Inxton.RepositoryDataSet;
using System.Collections.ObjectModel;

namespace TcoDrivesBeckhoff
{
    public partial class TcoSingleAxis
    {
       
    
        private  RepositoryHandler repoHandler;

        public   RepositoryHandler RepositoryHandler { get => repoHandler; }



        public void InitializeRemoteDataExchange(RepositoryDataSetHandler<PositioningParamItem> handler)
        {
            repoHandler = new RepositoryHandler(handler);
            _loadPositionTask.InitializeExclusively(LoadFromPlc);
            _savePositionTask.InitializeExclusively(SaveFromPlc);

        }

        private void SaveFromPlc()
        {
            _savePositionTask.Read();
            SetId = _savePositionTask._identifier.Cyclic;
            Save();
        }

        private void LoadFromPlc()
        {
            _loadPositionTask.Read();
            SetId = _loadPositionTask._identifier.Cyclic;
            Load();
        }

        public ObservableCollection<TcoMultiAxisMoveParam> Positions { get { return Extensions.ToObservableCollection(((IVortexObject)_positions).GetChildren().OfType<TcoMultiAxisMoveParam>()); } }

        public string SetId { get;  set; }
        public string NewSetId { get; set; }

    

        public void Save()
        {
            if (RepositoryHandler != null)
            {
                foreach (var item in Positions)
                {
                    var any = RepositoryHandler.CurrentSet.Items.Where(p => p.Key == item.Symbol).Any();
                    if (!any)
                    {
                        PlainTcoSingleAxisMoveParam plain = new PlainTcoSingleAxisMoveParam();
                        item.Axis1.FlushOnlineToPlain(plain);
                        RepositoryHandler.CurrentSet.Items.Add(new PositioningParamItem()
                        {
                            Key = item.Symbol,
                            Description = string.Empty,
                            MoveParam = new PlainTcoMultiAxisMoveParam() { Axis1 = plain},

                        });
                    }
                    else
                    {
                        var pos = RepositoryHandler.CurrentSet.Items.Where(p => p.Key == item.Symbol).FirstOrDefault();
                        if (pos != null)
                        {
                            PlainTcoSingleAxisMoveParam plain = new PlainTcoSingleAxisMoveParam();
                            item.Axis1.FlushOnlineToPlain(plain);
                            pos.MoveParam = new PlainTcoMultiAxisMoveParam() { Axis1 = plain };
                        }
                    }
                }
                RepositoryHandler.SaveDataSet(SetId);
            }
        
        }

        public void Delete()
        {
            if (RepositoryHandler != null)
            {

                RepositoryHandler.DeleteDataSet(SetId);
            }

        }

        public void CreateSet()
        {
            if (RepositoryHandler != null &&  !string.IsNullOrEmpty(NewSetId))
            {
                if (!RepositoryHandler.ListOfDataSets.Contains(NewSetId))
                {
                    RepositoryHandler.SaveDataSet(NewSetId);
                    SetId = NewSetId;
                }
            }
        }

        public void Load()
        {
            if (RepositoryHandler != null)
            {
                RepositoryHandler.LoadDataSet(SetId);

                foreach (var item in Positions)
                {
               
                    var pos = RepositoryHandler.CurrentSet.Items.Where(p => p.Key == item.Symbol).FirstOrDefault();
                    if (pos != null)
                    {
                          
                        item.FlushPlainToOnline(pos.MoveParam);
                        
                    }
                    
                }
            }
          
        }
    }


    public static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> col)
        {
            return new ObservableCollection<T>(col);
        }
    }
}


