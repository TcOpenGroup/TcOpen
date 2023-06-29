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
    public partial class TcoMultiAxis
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
            try 
            {
                _savePositionTask.Read();
                SetId = _savePositionTask._identifier.Cyclic;
                Save();
                _savePositionTask._exchangeSuccessfuly.Cyclic = true;
                _savePositionTask.Write();

            }
            catch (Exception)
            {
                ;
            }
        }

        private void LoadFromPlc()
        {
            try
            {
                _loadPositionTask.Read();
                SetId = _loadPositionTask._identifier.Cyclic;
                if (RepositoryHandler.ListOfDataSets.Contains(SetId))
                {
                    Load();
                    _loadPositionTask._doesNotExist.Cyclic = false;
                    _loadPositionTask._exchangeSuccessfuly.Cyclic = true;
                }
                else
                    _loadPositionTask._doesNotExist.Cyclic = true;
                
                _loadPositionTask.Write();
            }
            catch (Exception)
            {

                ;
            }
         
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
                        PlainTcoMultiAxisMoveParam plain = new PlainTcoMultiAxisMoveParam();
                        item.FlushOnlineToPlain(plain);
                        RepositoryHandler.CurrentSet.Items.Add(new PositioningParamItem()
                        {
                            Key = item.Symbol,
                            Description = string.Empty,
                            MoveParam = plain,

                        });
                    }
                    else
                    {
                        var pos = RepositoryHandler.CurrentSet.Items.Where(p => p.Key == item.Symbol).FirstOrDefault();
                        if (pos != null)
                        {
                            PlainTcoMultiAxisMoveParam plain = new PlainTcoMultiAxisMoveParam();
                            item.FlushOnlineToPlain(plain);
                            pos.MoveParam = plain;
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


}


