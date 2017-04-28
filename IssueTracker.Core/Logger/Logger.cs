using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Core.Logger
{
    public class Logger
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public void Error(Exception ex)
        {
            _logger.Error(ex);
        }
        public void Error(string err)
        {
            _logger.Error(err);
        }
    }
}
