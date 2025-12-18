using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PaeLibGeneral
{
    /// <summary>
    /// 日誌紀錄器類別(靜態)
    /// </summary>
    /// <example>
    /// <code>
    /// JLogger.LogDebug("MainFlow", "I'm doing somthing...");
    /// </code>
    /// </example>
    public static class JLogger
    {
        /// <summary>
        /// 預設的 Log 檔路徑，預設使用 "執行檔路徑\App_Data\Log\" 為檔案路徑
        /// </summary>
        public static string DefaultFilePath { get; set; }

        private static void LogEx(string loggerName, NLog.LogLevel level, string message,
            bool fileNameWithDate = true, string fileName = "", string filePath = "")
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrWhiteSpace(filePath))
            {
                if (string.IsNullOrEmpty(DefaultFilePath) || string.IsNullOrWhiteSpace(DefaultFilePath))
                {
                    //若未指定 Log 檔路徑，且未設定預設路徑，則預設使用 "執行檔路徑\App_Data\Log\" 為檔案路徑
                    filePath = string.Format("{0}\\App_Data\\Log\\", System.AppDomain.CurrentDomain.BaseDirectory);
                }
                else
                {
                    //若未指定 Log 檔路徑，但有設定預設路徑，則使用預設路徑。
                    filePath = DefaultFilePath;
                }
            }

            if (string.IsNullOrEmpty(fileName) || string.IsNullOrWhiteSpace(fileName))
            {
                if (string.IsNullOrEmpty(loggerName) || string.IsNullOrWhiteSpace(loggerName))
                {
                    //若未指定 Log 檔檔案名稱，且也未指定 Logger 名稱，則預設使用 "YYYYMMDD.log" 為檔名
                    fileName = string.Format("{0}.log", DateTime.Now.ToString("yyyyMMdd"));
                }
                else
                {
                    //若未指定 Log 檔檔案名稱，則預設使用 "LoggerName.log" 為檔名
                    fileName = string.Format("{0}.log", loggerName);
                }
            }

            if (fileNameWithDate)
            {
                //於檔名後方加入日期
                string fn = Path.GetFileNameWithoutExtension(fileName);
                string en = Path.GetExtension(fileName);
                fileName = string.Format("{0}_{1}{2}", fn, DateTime.Now.ToString("yyyyMMdd"), en);
            }

            // Step 1. Create configuration object
            LoggingConfiguration config = new LoggingConfiguration();

            // Step 2. Create targets and add them to the configuration
            FileTarget fileTarget = new FileTarget();
            fileTarget.KeepFileOpen = true;
            config.AddTarget("file", fileTarget);

            // Step 3. Set target properties
            //設定Log檔案路徑(完整路徑)
            fileTarget.FileName = Path.Combine(filePath, fileName);
            //內容格式：YYYY-MM-DD HH:MM:SS | level | logger name | message \r\n(換行)
            fileTarget.Layout = "${longdate} | ${level:uppercase=true} | ${logger} | ${message} ${newline}";
            //Log檔內容編碼格式：UTF8
            fileTarget.Encoding = Encoding.UTF8;
            //Log檔案數量限制：30個 (同檔名)
            fileTarget.MaxArchiveFiles = 30;
            //Log檔超過指定大小時，分割檔的明明方式：序列
            fileTarget.ArchiveNumbering = ArchiveNumberingMode.Sequence;
            //Log檔案大小限制：1MB
            fileTarget.ArchiveAboveSize = 1048576000;
            //Log檔超過指定大小時，分割檔的檔名格式
            fileTarget.ArchiveFileName = "Path.Combine(filePath, fileName){#######}";
            // don’t clutter the hard drive
            //fileTarget.DeleteOldFileOnStartup = true;

            // Step 4. Define rules
            LoggingRule rule = new LoggingRule("*", level, fileTarget);
            config.LoggingRules.Add(rule);

            // Step 5. Activate the configuration
            LogManager.Configuration = config;

            // Create logger
            Logger logger = LogManager.GetCurrentClassLogger();

            // Set Log Event
            LogEventInfo LogEvent = new LogEventInfo(level, loggerName, message);

            // Write to file
            logger.Log(LogEvent);
        }

        /// <summary>
        /// 紀錄 TRACE Level 的 Log
        /// </summary>
        /// <param name="loggerName">Log的要求者，例如模組、類別名稱等</param>
        /// <param name="message">Log訊息內容，預設格式為 "日期 時間 | TRACE | loggerName | message"</param>
        /// <param name="fileNameWithDate">Log檔名後方是否增加日期 "_YYYYMMDD"</param>
        /// <param name="fileName">Log指定檔名，預設檔名為 "loggerName.log"</param>
        /// <param name="filePath">Log指定路徑，預設路徑為 "執行檔路徑\App_Data\Log\"</param>
        public static void LogTrace(string loggerName, string message,
            bool fileNameWithDate = true, string fileName = "", string filePath = "")
        {
            LogEx(loggerName, LogLevel.Trace, message, fileNameWithDate, fileName, filePath);
        }

        /// <summary>
        /// 紀錄 DEBUG Level 的 Log
        /// </summary>
        /// <param name="loggerName">Log的要求者，例如模組、類別名稱等</param>
        /// <param name="message">Log訊息內容，預設格式為 "日期 時間 | DEBUG | loggerName | message"</param>
        /// <param name="fileNameWithDate">Log檔名後方是否增加日期 "_YYYYMMDD"</param>
        /// <param name="fileName">Log指定檔名，預設檔名為 "loggerName.log"</param>
        /// <param name="filePath">Log指定路徑，預設路徑為 "執行檔路徑\App_Data\Log\"</param>
        public static void LogDebug(string loggerName, string message,
            bool fileNameWithDate = true, string fileName = "", string filePath = "")
        {
            LogEx(loggerName, LogLevel.Debug, message, fileNameWithDate, fileName, filePath);
        }

        /// <summary>
        /// 紀錄 INFO Level 的 Log
        /// </summary>
        /// <param name="loggerName">Log的要求者，例如模組、類別名稱等</param>
        /// <param name="message">Log訊息內容，預設格式為 "日期 時間 | INFO | loggerName | message"</param>
        /// <param name="fileNameWithDate">Log檔名後方是否增加日期 "_YYYYMMDD"</param>
        /// <param name="fileName">Log指定檔名，預設檔名為 "loggerName.log"</param>
        /// <param name="filePath">Log指定路徑，預設路徑為 "執行檔路徑\App_Data\Log\"</param>
        public static void LogInfo(string loggerName, string message,
            bool fileNameWithDate = true, string fileName = "", string filePath = "")
        {
            LogEx(loggerName, LogLevel.Info, message, fileNameWithDate, fileName, filePath);
        }

        /// <summary>
        /// 紀錄 WARN Level 的 Log
        /// </summary>
        /// <param name="loggerName">Log的要求者，例如模組、類別名稱等</param>
        /// <param name="message">Log訊息內容，預設格式為 "日期 時間 | WARN | loggerName | message"</param>
        /// <param name="fileNameWithDate">Log檔名後方是否增加日期 "_YYYYMMDD"</param>
        /// <param name="fileName">Log指定檔名，預設檔名為 "loggerName.log"</param>
        /// <param name="filePath">Log指定路徑，預設路徑為 "執行檔路徑\App_Data\Log\"</param>
        public static void LogWarn(string loggerName, string message,
            bool fileNameWithDate = true, string fileName = "", string filePath = "")
        {
            LogEx(loggerName, LogLevel.Warn, message, fileNameWithDate, fileName, filePath);
        }

        /// <summary>
        /// 紀錄 ERROR Level 的 Log
        /// </summary>
        /// <param name="loggerName">Log的要求者，例如模組、類別名稱等</param>
        /// <param name="message">Log訊息內容，預設格式為 "日期 時間 | ERROR | loggerName | message"</param>
        /// <param name="fileNameWithDate">Log檔名後方是否增加日期 "_YYYYMMDD"</param>
        /// <param name="fileName">Log指定檔名，預設檔名為 "loggerName.log"</param>
        /// <param name="filePath">Log指定路徑，預設路徑為 "執行檔路徑\App_Data\Log\"</param>
        public static void LogError(string loggerName, string message,
            bool fileNameWithDate = true, string fileName = "", string filePath = "")
        {
            LogEx(loggerName, LogLevel.Error, message, fileNameWithDate, fileName, filePath);
        }

        /// <summary>
        /// 紀錄 FATAL Level 的 Log
        /// </summary>
        /// <param name="loggerName">Log的要求者，例如模組、類別名稱等</param>
        /// <param name="message">Log訊息內容，預設格式為 "日期 時間 | FATAL | loggerName | message"</param>
        /// <param name="fileNameWithDate">Log檔名後方是否增加日期 "_YYYYMMDD"</param>
        /// <param name="fileName">Log指定檔名，預設檔名為 "loggerName.log"</param>
        /// <param name="filePath">Log指定路徑，預設路徑為 "執行檔路徑\App_Data\Log\"</param>
        public static void LogFatal(string loggerName, string message,
            bool fileNameWithDate = true, string fileName = "", string filePath = "")
        {
            LogEx(loggerName, LogLevel.Fatal, message, fileNameWithDate, fileName, filePath);
        }
    }
}