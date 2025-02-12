using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LastDialogueLogic : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    public PlayerLogic playerLogic;

    public GameObject dialoguePainel;

    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private TMP_Text dialogueText;

    /*[SerializeField] private Image portImage;
    [SerializeField] private Sprite[] portrait;*/

    [SerializeField] protected string[] speaker;

    [SerializeField][TextArea] private string[] dialoguewords;
    //onde esta a conversa
    [SerializeField] protected int index;

    [SerializeField] private float wordSpeed;

    [SerializeField] public bool playerisClose;

    public bool[] finishpart = new bool[2];

    public bool isPart6;

    [SerializeField] private string levelDoJogo;

    [SerializeField] private GameObject blackPainel;
    [SerializeField] private GameObject Ato5;
    [SerializeField] private GameObject Ato6;

    AsyncOperation loading;

    Animator animPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPart6)
        {
            if ((Input.GetKeyDown(KeyCode.J)) && playerisClose)
            {
                //saber se acabou as falas
                if (index >= speaker.Length)
                {
                    playerLogic.podesemover = true;
                    WitchoutText();
                    StartCoroutine(WaitForNextDialogue());
                }
                
                //falará os dialogos
                else if (playerisClose)
                {
                    dialoguePainel.SetActive(true);
                    AtualizarTexto();
                    playerLogic = player.GetComponent<PlayerLogic>();
                    playerLogic.podesemover = false;
                }
            }
        }
    }
    public void TextBeforeEp()
    {
        PlayerAnimAto5 dioalogueBeforeEp = GameObject.FindGameObjectWithTag("CDB").GetComponent<PlayerAnimAto5>();

        if (index == 0 && !finishpart[0] && !finishpart[1])
        {
            speakerText.text = speaker[index];
            dialogueText.text = dialoguewords[index];
            finishpart[0] = true;
            dialoguePainel.SetActive(true);
            playerLogic = player.GetComponent<PlayerLogic>();
            playerLogic.rb.velocity = Vector2.zero;
            playerLogic.podesemover = false;
        }

        else if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.O))
        {
            if (index >= speaker.Length && finishpart[0] && finishpart[1])
            {
                 Debug.Log("COMECEI");
                WitchoutText();
                StartCoroutine(BlackPainel());
                finishpart[1] = true;

                /*controlScene = GameObject.FindGameObjectWithTag("SceneController").GetComponent<ControlScene>();
                animCDB = CDB.GetComponent<Animator>();
                animCDB.SetTrigger("isWalk");*/
            }
            //if (index <= speaker.Length && finishpart[0])
            else
            {
                Debug.Log("Entrei na Funcao ");
                AtualizarTexto();
                dialoguePainel.SetActive(true);
                finishpart[1] = true;
            }
        }
    }
    public void AtualizarTexto()
    {
        speakerText.text = speaker[index];
        dialogueText.text = dialoguewords[index];
        //portImage.sprite = portrait[index];
        index++;
    }
    public void WitchoutText()
    {
        index = 0;
        dialoguePainel.SetActive(false);
        playerLogic.podesemover = true;
    }

    public IEnumerator BlackPainel()
    {
        blackPainel.SetActive(true);
        Ato6.SetActive(true);

        yield return new WaitForSeconds(2f);

        blackPainel.SetActive(false);
        Ato5.SetActive(false);
    }
    public IEnumerator SceneBeforeEp()
    {
        yield return new WaitForSeconds(0.8f);

        TextBeforeEp();
    }

    IEnumerator WaitForNextDialogue()
    {
        blackPainel.SetActive(true);
        loading = SceneManager.LoadSceneAsync(levelDoJogo);

        while (!loading.isDone)
        {
            yield return null;
        }

        blackPainel.SetActive(false);

        //StopAllCoroutines();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("entrei");
            playerisClose = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            index = 0;
            playerisClose = false;
            WitchoutText();
        }
    }
}
