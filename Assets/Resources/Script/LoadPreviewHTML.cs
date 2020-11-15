using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadPreviewHTML : MonoBehaviour
{

    //public TextMeshProUGUI description;
    public Text title;

    // Start is called before the first frame update
    void Start()
    {
        //description.text = ObjectTrack.objectData.stones[ObjectTrack.IndexOfObject].descriptionHTML;
        title.text = ObjectTrack.objectData.epigrafias[ObjectTrack.IndexOfObject].nameDisplay;
    }
}
