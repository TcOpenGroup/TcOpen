using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using TcOpen.Inxton.TcoDrivesBeckhoff.Wpf.Properties;
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
            if (string.IsNullOrEmpty(this.Component.SetId))
            {
                MessageBox.Show(
                    strings.ResourceManager.GetString("SelectSetId"),
                    strings.ResourceManager.GetString("Attention"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
                return;
            }
            var answer = MessageBox.Show(
                $"{strings.ResourceManager.GetString("AskDelete")}  {this.Component.SetId.ToUpper()}?",
                strings.ResourceManager.GetString("Attention"),
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
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
                MessageBox.Show(
                    strings.ResourceManager.GetString("SetNewName"),
                    strings.ResourceManager.GetString("Attention"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
                return;
            }
            var answer = MessageBox.Show(
                $"{strings.ResourceManager.GetString("AskCreate")}  {this.Component.NewSetId.ToUpper()}?",
                strings.ResourceManager.GetString("Attention"),
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
            if (answer == MessageBoxResult.Yes)
            {
                if (Component.RepositoryHandler != null)
                    Component.CreateSet();
                else
                    strings.ResourceManager.GetString("DefineRepository");
            }
        }

        public PlainTcoSingleAxisMoveParam DefaultValues { get; set; } =
            new PlainTcoSingleAxisMoveParam();

        private void FillDefaultParams()
        {
            var AllUnchecked =
                !(bool)Axis1MoveAllowed
                && !(bool)Axis2MoveAllowed
                && !(bool)Axis3MoveAllowed
                && !(bool)Axis4MoveAllowed;
            foreach (var item in Component.Positions)
            {
                if ((bool)Axis1MoveAllowed || AllUnchecked)
                {
                    item.Axis1.Velocity.Cyclic = DefaultValues.Velocity;
                    item.Axis1.Acceleration.Cyclic = DefaultValues.Acceleration;
                    item.Axis1.Deceleration.Cyclic = DefaultValues.Deceleration;
                    item.Axis1.Jerk.Cyclic = DefaultValues.Jerk;
                }

                if ((bool)Axis2MoveAllowed || AllUnchecked)
                {
                    item.Axis2.Velocity.Cyclic = DefaultValues.Velocity;
                    item.Axis2.Acceleration.Cyclic = DefaultValues.Acceleration;
                    item.Axis2.Deceleration.Cyclic = DefaultValues.Deceleration;
                    item.Axis2.Jerk.Cyclic = DefaultValues.Jerk;
                }

                if ((bool)Axis3MoveAllowed || AllUnchecked)
                {
                    item.Axis3.Velocity.Cyclic = DefaultValues.Velocity;
                    item.Axis3.Acceleration.Cyclic = DefaultValues.Acceleration;
                    item.Axis3.Deceleration.Cyclic = DefaultValues.Deceleration;
                    item.Axis3.Jerk.Cyclic = DefaultValues.Jerk;
                }

                if ((bool)Axis4MoveAllowed || AllUnchecked)
                {
                    item.Axis4.Velocity.Cyclic = DefaultValues.Velocity;
                    item.Axis4.Acceleration.Cyclic = DefaultValues.Acceleration;
                    item.Axis4.Deceleration.Cyclic = DefaultValues.Deceleration;
                    item.Axis4.Jerk.Cyclic = DefaultValues.Jerk;
                }
            }
        }

        private void SavePositions()
        {
            if (string.IsNullOrEmpty(this.Component.SetId))
            {
                MessageBox.Show(
                    strings.ResourceManager.GetString("SelectSetId"),
                    strings.ResourceManager.GetString("Attention"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
                return;
            }
            var answer = MessageBox.Show(
                $"{strings.ResourceManager.GetString("AskSave")}  {this.Component.SetId.ToUpper()}?",
                strings.ResourceManager.GetString("Attention"),
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
            if (answer == MessageBoxResult.Yes)
            {
                if (Component.RepositoryHandler != null)
                {
                    Component.Save();
                    if (Component.ExportAfterSaving)
                    {
                        ExportPositions();
                    }
                }
                else
                    strings.ResourceManager.GetString("DefineRepository");
            }
        }

        private void ExportPositions()
        {
            if (string.IsNullOrEmpty(this.Component.SetId))
            {
                MessageBox.Show(
                    strings.ResourceManager.GetString("SelectSetId"),
                    strings.ResourceManager.GetString("Attention"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
                return;
            }

            if (Component.RepositoryHandler != null)
            {
                try
                {
                    var exports = Component.Export();
                    var sfd = new SaveFileDialog();
                    sfd.FileName = Component.SetId + DateTime.Now.ToString("_yyyyMMdd_HHmmss");
                    sfd.DefaultExt = "json";
                    sfd.ShowDialog();

                    using (var sw = new System.IO.StreamWriter(sfd.FileName))
                    {
                        sw.Write(exports);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
                strings.ResourceManager.GetString("DefineRepository");
        }

        private void LoadPositions()
        {
            if (string.IsNullOrEmpty(this.Component.SetId))
            {
                MessageBox.Show(
                    strings.ResourceManager.GetString("SelectSetId"),
                    strings.ResourceManager.GetString("Attention"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
                return;
            }
            var answer = MessageBox.Show(
                $"{strings.ResourceManager.GetString("AskLoad")}  {this.Component.SetId.ToUpper()}?",
                strings.ResourceManager.GetString("Attention"),
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
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
                MessageBox.Show(
                    strings.ResourceManager.GetString("SelectFirst"),
                    strings.ResourceManager.GetString("Attention"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
                return;
            }
            var answer = MessageBox.Show(
                $"{strings.ResourceManager.GetString("AskMovePos")}  {this.SelectedItem.HumanReadable.ToUpper()}?",
                strings.ResourceManager.GetString("Attention"),
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
            if (answer == MessageBoxResult.Yes)
            {
                //restore before movements if necessary
                Component._restoreTask._invokeRequest.Synchron = true;

                this.Component._axis1._moveAbsoluteTask._position.Synchron = SelectedItem
                    .Axis1
                    .Position
                    .Synchron;
                this.Component._axis1._moveAbsoluteTask._velocity.Synchron = SelectedItem
                    .Axis1
                    .Velocity
                    .Synchron;
                this.Component._axis1._moveAbsoluteTask._acceleration.Synchron = SelectedItem
                    .Axis1
                    .Acceleration
                    .Synchron;
                this.Component._axis1._moveAbsoluteTask._deceleration.Synchron = SelectedItem
                    .Axis1
                    .Deceleration
                    .Synchron;

                if ((bool)Axis1MoveAllowed)
                {
                    Component._axis1._moveAbsoluteTask._invokeRequest.Synchron = true;
                }

                this.Component._axis2._moveAbsoluteTask._position.Synchron = SelectedItem
                    .Axis2
                    .Position
                    .Synchron;
                this.Component._axis2._moveAbsoluteTask._velocity.Synchron = SelectedItem
                    .Axis2
                    .Velocity
                    .Synchron;
                this.Component._axis2._moveAbsoluteTask._acceleration.Synchron = SelectedItem
                    .Axis2
                    .Acceleration
                    .Synchron;
                this.Component._axis2._moveAbsoluteTask._deceleration.Synchron = SelectedItem
                    .Axis2
                    .Deceleration
                    .Synchron;

                if ((bool)Axis2MoveAllowed)
                {
                    Component._axis2._moveAbsoluteTask._invokeRequest.Synchron = true;
                }

                this.Component._axis3._moveAbsoluteTask._position.Synchron = SelectedItem
                    .Axis3
                    .Position
                    .Synchron;
                this.Component._axis3._moveAbsoluteTask._velocity.Synchron = SelectedItem
                    .Axis3
                    .Velocity
                    .Synchron;
                this.Component._axis3._moveAbsoluteTask._acceleration.Synchron = SelectedItem
                    .Axis3
                    .Acceleration
                    .Synchron;
                this.Component._axis3._moveAbsoluteTask._deceleration.Synchron = SelectedItem
                    .Axis3
                    .Deceleration
                    .Synchron;

                if ((bool)Axis3MoveAllowed)
                {
                    Component._axis3._moveAbsoluteTask._invokeRequest.Synchron = true;
                }

                this.Component._axis4._moveAbsoluteTask._position.Synchron = SelectedItem
                    .Axis4
                    .Position
                    .Synchron;
                this.Component._axis4._moveAbsoluteTask._velocity.Synchron = SelectedItem
                    .Axis4
                    .Velocity
                    .Synchron;
                this.Component._axis4._moveAbsoluteTask._acceleration.Synchron = SelectedItem
                    .Axis4
                    .Acceleration
                    .Synchron;
                this.Component._axis4._moveAbsoluteTask._deceleration.Synchron = SelectedItem
                    .Axis4
                    .Deceleration
                    .Synchron;

                if ((bool)Axis4MoveAllowed)
                {
                    Component._axis4._moveAbsoluteTask._invokeRequest.Synchron = true;
                }
            }
        }

        private void TeachPosition()
        {
            var answer = MessageBox.Show(
                $"{strings.ResourceManager.GetString("AskTeachPos")}'{SelectedItem.HumanReadable.ToUpper()}?",
                strings.ResourceManager.GetString("Attention"),
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
            if (answer == MessageBoxResult.Yes)
            {
                this.SelectedItem.Axis1.Position.Synchron = Math.Round(
                    this.Component._axis1._axisStatus.ActPos.Synchron,
                    3
                );
                this.SelectedItem.Axis2.Position.Synchron = Math.Round(
                    this.Component._axis2._axisStatus.ActPos.Synchron,
                    3
                );
                this.SelectedItem.Axis3.Position.Synchron = Math.Round(
                    this.Component._axis3._axisStatus.ActPos.Synchron,
                    3
                );
                this.SelectedItem.Axis4.Position.Synchron = Math.Round(
                    this.Component._axis4._axisStatus.ActPos.Synchron,
                    3
                );
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

        public override object Model
        {
            get => this.Component;
            set { this.Component = value as TcoMultiAxis; }
        }

        public RelayCommand TeachPositionCommand { get; private set; }
        public RelayCommand MoveToPositionCommand { get; private set; }
        public RelayCommand CreateNewDataSetCommand { get; private set; }
        public RelayCommand DeleteDataSetCommand { get; private set; }
        public RelayCommand LoadPositionsCommand { get; private set; }
        public RelayCommand SavePositionsCommand { get; private set; }
        public RelayCommand FillDefaultParamsCommand { get; private set; }
        public RelayCommand RefreshPositionsCommand { get; private set; }

        private bool axis1MoveAllowed;
        public bool Axis1MoveAllowed
        {
            get => axis1MoveAllowed;
            set => SetProperty(ref axis1MoveAllowed, value);
        }

        private bool axis2MoveAllowed;
        public bool Axis2MoveAllowed
        {
            get => axis2MoveAllowed;
            set => SetProperty(ref axis2MoveAllowed, value);
        }

        private bool axis3MoveAllowed;
        public bool Axis3MoveAllowed
        {
            get => axis3MoveAllowed;
            set => SetProperty(ref axis3MoveAllowed, value);
        }

        private bool axis4MoveAllowed;
        public bool Axis4MoveAllowed
        {
            get => axis4MoveAllowed;
            set => SetProperty(ref axis4MoveAllowed, value);
        }
    }

    public class TcoMultiAxisViewModel : TcoMultiAxisServiceViewModel { }
}
