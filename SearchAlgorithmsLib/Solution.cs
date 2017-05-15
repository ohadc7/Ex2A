// ***********************************************************************
// Assembly         : SearchAlgorithmsLib
// Author           : Ohad and Ido
// Created          : 04-05-2017
//
// Last Modified By : IDO1
// Last Modified On : 04-21-2017
// ***********************************************************************
// <copyright file="Solution.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Class Solution. maintains a sequence of states for arriving from the initial state to the goal state of the searchable.
    /// </summary> 
    public class Solution
    {
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path - list of states for arriving from the initial state to the goal state of the searchable..</value>
        public LinkedList<State> Path { get; set; }
    }
}