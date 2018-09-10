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
//using UnityEngine;
//using UnityEngine.Assertions;

namespace CSGameUtils
{
	/// <summary>
	/// Calculate the shortest path between two nodes.
	/// </summary>
	public static class ShortestPath
	{
		static List<T> CopyListItems<T>(List<T> nodesList) where T : SPNode
		{
			List<T> newNodesList = new List<T>();

			// Create nodes.
			for (var i = 0; i < nodesList.Count; i++)
            {
				var currNode = nodesList[i];
                var newNode = currNode.CopySelf() as T;
				newNodesList.Add(newNode);
			}

            // Set neighbors.
            for (var i = 0; i < nodesList.Count; i++)
            {
                var currNode = nodesList[i];
                var newNode = newNodesList[i];

                for (var j = 0; j < currNode.Neighbors.Count; j++)
                {
                    var newNeighborID = currNode.Neighbors[j].ID;
                    var neighbor = newNodesList.Find(n => n.ID == newNeighborID);
                    newNode.AddNeighbor(neighbor);
                }
            }

            return newNodesList;
		}

		/// <summary>
		/// Find the shortest path between originNodeID and targetNodeID in nodes Graph.
		/// </summary>
		/// <param name="originNodeID">Origin node.</param>
		/// <param name="targetNodeID">Target node.</param>
		/// <param name="nodes">Graph.</param>
		/// <returns></returns>
		public static T[] FindShortestPath<T>(int originNodeID, int targetNodeID, List<T> nodes) where T : SPNode
		{
            List<T> nodesToSearch = CopyListItems(nodes);

            var openList = new List<T>();
			var closedList = new List<T>();

			// Origin and target node must be in the graph.
			var originNode = nodesToSearch.Find(x => x.ID == originNodeID);
            var targetNode = nodesToSearch.Find(x => x.ID == targetNodeID);

			// Check if target node is conected with any other node.
			if (targetNode.Neighbors.Count == 0) return null;

			// Initialize the open list.
			GetNeighbors(originNode, openList, closedList);
        
			closedList.Add(originNode);
            var currNode = originNode;

			// Do while the target node is found in the closed list.
			while ((CheckNodeInList(targetNode, closedList) != true) && (openList.Count > 0)) {

                var nextNode = GetLeastCostNode(openList);

				currNode = nextNode;
				closedList.Add(currNode as T);

				GetNeighbors(currNode, openList, closedList);
			}
			//DumpPath(closedList);
			return BuildShortestPathList(targetNode, closedList);
		}

		/// <summary>
		/// Print the shortest path found. Debug method.
		/// </summary>
		public static void DumpPath(List<SPNode> list)
		{
			//Debug.Log("Path size: " + list.Count);

			// Forward.
			for (int i = 0; i < list.Count; i++) {
				SPNode curr = list[i];
				//Debug.Log("ID: " + curr.ID + " - prev node: " + ((curr.PreviousNode != null)? curr.PreviousNode.ID.ToString() : "none"));
			}
		}

		/// <summary>
		/// Build a list from the shortest path found.
		/// </summary>
		static T[] BuildShortestPathList<T>(T targetNode, List<T> list) where T : SPNode
		{
            var prevNode = targetNode.PreviousNode;
			List<T> spList = new List<T>();

			// If the target node wast found, return null.
			if (list.Find(x => x.ID == targetNode.ID) == null) return null;

			spList.Add(targetNode);

			while ((prevNode != null) && (prevNode != prevNode.PreviousNode)) {
				spList.Add(prevNode as T);
				prevNode = prevNode.PreviousNode;
			}

			spList.Reverse();

			return spList.ToArray();
		}

		/// <summary>
		/// Find the neighbors from a node.
		/// </summary>
		/// <param name="node">The node to be processed.</param>
		/// <param name="openList">The list to receive the neighbors.</param>
		static void GetNeighbors<T>(T node, List<T> openList, List<T> closedList) where T : SPNode
		{
			for (int i = 0; i < node.Neighbors.Count; i++)
            {
                var neighbor = node.Neighbors[i];

				if (closedList.Contains(neighbor as T)) continue;
				if (openList.Contains(neighbor as T)) continue;

				neighbor.PreviousNode = node;
				neighbor.WeightToReachFromPreviousNode = node.WeightToReachFromPreviousNode + neighbor.Weight;
                        
				openList.Add(neighbor as T);
			}
		}

		/// <summary>
		/// Checks if the node is present in the list.
		/// </summary>
		/// <param name="node">The node to look for.</param>
		/// <param name="closedList">The list to be searched.</param>
		/// <returns>true if the node is in the list; false otherwise</returns>
		static bool CheckNodeInList<T>(T node, List<T> closedList) where T : SPNode
		{
            var targetNode = closedList.Find(x => x.ID == node.ID);

			return (targetNode != null);
		}

		/// <summary>
		/// Finds the least cost node (remove it from list before returning).
		/// </summary>
		/// <returns>The least cost node in the list.</returns>
		static T GetLeastCostNode<T>(List<T> openList) where T : SPNode
		{
            var leastCostNode = openList[0];
			int leastCostNodeWeight = leastCostNode.WeightToReachFromPreviousNode;

			for (int i = 1; i < openList.Count; i++) {
                var nextNode = openList[i];

				int nextNodeWeight = nextNode.WeightToReachFromPreviousNode;
				if (nextNodeWeight < leastCostNodeWeight) {
					leastCostNode = nextNode;
					leastCostNodeWeight = leastCostNode.WeightToReachFromPreviousNode;
				}
			}

			openList.Remove(leastCostNode);

			return leastCostNode;
		}
	}
} // namespace CSGameUtils