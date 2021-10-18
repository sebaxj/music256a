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
    public GameObject[] the_cubes = new GameObject[512];

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
        for ( int i = 0; i < the_cubes.Length; i++ )
        {
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
            go.name = "bin" + i;
            // set this as a child of this Spectrum
            go.transform.parent = this.transform;
            // put into array
            the_cubes[i] = go;
        }

        // position this
        this.transform.position = new Vector3(this.transform.position.x, -257, this.transform.position.z);

        // scale the spectrum
        this.transform.localScale = new Vector3(0.895f, 2, 1);
    }

    // Update is called once per frame
    void Update()
    {
        // local refernce to the spectrum
        float[] spectrum = ChunityAudioInput.the_spectrum;

        // deal with spectrum history

        for( int i = 0; i < the_cubes.Length; i++ )
        {
            float y = 600 * Mathf.Sqrt(spectrum[i]);
            the_cubes[i].transform.localScale =
                new Vector3(the_cubes[i].transform.localScale.x,
                y,
                the_cubes[i].transform.localScale.z);
            the_cubes[i].transform.localPosition =
                new Vector3(the_cubes[i].transform.localPosition.x,
                y/2,
                the_cubes[i].transform.localPosition.z);
        }
    }
}
