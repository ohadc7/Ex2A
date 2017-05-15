// ***********************************************************************
// Assembly         : SearchAlgorithmsLib
// Author           : Ohad
// Created          : 04-05-2017
//
// Last Modified By : IDO1
// Last Modified On : 04-21-2017
// ***********************************************************************
// <copyright file="DepthFirstSearch.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{ 
    /// <summary>
    /// Class DepthFirstSearch.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.Searcher" />
    public class DepthFirstSearch<T> : Searcher
    {
        /// <summary>
        /// Searches the specified searchable by Depth First Search algorithm.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns>Solution.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override Solution search(ISearchable searchable)
        {
            var s = new Stack<State>();
            var initialState = searchable.getInitialState();
            var path = new LinkedList<State>();

            var discovered = new HashSet<State>();
            var solution = new Solution();
            solution.Path = path;
            s.Push(initialState);
            var sSize = s.Count;
            while (sSize > 0)
            {
                var v = s.Pop();
                evaluatedNodes++;
                if (v.Equals(searchable.getGoalState()))
                {
                    while (!v.Equals(initialState))
                    {
                        solution.Path.AddFirst(v);
                        v = v.CameFrom;
                    }
                    return solution;
                }

                if (!discovered.Contains(v))
                {
                    discovered.Add(v);
                    var successors = searchable.getAllPossibleStates(v);
                    foreach (var w in successors)
                    {
                        w.CameFrom = v;
                        s.Push(w);
                    }
                }
                sSize = s.Count;
            }
            Console.WriteLine("This Searchable doesn't have a solution and we didn't" +
                              "implement solutions for insolvable problems, so we throws an exception");
            throw new NotImplementedException();
        }
    }
}