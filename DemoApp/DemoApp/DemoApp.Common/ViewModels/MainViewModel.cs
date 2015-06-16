using Caliburn.Micro;

namespace DemoApp.Common.ViewModels
{
    public class MainViewModel : Screen
    {
        public string Name { get; set; }
        public MainViewModel()
        {
            Name = "Hello from portable";
        }

    }
}
