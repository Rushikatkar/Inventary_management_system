using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Logging
{
    public interface ILogService
    {
        Task LogLowInventoryAsync(string message);
    }
}
