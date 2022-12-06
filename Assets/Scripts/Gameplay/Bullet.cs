using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private int bounceCount = 1;
    [SerializeField] private float speed = 7.5f;

    private int bounces = 0;

    private void Start()
    {
        rigidbody.velocity = transform.forward * speed;
    }

    private void Update()
    {
        rigidbody.velocity = rigidbody.velocity.normalized * speed;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent(typeof(Player)) as Player;
        if (player != null)
        {
            player.TakeDamage(20, new Vector2(rigidbody.velocity.x,rigidbody.velocity.z));
            Destroy(gameObject);
            Debug.Log("Bullet hit player");
            return;
        }
        if (bounceCount <= bounces)
        {
            Destroy(gameObject);
            Debug.Log("Bullet bounced too much");
            return;
        }
        
        bounces++;
    }
}