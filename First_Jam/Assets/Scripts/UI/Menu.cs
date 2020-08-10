using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Menu : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    
    [SerializeField] Canvas Menucanvas, LogoAnimCanvas, InstructionCanvas;
    int flag =0;
    CharacterControllerMine player;
    // Start is called before the first frame update

    [SerializeField]Joystick js;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerMine>();
        InstructionCanvas.gameObject.SetActive(false);
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

    public void OnClickInstructions()
    {
        InstructionCanvas.gameObject.SetActive(true);
    }

    public void OnXInstructions()
    {
        InstructionCanvas.gameObject.SetActive(false);
    }

    public void OnObsRewindDown()
    {
        player.LMBPressed = true;
    }
    public void OnFieldRewindDown()
    {
        player.RMBPressed = true;
    }
    public void OnObsRewindUp()
    {
        player.LMBPressed = false;
    }

    public void OnFieldRewindUP()
    {
        player.RMBPressed = false;
    }
}
