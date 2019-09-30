using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero : MonoBehaviour
{
    static public hero s;
    public float gameRestartDelay = 2f;
    [Header("Set in Inspector")]
    public float speed = 30f;
    public float rollMult = -45f;
    public float pitchMult = 30f;
    [Header("Set Dynamically")]
    //public float shieldLevel = 1;
    private float _shieldLevel = 1;

    //weapon fields
    public Weapon[] weapons;

    //declare a new delegate type WeaponFireDelegate
    public delegate void WeaponFireDelegate();
    //Create a WeaponFireDelegate field named fireDelegate
    public WeaponFireDelegate fireDelegate;


    public KeyCode jump;
    [Range(1,70)]
    public float jumpVelocity;

    public float fallMultiplier=2.5f;
    public float lowJumpMultiplier = 2f;
    Rigidbody rb;
    bool grounded = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (s == null)
        {
            s = this;
        }
        else
        {
            Debug.LogError("Hero.Awake()- Attempted to assign second hero.s");
        }
        //fireDelegate += TempFire;

    }
    void Start()
    {
        //reset the weapons to start _Hero with 1 blaster
        ClearWeapons();
        weapons[0].SetType(WeaponType.blaster);
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        //pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;
        //no rotation, but will need to add jump
        
        if (Input.GetKeyDown(jump) && grounded)
        {
            
            rb.velocity = Vector3.up * jumpVelocity*3;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y*20f * (fallMultiplier - 1) * Time.deltaTime;

        }
        else if(rb.velocity.y>0 && !Input.GetKey(jump))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * 20f * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && fireDelegate != null)
        {
            fireDelegate();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = false;
        }

    }
    //this variable holds a reference to the last triggereing GameObject
    private GameObject lastTriggerGo = null;

    void OnTriggerEnter(Collider other)
    {
        //find the tag of other.gameObject or its parent GameObjects
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        //if there is a parent with a tag
        if (go != null)
        {
            //make sure it's not the same triggering go as last time
            if (go == lastTriggerGo)
            {
                return;
            }
            lastTriggerGo = go;

            if (go.tag == "dino")
            {
                //if the shield was triggered by an enemy
                //decrease the level of the shield by 1
                shieldLevel--;
                //destroy the enemy
                //Destroy(go);
            }
            else if (go.tag == "powerUp")
            {
                //if the shield was triggered by a PowerUp
                AbsorbPowerUp(go);
            }
            else
            {
                print("Triggered: " + go.name); //announcing it
            }
        }
        else
        {
            //otherwise announce the original other.gameObject
            print("Triggered: " + other.gameObject.name);
        }
    }

    public void AbsorbPowerUp(GameObject go)
    {
        PowerUp pu = go.GetComponent<PowerUp>();
        switch (pu.type)
        {
            case WeaponType.shield: // if it's the shield
                shieldLevel++;
                break;
            default: //if it's any weapon powerup
                //check the current weapon type
                if (pu.type == weapons[0].type)
                {
                    //then increase the number of weapons of this type
                    ClearWeapons();
                    Weapon w = GetEmptyWeaponSlot(); // find an availbe weapon
                    if (w != null)
                    {
                        //set it to the pu.type
                        w.SetType(pu.type);
                    }
                }
                else
                {
                    //if this is a different weapon
                    ClearWeapons();
                    weapons[0].SetType(pu.type);
                }
                break;
        }
        pu.AbsorbedBy(this.gameObject);
    }

    Weapon GetEmptyWeaponSlot()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].type == WeaponType.none)
            {
                return (weapons[i]);
            }
        }
        return (null);
    }

    void ClearWeapons()
    {
        foreach (Weapon w in weapons)
        {
            w.SetType(WeaponType.none);
        }
    }

    public float shieldLevel
    {
        get
        {
            return (_shieldLevel);
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);
            //if the shield is going to be set to less than zero
            if (value < 0)
            {
                Destroy(this.gameObject);
                //Tell Main.S to restart the game after a delay
                Main.s.DelayedRestart(gameRestartDelay);
            }
        }
    }
}
