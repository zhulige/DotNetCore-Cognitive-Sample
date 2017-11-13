using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace CustomVision_Prediction_Sample
{
    public class LogHelper
    {

        private const string _logFormat = "{0:T}; Key; {1}; Detail; {2}";

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="level"></param>
        /// <param name="msg"></param>
        /// <param name="msgKey">自定义msgKey</param>
        /// <param name="moduleName">自定义模块</param>
        public static void WriteLine(LogLevel level ,string msg, string msgKey = null, string moduleName = "")
        {
            string w = DateTime.Now.ToString() + " " + msg + " " + msgKey;
            switch (level)
            {
                case LogLevel.Trace:
                    System.Console.WriteLine(w);
                    WriteLog(new LogInfo(level, msg, msgKey, moduleName));
                    break;
                case LogLevel.Info:
                    System.Console.WriteLine(w);
                    WriteLog(new LogInfo(level, msg, msgKey, moduleName));
                    break;
                case LogLevel.Warning:
                    System.Console.WriteLine(w);
                    WriteLog(new LogInfo(level, msg, msgKey, moduleName));
                    break;
                case LogLevel.Error:
                    System.Console.WriteLine(w);
                    WriteLog(new LogInfo(level, msg, msgKey, moduleName));
                    break;

            }
           
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="level"></param>
        /// <param name="msgObj">Object类型</param>
        /// <param name="msgKey">自定义msgKey</param>
        /// <param name="moduleName">自定义模块</param>
        public static void WriteLine(LogLevel level, object msgObj,string msgKey = null, string moduleName = "")
        {
            string w = DateTime.Now.ToString() + " " + Newtonsoft.Json.JsonConvert.SerializeObject(msgObj);
            System.Console.WriteLine(w);
            WriteLog(new LogInfo(level, msgObj, msgKey, moduleName));
        }


        private static string getLogFilePath(string module, LogLevel level)
        {
            string _logBaseDirPath = "logs";
            string dirPath = string.Format(@"{0}/{1}/{2}/", _logBaseDirPath, module, level);
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            return string.Concat(dirPath, DateTime.Now.ToString("yyyyMMddHH"), ".log");
        }

        private static object obj = new object();

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="info"></param>
        private static void WriteLog(LogInfo info)
        {
            lock (obj)
            {
                string filePath = getLogFilePath(info.ModuleName, info.Level);

                using (StreamWriter sw = new StreamWriter(new FileStream(filePath, FileMode.Append, FileAccess.Write), Encoding.UTF8))
                {
                    sw.WriteLine(format: _logFormat, arg0: DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), arg1: info.MsgKey, arg2: info.Msg);

                }
            }

        }


    }


    /// <summary>
    /// 日志实体
    /// </summary>
    public sealed class LogInfo
    {
        /// <summary>
        /// 空构造函数
        /// </summary>
        public LogInfo()
        {
        }

        /// <summary>
        /// 日志构造函数
        /// </summary>
        /// <param name="loglevel"></param>
        /// <param name="logMsg"></param>
        /// <param name="msgKey"></param>
        /// <param name="moduleName"></param>
        internal LogInfo(LogLevel loglevel, object logMsg, string msgKey = null, string moduleName = "")
        {
            Level = loglevel;
            ModuleName = moduleName;
            this.Msg = logMsg;
            MsgKey = msgKey;
        }

        /// <summary>
        /// 日志等级
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        ///   key值  可以是自定义的标识  
        ///   根据此字段可以处理当前module下不同复杂日志信息
        /// </summary>
        public string MsgKey { get; set; }

        /// <summary>
        /// 日志信息  可以是复杂类型  如 具体实体类
        /// </summary>
        public object Msg { get; set; }

        /// <summary>
        /// 编号（全局唯一）
        /// </summary>
        public string LogCode { get; set; }
    }

    /// <summary>
    /// 日志等级
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 跟踪查看
        /// </summary>
        Trace = 0,
        /// <summary>
        /// 信息
        /// </summary>
        Info = 1,

        /// <summary>
        /// 警告
        /// </summary>
        Warning = 2,

        /// <summary>
        /// 错误
        /// </summary>
        Error = 3,

    }


}
