using GUI.ViewModel;
using System;
using System.Windows;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for MultiPlayerGameWindow.xaml
    /// </summary>
    public partial class MultiPlayerGameWindow : Window
    {
        private MultiPlayerViewModel mpVM;

        public MultiPlayerGameWindow(MultiPlayerViewModel mpVM)
        {
            this.mpVM = mpVM;
            this.DataContext = mpVM;
            //subscribe to events about the communication
            mpVM.FinishGameHappened += FinishGame;
            mpVM.Model.CommunicationProblemEvent += NotifyAboutCommunicationProblem;
            InitializeComponent();
        }

        private void MyMazeUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MyMazeBoard.Draw();
            this.KeyDown += MyMazeBoard.OnKeyDownHandler;
            this.KeyDown += mpVM.OnMyMoveHandler;
            mpVM.MyMazeBoard = MyMazeBoard;
        }

        private void OpponentMazeUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            OpponentMazeBoard.Draw();
            mpVM.Model.ReceivingMessageEvent += OpponentMazeBoard.OnOpponentMoveHandler;
            mpVM.Model.GameBecameClosedEvent += CloseGame;
            mpVM.OpponentMazeBoard = OpponentMazeBoard;
        }

        private void Button_Click_Menu(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to return to the main menu?",
                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                mpVM.PassCommandToServer("close");
            }
        }

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

        private void NotifyAboutCommunicationProblem()
        {
            MessageBox.Show("We didn't succeed to connect to the server",
                "Info", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            Dispatcher.Invoke(() =>
            {
                Application.Current.Shutdown();
            });
        }

        public void FinishGame(bool finish)
        {
            if (finish)
            {
                MessageBoxResult result = MessageBox.Show("Good Work! You finished the game. You are the winner!",
                    "Finish Game", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                mpVM.PassCommandToServer("close");
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            mpVM.PassCommandToServer("close");
            base.OnClosed(new EventArgs());
        }
    }
}
