// ***********************************************************************
// Assembly         : SearchAlgorithmsLib
// Author           : Ohad and Ido
// Created          : 04-05-2017
//
// Last Modified By : IDO1
// Last Modified On : 04-21-2017
// ***********************************************************************
// <copyright file="ISearchable.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{

    /// <summary> 
    /// Interface ISearchable. represents a problem: how can we arrive from initial state to the goal state.
    /// the problem is represented by a graph with list of neighbors of every vertex.
    /// everyone that want to use the SearchAlgorithmsLib to solve his problem has to implement this interface.
    /// </summary>
    public interface ISearchable
    {
        /// <summary>
        /// Gets the initial state of the problem.
        /// </summary>
        /// <returns>State.</returns>
        State getInitialState();
        
        /// <summary>
        /// Gets the state of the wanted  goal.
        /// </summary>
        /// <returns>State.</returns>
        State getGoalState();

        /// <summary>
        /// Gets all possible states that we can to arrive directly from the state s to them.
        /// every possible state has to maintain the cost to arrive from the initial state to it.
        /// </summary>
        /// <param name="s">The state.</param>
        /// <returns>List&lt;State&gt;.</returns>
        List<State> getAllPossibleStates(State s);
    }
}