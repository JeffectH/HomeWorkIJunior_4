using UnityEngine;

public class Spawner
{
    private int _minNumberCube = 2;
    private int _maxNumberCube = 6;

    public Cube[] Spawn(Cube originalCube, float probabilityNewCubes, Vector3 pointSpawn, Vector3 beforeScaleValue)
    {
        float randomValue = Random.Range(0f, 1f);

        if (randomValue <= probabilityNewCubes)
        {
            int count = Random.Range(_minNumberCube, _maxNumberCube);

            var cubes = new Cube[count];
            
            for (int i = 0; i < count; i++)
            {
                Cube cube = GameObject.Instantiate(originalCube, pointSpawn, Quaternion.identity);
                cube.Initialize(probabilityNewCubes, beforeScaleValue);
                cubes[i] = cube;
            }
            return cubes;
        }
        return new Cube[0];
    }
}
