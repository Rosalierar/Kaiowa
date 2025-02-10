using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cutscene : MonoBehaviour
{
    public GameObject ConfirmarPainel;
    public GameObject InicarCutscenePainel;


    public GameObject DialogoPainel;
    //public GameObject Dialogo3Painel;

    public GameObject Parte1;
    public GameObject Parte2;
    public GameObject Parte3;
    public GameObject Parte4;

    PlayerLogic playerLogic;
    GameObject player;

    Cronometro cronometro;
    DialogueLogic dialogueLogic;
    ContinueWithoutPainel continueWithoutPainel;
    public GameObject Npc;

    Part4 parte4;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");

        playerLogic = player.GetComponent<PlayerLogic>();

        //playerLogic = player.GetComponent<PlayerLogic>();
        cronometro = GameObject.FindGameObjectWithTag("Cronometro").GetComponent<Cronometro>();
        dialogueLogic = Npc.GetComponent<DialogueLogic>();
        continueWithoutPainel = GameObject.FindGameObjectWithTag("Player").GetComponent<ContinueWithoutPainel>();

        //parte4.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cronometro.contador >= cronometro.valorMaximo)
        {
            TerminouVideo();
        }
        if (Parte2.activeSelf)
        {
            if (dialogueLogic.finishVovo[1])
            {
                ConcluirPart3();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //part1
        if (other.CompareTag("Player") && gameObject.CompareTag("QuadIC"))
        {
            if (!continueWithoutPainel.sawAll)
            {
                playerLogic.rb.velocity = Vector2.zero;
                playerLogic.podesemover = false;
                playerLogic.SetPodeMover(false);
                ConfirmarPainel.SetActive(true);
            }
            else
            {
                InCutscene();
            }
        }
        //part2
        if (gameObject.CompareTag("Vovo") || (dialogueLogic.isVovo && cronometro.progressao == 3))
        {
            DialogoPainel.SetActive(true);
            dialogueLogic.AtualizarTexto();
        }
    }

    #region primeiraparte
    public void InCutscene()
    {
        ConfirmarPainel.SetActive(false);       
        InicarCutscenePainel.SetActive(true);   
        
        //iniciar um contador para fechar o video       
        cronometro.ContadorCutscene(true);
        Parte1.SetActive(false);
        
        cronometro.progressao = 3;
        //3
    }

    public void FecharPainel()
    {
        PlayerLogic playerLogic;
        GameObject player = GameObject.Find("Player");

        playerLogic = player.GetComponent<PlayerLogic>();

        ConfirmarPainel.SetActive(false);
        playerLogic.podesemover = true;

        playerLogic.SetPodeMover(true);
    }

    void TerminouVideo()
    {

        Parte2.SetActive(true);
        InicarCutscenePainel.SetActive(false);

        cronometro.ContadorCutscene(false);
        cronometro.contador = 0f;
        cronometro.valorMaximo = 10f;

        GameObject player = GameObject.Find("Player");
        playerLogic = player.GetComponent<PlayerLogic>();
        playerLogic.podesemover = false;
        //playerLogic.SetPodeMover(false);
    }
    #endregion primeiraparte

    #region segundaparte
    void ConcluirPart3()
    {
        Parte2.SetActive(false);
        Parte3.SetActive(true);
    }
    #endregion segundaparte

    void ConcluirAto1()
    {
        //parte 3
        DialogoPainel.SetActive(true);
    }
}
