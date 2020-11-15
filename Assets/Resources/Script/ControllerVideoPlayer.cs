using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ControllerVideoPlayer : MonoBehaviour
{

    #region VARIABLES

    // specify these in Unity Inspector    
    
    public Button buttonPlayPause, forwardButton, rewindButton;    
    public VideoPlayer videoPlayer;    
    public GameObject videoScreen, mainScreen, progressBar, /*audioControl,*/ webView, textTSpeech;
    private GameObject videoButton;

    //
    private IEnumerator coroutineAnimation;

    #endregion //VARIABLES


    /// <summary>
    ///     Use this for initialization
    /// </summary>
    void Start()
    {

        if (!System.IO.File.Exists(Application.persistentDataPath + "/" + ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].video.path) || ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].video.path.Length == 0)
        {
            
        }
        else
        {
            // Find button on scenne
            videoButton = GameObject.Find("ButtonVideo");

            // Change button icon and set function
            videoButton.GetComponent<RawImage>().texture = Resources.Load<Texture>("Icons/videoPlayer");
            videoButton.AddComponent<Button>().onClick.AddListener(ActiveScreen);
            videoButton.GetComponent<RawImage>().enabled = true;            
            videoScreen.AddComponent<Button>().onClick.AddListener(StartAnimation);

            // Add functions to buttons
            buttonPlayPause.onClick.AddListener(PlayPause);
            forwardButton.onClick.AddListener(delegate { MovForware(10); });
            rewindButton.onClick.AddListener(delegate { MovRewind(10); });

            // Check if video ended
            videoPlayer.loopPointReached += EndReached;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (videoPlayer.frameCount > 0)
        {
            progressBar.GetComponent<Image>().fillAmount = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
        }

        //Back button android
        if (videoScreen.activeSelf && Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                StopVideo();
            }

        }
    }

    /// <summary>
    ///      Function to reset video Player
    /// </summary>
    public void StopVideo()
    {
        buttonPlayPause.GetComponent<RawImage>().texture = Resources.Load<Texture>("Icons/playButton");
        videoPlayer.Stop();
        videoScreen.SetActive(false);
        mainScreen.SetActive(true);
        
        // Hide Webview 
        webView.GetComponent<WebView>().ShowWeb();
    }

    /// <summary>
    ///     Function to load a video scene, and attribute functions to buttons
    /// </summary>
    private void ActiveScreen()
    {
        /*
        // Pause audio 
        audioControl.GetComponent<AudioPlayer>().PauseAudioVideo();        
        */

        // Stop text to speech
        textTSpeech.GetComponent<TextToSpeech>().StopTTS();

        // Hide Webview 
        webView.GetComponent<WebView>().HideWeb();

        // Load video to videoplayer
        SetVideo(ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].video.path);

        //Star animation
        coroutineAnimation = ExitButtonAnimation();
        StartCoroutine(coroutineAnimation);

        //StartCoroutine(ExitButtonAnimation());             

        // Active video panel
        videoScreen.SetActive(true);
        mainScreen.SetActive(false);

        // Start play video
        videoPlayer.Play();
        // Change icon to pause
        buttonPlayPause.GetComponent<RawImage>().texture = Resources.Load<Texture>("Icons/pauseButton");
    }

    /// <summary>
    ///     Function to Forware a video X seconds
    /// </summary>
    /// /// <param name="time"> time in seconds </param>
    private void MovForware(int time)
    {
        StopCoroutine(coroutineAnimation);
        if (videoPlayer.isActiveAndEnabled)
        {
            videoPlayer.time = videoPlayer.time + 10;
            if (videoPlayer.isPlaying)
            {
                coroutineAnimation = ExitButtonAnimation();
                StartCoroutine(coroutineAnimation);
            }
        }
    }

    /// <summary>
    ///     Function to rewind a video X seconds
    /// </summary>
    /// <param name="time"> time in seconds </param>
    private void MovRewind(int time)
    {
        StopCoroutine(coroutineAnimation);
        if (videoPlayer.isActiveAndEnabled)
        {
            videoPlayer.time = videoPlayer.time - 10;
            if (videoPlayer.isPlaying)
            {
                coroutineAnimation = ExitButtonAnimation();
                StartCoroutine(coroutineAnimation);
            }
        }
    }

    /// <summary>
    ///     Function to receive a path to a video
    /// </summary>
    /// <param name="path"></param>
    private void SetVideo(string path)
    {        
        videoPlayer.url = Application.persistentDataPath + "/" + path;
    }

    /// <summary>
    ///     Function for play and pause video
    /// </summary>
    private void PlayPause()
    {
        StopCoroutine(coroutineAnimation);
        if (videoPlayer.isPlaying)
        {
            buttonPlayPause.GetComponent<RawImage>().texture = Resources.Load<Texture>("Icons/playButton");
            videoPlayer.Pause();
        }
        else
        {
            buttonPlayPause.GetComponent<RawImage>().texture = Resources.Load<Texture>("Icons/pauseButton");
            videoPlayer.Play();
            coroutineAnimation = ExitButtonAnimation();
            StartCoroutine(coroutineAnimation);
        }
    }

    /// <summary>
    ///     Function to reset video Player
    /// </summary>
    /// <param name="videoPlayer"></param>
    private void EndReached(VideoPlayer videoPlayer)
    {
        buttonPlayPause.GetComponent<RawImage>().texture = Resources.Load<Texture>("Icons/playButton");
        videoPlayer.Stop();
    }    

    /// <summary>
    ///     Function to start a animation to show buttons 
    /// </summary>
    private void StartAnimation()
    {
        videoScreen.GetComponent<Animator>().SetTrigger("Active");

        coroutineAnimation = ExitButtonAnimation();
        StartCoroutine(coroutineAnimation);
    }

    /// <summary>
    ///     Coroutine to hide buttons after 3 seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator ExitButtonAnimation()
    {
        yield return new WaitForSeconds(2);
        videoScreen.GetComponent<Animator>().SetTrigger("Exit");
    }
}