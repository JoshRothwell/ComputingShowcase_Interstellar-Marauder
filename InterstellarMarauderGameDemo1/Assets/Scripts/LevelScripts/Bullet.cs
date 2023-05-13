using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction = new Vector2(1, 0);
    public float speed = 2;
    public bool isEnemy = false;
    public AudioClip spawnSound;
    public float spawnSoundVolume = 0.5f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
        DontDestroyOnLoad(gameObject);

        rb = GetComponent<Rigidbody2D>();

        // Rotate the bullet sprite to match its direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Play the spawn sound if one is set
        if (spawnSound != null)
        {
            AudioSource.PlayClipAtPoint(spawnSound, transform.position, spawnSoundVolume);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Set the velocity of the bullet based on its direction and speed
        rb.velocity = direction.normalized * speed;
    }
}


