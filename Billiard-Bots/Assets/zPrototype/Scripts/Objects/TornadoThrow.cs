using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoThrow : MonoBehaviour
{
    public float endHeight;

    public float radius;

    private TornadoPull pull;

    // Start is called before the first frame update
    void Start()
    {
        pull = transform.parent.GetComponent<TornadoPull>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //other.transform.SetParent(transform.parent);
            pull.pullOBj.Remove(other.gameObject);
            StartCoroutine(Spin(other.transform));
        }
    }

    IEnumerator Spin(Transform player)
    {
        for(float i = 0; i < endHeight; i += Time.deltaTime / 0.1f)
        {
            float x = Mathf.Cos(i) * radius;
            float z = Mathf.Sin(i) * radius;
            Vector3 pos = new Vector3(x, i, z) + transform.parent.transform.position;
            player.position = Vector3.Lerp(player.position, pos, i / endHeight);

            yield return null;
        }
        Vector3 velocity = player.GetComponent<Rigidbody>().velocity;
        Debug.Log(velocity);
        player.GetComponent<Rigidbody>().AddForce(new Vector3(velocity.x, 10f, velocity.z) * 5f, ForceMode.Impulse);
    }
}
