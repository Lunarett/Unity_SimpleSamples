using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float lifetime = 2.0f;
    [SerializeField] private float damageAmount = 10.0f;
    [SerializeField] private LayerMask collisionLayer;

    private float timer = 0.0f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.velocity = transform.forward * speed;
        StartCoroutine(DestroyAfterLifetime());
    }

    private IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & collisionLayer) == 0) return;
        EnemyHealth damage = other.gameObject.GetComponent<EnemyHealth>();
        if (damage != null)
        {
            Debug.Log("Hit");
            damage.Damage(damageAmount);
        }
        Destroy(gameObject);
    }
}