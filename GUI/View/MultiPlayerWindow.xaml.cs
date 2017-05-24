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
using System.Threading;
using GUI.Model;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MultiPlayerWindow.xaml
    /// </summary>
    public partial class MultiPlayerWindow : Window
    {
        private MultiPlayerViewModel mpVM;
        private string[] namesOfAvailableGames;
        bool stop = false;

        public MultiPlayerWindow()
        {
            //create mpvm
            mpVM = new MultiPlayerViewModel();
            //subscribe to communicationProblems event
            mpVM.Model.CommunicationProblemEvent += NotifyAboutCommunicationProblem;
            mpVM.Rows = Properties.Settings.Default.MazeRows;
            mpVM.Cols = Properties.Settings.Default.MazeCols;
            this.DataContext = mpVM;

            InitializeComponent();
            
            cboMazeNames.DropDownOpened += UpdateComboBox;
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            //send command "start <maze_name> <rows> <cols>" to the server, wait to opponent and start a game in multiplayerGame Window.
            StringBuilder startString = new StringBuilder();
            startString.Append("start " + mpVM.MazeNameDefinition + " " + mpVM.MazeRowsDefinition + " " + mpVM.MazeColsDefinition);
            mpVM.ConnectAndCommunicate(startString.ToString());
            //this.waitng_indication.Visibility = Visibility.Visible;
           PassToGameWindow();
        }

        public void PassToGameWindow()
        {
            //wait to the server response
            while (!mpVM.IsReady)
            {
                if (stop)
                    return;
                Thread.Sleep(200);
            }

            //open game window
            MultiPlayerGameWindow mpGW = new MultiPlayerGameWindow(mpVM);
            mpVM.MultiplayerGameWindow = mpGW;
            mpGW.Show();
            this.Close();
        }

        private void JoinGameButton_Click(object sender, RoutedEventArgs e)
        {
            //send command "join <maze_name>" to the server and open multiplayerGame Window to join the game.
            mpVM.SelectedGame = namesOfAvailableGames[cboMazeNames.SelectedIndex];
            string joinCommand = "join " + mpVM.SelectedGame;
            mpVM.ConnectAndCommunicate(joinCommand);
            PassToGameWindow();
        }

        //it will will be executed when CommunicationProblemEvent takes place
        private void NotifyAboutCommunicationProblem()
        {
            stop = true;
            MessageBox.Show("We didn't succeed to connect to the server",
                "Info", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            Dispatcher.Invoke(() =>
            {
                Application.Current.Shutdown();
            });
        }

        private void UpdateComboBox(Object sender, EventArgs e)
        {
            //request from the server list of available games
            SingleClientModel model = new SingleClientModel();
            TcpClient client = model.Connect();
            string solve = (model.Communicate(client, "list"));
            var array = JArray.Parse(solve);
            JArray jarray = array;
            namesOfAvailableGames = jarray.ToObject<string[]>();
            List<string> stringsList = namesOfAvailableGames.OfType<string>().ToList();

            //add names of available games to the combo-box
            cboMazeNames.ItemsSource = stringsList;
        }
    }
}
