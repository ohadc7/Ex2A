﻿using GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for MultiPlayerGameWindow.xaml
    /// </summary>
    public partial class MultiPlayerGameWindow : Window
    {
        private MultiPlayerViewModel mpVM;

        public MultiPlayerGameWindow(MultiPlayerViewModel mpVM)
        {
            this.mpVM = mpVM;
            this.DataContext = mpVM;
            InitializeComponent();
        }

        private void MyMazeUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MyMazeBoard.Draw();
            this.KeyDown += MyMazeBoard.OnKeyDownHandler;
        }

        private void OpponentMazeUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            OpponentMazeBoard.Draw();
            //mpVM.Model.ReceivingMessageEvent += mpVM.UpdateViewThatTheServerSentMessageToUs;
            //mpVM.Model.ReceivingMessageEvent += OpponentMazeBoard.OnOpponentMoveHandler;
        }
    }
}
