using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class Switch : MonoBehaviour
{
    public Image On;
    public Image Off;
    //public Image img;
    int index;
    [SerializeField] Toggle audioToggle;

    Toggle musicToggle;
    
    void Start()
    {
        musicToggle = GetComponent<Toggle>();
        if (AudioListener.volume == 0)
        {
            audioToggle.isOn = false;
        }
    }

    public void ToggleAudioIsOnValueChange(bool audioIn)
    {
        if (audioIn)
        {
            AudioListener.volume = 1;
            Off.gameObject.SetActive(true);
            On.gameObject.SetActive(false);
        }
        else
        {
            AudioListener.volume = 0;
            On.gameObject.SetActive(true);
            Off.gameObject.SetActive(false);
        }
    }
    void Update()
    {
       if (index == 1)
        {
            //img.gameObject.SetActive(false);
        }
       if (index == 0) 
        {
            //img.gameObject.SetActive(true);
        }
    }

    public void ON()
    {
        index = 1;
        Off.gameObject.SetActive(true);
        On.gameObject.SetActive(false);
    }

    public void OFF()
    {
        index = 0;
        On.gameObject.SetActive(true);
        Off.gameObject.SetActive(false);   
    }

}

