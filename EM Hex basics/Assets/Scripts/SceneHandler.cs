using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneHandler {

  private static Dictionary<string, GameContext> parameters;

  public static void Load(string sceneName, Dictionary<string, GameContext> ctx) {
    SceneHandler.parameters = ctx;
    SceneManager.LoadScene(sceneName);
  }

  public static void Load(string sceneName, string key, GameContext value) {
    SceneHandler.parameters = new Dictionary<string, GameContext>();
    SceneHandler.parameters.Add(key, value);
    SceneManager.LoadScene(sceneName);
  }

  public static GameContext getParam(string name) {
    if (parameters == null) {
      return null;
    } else {
      return parameters[name];
    }
  }

  public static void setParam(string key, GameContext value) {
    if (parameters == null) {
      SceneHandler.parameters = new Dictionary<string, GameContext>();
    }
    SceneHandler.parameters.Add(key, value);
  }
}