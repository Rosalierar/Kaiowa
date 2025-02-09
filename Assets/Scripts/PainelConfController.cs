using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainelConfController : MonoBehaviour
{
    public GameObject player;
    PlayerLogic playerLogic;
    
    [SerializeField] GameObject Conf;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            playerLogic = player.GetComponent<PlayerLogic>();
            playerLogic.podesemover = false;
            Conf.SetActive(true);
        }
    }

    public void  CloseConf()
    {
        playerLogic = player.GetComponent<PlayerLogic>();
        playerLogic.podesemover = true;
        Conf.SetActive(false);
    }
}
