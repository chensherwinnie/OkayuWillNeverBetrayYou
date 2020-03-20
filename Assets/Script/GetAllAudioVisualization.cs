using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAllAudioVisualization : MonoBehaviour
{
    public static AudioVisualization[] AudioVisualizations;

    // Start is called before the first frame update
    void Start()
    {
        AudioVisualizations = GetComponentsInChildren<AudioVisualization>();
    }
}
