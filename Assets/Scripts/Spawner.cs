using UnityEngine;

public class Spawner
{
    private const int MinNumberCube = 2;
    private const int MaxNumberCube = 6;

    public bool IsSpawn(Cube originalCube, float probabilityNewCubes, Vector3 pointSpawn, Vector3 beforeScaleValue)
    {
        float randomValue = Random.Range(0f, 1f);

        if (randomValue <= probabilityNewCubes)
        {
            randomValue = Random.Range(MinNumberCube, MaxNumberCube);

            for (int i = 0; i < randomValue; i++)
            {
                Cube cube = Object.Instantiate(originalCube, pointSpawn, Quaternion.identity);
                cube.Initialize(probabilityNewCubes, beforeScaleValue);
            }
            return true;
        }
        return false;
    }
}
