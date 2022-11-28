using System;
using System.Collections;
using System.Collections.Generic;
using HinputClasses;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] public int health = 100;
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private float speed = 5;
    [SerializeField] private float maxSpeed = 5;

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawn;

    public void Update()
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

    public void MovePlayer(Vector2 direction, Vector2 rotation)
    {
        direction *= speed;
        rigidbody.velocity = Vector3.ClampMagnitude(new Vector3(direction.x, 0, direction.y), maxSpeed);
        if(rotation.magnitude <= 0.95f) return;
        transform.LookAt(transform.position + new Vector3(rotation.x,0,rotation.y));
    }

    private float lastShot = -1;
    [SerializeField] private float rateOfFire = .1f;

    protected virtual void Shoot()
    {
        if( lastShot + rateOfFire > Time.time) return;
        lastShot = Time.time;
        Bullet bullet = Instantiate(bulletPrefab,bulletSpawn.position,bulletSpawn.rotation).GetComponent(typeof(Bullet)) as Bullet;
    }

    public void SetGamepad(Gamepad gamepad)
    {
        _gamepad = gamepad;
    }

    public UnityEvent<int> onTakeDamage;
    public UnityEvent onDeath;

    public void TakeDamage(int damage)
    {
        health = health - damage;
        onTakeDamage.Invoke(health);

        if (_gamepad != null)
        {
            _gamepad.Vibrate(10,10,.75f);
        }
        
        if (health <= 0)
        {
            Debug.Log(health);
            onDeath.Invoke();
            gameObject.SetActive(false);
        }
    }

    private Gamepad _gamepad;
}
