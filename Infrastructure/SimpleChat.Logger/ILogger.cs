namespace SimpleChat.Logger
{
    using System;

    public interface ILogger
    {
        void Error(string message);

        void Error(string message, Exception e);

        void Info(string message);

        void Warn(string message);
    }
}
