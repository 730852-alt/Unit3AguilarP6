using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRB.AddForce(Vector3.up * 10, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig"); 
            dirtParticle.Stop();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
        dirtParticle.Play();
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
        }
        
    }
}
