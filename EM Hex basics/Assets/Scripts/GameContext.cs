using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext {

  // This object should have all the values required to serialise and deserialise game objects including the history of commands, current board state, players, unit positions and so on

  private Dictionary<Vector3Int, Territory> map;

  public GameContext(Dictionary<Vector3Int, Territory> aMap) {
    map = aMap;
  }

  public Dictionary<Vector3Int, Territory> GetMap() {
    return map;
  }

  public static GameContext GetDefault() {
    Dictionary<Vector3Int, Territory> defaultMap = new Dictionary<Vector3Int, Territory>();
    for (int i = 0; i < 10; i++) {
      for (int j = 0; j < 10; j++) {
        Vector3Int pos = new Vector3Int(i, j, 0);
        Territory territory = new Territory((i % 2 == 0 && j % 2 == 0) ? Territory.Type.FOREST : Territory.Type.PLAIN);
        defaultMap.Add(pos, territory);
      }
    }
    return new GameContext(defaultMap);

  }

}