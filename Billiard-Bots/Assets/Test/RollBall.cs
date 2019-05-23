using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBall : MonoBehaviour {

    public float f;

    void Update() {

        //Debug.Log(GetComponent<Rigidbody>().velocity);

        if (Input.GetKeyDown(KeyCode.F)) {
            GetComponent<Rigidbody>().AddForce(Vector3.forward * f);
            Debug.Log("adding force");
        }
        
    }
}
