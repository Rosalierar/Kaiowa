
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    ///Frases de Ressurgirmento
    
    [SerializeField] private TMP_Text textoRessurgimento;

    List<string> frasesDeRessurgir = new List<string>()
    {
        "N�o Desista!", "Esforce-se!", "Continue!", "N�o � o fim!", "Supere-se!", "Ultrapasse seus limites!",
        "A jornada continua!", "Seja Forte!", "Lute!", "Proteja!" };

    /// <summary>
    /// //
    /// </summary>
    PlayerLogic playerLogic;

    public float pushBackDistance;  // Dist�ncia que o jogador ser� empurrado para tr�s
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

    public bool invincible = false;

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
        Physics2D.IgnoreLayerCollision(8, 9, true);
        invincible = true;

        yield return new WaitForSeconds(1.5f);

        invincible = false;
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
    public void TakeDamage(int amount)
    {
        playerLogic.canKB = false;
        
        if (invincible) return;

        playerLogic.canKB = true;

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
        int indiceDaFrase = Random.Range(0, 10);
        textoRessurgimento.text = frasesDeRessurgir[indiceDaFrase];

        animator.SetTrigger("isDeath");
        controlScene = GameObject.FindGameObjectWithTag("SceneController").GetComponent<ControlScene>();
        controlScene.StopFollow();
        gameObject.transform.localScale = new Vector3(4, 4, 4);
        boxCollider.enabled = false;

        // Aguarda a anima��o de ataque super terminar antes de desativar
        yield return new WaitForSeconds(2f); // Ajuste o tempo para o tempo da anima��o de ataque

        textRespawnGameObj.SetActive(true);
        StartCoroutine(StartCoroutine());
    }

    public IEnumerator StartCoroutine()
    {
        yield return new WaitForSeconds(2f);

        controlScene.LoadScene();
    }

    /// <summary>
    /// FRASES PARA RESSURGIR
    /// </summary>
    public void FrasesDeMorte()
    {
        //Random para Tela de Ressurgimento
    }
}
