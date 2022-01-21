using Ninance_v2.Core;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace Ninance_v2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static TransactionHandler TransactionHandler;
        public static DatabaseHandler DatabaseHandler;
        public static ConfigHandler ConfigHandler;

        public App()
        {
            TransactionHandler = new TransactionHandler();
            DatabaseHandler = new DatabaseHandler();
            ConfigHandler = new ConfigHandler();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(
                    CultureInfo.CurrentCulture.IetfLanguageTag)));

            base.OnStartup(e);
        }
    }
}
