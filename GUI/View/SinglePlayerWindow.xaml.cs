using GUI.Model;
using GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for SinglePlayer.xaml
    /// </summary>
    public partial class SinglePlayerWindow : Window
    {
        public SinglePlayerViewModel spVM;
        private TcpClient client;
        public SinglePlayerWindow()
        {
            spVM = new SinglePlayerViewModel();
            this.DataContext = spVM;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            spVM.GenerateMaze();
            SinglePlayerGameWindow spGW = new SinglePlayerGameWindow(spVM.Model);
            spGW.Show();
            this.Close();
        }
    }
}
