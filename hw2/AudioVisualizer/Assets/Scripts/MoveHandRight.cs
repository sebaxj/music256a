using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHandRight : MonoBehaviour
{
    private int x = 350;
    private int y = -26;
    private int z = -2;
    private int TYPE = 0;

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
        if(TYPE == 0) {
            if(this.transform.position.x + 1 > 100) {
                this.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z);
            }
        } else if(TYPE == 1) {
            if(this.transform.position.x - 1 < 370) {
                this.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z);
            }
        }
    }
}
