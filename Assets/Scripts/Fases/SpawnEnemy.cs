using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    //game object utilizado para saber onde o player esta e enviar para o local/script que movimenta

    public Transform playerTransform;

    public Transform enemyPoints;
    public GameObject enemyPrefab;
    private bool instanciada = false;

    public int qualPatrolPont;

    public bool[] Phase;

    public Transform[] armazenarLocalizacaoDePatrolPointPrimeiroFase1;
    public Transform[] armazenarLocalizacaoDePatrolPointSegundoFase1;

    public Transform[] armazenarLocalizacaoDePatrolPointPrimeiroFase2;
    public Transform[] armazenarLocalizacaoDePatrolPointSegundoFase2;

    public Transform[] patrolPoints;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            if (!instanciada)
            {
                GameObject enemy = Instantiate(enemyPrefab, enemyPoints.position, transform.rotation);
                EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
                enemy.transform.SetParent(enemyPoints);
                instanciada = true;
                enemyMovement.qualPatrolPont = qualPatrolPont;
                //ArmazenarPatrolPoints(qualPatrolPont);

                // Passa os pontos de patrulha para o clone
                enemyMovement.ArmazenarPatrolPoints(qualPatrolPont, Phase, armazenarLocalizacaoDePatrolPointPrimeiroFase1,
                                                     armazenarLocalizacaoDePatrolPointSegundoFase1, armazenarLocalizacaoDePatrolPointPrimeiroFase2,
                                                     armazenarLocalizacaoDePatrolPointSegundoFase2);
                
                //enviar as informacoes de onde o player esta
                enemyMovement.ArmazenarLocalizacaoPlayer(playerTransform);

                EnemyDamage enemyDamage = enemy.GetComponent<EnemyDamage>();
                enemyDamage.ArmazenarLocalizacaoPlayer(playerTransform);

            }
        }
        catch
        {
            Debug.Log("Nao Tem Prefab");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ArmazenarPatrolPoints(int qualoPatrolPont)
    {
        if (Phase[0])
        {
            patrolPoints[0] = armazenarLocalizacaoDePatrolPointPrimeiroFase1[qualoPatrolPont];
            patrolPoints[1] = armazenarLocalizacaoDePatrolPointSegundoFase1[qualoPatrolPont];
        }
        else if (Phase[1])
        {
            patrolPoints[0] = armazenarLocalizacaoDePatrolPointPrimeiroFase2[qualoPatrolPont];
            patrolPoints[1] = armazenarLocalizacaoDePatrolPointSegundoFase2[qualoPatrolPont];
        }
    }
}
