using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagTower : MonoBehaviour {

    /*
     * get player velocity
     * add/subtract the vector towards the tower
     * normalize the combined vector, then multiply by magnitude of original velocity
     */

    public float magneticForce; //base force in/out from the base
    public bool exponential; //strength of the force is increased based on proximity to the center
    public enum Polarity { Positive, Negative};
    public Polarity p;
    public float radius;
    public LayerMask affectedObjects;

    private Vector3 v; //velocity
    private Vector3 toCenter;
    private Collider[] hitColliders;
    private float forceMod = 1;
    private float polarityMod = 1;

    private void Update () {
        
        hitColliders = Physics.OverlapSphere(transform.position, radius, affectedObjects);

        foreach (Collider c in hitColliders) {
            
            v = c.gameObject.GetComponent<Rigidbody>().velocity;
            //toCenter = new Vector3(v.x - transform.position.x, v.y, v.z - transform.position.z);
            toCenter = new Vector3(c.gameObject.transform.position.x - transform.position.x, 0, c.gameObject.transform.position.z - transform.position.z);

            
            if (exponential) {
                forceMod = ((radius - toCenter.magnitude)/radius) * ((radius - toCenter.magnitude) / radius);
            } else {
                forceMod = (radius - toCenter.magnitude) / radius;
            }

            Debug.Log(this.name + " force mod: " + forceMod);

            if (p == Polarity.Positive) { //if (p == player polarity)
                polarityMod = -1;
            } else {
                polarityMod = 1;
            }

            toCenter = toCenter.normalized * polarityMod * magneticForce * forceMod;

            v = new Vector3(v.x + toCenter.x, v.y, v.z + toCenter.z).normalized * v.magnitude;

            c.gameObject.GetComponent<Rigidbody>().velocity = v;
            
        }

    }


    public void setPolarity (Polarity newPolarity) {
        p = newPolarity;
    }

}
