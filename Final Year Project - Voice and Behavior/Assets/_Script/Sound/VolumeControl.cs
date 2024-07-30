using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer audioMixersfx; // Reference to the Audio Mixer for SFX
    public Slider volumeSlidersfx;   // Reference to the UI Slider for SFX
    //public AudioMixer audioMixerbgm; // Reference to the Audio Mixer for BGM
    //public Slider volumeSliderbgm;   // Reference to the UI Slider for BGM
	[SerializeField] private GameObject SettingMenu;
	
    private void Start()
    {
        // Initialize the slider value based on the current volume for SFX
        float currentVolumeSFX;
        if (audioMixersfx.GetFloat("MasterVolume", out currentVolumeSFX))
        {
            volumeSlidersfx.value = ConvertDbToSliderValue(currentVolumeSFX);
        }

        // Add a listener to the slider to call the SetVolumeSFX function whenever the value changes
        volumeSlidersfx.onValueChanged.AddListener(SetVolumeSFX);

        /*// Initialize the slider value based on the current volume for BGM
        float currentVolumeBGM;
        if (audioMixersfx.GetFloat("BGMVolume", out currentVolumeBGM))
        {
            volumeSliderbgm.value = ConvertDbToSliderValue(currentVolumeBGM);
        }

        // Add a listener to the slider to call the SetVolumeBGM function whenever the value changes
        volumeSliderbgm.onValueChanged.AddListener(SetVolumeBGM);*/
    }

    // This method will be called whenever the slider value changes for SFX
    public void SetVolumeSFX(float sliderValue)
	{
		float volume = ConvertSliderValueToDb(sliderValue);
		Debug.Log($"Setting SFX Volume to {volume}");
		audioMixersfx.SetFloat("MasterVolume", volume);
	}

	public void SetVolumeBGM(float sliderValue)
	{
		float volume = ConvertSliderValueToDb(sliderValue);
		Debug.Log($"Setting BGM Volume to {volume}");
		audioMixersfx.SetFloat("BGMVolume", volume);
	}

    // Converts a slider value (0 to 1) to a dB value
    private float ConvertSliderValueToDb(float sliderValue)
    {
        return Mathf.Lerp(-80f, 0f, sliderValue);
    }

    // Converts a dB value to a slider value (0 to 1)
    private float ConvertDbToSliderValue(float dbValue)
    {
        return Mathf.InverseLerp(-80f, 0f, dbValue);
    }
	
	public void HideSettingUI()
	{
		SettingMenu.SetActive(false);
	}
}
