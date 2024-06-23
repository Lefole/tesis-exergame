
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LightGame : MonoBehaviour
{   
    //variables de tracking
    private float testReactionTime = 0;
    //Interactores
    public Light[] lights;
    public Dictionary<string,Light> lightsMap;
    public XRRayInteractor interactor;

    //Lista de niveles
    private List<List<Light>> levels;

    private List<Light> actualLevelLights;
    private Light hoveredLight;

    //Contador de nivel
    public int currentLevel = 0;
    public bool hasPassedLevel = false;

    private readonly float lightIntesityOnValue = 3;
    private readonly float lightIntesityOffValue = 0;
    //private Color defaultLightColor;

    void Start()
    {
        lights = GetComponentsInChildren<Light>();
        foreach (Light light in lights)
        {
            light.intensity = lightIntesityOffValue;
            string lightName = light.gameObject.GetComponentInParent<GameObject>().name;
            lightName = lightName.Substring(lightName.IndexOf('-') + 1);
            lightsMap.Add(lightName, light);
        }

        levels = new List<List<Light>> { 
            new() { lightsMap["top"], lightsMap["bottom"], lightsMap["center"] },
            new() { lightsMap["bottom"], lightsMap["top"], lightsMap["center"] },
            new() { lightsMap["left"], lightsMap["right"], lightsMap["center"] },
            new() { lightsMap["right"], lightsMap["left"], lightsMap["center"] },
            new() {
                lightsMap["center"],
                lightsMap["top"], lightsMap["top-left"],
                lightsMap["left"], lightsMap["bottom-left"],
                lightsMap["bottom"], lightsMap["bottom-right"],
                lightsMap["right"], lightsMap["top-right"],
                lightsMap["center"]
            },
        };

        levels.Add(new List<Light>(levels[^1]));
        levels[^1].Reverse();
     
        actualLevelLights = levels[0];
        interactor.hoverEntered.AddListener(OnHoverEntered);

        currentLevel = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (currentLevel < levels.Count)
        {
            
            if (hasPassedLevel)
            {
                actualLevelLights = levels[currentLevel];
                hasPassedLevel = false;
            }
            removeHoveredLight();
            updateIntensityOfLights();
            passLevel();
        }
    }

    private void removeHoveredLight() {
        if (hoveredLight == actualLevelLights[0])
        {
            actualLevelLights[0].intensity=lightIntesityOffValue;
            actualLevelLights.RemoveAt(0);
        }
    }

    private void passLevel()
    {
        if(actualLevelLights.Count == 0)
        {
            currentLevel++;
            hasPassedLevel=true;
        }
    }

    private void updateIntensityOfLights()
    {
        if (actualLevelLights.Count >= 1)
        {
            actualLevelLights[0].intensity = lightIntesityOnValue;
        }
    }

    public void OnHoverEntered(HoverEnterEventArgs args)
    {
        IXRHoverInteractable hoverTarget = args.interactableObject;
        GameObject gameObjectHover = hoverTarget.ConvertTo<GameObject>();
        Light light = gameObjectHover.GetComponentInChildren<Light>();
        hoveredLight = light;
        Debug.Log("Test  Time:"+testReactionTime);
        testReactionTime = 0;
    }
}
