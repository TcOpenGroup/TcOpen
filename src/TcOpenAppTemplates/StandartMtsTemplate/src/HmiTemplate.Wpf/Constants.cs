using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmiProjectTemplate.Wpf
{
    public class Constants
    {
        // PLC
        public const DeployMode DEPLOY_MODE = DeployMode.Local;
        public static string PLC_AMS_ID = Environment.GetEnvironmentVariable("Tc3Target");
        // DB
        public const string CONNECTION_STRING_DB = DEPLOY_MODE==DeployMode.Plc ? PRODUCTION_CONNECTION_STRING_DB : LOCAL_CONNECTION_STRING_DB;

        public const string PRODUCTION_CONNECTION_STRING_DB = @"mongodb://mts:servis@localhost:27017";
        public const string LOCAL_CONNECTION_STRING_DB = @"mongodb://localhost:27017";//@"mongodb://mts:servis@localhost:27017";        
        public const string DB_NAME = "tcomtstemplate";
        public const string MONGODB_PATH = @"C:\Program Files\MongoDB\Server\5.0\bin\mongod.exe";
        public const string MONGODB_ARGS = "--dbpath D:\\DATA\\DB\\ --bind_ip_all";
        public const bool MONGODB_RUN = true;

        // USER
        public const string AUTOLOGIN_USERNAME = "default";
        public const string AUTOLOGIN_USERPASS = "";

        public const string DOCU_PATH = @"c:\MTS\Develop\vts\vortex.mts.documentation\";
        public const bool DOCU_AUTO_GENERATE_BLANK = true;
    }

    public enum DeployMode
    {
        Local = 1,
        Dummy = 2,
        Plc = 3
    }
}
