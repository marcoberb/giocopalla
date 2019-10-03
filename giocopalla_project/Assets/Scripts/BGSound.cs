using UnityEngine;
using UnityEngine.UI;

public class BGSound : MonoBehaviour
{
    public Image SoundOn;           //icon of sound activated
    public Image SoundOff;          //icon of sound deactivated
    private AudioSource Soundtrack;         //soundtrack present in all scenes*
    
    void Start()
    {
        /* to use always the same object as a soundtrack also in new instance of "Menu" scene (when "Menu" scene is destroyed, object 
        remains cause of "DontDestroy" script but when another "Menu" scene is instantiated the new soundtrack object will be destroyed 
        cause of "DontDestroy" script and this will reference to null) */
        this.Soundtrack = DontDestroy.objs[0].GetComponent<AudioSource>();

        //variable indicates sound preference of player. If there isn't one is set to enabled
        string soundPref = PlayerPrefs.GetString("sound", "enabled");

        if (soundPref.Equals("enabled"))
        {
            //soundtrack audible
            this.Soundtrack.mute = false;
            this.SoundOff.gameObject.SetActive(false);
            this.SoundOn.gameObject.SetActive(true);
        }
        else
        {
            //soundtrack inaudible
            this.Soundtrack.mute = true;
            this.SoundOn.gameObject.SetActive(false);
            this.SoundOff.gameObject.SetActive(true);
        }
    }

    /* void ChangeSoundState(): switches sound and related icons between audible and inaudible */
    public void ChangeSoundState()
    {
        if (PlayerPrefs.GetString("sound").Equals("enabled"))
        {
            //soundtrack inaudible
            PlayerPrefs.SetString("sound", "disabled");
            this.SoundOn.gameObject.SetActive(false);
            this.SoundOff.gameObject.SetActive(true);
            this.Soundtrack.mute = true;

        }
        else
        {
            //soundtrack audible
            PlayerPrefs.SetString("sound", "enabled");
            this.SoundOff.gameObject.SetActive(false);
            this.SoundOn.gameObject.SetActive(true);
            this.Soundtrack.mute = false;
        }
    }
}