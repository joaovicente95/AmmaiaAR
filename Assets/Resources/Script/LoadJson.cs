using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class LoadJson : MonoBehaviour
{

    #region Variables

    private const string nameFile = "JsonExample.json";

    [SerializeField]
    public Text messageError, message;
    [SerializeField]
    public GameObject ModalDialog;
    [SerializeField]
    public Button buttonNextScene, buttonFicar, buttonSair;

    #endregion // Variables

    /// <summary>
    ///     Use this for initialization
    /// </summary>    
    void Start()
    {
        StartCoroutine("LoadJsonFile");
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape) && ModalDialog.activeSelf.Equals(false))
            {
                // Remove all function o
                buttonFicar.onClick.RemoveAllListeners();
                // Activate modal and buttons
                ModalDialog.SetActive(true);
                buttonSair.gameObject.SetActive(true);
                buttonFicar.gameObject.SetActive(true);

                messageError.text = "Are you sure you want to exit? ";
                buttonFicar.GetComponentInChildren<Text>().text = "No";               

                buttonFicar.onClick.AddListener(Ficar);
                buttonSair.onClick.AddListener(delegate { Application.Quit(); });
            }

        }
    }

    /// <summary>
    ///     Public function to call a coroutine to load json
    /// </summary>
    public void Load()
    {
        message.GetComponent<Button>().interactable = false;
        StartCoroutine("LoadJsonFile");
    }
    


    /// <summary>
    ///     Coroutine to get file json from the server to load on memory
    /// </summary>    
    private IEnumerator LoadJsonFile()
    {
        message.text = "A ligar com o servidor, por favor aguarde";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(ObjectTrack.Objecturl + nameFile))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                if (ModalDialog.activeSelf.Equals(false))
                {
                    ModalDialog.SetActive(true);
                    messageError.text = "Oops! Não foi possível estabelecer ligação com o servidor";
                    message.GetComponent<Button>().interactable = true;
                    buttonFicar.gameObject.SetActive(true);
                    buttonFicar.GetComponentInChildren<Text>().text = "Ok";
                    buttonFicar.onClick.AddListener(delegate { ModalDialog.SetActive(false); });
                }
                
                message.text = "Por favor, click nesta mensagem para tentar novamente";
                message.GetComponent<Button>().interactable = true;
            }
            else
            {                
                buttonNextScene.interactable = true;
                message.color = new Color32(66, 159, 35, 255);
                message.text = "Conecção com o servidor estabelecida";
                ObjectTrack.objectData = JsonUtility.FromJson<Epigrafias>(webRequest.downloadHandler.text);
                StartCoroutine("Timer");
            }
        }

    }

    /// <summary>
    /// 
    /// </summary>
    private void Ficar()
    {
        ModalDialog.SetActive(false);
        messageError.text = "";
        buttonFicar.gameObject.SetActive(false);
        buttonSair.gameObject.SetActive(false);
        buttonFicar.onClick.RemoveListener(Ficar);
        buttonSair.onClick.RemoveAllListeners();
    }

    /// <summary>
    ///     Coroutine to take message off the screen
    /// </summary> 
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        message.enabled = false;
    }
}