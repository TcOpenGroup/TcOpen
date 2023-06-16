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
    public class TcoMultiAxisServiceViewModel : RenderableViewModel
    {
        private TcoMultiAxisMoveParam selectedItem;

        public TcoMultiAxisServiceViewModel()
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
                item.Axis2.Velocity.Cyclic = DefaultValues.Velocity;
                item.Axis2.Acceleration.Cyclic = DefaultValues.Acceleration;
                item.Axis2.Deceleration.Cyclic = DefaultValues.Deceleration;
                item.Axis2.Jerk.Cyclic = DefaultValues.Jerk;
                item.Axis3.Velocity.Cyclic = DefaultValues.Velocity;
                item.Axis3.Acceleration.Cyclic = DefaultValues.Acceleration;
                item.Axis3.Deceleration.Cyclic = DefaultValues.Deceleration;
                item.Axis3.Jerk.Cyclic = DefaultValues.Jerk;
                item.Axis4.Velocity.Cyclic = DefaultValues.Velocity;
                item.Axis4.Acceleration.Cyclic = DefaultValues.Acceleration;
                item.Axis4.Deceleration.Cyclic = DefaultValues.Deceleration;
                item.Axis4.Jerk.Cyclic = DefaultValues.Jerk;

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
                this.Component._axis1._moveAbsoluteTask._position.Synchron       = SelectedItem.Axis1.Position.Synchron;
                this.Component._axis1._moveAbsoluteTask._velocity.Synchron       = SelectedItem.Axis1.Velocity.Synchron;
                this.Component._axis1._moveAbsoluteTask._acceleration.Synchron   = SelectedItem.Axis1.Acceleration.Synchron;
                this.Component._axis1._moveAbsoluteTask._deceleration.Synchron   = SelectedItem.Axis1.Deceleration.Synchron;



                Component._axis1._moveAbsoluteTask._invokeRequest.Synchron = true;

                this.Component._axis2._moveAbsoluteTask._position.Synchron = SelectedItem.Axis2.Position.Synchron;
                this.Component._axis2._moveAbsoluteTask._velocity.Synchron = SelectedItem.Axis2.Velocity.Synchron;
                this.Component._axis2._moveAbsoluteTask._acceleration.Synchron = SelectedItem.Axis2.Acceleration.Synchron;
                this.Component._axis2._moveAbsoluteTask._deceleration.Synchron = SelectedItem.Axis2.Deceleration.Synchron;



                Component._axis2._moveAbsoluteTask._invokeRequest.Synchron = true;

                this.Component._axis3._moveAbsoluteTask._position.Synchron = SelectedItem.Axis3.Position.Synchron;
                this.Component._axis3._moveAbsoluteTask._velocity.Synchron = SelectedItem.Axis3.Velocity.Synchron;
                this.Component._axis3._moveAbsoluteTask._acceleration.Synchron = SelectedItem.Axis3.Acceleration.Synchron;
                this.Component._axis3._moveAbsoluteTask._deceleration.Synchron = SelectedItem.Axis3.Deceleration.Synchron;



                Component._axis3._moveAbsoluteTask._invokeRequest.Synchron = true;

                this.Component._axis4._moveAbsoluteTask._position.Synchron = SelectedItem.Axis4.Position.Synchron;
                this.Component._axis4._moveAbsoluteTask._velocity.Synchron = SelectedItem.Axis4.Velocity.Synchron;
                this.Component._axis4._moveAbsoluteTask._acceleration.Synchron = SelectedItem.Axis4.Acceleration.Synchron;
                this.Component._axis4._moveAbsoluteTask._deceleration.Synchron = SelectedItem.Axis4.Deceleration.Synchron;



                Component._axis4._moveAbsoluteTask._invokeRequest.Synchron = true;
            }
        }

        private void TeachPosition()
        {
            var answer = MessageBox.Show($"Are you sure to change position: '{SelectedItem.HumanReadable.ToUpper()}?", "TEACH POSITION", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                this.SelectedItem.Axis1.Position.Synchron = Math.Round(this.Component._axis1._axisStatus.ActPos.Synchron, 3);
                this.SelectedItem.Axis2.Position.Synchron = Math.Round(this.Component._axis2._axisStatus.ActPos.Synchron, 3);
                this.SelectedItem.Axis3.Position.Synchron = Math.Round(this.Component._axis3._axisStatus.ActPos.Synchron, 3);
                this.SelectedItem.Axis4.Position.Synchron = Math.Round(this.Component._axis4._axisStatus.ActPos.Synchron, 3);
            }
        }

        public TcoMultiAxis Component { get; private set; }

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

        public override object Model { get => this.Component; set { this.Component = value as TcoMultiAxis; } }

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
