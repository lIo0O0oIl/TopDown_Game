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
        _rigidbody.velocity = direction * bulletData.BulletSpeed;
    }

    public override void Init()
    {
        _light.intensity = 0;
        transform.localScale = Vector3.one; ;
        _rigidbody.velocity = Vector2.zero;
        isDead = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 많이 뭐 해주기
    }
}
