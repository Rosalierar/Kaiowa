using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    //CLASS
    Animator animatorBoss;

    //GAME OBJECTS
    public GameObject HealthBoss;
    public Slider slider;

    //VIDA
    public int bossHealth;
    public int maxBossHealth = 100;

    //STUN
    private float dazedTime;
    public float startDazedTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        animatorBoss = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        animatorBoss.SetTrigger("isHit");
        bossHealth -= amount;
        slider.value = bossHealth;

        if (bossHealth <= 0)
        {
            //Anim de morte 
            Destroy(gameObject);
        }
    }
    public void BossTakeDamage(int amount)
    {
        animatorBoss.SetTrigger("isHit");

        dazedTime = startDazedTime;
        bossHealth -= amount;
        slider.value = bossHealth;

        if (bossHealth <= 0)
        {
            PlayerPrefs.SetInt("MonstrosDerrotados", PlayerPrefs.GetInt("MonstrosDerrotados", 0) + 1);

            Debug.Log("Prefebs: " + PlayerPrefs.GetInt("MonstrosDerrotados"));

            animatorBoss.SetTrigger("isDeath");
            StartCoroutine(DestroyEnemy());

        }

        Debug.Log("Take Damage:" + bossHealth + "/" + amount);
    }

    private IEnumerator DestroyEnemy()
    {
        HealthBoss.SetActive(false);

        // Aguarda a anima��o de ataque super terminar antes de desativar
        yield return new WaitForSeconds(1f); // Ajuste o tempo para o tempo da anima��o de ataque

        //Debug.Log("Monstros derrotados: " + controlScene.defeatedMonsters);
        Destroy(gameObject);
    }
}
