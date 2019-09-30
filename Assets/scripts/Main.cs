using UnityEngine;
using System.Collections;
using System.Collections.Generic; //required to use Lists or Dictionaries
using UnityEngine.SceneManagement;
public class Main : MonoBehaviour
{
    static public Main s; //singleton
    static public Dictionary<WeaponType, WeaponDefinition> W_DEFS;

    public WeaponDefinition[] weaponDefinitions;
    public GameObject prefabPowerUp;
    public WeaponType[] powerUpFrequency = new WeaponType[] {
        WeaponType.blaster, WeaponType.blaster, WeaponType.spread, WeaponType.shield
    };


    public WeaponType[] activeWeaponTypes;


    public void ShipDestroyed(Enemy e)
    {
        //potentially generate a PowerUp
        if (Random.value <= e.powerUpDropChance)
        {
            //random.value generates a value between 0 and 1 though never exactly 1
            //if the e.powerUpDropChance is 0.50f, a powerup will be generated 50% of the time. For testing it's 1f

            //choose shich powerup to pick
            //pick one from teh possibilites in powerUpFrequency
            int ndx = Random.Range(0, powerUpFrequency.Length);
            WeaponType puType = powerUpFrequency[ndx];

            //spawn a powerup
            GameObject go = Instantiate(prefabPowerUp) as GameObject;
            PowerUp pu = go.GetComponent<PowerUp>();
            //set it to the proper WeaponType
            pu.SetType(puType);

            //set it to the position of the destroyed ship
            pu.transform.position = e.transform.position;
        }
    }

    void Awake()
    {
        s = this;

        //a generic dictionary with WeaponType as the key
        W_DEFS = new Dictionary<WeaponType, WeaponDefinition>();
        foreach (WeaponDefinition def in weaponDefinitions)
        {
            W_DEFS[def.type] = def;
        }
    }

    static public WeaponDefinition GetWeaponDefinition(WeaponType wt)
    {
        //check to make sure that the key exists in the Dictionary
        //Attempting to retrieve a key that didn't exist, would throw an error
        //so the follow if statent is impotant
        if (W_DEFS.ContainsKey(wt))
        {
            return (W_DEFS[wt]);
        }
        //this will return a defintion for WeaponType.none,
        //which means it has failed to find the WeaponDefinition
        return (new WeaponDefinition());
    }

    void Start()
    {
        activeWeaponTypes = new WeaponType[weaponDefinitions.Length];
        for (int i = 0; i < weaponDefinitions.Length; i++)
        {
            activeWeaponTypes[i] = weaponDefinitions[i].type;
        }
    }


    public void DelayedRestart(float delay)
    {
        //invoke the Restart() method in delay seconds
        Invoke("Restart", delay);
    }

    public void Restart()
    {
        //reload _Scene_0 to restart the game
        SceneManager.LoadScene("_Scene_0");
    }

    // Update is called once per frame
    void Update()
    {

    }
}