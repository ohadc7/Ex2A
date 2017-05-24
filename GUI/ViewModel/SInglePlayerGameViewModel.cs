// ***********************************************************************
// Assembly         : GUI
// Author           : ohad
// Created          : 05-15-2017
//
// Last Modified By : ohad
// Last Modified On : 05-24-2017
// ***********************************************************************
// <copyright file="SinglePlayerGameViewModel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
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
    /// <summary>
    /// Class SinglePlayerGameViewModel.
    /// </summary>
    /// <seealso cref="GUI.ViewModel.ViewModel" />
    class SinglePlayerGameViewModel : ViewModel
    {
        /// <summary>
        /// The model
        /// </summary>
        private SingleClientModel model;
        /// <summary>
        /// The client
        /// </summary>
        private TcpClient client;
        /// <summary>
        /// Delegate NotifyViewPropertyChanged
        /// </summary>
        /// <param name="finish">if set to <c>true</c> [finish].</param>
        public delegate void NotifyViewPropertyChanged(bool finish);
        public event NotifyViewPropertyChanged FinishGameHappend;
        public event NotifyViewPropertyChanged SolveAnimationFinishedHappend;
        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerGameViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SinglePlayerGameViewModel(SingleClientModel model)
        {

            this.model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        /// <summary>
        /// Generates the solve string.
        /// </summary>
        public void GenerateSolveString()
        {
            client = this.model.Connect();
            StringBuilder generateString = new StringBuilder();
            generateString.Append("solve " + VM_MazeName + " " + Properties.Settings.Default.DefaultSearchAlgorithm.ToString());
            model.GetSolveString(client, generateString.ToString());
        }


        /// <summary>
        /// Gets or sets the name of the vm maze.
        /// </summary>
        /// <value>The name of the vm maze.</value>
        public String VM_MazeName
        {
            get { return model.MazeName; }
            set
            {
                model.MazeName = value;

            }
        }

        /// <summary>
        /// Gets or sets the vm current position.
        /// </summary>
        /// <value>The vm current position.</value>
        public Position VM_CurrentPosition
        {
            get { return model.CurrentPosition; }
            set
            {
                model.CurrentPosition = value;

            }
        }

        /// <summary>
        /// Gets or sets the vm rows.
        /// </summary>
        /// <value>The vm rows.</value>
        public int VM_Rows
        {
            get { return model.Rows; }
            set
            {
                model.Rows = value;
            }
        }

        /// <summary>
        /// Gets or sets the vm cols.
        /// </summary>
        /// <value>The vm cols.</value>
        public int VM_Cols
        {
            get { return model.Cols; }
            set
            {
                model.Cols = value;
            }
        }
        /// <summary>
        /// Gets or sets the vm maze string.
        /// </summary>
        /// <value>The vm maze string.</value>
        public string VM_MazeString
        {
            get { return model.MazeString; }
            set
            {
                model.MazeString = value;
            }
        }
        /// <summary>
        /// Gets or sets the vm initialize position.
        /// </summary>
        /// <value>The vm initialize position.</value>
        public Position VM_InitPosition
        {
            get { return model.InitPosition; }
            set
            {
                model.InitPosition = value;
            }
        }
        /// <summary>
        /// Gets or sets the vm goal position.
        /// </summary>
        /// <value>The vm goal position.</value>
        public Position VM_GoalPosition
        {
            get { return model.GoalPosition; }
            set
            {
                model.GoalPosition = value;
            }
        }
        /// <summary>
        /// Gets or sets the vm solve string.
        /// </summary>
        /// <value>The vm solve string.</value>
        public string VM_SolveString
        {
            get { return model.SolveString; }
            set
            {
                model.SolveString = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [vm finish game].
        /// </summary>
        /// <value><c>true</c> if [vm finish game]; otherwise, <c>false</c>.</value>
        public bool VM_FinishGame
        {
            get { return model.FinishGame; }
            set
            {
                model.FinishGame = value;
                FinishGameHappend?.Invoke(true);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [vm solve finish].
        /// </summary>
        /// <value><c>true</c> if [vm solve finish]; otherwise, <c>false</c>.</value>
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
