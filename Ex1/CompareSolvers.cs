// ***********************************************************************
// Assembly         : Ex1
// Author           : Cohen
// Created          : 04-06-2017
//
// Last Modified By : Cohen
// Last Modified On : 04-21-2017
// ***********************************************************************
// <copyright file="CompareSolvers.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Adapter;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
 
namespace Ex1
{
    /// <summary>
    /// Class CompareSolvers.
    /// </summary>
    internal class CompareSolvers
    {
        /// <summary>
        /// Runs this instance.
        /// </summary>
        public static void Run()
        {
            var dfsMazeGenerator = new DFSMazeGenerator();
            var MyMaze = dfsMazeGenerator.Generate(20, 20);

            var s = MyMaze.ToString();

            Console.WriteLine(s);
            var SM = new SearchableMaze(MyMaze);

            Console.WriteLine("Start:" + new PointState(MyMaze.InitialPos));
            Console.WriteLine("Goal:" + new PointState(MyMaze.GoalPos));


            var DFS = new DepthFirstSearch<PointState>();
            var solutionDFS = DFS.search(SM);

            Console.WriteLine("The solution path by DFS is:");
            foreach (var state in solutionDFS.Path)
                Console.WriteLine(state.ToString());
            Console.WriteLine("The DFS algorithm treated " + DFS.evaluatedNodes + " nodes.");
            Console.WriteLine();
             

            var BFS = new BestFirstSearch<PointState>();
            var solution = BFS.search(SM);

            Console.WriteLine("The solution path by BFS is:");
            foreach (var state in solution.Path)
                Console.WriteLine(state.ToString());
            Console.WriteLine("The BFS algorithm treated " + BFS.evaluatedNodes + " nodes.");
            Console.WriteLine();
        }
    }
}