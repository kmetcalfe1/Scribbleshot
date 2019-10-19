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

    public GameObject bar;
    public healthBar healthbar;

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        bar = GameObject.Find("HealthBar");
        healthbar = bar.GetComponent<healthBar>();
        healthbar.setSize(1f);


    }


    void OnCollisionEnter(Collision coll)
    {
        
        GameObject otherGO = coll.gameObject;
        switch (otherGO.tag)
        {

            case "princessProjectile":
                
                Projectile p = otherGO.GetComponent<Projectile>();
                


                ShowDamage();
                health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                healthbar.setSize(health * 0.1f);

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
        GameObject dinoo = GameObject.Find("dinoSprite"); 
        dinoo.GetComponent<Renderer>().material.color = Color.red;
        showingDamage = true;

        damageDoneTime = Time.time + showDamageDuration;
    }


}