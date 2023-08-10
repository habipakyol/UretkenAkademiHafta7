using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{

    public float initialSpeed = 10f; 

    private GameManager gameManager;
    private PlayerController PlayerControllerScript;

    private float speed; 

    private float leftbound = -10;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        speed = initialSpeed;
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        
        if (PlayerControllerScript.gameover == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (gameManager.score >= 20)
        {
            speed = 15f;
        }
        else
        {
            speed = 10f;
        }

        if (transform.position.x < leftbound && CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }




}

