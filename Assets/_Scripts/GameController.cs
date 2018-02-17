using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static GameController instance;

    public GameObject enemyCraftPrefab;
    private float speed;
    private float timeElapsed;

    public bool gameOver;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        gameOver = false;
    }
    // Use this for initialization
    void Start () {
        speed = Random.Range(20f, 50f) * -1;
        speed = -100;
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed % 3.5 < 0.1)
        {
            Vector3 spawnOffset = new Vector3(0f, Random.Range(-5f, 5f), 0f);
            var enemy = (GameObject)Instantiate(enemyCraftPrefab,
                transform.position + spawnOffset, transform.rotation);
            enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0f));
        }

        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enemy collided with: " + other.tag);
        if (other.tag.Equals("boundary"))
        {
            Destroy(gameObject);
        }
    }

    public void PlayerDead()
    {
        gameOver = true;
    }
}
