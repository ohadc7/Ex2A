// ***********************************************************************
// Assembly         : Adapter
// Author           : Ohad and Ido
// Created          : 04-06-2017
//
// Last Modified By : IDO1
// Last Modified On : 04-21-2017
// ***********************************************************************
// <copyright file="SearchableMaze.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using MazeLib;
using SearchAlgorithmsLib;

namespace Adapter
{
    /// <summary>
    /// Class SearchableMaze. maze that we can to search its solution by SearchAlgorithmsLib.
    /// </summary>
    /// <seealso cref="SearchAlgorithmsLib.ISearchable" />
    public class SearchableMaze : ISearchable
    {
        /// <summary>
        /// The goal position
        /// </summary>
        private readonly PointState GoalPosition;
        /// <summary>
        /// The initial position
        /// </summary>
        private readonly PointState InitialPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchableMaze"/> class.
        /// </summary>
        /// <param name="maze">The maze.</param>
        public SearchableMaze(Maze maze)
        {
            MyMaze = maze;
            GoalPosition = new PointState(maze.GoalPos);
            InitialPosition = new PointState(maze.InitialPos);
        }

        /// <summary>
        /// Gets the searchable maze.
        /// </summary>
        /// <value> maze.</value>
        public Maze MyMaze { get; }

        /// <summary>
        /// Gets all possible cells in the maze that we can to arrive directly from the cell s to them.
        /// every possible maintain the cost to arrive from the initial position to it.
        /// </summary>
        /// <param name="s">The state.</param>
        /// <returns>List of accessible States.</returns>
        public List<State> getAllPossibleStates(State s)
        {
            var currentPosition = (s as PointState).CurrentPosition;
            var currRow = currentPosition.Row;
            var currCol = currentPosition.Col;
            var accessiblePositionStates = new List<State>();
            Position up, down, right, left;
            var costOfNeighbor = s.Cost + 1;
            if (currRow + 1 != MyMaze.Rows)
            {
                down = new Position(currRow + 1, currCol);
                if (CellType.Free == MyMaze[currRow + 1, currCol])
                    accessiblePositionStates.Add(new PointState(down, costOfNeighbor));
            }
            if (currRow != 0)
            {
                up = new Position(currRow - 1, currCol);
                if (CellType.Free == MyMaze[currRow - 1, currCol])
                    accessiblePositionStates.Add(new PointState(up, costOfNeighbor));
            } 
            if (currCol != 0)
            {
                left = new Position(currRow, currCol - 1);
                if (CellType.Free == MyMaze[currRow, currCol - 1])
                    accessiblePositionStates.Add(new PointState(left, costOfNeighbor));
            }
            if (currCol + 1 != MyMaze.Cols)
            {
                right = new Position(currRow, currCol + 1);
                if (CellType.Free == MyMaze[currRow, currCol + 1])
                    accessiblePositionStates.Add(new PointState(right, costOfNeighbor));
            }

            return accessiblePositionStates;
        }

        /// <summary>
        /// Gets the state of the wanted  goal.
        /// </summary>
        /// <returns>State.</returns>
        public State getGoalState()
        {
            return GoalPosition;
        }

        /// <summary>
        /// Gets the initial state of the problem.
        /// </summary>
        /// <returns>State.</returns>
        public State getInitialState()
        {
            return InitialPosition;
        }

        /// <summary>
        /// Equalses the SearchableMaze.
        /// </summary>
        /// <param name="sm">The sm.</param>
        /// <returns><c>true</c> if the name, initial and goal positions are equal, <c>false</c> otherwise.</returns>
        public bool Equals(SearchableMaze sm)
        {
            return MyMaze.ToString() == sm.MyMaze.ToString()
                   && GoalPosition.ToString() == sm.GoalPosition.ToString()
                   && InitialPosition.ToString() == sm.InitialPosition.ToString();
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="sm">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object sm)
        {
            return Equals(sm as SearchableMaze);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return MyMaze.GetHashCode() * 101 + GoalPosition.GetHashCode() * 103 + InitialPosition.GetHashCode() * 107;
        }
    }
}