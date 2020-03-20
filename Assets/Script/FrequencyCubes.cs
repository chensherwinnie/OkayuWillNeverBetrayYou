using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Generate 8 audio cubes where the parent gameobject is
 */
public class FrequencyCubes : MonoBehaviour
{
    struct Cube
    {
        public GameObject cube;
    }

    [SerializeField] private GameObject _CubePrefeb;
    private Cube[] _Cubes;
    private float CubeLength;

    [Header("General Setting")]
    public int AudioSourceIndex = 0;

    [SerializeField] public float Height = 1000f;
    [SerializeField] private float CubeLengthX = -1f;
    [SerializeField] private float CubeLengthZ = -1f;
    [SerializeField] private float IntervalDistance = 0f;
    [SerializeField][Range(0.2f, 3f)] private float BaseHeight = 0.5f;

    private void Start()
    {
        _Cubes = new Cube[GetAllAudioVisualization.AudioVisualizations[AudioSourceIndex]._frequencySamples.Length];
        CubeLength = 1f;

        CubeLengthX = CubeLengthX < 0 ? CubeLength : CubeLengthX;
        CubeLengthZ = CubeLengthZ < 0 ? CubeLength : CubeLengthZ;


        for (int i = 0; i < _Cubes.Length; i++)
        {
            Cube cube = new Cube { };
            cube.cube = (GameObject)Instantiate(_CubePrefeb);
            cube.cube.transform.parent = this.transform;
            float temp = CubeLengthX / 2 + CubeLengthX * (i - _Cubes.Length / 2);
            cube.cube.transform.position = this.transform.position + new Vector3(temp * (IntervalDistance + 1), 0, 0);
            cube.cube.layer = 8;
            cube.cube.name = "Audio Sample Cube " + i;
            _Cubes[i] = cube;
        }

        this.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void Update()
    {
        AdjustHeight();
    }

    private void AdjustHeight()
    {
        for (int i = 0; i < _Cubes.Length; i++)
            _Cubes[i].cube.transform.localScale = new Vector3(CubeLengthX, GetAllAudioVisualization.AudioVisualizations[AudioSourceIndex]._frequencySamples[i] * Height + BaseHeight, CubeLengthZ);
    }
}
