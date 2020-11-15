using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    [SerializeField]
    public GameObject swipeController, ImagePanel, webView;
    private GameObject imageButton;


    // Start is called before the first frame update
    void Start()
    {
        if (ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].images.imageList != null)
        {
            imageButton = GameObject.Find("ButtonImages");
            imageButton.AddComponent<Button>().onClick.AddListener(ActiveImage);
            imageButton.GetComponent<RawImage>().texture = Resources.Load<Texture>("Icons/imageButton");
            imageButton.GetComponent<RawImage>().enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (ImagePanel.activeSelf && Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                ExitImageShow();                
            }

        }
    }

    public void ExitImageShow()
    {
        swipeController.SetActive(false);
        ImagePanel.SetActive(false);
        // Show Webview 
        webView.GetComponent<WebView>().ShowWeb();
        
    }

    private void ActiveImage()
    {
        // Hide Webview 
        webView.GetComponent<WebView>().HideWeb();

        swipeController.SetActive(true);
        ImagePanel.SetActive(true);
    }
}
