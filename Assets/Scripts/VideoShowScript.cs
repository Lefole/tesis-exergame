using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VideoShowScript : MonoBehaviour
{
    public GameObject dangerImage;
    public GameObject normalExecution;
    public TextMeshProUGUI textMeshProUGUI;

    private PlayerControllerMovement playerControllerMovement;
    private GameController gameController;


    void Awake()
    {
        playerControllerMovement = GameObject.
            FindGameObjectWithTag("Player Movement Controller").GetComponent<PlayerControllerMovement>();
        gameController=GameObject.
            FindGameObjectWithTag("Game Controller").GetComponent<GameController>();
    }

    void Update()
    {
        if (!gameController.hasPassedTime) return;
        bool isAngleError = !playerControllerMovement.exerciseExecutionAngleCorrect;
        bool isHandDistanceError = !playerControllerMovement.exerciseExecutionMagnitudeCorrect;
        if (isAngleError || isHandDistanceError)
        {
            dangerImage.SetActive(true);
            DefineErrorText(isAngleError,isHandDistanceError);

        }
        else
        {
            dangerImage.SetActive(false);
        }
       
    }
    void DefineErrorText(bool isAngleError,bool isHandDistanceError)
    {
        string text = "";
        if (isAngleError && isHandDistanceError)
        {
            text = "Estira tu brazo completamente\nMantén un ángulo de 45°";
        }
        else if (isHandDistanceError)
        {
            text = "Estira tu brazo completamente";
        }
        else if (isAngleError)
        {
            text = "Mantén un ángulo de 45°";
        }
        textMeshProUGUI.text = text;

    }




}
