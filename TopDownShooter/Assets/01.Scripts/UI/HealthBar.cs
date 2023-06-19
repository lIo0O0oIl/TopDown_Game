using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private int healthAmountPerSep = 5;     // 체력 5당 유닛 한칸씩
    [SerializeField]
    private float barSize = 1f;     // 우리 체력바 1유닛 짜리니까 1로
    [SerializeField]
    private Vector2 sepSize;

    private Transform barTrm, separator, barBackground;

    private int maxHealth = 0;
    private int health = 0;

    private MeshFilter sepMeshFiller;
    private Mesh sepMesh;
    private MeshRenderer sepMeshRenderer;

    private void Awake()
    {
        barTrm = transform.Find("Bar");
        barTrm.localScale = new Vector3(0, 1, 1);

        separator = transform.Find("SeparatorContainer/Separator");
        sepMeshFiller = separator.GetComponent<MeshFilter>();
        sepMeshRenderer = separator.GetComponent<MeshRenderer>();

        sepMeshRenderer.sortingLayerName = "Top";
        sepMeshRenderer.sortingOrder = 20;
        barBackground = transform.Find("BarBackground");
        barSize = barBackground.localScale.x;

        //gameObject.SetActive(false);
    }

    private void CalcSeparator(int value)
    {
        sepMesh = new Mesh();
        SpriteRenderer sr = barBackground.GetComponent<SpriteRenderer>();
        int sepCount = Mathf.FloorToInt((float)value / healthAmountPerSep);     // 구분바가 몇 개 그려져야 하는지 갯수

        float boundSize = sr.bounds.size.x;
        float calcSize = (boundSize / sepCount) * 0.1f;
        sepSize.x = Mathf.Min(calcSize, sepSize.x);

        Vector3[] vertices = new Vector3[(sepCount - 1) * 4];
        Vector2[] uv = new Vector2[(sepCount - 1) * 4];
        int[] triangles = new int[(sepCount - 1) * 6];

        float barGap = barSize / value;

        for (int i = 0; i < sepCount - 1; i++)
        {
            Vector3 pos = new Vector3(barGap * (i + 1) * healthAmountPerSep, 0, 0);
            int vIndex = i * 4;
            vertices[vIndex + 0] = pos + new Vector3(-sepSize.x, -sepSize.y);
            vertices[vIndex + 1] = pos + new Vector3(-sepSize.x, +sepSize.y);
            vertices[vIndex + 2] = pos + new Vector3(+sepSize.x, +sepSize.y);       // 오른쪽 상단
            vertices[vIndex + 3] = pos + new Vector3(+sepSize.x, -sepSize.y);       // 오른쪽 하단

            uv[vIndex + 0] = Vector2.zero;
            uv[vIndex + 1] = new Vector2(0, 1);
            uv[vIndex + 2] = Vector2.one;
            uv[vIndex + 3] = new Vector2(1, 0);

            int tIndex = i * 6;
            triangles[tIndex + 0] = vIndex + 0;
            triangles[tIndex + 1] = vIndex + 1;
            triangles[tIndex + 2] = vIndex + 2;
            triangles[tIndex + 3] = vIndex + 0;
            triangles[tIndex + 4] = vIndex + 2;
            triangles[tIndex + 5] = vIndex + 3;
        }
        sepMesh.vertices = vertices;
        sepMesh.uv = uv;
        sepMesh.triangles = triangles;

        sepMeshFiller.mesh = sepMesh;   
    }

    public void SetHealth(int health)
    {
        this.health = health;
        if (maxHealth <= 0)
        {
            maxHealth = health;
            CalcSeparator(maxHealth);
            // 맨 처음 값이 셋팅되었다면 세퍼레이터 그려주고
        }

        this.health = Mathf.Clamp(this.health, 0, maxHealth);
        barTrm.DOScaleX((float)this.health / maxHealth, 0.4f);
        if (this.health <= 0) 
        {
            maxHealth = 0;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            gameObject.SetActive(true);
            SetHealth(100);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SetHealth(health - 5);
        }
    }
}
