using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControlAttacks : MonoBehaviour
{
    //TIMER ATTACKS
    float timeChangeAttack = 30f;
    [SerializeField] float timeAttack;

    //CONTROLLER ATTACKS
    int whatAttack = 0;

    //ATTACKS SCRIPTS
    BossAttackBasic attackBasic;
    BossAttackBullet attackBullet;
    BossAttackTrails attackTrails;


    // Start is called before the first frame update
    void Start()
    {
        attackBasic = GetComponent<BossAttackBasic>();
        attackBullet = GetComponent<BossAttackBullet>();
        attackTrails = GetComponent<BossAttackTrails>();
    }

    // Update is called once per frame
    void Update()
    {
        ControlTimeAttack();
    }

    public void DisableScript(int changeAttack)
    {
        switch (changeAttack)
        {
            case 0:
                attackBullet.enabled = false;
                attackTrails.enabled = false;
                attackBasic.enabled = true;
                break;
            case 1: 
                attackBasic.enabled = false;
                attackTrails.enabled = false;
                attackBullet.enabled = true;
                break;
            case 2:
                attackBasic.enabled = false;
                attackBullet.enabled = false;
                attackTrails.enabled = true;
                break;
        }
    }

    void ControlTimeAttack()
    {
        timeAttack += Time.deltaTime;

        if (timeAttack >= timeChangeAttack) 
        {
            if (whatAttack + 1 < 3)
                whatAttack = whatAttack + 1;
            else
                whatAttack = 0;

            DisableScript(whatAttack);
            timeAttack = 0;
        }
    }

}
