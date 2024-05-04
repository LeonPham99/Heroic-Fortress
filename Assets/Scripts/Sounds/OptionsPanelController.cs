using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanelController : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectSlider;

    private void OnEnable()
    {
        AudioManager.Instance.SetupMusicSlider(musicSlider);
        AudioManager.Instance.SetupSoundSlider(effectSlider);
    }
}
