using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    //CLASS
    Animator animatorBoss;

    //GAME OBJECTS
    public Slider slider;

    //VIDA
    public int bossHealth;
    public int maxBossHealth = 2000;

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
}
