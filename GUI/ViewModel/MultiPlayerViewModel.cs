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

    public class MultiPlayerViewModel : ViewModel
    {
        public bool IsReady; //indicates that the game is ready to begin (in new MultiplayeGameWindow)
        public MazeUserControl MyMazeBoard { get; set; }
        public MazeUserControl OpponentMazeBoard { get; set; }
        public delegate void NotifyViewPropertyChanged(bool finish);
        public event NotifyViewPropertyChanged FinishGameHappened;

        private MultiClientModel model;

        public MultiPlayerViewModel()
        {
            IsReady = false;
            this.model = new MultiClientModel();
        }

        public MultiClientModel Model
        {
            get { return model; }
        }

        public MultiPlayerGameWindow MultiplayerGameWindow { get; set; }

        //firstCommand - the start/join command to send to the server
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

        public void PassCommandToServer(string command)
        {
            this.model.MessageToSend = command;
            this.model.commandIsReadyToBeSent = true;
        }


        public int MazeRowsDefinition
        {
            get { return model.Rows; }
            set { model.Rows = value; }
        }

        public int MazeColsDefinition
        {
            get { return model.Cols; }
            set { model.Cols = value; }
        }

        public string mazeNameDefinition;
        public string MazeNameDefinition
        {
            get { return mazeNameDefinition; }
            set { mazeNameDefinition = value; ; }
        }

        private string selectedGame;
        public string SelectedGame
        {
            get { return selectedGame; }
            set { selectedGame = value; ; }

        }

        public string MazeName
        {
            get { return model.MazeName; }
            set { model.MazeName = value; }
        }

        public Position CurrentPosition
        {
            get { return model.CurrentPosition; }
            set { model.CurrentPosition = value; }
        }

        public Position OpponentPosition
        {
            get { return model.OpponentPosition; }
            set { model.OpponentPosition = value; }
        }

        public int Rows
        {
            get { return model.Rows; }
            set { model.Rows = value; }
        }

        public int Cols
        {
            get { return model.Cols; }
            set { model.Cols = value; }
        }

        public string MazeString
        {
            get { return model.MazeString; }
            set { model.MazeString = value; }
        }
        public Position InitPosition
        {
            get { return model.InitPosition; }
            set
            { model.InitPosition = value; }
        }
        public Position GoalPosition
        {
            get { return model.GoalPosition; }
            set { model.GoalPosition = value; }
        }
        public string SolveString
        {
            get { return model.SolveString; }
            set { model.SolveString = value; }
        }


        public String VM_MazeName
        {
            get { return model.MazeName; }
            set { model.MazeName = value; }
        }

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
