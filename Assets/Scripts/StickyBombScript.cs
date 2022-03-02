using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBombScript : MonoBehaviour
{
    private PlayerController playerController;
    private GameObject player;
    private GameObject enemy;
    private GameObject exp;
    private Rigidbody stickyRb;
    private Vector3 offset = new Vector3(0, 1.2f, 0);
    private Vector3 currentPos;
    private float radius = 6;
    private float knockForce = 1500;
    private float throwForce = 7;
    private bool inAir;
    private bool playerAttach;
    private bool enemyAttach;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        player = GameObject.Find("Player");
        exp = GameObject.FindGameObjectWithTag("Explosion");
        stickyRb = GetComponent<Rigidbody>();
        StartCoroutine(BombDelay());
    }

    void Update()
    {
        if (playerController != null)
        {
            Movement();
            Attached();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Movement()
    {
        if (playerController.isStickyBomb == true && playerController.isStickyThrow == false)
        {
            transform.position = playerController.playerRb.transform.position + offset;
        }
        else if (playerController.isStickyThrow == true)
        {
            if (inAir == false)
            {
                stickyRb.AddForce((playerController.playerRb.transform.forward + offset) * throwForce, ForceMode.Impulse);
                inAir = true;
            }
        }
        else if (playerController.isStickyBomb == false)
        {
            inAir = false;

            currentPos = transform.position;
            exp.transform.position = currentPos;
            exp.GetComponent<ParticleSystem>().Play();

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider nearby in colliders)
            {
                Rigidbody enemyRb = nearby.GetComponent<Rigidbody>();
                if (enemyRb != null)
                {
                    enemyRb.AddExplosionForce(knockForce, transform.position, radius);
                }
            }
            Destroy(gameObject);
        }
    }

    void Attached()
    {
        if (playerAttach == true)
        {
            transform.position = player.transform.position + player.transform.forward;
        }
        else if (enemyAttach == true)
        {
            transform.position = enemy.transform.position + enemy.transform.forward;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mine"))
        {
            playerController.isStickyBomb = false;
        }
        else if (collision.gameObject.CompareTag("Player") && enemyAttach == false)
        {
            playerAttach = true;
        }
        else if (collision.gameObject.CompareTag("Enemy") && playerAttach == false)
        {
            enemy = collision.gameObject;
            enemyAttach = true;
        }
    }

    IEnumerator BombDelay()
    {
        yield return new WaitForSeconds(4);
        playerController.isStickyBomb = false;
        playerController.isStickyThrow = false;
    }
}
