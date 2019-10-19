using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
    Animator anim;
    [Header("Set in Inspector")]
    public float rotationsPerSecond = 0.1f;
    [Header("Set Dynamically")]
    public int levelShown = 0;
    Material mat;
    // Use this for initialization
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //read the current shield level from the hero singleton
        int currLevel = Mathf.FloorToInt(hero.s.shieldLevel);
        //if this is different from levelShown
        if (currLevel == 1)
        {
            anim.SetInteger("shieldHealth", 1);
        }
        else if (currLevel == 2)
        {
            anim.SetInteger("shieldHealth", 2);
        }
        else if (currLevel == 0)
        {
            anim.SetInteger("shieldHealth",0);
            
        }
        //rotate the shield a bit every second
        float rZ = -(rotationsPerSecond * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }
}