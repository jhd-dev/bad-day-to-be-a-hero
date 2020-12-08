using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour
{
    public void SetVolume(float level)
	{
        AudioListener.volume = level;
	}
}
