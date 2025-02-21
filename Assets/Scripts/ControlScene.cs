using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlScene : MonoBehaviour
{
    //public GameObject CDB;
    //
    public GameObject HealthPainel;

    //texto do score
    public GameObject scoreGameObject;
    public TMP_Text textScore;
    string scoretext;

    //�parecer uma mensagem de nao prosseguir
    public string[] textScene;
    public GameObject NaoProsseguirGameObj;
    [SerializeField] private TMP_Text dialogueText;

    //BoxCollider2D boxCollider;

    public int totalMonsters;
    public int defeatedMonsters;

    int phase = 0;
    public int[] score = new int[3];

    [SerializeField] private int totalCages;
    public int colletedCages;

    public string levelDoJogo;

    // Start is called before the first frame update
    void Start()
    {
        // Inscrevendo no evento para monitorar a derrota de monstros
        
        CollectCages[] cages = FindObjectsOfType<CollectCages>();
        foreach (CollectCages cage in cages)
        {
            cage.CollectedCage += OnCollectCage;
        }

        /*try
        {
            CDB = GameObject.Find("ChefeDeTrabalho").GetComponent<GameObject>();
        }
        catch {
            Debug.Log("CDB NAO TEM");
        }*/
    }

    public void LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelDoJogo);
        //SceneManager.LoadScene(levelDoJogo);
    }
    public void StopFollow()
    {
        CinemachineVirtualCamera vCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineVirtualCamera>();
        vCam.Follow = null;
    }
    private void OnCollectCage()
    {
        colletedCages++;
        Debug.Log("Jaulas abertas: " + colletedCages);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(("Player")) && (PlayerPrefs.GetInt("MonstrosDerrotados") >= totalMonsters))
        {
            StartCoroutine(TimeForScore());
        }

        else
        {
            StartCoroutine(TimeForMessage());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(("Player")) && (PlayerPrefs.GetInt("MonstrosDerrotados") >= totalMonsters))
        {
            StartCoroutine(TimeForScore());
        }
    }
    private IEnumerator TimeForMessage()
    {
        NaoProsseguirGameObj.SetActive(true);

        yield return new WaitForSeconds(3f);

        NaoProsseguirGameObj.SetActive(false);
    }
    private IEnumerator TimeForScore()
    {
        scoretext = ("" + colletedCages + "");
        textScore.text = ("Pontua��o: " + scoretext);
        HealthPainel.SetActive(false);
        scoreGameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        scoreGameObject.SetActive(false);

        LoadScene();
    }

}
