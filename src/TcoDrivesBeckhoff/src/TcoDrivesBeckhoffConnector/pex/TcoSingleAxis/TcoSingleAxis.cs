using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TcoCore;
using TcOpen.Inxton.RepositoryDataSet;
using TcOpen.Inxton.Swift;
using Vortex.Connector;

namespace TcoDrivesBeckhoff
{
    public partial class TcoSingleAxis
    {
        private RepositoryHandler repoHandler;

        public RepositoryHandler RepositoryHandler
        {
            get => repoHandler;
        }

        public void InitializeRemoteDataExchange(
            RepositoryDataSetHandler<PositioningParamItem> handler
        )
        {
            repoHandler = new RepositoryHandler(handler);
            _loadPositionTask.InitializeExclusively(LoadFromPlc);
            _savePositionTask.InitializeExclusively(SaveFromPlc);
        }

        public void Initialize(RepositoryDataSetHandler<PositioningParamItem> handler)
        {
            repoHandler = new RepositoryHandler(handler);
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

        public ObservableCollection<TcoMultiAxisMoveParam> Positions
        {
            get
            {
                return Extensions.ToObservableCollection(
                    ((IVortexObject)_positions).GetChildren().OfType<TcoMultiAxisMoveParam>()
                );
            }
        }

        public string SetId { get; set; }
        public string NewSetId { get; set; }
        public bool ExportAfterSaving { get; set; } = true;

        public void Save()
        {
            if (RepositoryHandler != null)
            {
                RepositoryHandler.LoadDataSet(SetId);

                foreach (var item in Positions)
                {
                    var any = RepositoryHandler
                        .CurrentSet.Items.Where(p => p.Key == item.Symbol)
                        .Any();
                    if (!any)
                    {
                        PlainTcoSingleAxisMoveParam plain = new PlainTcoSingleAxisMoveParam();
                        item.Axis1.FlushOnlineToPlain(plain);
                        RepositoryHandler.CurrentSet.Items.Add(
                            new PositioningParamItem()
                            {
                                Key = item.Symbol,
                                Description = string.Empty,
                                MoveParam = new PlainTcoMultiAxisMoveParam() { Axis1 = plain },
                            }
                        );
                    }
                    else
                    {
                        var pos = RepositoryHandler
                            .CurrentSet.Items.Where(p => p.Key == item.Symbol)
                            .FirstOrDefault();
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

        public string Export()
        {
            string jsonString = string.Empty;
            if (RepositoryHandler != null)
            {
                var pos = RepositoryHandler.CurrentSet.Items;

                jsonString = JsonConvert.SerializeObject(pos);
            }
            return jsonString;
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
            if (RepositoryHandler != null && !string.IsNullOrEmpty(NewSetId))
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
                    var pos = RepositoryHandler
                        .CurrentSet.Items.Where(p => p.Key == item.Symbol)
                        .FirstOrDefault();
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
