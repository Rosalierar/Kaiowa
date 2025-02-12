using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueLogic : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    public PlayerLogic playerLogic;

    protected Cronometro cronometro;

    [SerializeField] public bool isVovo;

    public GameObject dialoguePainel;
    public GameObject btn;

    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private TMP_Text dialogueText;

    /*[SerializeField] private Image portImage;
    [SerializeField] private Sprite[] portrait;*/

    [SerializeField] protected string[] speaker;
    
    [SerializeField] [TextArea] private string[] dialoguewords;
    //onde esta a conversa
    [SerializeField] protected int index;

    public bool[] finishVovo = new bool[2];

    [SerializeField] private float wordSpeed;

    [SerializeField] public bool playerisClose;

    Animator animVovo;

    void Start()
    {
        cronometro = GameObject.FindGameObjectWithTag("Cronometro").GetComponent<Cronometro>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.J)) && playerisClose) {
            btn.SetActive(false);

            //saber se acabou as falas
            if (index >= speaker.Length && !isVovo)
            {
                WitchoutText();
            }
            else if(index >= speaker.Length && isVovo)
            {
                finishVovo[1] = true;
                cronometro.progressao = 2;
                //2

                WitchoutText();
            }
            //falará os dialogos
            else
            {
                dialoguePainel.SetActive(true);
                AtualizarTexto();
                playerLogic = player.GetComponent<PlayerLogic>();
                playerLogic.rb.velocity = Vector2.zero;
                playerLogic.podesemover = false;
            }
        }
    }

    //recomecar texto e fechar conversa
    public void WitchoutText()
    {
        index = 0;
        dialoguePainel.SetActive(false);
        playerLogic.podesemover = true;
        btn.SetActive(false);
    }
    public void AtualizarTexto()
    {
        speakerText.text = speaker[index];
        dialogueText.text = dialoguewords[index];
        //portImage.sprite = portrait[index];
        index++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            playerisClose = true;
            btn.SetActive(true);
        }
        if (other.CompareTag("Player") && isVovo)
        {
            animVovo = GetComponent<Animator>();
            animVovo.SetTrigger("Stopped");
            playerisClose = true;
            finishVovo[0] = true;
            cronometro.progressao++;
            //1
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            playerisClose = false;
            WitchoutText();
        }
    }
}
