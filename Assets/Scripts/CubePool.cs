using System.Collections;
using UnityEngine;
using UniRx.Toolkit;

[System.SerializableAttribute]
public class CubePool : ObjectPool<Transform>
{
    [SerializeField]
    Transform cubePrefab;
    [SerializeField]
    Transform parent;

    public Transform prefab => cubePrefab;

    protected override Transform CreateInstance()
    {
        var newTransform = GameObject.Instantiate<Transform>(cubePrefab, parent);

        return newTransform;
    }

    protected override void OnBeforeRent(Transform instance)
    {
        base.OnBeforeRent(instance);

        Vector3 offset = Vector3.down * instance.transform.localScale.y / 2;

        instance.transform.localPosition = Vector3.zero + offset;
    }
}
