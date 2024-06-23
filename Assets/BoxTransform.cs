using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoxTransform : MonoBehaviour
{
    public Transform transform;
    private int multiplicator;

    public GameController gameController;
    private Dictionary<int, Serie> series;
    
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("Game Controller").GetComponent<GameController>();
        
        multiplicator = 0;
    }

    void Update()
    {
        series = gameController.gameData.series;
        multiplicator=(CountTotalSuccessfull())-CountValidErrors(CountTotalErrors());
        if (multiplicator <= 0) multiplicator = 0;
        transform.position = new Vector3(-(float)(0.5+0.1*multiplicator), transform.position.y, transform.position.z);
    }

    int CountTotalSuccessfull()
    {
        int count = 0;

        foreach (var serie in series.Values)
        {
            foreach (var repetition in serie.serie.Values)
            {
                if (repetition.succesfull)
                {
                    count++;
                }
            }
        }
        if (count > 5)
        {
            count = 5;
        }

        return count;
    }

    int CountTotalErrors()
    {
        int count = 0;

        foreach (var serie in series.Values)
        {
            foreach (var repetition in serie.serie.Values)
            {
                if (!repetition.succesfull)
                {
                    count++;
                }
            }
        }
        if (count > 5)
        {
            count = 5;
        }

        return count;
    }

    int CountValidErrors(int countErrors)
    {
        List<Repetition> repetitions = series.Values.SelectMany(serie => serie.serie.Values).ToList();
        repetitions.Reverse();


        int validErrors = countErrors;
        for (int i = 0; i < countErrors && i < repetitions.Count; i += 1)
        {
            if (repetitions[i].succesfull)
            {
                validErrors--;
            }
        }

        return validErrors;
    }
}
