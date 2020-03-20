using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] GlitchEffect glitchEffect;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = FindObjectOfType<AudioSource>();
        glitchEffect = FindObjectOfType<GlitchEffect>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
            Resume();
    }

    private void Resume()
    {
        GameIsPaused = false;
        ChangeGlitchSetting(false);
        _audioSource.Play();
    }
    private void Pause()
    {
        GameIsPaused = true;
        ChangeGlitchSetting(true);
        _audioSource.Pause();
    }

    private void ChangeGlitchSetting(bool on)
    {
        if (on)
        {
            glitchEffect.intensity = 0.5f;
            glitchEffect.colorIntensity = 0.5f;
            glitchEffect.flipIntensity = 0.5f;
        }
        else
        {
            glitchEffect.intensity = 0;
            glitchEffect.colorIntensity = 0;
            glitchEffect.flipIntensity = 0;
        }
    }
}
