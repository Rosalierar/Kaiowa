using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    //CLASS
    EnabledMovePlayer enabledMovePlayer;

    //GAMEOBJECTS 
    public GameObject bossPrefab;
    public GameObject bossLife;
    
    //POSITIONS
    public Transform bossPos;
    public Transform playerTransform;

    //CONTROLLERS
    bool isInstantiate = false;
    
    // Start is called before the first frame update
    void Start()
    {
        enabledMovePlayer = GameObject.Find("ParedeChefao").GetComponent<EnabledMovePlayer>();

      
    }

    // Update is called once per frame
    void Update()
    {
       if (enabledMovePlayer.canFight)
       {
            GameObject boss = Instantiate(bossPrefab, bossPos.position, transform.rotation);
            BossMovement bossMovement = boss.GetComponent<BossMovement>();
            boss.transform.SetParent(bossPos);
            isInstantiate = true;
       }
    }
}
