using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    public Vector2 direction = new Vector2(1, 0);
    public GameObject explosion;
    public float speed = 2;

    public Vector2 velocity;

    public bool isEnemy = false;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        velocity = direction * speed;
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos += velocity * Time.fixedDeltaTime;

        transform.position = pos;
    }
    void DestroyDestructableRocket()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);

        Level.instance.RemoveDestructable();
        Destroy(gameObject);
    }
}
