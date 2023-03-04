using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Projectile projectilePrefab;
    public Transform fireLocation;
    public float fireRate = 0.1f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= fireRate && Input.GetButtonDown("Fire1"))
        {
            timer = 0f;
            Projectile projectile = Instantiate(projectilePrefab, fireLocation.position, fireLocation.rotation);
        }
    }

}
