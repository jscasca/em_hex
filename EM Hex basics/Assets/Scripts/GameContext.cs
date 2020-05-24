using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext {

  // This object should have all the values required to serialise and deserialise game objects including the history of commands, current board state, players, unit positions and so on

  private List<Player> players;
  private Player currentPlayer;
  private Dictionary<Vector3Int, Territory> map;
  private Dictionary<Vector3Int, IUnit> units;
  private GameConditions gameConds;

  public GameContext(List<Player> players,
    Player current,
    Dictionary<Vector3Int, Territory> map,
    Dictionary<Vector3Int, IUnit> units,
    GameConditions conds) {
    this.players = players;
    this.currentPlayer = current == null ? players[0] : current;
    this.map = map;
    this.units = units;
    this.gameConds = conds;
  }

  public List<Player> GetPlayers() {
    return players;
  }

  public Player GetCurrentPlayer() {
    return currentPlayer;
  }

  public Dictionary<Vector3Int, Territory> GetMap() {
    return map;
  }

  public Dictionary<Vector3Int, IUnit> GetUnits() {
    return units;
  }

  public GameConditions GetGameConditions() {
    return gameConds;
  }

  /************************************************************

  Below is code to test and generate default objects for testing

  *************************************************************/

  public static GameContext GetDefault() {
    List<Player> playerList = new List<Player>();
    Player p1 = new Player("myId", "Default", 1);
    Player p2 = new Player("noId", "Bot", 2);
    playerList.Add(p1);
    playerList.Add(p2);
    Player current = playerList[0];
    Dictionary<Vector3Int, Territory> defaultMap = new Dictionary<Vector3Int, Territory>();
    for (int i = 0; i < 10; i++) {
      for (int j = 0; j < 10; j++) {
        Vector3Int pos = new Vector3Int(i, j, 0);
        // Territory territory = new Territory((i % 2 == 0 && j % 2 == 0) ? Territory.Type.FOREST : Territory.Type.PLAIN);
        Territory territory = new Territory(GetRandomDefault(i, j));
        defaultMap.Add(pos, territory);
      }
    }

    Dictionary<Vector3Int, IUnit> units = new Dictionary<Vector3Int, IUnit>();
    GameConditions gm = new GameConditions();
    return new GameContext(playerList, null, defaultMap, units, gm);

  }

  public static Territory.Type GetRandomDefault(int i, int j) {
    if (i % 2 == 0) {
      if (j % 2 == 0) {
        return Territory.Type.FOREST;
      }
      if (i % 3 == 0) {
        return Territory.Type.SWAMP;
      }
    } else if (i % 3 == 0) {
      if (j % 2 == 0) {
        return Territory.Type.DESERT;
      }
      if (j % 3 == 0) {
        return Territory.Type.WATER;
      }
    } else if (i % 5 == 0 && j % 5 == 0) {
      return Territory.Type.MOUNTAIN;
    }
    if (i % 7 == 0) {
      if (j % 3 == 0) {
        return Territory.Type.CIRCLE;
      }
    }
    return Territory.Type.PLAIN;
  }

}