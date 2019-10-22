using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    RectTransform playButt;
    AudioSource select;

    void Start()
    {
        select = GameObject.Find("select").GetComponent<AudioSource>();
        playButt = GetComponent<RectTransform>();
    }
    public void playButton()
    {
        select.Play();
        playButt.sizeDelta = new Vector2(227, 118);
        StartCoroutine(moreWaiting());
        
    }
    public void exitButton()
    {
        select.Play();
        playButt.sizeDelta = new Vector2(187, 78);
        StartCoroutine(moreWaitingTwo());
    }
    
    IEnumerator moreWaiting()
    {
        //Debug.Log("doing");
        yield return new WaitForSeconds(0.1f);
        playButt.sizeDelta = new Vector2(247, 138);
        SceneManager.LoadScene("_Scene_0");
    }
    IEnumerator moreWaitingTwo()
    {
        //Debug.Log("doing");
        yield return new WaitForSeconds(0.1f);
        playButt.sizeDelta = new Vector2(217, 108);
        Application.Quit();
    }
}
