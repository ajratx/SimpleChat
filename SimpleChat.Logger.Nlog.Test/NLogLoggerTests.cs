using System;
using Xunit;

namespace SimpleChat.Logger.Nlog.Test
{
    public class NLogLoggerTests
    {
        [Fact]
        public void Test1()
        {
            var logger = new NLogLogger();
            logger.Warn("Fuuck warn");
            logger.Error("Fuuck error");
        }
    }
}
