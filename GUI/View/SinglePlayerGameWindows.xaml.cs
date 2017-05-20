using GUI.Model;
using GUI.ViewModel;
using MazeLib;
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
        private SinglePlayerGameViewModel spgVM;

        public SinglePlayerGameWindow(IClientModel model)
        {
            spgVM = new SinglePlayerGameViewModel(model);
            DataContext = spgVM;
            InitializeComponent();
        }

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

        private void MazeUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MyMazeBoard.Draw();
            this.KeyDown += MyMazeBoard.OnKeyDownHandler;

        }

        private void Button_Click_Restart(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to restart the game?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MyMazeBoard.Restart();
            }
        }

        private void Button_Click_Solve(object sender, RoutedEventArgs e)
        {
            this.spgVM.GenerateSolveString();
            this.Solve();

        }

        public void Solve()
        {
            
            Task t = Task.Run(() =>
            {
                Position p = spgVM.VM_InitPosition;
                foreach (char c in spgVM.VM_SolveString)
                {
                    switch (c)
                    {
                        case '0':
                            {
                                p.Col -= 1;
                                MyMazeBoard.animation(p);
                                break;
                            }
                        case '1':
                            {
                                p.Col += 1;
                                MyMazeBoard.animation(p);
                                break;
                            }
                        case '2':
                            {

                                p.Row -= 1;
                                MyMazeBoard.animation(p);
                                break;
                            }
                        case '3':
                            {

                                p.Row += 1;
                                MyMazeBoard.animation(p);
                                break;
                            }
                        default:
                            break;
                    }
                }
            });

        }

    }





}
