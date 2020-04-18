
public class Territory {
  //
  public enum Type {
    PLAIN,
    FOREST,
    MOUNTAIN,
    WATER,
    SWAMP,
    DESERT,
    CIRCLE
  }

  private Type type;

  private Player controller = null;
  
  public Territory(Type t) {
    type = t;
  }

  public Type GetTerritoryType() {
    return type;
  }

  public bool isBase() {
    return type == Type.CIRCLE;
  }

  public void setController(Player p) {
    controller = p;
  }

  public bool isControlled() {
    return controller != null;
  }
}