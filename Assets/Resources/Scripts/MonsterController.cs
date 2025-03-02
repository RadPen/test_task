using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float attackRange;
    public float damage;
    public float speed;
    public float rotationSpeed;
    public float agroRadius;

    private Transform player;
    private Rigidbody2D rBody;

    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        rBody = GetComponent<Rigidbody2D>();
        player = obj.transform;
    }

    void MoveTo(Vector2 direction)
    {
        rBody.velocity = direction.normalized * speed;
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;
            if (direction.magnitude < attackRange)
            {
                player.GetComponent<HealthControlling>().TakeDamage(damage);
            }
            if (direction.magnitude > agroRadius)
            {
                direction = new Vector2();
            }
            MoveTo(direction);
        }
    }
}
