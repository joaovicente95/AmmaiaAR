using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttonlayout : MonoBehaviour
{
    #region Variables

    public GameObject buttonLeft, buttonCenter, buttonRight;
    private bool[] positionIcons;

    #endregion //Variables


    // Start is called before the first frame update
    void Start()
    {
        positionIcons = new bool[3];
        Begin();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    ///     
    /// </summary>
    private void Begin()
    {
        /*
        if (ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].sound.positionButton != null)
        {
            UpdateLayout(ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].sound.positionButton, "ButtonAudio");
        }*/

        if (ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].video.positionButton != null)
        {
            if (ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].video.positionButton.Length == 0) { }
            else
                UpdateLayout(ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].textToRead.positionButton, "ButtonTTS");
        }        

        if (ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].video.positionButton != null)
        {
            if(ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].video.positionButton.Length == 0) { }
            else
                UpdateLayout(ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].video.positionButton, "ButtonVideo");
        }

        if (ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].images.positionButton != null)
        {
            if (ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].images.positionButton.Length == 0) { }
            else
                UpdateLayout(ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].images.positionButton, "ButtonImages");
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <param name="nameButton"></param>
    private void UpdateLayout(string position, string nameButton)
    {
        //  downleft  downcenter  downright
        // ObjectTrack.objectData.stones[index].sound.positionButton
        // ObjectTrack.objectData.stones[index].video.positionButton
        // ObjectTrack.objectData.stones[index].images.positionButton

        if (position.Equals("downleft") && positionIcons[0].Equals(false))
        {
            positionIcons[0] = true;
            buttonLeft.name = nameButton;
        }
        else if (position.Equals("downcenter") && positionIcons[1].Equals(false))
        {
            positionIcons[1] = true;
            buttonCenter.name = nameButton;
        }
        else if (position.Equals("downright") && positionIcons[2].Equals(false))
        {
            positionIcons[2] = true;
            buttonRight.name = nameButton;
        }
        else
        {

        }

    }
}