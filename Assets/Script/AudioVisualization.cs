using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * GOAL:
 *  1. Have the main samples, which has the size of 512. Update them
 *  
 *  2. Create the frequency samples based on the main samples, which has the size of 8.
 *     a. samples to each index's:
 *        2:4:8:16:32:64:128:256
 *        
 *  3. Create the frequency average (float)
 *  
 *  4. 
 */


[RequireComponent(typeof(AudioSource))]
public class AudioVisualization : MonoBehaviour
{
    public float[] _samples;
    private const int _numberOfSamples = 512;

    public float[] _frequencySamples;
    private const int _numberOfFrequencySamples = 8;

    public float _samplesAverage;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _samples = new float[_numberOfSamples];
        _frequencySamples = new float[_numberOfFrequencySamples];
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GenerateMainSamples();
        GenerateFrequencySamples();
        GenerateAverage();
    }

    void GenerateMainSamples()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void GenerateFrequencySamples()
    {
        int _samplesIndex = 0;

        for(int _frequnecyIndex = 0; _frequnecyIndex < _frequencySamples.Length; _frequnecyIndex++)
        {
            int _samplesCount = (int)Mathf.Pow(2, _frequnecyIndex + 1);
            float average = 0;

            if(_frequnecyIndex == 7)
            {
                _samplesCount += 2;
            }

            for(int j = 0; j < _samplesCount; j++)
            {
                average += _samples[_samplesIndex++] * _samplesIndex;
            }

            average /= _samplesCount;
            _frequencySamples[_frequnecyIndex] = average;
        }
    }

    void GenerateAverage()
    {
        _samplesAverage = 0;
        foreach(float fequency in _frequencySamples){
            _samplesAverage += fequency;
        }
        _samplesAverage /= _frequencySamples.Length;
    }
}
