using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string gameLevelName;
    [SerializeField] private GameObject initialMenuPanel;
    [SerializeField] private GameObject optionsPanel;
    public void Play()
    {
        SceneManager.LoadScene(gameLevelName);
    }

    public void OpenOptions()
    {
        initialMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        initialMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void Exit()
    {
        Debug.Log("Saiu do jogo");
        Application.Quit();
    }
}
