using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureParticleManager : MonoBehaviour
{
    public static TextureParticleManager Instance;
    private MeshParticleSystem meshParticleSystem;

    private List<Particle> shellList;
    private List<Particle> bloodList;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("텍스쳐파티클매니져 없음!");
        }
        Instance = this;

        meshParticleSystem = GetComponent<MeshParticleSystem>();
        shellList = new List<Particle>();
        bloodList = new List<Particle>();
    }

    private void Update()
    {
        // 여기에 블러드 리스트 관련 쓰기
        for (int i = 0; i < bloodList.Count; i++)
        {
            Particle p = bloodList[i];
            p.UpdateParticle();
            if (p.IsComplete())
            {
                bloodList.RemoveAt(i);
                i--;
            }
        }

        for (int i = 0; i < shellList.Count; i++)
        {
            Particle p = shellList[i];
            p.UpdateParticle();
            if (p.IsComplete())
            {
                shellList.RemoveAt(i);
                i--;
            }
        }

        // 디버깅용 코드
        if (Input.GetKeyDown(KeyCode.P))
        {
            ClearBloodAndShell();
        }
    }

    public void SpawnShell(Vector3 pos, Vector3 dir)
    {
        int uvIndex = meshParticleSystem.GetRandomShellIndex();
        float moveSpeed = Random.Range(1.5f, 2.5f);
        Vector3 quadSize = new Vector3(0.15f, 0.15f);
        float slowDownFactor = Random.Range(2f, 2.5f);
        shellList.Add(new Particle(pos, dir, meshParticleSystem, quadSize, Random.Range(0, 360f), uvIndex, moveSpeed, slowDownFactor, true));
    }

    public void SpawnBlood(Vector3 pos, Vector3 dir, float sizeFactor = 1f)
    {
        // 요기를 만들기
        Debug.Log("아야 피나와");
        int uvIndex = meshParticleSystem.GetRandomBloodIndex();
        float moveSpeed = Random.Range(0.3f, 0.5f);
        Vector3 quadSize = new Vector3(1, 1) * sizeFactor;
        float slowDownFactor = Random.Range(0.8f, 1.5f);
        bloodList.Add(new Particle(pos, dir, meshParticleSystem, quadSize, Random.Range(0, 360f), uvIndex, moveSpeed, slowDownFactor, false));
    }

    public void ClearBloodAndShell()
    {
        shellList.Clear();
        bloodList.Clear();
        meshParticleSystem.DestroyAllQuad();        // 하하 다 지워버리자고
    }
}
