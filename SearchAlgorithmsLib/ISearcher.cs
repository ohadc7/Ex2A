// ***********************************************************************
// Assembly         : SearchAlgorithmsLib
// Author           : Ohad and Ido
// Created          : 04-05-2017
//
// Last Modified By : IDO1
// Last Modified On : 04-21-2017
// ***********************************************************************
// <copyright file="ISearcher.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Interface ISearcher
    /// </summary> 
    public interface ISearcher
    {

        /// <summary>
        /// the search method. Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns>Solution.</returns>
        Solution search(ISearchable searchable);

        /// <summary>
        /// Gets the number of nodes evaluated. get how many nodes were evaluated by the algorithm.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int getNumberOfNodesEvaluated();
    }
}