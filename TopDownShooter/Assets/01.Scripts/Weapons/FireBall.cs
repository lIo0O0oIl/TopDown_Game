using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireBall : PoolableMono
{
    private Light2D _light;
    public Light2D Light => _light;
    public float LightMaxIntensity = 2.5f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D _rigidbody;
    private bool isDead = false;

    [SerializeField]
    private LayerMask whatIsEnemy;      // 누가 적인가?
    [SerializeField]
    private BulletDataSO bulletData;

    private float currentLifeTime = 0;

    private void Awake()
    {
        _light = transform.Find("BallLight").GetComponent<Light2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Flip(bool value)
    {
        spriteRenderer.flipX = value;
    }

    public void Fire(Vector2 direction)
    {
        currentLifeTime = 0;
        isDead = false;
        _rigidbody.velocity = direction * bulletData.BulletSpeed;
    }

    public override void Init()
    {
        _light.intensity = 0;
        transform.localScale = Vector3.one; ;
        _rigidbody.velocity = Vector2.zero;
        isDead = true;
    }

    private void Update()
    {
        if (isDead) return;
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime >= bulletData.LifeTime)
        {
            HitProcess();
            isDead = true;
            PoolManager.Instance.Push(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( ((1 << collision.gameObject.layer) & whatIsEnemy) > 0){
            HitProcess();
            isDead = true;
            PoolManager.Instance.Push(this);
        }
    }

    [SerializeField]
    private float expRadius = 1.15f;
    private void HitProcess()
    {
        ImpactScript impact = PoolManager.Instance.Pop(bulletData.ObstacleImpactPrefab.name) as ImpactScript;
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
        Vector3 pos = transform.position + transform.right * 0.5f;

        impact.SetPositionAndRotation(pos, rot);

        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, expRadius, whatIsEnemy);

        foreach (Collider2D col in cols)
        {
            if (col.TryGetComponent(out IDamageable health))
            {
                Vector3 normal = (transform.position - col.transform.position).normalized;
                int damage = Random.Range(bulletData.MinDamage, bulletData.MaxDamage);
                health.GetHit(damage, transform.position, normal);
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, expRadius);
            Gizmos.color = Color.white;
        }
    }
#endif
}
