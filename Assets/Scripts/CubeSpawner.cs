using UnityEngine;
using UniRx;
using DG.Tweening;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private CubePool cubePool;

    [SerializeField]
    private CubeParticlesPool particlesPool;

    [SerializeField]
    private InputPanel inputPanel;

    [SerializeField]
    private Transform leftHatch, rightHatch;

    private float spawnTime, speed, distance;
    private float t = 0;

    private void Start()
    {
        inputPanel.spawnTimeFieldObservable
            .Subscribe((str)=> ParseFloatSafe(str, ref spawnTime))
            .AddTo(gameObject);

        inputPanel.speedFieldObservable
            .Subscribe((str) => ParseFloatSafe(str, ref speed))
            .AddTo(gameObject);

        inputPanel.distanceFieldObservable
            .Subscribe((str) => ParseFloatSafe(str, ref distance))
            .AddTo(gameObject);
    }

    private void ParseFloatSafe(string str, ref float into)
    {
        if (str == null || str.Length == 0) return;

        float parsed = float.Parse(str);

        into = Mathf.Max(1, parsed);
    }

    private void Update()
    {
        if ((t += Time.deltaTime) < spawnTime) return;

        t = 0;;

        DOTween.Sequence()
            .Insert(0, leftHatch.DOLocalMoveZ(-1.1f, 0.15f))
            .Insert(0, rightHatch.DOLocalMoveZ(1.1f, 0.15f))
            .OnKill(()=>{
                SpawnCube();
            });
    }

    private void SpawnCube()
    {
        var cube = cubePool.Rent();

        float duration = distance / speed;

        DOTween.Sequence()
            .Append(cube.transform.DOLocalMoveY(cube.transform.localScale.y/2, 0.25f))
            .Append(cube.transform.DOLocalMoveX(distance, duration).SetEase(Ease.Linear))
            .OnKill(()=> {
                SpawnParticles(cube.transform.position);
                cubePool.Return(cube);
            });
    }

    private void SpawnParticles(Vector3 position)
    {
        var ps = particlesPool.Rent();

        ps.transform.position = position;
        ps.Play();

        float lifetime = ps.main.startLifetime.constant;

        this.InvokeAfter(lifetime, ()=>{ particlesPool.Return(ps); });
    }
}
