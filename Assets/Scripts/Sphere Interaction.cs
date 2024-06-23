using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SphereInteraction : MonoBehaviour
{
    public GameObject ParticleSystem;
    public InputActionReference handGrabInput;

    private ObjectSpawner spawner;
    private ScoreScript score;
    private Animator jammo_animator;


    private float timeSinceObjectGrabbed;

    AudioManager audioManager;
    GameController gameController;
    PlayerControllerMovement playerControllerMovement;


    private void Awake()
    {
        timeSinceObjectGrabbed = 0;
        playerControllerMovement = GameObject.FindGameObjectWithTag("Player Movement Controller").GetComponent<PlayerControllerMovement>();
        gameController = GameObject.FindGameObjectWithTag("Game Controller").GetComponent<GameController>();
        score = FindObjectOfType<ScoreScript>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        jammo_animator = GameObject.FindGameObjectWithTag("AnimController").GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<ObjectSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController.hasPassedTime) { 
            timeSinceObjectGrabbed = 0;
            spawner.timeSinceSpawn = 0;
            playerControllerMovement.angleList.Clear();
        };
        float grapValue=handGrabInput.action.ReadValue<float>();
        
        if (grapValue >= 0.5)
        {
            //Debug.Log("Cueeeeeento");
            timeSinceObjectGrabbed += Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sphere spawner"))
        {
            return;
        }
        Destroy(this.gameObject);

        if (!gameController.hasPassedTime)
        {
            return;
        }

        float angle = playerControllerMovement.angleList.Average();
        float restTime = spawner.timeSinceSpawn - timeSinceObjectGrabbed;
        float testTime = timeSinceObjectGrabbed;
        float totalTime = spawner.timeSinceSpawn;
        bool successfull = false;

        

        playerControllerMovement.angleList.Clear();

        if (collision.gameObject.CompareTag("Box Base"))
        {
            successfull = true;
            GameObject particleSys = Instantiate(ParticleSystem, transform.position, Quaternion.identity);
        }

        if (successfull)
        {
            score.OnCorrectInteraction();
            audioManager.PlaySFX(audioManager.correctInteraction);
            jammo_animator.SetTrigger("isInteractionSuccessfull");
        }
        else
        {
            audioManager.PlaySFX(audioManager.incorrectInteraction);
        }

        gameController.AddRepetitionToGameData(angle, successfull, testTime, restTime, totalTime);
        timeSinceObjectGrabbed = 0;


    }
}
