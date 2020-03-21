using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIControl : MonoBehaviour
{
    public Text SongTitle;
    public Image Background;
    public Image Pause;

    GameObject UI;
    Vector3 LastMousePosition;
    CanvasGroup UIOpcacity;
    AudioSource _audioSource;
    float SetAlphaTime = 0f;
    float BackgroundAlphaTime = 0f;

    float MaximumAlpha = 45 / 256f;
    float MinimumAlpha = 100 / 256f;

    [Range(0.3f, 3f)] public float OpacitySpeed = 1f;
    public float UIDormantTime = 3f;

    private void Start()
    {
        _audioSource = GetAllAudioVisualization.AudioVisualizations[1]._audioSource;
        LastMousePosition = Input.mousePosition;

        UI = FindObjectOfType<Canvas>().gameObject;
        UIOpcacity = UI.GetComponentInChildren<CanvasGroup>();
        SongTitle.text = _audioSource.clip.name;
    }

    // Update is called once per frame
    void Update()
    {
        SongTitle.text = _audioSource.clip.name;

        if (LastMousePosition != Input.mousePosition)
        {
            UIDormantTime = 3f;
        }

        if(UIDormantTime >= 0)
        {
            UIOpcacity.alpha = 1;
            SetAlphaTime = 0f;
        }
        else
        {
            UIOpcacity.alpha = Mathf.Lerp(1f, 0f, SetAlphaTime);
            SetAlphaTime += 1f * Time.deltaTime;
        }

        LastMousePosition = Input.mousePosition;
        UIDormantTime -= Time.deltaTime;
        SongTitle.text = _audioSource.clip.name;


        Color tempColor = Background.color;
        tempColor.a = Mathf.Lerp(MaximumAlpha, MinimumAlpha, BackgroundAlphaTime);

        BackgroundAlphaTime += 0.5f * Time.deltaTime;

        if(BackgroundAlphaTime > 80f)
        {
            float temp = MaximumAlpha;
            MaximumAlpha = MinimumAlpha;
            MinimumAlpha = temp;
            BackgroundAlphaTime = 0;
        }

        Background.color = tempColor;

        Pause.enabled = UserInput.GameIsPaused;
    }
}
