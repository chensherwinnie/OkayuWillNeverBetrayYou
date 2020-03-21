using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Generate 8 audio cubes where the parent gameobject is
 */
public class AmplitudeCube : MonoBehaviour
{
    struct Cube
    {
        public GameObject cube;
    }
    Cube cube;

    [SerializeField] private GameObject _CubePrefeb;
    private float CubeLength;

    [Header("General Setting")]
    public int AudioSourceIndex = 0;

    [SerializeField] public float Height = 1000f;
    [SerializeField] private float CubeLengthX = -1f;
    [SerializeField] private float CubeLengthZ = -1f;
    [SerializeField] private float BaseSize = 5f;
    [SerializeField] private bool ExpandXDimension = false;
    [SerializeField] private bool ExpandZDimension = false;


    private void Start()
    {
        CubeLength = 1f;
        CubeLengthX = CubeLengthX < 0 ? CubeLength : CubeLengthX;
        CubeLengthZ = CubeLengthZ < 0 ? CubeLength : CubeLengthZ;

        cube = new Cube { };
        cube.cube = (GameObject)Instantiate(_CubePrefeb);
        cube.cube.transform.parent = this.transform;
        cube.cube.transform.position = this.transform.position;
        cube.cube.layer = 8;
        cube.cube.name = "Audio Sample Cube ";

        this.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void Update()
    {
        AdjustHeight();
    }

    private void AdjustHeight()
    {
        float yDimension = GetAllAudioVisualization.AudioVisualizations[AudioSourceIndex].Average * Height + BaseSize;

        float xDimension = ExpandXDimension ? yDimension : CubeLengthX;
        float zDimension = ExpandZDimension ? yDimension : CubeLengthZ;

        cube.cube.transform.localScale = new Vector3(xDimension, yDimension, zDimension);
    }
}
