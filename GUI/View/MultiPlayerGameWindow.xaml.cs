// ***********************************************************************
// Assembly         : GUI
// Author           : ohad
// Created          : 05-15-2017
//
// Last Modified By : ohad
// Last Modified On : 05-24-2017
// ***********************************************************************
// <copyright file="MultiPlayerGameWindow.xaml.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using GUI.ViewModel;
using System;
using System.Windows;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for MultiPlayerGameWindow.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MultiPlayerGameWindow : Window
    {
        /// <summary>
        /// The mp vm
        /// </summary>
        private MultiPlayerViewModel mpVM;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerGameWindow"/> class.
        /// </summary>
        /// <param name="mpVM">The mp vm.</param>
        public MultiPlayerGameWindow(MultiPlayerViewModel mpVM)
        {
            this.mpVM = mpVM;
            this.DataContext = mpVM;
            //subscribe to events about the communication
            mpVM.FinishGameHappened += FinishGame;
            mpVM.Model.CommunicationProblemEvent += NotifyAboutCommunicationProblem;
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Loaded event of the MyMazeUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MyMazeUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MyMazeBoard.Draw();
            this.KeyDown += MyMazeBoard.OnKeyDownHandler;
            this.KeyDown += mpVM.OnMyMoveHandler;
            mpVM.MyMazeBoard = MyMazeBoard;
        }

        /// <summary>
        /// Handles the Loaded event of the OpponentMazeUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OpponentMazeUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            OpponentMazeBoard.Draw();
            mpVM.Model.ReceivingMessageEvent += OpponentMazeBoard.OnOpponentMoveHandler;
            mpVM.Model.GameBecameClosedEvent += CloseGame;
            mpVM.OpponentMazeBoard = OpponentMazeBoard;
        }

        /// <summary>
        /// Handles the Menu event of the Button_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Click_Menu(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to return to the main menu?",
                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                mpVM.PassCommandToServer("close");
            }
        }

        /// <summary>
        /// Closes the game.
        /// </summary>
        private void CloseGame()
        {
            MessageBox.Show("The game is closed", "Info", MessageBoxButton.OK, MessageBoxImage.Asterisk);

            Dispatcher.Invoke(() =>
            {
                MainWindow win = (MainWindow)Application.Current.MainWindow;
                win.Show();
                this.Close();
            });
        }

        /// <summary>
        /// Notifies the about communication problem.
        /// </summary>
        private void NotifyAboutCommunicationProblem()
        {
            MessageBox.Show("We didn't succeed to connect to the server",
                "Info", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            Dispatcher.Invoke(() =>
            {
                Application.Current.Shutdown();
            });
        }

        /// <summary>
        /// Finishes the game.
        /// </summary>
        /// <param name="finish">if set to <c>true</c> [finish].</param>
        public void FinishGame(bool finish)
        {
            if (finish)
            {
                MessageBoxResult result = MessageBox.Show("Good Work! You finished the game. You are the winner!",
                    "Finish Game", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                mpVM.PassCommandToServer("close");
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Closed" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnClosed(EventArgs e)
        {
            mpVM.PassCommandToServer("close");
            base.OnClosed(new EventArgs());
        }
    }
}
