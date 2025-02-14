using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    //CLASS
    SpawnBoss spawnBoss;
    Health pLayerHealth;
    EnabledMovePlayer enabledMovePlayer;
    PlayerLogic playerLogic;

    //GAMEOBJECTS 
    [SerializeField] private GameObject cam1;
    [SerializeField] private GameObject cam2;
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
        spawnBoss = GetComponent<SpawnBoss>();
        enabledMovePlayer = GameObject.Find("ParedeChefao").GetComponent<EnabledMovePlayer>();
        pLayerHealth = GameObject.Find("Player").GetComponent<Health>();
        playerLogic = GameObject.Find("Player").GetComponent<PlayerLogic>();
    }

    // Update is called once per frame
    void Update()
    {
       if (enabledMovePlayer.canFight && !isInstantiate)
       {
            GameObject boss = Instantiate(bossPrefab, bossPos.position, transform.rotation);
            BossMovement bossMovement = boss.GetComponent<BossMovement>();
            boss.transform.SetParent(bossPos);
            bossMovement.ArmazenarPlayerPosition(playerTransform);
            isInstantiate = true;

            spawnBoss.enabled = false;

            StartCoroutine(ActiveLifeBoss());
       }
    }
    private IEnumerator ActiveLifeBoss()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);

        // Aguarda a animação de ataque super terminar antes de desativar
        yield return new WaitForSeconds(2f); // Ajuste o tempo para o tempo da animação de ataque

        bossLife.SetActive(true);

        yield return new WaitForSeconds(4f);
        cam2.SetActive(true);
        cam1.SetActive(false);
        playerLogic.podesemover = true;
    }
}
