using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Conversas : MonoBehaviour
{
    public GameObject Parte2;
    public GameObject Player;

    PlayerLogic playerLogic;

    // Start is called before the first frame update
    void Start()
    {
        playerLogic = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Parte2.activeSelf && playerLogic.enabled == true)
        {
            playerLogic.enabled = false;
        }
    }

}
