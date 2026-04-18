using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float baseSpeed = 25.0f;
    private float speed = 25.0f;

    public float speedIncreaseRate = 0.5f;

    private PlayerController playerControllerScript;
    private float leftBound = -15.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        speed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {   
        if(playerControllerScript.gameOver == false)
        {
            // Increase Speed over time
            speed += Time.deltaTime * speedIncreaseRate;

            // Move Object
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        
        // Destroy Objects when off screen
        if(transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
