using System.Reflection;
using Ninance_v2.Core;

namespace Ninance_v2.MVVM.ViewModel
{
    public class AboutViewModel : ObservableObject
    {
        public string Version
        {
            get {
                return "Version: v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public AboutViewModel()
        {
        }
    }
}
