using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AudioPlayer : MonoBehaviour
{

    #region Variables

    public AudioSource audioSource;
    private GameObject buttonAudio;
    private bool audioFlag = false;

    #endregion //Variables

    public void Start()
    {
        if (!File.Exists(Application.persistentDataPath + "/" + ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].sound.path))
        {

        }
        else
        {
            // Find button on scenne 
            buttonAudio = GameObject.Find("ButtonAudio");

            // Add function on buttons
            buttonAudio.AddComponent<LongClickButton>();
            buttonAudio.GetComponent<LongClickButton>().onClick.AddListener(ControlAudio);
            buttonAudio.GetComponent<LongClickButton>().onLongPress.AddListener(StopAudio);
            buttonAudio.GetComponent<LongClickButton>().HoldTime = 2f;

            // Set a image on button 
            buttonAudio.GetComponent<RawImage>().texture = Resources.Load<Texture>("Icons/musicPlayer");

            // Load Clip
            StartCoroutine(LoadClip(ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].sound.path));            
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Update()
    {
        if (audioSource.isPlaying)
        {
            EndClip();
        }
    }

    /// <summary>
    ///     Function to control the audio if button video is pressed
    /// </summary>
    public void PauseAudioVideo()
    {
        if (audioSource.isPlaying)
        {
            PauseAudio();
        }
    }

    /// <summary>
    ///     Function to control the audio
    /// </summary>
    public void ControlAudio()
    {
        if (!audioFlag)
        {
            audioSource.Play();
            buttonAudio.GetComponent<RawImage>().texture = Resources.Load<Texture>("Icons/soundFalse");
            audioFlag = true;
        }
        else
        {
            if (audioSource.isPlaying)
            {
                PauseAudio();
            }
            else
            {
                audioSource.UnPause();
                buttonAudio.GetComponent<RawImage>().texture = Resources.Load<Texture>("Icons/soundFalse");
            }
        }
    }

    /// <summary>
    ///     Function to pause audio
    /// </summary>
    private void PauseAudio()
    {
        audioSource.Pause();
        buttonAudio.GetComponent<RawImage>().texture = Resources.Load<Texture>("Icons/soundTrue");
    }

    /// <summary>
    ///     Function to stop audio
    /// </summary>    
    private void StopAudio()
    {
        audioSource.Stop();
        audioFlag = false;
        buttonAudio.GetComponent<RawImage>().texture = Resources.Load<Texture>("Icons/musicPlayer");
    }

    /// <summary>
    ///     Load sound form file
    /// </summary>
    /// <param name="soundPath"></param>
    /// <returns></returns>
    private IEnumerator LoadClip(string soundPath)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + Application.persistentDataPath + '/' + soundPath, AudioType.WAV))
        {
            yield return www.SendWebRequest();            
            if (www.isNetworkError)
            {

            }
            else
            {
                audioSource.clip = DownloadHandlerAudioClip.GetContent(www);
                buttonAudio.GetComponent<RawImage>().enabled = true;
                buttonAudio.GetComponent<RawImage>().texture = Resources.Load<Texture>("Icons/soundFalse");
                audioFlag = true;
                audioSource.Play();
            }
        }
    }

    /// <summary>
    ///     Function to verify end of a clip
    /// </summary>
    private void EndClip()
    {
        if (audioSource.clip != null)
        {
            if (audioSource.clip.length <= audioSource.time)
            {
                StopAudio();
            }
        }
    }
}
