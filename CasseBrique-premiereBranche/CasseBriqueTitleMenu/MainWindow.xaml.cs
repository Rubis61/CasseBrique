using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CasseBriqueTitleMenu
{
    public enum UserAction { None, PlaySolo, PlayMulti, Options, Exit };

    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public UserAction Response { get; set; }
        public Action playAction;

        public MainWindow(Action playSolo)
        {
            InitializeComponent();

            Response = UserAction.None;
            playAction += playSolo;
        }

        private void PlaySolo(object sender, RoutedEventArgs e)
        {
            Response = UserAction.PlaySolo;
            playAction();
        }
    }
}
