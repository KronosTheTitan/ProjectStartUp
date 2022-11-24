using System;
using System.Collections;
using System.Collections.Generic;
using HinputClasses;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private float speed = 1;
    [SerializeField] private float maxSpeed = 1;

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawn;

    public void Update()
    {
        Vector2 direction = _gamepad.leftStick.position;
        Vector2 rotation = _gamepad.rightStick.position;
        MovePlayer(direction.normalized, rotation.normalized);

        if (_gamepad.rightTrigger.justPressed || Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    public void MovePlayer(Vector2 direction, Vector2 rotation)
    {
        direction *= speed;
        rigidbody.velocity = Vector3.ClampMagnitude(new Vector3(direction.x, 0, direction.y), maxSpeed);
    }

    private float lastShot = -1;

    protected virtual void Shoot()
    {
        if( lastShot + 1 > Time.time) return;
        Bullet bullet = Instantiate(bulletPrefab,bulletSpawn.position,bulletSpawn.rotation).GetComponent(typeof(Bullet)) as Bullet;
    }

    public void SetGamepad(Gamepad gamepad)
    {
        _gamepad = gamepad;
    }

    private Gamepad _gamepad;
}
