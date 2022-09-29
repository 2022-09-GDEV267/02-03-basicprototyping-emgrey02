using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ball : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;

    [SerializeField]
    private GameObject damageParticles;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();

        if (obstacle != null)
        {
            obstacle.TakeDamage(3);
        }

        Instantiate(damageParticles, transform.position, damageParticles.transform.rotation);
        Destroy(this.gameObject);
    }

   
}
