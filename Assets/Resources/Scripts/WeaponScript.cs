using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Weapon weapon;
    public GameObject projectilePrefab;

    private LoadingSceneScript loadingScript;
    private Transform shootPoint;

    void Start()
    {
        InitialData();
    }

    void Update()
    {
        Transform nearestTarget = FindNearestTargetWithTag("Enemy");
        var angle = 0f;

        if (nearestTarget != null)
        {
            var direction = nearestTarget.position - transform.position;
            if (direction.magnitude < weapon.distanceWeapon)
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
        TurnOfArms(angle);
    }

    private void InitialData()
    {
        loadingScript = FindObjectOfType<LoadingSceneScript>();
        shootPoint = this.transform.GetChild(0);
    }

    private void TurnOfArms(float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, weapon.rotationSpeed * Time.deltaTime);
    }

    private Transform FindNearestTargetWithTag(string tag)
    {
        var targets = GameObject.FindGameObjectsWithTag(tag);
        Transform nearestTarget = null;
        var nearestDistance = Mathf.Infinity;

        foreach (GameObject target in targets)
        {
            var distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestTarget = target.transform;
            }
        }
        return nearestTarget;
    }

    public void Shoot()
    {
        if (loadingScript.clip.ShootClip())
        {
            var projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            projectile.GetComponent<ProjectileScript>().SetParameters(weapon);

        }
    }
}
