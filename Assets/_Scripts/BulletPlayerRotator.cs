using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayerRotator : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Transform>().Rotate(
            new Vector2(90f, 0f));
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Bullet hit " + other.tag);
        if (other.tag.Equals("enemy")) {
            Destroy(other.gameObject);  // Destroy enemy spacecraft
            Destroy(gameObject); // Destroy bullet
        }
    }
}
