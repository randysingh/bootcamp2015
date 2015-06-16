using DemoApp.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DemoApp.Windows.Services
{
    public class WindowsLocalDataService : ILocalDataService
    {
        public string GetValue(string key)
        {
            var data = ApplicationData.Current.LocalSettings;

            if (data.Values.ContainsKey(key))
            {
                return data.Values[key].ToString();
            }

            return null;
        }

        public void SetValue(string key, string value)
        {
            var data = ApplicationData.Current.LocalSettings;

            data.Values[key] = value;
        }
    }
}
