////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2018 Audiokinetic Inc. / All Rights Reserved
//
////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderControlledRTPC : MonoBehaviour
{
    
    private Slider slider;
    public AudioMixer Mixer;
    public bool MusicSliderActive = false;


    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetRTPC(float value)
    {
        if (Menu.isOpen)
        {
           
            if (MusicSliderActive)
            {
                Mixer.SetFloat("musicVol", slider.value);
            }
            else
            {
                Mixer.SetFloat("masterVol", slider.value);
            }  
        }
    }
}