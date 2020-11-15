using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageShow : MonoBehaviour
{
    public RawImage img;
    public Text description;
    public Text title;
    public GameObject panelDescrip, infoButton;
    private Texture2D imageBits;
    private int maxImages, currentImage;

    // Start is called before the first frame update
    void Start()
    {

        if (ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].images.imageList != null)
        {
            currentImage = 0;
            maxImages = ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].images.imageList.Count;
            imageBits = LoadImage(ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].images.imageList[currentImage].path);
            title.text = ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].images.imageList[currentImage].title;
            description.text = ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].images.imageList[currentImage].description;
            img.texture = imageBits;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    public void ShowDesc()
    {
        panelDescrip.gameObject.SetActive(true);
        infoButton.gameObject.SetActive(false);
    }

    /// <summary>
    ///     
    /// </summary>
    public void HideDesc()
    {
        panelDescrip.gameObject.SetActive(false);
        infoButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// 
    /// </summary>
    public void NextImage()
    {        
        currentImage++;
        if (currentImage == maxImages)
        {
            currentImage = 0;
        }

        title.text = ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].images.imageList[currentImage].title;
        description.text = ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].images.imageList[currentImage].description;
        imageBits = LoadImage(ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].images.imageList[currentImage].path);
        img.texture = imageBits;
        HideDesc();
    }

    public void PreviousImage()
    {        
        currentImage--;
        if (currentImage < 0)
        {
            currentImage = maxImages - 1;            
        }
        
        title.text = ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].images.imageList[currentImage].title;
        description.text = ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].images.imageList[currentImage].description;
        imageBits = LoadImage(ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].images.imageList[currentImage].path);
        img.texture = imageBits;
        HideDesc();

    }

    private Texture2D LoadImage(string path)
    {        
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(Application.persistentDataPath + '/' + path))
        {
            fileData = File.ReadAllBytes(Application.persistentDataPath + '/' + path);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }

        return tex;
    }
}
