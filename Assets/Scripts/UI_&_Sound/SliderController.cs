using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

// https://johnleonardfrench.com/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/

public class SliderController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _sliderValueText;
    [SerializeField] private Slider _slider;

    //[SerializeField] bool _changeOnlyMusic;

    [SerializeField] private AudioMixer _mixer;

    //private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        float volumeDb = 0;
        _mixer.GetFloat("Volume", out volumeDb);

        float value = Db2frac(volumeDb);
        _sliderValueText.text = value.ToString("0.00");
        _slider.value = value;


        _slider.onValueChanged.AddListener((val) =>
        {
            _sliderValueText.text = val.ToString("0.00");

            _mixer.SetFloat("Volume", Frac2db(val));

            PlayerPrefs.SetFloat(_mixer.name, Frac2db(val));
        });
    }

    private float Frac2db(float v) // Logarithmic transforms
    {
        return Mathf.Log10(v) * 20;
    }
    private float Db2frac(float v)
    {
        return Mathf.Pow(10, v / 20);
    }
}
