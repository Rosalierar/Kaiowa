using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
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

        yield return new WaitForSeconds(1f);

        invincible = false;
    }
    public void TakeDamage(int amount, Vector2 localDamage)
    {
        if (invincible) return;
        animator.SetTrigger("isHit");
        health -= amount;
        slider.value = health;

        // Calcular a direção do empurrao

        Vector2 pushDirection = (transform.position - (Vector3)localDamage).normalized;
        if (Mathf.Abs(transform.position.x - localDamage.x) == Mathf.Abs(transform.position.y - localDamage.y))
        {
            Debug.Log("LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL");
            // Empuxo horizontal
            pushDirection = new Vector2(pushDirection.x, 0).normalized;
        }/*
        else
        {
            Debug.Log("HHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
            // Empuxo vertical
            pushDirection = new Vector2(pushDirection.x, 0).normalized;
        }*/

        // Move o jogador para trás (deslocamento gradual)
        StartCoroutine(PushPlayerBack(pushDirection));

        StartCoroutine(TakeHit());

        Debug.Log(amount +"/" + health);

        if (health <= 0) { 
            StartCoroutine(TimeForRespawn());
            Debug.Log("Morri!");
        }
    }
    private IEnumerator PushPlayerBack(Vector2 direction)
    {
        float pushed = 0f;

        // Enquanto o jogador não tiver sido empurrado pela distância definida
        while (pushed < pushBackDistance)
        {
            // Desloca o jogador na direção oposta à colisão
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction * pushBackDistance, pushBackSpeed * Time.deltaTime);
            pushed += pushBackSpeed * Time.deltaTime;
            yield return null;
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
