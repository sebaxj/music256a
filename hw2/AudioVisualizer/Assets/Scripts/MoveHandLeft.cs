using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHandLeft : MonoBehaviour
{
    private int TYPE = 2;
    private int zRotation = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up")) {
            if(TYPE == 2) {
                TYPE = 1;
            } else if(TYPE == 0) {
                TYPE = 1;
            } else if(TYPE == 1) {
                TYPE = 0;
            }
        }

        if (Input.GetKey("down")) {
            this.transform.localPosition = new Vector3(-355, 29, -2);
            transform.rotation = Quaternion.identity;
            TYPE = 2;
        }
        
        if(TYPE == 1) {
            if(zRotation < 2) {
                this.transform.Rotate(new Vector3(0, 0, zRotation--));
            }
            if(this.transform.position.x < 105 && this.transform.position.y < 40) {
                this.transform.localPosition = new Vector3(this.transform.position.x + 5, this.transform.position.y + 5, this.transform.position.z);
            } else if(this.transform.position.x > -355 && this.transform.position.y > 29) {
                this.transform.localPosition = new Vector3(this.transform.position.x - 5, this.transform.position.y - 5, this.transform.position.z);
            }
        } else if(TYPE == 0) {
            if(zRotation > -2) {
                this.transform.Rotate(new Vector3(0, 0, zRotation++));
            }
            if(this.transform.position.x < 105 && this.transform.position.y > -40) {
                this.transform.localPosition = new Vector3(this.transform.position.x + 5, this.transform.position.y - 5, this.transform.position.z);
            } else if(this.transform.position.x > -355 && this.transform.position.y < 29) {
                this.transform.localPosition = new Vector3(this.transform.position.x - 5, this.transform.position.y + 5, this.transform.position.z);
            }
        }
    }
}
