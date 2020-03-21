using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCubes : MonoBehaviour
{
    struct Cube
    {
        public GameObject cube;
        public Vector3 RoundPosition;
        public Vector3 FlatPosition;
        public float Velocity;
    }

    [SerializeField] private GameObject _CubePrefeb;
    private Cube[] _Cubes;

    private float _degreePerCube;
    private float CubeLength;
    private bool LastMode;
    private bool NeedToAdjustCubePosition;

    [Header("General Setting")]
    public float Height = 1000;
    [Range(0.0001f, 0.001f)]public float BaseHeight = 0.00015f;

    [Header("RoundMode Setting")]
    public bool RoundModeActivate = false;
    [Range(50f, 150f)]public float Radius = 100f;

    private void Start()
    {
        _Cubes = new Cube[GetAllAudioVisualization.AudioVisualizations[0]._samples.Length];
        CubeLength = 1f / _Cubes.Length * 100;
        _degreePerCube = 360f / _Cubes.Length;

        for (int i = 0; i < _Cubes.Length; i++)
        {
            Cube cube = new Cube { };
            cube.RoundPosition = new Vector3(Mathf.Cos(_degreePerCube * i * Mathf.Deg2Rad) * Radius, 0, Mathf.Sin(_degreePerCube * i * Mathf.Deg2Rad) * Radius);
            cube.FlatPosition = new Vector3(CubeLength * (i - _Cubes.Length/2), 0, 0);
            cube.Velocity = Vector3.Distance(cube.RoundPosition, cube.FlatPosition) / DataSetting.SchemeChangeTime;

            cube.cube = (GameObject)Instantiate(_CubePrefeb);
            cube.cube.transform.parent = this.transform;
            cube.cube.layer = 8;
            cube.cube.name = "Audio Sample Cube " + i;
            cube.cube.transform.position = RoundModeActivate ? cube.RoundPosition : cube.FlatPosition;

            _Cubes[i] = cube;
        }
    }

    private void FixedUpdate()
    {
        if (NeedToAdjustCubePosition)
        {
            NeedToAdjustCubePosition = false;
            if (RoundModeActivate)
                NeedToAdjustCubePosition = _Cubes[0].cube.transform.position != _Cubes[0].RoundPosition;
            else
                NeedToAdjustCubePosition = _Cubes[0].cube.transform.position != _Cubes[0].FlatPosition;
        }

        AdjustHeight();
        MoveCubes();

        if (!NeedToAdjustCubePosition && LastMode != RoundModeActivate)
            NeedToAdjustCubePosition = true;

        LastMode = RoundModeActivate;
    }

    private void MoveCubes()
    {
        if (!NeedToAdjustCubePosition)
            return;

        if (RoundModeActivate)
            MoveOnRoundMode();
        else
            MoveOnFlatMode();
  
    }

    private void MoveOnRoundMode()
    {
        for (int i = 0; i < _Cubes.Length; i++)
            _Cubes[i].cube.transform.position = Vector3.MoveTowards(_Cubes[i].cube.transform.position, _Cubes[i].RoundPosition, _Cubes[i].Velocity * Time.deltaTime);
    }

    private void MoveOnFlatMode()
    {
        for (int i = 0; i < _Cubes.Length; i++)
            _Cubes[i].cube.transform.position = Vector3.MoveTowards(_Cubes[i].cube.transform.position, _Cubes[i].FlatPosition, _Cubes[i].Velocity * Time.deltaTime);
    }

    private void AdjustHeight()
    {
        if(LastMode != RoundModeActivate)
            Height = RoundModeActivate ? Height * 5 : Height / 5;

        if (LastMode != RoundModeActivate)
            return;

        for (int i = 0; i < _Cubes.Length; i++)
             _Cubes[i].cube.transform.localScale = new Vector3(CubeLength, (GetAllAudioVisualization.AudioVisualizations[0]._samples[i] + BaseHeight) * Height, CubeLength);
    }

    //Used by the UI
    public void AdjustRoundMode()
    {
        RoundModeActivate = !RoundModeActivate;
    }

}
