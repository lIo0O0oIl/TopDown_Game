using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle
{
    private Vector3 quadPosition;
    private Vector3 direction;      // 이동방향
    private MeshParticleSystem meshParticleSystem;
    private int quadIndex;
    private Vector3 quadSize;
    private float rotation;
    private int uvIndex;

    private float moveSpeed;
    private float slowDownFactor;       // 느려지는 정도

    private bool isRotate;
    public int QuadIndex => quadIndex;

    public Particle (Vector3 quadPosition, Vector3 direction, MeshParticleSystem meshSystem, Vector3 quadSize, float rotation, int uvIndex, float moveSpeed, float slowDownFactor, bool isRotate = false)
    {
        this.quadPosition = quadPosition;
        this.direction = direction;
        this.meshParticleSystem = meshSystem;
        this.quadSize = quadSize;
        this.rotation = rotation;
        this.uvIndex = uvIndex;
        this.moveSpeed = moveSpeed;
        this.slowDownFactor = slowDownFactor;
        this.isRotate = isRotate;
        quadIndex = meshParticleSystem.AddQuad(this.quadPosition, this.rotation, this.quadSize, false, this.uvIndex);
    }

    public void UpdateParticle()
    {
        quadPosition += direction * moveSpeed * Time.deltaTime;
        if (isRotate)
        {
            rotation += 360f * (moveSpeed * 0.1f) * Time.deltaTime;
        }

        meshParticleSystem.UpdateQuad(quadIndex, quadPosition, rotation, quadSize, false, uvIndex);
        moveSpeed -= moveSpeed * slowDownFactor * Time.deltaTime;       // 이동 속도는 점점 느려지게
    }

    public bool IsComplete()
    {
        return moveSpeed < 0.05f;       // 이 정도까지 느려졌다면 더이상 업데이트 없이 정지해라
    }
}
