using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementHighlighter : MonoBehaviour
{

	public Tilemap highlightmap;

	public Tile highlighted;
	public Tile inRange;

	private TerrainManager map;

	private Vector2[] oddAdjacencies = new Vector2[] {
		new Vector2(0, -1), //Vector2.down,
		new Vector2(-1, 0), //Vector2.left,
		new Vector2(0, 1), //Vector2.up,
		new Vector2(1, 1), //Vector2.one,
		new Vector2(1,0), //Vector2.right,
		new Vector2(1, -1) //Vector2.right + Vector2.down
	};

	private Vector2[] evenAdjacencies = new Vector2[] {
		new Vector2(-1, -1),
		new Vector2(-1, 0),
		new Vector2(-1, 1),
		new Vector2(0, 1),
		new Vector2(1, 0),
		new Vector2(0, -1)
	};

	// Start is called before the first frame update
	void Start() {
		map = TerrainManager.instance;
	}

	// Update is called once per frame
	void Update() {}

	public void HighlightPath(Vector3Int pos, int team, int mobility, Dictionary<Territory.Type, int> modifiers) {
		// fromt eh initial path we will travel in depth until we spend all mobility
		// HashSet<Vector3Int> highlights = new HashSet<Vector3Int>();
		highlightmap.ClearAllTiles();
		// for (int i = 0; i < 6; i++) {
		// 	Vector2 aux = pos.y % 2 == 0 ? evenAdjacencies[i] : oddAdjacencies[i];
		// 	Vector3Int adj = new Vector3Int((int)(pos.x + aux.x), (int)(pos.y + aux.y), 0);
		// 	Tile s = Instantiate(highlighted);
		// 	highlightmap.SetTile(adj, s);
		// }
		HashSet<Vector3Int> highlights = GetViable(pos, team, mobility, modifiers, new HashSet<Vector3Int>());
		foreach( Vector3Int v in highlights) {
			Tile s = Instantiate(highlighted);
			highlightmap.SetTile(v, s);
		}
	}

	public HashSet<Vector3Int> GetViable(Vector3Int current, int team, int mobilityLeft, Dictionary<Territory.Type, int> mods, HashSet<Vector3Int> visitedBefore) {
		HashSet<Vector3Int> visited = visitedBefore;
		visited.Add(current);
		if (CanMoveOver(team)) {
			for (int i = 0; i < 6; i++) {
				Vector3Int neighbour = GetNeighbour(current, i);
				Territory t = map.GetTerritoryAt(neighbour);
				if (t != null) {
					Territory.Type type = t.GetTerritoryType();
					if (mods.ContainsKey(type)) {
						int cost = mods[type];
						if (cost <= mobilityLeft) {
								// move there and recalculate
								visited = GetViable(neighbour, team, mobilityLeft - cost, mods, visited);
						}
					}
				} // else just skip
			}
		}
		return visited;
	}

	// TODO: Implement this
	public bool CanMoveOver(int team) {
		// Check if ControlZone or something else
		return true;
	}

	// Get the neighboor in position N where:
	// 0: lower left
	// 1: left
	// 2: upper left
	// 3: upper right
	// 4: right
	// 5: lower right
	public Vector3Int GetNeighbour(Vector3Int pos, int i) {
		Vector2 aux = pos.y % 2 == 0? evenAdjacencies[i] : oddAdjacencies[i];
		Vector3Int adj = new Vector3Int((int)(pos.x + aux.x), (int)(pos.y + aux.y), 0);
		return adj;
	}
}
