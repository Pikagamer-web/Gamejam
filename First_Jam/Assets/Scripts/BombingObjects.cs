using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombingObjects : MonoBehaviour
{
    CharacterControllerMine player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerMine>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.z > transform.position.z)
        {
            Invoke("DeactivateBomb", 3f);
        }
    }
    void DeactivateBomb()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        AudioManager.instance.PlayCollisionSound(transform.position);
    }
}
