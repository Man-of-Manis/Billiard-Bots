using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInputControl : MonoBehaviour {
    
    void Update() {
        //press y to change camera mode
        if (Input.GetKeyDown("joystick button 3")) {
            GetComponent<CameraController>().switchMode();
        }
        //press right bumper to cycle through static cams
        if (Input.GetKeyDown("joystick button 4")) { //left bumper
            GetComponent<CameraController>().cycleCamera("left");
        } else if (Input.GetKeyDown("joystick button 5")) { //right bumper
            GetComponent<CameraController>().cycleCamera("right");
        }
    }
}
