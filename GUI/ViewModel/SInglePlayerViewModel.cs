using GUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModel
{
    public class SinglePlayerViewModel : ViewModel
    {
        public IClientModel model;

        public SinglePlayerViewModel()
        {
            model = new ClientModel();
        }

        public IClientModel Model
        {
            get { return model; }
        }

        private int mazeRowsDefinition;
        public int MazeRowsDefinition
        {
            get { return Properties.Settings.Default.MazeRows;}
            set { mazeRowsDefinition = value; }
        }
        public int mazeColsDefinition;
        public int MazeColsDefinition
        {
            get { return Properties.Settings.Default.MazeCols;}
            set { mazeColsDefinition = value; }
        }

        public string mazeNameDefinition;

        public string MazeNameDefinition
        {
            get { return mazeNameDefinition; }
            set { mazeNameDefinition = value; ; }
        }
    }
}
