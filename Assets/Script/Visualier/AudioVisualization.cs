using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
 *  4. Create buffer?
 */

[RequireComponent(typeof(AudioSource))]
public class AudioVisualization : MonoBehaviour
{
    [SerializeField] AudioMixerGroup MainGroup;
    [SerializeField] AudioMixerGroup MicrophoneGroup;

    public float[] _samples;
    private const int _numberOfSamples = 512;

    private float[] _privateFrequencySamples;
    private const int _numberOfFrequencySamples = 8;
    private float[] _highestFrequencySamples;
    private float[] _bufferDecreaseSpeed;

    public float[] FrequencySamples;
    
    public float _samplesAverage;
    public float Average;

    public AudioSource _audioSource;
    private bool _isMain;

    // Start is called before the first frame update
    void Start()
    {
        _samples = new float[_numberOfSamples];
        _privateFrequencySamples = new float[_numberOfFrequencySamples];
        _highestFrequencySamples = new float[_numberOfFrequencySamples];
        _bufferDecreaseSpeed = new float[_numberOfFrequencySamples];

        FrequencySamples = new float[_numberOfFrequencySamples];

        _audioSource = GetComponent<AudioSource>();

        
        if (this.name == "Main AudioSource")
        {
            _isMain = true;
        }
        /*
        if (Microphone.devices.Length > 0 && _isMain)
        {
            _audioSource.clip = Microphone.Start(Microphone.devices[0], true, 999, 100);
            while (!(Microphone.GetPosition(null) > 0)){}
        }
        */
        
        _audioSource.Play();
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
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.BlackmanHarris);
    }

    void GenerateFrequencySamples()
    {
        int _samplesIndex = 0;

        for(int _frequnecyIndex = 0; _frequnecyIndex < _privateFrequencySamples.Length; _frequnecyIndex++)
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
            _privateFrequencySamples[_frequnecyIndex] = average;
        }

        UpdateHighestFrequency();
        GenerateFrequencyBuffer();

    }

    void UpdateHighestFrequency()
    {
        for(int i = 0; i < _privateFrequencySamples.Length; i++)
            _highestFrequencySamples[i] = _privateFrequencySamples[i] > _highestFrequencySamples[i] ? _privateFrequencySamples[i] : _highestFrequencySamples[i];
    }

    void GenerateFrequencyBuffer()
    {
        for(int i  = 0; i < _privateFrequencySamples.Length; i++)
        {
            if(_privateFrequencySamples[i] > FrequencySamples[i])
            {
                FrequencySamples[i] = _privateFrequencySamples[i];
                _bufferDecreaseSpeed[i] = 1f;
            }
            else if(_privateFrequencySamples[i] < FrequencySamples[i])
            {
                FrequencySamples[i] *= _bufferDecreaseSpeed[i];
                _bufferDecreaseSpeed[i] /= 1.07f;
            }
        }
    }

    void GenerateAverage()
    {
        _samplesAverage = 0;
        foreach(float fequency in FrequencySamples){
            _samplesAverage += fequency;
        }
        _samplesAverage /= FrequencySamples.Length;

        if (_samplesAverage > Average)
        {
            Average = _samplesAverage;
        }
        else if(_samplesAverage < Average)
        {
            Average *= 0.95f;
        }
    }

    public void ChangeClip(string newSong)
    {
        Debug.Log(newSong);
        _audioSource.Stop();
        _audioSource.clip = Resources.Load<AudioClip>("Audio/BackgroundMusic/" + newSong);
        _audioSource.Play();
    }

    public void AdjustVolume(float newVolume)
    {
        _audioSource.volume = newVolume;
    }
}
