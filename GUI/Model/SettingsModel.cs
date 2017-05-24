// ***********************************************************************
// Assembly         : GUI
// Author           : ohad
// Created          : 05-15-2017
//
// Last Modified By : ohad
// Last Modified On : 05-19-2017
// ***********************************************************************
// <copyright file="SettingsModel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GUI.Model
{
    /// <summary>
    /// Class SettingsModel.
    /// </summary>
    /// <seealso cref="GUI.Model.ISettingsModel" />
    class SettingsModel : ISettingsModel
    {

        /// <summary>
        /// Gets or sets the server ip.
        /// </summary>
        /// <value>The server ip.</value>
        public string ServerIP
        {
            get { return Properties.Settings.Default.ServerIP; }
            set { Properties.Settings.Default.ServerIP = value; }
        }
        /// <summary>
        /// Gets or sets the server port.
        /// </summary>
        /// <value>The server port.</value>
        public int ServerPort
        {
            get { return Properties.Settings.Default.ServerPort; }
            set { Properties.Settings.Default.ServerPort = value; }
        }

        /// <summary>
        /// Gets or sets the maze rows.
        /// </summary>
        /// <value>The maze rows.</value>
        public int MazeRows
        {
            get
            {
                return Properties.Settings.Default.MazeRows;
            }
            set
            {
                Properties.Settings.Default.MazeRows = value;
            }
        }

        /// <summary>
        /// Gets or sets the maze cols.
        /// </summary>
        /// <value>The maze cols.</value>
        public int MazeCols
        {
            get
            {
                return Properties.Settings.Default.MazeCols;
            }
            set
            {
                Properties.Settings.Default.MazeCols = value;
            }
        }
        /// <summary>
        /// Gets or sets the search algorithm.
        /// </summary>
        /// <value>The search algorithm.</value>
        public int SearchAlgorithm
        {
            get
            {
                return Properties.Settings.Default.DefaultSearchAlgorithm;
            }
            set
            {
                Properties.Settings.Default.DefaultSearchAlgorithm = value;
            }
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }
    }
}
