using DemoApp.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DemoApp.Common.ViewModels
{
    public class JobViewModel
    {
        public Job Job { get; set; }

        /// <summary>
        /// Set this to a command that goes to the detail view.
        /// </summary>
        public ICommand Select { get; set;}
    }
}
