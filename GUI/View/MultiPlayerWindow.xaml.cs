// ***********************************************************************
// Assembly         : GUI
// Author           : ohad
// Created          : 05-15-2017
//
// Last Modified By : ohad
// Last Modified On : 05-24-2017
// ***********************************************************************
// <copyright file="MultiPlayerWindow.xaml.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
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
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MultiPlayerWindow : Window
    {
        /// <summary>
        /// The mp vm
        /// </summary>
        private MultiPlayerViewModel mpVM;
        /// <summary>
        /// The names of available games
        /// </summary>
        private string[] namesOfAvailableGames;
        /// <summary>
        /// The stop
        /// </summary>
        bool stop = false;
        /// <summary>
        /// The wa
        /// </summary>
        private WaitingAlertWindow wa;
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerWindow"/> class.
        /// </summary>
        public MultiPlayerWindow()
        {
            //create mpvm
            mpVM = new MultiPlayerViewModel();
            //subscribe to communicationProblems event
            mpVM.Model.CommunicationProblemEvent += NotifyAboutCommunicationProblem;
            mpVM.Rows = Properties.Settings.Default.MazeRows;
            mpVM.Cols = Properties.Settings.Default.MazeCols;
            this.DataContext = mpVM;
            wa = new WaitingAlertWindow();
            InitializeComponent();
            
            cboMazeNames.DropDownOpened += UpdateComboBox;
        }

        /// <summary>
        /// Handles the Click event of the StartGameButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            wa.Show();
            //send command "start <maze_name> <rows> <cols>" to the server, wait to opponent and start a game in multiplayerGame Window.
            StringBuilder startString = new StringBuilder();
            startString.Append("start " + mpVM.MazeNameDefinition + " " + mpVM.MazeRowsDefinition + " " + mpVM.MazeColsDefinition);
            mpVM.ConnectAndCommunicate(startString.ToString());
            //this.waitng_indication.Visibility = Visibility.Visible;
           PassToGameWindow();
        }

        /// <summary>
        /// Passes to game window.
        /// </summary>
        public void PassToGameWindow()
        {
            //wait to the server response
            while (!mpVM.IsReady)
            {
                if (stop)
                    return;
                Thread.Sleep(200);
            }
            wa.Close();
            //open game window
            MultiPlayerGameWindow mpGW = new MultiPlayerGameWindow(mpVM);
            mpVM.MultiplayerGameWindow = mpGW;
            mpGW.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the JoinGameButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void JoinGameButton_Click(object sender, RoutedEventArgs e)
        {
            //send command "join <maze_name>" to the server and open multiplayerGame Window to join the game.
            mpVM.SelectedGame = namesOfAvailableGames[cboMazeNames.SelectedIndex];
            string joinCommand = "join " + mpVM.SelectedGame;
            mpVM.ConnectAndCommunicate(joinCommand);
            PassToGameWindow();
        }

        //it will will be executed when CommunicationProblemEvent takes place
        /// <summary>
        /// Notifies the about communication problem.
        /// </summary>
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

        /// <summary>
        /// Updates the ComboBox.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
