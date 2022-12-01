using System;
using System.Collections;
using System.Collections.Generic;
using HinputClasses;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] public int health = 100;

    [Header("Score")]
    public int score;
    [SerializeField] private float lastScore;
    [SerializeField] private float scoreInterval;
    
    [Header("Movement")]
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private float speed = 5;
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float knockbackStrength = 1.5f;
    [SerializeField] private float knockbackDuration = .5f;

    private bool _inKnockback = false;
    
    [Header("Shooting")]
    [SerializeField] private float rateOfFire = .1f;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawn;
    
    private float _lastShot = -1;

    [Header("Gamepad")] 
    [SerializeField] private float vibrationStrength = 10f;
    [SerializeField] private float vibrationDuration = .5f;
    private Gamepad _gamepad;

    [Header("Events")]
    public UnityEvent onScoreIncrease;
    public UnityEvent onTakeDamage;
    public UnityEvent onDeath;
    
    public void Update()
    {
        if (!_inKnockback)
        {
            if(_gamepad == null) return;
            Vector2 direction = _gamepad.leftStick.position;
            Vector2 rotation = _gamepad.rightStick.position;
            MovePlayer(direction.normalized, rotation.normalized);
            
            if (_gamepad.rightTrigger.pressed || Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
    }

    public void ReceiveScore(int amount)
    {
        if(Time.time < lastScore + scoreInterval) return;
        lastScore = Time.time;
        score += amount;
        onScoreIncrease.Invoke();
    }

    /// <summary>
    /// This function is used to knock back the player.
    /// It is used as a coroutine so you it can lock
    /// the controls until the knockback effect is over.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private IEnumerator Knockback(Vector2 direction)
    {
        _inKnockback = true;
        rigidbody.AddForce(new Vector3(direction.x,0,direction.y) * (-1 * knockbackStrength), ForceMode.Impulse);
        yield return new WaitForSeconds(knockbackDuration);
        _inKnockback = false;
        print("Done!");
    }

    /// <summary>
    /// Use this function in events whenever you want the controller to vibrate.
    /// strength and duration variables can be found in the gamepad section.
    /// </summary>
    public void VibrateController()
    {
        _gamepad?.Vibrate(vibrationStrength, vibrationStrength, vibrationDuration);
    }

    /// <summary>
    /// MovePlayer is used to move the player and change their
    /// rotation.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="rotation"></param>
    private void MovePlayer(Vector2 direction, Vector2 rotation)
    {
        direction *= speed;
        rigidbody.velocity = Vector3.ClampMagnitude(new Vector3(direction.x, 0, direction.y), maxSpeed);
        if(rotation.magnitude <= 0.95f) return;
        transform.LookAt(transform.position + new Vector3(rotation.x,0,rotation.y));
    }

    /// <summary>
    /// This function shoots a projectile towards the direction the player is currently
    /// rotated.
    /// </summary>
    protected virtual void Shoot()
    {
        if( _lastShot + rateOfFire > Time.time) return;
        _lastShot = Time.time;
        Instantiate(bulletPrefab,bulletSpawn.position,bulletSpawn.rotation);
    }

    /// <summary>
    /// Assigns the gamepad from the player manager.
    /// </summary>
    /// <param name="gamepad"></param>
    public void SetGamepad(Gamepad gamepad)
    {
        _gamepad = gamepad;
    }

    public void TakeDamage(int damage , Vector2 direction)
    {
        health -= damage;
        onTakeDamage.Invoke();

        if (health <= 0)
        {
            Debug.Log(health);
            onDeath.Invoke();
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(Knockback(direction));
        }
    }
}
