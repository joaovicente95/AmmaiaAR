using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextToSpeech : MonoBehaviour
{

    #region Variables

    private AndroidJavaClass unityPlayer = null;
    private AndroidJavaObject activity = null;
    private AndroidJavaObject context = null;
    private AndroidJavaClass pluginClass = null;
    private bool flagTTS;
    private GameObject buttonTTS;
        
    #endregion //Variables


    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].textToRead.textToSpeech.Length <= 0 || ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].textToRead.textToSpeech.Length > 4000)
            {

            }
            else
            {
                // Find button on scenne 
                buttonTTS = GameObject.Find("ButtonTTS");

                // Add function on buttons
                buttonTTS.AddComponent<Button>();
                buttonTTS.GetComponent<Button>().onClick.AddListener(Startspeak);

                // Set a image on button 
                buttonTTS.GetComponent<RawImage>().texture = Resources.Load<Texture>("Icons/soundTrue");
                buttonTTS.GetComponent<RawImage>().enabled = true;


                unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

                activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

                context = activity.Call<AndroidJavaObject>("getApplicationContext");

                pluginClass = new AndroidJavaClass("com.example.texttospeech.TTS_Plugin");

                // Send Context to plugin
                pluginClass.CallStatic("setContext", context);

                //BUG 
                pluginClass.CallStatic("speak", ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].textToRead.textToSpeech);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (flagTTS.Equals(true))
        {
            if (!pluginClass.CallStatic<bool>("isSpeaking"))
            {
                buttonTTS.GetComponent<RawImage>().texture = Resources.Load<Texture>("Icons/soundTrue");
                flagTTS = false;
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public void Startspeak()
    {        
        if (pluginClass.CallStatic<bool>("isSpeaking"))
        {
            pluginClass.CallStatic("stopTTS");
            buttonTTS.GetComponent<RawImage>().texture = Resources.Load<Texture>("Icons/soundTrue");
            flagTTS = false;
        }
        else
        {
            buttonTTS.GetComponent<RawImage>().texture = Resources.Load<Texture>("Icons/soundFalse");
            pluginClass.CallStatic("speak", ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].textToRead.textToSpeech);
            flagTTS = true;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public void StopTTS()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (pluginClass.CallStatic<bool>("isSpeaking"))
            {
                pluginClass.CallStatic("stopTTS");
                buttonTTS.GetComponent<RawImage>().texture = Resources.Load<Texture>("Icons/soundTrue");
            }
        }

    }


    /// <summary>
    /// 
    /// </summary>
    public void ExitTTS()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (pluginClass.CallStatic<bool>("isSpeaking"))
            {
                pluginClass.CallStatic("exitTTS");
            }
        }

    }
}
