using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCube : MonoBehaviour
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
        transform.Rotate(Vector3.one * Time.deltaTime * 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.CompareTag("Player"))
        {
            if(player.fieldStrength <= 100)
            {
                player.fieldStrength += 10f;
                gameObject.SetActive(false);
            }
            
        }
    }
}
