using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LastDialogueLogic : DialogueLogic
{

    [SerializeField] private string levelDoJogo;

    [SerializeField] private GameObject blackPainel;

    AsyncOperation loading;

    Animator Anim;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
            else
            {
                dialoguePainel.SetActive(true);
                AtualizarTexto();
                playerLogic = player.GetComponent<PlayerLogic>();
                playerLogic.podesemover = false;
            }
        }
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

            WitchoutText();
        }
    }
}
