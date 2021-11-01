using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    // Initialize Vars
    // Store state of square
    public bool clicked = false;

    // prefab reference
    public GameObject the_pfCell;

    // num of grid cells
    private const int NUM_CELLS = 2160;

    // array of game objects
    public GameObject[] grid = new GameObject[NUM_CELLS];

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

        int cols = 60;

        // the x increment is calculated as a function of the local scale of the cube
        // to that they are placed exactly side by side
        float xIncrement = the_pfCell.transform.localScale.x;

        for(int i = 0; i < NUM_CELLS; i++)
        {
            if(i == 1 * cols || i == 2 * cols
            || i == 3 * cols || i == 4 * cols
            || i == 5 * cols || i == 6 * cols
            || i == 7 * cols || i == 8 * cols
            || i == 9 * cols || i == 10 * cols
            || i == 11 * cols || i == 12 * cols
            || i == 13 * cols || i == 14 * cols
            || i == 15 * cols || i == 16 * cols
            || i == 17 * cols || i == 18 * cols
            || i == 19 * cols || i == 20 * cols
            || i == 21 * cols || i == 22 * cols
            || i == 23 * cols || i == 24 * cols
            || i == 25 * cols || i == 26 * cols
            || i == 27 * cols || i == 28 * cols
            || i == 29 * cols || i == 30 * cols
            || i == 31 * cols || i == 32 * cols
            || i == 33 * cols || i == 34 * cols
            || i == 35 * cols) {
                x = 0;
                y += xIncrement; 
            }
            // instantiate a prefab game object
            GameObject go = Instantiate(the_pfCell);
            // color material
            go.GetComponent<Renderer>().material.color = Color.black;
            // default position
            go.transform.position = new Vector3(x, y, z);
            // increment the x position
            x += xIncrement;
            // give a name!
            go.name = "cube" + i;
            // set a child of this waveform
            go.transform.parent = this.transform;
            // put into array
            grid[i] = go; 
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

        // move playhead
        // update the playhead using info from ChucK's playheadPos
        grid[m_ckCurrentCell.GetCurrentValue() + 360].GetComponent<Renderer>().material.color = Color.white;

        // make previous cell black
        if(m_ckCurrentCell.GetCurrentValue() == 1) {
            grid[360].GetComponent<Renderer>().material.color = Color.black;
        } else if(m_ckCurrentCell.GetCurrentValue() == 60) {
            grid[419].GetComponent<Renderer>().material.color = Color.black;
        } else {
            grid[m_ckCurrentCell.GetCurrentValue() + 360 - 1].GetComponent<Renderer>().material.color = Color.black;
        }
    }
}
