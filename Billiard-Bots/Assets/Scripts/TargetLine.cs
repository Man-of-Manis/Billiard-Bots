using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLine : MonoBehaviour {

    private int numC;
    private GameObject canvas;
    private GameObject[] dots;

    void Start () {

        canvas = transform.GetChild(0).gameObject;
        numC = canvas.transform.childCount;
        dots = new GameObject[numC];

        for (int i = 0; i < numC; i++) {
            dots[i] = canvas.transform.GetChild(i).gameObject;
        }

    }

    // Update is called once per frame
    void Update() {

        int dotsToHide;

        RaycastHit hitPoint;

        if (Physics.Raycast(transform.parent.position, transform.parent.forward, out hitPoint, 10)) {
            if (hitPoint.distance < 10) {
                dotsToHide = numC - (int)((hitPoint.distance / 0.5f) % numC);
                Debug.Log(hitPoint.distance);
                Debug.Log(dotsToHide);
            } else {
                dotsToHide = 0;
            }
            
        } else {
            dotsToHide = 0;
        }


        hideDots(dotsToHide);


    }


    private void hideDots (int h) {

        for (int i = 0; i < numC; i++) {
            bool active = !(i < h);
            
            canvas.transform.GetChild(i).gameObject.SetActive(active);

        }

    }



}
