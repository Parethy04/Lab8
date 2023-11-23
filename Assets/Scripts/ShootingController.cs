using System.Collections;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private ProjectileType currentProjectileType;

    [SerializeField] private float fireRate = 0.5f; // Adjust fire rate as needed
    private float nextFire = 0.0f;
    public enum ProjectileType
    {
        Single,
        Burst,
        Auto
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            switch (currentProjectileType)
            {
                case ProjectileType.Single:
                    FireSingle();
                    break;
                case ProjectileType.Burst:
                    StartCoroutine(FireBurst());
                    break;
                case ProjectileType.Auto:
                    FireAuto();
                    break;
            }
            nextFire = Time.time + fireRate;
        }
    }

    private void FireSingle()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation); //shoots once
    }

    private IEnumerator FireBurst()
    {
        for (int i = 0; i < 3; i++) // affects the burst count
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(0.1f); // Adjust the time between each shot
        }
    }

    private IEnumerator AutoFireCoroutine()
    {
        while (true)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(fireRate); // lets adjust firerate
        }
    }

    private void FireAuto()
    {
        StartCoroutine(AutoFireCoroutine()); //lets gameobject continue shooting

    }

    private void StopAutoFire()
    {
        StopCoroutine(AutoFireCoroutine());
    }
}
