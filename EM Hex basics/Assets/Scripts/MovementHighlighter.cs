using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementHighlighter : MonoBehaviour
{

    public Tilemap highlightmap;

    public Tile highlighted;
    public Tile inRange;

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
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HighlightPath(Vector3Int pos, int mobility, Dictionary<string, int> modifiers) {
        // fromt eh initial path we will travel in depth until we spend all mobility
        highlightmap.ClearAllTiles();
        for (int i = 0; i < 6; i++) {
            Vector2 aux = pos.y % 2 == 0 ? evenAdjacencies[i] : oddAdjacencies[i];
            Vector3Int adj = new Vector3Int((int)(pos.x + aux.x), (int)(pos.y + aux.y), 0);
            Tile s = Instantiate(highlighted);
            highlightmap.SetTile(adj, s);
        }

        /*
for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 10; j++) {
                //
                Vector3Int pos = new Vector3Int(i, j, 0);
                Tile t = Instantiate(plains);
                tilemap.SetTile(pos, t);
            }
        }
        */
    }
}
