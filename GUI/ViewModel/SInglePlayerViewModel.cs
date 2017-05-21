using GUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModel
{
    public class SinglePlayerViewModel : ViewModel
    {
        public IClientModel model;
        private int mazeRowsDefinition;
        private int mazeColsDefinition;
        private TcpClient client;
        public SinglePlayerViewModel()
        {
            model = new ClientModel();
            client = model.Connect();
            mazeRowsDefinition = Properties.Settings.Default.MazeRows;
            mazeColsDefinition = Properties.Settings.Default.MazeCols;
        }

        public IClientModel Model
        {
            get { return model; }
        }


        public void GenerateMaze()
        {
            StringBuilder generateString = new StringBuilder();
            generateString.Append("generate " + MazeNameDefinition + " " + MazeRowsDefinition + " " + MazeColsDefinition);
            model.GetMaze(client, generateString.ToString());
        }

        public int MazeRowsDefinition
        {
            get { return mazeRowsDefinition;}
            set { model.Rows = value; }
        }
        
        public int MazeColsDefinition
        {
            get { return mazeColsDefinition;}
            set { model.Cols = value; }
        }

        public string mazeNameDefinition;

        public string MazeNameDefinition
        {
            get { return mazeNameDefinition; }
            set { mazeNameDefinition = value; }
        }
    }
}
