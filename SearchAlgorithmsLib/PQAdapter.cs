// ***********************************************************************
// Assembly         : SearchAlgorithmsLib
// Author           : Ohad and Ido
// Created          : 04-06-2017
//
// Last Modified By : IDO1
// Last Modified On : 04-21-2017
// ***********************************************************************
// <copyright file="PQAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    /// <summary> 
    /// Class PQAdapter. adapt the searcher to algorithms that use priority queue.
    /// </summary>
    /// <seealso cref="SearchAlgorithmsLib.Searcher" />
    public abstract class PQAdapter : Searcher
    {
        /// <summary>
        /// openList - priority queue of states.
        /// </summary>
        public PriorityQueue<State> openList;

        /// <summary>
        /// Initializes a new instance of the <see cref="PQAdapter"/> class.
        /// </summary>
        /// <param name="optionalSpecialComparer">The optional special comparer.</param>
        public PQAdapter(IComparer<State> optionalSpecialComparer)
        {
            openList = new PriorityQueue<State>(optionalSpecialComparer);
        }

        // a property of openList
        /// <summary>
        /// Gets the size of the open list.
        /// </summary>
        /// <value>The size of the open list.</value>
        public int OpenListSize => openList.Count;

        /// <summary>
        /// Pops the open list.
        /// </summary>
        /// <returns>the better State in the priority queue.</returns>
        public State popOpenList()
        {
            evaluatedNodes++;
            return openList.pop();
        }
    }
}