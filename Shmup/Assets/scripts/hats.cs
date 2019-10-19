using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hats : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 7f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= Vector3.up * 0.5f;
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
