using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    float speed = 0.2f;
    public LineRenderer lr;
    dinoWeapon dinoWeaponScript;
    GameObject princesss;
    hero princessScript;
    healthBar healthScript;
    GameObject laserPoint;
    //float birthtime;
    bool enable = true;
    AudioSource pop;
    //Vector3 laserPointPosition;
    // Vector3 beginPos;
    void Start()
    {
        pop = GameObject.Find("pop").GetComponent<AudioSource>();
        dinoWeaponScript = GameObject.Find("dinoWeapon").GetComponent<dinoWeapon>();
        healthScript = GameObject.Find("HealthBar").GetComponent<healthBar>();
        healthScript.roaring();
        princesss = GameObject.Find("_princess");
        laserPoint = dinoWeaponScript.newPoint;
        princessScript = princesss.GetComponent<hero>();
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, transform.position);
        //birthtime = Time.time;
        Destroy(laserPoint, 4f);
        Destroy(gameObject, 4f);
        //beginPos = transform.position;
        //laserPointPosition = laserPoint.transform.position;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //StartCoroutine(oneSec());
        if (healthScript.roarr)
        {
            lr.startWidth = 1f;
            lr.endWidth = 3f;
            //Vector3 newBeginPos = transform.localToWorldMatrix * new Vector4(beginPos.x, beginPos.y, beginPos.z, 1);
            //lr.SetPosition(0, newBeginPos);
            if (dinoWeaponScript.newPoint.transform.position.x <= 50)
            {
                dinoWeaponScript.newPoint.transform.position += Vector3.left *speed* 8f;
            }
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.right, out hit))
            {
                if (hit.collider && hit.collider.tag == "princessShield")
                {
                    pop.Play();
                    lr.SetPosition(1, hit.point);
                    if (enable)
                    {
                        princessScript.shieldLevel--;
                        enable = false;
                        StartCoroutine(timer());
                    }
                    
                }
            }
            else
            {
                lr.SetPosition(1, laserPoint.transform.position);
            }
        }
        else
        {
            lr.startWidth = 0f;
            lr.endWidth = 0f;
        }
    }
    IEnumerator timer()
    {
        yield return new WaitForSeconds(0.5f);
        enable = true;
    }
    IEnumerator oneSec()
    {
        yield return new WaitForSeconds(1f);
    }
}

