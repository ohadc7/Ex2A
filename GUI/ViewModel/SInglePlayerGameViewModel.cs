﻿using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using CommunicationSettings;
using MazeLib;
using GUI.Model;
using System.Text;

namespace GUI.ViewModel
{
    class SinglePlayerGameViewModel : ViewModel
    {
        public SingleClientModel model;
        private TcpClient client;
        public delegate void NotifyViewPropertyChanged(bool finish);
        public event NotifyViewPropertyChanged FinishGameHappend;
        public event NotifyViewPropertyChanged SolveAnimationFinishedHappend;
        public SinglePlayerGameViewModel(SingleClientModel model)
        {

            this.model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public void GenerateSolveString()
        {
            client = this.model.Connect();
            StringBuilder generateString = new StringBuilder();
            generateString.Append("solve " + VM_MazeName + " " + Properties.Settings.Default.DefaultSearchAlgorithm.ToString());
            model.GetSolveString(client, generateString.ToString());
        }


        public String VM_MazeName
        {
            get { return model.MazeName; }
            set
            {
                model.MazeName = value;

            }
        }

        public Position VM_CurrentPosition
        {
            get { return model.CurrentPosition; }
            set
            {
                model.CurrentPosition = value;

            }
        }

        public int VM_Rows
        {
            get { return model.Rows; }
            set
            {
                model.Rows = value;
            }
        }
        
        public int VM_Cols
        {
            get { return model.Cols; }
            set
            {
                model.Cols = value;
            }
        }
        public string VM_MazeString
        {
            get { return model.MazeString; }
            set
            {
                model.MazeString = value;
            }
        }
        public Position VM_InitPosition
        {
            get { return model.InitPosition; }
            set
            {
                model.InitPosition = value;
            }
        }
        public Position VM_GoalPosition
        {
            get { return model.GoalPosition; }
            set
            {
                model.GoalPosition = value;
            }
        }
        public string VM_SolveString
        {
            get { return model.SolveString; }
            set
            {
                model.SolveString = value;
            }
        }

        public bool VM_FinishGame
        {
            get { return model.FinishGame; }
            set
            {
                model.FinishGame = value;
                FinishGameHappend?.Invoke(true);
            }
        }

        public bool VM_SolveFinish
        {
            get { return model.SolveFinish; }
            set
            {
                model.SolveFinish = value;
                SolveAnimationFinishedHappend?.Invoke(true);
            }
        }
    }
}
