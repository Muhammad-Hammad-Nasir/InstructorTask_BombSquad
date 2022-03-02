using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bomb;
    public GameObject mine;
    public GameObject stickyBomb;
    public Rigidbody playerRb;
    public bool isBomb;
    public bool isThrow;
    public bool isMinePower;
    public bool isMineBomb;
    public bool isMineThrow;
    public bool isStickyPower;
    public bool isStickyBomb;
    public bool isStickyThrow;

    private Vector3 offset = new Vector3(0, 1, 0);
    private float vertical;
    private float horizontal;
    private float rotateSpeed;
    private float speed;
    private float jumpForce;
    private float yLimit = 5;
    private int MineNum = 3;
    private bool isOnGround;

    void Start()
    {
        jumpForce = 4;
        rotateSpeed = 720;
        speed = 7;
    }

    void Update()
    {
        if (playerRb != null)
        {
            Movement();
            Jump();
            if (!isMinePower && !isStickyPower)
            {
                BombThrow();
            }
            else if (isMinePower)
            {
                MineThrow();
            }
            else if (isStickyPower)
            {
                StickyThrow();
            }
        }
        OutOfBounds();
    }

    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 10;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 7;
        }

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        Vector3 newPosition = new Vector3(horizontal, 0.0f, vertical);

        playerRb.AddForce(Vector3.forward * vertical * speed, ForceMode.Acceleration);
        playerRb.AddForce(Vector3.right * horizontal * speed, ForceMode.Acceleration);

        if (newPosition != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(newPosition, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround == true)
        {
            isOnGround = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void BombThrow()
    {
        if (Input.GetKeyDown(KeyCode.E) && isBomb == false)
        {
            isBomb = true;
            Instantiate(bomb, transform.position + offset, transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.E) && isThrow == false && isBomb == true)
        {
            isThrow = true;
        }
    }

    void MineThrow()
    {
        if (Input.GetKeyDown(KeyCode.E) && isMineBomb == false && MineNum > 0)
        {
            isMineBomb = true;
            Instantiate(mine, transform.position + offset, transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.E) && isMineThrow == false && isMineBomb == true)
        {
            isMineThrow = true;
            isMineBomb = false;
            MineNum--;
        }
        else if (MineNum == 0)
        {
            isMinePower = false;
            MineNum = 3;
        }
    }

    void StickyThrow()
    {
        if (Input.GetKeyDown(KeyCode.E) && isStickyBomb == false)
        {
            isStickyBomb = true;
            Instantiate(stickyBomb, transform.position + offset, transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.E) && isStickyThrow == false && isStickyBomb == true)
        {
            isStickyThrow = true;
        }
    }

    void OutOfBounds()
    {
        if (transform.position.y < -yLimit)
        {
            Debug.Log("GameOver");
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 awayFromPlayer = (transform.position - collision.transform.position);
            playerRb.AddForce(awayFromPlayer * 5, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mine"))
        {
            isMinePower = true;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Sticky"))
        {
            isStickyPower = true;
            StartCoroutine(StickyDelay());
            Destroy(other.gameObject);
        }
    }

    IEnumerator StickyDelay()
    {
        yield return new WaitForSeconds(20);
        isStickyPower = false;
    }
}
