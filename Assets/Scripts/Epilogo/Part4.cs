using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Part4 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    PlayerLogic playerLogic;

    [SerializeField] private string levelDoJogo;

    public GameObject parte4;
    public GameObject dialogoPainel;
    public GameObject blackPainel;
    public GameObject caminhao;

    Cronometro cronometro;
    Part4 part4;

    float contagemRegressiva;

    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private TMP_Text dialogueText;

    /*[SerializeField] private Image portImage;
    [SerializeField] private Sprite[] portrait;*/

    [SerializeField] private string[] speaker;

    [SerializeField][TextArea] private string[] dialoguewords;
    //onde esta a conversa

    [SerializeField] private int index = 0;

    DialogueLogic dialogueLogic;
    Cutscene cutscene;

    bool iniciar = true;

    // Start is called before the first frame update
    void Start()
    {
        cronometro = GameObject.FindGameObjectWithTag("Cronometro").GetComponent<Cronometro>();
        part4 = GameObject.FindGameObjectWithTag("Part4").GetComponent<Part4>();
    }

    // Update is called once per frame
    void Update()
    {
        if (iniciar)
        {
            caminhao.SetActive(true);
            Debug.Log("1");
            dialogueLogic = GameObject.FindGameObjectWithTag("Part4").GetComponent<DialogueLogic>();
            speakerText.text = speaker[index];
            dialogueText.text = dialoguewords[index];
            //portImage.sprite = portrait[index];
            dialogoPainel.SetActive(true);
            index++;
            iniciar = false;
            playerLogic = player.GetComponent<PlayerLogic>();
            playerLogic.podesemover = false;
        }
        else
        {
            Debug.Log("2");
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (index >= speaker.Length)
                {
                    StartCoroutine(Wait3Seconds());
                }
                else
                {
                    speakerText.text = speaker[index];
                    dialogueText.text = dialoguewords[index];
                    //portImage.sprite = portrait[index];
                    index++;
                }
            }
        }
    }

    IEnumerator Wait3Seconds()
    {
        caminhao.SetActive(false);
        blackPainel.SetActive(true);

        yield return new WaitForSeconds(3f);

        blackPainel.SetActive(false);

        cronometro.progressao = 4;
        //4

        dialogoPainel.SetActive(false);

        SceneManager.LoadScene(levelDoJogo);

        //StopCoroutine(Wait3Seconds());
    }

    public void Finalizar()
    {
        //mudar de cena
        playerLogic.podesemover = true;
        dialogoPainel.SetActive(false);
        SceneManager.LoadScene(levelDoJogo);
    }
}
