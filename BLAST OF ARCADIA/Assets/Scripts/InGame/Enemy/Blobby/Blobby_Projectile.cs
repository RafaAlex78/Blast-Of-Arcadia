using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blobby_Projectile : MonoBehaviour
{
    private float _damage;


    public float Damage { get => _damage; set => _damage = value; }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime*1000);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            collision.GetComponent<Rigidbody2D>().GetComponent<IDamageable>().TakeDemage(Damage);
            Destroy(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
