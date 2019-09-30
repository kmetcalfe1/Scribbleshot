using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float rotationsPerSecond = 0.1f;
    [Header("Set Dynamically")]
    public int levelShown = 0;
    Material mat;
    // Use this for initialization
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //read the current shield level from the hero singleton
        int currLevel = Mathf.FloorToInt(hero.s.shieldLevel);
        //if this is different from levelShown
        if (levelShown != currLevel)
        {
            levelShown = currLevel;
            //adjust the texture offset to show different shield level
            mat.mainTextureOffset = new Vector2(0.2f * levelShown, 0);
        }
        //rotate the shield a bit every second
        float rZ = -(rotationsPerSecond * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }
}