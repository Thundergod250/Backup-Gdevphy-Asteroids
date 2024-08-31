using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Asteroid : MonoBehaviour
{ 
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    private float moveSpeed; 
    private bool active;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private bool ifBig
    {
        get { return transform.localScale.x == 3f; }
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveSpeed = ifBig ? 4f : 8f;
        transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));

        if (ifBig)
        {
            active = false;
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.2f);
            Invoke("SetAlive", 1f);
        }
        else
            active = true; 
    }

    private void Update()
    {
        if (!active)
            return; 

        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        rb2d.angularVelocity = 10; 
        Gameplay.instance.ScreenWrapping(transform); 
    }

    private void SetAlive()
    {
        spriteRenderer.color = Color.white;
        active = true; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!active)
            return; 

        if (collision.GetComponent<Bullet>() != null)
        {
            Destroy(collision.gameObject); 

            if (ifBig)
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject _mini = Instantiate(gameObject, transform.position, Quaternion.identity);
                    _mini.transform.localScale = new Vector2(1.25f, 1.25f);
                }
                Gameplay.score -= 1;
            }
            Destroy(gameObject);
            Gameplay.score += 2; 
        }
    }


}
