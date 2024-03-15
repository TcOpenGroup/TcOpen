using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Vortex.Connector;
using Vortex.Presentation.Wpf;
using RelayCommand = TcOpen.Inxton.Input.RelayCommand;

using TcOpen.Inxton.TcoDrivesBeckhoff.Wpf.Properties;

namespace TcoDrivesBeckhoff
{

    public class TcoSingleAxisServiceViewModel : RenderableViewModel
    {
        private TcoMultiAxisMoveParam selectedItem;

        public TcoSingleAxisServiceViewModel()
        {
            TeachPositionCommand = new RelayCommand(a => this.TeachPosition());
            MoveToPositionCommand = new RelayCommand(a => MoveToPosition());
            CreateNewDataSetCommand = new RelayCommand(a => this.CreateNewDataSet());
            DeleteDataSetCommand = new RelayCommand(a => this.DeleteDataSet());
            LoadPositionsCommand = new RelayCommand(a => this.LoadPositions());
            SavePositionsCommand = new RelayCommand(a => SavePositions());
            FillDefaultParamsCommand = new RelayCommand(a => FillDefaultParams());

        }

        private void DeleteDataSet()
        {
            if (string.IsNullOrEmpty(this.Component.SetId))
            {
                MessageBox.Show(strings.ResourceManager.GetString("SelectSetId"), strings.ResourceManager.GetString("Attention"), MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var answer = MessageBox.Show($"{strings.ResourceManager.GetString("AskDelete")}  {this.Component.SetId.ToUpper()}?", strings.ResourceManager.GetString("Attention"), MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                if (Component.RepositoryHandler != null)
                    Component.Delete();
                else
                    strings.ResourceManager.GetString("DefineRepository");
            }
        }

        private void CreateNewDataSet()
        {
            if (string.IsNullOrEmpty(this.Component.NewSetId))
            {
                MessageBox.Show(strings.ResourceManager.GetString("SetNewName"), strings.ResourceManager.GetString("Attention"), MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var answer = MessageBox.Show($"{strings.ResourceManager.GetString("AskCreate")}  {this.Component.NewSetId.ToUpper()}?", strings.ResourceManager.GetString("Attention"), MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                if (Component.RepositoryHandler != null)
                    Component.CreateSet();
                else
                    strings.ResourceManager.GetString("DefineRepository");
            }

        }

        public PlainTcoSingleAxisMoveParam DefaultValues { get; set; } = new PlainTcoSingleAxisMoveParam();

        private void FillDefaultParams()
        {
            foreach (var item in Component.Positions)
            {
                item.Axis1.Velocity.Cyclic = DefaultValues.Velocity;
                item.Axis1.Acceleration.Cyclic = DefaultValues.Acceleration;
                item.Axis1.Deceleration.Cyclic = DefaultValues.Deceleration;
                item.Axis1.Jerk.Cyclic = DefaultValues.Jerk;

            }

        }

        private void SavePositions()
        {
            if (string.IsNullOrEmpty(this.Component.SetId))
            {
                MessageBox.Show(strings.ResourceManager.GetString("SelectSetId"), strings.ResourceManager.GetString("Attention"), MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var answer = MessageBox.Show($"{strings.ResourceManager.GetString("AskSave")}  {this.Component.SetId.ToUpper()}?", strings.ResourceManager.GetString("Attention"), MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                if (Component.RepositoryHandler != null)
                    Component.Save();
                else
                    strings.ResourceManager.GetString("DefineRepository");
            }

        }

        private void LoadPositions()
        {
            if (string.IsNullOrEmpty(this.Component.SetId))
            {
                MessageBox.Show(strings.ResourceManager.GetString("SelectSetId"), strings.ResourceManager.GetString("Attention"), MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var answer = MessageBox.Show($"{strings.ResourceManager.GetString("AskLoad")}  {this.Component.SetId.ToUpper()}?", strings.ResourceManager.GetString("Attention"), MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                if (Component.RepositoryHandler != null)
                    Component.Load();
                else
                    strings.ResourceManager.GetString("DefineRepository");
            }
        }

        private void MoveToPosition()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show(strings.ResourceManager.GetString("SelectFirst"), strings.ResourceManager.GetString("Attention"), MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var answer = MessageBox.Show($"{strings.ResourceManager.GetString("AskMovePos")}  {this.SelectedItem.HumanReadable.ToUpper()}?", strings.ResourceManager.GetString("Attention"), MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)

            {
                //restore before movements if necessary
                Component._restoreTask._invokeRequest.Synchron = true;

                this.Component._axis._moveAbsoluteTask._position.Synchron       = SelectedItem.Axis1.Position.Synchron;
                this.Component._axis._moveAbsoluteTask._velocity.Synchron       = SelectedItem.Axis1.Velocity.Synchron;
                this.Component._axis._moveAbsoluteTask._acceleration.Synchron   = SelectedItem.Axis1.Acceleration.Synchron;
                this.Component._axis._moveAbsoluteTask._deceleration.Synchron   = SelectedItem.Axis1.Deceleration.Synchron;



                Component._axis._moveAbsoluteTask._invokeRequest.Synchron = true;
            }
        }

        private void TeachPosition()
        {
            var answer = MessageBox.Show($"{strings.ResourceManager.GetString("AskTeachPos")}'{SelectedItem.HumanReadable.ToUpper()}?", strings.ResourceManager.GetString("Attention"), MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                this.SelectedItem.Axis1.Position.Synchron = Math.Round(this.Component._axis._axisStatus.ActPos.Synchron, 3);
             

            }
        }

        public TcoSingleAxis Component { get; private set; }

        public TcoMultiAxisMoveParam SelectedItem
        {
            get => selectedItem; 
            set
            {
                if (selectedItem == value)
                {
                    return;
                }

                SetProperty(ref selectedItem, value);
            }
        }

        public override object Model { get => this.Component; set { this.Component = value as TcoSingleAxis; } }

        public RelayCommand TeachPositionCommand { get; private set; }
        public RelayCommand MoveToPositionCommand { get; private set; }
        public RelayCommand CreateNewDataSetCommand { get; private set; }
        public RelayCommand DeleteDataSetCommand { get; private set; }
        public RelayCommand LoadPositionsCommand { get; private set; }
        public RelayCommand SavePositionsCommand { get; private set; }
        public RelayCommand FillDefaultParamsCommand { get; private set; }
        public RelayCommand RefreshPositionsCommand { get; private set; }
    }

    public class TcoSingleAxisViewModel : TcoSingleAxisServiceViewModel
    { }
}
