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

namespace CSGameUtils
{
	/// <summary>
	/// Test ShortestPath class.
	/// </summary>
	public class SPTest //: MonoBehaviour
	{

		// Use this for initialization
		void Start ()
		{
			//Debug.Log("Test A START +++++");
			TestA();
			//Debug.Log("Test A END +++++\n");

			//Debug.Log("Test B START +++++");
			TestB();
			//Debug.Log("Test B END +++++\n");

			//Debug.Log("Test C START +++++");
			TestC();
			//Debug.Log("Test C END +++++\n");
		}

		/// <summary>
		/// Test A.
		/// Graph (same weight for all nodes):
		/// 
		///   B----A
		///    \  /
		///     \/
		///      C----D
		///      /\  /
		///     F  \/
		///    /    E
		///   G
		// Expected path: A -> C -> F -> G.
		// Expected path: 0 -> 2 -> 5 -> 6.
		/// </summary>
		public static void TestA()
		{
			SPNode nodeA = new SPNode(0, 0);
			SPNode nodeB = new SPNode(1, 1);
			SPNode nodeC = new SPNode(2, 1);
			SPNode nodeD = new SPNode(3, 1);
			SPNode nodeE = new SPNode(4, 1);
			SPNode nodeF = new SPNode(5, 1);
			SPNode nodeG = new SPNode(6, 1);

			nodeA.AddNeighbor(nodeB, nodeC);
			nodeB.AddNeighbor(nodeA, nodeC);
			nodeC.AddNeighbor(nodeA, nodeB, nodeD, nodeE, nodeF);
			nodeD.AddNeighbor(nodeC, nodeE);
			nodeF.AddNeighbor(nodeC, nodeG);
			nodeG.AddNeighbor(nodeF);

			List<SPNode> nodesList = new List<SPNode>()
			{
				nodeA,
				nodeB,
				nodeC,
				nodeD,
				nodeE,
				nodeF,
				nodeG
			};

			SPNode[] sp = ShortestPath.FindShortestPath(0, 6, nodesList);
			ShortestPath.DumpPath(new List<SPNode>(sp));
		}

		/// <summary>
		/// Test B.
		/// Graph (C weight is heavy than the other):
		/// 
		///   B----A
		///    \  / \
		///     \/   \
		///      C----D
		///      /\  /
		///     F  \/
		///    /    E
		///   G ---/
		// Expected path: A -> D -> E -> G
		// Expected path: 0 -> 3 -> 4 -> 6
		/// </summary>
		public static void TestB()
		{
			SPNode nodeA = new SPNode(0, 0);
			SPNode nodeB = new SPNode(1, 1);
			SPNode nodeC = new SPNode(2, 3);
			SPNode nodeD = new SPNode(3, 1);
			SPNode nodeE = new SPNode(4, 1);
			SPNode nodeF = new SPNode(5, 1);
			SPNode nodeG = new SPNode(6, 1);

			nodeA.AddNeighbor(nodeB, nodeC, nodeD);
			nodeB.AddNeighbor(nodeA, nodeC);
			nodeC.AddNeighbor(nodeA, nodeB, nodeD, nodeE, nodeF);
			nodeD.AddNeighbor(nodeA, nodeC, nodeE);
			nodeE.AddNeighbor(nodeC, nodeD, nodeG);
			nodeF.AddNeighbor(nodeC, nodeG);
			nodeG.AddNeighbor(nodeE, nodeF);

			List<SPNode> nodesList = new List<SPNode>()
			{
				nodeA,
				nodeB,
				nodeC,
				nodeD,
				nodeE,
				nodeF,
				nodeG
			};

			SPNode[] sp = ShortestPath.FindShortestPath(0, 6, nodesList);
			ShortestPath.DumpPath(new List<SPNode>(sp));
		}


		/// <summary>
		/// Test C.
		/// Graph (# represents heavy weight paths. heavy nodes are D and H.):
		/// 
		///   A----B----C
		///   #    | \  |
		///   #    |  \ |
		///   D ## E----F
		///   #    #    |
		///   #    #    |
		///   G ## H ###I
		// Expected path: A -> B -> F -> I
		// Expected path: 0 -> 1 -> 5 -> 8
		/// </summary>
		public static void TestC()
		{
			SPNode nodeA = new SPNode(0, 1);
			SPNode nodeB = new SPNode(1, 1);
			SPNode nodeC = new SPNode(2, 1);
			SPNode nodeD = new SPNode(3, 3);
			SPNode nodeE = new SPNode(4, 1);
			SPNode nodeF = new SPNode(5, 1);
			SPNode nodeG = new SPNode(6, 1);
			SPNode nodeH = new SPNode(7, 3);
			SPNode nodeI = new SPNode(8, 1);

			nodeA.AddNeighbor(nodeB, nodeD);
			nodeB.AddNeighbor(nodeA, nodeC, nodeE, nodeF);
			nodeC.AddNeighbor(nodeB, nodeF);
			nodeD.AddNeighbor(nodeA, nodeE, nodeG);
			nodeE.AddNeighbor(nodeB, nodeD, nodeF, nodeH);
			nodeF.AddNeighbor(nodeC, nodeE, nodeI);
			nodeG.AddNeighbor(nodeD, nodeH);
			nodeH.AddNeighbor(nodeE, nodeG, nodeI);
			nodeI.AddNeighbor(nodeF, nodeH);

			List<SPNode> nodesList = new List<SPNode>()
			{
				nodeA,
				nodeB,
				nodeC,
				nodeD,
				nodeE,
				nodeF,
				nodeG,
				nodeH,
				nodeI
			};
			
			SPNode[] sp = ShortestPath.FindShortestPath(0, 8, nodesList);
			ShortestPath.DumpPath(new List<SPNode>(sp));
		}
	}
} // namespace CSGameUtils