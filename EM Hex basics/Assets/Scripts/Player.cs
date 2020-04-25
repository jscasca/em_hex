
public class Player {

  private string name;
  private int team;
  
  public Player(string name, int team) {
    this.name = name;
    this.team = team;
  }

  public string getName() {
    return name;
  }

  public int getTeam() {
    return team;
  }
}