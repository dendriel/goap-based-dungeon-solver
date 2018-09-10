/**
 * Copyright © 2017 Vitor Rozsa, vitor.rozsa@hotmail.com
 * 
 *	This file is part of Shortest Path.
 *
 *	Shortest Path is free software: you can redistribute it and/or modify
 *	it under the terms of the GNU General Public License as published by
 *	the Free Software Foundation, either version 3 of the License, or
 *	(at your option) any later version.
 *
 *	Shortest Path is distributed in the hope that it will be useful,
 *	but WITHOUT ANY WARRANTY; without even the implied warranty of
 *	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
 *	GNU General Public License for more details.
 *
 *	You should have received a copy of the GNU General Public License
 *	along with Shortest Path. If not, see<http://www.gnu.org/licenses/>.
 */
using System.Collections.Generic;

namespace CSGameUtils
{
	/// <summary>
	/// A node to be used in the ShortestPath class algorithm.
	/// </summary>
	public class SPNode
	{
		/// <summary>
		/// The node identification.
		/// </summary>
		public int ID { get; private set; }

		/// <summary>
		/// Weight to reach this node (from any other node).
		/// </summary>
		public int Weight { get; private set; }

		/// <summary>
		/// Best node to get in this node.
		/// </summary>
		public SPNode PreviousNode;

		/// <summary>
		/// Weight to reach from previous node.
		/// </summary>
		public int WeightToReachFromPreviousNode;

		/// <summary>
		/// The neighbors (links) of this node.
		/// </summary>
		public List<SPNode> Neighbors { get; private set; }

		/// <summary>
		/// Create a new node to be used in the shortest path graph.
		/// </summary>
		/// <param name="weight">The node's weight.</param>
		public SPNode(int id, int weight)
		{
			ID = id;
			Weight = weight;
			Neighbors = new List<SPNode>();
			PreviousNode = null;
			WeightToReachFromPreviousNode = 0;
		}

        protected SPNode(SPNode node)
        {
            ID = node.ID;
            Weight = node.Weight;
            Neighbors = new List<SPNode>();
            PreviousNode = null;
            WeightToReachFromPreviousNode = 0;
            //Neighbors = new List<SPNode>(node.Neighbors);
            //PreviousNode = node.PreviousNode;
            //WeightToReachFromPreviousNode = node.WeightToReachFromPreviousNode;
        }

		/// <summary>
		/// Add a neighbor to the neighbor's list.
		/// </summary>
		/// <param name="node"></param>
		public void AddNeighbor(params SPNode[] node)
		{
			for (int i = 0; i < node.Length; i++) {
				Neighbors.Add(node[i]);
			}
		}

        public virtual SPNode CopySelf()
        {
            var copy = new SPNode(ID, Weight);
            //copy.Neighbors = new List<SPNode>(Neighbors);
            //copy.PreviousNode = PreviousNode;
            //copy.WeightToReachFromPreviousNode = WeightToReachFromPreviousNode;

            return copy;
        }
	}
} // namespace CSGameUtils