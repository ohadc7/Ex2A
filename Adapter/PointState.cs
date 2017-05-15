// ***********************************************************************
// Assembly         : Adapter
// Author           : Ohad and Ido
// Created          : 04-06-2017
//
// Last Modified By : IDO1
// Last Modified On : 04-21-2017
// ***********************************************************************
// <copyright file="PointState.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MazeLib;
using SearchAlgorithmsLib;

namespace Adapter
{
    /// <summary>
    /// Class PointState.
    /// </summary> 
    /// <seealso cref="SearchAlgorithmsLib.State" />
    public class PointState : State
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PointState"/> class.
        /// </summary>
        /// <param name="currentPosition">The current position.</param>
        public PointState(Position currentPosition)
        {
            CurrentPosition = currentPosition;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointState"/> class.
        /// </summary>
        /// <param name="currentPosition">The current position.</param>
        /// <param name="cost">The cost.</param>
        public PointState(Position currentPosition, double cost)
        {
            CurrentPosition = currentPosition;
            Cost = cost;
        }

        /// <summary>
        /// Gets the current position.
        /// </summary>
        /// <value>The current position.</value>
        public Position CurrentPosition { get; }

        /// <summary>
        /// Gets or sets the position that we came from to this position. It will be set by the searcher.
        /// </summary>
        /// <value>The point-state that the current point-state came from.</value>
        public State CameFrom { get; set; }

        /// <summary>
        /// Equalses the specified state.
        /// </summary>
        /// <param name="s">The state.</param>
        /// <returns><c>true</c> if the row and the column is the same, <c>false</c> otherwise.</returns>
        public bool Equals(State s)
        {
            var pointState = s as PointState;
            return pointState != null && CurrentPosition.Row == pointState.CurrentPosition.Row &&
                   CurrentPosition.Col == pointState.CurrentPosition.Col;
        }

        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        /// <value>The cost of passing from the initial state to this state. It has to be defined by the searchable maze</value>
        public double Cost { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this position.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this position.</returns>
        public override string ToString()
        {
            return string.Format("{0},{1}", CurrentPosition.Row, CurrentPosition.Col);
        }


        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="s">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object s)
        {
            return Equals(s as State);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return CurrentPosition.GetHashCode();
        }
    }
}