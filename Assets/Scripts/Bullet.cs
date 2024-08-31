using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 15f;
    private Rigidbody2D rb2d; 
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Translate(Vector2.up * forwardSpeed * Time.deltaTime);
        rb2d.angularVelocity = Random.Range(-100, 100);
    }

    public void Init()
    {
        Invoke("Lifetime", 3); 
    }

    private void Lifetime()
    {
        Destroy(gameObject);
    }
}
