using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Business.Logger
{
    public class LoggerProvider : ILoggerProvider
    {
        public IWebHostEnvironment _hostingEnvironment;
        public LoggerProvider(IWebHostEnvironment hostingEnvironment) => _hostingEnvironment = hostingEnvironment;
        public ILogger CreateLogger(string categoryName) => new Logger(_hostingEnvironment);
        public void Dispose() => throw new NotImplementedException();
    }
}
