using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _reductionValueProbability;
    [SerializeField] private float _reductionValueScale;
    [SerializeField] private float _probabilityNewCubes;

    [SerializeField] private float _baseMainExplosionForce = 300f;
    [SerializeField] private float _baseMainExplosionRadius = 20f;
    [SerializeField] private float _baseChildExplosionForce = 100f;
    [SerializeField] private float _baseChildExplosionRadius = 10f;
    [SerializeField] private float _childScaleDivider = 2f;
    
    private Explosion _explosion;
    private Spawner _spawner;

    private void Awake()
    {
        _explosion = new Explosion();
        _spawner = new Spawner();
    }

    public void Initialize(float probabilityNewCubes, Vector3 scaleValue)
    {
        _probabilityNewCubes = probabilityNewCubes;
        transform.localScale = scaleValue;

        ReducingProbability();
        DecreaseScale();
        GenerateNevColor();
    }

    private void OnMouseDown()
    {
        float randomValue = Random.Range(0f, 1f);

        if (randomValue <= _probabilityNewCubes){
            Cube[] newCubes = _spawner.Spawn(this, _probabilityNewCubes, transform.position, transform.localScale);

            foreach (Cube cube in newCubes){
                var rb = cube.GetComponent<Rigidbody>();

                rb?.AddExplosionForce(CalculateChildExplosionForce(), transform.position, CalculateChildExplosionRadius());
            }
        }
        else{
            _explosion.ExplodeForAllExcept(transform.position, this, CalculateMainExplosionForce(), CalculateMainExplosionRadius());
        }

        Destroy(gameObject);
    }

    private float CalculateMainExplosionForce()
        => _baseMainExplosionForce / transform.localScale.x;

    private float CalculateMainExplosionRadius()
        => _baseMainExplosionRadius / transform.localScale.x;

    private float CalculateChildExplosionForce()
        => _baseChildExplosionForce / (transform.localScale.x * _childScaleDivider);

    private float CalculateChildExplosionRadius()
        => _baseChildExplosionRadius / (transform.localScale.x * _childScaleDivider);

    private void ReducingProbability()
        => _probabilityNewCubes /= _reductionValueProbability;

    private void DecreaseScale()
        => transform.localScale /= _reductionValueScale;

    private void GenerateNevColor()
    {
        Color randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        GetComponent<Renderer>().material.color = randomColor;
    }
}
