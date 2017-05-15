// ***********************************************************************
// Assembly         : SearchAlgorithmsLib
// Author           : Ido
// Created          : 04-05-2017
//
// Last Modified By : IDO1
// Last Modified On : 04-21-2017
// ***********************************************************************
// <copyright file="BestFirstSearch.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Class BestFirstSearch.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.PQAdapter" />
    public class BestFirstSearch<T> : PQAdapter
    {
        /// <summary> 
        /// Initializes a new instance of the <see cref="BestFirstSearch{T}"/> class.
        /// </summary>
        public BestFirstSearch() : base(new StateComparer())
        {
        }

        /// <summary>
        /// Searches the specified searchable by best first search algorithm.
        /// I described my pseudo code in https://piazza.com/class/j0gg3d51sy92qp?cid=35
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns>Solution.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override Solution search(ISearchable searchable)
        {
            IComparer<State> comperar = new StateComparer();
            var closeStates = new HashSet<State>();
            var initialState = searchable.getInitialState();
            openList.push(initialState);
            while (OpenListSize > 0)
            {
                var bestOpenState = popOpenList();
                closeStates.Add(bestOpenState);
                if (bestOpenState.Equals(searchable.getGoalState()))
                {
                    var bestPath = new LinkedList<State>();
                    while (!bestOpenState.Equals(initialState))
                    {
                        bestPath.AddFirst(bestOpenState);
                        bestOpenState = bestOpenState.CameFrom;
                    }
                    bestPath.AddFirst(initialState);
                    var solution = new Solution();
                    solution.Path = bestPath;
                    //return the solution when it is founded
                    return solution;
                }
                var successors = searchable.getAllPossibleStates(bestOpenState);
                foreach (var s in successors)
                    if (!closeStates.Contains(s))
                        if (!openList.DoesContain(s))
                        {
                            s.CameFrom = bestOpenState;
                            openList.push(s);
                        }
                        else
                        {
                            s.CameFrom = bestOpenState;
                            openList.TryToDecreaseTheKeyOfTheElement(s);
                        }
            }
            Console.WriteLine("This Searchable doesn't have a solution and we didn't" +
                              "implement solutions for insolvable problems, so we throw an exception");
            throw new NotImplementedException();
        }
    }
}