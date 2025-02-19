using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PainelConfController : MonoBehaviour
{
    bool isOpen = false;
    public string levelDeJogo;

    public GameObject player;
    PlayerLogic playerLogic;

    [SerializeField] GameObject Conf;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab) || Input.GetKeyUp(KeyCode.Escape))
        {
            if (!isOpen)
            {
                playerLogic = player.GetComponent<PlayerLogic>();
                playerLogic.rb.velocity = Vector2.zero;
                playerLogic.podesemover = false;
                Conf.SetActive(true);
                isOpen = true;
            }
            else if (isOpen)
            {
                CloseConf();
            }
        }
    }

    public void CloseConf()
    {
        playerLogic = player.GetComponent<PlayerLogic>();
        playerLogic.podesemover = true;
        Conf.SetActive(false);
        isOpen = false;
    }

    public void RecarregarCena()
    {
        SceneManager.LoadScene(levelDeJogo);
    }
}
