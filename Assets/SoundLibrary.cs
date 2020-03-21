using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SoundLibrary : MonoBehaviour
{
    private Object[] audioSources;
    public static List<string> AllMusic;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = Resources.LoadAll("Audio/BackgroundMusic");

        AllMusic = new List<string>();
        //DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/Resources/Audio/BackgroundMusic");
        //FileInfo[] fileInfos = dir.GetFiles("*.*", SearchOption.AllDirectories);

        foreach (Object info in audioSources)
        {

            AllMusic.Add(info.name);
        }
    }
}
