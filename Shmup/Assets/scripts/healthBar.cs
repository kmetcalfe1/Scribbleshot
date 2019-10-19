using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class healthBar : MonoBehaviour
{
    private Transform bar;
    GameObject barColor;
    Renderer rend;
    public bool waiting = false;
    Animator anim;
    bool passed1 = false;
    bool passed2 = false;
    public GameObject powerPrefab;
    public GameObject winn;
    public bool roarr = false;
    hero princessScript;
    public GameObject gun;
    dinoWeapon dinoWeaponScript;
    public bool red = false;
    int randomSpawnPointP;
    public Transform[] spawnPointsP;
    AudioSource roarSound;
    AudioSource laserSound;
    AudioSource winSound;

    // Start is called before the first frame update
    void Start()
    {
        dinoWeaponScript = GameObject.Find("dinoWeapon").GetComponent<dinoWeapon>();
        princessScript =GameObject.Find("_princess").GetComponent<hero>();
        anim = GameObject.Find("dinoSprite").GetComponent<Animator>();
        bar = transform.Find("bar");
        barColor = GameObject.Find("health");
        rend = barColor.GetComponent<Renderer>();
        roarSound = GameObject.Find("roar").GetComponent<AudioSource>();
        laserSound = GameObject.Find("laser").GetComponent<AudioSource>();
        winSound = GameObject.Find("winSound").GetComponent<AudioSource>();
        rend.material.color = Color.green;

    }
    public void setSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
        if(sizeNormalized<= 0.66f && !passed1)
        {
            passed1 = true;
            rend.material.color = Color.yellow;
            if (princessScript.shieldLevel < 2)
            {
                princessScript.shieldLevel = princessScript.shieldLevel + 1;
            }
            
            waiting = true;
                anim.SetBool("phaseEnd", true);
                StartCoroutine(wait());
        }

        if (sizeNormalized <= 0.33f && !passed2)
            {
            if (princessScript.shieldLevel < 2)
            {
                princessScript.shieldLevel = princessScript.shieldLevel + 1;
            }
            passed2 = true;
                rend.material.color = Color.red;
                red = true;
                waiting = true;
                anim.SetBool("phaseEnd", true);
                StartCoroutine(wait());
            }
        if(sizeNormalized <= 0f)
        {
            StartCoroutine(winnn());
            
        }
        
    }
    public void roaring()
    {
        
        anim.SetBool("laser", true);
        StartCoroutine(roar());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator wait()
    {
        randomSpawnPointP = Random.Range(0, spawnPointsP.Length);
        yield return new WaitForSeconds(2);
        roarSound.Play();
        yield return new WaitForSeconds(3);
        anim.SetBool("phaseEnd", false);
        waiting = false;
        Instantiate(original: powerPrefab, spawnPointsP[randomSpawnPointP].position, rotation: Quaternion.identity);

    }
    IEnumerator roar()
    {
        laserSound.Play();
        yield return new WaitForSeconds(0.85f);
        gun.SetActive(true);
        roarr = true;
        yield return new WaitForSeconds(3.15f);
        anim.SetBool("laser", false);
        gun.SetActive(false);
        roarr = false;

    }
    IEnumerator winnn()
    {
        winn.SetActive(true);
        winSound.Play();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
