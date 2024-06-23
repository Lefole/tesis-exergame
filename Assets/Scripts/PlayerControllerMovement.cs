using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerMovement : MonoBehaviour
{
    public InputActionReference headPositionInput;
    public InputActionReference handPositionInput;
    public InputActionReference handGrabInput;

    public List<float> angleList;
    public float grapValue;

    public bool exerciseExecutionAngleCorrect;
    public bool exerciseExecutionMagnitudeCorrect;


    void Awake()
    {
        grapValue = 0;
        angleList = new List<float>();
        exerciseExecutionAngleCorrect = true;
        exerciseExecutionMagnitudeCorrect = true;
    }


    void Update()
    {
        float grapValue = handGrabInput.action.ReadValue<float>();
        //Debug.Log(grapValue);


        Vector3 handPosition = handPositionInput.action.ReadValue<Vector3>();
        Vector3 extendedHandPosition = new(handPosition.x, 0, handPosition.z);

        float angle = Vector3.Angle(handPosition, extendedHandPosition);
        angleList.Add(angle);

        float distance = Vector3.Magnitude(handPosition);

        verifyUserAngle(angle, handPosition.y);
        verifyUserHandMagnitude(distance, handPosition);

    }

    void verifyUserAngle(float angle,float handElevation)
    {
        Debug.Log(angle);
        exerciseExecutionAngleCorrect = (angle >= 25 && angle <= 45 && handElevation < 0);
    }

    void verifyUserHandMagnitude(float handMagnitude, Vector3 handPosition)
    {
        //Values based in my hand values
        double hand_distance_suposition = (1.5 + handPosition.x) / 2.7;
        exerciseExecutionMagnitudeCorrect = (handMagnitude>=hand_distance_suposition-0.05);
    }
}
