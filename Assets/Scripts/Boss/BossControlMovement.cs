using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControlMovement : MonoBehaviour
{
    //COTROLLER MOVEMENT
    [SerializeField] bool canWalk = true;

    //TIMER MOVEMENT
    float timeStopMovement = 20f;
    float timeCanMove = 5f;
    [SerializeField] float timeMovement;

    //SCRIPT MOVEMENT
    BossMovement bossMovement;
    Rigidbody2D rbBoss;


    // Start is called before the first frame update
    void Start()
    {
        bossMovement = GetComponent<BossMovement>();
        rbBoss = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        StopMove(canWalk);
    }

    void StopMove(bool whatDo)
    {
        timeMovement += Time.deltaTime;

        if (whatDo)
        {
            //nao vai poder se mexer assim que o tempo ultrapassar
            if (timeMovement >= timeStopMovement)
            {
                rbBoss.velocity = Vector2.zero;
                bossMovement.canMove = false;

                timeMovement = 0;
                canWalk = false;
            }
        }
        else
        {
            //vai voltar a se mexer
            if (timeMovement >= timeCanMove)
            {
                bossMovement.canMove = true;
                timeMovement = 0;
                canWalk = true;
            }
        }
    }

    void ControlTimeMovement()
    {

    }
}
