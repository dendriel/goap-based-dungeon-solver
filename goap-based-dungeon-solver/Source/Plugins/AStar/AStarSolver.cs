/**
 * Copyright © 2017 Vitor Rozsa, vitor.rozsa@hotmail.com
 * 
 *	This file is part of A* Solver.
 *
 *	A* Solver is free software: you can redistribute it and/or modify
 *	it under the terms of the GNU General Public License as published by
 *	the Free Software Foundation, either version 3 of the License, or
 *	(at your option) any later version.
 *
 *	A* Solver is distributed in the hope that it will be useful,
 *	but WITHOUT ANY WARRANTY; without even the implied warranty of
 *	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
 *	GNU General Public License for more details.
 *
 *	You should have received a copy of the GNU General Public License
 *	along with Shortest Path. If not, see<http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace CSGameUtils
{
	/// <summary>
	/// Provides the A* algorithm.
	/// 
	/// From Wikipedia (https://en.wikipedia.org/wiki/A*_search_algorithm):
	/// Peter Hart, Nils Nilsson and Bertram Raphael of Stanford Research Institute (now
	/// SRI International) first described the algorithm in 1968. It is an extension
	/// of Edsger Dijkstra's 1959 algorithm. A* achieves better performance by using
	/// heuristics to guide its search.
	/// </summary>
	public abstract class AStarSolver
	{
		/// <summary>
		/// Sleep time between solver loops.
		/// </summary>
		protected const int DEFAULT_SLEEP_TIME_MS = 100;

		/// <summary>
		/// First node to visit when solving a maze.
		/// </summary>
		protected IAStarNode startingNode;

		/// <summary>
		/// Open list (frontier) nodes.
		/// </summary>
		protected List<IAStarNode> openList;

		/// <summary>
		/// Closed list (visited) nodes.
		/// </summary>
		protected List<IAStarNode> closedList;

		/// <summary>
		/// Amount of available nodes.
		/// </summary>
		public int OpenListCount { get { return openList.Count; } }

		/// <summary>
		/// Amount of visited nodes.
		/// </summary>
		public int ClosedListCount { get { return closedList.Count; } }

		/// <summary>
		/// Elapsed time since started solving a maze (in miliseconds).
		/// </summary>
		public long ElapsedTimeInMs { get; private set; }

		/// <summary>
		/// Current node being checked.
		/// </summary>
		public IAStarNode CurrNode { get; private set; }

		/// <summary>
		/// How much time to sleep between loops (when IsSleepBetweenLoopsEnabled is true).
		/// Default is DEFAULT_SLEEP_TIME_MS.
		/// </summary>
		public int SleepTimeInMs { get; set; }

		/// <summary>
		/// Solver must sleep between loops?
		/// </summary>
		protected Func<bool> IsSleepBetweenLoopsEnabled;
		
		/// <summary>
		/// Create a maze solver.
		/// </summary>
		/// <param name="_startingNode">Starting node.</param>
		/// <param name="_IsSleepBetweenLoopsEnabled">Func to check if solver should sleep (100 ms)
		/// between loops. Return true to sleep between loops.</param>
		public AStarSolver(IAStarNode _startingNode, Func<bool> _IsSleepBetweenLoopsEnabled)
		{
			startingNode = _startingNode;
			IsSleepBetweenLoopsEnabled = _IsSleepBetweenLoopsEnabled;

			SleepTimeInMs = DEFAULT_SLEEP_TIME_MS;
		}

		/// <summary>
		/// Find a solution for a maze. (Template Method Design Pattern).
		/// </summary>
		/// <returns>An ordered list (first to last) of nodes to be visited to reach a solution;
		/// if interrupted, returns an empty list.</returns>
		public IAStarNode[] FindSolution()
		{
			Stopwatch watch;
			ElapsedTimeInMs = 0;

			openList = new List<IAStarNode>();
			closedList = new List<IAStarNode>();

			InitializeOpenList();

			IAStarNode nextNode;
			do {
				watch = Stopwatch.StartNew();

				nextNode = FindLightestNode();
				closedList.Add(nextNode);
				CurrNode = nextNode;

				ReportProgress();

				if (IsSleepBetweenLoopsEnabled()) {
					Thread.Sleep(SleepTimeInMs);
				}

				if (HasFoundASolution()) {
					break;
				}

			    RefreshOpenList();

				IAStarNode[] neighbors = FindNeighbors();
				openList.AddRange(neighbors);

				watch.Stop();
				ElapsedTimeInMs += watch.ElapsedMilliseconds;

				if (IsCancelationPending()) {
					break;
				}
			} while (openList.Count > 0);


			IAStarNode[] solution = null;
			if (HasFoundASolution() || (IsCancelationPending() != true) && (openList.Count > 0)) {
				solution = BuildSolutionArray();

			} else {
				solution = new IAStarNode[0];
			}
			
			return solution;
		}

		/// <summary>
		/// Add the first node to be visited in the open list.
		/// </summary>
		void InitializeOpenList()
		{
			openList.Add(startingNode);
		}

		/// <summary>
		/// Find the node with best heuristic and retrieve it from the list.
		/// (will remove its reference from the list).
		/// </summary>
		/// <returns>The node with best heuristics in the list.</returns>
		IAStarNode FindLightestNode()
		{
			IAStarNode bestNode = openList[0];

			for (int i = 1; i < openList.Count; i++) {
				IAStarNode currNode = openList[i];
				if (currNode.TotalCost < bestNode.TotalCost) {
					bestNode = currNode;
				}
			}

			openList.Remove(bestNode);

			return bestNode;
		}

		/// <summary>
		/// Check if the current node being tested is a in a solution state. (Primitive Operation)
		/// </summary>
		/// <returns>true if found a solution; false otherwise.</returns>
		protected abstract bool HasFoundASolution();

		/// <summary>
		/// Find the valid neighbors (next states) from the current node. (Primitive Operation)
		/// </summary>
		/// <returns>A list of valid neighbors from the current node.</returns>
		protected abstract IAStarNode[] FindNeighbors();

        /// <summary>
        /// Call before FindNeighbors to refresh the open list.
        /// </summary>
	    protected abstract void RefreshOpenList();

		/// <summary>
		/// Retrieve all nodes of a solution starting from the last node. Reverse the solution so earlier
		/// nodes are in the starting positions. (Hook Operation)
		/// </summary>
		/// <returns>The nodes that compose the solution.</returns>
		protected virtual IAStarNode[] BuildSolutionArray()
		{
			List<IAStarNode> solution = new List<IAStarNode>();

			IAStarNode nextNode = CurrNode;

			// Create a method for this.
			while (nextNode != null && nextNode.PrevNode != null) {
				solution.Add(nextNode);
				nextNode = nextNode.PrevNode;
			}

            if (nextNode != null)
            {
                solution.Add(nextNode);
            }

			solution.Reverse();

			return solution.ToArray();
		}
		
		/// <summary>
		/// Report progress. (Hook Operation)
		/// May be kept empty.
		/// </summary>
		protected virtual void ReportProgress()
		{
			// Report any progress as needed (e.g.: update interface status).
		}

		/// <summary>
		/// Check if the solver must be interrupted. (Hook Operation)
		/// May always return false.
		/// </summary>
		/// <returns>true if the solver must be interrupted; false otherwise.</returns>
		protected virtual bool IsCancelationPending()
		{
			return false;
		}
	}
}
