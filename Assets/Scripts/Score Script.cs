using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreTextMesh;
    public int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
    }

    private void Update()
    {
        ScoreTextMesh.text = Score.ToString();
    }

    public void OnCorrectInteraction()
    {
        Score += 100;
    }
}
