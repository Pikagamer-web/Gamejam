using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Menu : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    
    [SerializeField] Canvas Menucanvas, LogoAnimCanvas;
    int flag =0;
    // Start is called before the first frame update
    void Start()
    {
        Menucanvas.gameObject.SetActive(false);
        LogoAnimCanvas.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!videoPlayer.isPlaying && flag == 0)
        {
            LogoAnimCanvas.gameObject.SetActive(false);
            Menucanvas.gameObject.SetActive(true);
            ++flag;
        }
       
    }

    public void OnClickPlay()
    {
        SceneManager.LoadSceneAsync(1);
        flag = 0;
    }

    public void OnClickOptions()
    {

    }
}
