using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public GameObject HUDPanel;
    private Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private float score;
    public float scoreMultiplier = 10.0f;
    public float jumpForce = 10.0f;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -9.81f * gravityModifier, 0);
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        score = 0;
        scoreText.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
        if (!gameOver)
        {
            score += Time.deltaTime * scoreMultiplier;
            scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString("D5");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle") && !gameOver)
        {
            Debug.Log("Game Over");
            gameOver = true;

            // Animations, Sound and Particle
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);

            // UI Elements
            gameOverPanel.SetActive(true);
            HUDPanel.SetActive(false);
            finalScoreText.text = "Final Score: " + Mathf.FloorToInt(score);
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
