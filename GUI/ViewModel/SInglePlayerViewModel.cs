// ***********************************************************************
// Assembly         : GUI
// Author           : ohad
// Created          : 05-16-2017
//
// Last Modified By : ohad
// Last Modified On : 05-21-2017
// ***********************************************************************
// <copyright file="SinglePlayerViewModel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using GUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModel
{
    /// <summary>
    /// Class SinglePlayerViewModel.
    /// </summary>
    /// <seealso cref="GUI.ViewModel.ViewModel" />
    public class SinglePlayerViewModel : ViewModel
    {
        /// <summary>
        /// The model
        /// </summary>
        public SingleClientModel model;
        /// <summary>
        /// The client
        /// </summary>
        private TcpClient client;
        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerViewModel"/> class.
        /// </summary>
        public SinglePlayerViewModel()
        {
            model = new SingleClientModel();
            client = model.Connect();
            model.Rows = Properties.Settings.Default.MazeRows;
            model.Cols = Properties.Settings.Default.MazeCols;
        }



        /// <summary>
        /// Generates the maze.
        /// </summary>
        public void GenerateMaze()
        {
            StringBuilder generateString = new StringBuilder();
            generateString.Append("generate " + MazeNameDefinition + " " + MazeRowsDefinition + " " + MazeColsDefinition);
            model.GetMaze(client, generateString.ToString());
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
            set { mazeNameDefinition = value; }
        }
    }
}
