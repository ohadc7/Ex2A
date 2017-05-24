// ***********************************************************************
// Assembly         : GUI
// Author           : ohad
// Created          : 05-21-2017
//
// Last Modified By : ohad
// Last Modified On : 05-21-2017
// ***********************************************************************
// <copyright file="AbstractClient.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace GUI.Model
{
    /// <summary>
    /// Class AbstractClient.
    /// </summary>
    /// <seealso cref="GUI.Model.IClientModel" />
    public abstract class AbstractClient : IClientModel
    {

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// The maze name
        /// </summary>
        private string mazeName;
        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>The name of the maze.</value>
        public string MazeName
        {
            get { return mazeName; }
            set
            {
                mazeName = value;
                NotifyPropertyChanged("MazeName");
            }
        }
        /// <summary>
        /// The current position
        /// </summary>
        private Position currentPosition;
        /// <summary>
        /// Gets or sets the current position.
        /// </summary>
        /// <value>The current position.</value>
        public Position CurrentPosition
        {
            get { return currentPosition; }
            set
            {
                currentPosition = value;
                NotifyPropertyChanged("CurrentPosition");
            }
        }
        /// <summary>
        /// The rows
        /// </summary>
        private int rows;
        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>The rows.</value>
        public int Rows
        {
            get { return rows; }
            set
            {
                rows = value;
                NotifyPropertyChanged("Rows");
            }
        }
        /// <summary>
        /// The cols
        /// </summary>
        private int cols;
        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>The cols.</value>
        public int Cols
        {
            get { return cols; }
            set
            {
                cols = value;
                NotifyPropertyChanged("Cols");
            }
        }
        /// <summary>
        /// The maze
        /// </summary>
        private string maze;
        /// <summary>
        /// Gets or sets the maze string.
        /// </summary>
        /// <value>The maze string.</value>
        public string MazeString
        {
            get { return maze; }
            set
            {
                maze = value;
                NotifyPropertyChanged("MazeString");
            }
        }
        /// <summary>
        /// The initialize position
        /// </summary>
        private Position initPosition;
        /// <summary>
        /// Gets or sets the initialize position.
        /// </summary>
        /// <value>The initialize position.</value>
        public Position InitPosition
        {
            get { return initPosition; }
            set
            {
                initPosition = value;
                NotifyPropertyChanged("InitPosition");
            }
        }
        /// <summary>
        /// The goal position
        /// </summary>
        private Position goalPosition;
        /// <summary>
        /// Gets or sets the goal position.
        /// </summary>
        /// <value>The goal position.</value>
        public Position GoalPosition
        {
            get { return goalPosition; }
            set
            {
                goalPosition = value;
                NotifyPropertyChanged("GoalPosition");
            }
        }
        /// <summary>
        /// The solve string
        /// </summary>
        private string solveString;
        /// <summary>
        /// Gets or sets the solve string.
        /// </summary>
        /// <value>The solve string.</value>
        public string SolveString
        {
            get { return solveString; }
            set
            {
                solveString = value;
                NotifyPropertyChanged("SolveString");
            }
        }

        /// <summary>
        /// The finish game
        /// </summary>
        private bool finishGame;
        /// <summary>
        /// Gets or sets a value indicating whether [finish game].
        /// </summary>
        /// <value><c>true</c> if [finish game]; otherwise, <c>false</c>.</value>
        public bool FinishGame
        {
            get { return finishGame; }
            set
            {
                finishGame = value;
                NotifyPropertyChanged("FinishGame");
            }
        }

        /// <summary>
        /// The solve finish
        /// </summary>
        private bool solveFinish;
        /// <summary>
        /// Gets or sets a value indicating whether [solve finish].
        /// </summary>
        /// <value><c>true</c> if [solve finish]; otherwise, <c>false</c>.</value>
        public bool SolveFinish
        {
            get { return solveFinish; }
            set
            {
                solveFinish = value;
                NotifyPropertyChanged("SolveFinish");
            }
        }
    }
}
