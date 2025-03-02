using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private float speed;
    private float lifetime;
    private float damage;
    private Rigidbody2D rb;

    void Start()
    {
        Destroy(gameObject, lifetime);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var let = other.gameObject;
        if (let.CompareTag("Enemy"))
        {
            let.GetComponent<HealthControlling>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SetParameters(Weapon weapon)
    {
        speed = weapon.speedProjectile;
        lifetime = weapon.distanceWeapon / 15f;
        damage = weapon.damageWeapon;
    }
}
