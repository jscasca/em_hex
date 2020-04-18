using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainManager : MonoBehaviour
{

  public static TerrainManager instance;
  public Tilemap map;

  public Tile plains;
  public Tile swamps;
  public Tile forests;
  public Tile mountains;
  public Tile waters;
  public Tile deserts;
  public Tile circle;

  public Dictionary<Vector3Int, Territory> tiles = new Dictionary<Vector3Int, Territory>();

  void Awake() {
    Debug.Log("Setting TerrainManager");
    if (instance == null) {
      instance= this;
    } else if (instance != this){
      Destroy(gameObject);
    }
  }

  private Tile GetTile(Territory t) {
    Tile tile;
    switch(t.GetTerritoryType()) {
      case Territory.Type.PLAIN: tile = Instantiate(plains); break;
      case Territory.Type.FOREST: tile = Instantiate(forests); break;
      case Territory.Type.MOUNTAIN: tile = Instantiate(mountains); break;
      case Territory.Type.SWAMP: tile = Instantiate(swamps); break;
      case Territory.Type.DESERT: tile = Instantiate(deserts); break;
      case Territory.Type.WATER: tile = Instantiate(waters); break;
      case Territory.Type.CIRCLE: tile = Instantiate(circle); break;
      default: tile = null; break;
    }
    return tile;
  }

  public void LoadMap(Dictionary<Vector3Int, Territory> TerritoryMap) {
    tiles = TerritoryMap;
    // foreach...
    foreach (KeyValuePair<Vector3Int, Territory> territory in tiles) {
      // tile.Key, tile.Value
      Debug.Log("Setting tile in: " + territory.Key);
      Tile tile = GetTile(territory.Value);
      map.SetTile(territory.Key, tile);
    }
  }

  public Dictionary<Vector3Int, Territory> GetMap() {
    return tiles;
  }

  public Territory GetTerritoryAt(Vector3Int pos) {
    //
    return tiles[pos];
  }

  public void SetTerritoryAt(Vector3Int pos, Territory t) {
    //
    tiles[pos] = t;
  }
}
