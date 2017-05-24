// ***********************************************************************
// Assembly         : GUI
// Author           : ohad
// Created          : 05-15-2017
//
// Last Modified By : ohad
// Last Modified On : 05-24-2017
// ***********************************************************************
// <copyright file="MultiPlayerViewModel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using GUI.Controls;
using GUI.Model;
using GUI.View;
using MazeLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace GUI.ViewModel
{
    //public delegate void MyEventHandler();

    /// <summary>
    /// Class MultiPlayerViewModel.
    /// </summary>
    /// <seealso cref="GUI.ViewModel.ViewModel" />
    public class MultiPlayerViewModel : ViewModel
    {
        /// <summary>
        /// The is ready
        /// </summary>
        public bool IsReady; //indicates that the game is ready to begin (in new MultiplayeGameWindow)
        /// <summary>
        /// Gets or sets my maze board.
        /// </summary>
        /// <value>My maze board.</value>
        public MazeUserControl MyMazeBoard { get; set; }
        /// <summary>
        /// Gets or sets the opponent maze board.
        /// </summary>
        /// <value>The opponent maze board.</value>
        public MazeUserControl OpponentMazeBoard { get; set; }
        /// <summary>
        /// Delegate NotifyViewPropertyChanged
        /// </summary>
        /// <param name="finish">if set to <c>true</c> [finish].</param>
        public delegate void NotifyViewPropertyChanged(bool finish);
        public event NotifyViewPropertyChanged FinishGameHappened;

        /// <summary>
        /// The model
        /// </summary>
        private MultiClientModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerViewModel"/> class.
        /// </summary>
        public MultiPlayerViewModel()
        {
            IsReady = false;
            this.model = new MultiClientModel();
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <value>The model.</value>
        public MultiClientModel Model
        {
            get { return model; }
        }

        /// <summary>
        /// Gets or sets the multiplayer game window.
        /// </summary>
        /// <value>The multiplayer game window.</value>
        public MultiPlayerGameWindow MultiplayerGameWindow { get; set; }

        //firstCommand - the start/join command to send to the server
        /// <summary>
        /// Connects the and communicate.
        /// </summary>
        /// <param name="firstCommand">The first command.</param>
        public void ConnectAndCommunicate(string firstCommand)
        {
            model.ReceivingMessageEvent += UpdateViewThatTheServerSentMazeToUs;
            //Task to send this command to server and manage continous communication with the server
            var t = new Task(() =>
            {
                PassCommandToServer(firstCommand);
                this.model.Communicate();
            });
            //manage all the communication with the server
            t.Start();
        }

        //it will be executed only in the first time model.ReceivingMessageEvent (receiving message from server) 
        //takes place (The message in this case is the maze that the server generated).
        /// <summary>
        /// Updates the view that the server sent maze to us.
        /// </summary>
        /// <param name="serverResponedMaze">The server responed maze.</param>
        private void UpdateViewThatTheServerSentMazeToUs(string serverResponedMaze)
        {
            model.ReceivingMessageEvent -= UpdateViewThatTheServerSentMazeToUs;

            Maze maze = Maze.FromJSON(serverResponedMaze);
            var data = (JObject)JsonConvert.DeserializeObject(serverResponedMaze);
            model.MazeString = data["Maze"].Value<String>();
            model.MazeName = maze.Name;
            model.Cols = maze.Cols;
            model.Rows = maze.Rows;
            model.InitPosition = maze.InitialPos;
            model.GoalPosition = maze.GoalPos;
            model.CurrentPosition = maze.InitialPos;

            IsReady = true;
        }

        //it will be executed when the user will press the keyboard
        /// <summary>
        /// Handles the <see cref="E:MyMoveHandler" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        public void OnMyMoveHandler(object sender, KeyEventArgs e)
        {
            //update the server about the current player step
            if (e.Key == Key.Left)
            {
                PassCommandToServer("play left");
            }
            if (e.Key == Key.Right)
            {
                PassCommandToServer("play right");
            }
            if (e.Key == Key.Up)
            {
                PassCommandToServer("play up");
            }
            if (e.Key == Key.Down)
            {
                PassCommandToServer("play down");
            }

            //if the current player arrived to the goal position in the maze, activate the appropriate event
            if (MyMazeBoard.currentPosition.Equals(MyMazeBoard.GoalPosition))
            {
                MyMazeBoard.FinishGame = true;
                FinishGameHappened.Invoke(true);
            }
        }

        /// <summary>
        /// Passes the command to server.
        /// </summary>
        /// <param name="command">The command.</param>
        public void PassCommandToServer(string command)
        {
            this.model.MessageToSend = command;
            this.model.commandIsReadyToBeSent = true;
        }


        /// <summary>
        /// Gets or sets the maze rows definition.
        /// </summary>
        /// <value>The maze rows definition.</value>
        public int MazeRowsDefinition
        {
            get { return model.Rows; }
            set { model.Rows = value; }
        }

        /// <summary>
        /// Gets or sets the maze cols definition.
        /// </summary>
        /// <value>The maze cols definition.</value>
        public int MazeColsDefinition
        {
            get { return model.Cols; }
            set { model.Cols = value; }
        }

        /// <summary>
        /// The maze name definition
        /// </summary>
        public string mazeNameDefinition;
        /// <summary>
        /// Gets or sets the maze name definition.
        /// </summary>
        /// <value>The maze name definition.</value>
        public string MazeNameDefinition
        {
            get { return mazeNameDefinition; }
            set { mazeNameDefinition = value; ; }
        }

        /// <summary>
        /// The selected game
        /// </summary>
        private string selectedGame;
        /// <summary>
        /// Gets or sets the selected game.
        /// </summary>
        /// <value>The selected game.</value>
        public string SelectedGame
        {
            get { return selectedGame; }
            set { selectedGame = value; ; }

        }

        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>The name of the maze.</value>
        public string MazeName
        {
            get { return model.MazeName; }
            set { model.MazeName = value; }
        }

        /// <summary>
        /// Gets or sets the current position.
        /// </summary>
        /// <value>The current position.</value>
        public Position CurrentPosition
        {
            get { return model.CurrentPosition; }
            set { model.CurrentPosition = value; }
        }

        /// <summary>
        /// Gets or sets the opponent position.
        /// </summary>
        /// <value>The opponent position.</value>
        public Position OpponentPosition
        {
            get { return model.OpponentPosition; }
            set { model.OpponentPosition = value; }
        }

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>The rows.</value>
        public int Rows
        {
            get { return model.Rows; }
            set { model.Rows = value; }
        }

        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>The cols.</value>
        public int Cols
        {
            get { return model.Cols; }
            set { model.Cols = value; }
        }

        /// <summary>
        /// Gets or sets the maze string.
        /// </summary>
        /// <value>The maze string.</value>
        public string MazeString
        {
            get { return model.MazeString; }
            set { model.MazeString = value; }
        }
        /// <summary>
        /// Gets or sets the initialize position.
        /// </summary>
        /// <value>The initialize position.</value>
        public Position InitPosition
        {
            get { return model.InitPosition; }
            set
            { model.InitPosition = value; }
        }
        /// <summary>
        /// Gets or sets the goal position.
        /// </summary>
        /// <value>The goal position.</value>
        public Position GoalPosition
        {
            get { return model.GoalPosition; }
            set { model.GoalPosition = value; }
        }
        /// <summary>
        /// Gets or sets the solve string.
        /// </summary>
        /// <value>The solve string.</value>
        public string SolveString
        {
            get { return model.SolveString; }
            set { model.SolveString = value; }
        }


        /// <summary>
        /// Gets or sets the name of the vm maze.
        /// </summary>
        /// <value>The name of the vm maze.</value>
        public String VM_MazeName
        {
            get { return model.MazeName; }
            set { model.MazeName = value; }
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
                FinishGameHappened?.Invoke(true);
            }
        }

    }
}
