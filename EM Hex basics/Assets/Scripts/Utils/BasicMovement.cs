using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class BasicMovement
{

	private static Vector2[] oddAdjacencies = new Vector2[] {
		new Vector2(0, -1), //Vector2.down,
		new Vector2(-1, 0), //Vector2.left,
		new Vector2(0, 1), //Vector2.up,
		new Vector2(1, 1), //Vector2.one,
		new Vector2(1,0), //Vector2.right,
		new Vector2(1, -1) //Vector2.right + Vector2.down
	};

	private static Vector2[] evenAdjacencies = new Vector2[] {
		new Vector2(-1, -1),
		new Vector2(-1, 0),
		new Vector2(-1, 1),
		new Vector2(0, 1),
		new Vector2(1, 0),
		new Vector2(0, -1)
	};

  public static HashSet<Vector3Int> GetMovePath(TerrainManager map, ArmyUnit unit) {
    Debug.Log(string.Format("Getting move path for map [{0}]", map.ToString()));
    Debug.Log(string.Format("Getting move path for unit [{0}]", unit.ToString()));
    HashSet<Vector3Int> viable = NeighbourPath(map, new HashSet<Vector3Int>(), unit.GetCurrentPos(), unit, unit.GetMobility());
    Debug.Log(string.Format("Viable path: {0}", LogHashSet(viable)));
    return viable;
  }

  private static HashSet<Vector3Int> NeighbourPath(TerrainManager map, HashSet<Vector3Int> visitedBefore, Vector3Int current, ArmyUnit unit, int mobilityLeft) {
    // if (IsZOC(map, unit, current)) {
    //   if (visitedBefore.Count > 0) {
    //     return visitedBefore;
    //   } else {
    //     return PathFromNeighbours(m, visitedBefore, current, unit, mobilityLeft);
    //   }
    // } else {
    //   return PathFromNeighbours(m, visitedBefore, current, unit, mobilityLeft);
    // }
    return (IsZOC(map, unit, current) && visitedBefore.Count > 0) ? visitedBefore : PathFromNeighbours(map, visitedBefore, current, unit, mobilityLeft);
  }

  private static HashSet<Vector3Int> PathFromNeighbours(TerrainManager map, HashSet<Vector3Int> visitedBefore, Vector3Int current, ArmyUnit unit, int mobilityLeft) {
    HashSet<Vector3Int> visited = visitedBefore;
    for (int i = 0; i < 6; i++) {
      Vector3Int neighbour = GetNeighbour(current, i);
      Territory t = map.GetTerritoryAt(neighbour);
      if ( t != null) {
        // t we can move
        Territory.Type type = t.GetTerritoryType();
        int cost = unit.TerrainCost(type);
        if (cost >= 0 && cost <= mobilityLeft && CanMoveOver(map, unit, current)) {
          visited.Add(current);
          visited = NeighbourPath(map, visited, neighbour, unit, mobilityLeft - cost);
        }
      }
    }
    return visited;
  }

  private static string LogHashSet(HashSet<Vector3Int> coords) {
    string log = "";
    foreach (Vector3Int h in coords) {
      log = string.Format("{0} [{1}],", log, h);
    }
    return log;
  }

// TODO: review this fn
  private static bool IsZOC(TerrainManager map, ArmyUnit unit, Vector3Int pos) {
    bool nearbyEnemy = false;
    for (int i = 0; i < 6; i++) {
      Vector3Int n = GetNeighbour(pos, i);
      ArmyUnit nUnit = map.GetUnitAt(n);
      if (nUnit != null && (nUnit.GetPlayer() == null || nUnit.GetPlayer().GetTeam() != unit.GetPlayer().GetTeam() )) {
        nearbyEnemy = true;
      }
    }
    Debug.Log(string.Format("Is ZoC: [{0}]", nearbyEnemy));
    return nearbyEnemy;
  }

	// TODO: Implement this
	public static bool CanMoveOver(TerrainManager map, ArmyUnit unit, Vector3Int pos) {
    bool enemy = false;
    ArmyUnit nUnit = map.GetUnitAt(pos);
    // check if unit has stealth or other things that allow passing through
    if (nUnit != null && (nUnit.GetPlayer() == null || nUnit.GetPlayer().GetTeam() != unit.GetPlayer().GetTeam() )) {
      enemy = true;
    }
    Debug.Log(string.Format("Can move over: [{0}]", enemy));
		// Check if ControlZone or something else
		return !enemy;
	}

	// Get the neighboor in position N where:
	// 0: lower left
	// 1: left
	// 2: upper left
	// 3: upper right
	// 4: right
	// 5: lower right
	public static Vector3Int GetNeighbour(Vector3Int pos, int i) {
		Vector2 aux = pos.y % 2 == 0? evenAdjacencies[i] : oddAdjacencies[i];
		Vector3Int adj = new Vector3Int((int)(pos.x + aux.x), (int)(pos.y + aux.y), 0);
		return adj;
	}
}
