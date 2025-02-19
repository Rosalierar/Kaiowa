using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSong : MonoBehaviour
{
    ControlSound controlSound;
    ControlScene scene;
    public GameObject Parede;
    public GameObject Parede2;

    public bool killBoss = false;

    // Start is called before the first frame update
    void Start()
    {
        controlSound = GameObject.FindWithTag("SoundController").GetComponent<ControlSound>();
        scene = GameObject.Find("ParedeNivel").GetComponent<ControlScene>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((PlayerPrefs.GetInt("MonstrosDerrotados") >= scene.totalMonsters) && killBoss)
        {
            Parede.SetActive(false);
            controlSound.SoundOnOff(0);
            
            Parede2.SetActive(false);
        }
    }
}
