// ***********************************************************************
// Assembly         : SearchAlgorithmsLib
// Author           : Ohad and Ido
// Created          : 04-16-2017
//
// Last Modified By : IDO1
// Last Modified On : 04-21-2017
// ***********************************************************************
// <copyright file="GenericState.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{ 
    /// <summary>
    /// Class GenericState. generic state for someone that wants to use SearchAlgorithmsLib
    /// but doesn't want to implement the state interface by himself.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.State" />
    public class GenericState<T> : State
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericState{T}"/> class. private constructor!
        /// </summary>
        /// <param name="stateContent">Content of the state. generic .</param>
        private GenericState(T stateContent)
        {
            StateContent = stateContent;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericState{T}"/> class.
        /// </summary>
        /// <param name="stateContent">Content of the state.</param>
        /// <param name="cost">The cost to arrive to the state from the initial state.</param>
        public GenericState(T stateContent, double cost)
        {
            StateContent = stateContent;
            Cost = cost;
        }

        /// <summary>
        /// Gets the content of the state.
        /// </summary>
        /// <value>The content of the state.</value>
        public T StateContent { get; }
        /// <summary>
        /// Gets or sets the cameFrom state. It will be set by the searcher.
        /// </summary>
        /// <value>The state that the current state came from.</value>
        public State CameFrom { get; set; }
        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        /// <value>The cost of passing from the 'CameFrom' state to this state. It has to be defined by the searchable that it belongs to</value>
        public double Cost { get; set; }

        /// <summary>
        /// Equalses the specified state.
        /// </summary>
        /// <param name="s">The state.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Equals(State s)
        {
            return Equals(s as GenericState<T>);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="s">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object s)
        {
            return Equals(s as GenericState<T>);
        }

        /// <summary>
        /// Equalses the specified s according to its content.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Equals(GenericState<T> s)
        {
            return StateContent.Equals(s.StateContent);
        }

        /// <summary>
        /// Returns a hash code for this instance according to its content.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return StateContent.GetHashCode();
        }

        /// <summary>
        /// Class StatePool. pool of states that maintain states that have already created.
        /// </summary>
        public static class StatePool
        {
            /// <summary>
            /// The states hash table for finding a state efficiently if we have already created it.
            /// </summary>
            private static readonly Dictionary<T, GenericState<T>> StatesHashTable =
                new Dictionary<T, GenericState<T>>();

            /// <summary>
            /// Gets the state.
            /// </summary>
            /// <param name="stateContent">Content of the state.</param>
            /// <returns>GenericState&lt;T&gt;.</returns>
            public static GenericState<T> GetState(T stateContent)
            {
                if (StatesHashTable.ContainsKey(stateContent))
                    return StatesHashTable[stateContent];
                var newState = new GenericState<T>(stateContent);
                StatesHashTable.Add(stateContent, newState);
                return newState;
            }
        }
    }
}