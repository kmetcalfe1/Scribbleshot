using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dinoWeapon : MonoBehaviour
{
    float velocity = 50f;
    public GameObject projectilePrefab;
    public GameObject laserPrefab;
    public GameObject laserPointPrefab;
    GameObject princess;
    Vector3 lastPos;
    int random;
    healthBar phase;
    public GameObject laserPointPoint;

    public Transform[] spawnPoints;
    public GameObject[] rockets;
    int randomSpawnPoint, randomRocket;

    public Transform[] spawnPointsClouds;
    public GameObject[] clouds;
    int randomSpawnPointClouds, randomCloud;
    int prevCloud = -1;

    public Transform[] spawnPointsBalloons;
    public GameObject[] balloons;
    int randomBalloon;

    public Transform[] spawnPointsHats;
    public GameObject[] hats;
    int randomSpawnPointHats, randomHat;

    public GameObject newPoint;

    public List<int> phases = new List<int>();

    bool passed = false;
    //bool timeThrough = false;


    // Start is called before the first frame update
    void Start()
    {
        phases.Add(0);
        phases.Add(1);
        phases.Add(2);
        phases.Add(3);
        phase = GameObject.Find("HealthBar").GetComponent<healthBar>();
        princess = GameObject.Find("_princess");
        random = Random.Range(0, phases.Count);
        phases.RemoveAt(random);
        
        random = 3;
        if (random == 0f)
        {
            InvokeRepeating("Fire", 2.0f, 1.5f);
        }
        else if (random == 1)
        {
            InvokeRepeating("SpawnARocket", 2f, 2.5f);
        }
        else if (random == 2)
        {
            InvokeRepeating("CloudSpawning", 2f, 3f);

            InvokeRepeating("laser", 4f, 8f);
        }
        else if (random == 3)
        {
            InvokeRepeating("balloon", 2f, 2f);
            InvokeRepeating("hat", 2f, 1f);
        }



    }

    // Update is called once per frame
    void Update()
    {
        if (phase.waiting)
        {
            if (random == 0)
            {
                CancelInvoke("Fire");
            }
            if (random == 1)
            {
                CancelInvoke("SpawnARocket");
            }
            if (random == 2)
            {
                CancelInvoke("CloudSpawning");
                CancelInvoke("laser");
            }
            if (random == 3)
            {
                CancelInvoke("balloon");
                CancelInvoke("hat");

            }
            if ((phases.Count==3) || (phase.red && phases.Count==2))
            {
                random = phases[Random.Range(0, phases.Count)];
                if (phases.Contains(random) && phases.Count>=1)
                {
                    phases.Remove(random);
                }
            }
          
            if (random == 0)
            {
                InvokeRepeating("Fire", 1.0f, 1.5f);

            }
            else if (random == 1)
            {
                InvokeRepeating("SpawnARocket", 1f, 2.5f);

            }
            else if (random == 2)
            {
                InvokeRepeating("CloudSpawning", 2f, 3f);
                InvokeRepeating("laser", 4f, 8f);

            }
            else if (random == 3)
            {
                InvokeRepeating("balloon", 2f, 2f);
                InvokeRepeating("hat", 2f, 1f);

            }
        }
    }
    void balloon()
    {
        randomBalloon = Random.Range(0, balloons.Length);
        Instantiate(balloons[randomBalloon], spawnPointsBalloons[0].position,
        Quaternion.identity);

        randomBalloon = Random.Range(0, balloons.Length);
        Instantiate(balloons[randomBalloon], spawnPointsBalloons[1].position,
        Quaternion.identity);
    }
    void hat()
    {
        randomSpawnPointHats = Random.Range(0, spawnPointsHats.Length);
        randomHat = Random.Range(0, hats.Length);
        Instantiate(hats[randomHat], spawnPointsHats[randomSpawnPointHats].position,
        Quaternion.identity);
    }
    void laser()
    {
        //StartCoroutine(second());
        newPoint = (GameObject)Instantiate(original: laserPointPrefab, laserPointPoint.transform.position,
    Quaternion.identity);
        Instantiate(original: laserPrefab, new Vector3(laserPointPoint.transform.position.x, laserPointPoint.transform.position.y + 15, laserPointPoint.transform.position.z),
    Quaternion.identity);

    }
    void CloudSpawning()
    {
        randomSpawnPointClouds = Random.Range(0, spawnPointsClouds.Length);
        if (randomSpawnPointClouds == 0 && prevCloud == 0)
        {
            randomSpawnPointClouds = 1;
        }
        else if (randomSpawnPointClouds == 1 && prevCloud == 1)
        {
            randomSpawnPointClouds = 2;
        }
        else if (randomSpawnPointClouds == 2 && prevCloud == 2)
        {
            randomSpawnPointClouds = 0;
        }
        randomCloud = Random.Range(0, clouds.Length);
        Instantiate(clouds[randomCloud], spawnPointsClouds[randomSpawnPointClouds].position,
        Quaternion.identity);
        prevCloud = randomSpawnPointClouds;
    }
    void SpawnARocket()
    {
        randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        randomRocket = Random.Range(0, rockets.Length);
        Instantiate(rockets[randomRocket], spawnPoints[randomSpawnPoint].position,
        Quaternion.identity);

    }
    public void Fire()
    {
        dinoProjectile p;
        Vector3 vel = Vector3.left * velocity;


        if (transform.up.y < 0)
        {

            vel.y = -vel.y;

        }
        //lastPos = new Vector3(princess.transform.position.x, princess.transform.position.y + 1, princess.transform.position.z);
        p = MakeProjectile();
        //p.rigid.velocity = vel;
        p.rigid.velocity = (princess.transform.position - p.transform.position).normalized * 30;

    }

    public dinoProjectile MakeProjectile()
    {

        GameObject go = Instantiate<GameObject>(projectilePrefab);

        go.tag = "dinoProjectile";

        go.layer = LayerMask.NameToLayer("dinoProjectile");

        go.transform.position = transform.position;

        //go.transform.SetParent(PROJECTILE_ANCHOR, true);

        dinoProjectile p = go.GetComponent<dinoProjectile>();

        return (p);
    }

    IEnumerator second()
    {
        yield return new WaitForSeconds(5);
    }
}