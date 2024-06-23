
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject spawnObject;
    public float timeSinceSpawn=0;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceSpawn = 0;
        spawnObject.tag = "Sphere";
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawn += Time.deltaTime;

        var elements = GameObject.FindGameObjectsWithTag("Sphere");

        if(elements.Length <= 0)
        {
            Instantiate(spawnObject, transform.position, Quaternion.identity);
            timeSinceSpawn = 0;
        }
        else
        {
            for(int i=1; i<elements.Length; i++)
            {
                Destroy(elements[i]);
            }
        }
        
    }
}
