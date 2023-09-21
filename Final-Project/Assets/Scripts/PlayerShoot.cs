using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float shootCooldown = 1.0f;
    public int maxAmmo = 5;
    public float projectileForce = 10.0f;
    public bool displayRaycast = true;

    private int currentAmmo;
    private bool canShoot = true;
  
    private Vector2 inputDirection;



    public void OnMove(InputValue inputValue)
    {
        Vector2 inputvector = inputValue.Get<Vector2>();


        inputDirection = inputvector.normalized;
    }
    

        private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
        if (canShoot && currentAmmo > 0 && inputDirection != Vector2.zero)
        {
            // Calculate the shooting direction based on the input direction
            float angle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    public void OnShoot()
    {
        if (canShoot && currentAmmo > 0 && inputDirection != Vector2.zero)
        {
            // Calculate the shooting direction based on the input direction
            float angle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(0, 0, angle);
            Shoot();
        }
    }
    private void Shoot()
    {
        if (projectilePrefab && firePoint)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
              rb.AddForce(inputDirection * projectileForce, ForceMode2D.Impulse);
            }

            if (displayRaycast)
            {
                Debug.DrawRay(firePoint.position, firePoint.up * 10.0f, Color.red, 1.0f);
            }

            currentAmmo--;

            if (currentAmmo <= 0)
            {
                canShoot = false;
                StartCoroutine(RechargeAmmo());
            }
        }
    }

    private IEnumerator RechargeAmmo()
    {
        yield return new WaitForSeconds(shootCooldown);
        currentAmmo = maxAmmo;
        canShoot = true;
    }
}
