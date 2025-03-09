using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public Transform target;
    public float speed = 10f; 
    public float turnSpeed = 10f;
    private Rigidbody2D rb;
    public GameObject explode;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target= GameObject.Find("Player").transform;
    }

    void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float currentRotation = transform.eulerAngles.z;
        float desiredRotation = Mathf.LerpAngle(currentRotation, angle, turnSpeed * Time.fixedDeltaTime);
        float rot = desiredRotation - currentRotation;
        rb.AddTorque(rot * turnSpeed);
        Vector2 force = (direction * speed - rb.velocity);
        rb.AddForce(force);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        GameObject explosion = Instantiate(explode, transform.position, Quaternion.identity);
        GameObject scoreCountObject = GameObject.Find("Canvas");
        ScoreCount scoreCount = scoreCountObject.GetComponentInChildren<ScoreCount>();
        scoreCount.SpawnMissile(scoreCount.missilecount);
        PlayerPrefs.SetInt("Count", PlayerPrefs.GetInt("Count")+1);
        Destroy(gameObject);
    }
}