using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Vortex.Presentation.Wpf;

namespace TcoData
{
    /// <summary>
    /// Interaction logic for fbDataExchangeControlView.xaml
    /// </summary>
    public partial class DataView : UserControl
    {
        public DataView()
        {
           
            InitializeComponent();
            //this.DataGrid.Columns.Add(new DataGridTextColumn() { Binding = new Binding() { Path = new PropertyPath("_EntityId") }, Header = "ID" });
        }

        private dynamic _context
        {
            get
            {
                return this.DataContext;
            }
        }

        private void ControlDataVisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
            if(EditData.Visibility == Visibility.Visible && _context != null && EditData.Content == null)
                EditData.Content = new RenderableContentControl() { DataContext = _context.DataExchange._data, PresentationType = "ShadowControlSlim-ShadowControl" };
            
        }

        private void DisplayDataVisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DisplayData.Visibility == Visibility.Visible && _context != null && DisplayData.Content == null)
                DisplayData.Content = new RenderableContentControl() { DataContext = _context.DataExchange._data, PresentationType = "ShadowDisplaySlim-ShadowControl" };
        }
     
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (this.Visibility == Visibility.Visible)
                {
                    _context?.FillObservableRecords();
                }
            }
            catch (Exception)
            {

                //++ Ignore
            }            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ResizableGrid.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Auto);
        }



        
        public ObservableCollection<DataGridColumn> DataListColumns
        {
            get => this.DataGrid.Columns;
            set
            {
                if (value.Count > 0)
                {
                    this.DataGrid.Columns.Clear();

                    foreach (var column in value)
                    {
                        this.DataGrid.Columns.Add(column);
                    }
                }
            }
        }

    }
}
