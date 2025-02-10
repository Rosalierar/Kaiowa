using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyData : MonoBehaviour
{
    ControlScene controlScene;

    Animator anim;
    //1 vida[0] e 2 dano[1] 
    public int[] enemyData = new int[2];

    //1 fogo arbusto //2 fovoador //3 fogohumanoide //4 fogochefe
    public bool[] character = new bool[4];

    float speed;
    private float dazedTime;
    public float startDazedTime;

    public string enemyName;


    // Definindo o evento de monstro derrotado
    public delegate void OnMonsterDefeated();
    public event OnMonsterDefeated MonsterDefeated;

    // Start is called before the first frame update
    void Start()
    {
        //variavel global para monstros derrotados
        PlayerPrefs.SetInt("MonstrosDerrotados", 0);

        TranformFalseCharacter();
        KnowCharacter();
        AssignAttributes();

        anim = GetComponent<Animator>();

        controlScene = GameObject.FindGameObjectWithTag("SceneController").GetComponent<ControlScene>();

    }

    // Update is called once per frame
    void Update()
    {
        if (dazedTime <= 0 && (character[0] || character[2])) { speed = 5; }
        else { 
            speed = 0; 
            dazedTime -= Time.deltaTime;
        }
    }
    protected void TranformFalseCharacter()
    {
        for (int i = 0; i < 4; i++)
        {
            character[i] = false;
        }
    }

    protected void KnowCharacter()
    {
        if (gameObject.CompareTag("FogoArbusto"))
        {
            character[0] = true; character[1] = false; character[2] = false; character[3] = false;
        }
        else if (gameObject.CompareTag("FogoVoador"))
        {
            character[1] = true; character[0] = false; character[2] = false; character[3] = false;
        }
        else if (gameObject.CompareTag("FogoHumanoide"))
        {
            character[2] = true; character[1] = false; character[0] = false; character[3] = false;
        }
        else if (gameObject.CompareTag("FogoChefe"))
        {
            character[3] = true; character[1] = false; character[2] = false; character[0] = false;
        }
    }
    protected void AssignAttributes()
    {
        //fogoARbusto
        if (character[0])
        {
            enemyName = "FogoArbusto";
            enemyData[0] = 30;
            enemyData[1] = 10;
        }
        //fagovoador
        if (character[1])
        {
            enemyName = "FogoVoador";
            enemyData[0] = 40;
            enemyData[1] = 5;
        }
        //vogo humanoide
        if (character[2])
        {
            enemyName = "FogoHumanoide";
            enemyData[0] = 100;
            enemyData[1] = 15;
        }
        //fogo chefe
        if (character[3])
        {
            enemyName = "FogoChefe";
            enemyData[0] = 200;
            enemyData[1] = 25;
        }
    }

    public void EnemyTakeDamage(int amount)
    {
        anim.SetTrigger("isHurt");

        dazedTime = startDazedTime;
        enemyData[0] -= amount;

        if (enemyData[0] <= 0)
        {
            PlayerPrefs.SetInt("MonstrosDerrotados", PlayerPrefs.GetInt("MonstrosDerrotados", 0) +1);
            
            Debug.Log("Prefebs: " + PlayerPrefs.GetInt("MonstrosDerrotados"));

            anim.SetTrigger("isDeath");
            StartCoroutine(DestroyEnemy());
        }

        Debug.Log("Take Damage:" + enemyData[0] +"/" + amount);
    }
    private IEnumerator DestroyEnemy()
    {
        // Aguarda a animação de ataque super terminar antes de desativar
        yield return new WaitForSeconds(1f); // Ajuste o tempo para o tempo da animação de ataque
        MonsterDefeated?.Invoke();
        //controlScene.defeatedMonsters++;
        Debug.Log("Monstros derrotados: " + controlScene.defeatedMonsters);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Poder"))
        {
            //collision.gameObject.SetActive(false);
        }
    }
}
