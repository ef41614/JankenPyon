using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{

    Slider slider;
    BGM_SE_Manager BGM_SE_Manager;

    void Start()
    {
        slider = GetComponent<Slider>();
        BGM_SE_Manager = FindObjectOfType<BGM_SE_Manager>();
    }

    public void OnValueChanged()
    {
//        BGM_SE_Manager.audioSource.Volume = slider.value;
       // BGM_SE_Manager. = slider.value;

    }

}