// ***********************************************************************
// Assembly         : GUI
// Author           : ohad
// Created          : 05-15-2017
//
// Last Modified By : ohad
// Last Modified On : 05-21-2017
// ***********************************************************************
// <copyright file="IClientModel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MazeLib;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Net.Sockets;

namespace GUI.Model
{
    /// <summary>
    /// Interface IClientModel
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public interface IClientModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>The name of the maze.</value>
        string MazeName
        {
            get;
            set
            ;
        }
        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>The rows.</value>
        int Rows
        {
            get;
            set
           ;
        }

        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>The cols.</value>
        int Cols
        {
            get;
            set
            ;
        }

        /// <summary>
        /// Gets or sets the maze string.
        /// </summary>
        /// <value>The maze string.</value>
        string MazeString
        {
            get;
            set
            ;
        }
        /// <summary>
        /// Gets or sets the initialize position.
        /// </summary>
        /// <value>The initialize position.</value>
        Position InitPosition
        {
            get;
            set
            ;
        }
        /// <summary>
        /// Gets or sets the goal position.
        /// </summary>
        /// <value>The goal position.</value>
        Position GoalPosition
        {
            get;
            set
           ;
        }
        /// <summary>
        /// Gets or sets the current position.
        /// </summary>
        /// <value>The current position.</value>
        Position CurrentPosition
        {
            get;
            set
           ;
        }
        /// <summary>
        /// Gets or sets the solve string.
        /// </summary>
        /// <value>The solve string.</value>
        string SolveString
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets a value indicating whether [finish game].
        /// </summary>
        /// <value><c>true</c> if [finish game]; otherwise, <c>false</c>.</value>
        bool FinishGame
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets a value indicating whether [solve finish].
        /// </summary>
        /// <value><c>true</c> if [solve finish]; otherwise, <c>false</c>.</value>
        bool SolveFinish
        {
            get;
            set;
        }

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        void NotifyPropertyChanged(string propName);

    }

}
