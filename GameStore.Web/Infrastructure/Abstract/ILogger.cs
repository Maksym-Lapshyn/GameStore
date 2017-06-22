using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Web.Infrastructure.Abstract
{
    public interface ILogger
    {
        void Debug(string message);

        void Info(string message);

        void Error(Exception x);
    }
}
