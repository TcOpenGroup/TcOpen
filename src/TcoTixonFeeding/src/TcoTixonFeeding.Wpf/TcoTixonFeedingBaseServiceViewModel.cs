using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using Newtonsoft.Json;
using TcoCore;
using TcOpen.Inxton.Input;
using Vortex.Presentation.Wpf;
using RelayCommand = TcOpen.Inxton.Input.RelayCommand;

namespace TcoTixonFeeding
{
    public interface IHasConfigObject
    {
        dynamic _config { get; set; }
    }

    public abstract class TcoTixonFeedingBaseServiceViewModel<T, U> : RenderableViewModel
        where T : TcoCore.TcoComponent, new()
        where U : class
    {
        public TcoTixonFeedingBaseServiceViewModel()
            : base()
        {
            Component = new T();
            ExportConfigCommand = new RelayCommand(a => ExportParams());
            ImportConfigCommand = new RelayCommand(a => ImportParams());
        }

        public T Component { get; internal set; }
        public RelayCommand ExportConfigCommand { get; private set; }
        public RelayCommand ImportConfigCommand { get; private set; }

        public override object Model
        {
            get => Component;
            set { Component = value as T; }
        }

        private void ExportParams()
        {
            try
            {
                string jsonString = string.Empty;

                var cloned = this.Component.GetPlainFromOnline();
                var config = cloned.GetType().GetProperty("_config").GetValue(cloned);

                jsonString = JsonConvert.SerializeObject(config, Formatting.Indented);

                var sfd = new SaveFileDialog();
                sfd.FileName = Component.AttributeName + DateTime.Now.ToString("_yyyyMMdd_HHmmss");
                sfd.DefaultExt = "json";
                sfd.ShowDialog();

                using (var sw = new System.IO.StreamWriter(sfd.FileName))
                {
                    sw.Write(jsonString);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ImportParams()
        {
            string jsonString = string.Empty;
            try
            {
                var ofd = new OpenFileDialog();
                ofd.Filter = "Json Files (.json)|*.json";
                var result = ofd.ShowDialog();
                if (result == true)
                {
                    // Get the path of specified file
                    string filePath = ofd.FileName;

                    // Read the contents of the file
                    jsonString = File.ReadAllText(filePath);

                    U deserialized = JsonConvert.DeserializeObject<U>(jsonString);

                    var plainer = this.Component.GetPlainFromOnline();
                    plainer.GetType().GetProperty("_config").SetValue(plainer, deserialized);

                    this.Component.FlushPlainToOnline((dynamic)plainer);
                }
            }
            catch (JsonReaderException ex)
            {
                MessageBox.Show("Invalid JSON format: " + ex.Message);
            }
            catch (JsonSerializationException ex)
            {
                MessageBox.Show("JSON deserialization error: " + ex.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
