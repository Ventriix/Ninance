using Ninance_v2.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ninance_v2
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            PreviewKeyDown += new KeyEventHandler(HandleEscape);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void HandleEscape(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
