using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    PlayerLogic playerLogic;

    public float pushBackDistance;  // Distância que o jogador será empurrado para trás
    public float pushBackSpeed;
    public float Force = 5f;

    Animator animator;
    public GameObject textRespawnGameObj;
    public BoxCollider2D boxCollider;
    Rigidbody2D rb;

    public ControlScene controlScene;

    public int health;
    public int maxHealth = 100;

    public float impulseForce;
    public float invincibleTime = 1.5f;

    bool invincible = false;

    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerLogic = GetComponent<PlayerLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("Morreu");
        }
    }
    private IEnumerator TakeHit()
    {
        invincible = true;

        yield return new WaitForSeconds(1.5f);

        invincible = false;
    }
    public void TakeDamage(int amount)
    {
        if (invincible) return;
        animator.SetTrigger("isHit");
        health -= amount;
        slider.value = health;
        
        StartCoroutine(TakeHit());

        Debug.Log(amount +"/" + health);

        if (health <= 0) { 
            StartCoroutine(TimeForRespawn());
            Debug.Log("Morri!");
        }
    }

    public IEnumerator TimeForRespawn()
    {
        animator.SetTrigger("isDeath");
        controlScene = GameObject.FindGameObjectWithTag("SceneController").GetComponent<ControlScene>();
        controlScene.StopFollow();
        gameObject.transform.localScale = new Vector3(4, 4, 4);
        boxCollider.enabled = false;

        // Aguarda a animação de ataque super terminar antes de desativar
        yield return new WaitForSeconds(2f); // Ajuste o tempo para o tempo da animação de ataque

        textRespawnGameObj.SetActive(true);
        StartCoroutine(StartCoroutine());
    }

    public IEnumerator StartCoroutine()
    {
        yield return new WaitForSeconds(2f);

        controlScene.LoadScene();
    }
}
