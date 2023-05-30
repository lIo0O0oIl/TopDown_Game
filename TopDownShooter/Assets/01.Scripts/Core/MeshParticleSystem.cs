using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshParticleSystem : MonoBehaviour
{
    private const int MAX_QUAD_COUNT = 15000;

    [SerializeField]
    public struct ParticleUVPixel
    {
        public Vector2Int uv00Pixel;        // 좌측 하단의 픽셀
        public Vector2Int uv11Pixel;        // 우측 상단의 픽셀을 넣음
    }

    public struct UVCoords
    {
        public Vector2 uv00;
        public Vector2 uv11;
    }

    [SerializeField]
    private ParticleUVPixel[] uvPixelsArr;
    private UVCoords[] UVCoordsArr;

    private Mesh mesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        meshRenderer.sortingLayerName = "Agent";
        meshRenderer.sortingOrder = 50; // 맨 위로 올라오게 설정
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            int idx = Random.Range(0, 8);
            DrawBloodParticle(idx);
        }
    }

    private void DrawBloodParticle(int idx)
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        vertices[0] = new Vector3(0, 0);
        vertices[1] = new Vector3(0, 5);
        vertices[2] = new Vector3(5, 5);
        vertices[3] = new Vector3(5, 0);

        uv[0] = new Vector2(0.125f * idx, 0.5f);
        uv[1] = new Vector2(0.125f * idx, 1f);
        uv[2] = new Vector2((idx + 1) * 0.125f, 1f);
        uv[3] = new Vector2((idx + 1) * 0.125f, 0.5f);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh.vertices = vertices;       // 정점
        mesh.uv = uv;
        mesh.triangles = triangles;

        meshFilter.mesh = mesh;
    }
}
