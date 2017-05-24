// ***********************************************************************
// Assembly         : GUI
// Author           : ohad
// Created          : 05-15-2017
//
// Last Modified By : ohad
// Last Modified On : 05-21-2017
// ***********************************************************************
// <copyright file="SinglePlayerWindow.xaml.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
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
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class SinglePlayerWindow : Window
    {
        /// <summary>
        /// The sp vm
        /// </summary>
        public SinglePlayerViewModel spVM;
        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerWindow"/> class.
        /// </summary>
        public SinglePlayerWindow()
        {
            try
            {
                spVM = new SinglePlayerViewModel();
            }
            catch (Exception)
            {
                NotifyCommunicationProblem();
            }

            this.DataContext = spVM;
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                spVM.GenerateMaze();
            }
            catch (Exception)
            {
                NotifyCommunicationProblem();
            }
            SinglePlayerGameWindow spGW = new SinglePlayerGameWindow(spVM.model);
            spGW.Show();
            this.Close();
        }

        private void NotifyCommunicationProblem()
        {
            MessageBox.Show("We didn't succeed to connect to the server",
                "Info", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            Dispatcher.Invoke(() =>
            {
                Application.Current.Shutdown();
            });

        }
    }
}
