using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainEngineSrc : MonoBehaviour
{
    public Tilemap tilemap;

    public Tile plains;
    public Tile swamps;
    public Tile forests;
    public Tile mountains;
    public Tile waters;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake() {
        Debug.Log("Init terrain tilemap");
        for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 10; j++) {
                //
                Vector3Int pos = new Vector3Int(i, j, 0);
                Tile t = Instantiate(plains);
                tilemap.SetTile(pos, t);
            }
        }
    }
}
