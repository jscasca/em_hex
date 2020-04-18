using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitPiece : MonoBehaviour
{
    public Grid grid;

    private int startingHealth = 10;

    private int health;

    private int mobility;

    private string unitName;

    private List<string> classes;

    private Dictionary<string, int> mobilityModifiers;


    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(int dmg) {
      health -= dmg;
      if (health <= 0) {
        // destroy
      }
    }

    private void Die() {
      // ?? what about on death effects?

      // override in the inheritin class wih `protected override void Die(){}`
    }
}
