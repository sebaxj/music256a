using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//-----------------------------------------------------------------------------
// name: Waveform.cs
// desc: set up and draw the audio waveform
//-----------------------------------------------------------------------------
public class Waveform : MonoBehaviour
{
    // prefab reference
    public GameObject the_pfCube;
    // array of game objects
    public GameObject[] the_cubes = new GameObject[1024];
    // controllable scale
    public float MY_SCALE = 200;

    // Start is called before the first frame update
    void Start()
    {
        float x = -512, y = 0, z = 0;
        // calculate
        float xIncrement = the_pfCube.transform.localScale.x;

        for( int i = 0; i < the_cubes.Length; i++ )
        {
            // instantiate a prefab game object
            GameObject go = Instantiate(the_pfCube);
            // color material
            go.GetComponent<Renderer>().material.SetColor("_BaseColor", new Color(.5f, 1, .5f));
            // default position
            go.transform.position = new Vector3(x, y, z);
            // increment the x position
            x += xIncrement;
            // give a name!
            go.name = "cube" + i;
            // set a child of this waveform
            go.transform.parent = this.transform;
            // put into array
            the_cubes[i] = go;
        }

        // position this
        this.transform.position = new Vector3(this.transform.position.x, 100, this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // local reference to the time domain waveform
        float[] wf = ChunityAudioInput.the_waveform;

        // position the cubes
        for( int i = 0; i < the_cubes.Length; i++ )
        {
            the_cubes[i].transform.localPosition =
                new Vector3(the_cubes[i].transform.localPosition.x,
                            MY_SCALE * wf[i],
                            the_cubes[i].transform.localPosition.z);
        }
    }
}
