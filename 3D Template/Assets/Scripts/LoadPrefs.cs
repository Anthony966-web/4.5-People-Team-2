<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
=======
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
>>>>>>> c08c2f4748b2e3cd2e0f03db15f9ae9556a0669d

//public class LoadPrefs : MonoBehaviour
//{
//    [Header("General Settings")]
//    [SerializeField] private bool canUse = false;
//    [SerializeField] private MenuController menuController;

<<<<<<< HEAD
    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;

    [Header("Brightness Settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;

    [Header("Quality Game1 Settings")]
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [Header("Fullscreen Settings")]
    [SerializeField] private Toggle fullScreenToggle;

    [Header("Sensitivity Settings")]
    [SerializeField] private TMP_Text SensitvityTextValue = null;
    [SerializeField] private Slider SensitvitySlider = null;

    [Header("Invert Y Settings")]
    [SerializeField] private Toggle invertYToggle = null;

    private void Awake()
    {
        if (canUse)
        {
            if (PlayerPrefs.HasKey("masterVolume"))
            {
                float localVolume = PlayerPrefs.GetFloat("masterVolume");

                volumeTextValue.text = localVolume.ToString("0.0");
                volumeSlider.value = localVolume;
                AudioListener.volume = localVolume;
            }
            else
            {
                menuController.ResetButton("Audio");
            }

            if (PlayerPrefs.HasKey("masterQuality"))
            {
                int localQuality = PlayerPrefs.GetInt("masterQuality");
                qualityDropdown.value = localQuality;
                QualitySettings.SetQualityLevel(localQuality);
            }

            if (PlayerPrefs.HasKey("masterFullscreen"))
            {
                int localFullscreen = PlayerPrefs.GetInt("masterFullscreen");

                if (localFullscreen == 1)
                {
                    Screen.fullScreen = true;
                    fullScreenToggle.isOn = true;
                }
                else
                {
                    Screen.fullScreen = false;
                    fullScreenToggle.isOn = false;
                }
            }

            if (PlayerPrefs.HasKey("masterBrightness"))
            {
                float localBrightness = PlayerPrefs.GetFloat("masterBrightness");

                brightnessTextValue.text = localBrightness.ToString("0.0");
                brightnessSlider.value = localBrightness;
            }
        }
    }
}
=======
//    [Header("Volume Settings")]

//    [Header("Brightness Settings")]

//    [Header("Quality Settings")]

//    [Header("Fullscreen Settings")]

//    [Header("Sensitivity Settings")]

//    [Header("Invert Y Settings")]
//}
>>>>>>> c08c2f4748b2e3cd2e0f03db15f9ae9556a0669d
