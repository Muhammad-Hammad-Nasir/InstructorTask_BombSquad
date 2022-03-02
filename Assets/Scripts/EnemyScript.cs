using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Rigidbody enemyRb;

    private GameObject player;
    private GameObject enemyBomb;
    private Vector3 offset = new Vector3(0, 1, 0);
    private float yLimit = 5;
    private float speed = 6;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (player != null)
        {
            Movement();
        }

        if (transform.position.y < -yLimit)
        {
            Destroy(gameObject);
        }
    }

    void Movement()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 awayFromPlayer = (transform.position - player.transform.position);
            enemyRb.AddForce(awayFromPlayer * 5, ForceMode.Impulse);
        }
    }
}
