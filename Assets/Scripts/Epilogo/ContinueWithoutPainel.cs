using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueWithoutPainel : DialogueLogic
{
    public int[] person = new int[3];
    public bool sawAll;

    public bool[] view = new bool [3];

    DialogueLogic dialogueLogic;
    ContinueWithoutPainel continueWithoutPainel;

    public GameObject Npc;

    // Start is called before the first frame update
    void Start()
    {
        dialogueLogic = Npc.GetComponent<DialogueLogic>();
        continueWithoutPainel = GetComponent<ContinueWithoutPainel>();
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.C))) {
            if (view[0])
            {
                person[0]++;
            }
            if (view[1])
            {
                person[1]++;
            }
            if (view[2])
            {
                person[2]++;
            }

            if (person[0] >= 1 && person[1] >= 1 && person[2] >= 1)
            {
                sawAll = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NpcMulher"))
        {
            view[0] = true;
        }
        if (other.CompareTag("NpcOutro"))
        {
            view[1] = true;
        }
        if (other.CompareTag("NpcHomem"))
        {
            view[2] = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NpcMulher"))
        {
            view[0] = false;
        }
        if (other.CompareTag("NpcOutro"))
        {
            view[1] = false;
        }
        if (other.CompareTag("NpcHomem"))
        {
            view[2] = false;
        }
    }
}
