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
namespace CSGameUtils
{
	/// <summary>
	/// Handle maze node data and heuristics calculation.
	/// </summary>
	public interface IAStarNode
	{
		/// <summary>
		/// Previous node.
		/// </summary>
		IAStarNode PrevNode { get; }

		/// <summary>
		/// Total cost to get in this node. (path cost + other heuristics cost).
		/// </summary>
		int TotalCost { get; }
	}
}
