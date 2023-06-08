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
            _loadPositionTask.InitializeExclusively(Load);
            _savePositionTask.InitializeExclusively(Save);

        }

        public ObservableCollection<TcoSingleAxisMoveParam> Positions { get { return Extensions.ToObservableCollection(((IVortexObject)_positions).GetChildren().OfType<TcoSingleAxisMoveParam>()); } }

     

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
                            PlainTcoSingleAxisMoveParam plain = new PlainTcoSingleAxisMoveParam();
                            item.FlushOnlineToPlain(plain);
                            pos.MoveParam = plain;
                        }
                    }
                }
                RepositoryHandler.SaveDataSet(AttributeName);
            }
            else
            {
                if (RepositoryHandler != null)
                {
                    RepositoryHandler.LoadDataSet(AttributeName);
                }
             
            }
        }

        public void Load()
        {
            if (RepositoryHandler != null)
            {
                RepositoryHandler.LoadDataSet(AttributeName);

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


