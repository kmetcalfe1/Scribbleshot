using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed = 10f; //the speed in m/s
    public float fireRate = 0.3f; //seconds/shot (Unused)
    public float health = 10;
    public int score = 100; //points earned for destroying this

    public float showDamageDuration = 0.1f;
    public float powerUpDropChance = 1f; //chance to drop a powerup

    protected BoundsCheck bndCheck;
    public Color[] originalColors;
    public Material[] materials; // all the materials of this & its children
    public bool showingDamage = false;

    public float damageDoneTime; // Time to stop showing damage

    public bool notifiedOfDestruction = false; // Will be used later

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        materials = Utils.GetAllMaterials(gameObject);
        originalColors = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
        if (bndCheck != null && bndCheck.offDown)
        {                      
            // We're off the bottom, so destroy this GameObject        
            Destroy(gameObject);                                        
        }
        if (showingDamage && Time.time > damageDoneTime)
        {      

            UnShowDamage();

        }
    }

    //public virtual void Move()
    //{
    //Vector3 tempPos = pos;
    //tempPos.y -= speed * Time.deltaTime;
    //pos = tempPos;
    //}

    //this is a Property: a method that acts like a field
    //public Vector3 pos
    //{
    //get
    //{
    //return (this.transform.position);
    //}
    //set
    //{
    //this.transform.position = value;
    //}
    //}


    void OnCollisionEnter(Collision coll)
    {                   

        GameObject otherGO = coll.gameObject;

        switch (otherGO.tag)
        {

            case "princessProjectile":       

                Projectile p = otherGO.GetComponent<Projectile>();
                // If this Enemy is off screen, don't damage it.
                Debug.Log("hit");
                if (!bndCheck.isOnScreen)
                {                       

                    Destroy(otherGO);

                    break;

                }
                ShowDamage();
                health -= Main.GetWeaponDefinition(p.type).damageOnHit;

                if (health <= 0)
                {        

                    // Destroy this Enemy

                    Destroy(this.gameObject);

                }

                Destroy(otherGO);    

                break;



            default:

                print("Enemy hit by non-ProjectileHero: " + otherGO.name); // f

                break;



        }

    }

    void ShowDamage()
    {
        foreach (Material m in materials)
        {
            m.color = Color.red;
        }
        showingDamage = true;

        damageDoneTime = Time.time + showDamageDuration;
    }

    void UnShowDamage()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
        showingDamage = false;
    }
}