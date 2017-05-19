using GUI.Model;
using MazeLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GUI.ViewModel
{
    public class MultiPlayerViewModel : ViewModel
    {
        //TcpClient client;
        public bool IsReady;

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

        //firstCommand - the start/join command
        public void ConnectAndCommunicate(string firstCommand)
        {
            model.ReceivingMessageEvent += UpdateViewThatTheServerSentMazeToUs;
            var t = new Task(() =>
            {
                PassCommandToServer(firstCommand);
                this.model.Communicate();
            });
            //manage all the communication with the server
            t.Start();
        }

        private void UpdateViewThatTheServerSentMessageToUs(string message)
        {
            Console.WriteLine("debug: message from the server - " + message);
        }

        private void UpdateViewThatTheServerSentMazeToUs(string serverResponedMaze)
        {
            Console.WriteLine("debug: maze from the server - " + serverResponedMaze);
            model.ReceivingMessageEvent -= UpdateViewThatTheServerSentMazeToUs;
            model.ReceivingMessageEvent += UpdateViewThatTheServerSentMessageToUs;

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


        private int mazeRowsDefinition;
        public int MazeRowsDefinition
        {
            get { return mazeRowsDefinition; }
            set { mazeRowsDefinition = value; }
        }
        private int mazeColsDefinition;
        public int MazeColsDefinition
        {
            get { return mazeColsDefinition; }
            set { mazeColsDefinition = value; }
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

        public void PassCommandToServer(string command)
        {
            this.model.MessageToSend = command;
            this.model.commandIsReadyToBeSent = true;
        }

        public string MazeName
        {
            get { return model.MazeName; }
            set
            {
                model.MazeName = value;
            }
        }
        public Position CurrentPosition
        {
            get { return model.CurrentPosition; }
            set
            {
                model.CurrentPosition = value;
            }
        }
        public int Rows
        {
            get { return model.Rows; }
            set
            {
                model.Rows = value;
            }
        }
        public int Cols
        {
            get { return model.Cols; }
            set
            {
                model.Cols = value;
            }
        }
        public string MazeString
        {
            get { return model.MazeString; }
            set
            {
                model.MazeString = value;
            }
        }
        public Position InitPosition
        {
            get { return model.InitPosition; }
            set
            {
                model.InitPosition = value;
            }
        }
        public Position GoalPosition
        {
            get { return model.GoalPosition; }
            set
            {
                model.GoalPosition = value;
            }
        }
        public string SolveString
        {
            get { return model.SolveString; }
            set
            {
                model.SolveString = value;
            }
        }
    }
}
