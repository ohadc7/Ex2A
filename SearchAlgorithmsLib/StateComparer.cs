// ***********************************************************************
// Assembly         : SearchAlgorithmsLib
// Author           : Ohad and Ido
// Created          : 04-05-2017
//
// Last Modified By : IDO1
// Last Modified On : 04-21-2017
// ***********************************************************************
// <copyright file="StateComparer.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Implementation of IComparer of States. compare states according to their cost.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IComparer{SearchAlgorithmsLib.State}" />
    internal class StateComparer : IComparer<State>
    {
        /// <summary>
        /// Compares the specified x and y according to their cost. 
        /// </summary>
        /// <param name="x">The first compared value.</param>
        /// <param name="y">The second compared value.</param>
        /// <returns>System.Int32.</returns>
        public int Compare(State x, State y)
        {
            if (x.Cost < y.Cost)
                return -1;
            if (x.Cost > y.Cost) 
                return 1;
            return 0;
        }
    }
}