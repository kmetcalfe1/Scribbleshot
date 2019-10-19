using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 7f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * 0.3f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "princessProjectile")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
