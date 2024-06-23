using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartController : MonoBehaviour
{
    private float startTime;
    public bool hasPassedTime;
    private ScoreScript score;
    private bool changeLevel;
    // Start is called before the first frame update
    void Start()
    {
        score = FindObjectOfType<ScoreScript>();
        startTime = Time.time;
        hasPassedTime = false;
        changeLevel = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeLevelAfterDelay(int levelIndex)
    {
        StartCoroutine(ChangeLevelCoroutine(levelIndex));
    }

    private IEnumerator ChangeLevelCoroutine(int levelIndex)
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(levelIndex);
    }
}
