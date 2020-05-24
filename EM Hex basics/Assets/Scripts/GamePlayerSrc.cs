using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GamePlayerSrc : MonoBehaviour
{
	public Grid grid;

	public Tilemap terrainMap;

	public MovementHighlighter path;

	private TerrainManager map;

	private string playerId = null;

	private int state = 0;

	private Vector3 _initialMousePosition = Vector3.zero;
	private Vector3 _prevMousePos = Vector3.zero;
	private bool _mouseDown = false;
	private float _pressedTime = 0.0f;
	private float _minDragTime = 1.0f;

	void Awake() {
		// set the id in cache
		Debug.Log("Setting id");
		string id = SceneHandler.GetMyId();
		Debug.Log("My id: " + id);
	}
	// Start is called before the first frame update
	void Start()
	{
		map = TerrainManager.instance;
	}

	bool IsMyPlayer(Player p) {
		if (playerId != null) {
			return p != null && p.GetId() == playerId;
		}
		return false;
	}

	void SelectCoord(Vector3Int coord) {
		// first check state
		switch(state) {
			case 1: OtherSelection(coord); break;
			default: BasicSelection(coord); break;
		}
	}

	void BasicSelection(Vector3Int coord) {
		ArmyUnit u = map.GetUnitAt(coord);
		Territory t = map.GetTerritoryAt(coord);
		if (u != null) {
			// Check the unit
			if (IsMyPlayer(u.GetPlayer())) {
					//
			}
		} else if(t != null) {
				// check for a base or do random GUI update
			if (t.IsSummoningCircle()) {
					//
			}
		}
	}

	void OtherSelection(Vector3Int coord) {
			//
	}

	// Update is called once per frame
	void Update() {
		Vector3 mousePosition = Input.mousePosition;
		if (_mouseDown) {
			Debug.Log("Dragging: ");
			// start dragging the map
			Slide(mousePosition.x - _prevMousePos.x, mousePosition.y - _prevMousePos.y, -0.08f);
			_prevMousePos = mousePosition;
		}
		if (Input.GetMouseButtonDown(0)) {
			if (!_mouseDown) {
				_mouseDown = true;
				_initialMousePosition = mousePosition;
				_prevMousePos = _initialMousePosition;
			}
		}
		if (Input.GetMouseButtonUp(0)) {
			_mouseDown = false;
			float distance = Vector3.Distance(mousePosition, _initialMousePosition);
			// Didnt drag, just clicked
			Debug.Log("Dragged : " + distance);
			if (distance == 0) {
				Vector3 pos = Camera.main.ScreenToWorldPoint(mousePosition);
				ClickOn(pos);
			}
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
				// Camera.main.transform.Translate (Input.GetAxisRaw("Horizontal")*2,Input.GetAxisRaw("Vertical")*2, 0);
				Slide(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 2);
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

	void Slide(float x, float y, float factor) {
		Camera.main.transform.Translate (x * factor, y * factor, 0);
	}

	void Zoom(bool zoomIn) {
		// Camera.main.transform.Translate(0, 0, zoomIn ? 1 : -1);
		Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, Camera.main.orthographicSize + (zoomIn? 1: -1), 2);
	}

	void ClickOn(Vector3 pos) {
		Vector3Int coord = grid.WorldToCell(pos);
		Debug.Log("Click on: " + coord.ToString());
		Territory t = map.GetTerritoryAt(coord);
		ArmyUnit u = map.GetUnitAt(coord);

		Player p = PlayerHandler.instance.GetCurrent();

		// Probably work on states
		// State 1: nothing selected
		// Staet 2: Selected unit (waiting on move/atk)
		// State 3: selected ability (waiting on target)
		// State 4: multipoint ability (waiting on n targets)
		if (u != null) {
			// getThe Actions?
			Debug.Log("Found unit!");
		}
		Dictionary<Territory.Type, int> mobMods = new Dictionary<Territory.Type, int>();
		mobMods.Add(Territory.Type.PLAIN, 3);
		mobMods.Add(Territory.Type.FOREST, 3);
		mobMods.Add(Territory.Type.MOUNTAIN, 6);
		mobMods.Add(Territory.Type.CIRCLE, 4);
		// Onliy highlight path on selecting unit
		path.HighlightPath(coord, 1, 12, mobMods);
	}
}
