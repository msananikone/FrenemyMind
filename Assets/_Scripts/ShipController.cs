using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour {

	public float speed = 5f;
    public Transform bulletSpawn;
    public GameObject bulletPrefab;
	private Rigidbody2D rb;

    public Text txtAmmo;
    private int ammoCount;
	public int initAmmo = 200;

	public Text txtScore;
	private int scoreCount;

	public Text txtHealth;
	private int healthCount;
	public int initHealth = 25;

    public AudioSource laserFire;
	public AudioSource playerHit;

	// Use this for initialization
	void Start () {
        ammoCount = initAmmo;
        txtAmmo.text = "Ammo: " + ammoCount;

		txtScore.text = "Score: " + scoreCount;

		healthCount = initHealth;
		txtHealth.text = "Health: " + healthCount;
		rb = gameObject.GetComponent<Rigidbody2D>();        
	}

    // Update is called once per frame
    void FixedUpdate() {

        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        // Debug.Log ("Horizontal: " + moveH);
        // Debug.Log ("Vertical: " + moveV);

        Vector2 motion = new Vector2(moveH, moveV);

        rb.AddForce(motion * speed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
		//x axis
		if (transform.position.x <= -8f) {
			transform.position = new Vector2 (-8f, transform.position.y);
		} else if (transform.position.x >= 8f) {
			transform.position = new Vector2 (8f, transform.position.y);
		}
		//y axis
		if (transform.position.y <= -5f) {
			transform.position = new Vector2 (transform.position.x, -5);
		} else if (transform.position.y >= 5f) {
			transform.position = new Vector2 (transform.position.x, 5);
		}
  	}


	void Fire()
    {
        if(ammoCount > 0) { 
            ammoCount--;
            txtAmmo.text = "Ammo: " + ammoCount;
            laserFire.Play();
            var bullet = (GameObject) Instantiate(bulletPrefab,
                bulletSpawn.position, bulletSpawn.rotation);

            Vector2 bulletMotion = new Vector2(10f, 0f);
            bullet.GetComponent<Rigidbody2D>().AddForce(bulletMotion * 30);
            Destroy(bullet, 2f);
		}
    }

	void OnTriggerEnter2D(Collider2D other){
        if (other.tag.Equals("enemy"))
        {
			healthCount = 0;
			txtHealth.text = "Health: " + healthCount;
			playerHit.Play ();
            Destroy(other.gameObject);
            Destroy(gameObject);
            GameController.instance.PlayerDead();
        }
	}

	public void updateScore(int point){
		scoreCount = scoreCount + point;
		txtScore.text = "Score: " + scoreCount;
	}
}
