using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO
// 1. Refactor code to be on a matrix grid  
// 2. Fix ChucK code to properly play sequenced notes off the matrix grid.
// 3. Keep track of state of each cell with more variables to change speed, sound etc.
// 4. Fix glitches with cell colors
// 5. Add functionality with other vitals to add other instrument
// 6. Expand size of everything to have more cells (better resolution, longer sequence)
// 7. Fix click ability to use a "wand" technique to be able to draw
// 9. Fix colors with lights (yellow more yellow, black more black, white more white)
// 10. Refactor code
// 11. Make it look more "retro"

public class Clicker : MonoBehaviour
{
    // Initialize Vars
    // Store state of square
    public bool clicked = false;

    // prefab reference
    public GameObject the_pfCell;

    // number of columns and rows of the grid
    private const int NUM_COLS = 80;
    private const int NUM_ROWS = 48;

    // array of game objects
    public GameObject[,] grid = new GameObject[NUM_COLS, NUM_ROWS];

    // --------- AUDIO -------------
    // int sync
    private ChuckIntSyncer m_ckCurrentCell;

    // Start is called before the first frame update
    void Start()
    {    
        // x is axis which controls "width" of waveform from left to right
        // z axis controls the depth of objects relative to the camera (pos is more backwards)
        // y axis controls "height" of objects from top to bottom
        float x = 0, y = 0, z = 0; 

        // the x increment is calculated as a function of the local scale of the qud
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
        m_ckCurrentCell.SyncInt(GetComponent<ChuckSubInstance>(), "currentCell");
    }

    // Update is called once per frame
    void Update()
    {
        // edit
        if(Input.GetMouseButtonDown(0))
        {
            if(!clicked) { // if it is black (hasn't been clicked -> black)
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {
                    hit.collider.GetComponent<Renderer>().material.color = Color.yellow;
                }
                clicked = true;
            } else { // if it is black (has been clicked -> black)
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {
                    hit.collider.GetComponent<Renderer>().material.color = Color.black;
                }
                clicked = false;
            }
            // send edit
        }

        // // move playhead
        // // update the playhead using info from ChucK's playheadPos
        // grid[m_ckCurrentCell.GetCurrentValue() + 360].GetComponent<Renderer>().material.color = Color.white;

        // // make previous cell black
        // if(m_ckCurrentCell.GetCurrentValue() == 1) {
        //     grid[360].GetComponent<Renderer>().material.color = Color.black;
        // } else if(m_ckCurrentCell.GetCurrentValue() == 60) {
        //     grid[419].GetComponent<Renderer>().material.color = Color.black;
        // } else {
        //     grid[m_ckCurrentCell.GetCurrentValue() + 360 - 1].GetComponent<Renderer>().material.color = Color.black;
        // }
    }
}
