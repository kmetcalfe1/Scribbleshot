using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    public float lifetime = 5f;
    Rigidbody rb;
    float moveSpeed;
    float birthtime;
    public Vector3[] points;
    BoundsCheck bndCheck;


    // Use this for initialization
    void Start()
    {
        bndCheck = GetComponent<BoundsCheck>();
        rb = GetComponent<Rigidbody>();
        moveSpeed = Random.Range(1f, 3f);
        this.gameObject.tag = "rocket";
        this.gameObject.layer = LayerMask.NameToLayer("rocket");
        
        points = new Vector3[3];
        points[0] = transform.position;
        float xMin = -bndCheck.camWidth + bndCheck.radius;
        float xMax = bndCheck.camWidth - bndCheck.radius;

        Vector3 v;
        //pick a random middle posiion in the bottom half of the screen
        v = Vector3.zero;
        v.x = Random.Range(xMin, xMax);
        v.y = -bndCheck.camHeight * Random.Range(2.75f, 2);
        points[1] = v;

        //pick a random final positon above the top of the screen
        v = Vector3.zero;
        v.y = transform.position.y;
        v.x = Random.Range(xMin, xMax);
        points[2] = v;
        birthtime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        MoveMonster();
    }

    void MoveMonster()
    {
        //Bezier curves work based on a u value between 0 & 1
        float u = (Time.time - birthtime) / lifetime;
        if (u > 1)
        {
            Destroy(this.gameObject);
            return;
        }
        //Interpolate the three Bezier curve points
        Vector3 p01, p12;
        u = u - 0.2f * Mathf.Sin(u * Mathf.PI * 2);
        p01 = (1 - u) * points[0] + u * points[1];
        p12 = (1 - u) * points[1] + u * points[2];
        transform.position = (1 - u) * p01 + u * p12;
        //base.Move();

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