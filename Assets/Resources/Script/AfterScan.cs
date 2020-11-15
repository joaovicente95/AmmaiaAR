using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AfterScan : MonoBehaviour
{
    #region Variables
        
    public GameObject nextSceneButton;
    public GameObject downloadscreen;
    public GameObject textInfo;
    public GameObject buttonRestart;
    public Text errorText;   
    public TextMeshProUGUI nameEpigraphy;
    private GameObject loadScenes;

    
    private bool errorDownloadFlag = false;
    private bool finalFileFlag = false;
    //private bool flagTextToSpeech = false;

    #endregion //Variables

    // Start is called before the first frame update
    void Start()
    {
        loadScenes = GameObject.Find("SceneControl");
    }

    // Update is called once per frame
    void Update()
    {
        if (finalFileFlag.Equals(true) && errorDownloadFlag.Equals(false)/* && flagTextToSpeech.Equals(true)*/)
        {
            textInfo.SetActive(true);
            nextSceneButton.SetActive(true);
            buttonRestart.SetActive(true);
            finalFileFlag = false;
            //flagTextToSpeech = false;
        }

        // 
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                loadScenes.GetComponent<LoadingScenes>().LoadScene(0);
            }

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void ResetScan()
    {
        textInfo.SetActive(false);
        nextSceneButton.SetActive(false);
        buttonRestart.SetActive(false);
        nameEpigraphy.gameObject.SetActive(false);
        finalFileFlag = false;
        //flagTextToSpeech = false;        
        nameEpigraphy.text = "";

    }

    /// <summary>
    ///     Function to start downloading files a load a new scene
    /// </summary>
    public void Begin()
    {
        if (GetIndex())
        {
            nameEpigraphy.gameObject.SetActive(true);
            nameEpigraphy.text = ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].nameDisplay;
            GetFilesObject();
        }
        else
        {
            errorText.text = "Not find";
        }
    }

    /// <summary>
    ///     Function to get files from object identified
    /// </summary>
    private void GetFilesObject()
    {
        int index = ObjectTrack.IndexOfObject;

        downloadscreen.SetActive(true);
        
        /*
        // Download audio
        if (ObjectTrack.objectData.epigrafias[index].sound.path != null)
        {
            if (!File.Exists(Application.persistentDataPath + '/' + ObjectTrack.objectData.epigrafias[index].sound.path))
            {
                StartCoroutine(TextToSpeechDown(ObjectTrack.objectData.epigrafias[index].sound.path));
            }
            else
            {
                flagTextToSpeech = true;
            }
        }*/

        // Download page html
        if (ObjectTrack.objectData.epigrafias[index].pathPage.Length == 0 || ObjectTrack.objectData.epigrafias[index].pathPage != null)
        {
            if (!File.Exists(Application.persistentDataPath + '/' + ObjectTrack.objectData.epigrafias[index].pathPage))
            {
                StartCoroutine(DownSaveFiles(ObjectTrack.objectData.epigrafias[index].pathPage, false));
            }
        }

        // Download Images
        if (ObjectTrack.objectData.epigrafias[index].images.imageList.Count != 0 || ObjectTrack.objectData.epigrafias[index].images.imageList != null)
        {
            //Create folder with the name of the object identified
            if (Directory.Exists(Application.persistentDataPath + "/Images/" + ObjectTrack.objectData.epigrafias[index].nameVuforia)) { }
            else
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/Images/" + ObjectTrack.objectData.epigrafias[index].nameVuforia);
            }

            foreach (ImageList image in ObjectTrack.objectData.epigrafias[index].images.imageList)
            {
                if (!File.Exists(Application.persistentDataPath + '/' + image.path))
                {
                    StartCoroutine(DownSaveFiles(image.path, false));
                }
            }
        }

        // Download video
        
        if (ObjectTrack.objectData.epigrafias[index].video.path != null)
        {
            if (ObjectTrack.objectData.epigrafias[index].video.path.Length == 0 )
            {
                finalFileFlag = true;
                downloadscreen.SetActive(false);
            }
            else if (!File.Exists(Application.persistentDataPath + '/' + ObjectTrack.objectData.epigrafias[index].video.path))
            {
                StartCoroutine(DownSaveFiles(ObjectTrack.objectData.epigrafias[index].video.path, true));
            }
            else
            {
                finalFileFlag = true;
                downloadscreen.SetActive(false);
            }
        }
        else
        {
            finalFileFlag = true;
            downloadscreen.SetActive(false);
        }
    }

    /// <summary>
    ///     Coroutine to download and save a file on device
    /// </summary>
    /// <param name="path">Path to file</param>
    /// <param name="finalFile"></param>
    private IEnumerator DownSaveFiles(string path, bool finalFile)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(ObjectTrack.Objecturl + path))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                errorDownloadFlag = true;
                errorText.enabled = true;
                errorText.text = "Download error";
            }
            else
            {
                File.WriteAllBytes(Application.persistentDataPath + '/' + path, webRequest.downloadHandler.data);
                if (finalFile)
                {
                    finalFileFlag = true;
                    downloadscreen.SetActive(false);
                }
            }
        }
    }

    /*
    /// <summary>
    ///     Download a file from a voicerrs api
    /// </summary>
    /// <param name="path">Path to file</param>
    /// <returns></returns>
    private IEnumerator TextToSpeechDown(string path)
    {
        string url = "api.voicerss.org/?key=" + ObjectTrack.KeyTextToSpeech + "&hl=pt-pt&f=16khz_16bit_stere&src=" + ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].sound.textToSpeech;
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError)
            {
                errorDownloadFlag = true;
                errorText.enabled = true;
                errorText.text = "Download error";
            }
            else
            {
                File.WriteAllBytes(Application.persistentDataPath + '/' + path, www.downloadHandler.data);
                flagTextToSpeech = true;
            }
        }
    }*/

    /// <summary>
    ///     Coroutine to take message off the screen
    /// </summary> 
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        errorText.enabled = false;
    }

    /// <summary>
    ///     Save an index from a list of the traceable element
    /// </summary>
    /// <returns>
    ///     false -> If traceable element dont exist on list
    ///     true -> If traceable element exist on list
    /// </returns>
    private bool GetIndex()
    {
        bool temp = false;
        for (int i = 0; i < ObjectTrack.objectData.epigrafias.Count; i++)
        {
            if (ObjectTrack.objectData.epigrafias[i].nameVuforia.Equals(ObjectTrack.ObjectName))
            {
                ObjectTrack.IndexOfObject = i;
                temp = true;
                break;
            }
        }
        return temp;
    }    
}
