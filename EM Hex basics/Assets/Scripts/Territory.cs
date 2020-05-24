
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

  public bool IsSummoningCircle() {
    return type == Type.CIRCLE;
  }

  public void SetController(Player p) {
    controller = p;
  }

  public bool IsControlled() {
    return controller != null;
  }
}