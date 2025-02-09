using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchCam : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public CinemachineVirtualCamera cam3;

    Cronometro cronometro;


    // Start is called before the first frame update
    void Start()
    {
        cronometro = GameObject.FindGameObjectWithTag("Cronometro").GetComponent<Cronometro>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cronometro.progressao != 4)
        {
            if (cronometro.progressao == 0)
                CameraManager.SwitchCamera(cam1);
            if (cronometro.progressao == 1)
                CameraManager.SwitchCamera(cam2);
            if (cronometro.progressao == 2)
                CameraManager.SwitchCamera(cam3);
        }
        else if (cronometro.progressao == 4) 
        {
            cronometro.progressao = 5;
            CameraManager.SwitchCamera(cam1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        cronometro.progressao = 6;
        CameraManager.SwitchCamera(cam2);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        cronometro.progressao = 7;
        CameraManager.SwitchCamera(cam1);
    }
}
