namespace GUI.Model
{
    class SettingsModel : ISettingsModel
    {

        public int DefaultMazeRows = 5;
        public int DefaultMazeCols = 5;
        public int DefaultSearchAlgorithm = 0;

        public string ServerIP
        {
            get { return Properties.Settings.Default.ServerIP; }
            set { Properties.Settings.Default.ServerIP = value; }
        }
        public int ServerPort
        {
            get { return Properties.Settings.Default.ServerPort; }
            set { Properties.Settings.Default.ServerPort = value; }
        }
    
        public int MazeRows
        {
            get
            {
                return DefaultMazeRows;
            }
            set
            {
                DefaultMazeRows = value;
            }
        }
    
        public int MazeCols
        {
            get
            {
                return DefaultMazeCols;
            }
            set
            {
                DefaultMazeCols = value;
            }
        }
        public int SearchAlgorithm
        {
            get
            {
                return DefaultSearchAlgorithm;
            }
            set
            {
                DefaultSearchAlgorithm = value;
            }
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }
    }
}
