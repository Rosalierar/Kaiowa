using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAnim : MonoBehaviour
{
    public GameObject Farid;
    DialogueLogic2024 dialogueLogic;

    public GameObject cam1;
    public GameObject cam2;


    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        cam2.SetActive(true);

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Desativar();

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 0.1f)  // normalizedTime varia de 0 a 1
        {
            dialogueLogic = Farid.GetComponent<DialogueLogic2024>();
            dialogueLogic.StartCoroutine(dialogueLogic.NextScene());
        }

        if (stateInfo.normalizedTime >= 0.8f)  // normalizedTime varia de 0 a 1
        {
            Debug.Log("A animação terminou!");
            anim.SetTrigger("Idle");
            cam2.SetActive(false);
        }
    }

    void Desativar()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        

    }
}
