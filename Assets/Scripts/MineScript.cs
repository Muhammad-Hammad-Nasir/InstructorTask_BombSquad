using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    private PlayerController playerController;
    private GameObject exp;
    private Rigidbody mineRb;
    private Vector3 offset = new Vector3(0, 1, 0);
    private Vector3 throwOffset = new Vector3(0, 1.5f, 0);
    private float radius = 3;
    private float knockForce = 1500;
    private float throwForce = 7;
    private bool inAir;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        mineRb = GetComponent<Rigidbody>();
        exp = GameObject.FindGameObjectWithTag("Explosion");
    }

    void Update()
    {
        if (playerController != null)
        {
            Movement();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Movement()
    {
        if (playerController.isMineBomb == true && playerController.isMineThrow == false && inAir == false)
        {
            transform.position = playerController.playerRb.transform.position + offset;
        }
        else if (playerController.isMineThrow == true)
        {
            if (inAir == false)
            {
                mineRb.AddForce((playerController.playerRb.transform.forward + throwOffset) * throwForce, ForceMode.Impulse);
                playerController.isMineThrow = false;
                inAir = true;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bomb"))
        {
            Vector3 currentPos = transform.position;
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
}
