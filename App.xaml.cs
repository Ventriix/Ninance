using Ninance_v2.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Ninance_v2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static TransactionHandler TransactionHandler;

        public App()
        {
            TransactionHandler = new TransactionHandler();
        }
    }
}
