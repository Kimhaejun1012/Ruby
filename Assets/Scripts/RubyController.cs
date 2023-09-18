using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class RubyController : MonoBehaviour
{
    public ParticleSystem hitParticle;

    public Projectile projectilePrefab;
    //AudioSource audioSource;
    private float maxHp = 5f;
    private float currentHpUI;
    private float currentHp;
    public float speed = 4f;

    public Image image;

    private Animator animator;
    private Rigidbody2D rigidbodt2d;
    private SpriteRenderer spriteRenderer;

    private Vector2 lookDirection = new Vector2(1, 0);
    private Vector2 direction;

    private float timeInvicible = 1f;
    private bool isInvicible = false;
    private float invincibleTimer;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbodt2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        hitParticle = GetComponentInChildren<ParticleSystem>(); //�ڽĿ�����Ʈ�� ���۳�Ʈ �˻� �ϳ��� ���� ���� ����
    }

    private void Start()
    {
        currentHp = maxHp;
        currentHpUI = maxHp;
        //hitParticle.Stop();
    }

    private void FixedUpdate()
    {
        //���� ������Ʈ �ֱ⿡ ���缭 �ѹ��� ȣ��
        Vector2 position = rigidbodt2d.position;
        position += direction * speed * Time.deltaTime;
        rigidbodt2d.MovePosition(position);
    }

    // Update is called once per frame
    public void Update()
    {
        //Ű �Է��� ������Ʈ
        //Debug.Log(Time.deltaTime);

        if(isInvicible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0 )
            {
                isInvicible = false;
                spriteRenderer.color = Color.white;

            }
        }

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        direction = new Vector2(h, v);
        var directionMag = direction.magnitude;

        if (directionMag > 1)
        {
            direction.Normalize();
        }

        if(directionMag > 0)
        {
            lookDirection = direction;
        }

        if(Input.GetButtonDown("Fire1"))
        {
            var lookNomalized = lookDirection.normalized;

            var pos = rigidbodt2d.position;
            pos.y += 0.5f;

            pos += lookNomalized * 0.5f;

            var projectile = Instantiate(projectilePrefab, pos, Quaternion.identity);
            projectile.Launch(lookDirection.normalized, 10f);
            animator.SetTrigger("Launch");
        }

        animator.SetFloat("Speed", directionMag);
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);

        currentHp = Mathf.Clamp((currentHp -= Time.deltaTime), currentHpUI, maxHp);

        image.fillAmount = currentHp / maxHp;

    }

    public void TakeDamage(int damage)
    {
        if(isInvicible)
        {
            return;
        }

        spriteRenderer.color = Color.red;

        currentHpUI = Mathf.Clamp(currentHpUI - damage, 0, maxHp);

        isInvicible = true;
        invincibleTimer = timeInvicible;

        //audioSource.PlayOneShot(hitsound);
        animator.SetTrigger("Hit");

        hitParticle.Stop(); // ���������� ����Ʈ�� ó������ ����ϰ� �������� Stop->Play�� �ؾ���
        hitParticle.Play();
    }
    public void TakeHeal(int heal)
    {
        spriteRenderer.color = Color.white;

        currentHpUI = Mathf.Clamp(currentHpUI + heal, 0, maxHp);

        Debug.Log(currentHp);
    }
}
