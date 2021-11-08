using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Clicker : MonoBehaviour
{
    // Initialize Vars //

    // button references
    public Button RESET;
    public Button SAVE;
    public Button DRONE;
    public Button LOAD;

    // prefab reference
    public GameObject the_pfCell;

    // number of columns and rows of the grid
    private const int NUM_COLS = 80;
    private const int NUM_ROWS = 48;

    // array of game objects
    public GameObject[,] grid = new GameObject[NUM_COLS, NUM_ROWS];

    // array of game objects to store saved sequence
    public int[,] SAVE1 = new int[NUM_COLS, NUM_ROWS];
    private int saved = 0; // to keep track of save-state
    private int SAVED = 0;



    // --------- AUDIO -------------
    // int sync
    private ChuckIntSyncer m_ckCurrentCell;

    // --------- SHARED ------------
    // col sync
    // row syn
    // reset indicator
    private int edit_COL;
    private int edit_ROW;
    private int reset = 0;
    private float droneGain = 0f;

    // int to keep track of which version of sequence we are playing
    private int version = 0;

    // Color variable to keep track of color of playhead before it is changed to white
    // in case it is yellow (yellow must persist as playhead moves through it)
    Color prev_Cell;

    // Start is called before the first frame update
    void Start()
    {    

        // x is axis which controls height of bottom to top (0 is origin)
        // z axis controls the depth of objects relative to the camera (pos is more backwards relative to the camera)
        // y axis controls width of objects from left to right
        float x = 0, y = 0, z = 0; 

        // the x increment is calculated as a function of the local scale of the quad
        // to that they are placed exactly side by side
        float Increment = the_pfCell.transform.localScale.x;

        int i = 0;

        for(int col = 0; col < NUM_COLS; col++) {
            for(int row = 0; row < NUM_ROWS; row++) {
                // instantiate a prefab game object
                GameObject go = Instantiate(the_pfCell);

                // color material
                go.GetComponent<Renderer>().material.color = Color.black;

                // default position
                go.transform.position = new Vector3(x, y, z);

                // increment the y position
                y += Increment;

                // give a name!
                go.name = "cube" + i;

                // set a child of this Game Object (Grid)
                go.transform.parent = this.transform;

                // put into array
                grid[col, row] = go; 

                // increment go count
                i++;
            }
            // reset y position
            y = 0f;

            // increment x position
            x += Increment;

        }
        
        // position this ('this' refers to the grid)
        this.transform.position = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);

        // run the sequencer
        GetComponent<ChuckSubInstance>().RunFile("ecg_audio.ck", true);

        // add the int sync
        m_ckCurrentCell = gameObject.AddComponent<ChuckIntSyncer>();
        m_ckCurrentCell.SyncInt(GetComponent<ChuckSubInstance>(), "cur_COL");

        // add button listeners
        RESET.onClick.AddListener(resetScreen);
        DRONE.onClick.AddListener(toggleDrone);
        SAVE.onClick.AddListener(saveSequence);
        LOAD.onClick.AddListener(loadSequence);
    }

    // Update is called once per frame
    void Update()
    {
        // edit
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // change color of selected cell
            if(Physics.Raycast(ray, out hit)) {
                Color color = hit.collider.GetComponent<Renderer>().material.color;
                if(color == Color.black || color == Color.white) {
                    hit.collider.GetComponent<Renderer>().material.color = Color.yellow;
                } else if(color == Color.yellow){
                    hit.collider.GetComponent<Renderer>().material.color = Color.black;
                }
            }

            // determine coordinate of selected cell
            for(int col = 0; col < NUM_COLS; col++) {
                for(int row = 0; row < NUM_ROWS; row++) {
                    if(grid[col, row].name == hit.collider.name) {
                        edit_COL = col;
                        edit_ROW = row;
                        goto LoopEnd;
                    }
                }
            }

            LoopEnd:

            // send edit to ChucK
            GetComponent<ChuckSubInstance>().SetInt("edit_COL", edit_COL);
            GetComponent<ChuckSubInstance>().SetFloat("droneGain", droneGain);
            GetComponent<ChuckSubInstance>().SetInt("reset", reset);
            GetComponent<ChuckSubInstance>().SetInt("save", saved);
            GetComponent<ChuckSubInstance>().SetInt("edit_ROW", edit_ROW);
            // GetComponent<ChuckSubInstance>().SetInt("version", version);
            GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened");
        }

        // move playhead
        grid[m_ckCurrentCell.GetCurrentValue(), 12].GetComponent<Renderer>().material.color = Color.white;

        for(int i = 0; i < NUM_COLS; i++) {
            if(i != m_ckCurrentCell.GetCurrentValue()) {
                grid[i, 12].GetComponent<Renderer>().material.color = Color.black;
            } 
        }
    }

    void resetScreen() {

        // tell ChucK to reset sound grid
        reset = 1;
        version = 0;
        GetComponent<ChuckSubInstance>().SetInt("reset", reset);
        // GetComponent<ChuckSubInstance>().SetInt("version", version);
        GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened");

        // reset all cells to black
        for(int col = 0; col < NUM_COLS; col++) {
            for(int row = 0; row < NUM_ROWS; row++) {
                grid[col, row].GetComponent<Renderer>().material.color = Color.black;
            }
        }

        // set reset to 0
        reset = 0;
    }

    void toggleDrone() {
        droneGain = droneGain == 0f ? 0.2f : 0f;

        GetComponent<ChuckSubInstance>().SetFloat("droneGain", droneGain);
        GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened");
    }

    void saveSequence() {
        saved = 1;
        SAVED = 1;
        GetComponent<ChuckSubInstance>().SetInt("save", saved);
        GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened");

        for(int col = 0; col < NUM_COLS; col++) {
            for(int row = 0; row < NUM_ROWS; row++) {
                SAVE1[col, row] = grid[col, row].GetComponent<Renderer>().material.color == Color.yellow ? 1 : 0;
            }
        }

        saved = 0;
        GetComponent<ChuckSubInstance>().SetInt("save", saved);
        GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened");
        resetScreen();
    }

    void loadSequence() {
        if(SAVED == 1) {
            resetScreen();
            version = 1;
            // GetComponent<ChuckSubInstance>().SetInt("version", version);
            GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened");

            for(int col = 0; col < NUM_COLS; col++) {
                for(int row = 0; row < NUM_ROWS; row++) {
                    if(SAVE1[col, row] == 1) {
                        grid[col, row].GetComponent<Renderer>().material.color = Color.yellow;
                    } else {
                        grid[col, row].GetComponent<Renderer>().material.color = Color.black;
                    }
                }
            }

        } else {
            EditorUtility.DisplayDialog("WARNING:", "You have no saved sequences :(", "OK, I will go make some!");
        }
    }
}
