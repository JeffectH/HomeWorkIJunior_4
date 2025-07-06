using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _reductionValueProbability;
    [SerializeField] private float _reductionValueScale;
    [SerializeField] private float _probabilityNewCubes;

    private Explosion _explosion;
    private Spawner _spawner;
    private Renderer _renderer;

    private void Awake()
    {
        _explosion = new Explosion();
        _spawner = new Spawner();

        _renderer = gameObject.GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
        bool isSpawn = _spawner.IsSpawn(this, _probabilityNewCubes, transform.position, transform.localScale);

        if (isSpawn == false)
            _explosion.Expode(transform.position,transform.localScale);

        Destroy(gameObject);
    }

    public void Initialize(float probabilityNewCubes, Vector3 scaleValue)
    {
        _probabilityNewCubes = probabilityNewCubes;
        transform.localScale = scaleValue;

        ReducingProbability();
        DecreaseScale();
        GenerateNewColor();
    }

    private void ReducingProbability() =>
        _probabilityNewCubes /= _reductionValueProbability;

    private void DecreaseScale() =>
        transform.localScale /= _reductionValueScale;

    private void GenerateNewColor()
    {
        Color randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        _renderer.material.color = randomColor;
    }
}
