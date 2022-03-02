using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBombScript : MonoBehaviour
{
    /*
    private EnemyScript enemyScript;
    private GameObject exp;
    private Rigidbody enemyBombRb;
    private Vector3 offset = new Vector3(0, 1, 0);
    private Vector3 currentPos;
    private float radius = 6;
    private float knockForce = 2500;
    private float throwForce = 7;
    private bool inAir;

    void Start()
    {
        enemyScript = GameObject.Find("Enemy").GetComponent<EnemyScript>();
        exp = GameObject.FindGameObjectWithTag("Explosion");
        enemyBombRb = GetComponent<Rigidbody>();
        StartCoroutine(BombDelay());
    }

    void Update()
    {
        if (enemyScript != null)
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
        if (enemyScript.isBomb == true && enemyScript.isThrow == false)
        {
            transform.position = enemyScript.enemyRb.transform.position + offset;
        }
        else if (enemyScript.isThrow == true)
        {
            if (inAir == false)
            {
                enemyBombRb.AddForce((enemyScript.enemyRb.transform.forward + offset) * throwForce, ForceMode.Impulse);
                inAir = true;
            }
        }
        else if (enemyScript.isBomb == false)
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
            enemyScript.isBomb = false;
        }
    }

    IEnumerator BombDelay()
    {
        yield return new WaitForSeconds(4);
        enemyScript.isBomb = false;
        enemyScript.isThrow = false;
    }
    */
}
