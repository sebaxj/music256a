using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// localPosition is the position of the transform relative 
// to the parent transform (Waveform (parent) -> cube (child))

// position is the position of te transform in world space

//-----------------------------------------------------------------------------
// name: Waveform.cs
// desc: set up and draw the audio waveform
//-----------------------------------------------------------------------------
public class Waveform : MonoBehaviour
{
    // constant to for the number of cubes (same as number of samples in time domain)
    // to store in the array
    private const int NUM_CUBES = 1024;

    private int TYPE = 0;

    // starting x scale of waveform parent
    private float SCALE = 0.42f;

    // prefab reference
    public GameObject the_pfCube;
    // array of game objects
    public GameObject[] the_cubes = new GameObject[NUM_CUBES];

    // controllable scale of the y-axis movement "amplification" of cubes
    public float MY_SCALE = 200;

    // Start is called before the first frame update
    void Start()
    {
        // x is axis which controls "width" of waveform from left to right
        // z axis controls the depth of objects relative to the camera (pos is more backwards)
        // y axis controls "height" of objects from top to bottom
        float x = -512, y = 0, z = 0; 

        // the x increment is calculated as a function of the local scale of the cube
        // to that they are placed exactly side by side
        float xIncrement = the_pfCube.transform.localScale.x;

        for( int i = 0; i < the_cubes.Length; i++ )
        {
            // instantiate a prefab game object
            GameObject go = Instantiate(the_pfCube);
            // color material
            go.GetComponent<Renderer>().material.SetColor("_BaseColor", new Color(255, 0, 228));
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

        // position this ('this' refers to the waveform)
        this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z - 1);

        // scale the waveform
        this.transform.localScale = new Vector3(SCALE, 2, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if(TYPE == 0) {
                TYPE = 1;
            } else if(TYPE == 1) {
                TYPE = 0;
            }
        }

        // local reference to the time domain waveform
        // contains 1024 floating point numbers with the magnitude of waveform
        float[] wf = ChunityAudioInput.the_waveform; 
        
        // multiply the waveform by sine function to create smooth wave
        for(int i = 0; i < wf.Length; i++) {
            wf[i] *= (float)(Math.Sin(i * (Math.PI / NUM_CUBES)));
        }

        // position the cubes
        for( int i = 0; i < the_cubes.Length; i++ )
        {
            the_cubes[i].transform.localPosition =
                new Vector3(the_cubes[i].transform.localPosition.x,
                            MY_SCALE * wf[i],
                            the_cubes[i].transform.localPosition.z);
            
            // if(TYPE == 0) {
            //     // scale the waveform
            //     if(SCALE <= 0.7f) {
            //         SCALE += 0.0001f;
            //     }
            // } else if(TYPE == 1) {
            //     // scale the waveform
            //     if(SCALE >= 0.2f) {
            //         SCALE -= 0.0001f;
            //     }
            // }
            this.transform.localScale = new Vector3(SCALE, 2, 1);
        }
    }
}


