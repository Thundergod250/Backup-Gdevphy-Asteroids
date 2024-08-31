using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float forwardForce = 10; 
    [SerializeField] private float angularForce = 100;
    [SerializeField] private GameObject gameOverText;
    private Rigidbody2D rb2d;
    private float timer;
    private bool isShooting; 

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bullet.SetActive(false); 
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.1f)
        {
            timer -= 0.1f;
            if (Input.GetKey(KeyCode.Space))
            {
                GameObject _b = Instantiate(bullet, transform.position, transform.rotation);
                _b.SetActive(true);
                _b.GetComponent<Bullet>().Init();
                isShooting = true;
            }
            else 
                isShooting = false;
        }
        Gameplay.instance.ScreenWrapping(transform);
    }
    private void FixedUpdate()
    {
        if (!isShooting)
            PlayerMovement(1);
        else
            PlayerMovement(3);
    }

    private void PlayerMovement(int speedModifier)
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            rb2d.angularVelocity = angularForce * speedModifier;
        else if (Input.GetKey(KeyCode.RightArrow))
            rb2d.angularVelocity = -angularForce * speedModifier;
        if (Input.GetKey(KeyCode.UpArrow))
            rb2d.AddForce(transform.up * forwardForce * speedModifier);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Asteroid>() != null)
        {
            Time.timeScale = 0;
            gameOverText.SetActive(true);
        }
    }
}
