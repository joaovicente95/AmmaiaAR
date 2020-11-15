using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WebView : MonoBehaviour
{
    public RectTransform bottomPanel;
    public RectTransform overPanel;
    private UniWebView m_UniWebView;
    private string url;

    public void Start()
    {       
        url = "file:///" + Application.persistentDataPath + '/' + ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].pathPage;
        StartCoroutine(TimerWebView());        
    }

    private void Awake()
    {
        //m_ButtonEnter.onClick.AddListener(LoadUrl);
        //m_ButtonClose.onClick.AddListener(CloseUrl);
        //m_ButtonUnityCallWebView.onClick.AddListener(UnityCallWebView);
    }

    /// <summary>
    ///     
    /// </summary> 
    private IEnumerator TimerWebView()
    {
        yield return new WaitForSeconds(0.05F);
        LoadUrl();
    }

    public void LoadUrl()
    {
        if (m_UniWebView != null)
        {
            m_UniWebView.CleanCache();
        }

        Canvas parentCanvasOver = overPanel.GetComponentInParent<Canvas>();
        Canvas parentCanvasBottom = bottomPanel.GetComponentInParent<Canvas>();

        int overPixel = (int)(overPanel.rect.height * parentCanvasOver.scaleFactor + 0.5f);        
        int bottomPixel = (int)(bottomPanel.rect.height * parentCanvasBottom.scaleFactor + 0.5f);
        
        m_UniWebView = UniWebViewHelper.CreateUniWebView(gameObject, url, overPixel, 0, bottomPixel, 0);
        m_UniWebView.OnLoadComplete += OnLoadComplete;
        m_UniWebView.Load();
    }
    private void OnLoadComplete(UniWebView webView, bool success, string errorMessage)
    {
        if (success)
        {
            webView.Show();
        }
    }

    public void CloseUrl()
    {
        m_UniWebView.Hide();       
        m_UniWebView.OnLoadComplete -= OnLoadComplete;
        Destroy(m_UniWebView);
    }

    public void HideWeb()
    {
        m_UniWebView.Hide();
    }

    public void ShowWeb()
    {
        m_UniWebView.Show();
    }
}
