namespace TaskBuilder.Helpers
{
    using System;

    public enum LogType
    {
        None = 0,
        Warning = 1,
        Error = 2
    }

    public class LoggerHelper
    {
        private Action<string, object[]> logError;
        private Action<string, object[]> logWarning;

        public LoggerHelper(Action<string, object[]> logError, Action<string, object[]> logWarning)
        {
            this.logError = logError;
            this.logWarning = logWarning;
        }

        public void Log(string msgToLog, int logType)
        {
            switch(logType)
            {
                case (int)LogType.Error:
                    logError(msgToLog, null);
                    break;
                case (int)LogType.Warning:
                    logWarning(msgToLog, null);
                    break;
                default:
                    break;
            }
        }
    }
}
