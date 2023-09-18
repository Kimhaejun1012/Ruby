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
            Destroy(gameObject);
            //적 데미지 적용
            collision.SendMessage("EnemyHit"); //컴퍼넌트에도 붙어있고 게임오브젝트에도 있는 메소드임 호출할 메소드의 이름을 문자열로 넘겨서 호출함, 매개변수는 한개까지 가능 근데 오브젝트 형이라서 인트형 보내면 박싱함
        }
    }
}
