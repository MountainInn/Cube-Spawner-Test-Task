using UnityEngine;
using UniRx.Toolkit;

[System.SerializableAttribute]
public class CubeParticlesPool : ObjectPool<ParticleSystem>
{
    [SerializeField]
    ParticleSystem particleSystemPrefab;
    [SerializeField]
    Transform parent;

    protected override ParticleSystem CreateInstance()
    {
        var newParticleSystem = GameObject.Instantiate<ParticleSystem>(particleSystemPrefab, parent);

        return newParticleSystem;
    }
}
