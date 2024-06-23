using System.Collections;
using Newtonsoft.Json;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public Animator sceneAnimator;

    private float startTime;
    public bool hasPassedTime;


    private ScoreScript score;
    private bool changeLevel;

    public int repetitionIndex;
    public int serieIndex;

    public GameData gameData;



    void Start()
    {
        gameData= new GameData();
        serieIndex = 0;
        repetitionIndex = 0;

        score = FindObjectOfType<ScoreScript>();
        startTime = Time.time;
        hasPassedTime = false;
        changeLevel = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime >= 48) hasPassedTime = true;

        //Debug.Log(JsonConvert.SerializeObject(gameData.series));
        //if (gameData.series.Count>0) Debug.Log(gameData.series[serieIndex].serie.Count);
        if (serieIndex>=2 && changeLevel == false)
        {
            sceneAnimator.SetTrigger("GameEnded");
            string jsonGameData = JsonConvert.SerializeObject(gameData.series);

            Debug.Log(jsonGameData);
            File.WriteAllText(Application.dataPath + "/gameData.json",jsonGameData);
            ChangeLevelAfterDelay(0);
            changeLevel = true;
        }
    }

    public void AddRepetitionToGameData(float abduction_angle, bool succesfull, float test_time, float rest_time, float total_time)
    {
        Repetition newRepetition = new Repetition(abduction_angle, succesfull, test_time, rest_time, total_time);

        if (!gameData.series.ContainsKey(serieIndex))
        {
            gameData.series[serieIndex] = new Serie();
            Debug.Log("New serie created at: " + serieIndex);
        }
        
        gameData.series[serieIndex].serie[repetitionIndex] = newRepetition;
        IncrementRepetitionAndSerie();
        
    }
    
    void IncrementRepetitionAndSerie()
    {
        repetitionIndex++;
        if (repetitionIndex >= 10)
        {
            repetitionIndex = 0;
            serieIndex++;
            if(repetitionIndex<2) sceneAnimator.SetTrigger("SerieEnded");
        }
    }

    public void ChangeLevelAfterDelay(int levelIndex)
    {
        StartCoroutine(ChangeLevelCoroutine(levelIndex));
    }

    private IEnumerator ChangeLevelCoroutine(int levelIndex)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(levelIndex);
    }
}
