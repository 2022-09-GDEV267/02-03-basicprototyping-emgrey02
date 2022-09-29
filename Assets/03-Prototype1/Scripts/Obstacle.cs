using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Scripting.APIUpdating;

public class Obstacle : MonoBehaviour
{
    public float maxHealth = 10;
    public float health = 10;

    public float pointWorth = 2;

    public HealthBar healthBar;

    private bool forwards = true;

    public delegate void OnDestroyDelegate(float points);
    public event OnDestroyDelegate OnDestroy;

    [SerializeField]
    private GameObject deathParticles;

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar();

        if (health <= 0)
        {
            Instantiate(deathParticles, transform.position, deathParticles.transform.rotation);
            if (OnDestroy != null)
            {
                OnDestroy(pointWorth);
            }
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Vector3 currentPos = transform.position;
        
        if (forwards)
        {
            if (currentPos.z >= -1.5)
            {
                forwards = false;
                currentPos.z -= .05f;
            } else
            {
                currentPos.z += .05f;
            }
        } else
        {
            if (currentPos.z <= -8.5)
            {
                forwards = true;
                currentPos.z += .05f;
            } else
            {
                currentPos.z -= .05f;
            }
        }

        transform.position = currentPos;
    }
}
