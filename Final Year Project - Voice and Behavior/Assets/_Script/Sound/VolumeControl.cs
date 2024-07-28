using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer audioMixersfx; // Reference to the Audio Mixer for SFX
    public Slider volumeSlidersfx;   // Reference to the UI Slider for SFX
    public AudioMixer audioMixerbgm; // Reference to the Audio Mixer for BGM
    public Slider volumeSliderbgm;   // Reference to the UI Slider for BGM

    private void Start()
    {
        // Initialize the slider value based on the current volume for SFX
        float currentVolumeSFX;
        if (audioMixersfx.GetFloat("SFXVolume", out currentVolumeSFX))
        {
            volumeSlidersfx.value = currentVolumeSFX;
        }

        // Add a listener to the slider to call the SetVolumeSFX function whenever the value changes
        volumeSlidersfx.onValueChanged.AddListener(SetVolumeSFX);

        // Initialize the slider value based on the current volume for BGM
        float currentVolumeBGM;
        if (audioMixerbgm.GetFloat("BGMVolume", out currentVolumeBGM))
        {
            volumeSliderbgm.value = currentVolumeBGM;
        }

        // Add a listener to the slider to call the SetVolumeBGM function whenever the value changes
        volumeSliderbgm.onValueChanged.AddListener(SetVolumeBGM);
    }

    // This method will be called whenever the slider value changes for SFX
    public void SetVolumeSFX(float volume)
    {
        audioMixersfx.SetFloat("SFXVolume", volume);
    }

    // This method will be called whenever the slider value changes for BGM
    public void SetVolumeBGM(float volume)
    {
        audioMixerbgm.SetFloat("BGMVolume", volume);
    }
}









