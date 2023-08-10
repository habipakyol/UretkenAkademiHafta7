using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    public float jumpforce;
    public bool doubleJumpUsed = false;
    public float doubleJumpForce;
    public float gravityModifier;

    public bool isGrounded = true;
    public bool gameover;

    private Vector3 originalGravity;

    public Animator playerAnim;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public GameObject restartGames;

    GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalGravity = Physics.gravity;
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        gameManager = GameObject.FindObjectOfType<GameManager>();

    }
    void Update()
    {
        JumpControl();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameover = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            Destroy(collision.gameObject);
            restartGames.SetActive(true);
            ResetGravity();
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            gameManager.AddScore(2);
            Destroy(collision.gameObject);
        }
    }

    public void JumpControl()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !gameover)
        {
            rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            isGrounded = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            doubleJumpUsed = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && !doubleJumpUsed)
        {
            doubleJumpUsed = true;
            rb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
            playerAnim.Play("Running_Jump", 3, 0f);
        }
    }

    public void ResetGravity()
    {
        Physics.gravity = originalGravity;
    }

}
