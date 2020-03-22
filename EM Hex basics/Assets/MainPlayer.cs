using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MainPlayer : MonoBehaviour
{
    public Grid grid;
    // public Tilemap terrainMap;
    public Tilemap terrainMap;
    public Tilemap figureField;
    public Tilemap auraField;
    bool hasMoved;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(string.Format("Co-ords of mouse is [X: {0} Y: {0}]", pos.x, pos.y));
            Vector3Int coord = grid.WorldToCell(pos);
            Debug.Log("Click on: " + coord.ToString());

            GameObject g = figureField.GetInstantiatedObject(coord);
            if (g == null) {
                Debug.Log("No inst object");
                // Open the menu for action? Check if the thing has the player etc...
                Summon(pos);
            } else {
                Debug.Log("Inst: " + g.ToString());
            }

            // check if there is a tile there?
        }
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            //
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                Camera.main.transform.Translate(0, 0, 1);
            } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                Camera.main.transform.Translate(0, 0, -1);
            }
        } else {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
            // move view
                Camera.main.transform.Translate (Input.GetAxisRaw("Horizontal")*2,Input.GetAxisRaw("Vertical")*2, 0);
            }
        }
    }

    void Summon(Vector3 pos) {
        //
        GameObject summon = new GameObject("new summon");
        summon.AddComponent<SpriteRenderer>();
        SpriteRenderer sr = summon.GetComponent<SpriteRenderer>();
        Sprite s = Resources.Load<Sprite>("sprites/archer");
        sr.sprite = s;
        Instantiate(summon, pos, Quaternion.identity);
    }
}
