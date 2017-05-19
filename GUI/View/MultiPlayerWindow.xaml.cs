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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MultiPlayerWindow.xaml
    /// </summary>
    public partial class MultiPlayerWindow : Window
    {
        private MultiPlayerViewModel mpVM;
        //private TcpClient client;

        public MultiPlayerWindow()
        {
            //request from the server list of available games
            SinglePlayerViewModel spvm = new SinglePlayerViewModel();
            TcpClient client = spvm.model.Connect();
            JArray jarray = spvm.model.GetListOfGames(client);
            Console.WriteLine(jarray);
            string[] stringsArray = jarray.ToObject<string[]>();
            List<string> stringsList = stringsArray.OfType<string>().ToList();

            //create mpvm
            mpVM = new MultiPlayerViewModel();
            /*client =*/ //mpVM.ConnectAndCommunicate();
            this.DataContext = mpVM;
            InitializeComponent();

            cboMazeNames.ItemsSource = stringsList;
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder startString = new StringBuilder();
            startString.Append("start " + mpVM.MazeNameDefinition + " " + mpVM.MazeRowsDefinition + " " + mpVM.MazeColsDefinition); 
            //mpVM.Model.GetMaze(client, generateString.ToString());

            mpVM.ConnectAndCommunicate(startString.ToString());

            MultiPlayerGameWindow mpGW = new MultiPlayerGameWindow();//MultiPlayerGameWindow(spVM.Model);
            mpGW.Show();
            this.Close();
        }

        private void JoinGameButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
