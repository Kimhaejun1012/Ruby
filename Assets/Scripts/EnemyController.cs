using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float speed = 4f;
    public bool isFix = false;
    public float time;
    public float maxenemyHp = 5f;
    public float enemyHp;
    public float enemyHpUi;
    public float hpui;

    public Color oriColor;

    private Animator animator;
    private Rigidbody2D rigidbodt2d;
    private SpriteRenderer spriteRenderer;

    public Image hpbarUI;

    public ParticleSystem hitParticle;
    public ParticleSystem smokeParticle;

    //AudioSource audioSource;

    private Vector2 lookDirection = new Vector2(1, 0);
    public Vector2 direction = new Vector2(0, 0);

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbodt2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        enemyHp = maxenemyHp;
        enemyHpUi = maxenemyHp;
        oriColor = spriteRenderer.color;

    }

    private void FixedUpdate()
    {
        //물리 업데이트 주기에 맞춰서 한번씩 호출
        Vector2 position = rigidbodt2d.position;
        position += direction * speed * Time.deltaTime;
        rigidbodt2d.MovePosition(position);
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time > 2f)
        {
            direction = -direction;
            time = 0f;
        }

        var directionMag = direction.magnitude;

        if (directionMag > 1)
        {
            direction.Normalize();
        }

        if (directionMag > 0)
        {
            lookDirection = direction;
        }

        animator.SetFloat("Speed", directionMag);
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);

        enemyHp = Mathf.Clamp((enemyHp -= Time.deltaTime), enemyHpUi, maxenemyHp);
        
        hpbarUI.fillAmount = enemyHp / maxenemyHp;

        Debug.Log(enemyHp);
    }

    public void EnemyHit()
    {
        hitParticle.Play();
        enemyHpUi = Mathf.Clamp(enemyHpUi - 1, 0, maxenemyHp);
        Debug.Log("ENEMY HP : " + enemyHp);
        float redval = 1f - (enemyHp / (float)maxenemyHp);

        Color newColor = new Color();
        newColor = Color.red;
        newColor.r = redval;
        
        if (enemyHpUi <= 0)
        {
            spriteRenderer.color = oriColor;
            direction.x = 0;
            animator.SetTrigger("Fix");
            smokeParticle.Play();
        }
        else
        {
            spriteRenderer.color = newColor;
        }
    }
}
