using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string levelDoJogo;
    [SerializeField] private string menu;

    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject painelCreditos;

    public void Jogar()
    {
        SceneManager.LoadScene(levelDoJogo);
    }
    public void AbrirOpcoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }
    public void FecharOpcoes()
    {
        painelMenuInicial.SetActive(true);
        painelOpcoes.SetActive(false);
    }
    public void AbrirCreditos()
    {
        painelMenuInicial.SetActive(false);
        painelCreditos.SetActive(true);
    }
    public void FecharCreditos()
    {
        painelMenuInicial.SetActive(true);
        painelCreditos.SetActive(false);
    }
    public void SairdoJogo()
    {
        Debug.Log("Saiu do jogo");
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene(menu);
    }
}
