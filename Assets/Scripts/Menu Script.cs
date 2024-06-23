using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject gameSelection;

    [Header("Main Menu Buttons")]
    public Button startButton;
    public Button quitButton;

    [Header("Game Selection Button")]
    public Button movementExerciceButton;
    public Button codmanExerciceButton;
    public Button returnMenu;

    AudioManager audioManager;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    void Start()
    {
        EnableMainMenu();

        startButton.onClick.AddListener(EnableGameSelection);
        quitButton.onClick.AddListener(QuitGame);

        movementExerciceButton.onClick.AddListener(()=>NavigateToScene(1));
        codmanExerciceButton.onClick.AddListener(()=>NavigateToScene(2));

        returnMenu.onClick.AddListener(EnableMainMenu);
    }

    public void NavigateToScene(int sceneIndex)
    {
        audioManager.PlaySFX(audioManager.clickSound);
        SceneManager.LoadScene(sceneIndex);
    }
    public void QuitGame()
    {
        audioManager.PlaySFX(audioManager.clickSound);
        Application.Quit();
    }

    public void EnableMainMenu()
    {
        audioManager.PlaySFX(audioManager.clickSound);
        mainMenu.SetActive(true);
        gameSelection.SetActive(false);
    }

    public void EnableGameSelection()
    {
        audioManager.PlaySFX(audioManager.clickSound);
        mainMenu.SetActive(false);
        gameSelection.SetActive(true);
    }
}
