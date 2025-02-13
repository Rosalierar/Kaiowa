using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimAto5 : MonoBehaviour
{
    public GameObject player;
    public GameObject cdb;
    LastDialogueLogic dialogueLogicLast;
    Animator animplayer;

    public bool apareceuDialogo = false ;

    // Start is called before the first frame update
    void Start()
    { 
        animplayer = player.GetComponent<Animator>();
        animplayer.SetTrigger("isWalking");
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animplayer.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 1f && !apareceuDialogo)  // normalizedTime varia de 0 a 1
        {
            dialogueLogicLast = cdb.GetComponent<LastDialogueLogic>();
            animplayer.SetTrigger("idle");
            dialogueLogicLast.StartCoroutine(dialogueLogicLast.SceneBeforeEp());

            apareceuDialogo = true;
        }
    }
}
