using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeshParticleSystem : MonoBehaviour
{
    private const int MAX_QUAD_COUNT = 15000;

    [Serializable]
    public struct ParticleUVPixel
    {
        public Vector2Int uv00Pixel;        // ���� �ϴ��� �ȼ�
        public Vector2Int uv11Pixel;        // ���� ����� �ȼ��� ����
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

    private Vector3[] vertices;
    private Vector2[] uv;
    private int[] triangles;

    private bool updateVertices;
    private bool updateUV;
    private bool updatetriangles;

    private int quadIndex = 0;      // ���� ������ �ε����� �Է��Ѵ�.

    private void Awake()
    {
        mesh = new Mesh();
        vertices = new Vector3[4 * MAX_QUAD_COUNT];
        uv = new Vector2[4 * MAX_QUAD_COUNT];
        triangles = new int[6 * MAX_QUAD_COUNT];

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 1000f);        // �ٿ�� �ڽ� ũ��

        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = "Agent";
        meshRenderer.sortingOrder = 0; // �÷���� �������ٴ� �Ʒ��ʿ� �׷���

        Texture mainTex = meshRenderer.material.mainTexture;
        int tWidth = mainTex.width;     //128
        int tHeight = mainTex.height;       //32

        List<UVCoords> uvCoordList = new List<UVCoords>();
        foreach(ParticleUVPixel pixelUV in uvPixelsArr)
        {
            UVCoords temp = new UVCoords
            {
                uv00 = new Vector2((float) pixelUV.uv00Pixel.x / tWidth, (float) pixelUV.uv00Pixel.y / tHeight),
                uv11 = new Vector2((float) pixelUV.uv11Pixel.x / tWidth, (float) pixelUV.uv11Pixel.y / tHeight),
            };
            uvCoordList.Add(temp);
        }

        UVCoordsArr = uvCoordList.ToArray();
    }

    public int GetRandomBloodIndex()
    {
        return Random.Range(0, 8);
    }

    public int GetRandomShellIndex()
    {
        return Random.Range(8, 9);      // �̰� �ϳ��� ������ Ȯ���� ���ؼ�
    }

    public int AddQuad(Vector3 pos, float rot, Vector3 quadSize, bool skewed, int uvIndex)      // skewed �� �� ��Ʈ�°�..?
    {
        UpdateQuad(quadIndex, pos, rot, quadSize, skewed, uvIndex);

        int spawnedQuadIndex = quadIndex;
        quadIndex = (quadIndex + 1) % MAX_QUAD_COUNT;       // �ִ�ġ�� �Ѿ�� ù��°�� ������

        return spawnedQuadIndex;
    }

    public void UpdateQuad(int quadIndex, Vector3 pos, float rot, Vector3 quadSize, bool skewed, int uvIndex)
    {
        int vIndex0 = quadIndex * 4;
        int vIndex1 = vIndex0 + 1;
        int vIndex2 = vIndex0 + 2;
        int vIndex3 = vIndex0 + 3;

        if (skewed)
        {
            // ��Ʋ��
        }
        else
        {
            vertices[vIndex0] = pos + Quaternion.Euler(0, 0, rot - 180) * quadSize;     // -1, -1
            vertices[vIndex1] = pos + Quaternion.Euler(0, 0, rot - 270) * quadSize;     // -1 1
            vertices[vIndex2] = pos + Quaternion.Euler(0, 0, rot - 0) * quadSize;       // 1, 1
            vertices[vIndex3] = pos + Quaternion.Euler(0, 0, rot - 90) * quadSize;      // 1 -1
        }

        UVCoords _uv = UVCoordsArr[uvIndex];
        uv[vIndex0] = _uv.uv00;
        uv[vIndex1] = new Vector2(_uv.uv00.x, _uv.uv11.y);
        uv[vIndex2] = _uv.uv11;
        uv[vIndex3] = new Vector2(_uv.uv11.x, _uv.uv00.y);

        int tIndex = quadIndex * 6;
        triangles[tIndex + 0] = vIndex0;
        triangles[tIndex + 1] = vIndex1;
        triangles[tIndex + 2] = vIndex2;
        triangles[tIndex + 3] = vIndex0;
        triangles[tIndex + 4] = vIndex2;
        triangles[tIndex + 5] = vIndex3;

        updateVertices = true;
        updateUV = true;
        updatetriangles = true;
    }

    private void LateUpdate()
    {
        if (updateVertices)
        {
            mesh.vertices = vertices;
            updateVertices = false;
        }

        if (updateUV)
        {
            mesh.uv = uv;
            updateUV = false;
        }

        if (updatetriangles)
        {
            mesh.triangles = triangles;
            updatetriangles = false;
        }
    }

    public void DestroyQuad(int quadIndex)
    {
        int vIndex0 = quadIndex * 4;
        int vIndex1 = vIndex0 + 1;
        int vIndex2 = vIndex0 + 2;
        int vIndex3 = vIndex0 + 3;

        vertices[vIndex0] = Vector3.zero;
        vertices[vIndex1] = Vector3.zero;
        vertices[vIndex2] = Vector3.zero;
        vertices[vIndex3] = Vector3.zero;

        updateVertices = true;
    }

    public void DestroyAllQuad()
    {
        Array.Clear(vertices, 0, vertices.Length);      // �̰� C++ ���� memset �ϰ� ����. �ѹ��� �� �о���. 0����
        quadIndex = 0;
        updateVertices = true;
    }

    /*private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            Vector3 quadSize = Vector3.one * 0.5f;
            float rot = Random.Range(0, 360f);
            int uvIndex = GetRandomShellIndex();
            int qIndex = AddQuad(pos, rot, quadSize, false, uvIndex);

            FunctionUpdater.Instance.Create(() =>       // ��� ������Ʈ ���� ������ ��
            {
                pos += new Vector3(1, 1) * 0.8f * Time.deltaTime;
                //rot += 30 * Time.deltaTime;
                quadSize += new Vector3(1, 1) * Time.deltaTime;
                rot += 360f * Time.deltaTime;

                UpdateQuad(qIndex, pos, rot, quadSize, false, uvIndex);
            });
        }
    }*/

    /*private void Update()
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

        mesh.vertices = vertices;       // ����
        mesh.uv = uv;
        mesh.triangles = triangles;

        meshFilter.mesh = mesh;
    }*/
}
