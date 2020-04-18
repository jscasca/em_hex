using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

  void Awake() {
    // SceneHandler.getParam("<this game>")
  }

  void Start() {
    GameContext ctx = GetDefault();
    TerrainManager.instance.LoadMap(ctx.GetMap());
  }

  public GameContext GetDefault() {
    //
    return GameContext.GetDefault();
  }

}