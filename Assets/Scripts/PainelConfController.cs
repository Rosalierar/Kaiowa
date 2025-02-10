using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PainelConfController : MonoBehaviour
{
    public string levelDeJogo;

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

    public void CloseConf()
    {
        playerLogic = player.GetComponent<PlayerLogic>();
        playerLogic.podesemover = true;
        Conf.SetActive(false);
    }

    public void RecarregarCena()
    {
        SceneManager.LoadScene(levelDeJogo);
    }
}
