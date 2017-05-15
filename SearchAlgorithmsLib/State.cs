// ***********************************************************************
// Assembly         : SearchAlgorithmsLib
// Author           : Ohad and Ido
// Created          : 04-05-2017
//
// Last Modified By : IDO1
// Last Modified On : 04-21-2017
// ***********************************************************************
// <copyright file="State.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Interface State. It is a vertex in graph of states that the SearchAlgorithmsLib search in.
    /// </summary>
    public interface State
    {

        /// <summary> 
        /// Equalses the specified state.
        /// </summary>
        /// <param name="s">The state.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool Equals(State s); // we overload Object's Equals method

        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        /// <value>The cost of passing from the initial state to this state. It has to be defined by the searchable that it belongs to </value>
        double Cost { get; set; }

        /// <summary>
        /// Gets or sets the came from. It will be set by the searcher.
        /// </summary>
        /// <value>The state that the current state came from.</value>
        State CameFrom { get; set; }

    }
}