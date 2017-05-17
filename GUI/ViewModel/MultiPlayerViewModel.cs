using GUI.Model;

namespace GUI.ViewModel
{
    class MultiPlayerViewModel : ViewModel
    {

        private IClientModel model;

        public MultiPlayerViewModel()
        {
            this.model = new MultiClientModel();
            this.model.Connect();
            //manage all the communication with the server
        }

        public IClientModel Model
        {
            get { return model; }
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

    }
}
