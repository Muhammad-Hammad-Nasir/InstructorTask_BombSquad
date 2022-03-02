using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    private PlayerController playerController;
    private GameObject exp;
    private Rigidbody bombRb;
    private Vector3 offset = new Vector3(0, 1, 0);
    private Vector3 currentPos;
    private float radius = 6;
    private float knockForce = 2500;
    private float throwForce = 7;
    private bool inAir;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        exp = GameObject.FindGameObjectWithTag("Explosion");
        bombRb = GetComponent<Rigidbody>();
        StartCoroutine(BombDelay());
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
        if (playerController.isBomb == true && playerController.isThrow == false)
        {
            transform.position = playerController.playerRb.transform.position + offset;
        }
        else if (playerController.isThrow == true)
        {
            if (inAir == false)
            {
                bombRb.AddForce((playerController.playerRb.transform.forward + offset) * throwForce, ForceMode.Impulse);
                inAir = true;
            }
        }
        else if (playerController.isBomb == false)
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mine"))
        {
            playerController.isBomb = false;
        }
    }

    IEnumerator BombDelay()
    {
        yield return new WaitForSeconds(4);
        playerController.isBomb = false;
        playerController.isThrow = false;
    }
}
