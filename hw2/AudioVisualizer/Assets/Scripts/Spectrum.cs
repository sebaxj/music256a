using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Why is the magnitude spectrum half of the time domain
//  

//-----------------------------------------------------------------------------
// name: Spectrum.cs
// desc: set up and draw the spectrum
//-----------------------------------------------------------------------------
public class Spectrum : MonoBehaviour
{
    // prefab reference
    public GameObject the_pfCube;

    // array of cubes
    public GameObject[,] the_cubes = new GameObject[32, 512];

    // spectrum history matrix
    public float[,] history = new float[32, 512];

    // Start is called before the first frame update
    void Start()
    {
        // x, y, z
        float x = -512, y = 0, z = 0;
        // increment
        float xIncrement = the_pfCube.transform.localScale.x * 2;
        // place the cubes initially
        for(int i = 0; i < 32; i++) {
            for (int j = 0; j < 512; j++) {
                // instantiate
                GameObject go = Instantiate(the_pfCube);
                
                // color material
                go.GetComponent<Renderer>().material.SetColor("_BaseColor", new Color(256, 256, 256));
                // transform it
                go.transform.position = new Vector3(x, y, z);
                // increment x
                x += xIncrement;
                // scale it to be 2x wider
                go.transform.localScale = new Vector3(2, 0, 1);
                // give it name
                go.name = "bin" + i + ":" + j;
                // set this as a child of this Spectrum
                go.transform.parent = this.transform;
                // put into array
                the_cubes[i, j] = go;
            }
            x = -512;
        }

        // position this
        this.transform.position = new Vector3(this.transform.position.x, -257, this.transform.position.z);

        // scale the spectrum
        this.transform.localScale = new Vector3(0.895f, 2, 1);
        // this.transform.localScale = new Vector3(0.8f, .9f, .8f);
    }


    // Update is called once per frame
    void Update()
    {
        // local reference to the spectrum
        float[] spectrum = ChunityAudioInput.the_spectrum;

        // move spectrum history over by one index to make room for newest
        for(int i = 30; i >= 0; i--) {
            for(int j = 0; j < 512; j++) {
                // move array of [i, j] to [i + 1, j]
                history[i + 1, j] = history[i, j]; 
            }
        }

        // move newest array into history[0,0]
        for(int i = 0; i < 512; i++) {
            history[0, i] = 600 * Mathf.Sqrt(spectrum[i]);
        }

        // loop through history and render
        float yOffset = 0f;
        for(int i = 0; i < 31; i++) {
            for(int j = 0; j < 512; j++) {
                the_cubes[i, j].transform.localScale =
                    new Vector3(the_cubes[i, j].transform.localScale.x,
                    history[i, j],
                    the_cubes[i, j].transform.localScale.z);
                the_cubes[i, j].transform.localPosition =
                    new Vector3(the_cubes[i, j].transform.localPosition.x,
                    ((history[i, j])/2) + yOffset,
                    the_cubes[i, j].transform.localPosition.z);
            }
            yOffset += 2f;
        }
    } 
}
