using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySongs : MonoBehaviour
{
    [SerializeField] private GameObject _ButtonPrefeb;
    private Object[] audioSources;
    private List<GameObject> AllSongButtons;

    // Start is called before the first frame update
    void Start()
    {
        AllSongButtons = new List<GameObject>();
        float yposition = -15;
        for(int i = 0; i < SoundLibrary.AllMusic.Count; i++)
        {
            GameObject initiateButton = (GameObject)Instantiate(_ButtonPrefeb);
            initiateButton.transform.parent = this.transform;

            initiateButton.GetComponentInChildren<Text>().text = SoundLibrary.AllMusic[i];
            initiateButton.transform.localScale = new Vector3(1, 1, 1);
            initiateButton.transform.position = this.transform.position + new Vector3(0, yposition, 0);
            string temp = SoundLibrary.AllMusic[i];
            initiateButton.GetComponent<Button>().onClick.AddListener(delegate { GetAllAudioVisualization.AudioVisualizations[1].ChangeClip(temp); });
            initiateButton.name = "Song Button: " + temp;

            AllSongButtons.Add(initiateButton);
            yposition += -35;
        }
    }
}
