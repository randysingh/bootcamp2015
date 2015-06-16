using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Common.Interfaces
{
    /// <summary>
    /// Saves and retrieves simple key/value data.
    /// </summary>
    public interface ILocalDataService
    {
        string GetValue(string key);

        void SetValue(string key, string value);
    }
}
