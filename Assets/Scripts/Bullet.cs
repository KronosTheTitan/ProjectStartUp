using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;

    [SerializeField] private int bounceCount;

    [SerializeField] private float speed;

    private void Start()
    {
        rigidbody.velocity = transform.forward * speed;
    }

    private void Update()
    {
        rigidbody.velocity = rigidbody.velocity.normalized * speed;
    }

    private int bounces = 0;
    
    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent(typeof(Player)) as Player;
        if (player != null)
        {
            player.TakeDamage(20);
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
        
        //Vector2 velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.z);
        //Vector2 normal = new Vector2(collision.contacts[0].normal.x,collision.contacts[0].normal.y);
        //Debug.DrawLine(normal,normal*-1);

        //velocity = Vector2.Reflect(velocity, normal);
        //velocity.Normalize();

        

        //rigidbody.velocity = new Vector3(velocity.x, 0, velocity.y);
        //Debug.DrawRay(normal,normal*-1);
        //Debug.LogError("pause");
    }
}