using UnityEngine;
using System.Collections;

public class dinoProjectile : MonoBehaviour
{
    public float velocity = 20; //speed of projectile
    private BoundsCheck bndCheck;
    private Renderer rend;
    public Rigidbody rigid;
   
    void Awake()
    {
        //test to se whether this has passed off screen every 2 seconds
        bndCheck = GetComponent<BoundsCheck>();
        rend = GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        if (bndCheck.offUp || bndCheck.offRight || bndCheck.offLeft || bndCheck.offDown)
        {
            Destroy(gameObject);
        }
    }
}