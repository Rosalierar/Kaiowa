using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    EnemyData enemyData1;
    EnemyData enemyData2;
    Health playerhealth;
    DialogueLogic2024 dialogueLogic;

    public GameObject Farid;
    public GameObject Ato1_5;
    public GameObject DialogoPainel;
    public GameObject SceneController;
    public GameObject parede;
    public GameObject cura;

    public GameObject[] textotutorial = new GameObject[6];
    public int progressoTutorial = 0;

    public BoxCollider2D boxCollider2D1;
    public BoxCollider2D boxCollider2D2;
    //Animator animatorr;

    bool ativou;
    bool tocou;

    // Start is called before the first frame update
    void Start()
    {
        playerhealth = GetComponent<Health>();  
        boxCollider2D1 = GameObject.Find("AlvoMaior").GetComponent<BoxCollider2D>();
        boxCollider2D2 = GameObject.Find("AlvoMenor").GetComponent<BoxCollider2D>();
        cura.SetActive(false);

        for (int i = 0; i < textotutorial.Length; i++)
        {
            Debug.Log("estou no for");
            if (i != 0)
                textotutorial[i].SetActive(false);
            else
                textotutorial[i].SetActive(true);

        }
        Debug.Log("terminei start");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Tutorial Progressao: " + progressoTutorial);
            PrimeiroTexto();
            SegundoTexto();
            TerceiroTexto();
            QuartoTexto();
            QuintoTexto();
            SextoTexto();

        if (progressoTutorial == 6)
        {
            Debug.Log("Entrei na ultima parte");
            SceneController.SetActive(true);
            parede.SetActive(false);
            dialogueLogic = Farid.GetComponent<DialogueLogic2024>();

            //dialogueLogic.StartCoroutine(dialogueLogic.NextScene());
        }
    }

    void PrimeiroTexto()
    {
       
        if (Input.GetKeyDown(KeyCode.J) && progressoTutorial == 0)
        {
            StartCoroutine(TempoParaAparecerTexto(0,1));
        }
    }
    void SegundoTexto()
    {
        if (Input.GetKeyDown(KeyCode.W) && progressoTutorial == 1)
        {
            StartCoroutine(TempoParaAparecerTexto(1,2));
        }
    }
    void TerceiroTexto()
    {
        if (progressoTutorial == 2 && !ativou)
        {
            cura.SetActive(true);
            ativou = true;
        }
        if (Input.GetKeyDown(KeyCode.J) && tocou){
            cura.SetActive(false);
            playerhealth.health += 20;
            playerhealth.slider.value = playerhealth.health;

            Debug.Log("Pegou cura");

            StartCoroutine(TempoParaAparecerTexto(2, 3));
        }
    }
    void QuartoTexto()
    {;
        if (Input.GetKeyDown(KeyCode.K) && progressoTutorial == 3)
        {
            StartCoroutine(TempoParaAparecerTexto(3,4));
        }
    }

    void QuintoTexto()
    {
        if (Input.GetKeyDown(KeyCode.L) && progressoTutorial == 4)
        {
            StartCoroutine(TempoParaAparecerTexto(4,5));

            boxCollider2D1.enabled = true;
            boxCollider2D2.enabled = true;
            enemyData1 = GameObject.Find("AlvoMaior").GetComponent<EnemyData>();
            enemyData2 = GameObject.Find("AlvoMenor").GetComponent<EnemyData>();
        }
    }
    void SextoTexto()
    {
        if (progressoTutorial == 5)
        {
            if (enemyData1.enemyData[0] <= 0 && enemyData2.enemyData[0] <= 0)
            {
                StartCoroutine(TempoParaAparecerTexto(5,6));

                boxCollider2D1.enabled = false;
                boxCollider2D2.enabled = false;
            }
        }
    }

    void AtivarProximaMensagem(int textoAnterior, int textoPosterior)
    {
        textotutorial[textoAnterior].SetActive(false);
        textotutorial[textoPosterior].SetActive(true);
    }

    //Esperar alguns segundos para o proximo texto
    private IEnumerator TempoParaAparecerTexto(int textoAnterior, int textoPosterior)
    {
        progressoTutorial++;

        // Aguarda a animação de ataque super terminar antes de desativar
        yield return new WaitForSeconds(0.5f); // Ajuste o tempo para o tempo da animação de ataque

        AtivarProximaMensagem(textoAnterior, textoPosterior);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cura") && progressoTutorial == 2)
        {
            tocou = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cura") && progressoTutorial == 2)
        {
            tocou = false;
        }
    }
}
