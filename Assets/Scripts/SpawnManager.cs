using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    private Vector3 spawnPos = new Vector3(30, 0, 0);
    private float startDelay = 0f;
    private float repeatRate = 2.0f;
    private PlayerController playerControllerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false)
        {
            int Index = Random.Range(0, obstaclePrefab.Length);
            Instantiate(obstaclePrefab[Index], spawnPos, obstaclePrefab[Index].transform.rotation);
        }  
    }
}
