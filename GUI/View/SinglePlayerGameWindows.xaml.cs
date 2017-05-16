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
    /// Interaction logic for SinglePlayerGameWindows.xaml
    /// </summary>
    public partial class SinglePlayerGameWindow : Window
    {
        private SinglePlayerGameViewModel spVM;
        private IClientModel Model;
        private TcpClient client;
        public SinglePlayerGameWindow(IClientModel model)
        {
            Model = model;
            spVM = new SinglePlayerGameViewModel(Model);
            DataContext = spVM;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           client = Model.Connect();

            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }

        private void MazeUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MyMazeBoard.Draw();
        }
    }
}
