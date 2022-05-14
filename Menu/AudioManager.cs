using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;

    private string masterKey = "MasterVolume";
    // Start is called before the first frame update
    void Start()
    {
        // if key doesn't exist, default is second param
        slider.value = PlayerPrefs.GetFloat(masterKey, 0.75f);
    }

    // Used in OnValueChanged of slider
    public void adjustVolume(float sliderVal)
    {
        audioMixer.SetFloat(masterKey, Mathf.Log10(sliderVal) * 20);
        PlayerPrefs.SetFloat(masterKey, sliderVal);
    }
}
