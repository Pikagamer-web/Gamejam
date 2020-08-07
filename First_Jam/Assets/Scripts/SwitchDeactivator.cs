using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchDeactivator : MonoBehaviour
{
    CharacterControllerMine player;
    public bool CanDeactiveSwitch = false;
    [SerializeField] Canvas GameOverCanvas;
    [SerializeField] GameObject gameOver, gameSuccess, credits;
    // Start is called before the first frame update
    void Start()
    {
        gameSuccess.SetActive(false);
        credits.SetActive(false);
        gameOver.SetActive(false);
        GameOverCanvas.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerMine>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.root.gameObject.CompareTag("Player") && Input.GetMouseButtonDown(0) && CanDeactiveSwitch )
        {
            OnGameComplete();
        }
    }

    private void OnGameComplete()
    {
        Debug.Log("YAHOOOOOO");
        GameOverCanvas.gameObject.SetActive(true);
        gameSuccess.SetActive(true);
        Invoke("ShowCredits", 1f);
        
    }
    void ShowCredits()
    {
        credits.SetActive(true);
        Invoke("LoadMenu", 5f);
    }
    void LoadMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
