using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GamePlayerSrc : MonoBehaviour
{
    public Grid grid;

    public Tilemap terrainMap;
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

            // if (g == null) {
            //     Debug.Log("No inst object");
            //     // Open the menu for action? Check if the thing has the player etc...
            // } else {
            //     Debug.Log("Inst: " + g.ToString());
            // }

            // check if there is a tile there?
        }
        if (Input.GetKey(KeyCode.LeftControl)) {
            //
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                Debug.Log("Zoom");
                Zoom(true);
            } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                Debug.Log("swosh");
                Zoom(false);
            }
        } else {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
            // move view
                Camera.main.transform.Translate (Input.GetAxisRaw("Horizontal")*2,Input.GetAxisRaw("Vertical")*2, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            Debug.Log("Zoom");
            Zoom(true);
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            Debug.Log("swosh");
            Zoom(false);
        }
    }

    void Zoom(bool zoomIn) {
        // Camera.main.transform.Translate(0, 0, zoomIn ? 1 : -1);
        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, Camera.main.orthographicSize + (zoomIn? 1: -1), 2);
    }
}
