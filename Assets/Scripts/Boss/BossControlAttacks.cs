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
                attackBasic.enabled = true;
                attackBullet.enabled = false;
                attackTrails.enabled = false;
                break;
            case 1: 
                attackBullet.enabled = true;
                attackBasic.enabled = false;
                attackTrails.enabled = false;
                break;
            case 2:
                attackTrails.enabled = true;
                attackBasic.enabled = false;
                attackBullet.enabled = false;
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
