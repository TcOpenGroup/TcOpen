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

namespace TcoDrivesBeckhoff
{
    public class TcoSingleAxisServiceViewModel : RenderableViewModel
    {
        private TcoSingleAxisMoveParam selectedItem;

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
            if (Component.RepositoryHandler != null)
                Component.Delete();
            else
                MessageBox.Show("Define Repository Handler first!");
        }

        private void CreateNewDataSet()
        {
            if (Component.RepositoryHandler != null)
                Component.CreateSet();
            else
                MessageBox.Show("Define Repository Handler first!");
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
            if (Component.RepositoryHandler != null)
                 Component.Save();
            else
                MessageBox.Show("Define Repository Handler first!");
       

        }

        private void LoadPositions()
        {
            if (Component.RepositoryHandler != null)
                Component.Load();
            else
                MessageBox.Show("Define Repository Handler first!");
        }

        private void MoveToPosition()
        {
            var answer = MessageBox.Show($"Are  you sure  move to  {this.SelectedItem.HumanReadable.ToUpper()}?", "MOVE TO POSITION", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                this.Component._axis._moveAbsoluteTask._position.Synchron       = SelectedItem.Position.Synchron;
                this.Component._axis._moveAbsoluteTask._velocity.Synchron       = SelectedItem.Velocity.Synchron;
                this.Component._axis._moveAbsoluteTask._acceleration.Synchron   = SelectedItem.Acceleration.Synchron;
                this.Component._axis._moveAbsoluteTask._deceleration.Synchron   = SelectedItem.Deceleration.Synchron;



                Component._axis._moveAbsoluteTask._invokeRequest.Synchron = true;
            }
        }

        private void TeachPosition()
        {
            var answer = MessageBox.Show($"Are you sure to change position: '{SelectedItem.HumanReadable.ToUpper()}?", "TEACH POSITION", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                this.SelectedItem.Position.Synchron = Math.Round(this.Component._axis._axisStatus.ActPos.Synchron, 3);
                //this.SelectedItem.Acceleration.Synchron = Math.Round(this.Component._axis._axisStatus.ActAcc.Synchron, 3);
                //this.SelectedItem.Deceleration.Synchron = Math.Round(this.Component._axis._axisStatus.ActAcc.Synchron, 3);
                //this.SelectedItem.Velocity.Synchron = Math.Round(this.Component._axis._axisStatus.ActVelo.Synchron, 3);

            }
        }

        public TcoSingleAxis Component { get; private set; }

        public TcoSingleAxisMoveParam SelectedItem
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

}
