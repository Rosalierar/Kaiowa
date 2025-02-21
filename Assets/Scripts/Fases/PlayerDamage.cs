using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{ 
    //gameobject do poder basico
    public GameObject poderbasico;

    //poder pesado
    public Transform shotPoint; 
    public GameObject poderPesadoPrefab;

    EnemyData enemyData;
    Cronometro cronometro;

    //animacoes
    Animator anim;
    int kPressed;
    int lPressed;
    int isHitHash;
    int isDeathHash;

    //informacoes de qual � o poder
    public bool[] poder = new bool[2];
    public bool[] atk = new bool[2];

    //valor da forca do ataque (a quantidade do dano)
    public int[] attack = new int[2];

    // Start is called before the first frame update
    void Start()
    {
        cronometro = GameObject.FindGameObjectWithTag("Cronometro").GetComponent<Cronometro>();

        anim = GetComponent<Animator>();
        isHitHash = Animator.StringToHash("isHit");
        isDeathHash = Animator.StringToHash("isDeath");
    }

    // Update is called once per frame
    void Update()
    {
        //Apertou o Attack Pesado  
        if (Input.GetKeyDown(KeyCode.L) && !cronometro.CountDown[1]) {
            atk[1] = true;
            atk[0] = false;
            AtkStrong();

            anim.SetTrigger("isAtk2");
            //anim atk2
            Debug.Log("atk2");
        }

        //Apertou o Attack Basico  
        if (Input.GetKeyDown(KeyCode.K) && !cronometro.CountDown[0]) {
            atk[0] = true;
            atk[1] = false;
            try
            {
                StartCoroutine(AtivarPoderBasico());
            }
            catch
            {
                Debug.Log("nAO POSSUO A pODER");
            }
            //anim atk1
            anim.SetTrigger("isAtk1");
            Debug.Log("atk1");
        }

        //Apertou o Ataque Super
    }
    void AtkStrong()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            try
            {
                Instantiate(poderPesadoPrefab, shotPoint.position, transform.rotation);
            }
            catch {
                Debug.Log("nAO POSSUO A PREFAB");
            }
        }
    }
    private IEnumerator AtivarPoderBasico()
    {
        // Ativa o GameObject antes do ataque basico
        poderbasico.SetActive(true);

        // Aguarda a anima��o de ataque super terminar antes de desativar
        yield return new WaitForSeconds(0.2f); // Ajuste o tempo para o tempo da anima��o de ataque

        // Desativa o GameObject ap�s o tempo da anima��o
        poderbasico.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyData enemyData = collision.gameObject.GetComponent<EnemyData>();

        if (enemyData != null)
        {
            string enemyName = enemyData.enemyName;

            Debug.Log("Atacando: " + enemyName);

            if (enemyName != "BossFire")
            {
                Debug.Log("Nao sou o chefe");
                if (poder[0])
                    enemyData.EnemyTakeDamage(attack[0]);
                if (poder[1])
                {
                    enemyData.EnemyTakeDamage(attack[1]);
                }
            }
            else
            {
                BossHealth booshealth = collision.gameObject.GetComponent<BossHealth>();
                if (poder[0])
                    booshealth.BossTakeDamage(attack[0]);
                if (poder[1])
                {
                    Debug.Log("Sou o chefe");
                    booshealth.BossTakeDamage(attack[1]);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyData enemyData = collision.gameObject.GetComponent<EnemyData>();

        if (enemyData != null)
        {
            if (poder[1])
                Destroy(gameObject);
        }
    }
}
