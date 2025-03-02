using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ClipScript : MonoBehaviour
{
    public int currentClip { get; private set; }

    private Text textVolume;
    private int volumeClip;

    void Start()
    {
        textVolume = GetComponent<Text>();
        UpdateClip(0);
    }

    public void UpdateClip(int volume)
    {
        volumeClip = volume;
        currentClip = volume;
        ShowClipe(volume, volume);
    }

    public bool ShootClip()
    {
        currentClip--;

        if (currentClip < 0)
            return false;
        ShowClipe(currentClip, volumeClip);
        return true;
    }

    public void SetClip(int remainder)
    {
        currentClip = remainder;
        textVolume.text = currentClip.ToString() + " / " + volumeClip.ToString();
    }

    public void ShowClipe(int remainder, int volume)
    {
        textVolume.text = remainder.ToString() + " / " + volume.ToString();
    }
}
