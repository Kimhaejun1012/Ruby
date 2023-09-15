using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10f);
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2D.AddForce(direction * force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            //적 데미지 적용
            Destroy(rigidbody2D);

        }
    }

}
