using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TcoRexrothPressConnector.SmartfunctionKit.RestApi;
using Vortex.Connector;

namespace TcoRexrothPress
{
    public partial class TcoSmartFunctionKit_v_4_x_x
    {
        public void InitializeTask()
        {
            this._getResultsTask.InitializeExclusively(GetResults);
            this._exportCurveTask.InitializeExclusively(SaveLast);
        }

        private void GetResults()
        {
            this.Read();

            var client = new Client(_config.IpAddress.Cyclic);

            var curve = client.GetLastCurveData();
            _results.createdDate.Cyclic = curve.createdDate;
            _results.customId.Cyclic = curve.customId;
            _results.cycleTime.Cyclic = Convert.ToSingle(curve.cycleTime);
            _results.dataRecordingDisabled.Cyclic = curve.dataRecordingDisabled;
            _results.id.Cyclic = curve.id;
            _results.maxForce.Cyclic = Convert.ToSingle(curve.maxForce);
            _results.maxPosition.Cyclic = Convert.ToSingle(curve.maxPosition);
            _results.samplingInterval.Cyclic = (short)curve.samplingInterval;
            _results.status.Cyclic = curve.status;
            _results.valid.Cyclic = curve.valid;
            _results.validationTime.Cyclic = (short)curve.validationTime;
            _results._v.Cyclic = (short)curve.__v;
            this.Write();
        }

        private string RemoveUnnecessary(string source)
        {
            string result = string.Empty;
            string regex =
                new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex reg = new Regex(string.Format("[{0}]", Regex.Escape(regex)));
            result = reg.Replace(source, "");
            return result;
        }

        private void SaveLast()
        {
            this.Read();

            var client = new Client(_config.IpAddress.LastValue);

            if (_config.CurveExportLocation.Cyclic == string.Empty)
            {
                throw new FileNotFoundException(@"Export location is not defined!");
            }
            var curve = client.GetLastCurveData();

            string dateTieme = DateTime.Now.ToString("yyyyMMddHHmmss");
            string fileName = String.Format(
                "{0}_{1}_{2}.json ",
                curve.customId,
                curve.id,
                dateTieme
            );
            string dirName = DateTime.Now.ToString("yyyyMMdd");
            Directory.CreateDirectory(Path.Combine(_config.CurveExportLocation.LastValue, dirName));
            string path = Path.Combine(
                _config.CurveExportLocation.Cyclic,
                dirName,
                RemoveUnnecessary(fileName)
            );

            try
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Converters.Add(new JavaScriptDateTimeConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;

                using (StreamWriter sw = new StreamWriter(path))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, curve);
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}
