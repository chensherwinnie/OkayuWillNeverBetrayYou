using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    [Header("Round Mode Setting")]
    [SerializeField] private Vector3 RoundModePosition;
    [SerializeField] public Vector3 RoundModeRotation;

    [Header("Flat Mode Setting")]
    [SerializeField] public Vector3 FlatModePostion;
    [SerializeField] public Vector3 FlatModeRotation;

    private float RotationSpeed = 0;
    public AudioCubes _audioCubes;

    private float velocity;

    // Start is called before the first frame update
    void Start()
    {
        _audioCubes = FindObjectOfType<AudioCubes>();

        if (_audioCubes.RoundModeActivate)
            transform.position = RoundModePosition;
        else
            transform.position = FlatModePostion;

        velocity = Vector3.Distance(RoundModePosition, FlatModePostion) / DataSetting.SchemeChangeTime;

    }

    // Update is called once per frame
    void Update()
    {

        if (_audioCubes.RoundModeActivate)
        {
            transform.position = Vector3.MoveTowards(transform.position, RoundModePosition, velocity * Time.deltaTime);
            transform.rotation = Quaternion.Euler(RoundModeRotation);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, FlatModePostion, velocity * Time.deltaTime);
            transform.rotation = Quaternion.Euler(FlatModeRotation);

            if (transform.rotation == Quaternion.Euler(FlatModeRotation))
            {
                RotationSpeed = 1;
            }
        }

    }
}
