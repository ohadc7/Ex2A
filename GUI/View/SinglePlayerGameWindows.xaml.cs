// ***********************************************************************
// Assembly         : GUI
// Author           : ohad
// Created          : 05-15-2017
//
// Last Modified By : ohad
// Last Modified On : 05-24-2017
// ***********************************************************************
// <copyright file="SinglePlayerGameWindows.xaml.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using GUI.Model;
using GUI.ViewModel;
using MazeLib;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for SinglePlayerGameWindows.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class SinglePlayerGameWindow : Window
    {
        /// <summary>
        /// The SPG vm
        /// </summary>
        private SinglePlayerGameViewModel spgVM;

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerGameWindow"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SinglePlayerGameWindow(SingleClientModel model)
        {
            spgVM = new SinglePlayerGameViewModel(model);
            spgVM.FinishGameHappend += FinishGame;
            spgVM.SolveAnimationFinishedHappend += SolveMessege;
            DataContext = spgVM;
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Menu event of the Button_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Click_Menu(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to return to the main menu?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow win = (MainWindow)Application.Current.MainWindow;
                win.Show();
                this.Close();
            }
           
        }

        /// <summary>
        /// Handles the Loaded event of the MazeUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MazeUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MyMazeBoard.Draw();
            this.KeyDown += MyMazeBoard.OnKeyDownHandler;

        }

        /// <summary>
        /// Handles the Restart event of the Button_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Click_Restart(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to restart the game?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MyMazeBoard.Restart();
            }
        }

        /// <summary>
        /// Handles the Solve event of the Button_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Click_Solve(object sender, RoutedEventArgs e)
        {
            this.spgVM.GenerateSolveString();
            this.Solve();
        }

        /// <summary>
        /// Solves this instance.
        /// </summary>
        public void Solve()
        {
            Position p = spgVM.VM_InitPosition;
            Task t = Task.Run(() =>
            {
                MyMazeBoard.Animation(p);
                foreach (char c in spgVM.VM_SolveString)
                {
                    switch (c)
                    {
                        case '0':
                            {
                                p.Col -= 1;
                                MyMazeBoard.Animation(p);
                                break;
                            }
                        case '1':
                            {
                                p.Col += 1;
                                MyMazeBoard.Animation(p);
                                break;
                            }
                        case '2':
                            {
                                p.Row -= 1;
                                MyMazeBoard.Animation(p);
                                break;
                            }
                        case '3':
                            {
                                p.Row += 1;
                                MyMazeBoard.Animation(p);
                                break;
                            }
                        default:
                            break;
                    }
                }

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
                MessageBoxResult result = MessageBox.Show("Good Work! You finished the game.", "Finish Game", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                MainWindow win = (MainWindow)Application.Current.MainWindow;
                win.Show();
                this.Close();
            }
        }

        /// <summary>
        /// Solves the messege.
        /// </summary>
        /// <param name="Solvefinish">if set to <c>true</c> [solvefinish].</param>
        public void SolveMessege(bool Solvefinish)
        {
            if (Solvefinish)
            {
                MessageBoxResult result = MessageBox.Show("Now that we solve the game for you, do you want to try again?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    MyMazeBoard.Restart();
                }
                if (result == MessageBoxResult.No)
                {
                    MessageBox.Show("press Ok to get back to the main menu", "Info", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    MainWindow win = (MainWindow)Application.Current.MainWindow;
                    win.Show();
                    this.Close();
                }
            }

        }

    }





}
