using GUI.ViewModel;
using GUI.View;
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
using System.Windows.Shapes;
using System.Net.Sockets;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MultiPlayerWindow.xaml
    /// </summary>
    public partial class MultiPlayerWindow : Window
    {
        private MultiPlayerViewModel mpVM;
        private TcpClient client;

        public MultiPlayerWindow()
        {
            mpVM = new MultiPlayerViewModel();
            client = mpVM.Model.Connect();
            this.DataContext = mpVM;
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder generateString = new StringBuilder();
            generateString.Append("generate " + mpVM.MazeNameDefinition + " " + mpVM.MazeRowsDefinition + " " + mpVM.MazeColsDefinition); 

            mpVM.Model.GetMaze(client, generateString.ToString());
            MultiPlayerGameWindow mpGW = new MultiPlayerGameWindow();//MultiPlayerGameWindow(spVM.Model);
            mpGW.Show();
            this.Close();
        }
    }
}
