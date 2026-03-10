using System;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class SoftBodyPhysics : MonoBehaviour
{
    [Range(0.0f, 2.0f)]
    public float softness = 1.0f;

    [Range(0.01f, 1.0f)]
    public float damping = 0.1f;

    public float stiffness = 1.0f;

    private void Start()
    {
        CreateSoftBodyPhysics();
    }

    void CreateSoftBodyPhysics()
    {
        SkinnedMeshRenderer smr = GetComponent<SkinnedMeshRenderer>();
        if (smr == null)
        {
            Debug.Log("Subnormal");
            return;
        }
        Cloth cloth = gameObject.AddComponent<Cloth>();
        cloth.damping = damping;
        cloth.bendingStiffness = stiffness;

        cloth.coefficients = GenerateClothCoefficients(smr.sharedMesh.vertices.Length);
    }

    private ClothSkinningCoefficient[] GenerateClothCoefficients(int vertexCount)
    {
        ClothSkinningCoefficient[] coefficients = new ClothSkinningCoefficient[vertexCount];
        for (int i = 0; i < vertexCount; i++)
        {
            coefficients[i].maxDistance = softness;
            coefficients[i].collisionSphereDistance = 0.0f;
        }
        return coefficients;
    }
}
