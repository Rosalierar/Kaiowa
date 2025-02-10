using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueLogic2024 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    PlayerLogic playerLogic;

    ControlScene controlScene;
    [SerializeField] private GameObject blackPainel;
    [SerializeField] private GameObject Part1;
    [SerializeField] private GameObject PaineldeVida;

    [SerializeField] private GameObject CDB;

    Animator Anim;
    Animator animCDB;
    public Cronometro cronometro;

    DialogueLogic2024 dialogueLogic2024;
    DialogueLogic2024 dialogue2Logic2024;

    [SerializeField] private GameObject dialoguePainel;
    [SerializeField] private GameObject dialoguePainelTuto;

    public GameObject[] Partes;
    //public GameObject btn;

    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private TMP_Text dialogueText;

    /*[SerializeField] private Image portImage;
    [SerializeField] private Sprite[] portrait;*/

    [SerializeField] protected string[] speaker;

    [SerializeField][TextArea] private string[] dialoguewords;
    //onde esta a conversa
    [SerializeField] protected int index;

    public bool[] finishpart1 = new bool[2];
    public bool[] finishpart2 = new bool[2];

    //bool finishpart0_5 = false;
    [SerializeField] private float wordSpeed;
    [SerializeField] private bool encostou = false; 
    //[SerializeField] private bool playerisClose;

    // Start is called before the first frame update
    void Start()
    {
        cronometro = GameObject.FindGameObjectWithTag("Cronometro").GetComponent<Cronometro>();
        dialogueLogic2024 = GetComponent<DialogueLogic2024>();
        dialogue2Logic2024 = GameObject.FindGameObjectWithTag("NpcHomem").GetComponent<DialogueLogic2024>();
        dialogue2Logic2024.enabled = false;

        finishpart2[0] = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.J)))
        {
            //saber se acabou as falas
            if (index >= speaker.Length && !finishpart1[0])
            {
                finishpart1[0] = true;
                WitchoutText();

                Anim = GameObject.FindGameObjectWithTag("NpcHomem").GetComponent<Animator>();

                Anim.SetTrigger("Walk");
            }
            //falará os dialogos
            else if (index <= speaker.Length && !finishpart1[0])
            {
                playerLogic = player.GetComponent<PlayerLogic>();
                playerLogic.rb.velocity = Vector2.zero;
                playerLogic.podesemover = false;
                dialoguePainel.SetActive(true);
                AtualizarTexto();
            }
            if (index >= speaker.Length && encostou && !finishpart1[1])
            {
                finishpart1[1] = true;
                dialogue2Logic2024.enabled = false;
                WitchoutText();
                StartCoroutine(WaitForNextDialogue());
                Partes[1].SetActive(true);
            }
            
        }
    }

    public void TextTutorial()
    {
        Debug.Log("Entrei na Funcao ");
        if (index == 0 && !finishpart2[0]&& !finishpart2[1])
        {
            finishpart2[0] = true;
            dialoguePainel.SetActive(true);
            AtualizarTexto();
            playerLogic = player.GetComponent<PlayerLogic>();
            playerLogic.rb.velocity = Vector2.zero;
            playerLogic.podesemover = false;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (index <= speaker.Length && finishpart2[0])
            {
                finishpart2[0] = false;
                dialoguePainel.SetActive(true);
                AtualizarTexto();
            }
            else if (index >= speaker.Length && !finishpart2[0] && !finishpart2[1])
            {
                WitchoutText();
                controlScene = GameObject.FindGameObjectWithTag("SceneController").GetComponent<ControlScene>();
                animCDB = CDB.GetComponent<Animator>();
                animCDB.SetTrigger("isWalk");
                finishpart2[1] = true;
            }
        }
    } 
        
    public void WitchoutText()
    {
        index = 0;
        dialoguePainel.SetActive(false);
        playerLogic.podesemover = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("NpcHomem") && other.CompareTag("QuadIC")) {
            Debug.Log("entrei");
            encostou = true;
            dialogue2Logic2024.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            WitchoutText();
        }
    }

    public void AtualizarTexto()
    {
        speakerText.text = speaker[index];
        dialogueText.text = dialoguewords[index];
        //portImage.sprite = portrait[index];
        index++;
    }
    IEnumerator WaitForNextDialogue()
    {
        blackPainel.SetActive(true);

        yield return new WaitForSeconds(3f);

        blackPainel.SetActive(false);
        PaineldeVida.SetActive(true);
        Part1.SetActive(false);
        //StopAllCoroutines();
    }

    public IEnumerator NextScene()
    {
        yield return new WaitForSeconds(2.3f);

        TextTutorial();
    }
}
