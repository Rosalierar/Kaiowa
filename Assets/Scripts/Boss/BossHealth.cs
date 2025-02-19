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
    public int maxBossHealth = 1000;
    public bool killBoss = false;

    //STUN
    private float dazedTime;
    public float startDazedTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        animatorBoss = GetComponent<Animator>();
        bossHealth = maxBossHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BossTakeDamage(int amount)
    {
        animatorBoss.SetTrigger("isHurt");

        dazedTime = startDazedTime;
        bossHealth -= amount;
        slider.value = bossHealth;

        if (bossHealth <= 0)
        {
            PlayerPrefs.SetInt("MonstrosDerrotados", PlayerPrefs.GetInt("MonstrosDerrotados", 0) + 1);

            Debug.Log("Prefebs: " + PlayerPrefs.GetInt("MonstrosDerrotados"));

            animatorBoss.SetTrigger("isDeath");

            killBoss = true;
            ChangeSong changeSong = GameObject.FindWithTag("QuadIC").GetComponent<ChangeSong>();
            changeSong.killBoss = killBoss;

            StartCoroutine(DestroyEnemy());

        }

        Debug.Log("Take Damage:" + bossHealth + "/" + amount);
    }

    private IEnumerator DestroyEnemy()
    {
        HealthBoss.SetActive(false);

        // Aguarda a animação de ataque super terminar antes de desativard
        yield return new WaitForSeconds(1f); // Ajuste o tempo para o tempo da animação de ataque

        //Debug.Log("Monstros derrotados: " + controlScene.defeatedMonsters);
        Destroy(gameObject);
    }

    public void PegarSlider(GameObject painelBossHealth, Slider sliderBoss)
    {
        Debug.Log("tENHO AS vIDAS");
        HealthBoss = painelBossHealth;
        slider = sliderBoss;
    }
}
