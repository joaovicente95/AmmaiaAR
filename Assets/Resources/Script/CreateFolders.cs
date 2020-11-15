using System;
using System.IO;
using UnityEngine;

public class CreateFolders : MonoBehaviour
{
    #region VARIABLES_CONST_SERVER_FOLDERS
    
    private string pathSounds = "Sounds/";
    private string pathImage = "Images/";
    private string pathPages = "Pages/";
    private string pathVideo = "Video/";

    #endregion // VARIABLES_CONST_SERVER_FOLDERS

    // Start is called before the first frame update
    void Start()
    {
        CreatePath(pathSounds);
        CreatePath(pathImage);
        CreatePath(pathPages);
        CreatePath(pathVideo);
    }

    /// <summary>
    ///     Function to create folder on device
    /// </summary>
    /// <param name="directoryName">Name of directory</param>
    private void CreatePath(string directoryName)
    {
        if (Directory.Exists(Application.persistentDataPath + "/" +directoryName)) {           
            if (Directory.GetLastWriteTimeUtc(Application.persistentDataPath + "/" + directoryName) < DateTime.Now.AddDays(-1)) {
                Directory.Delete(Application.persistentDataPath + "/" + directoryName, true);
                Directory.CreateDirectory(Application.persistentDataPath + "/" + directoryName);
            }
        }
        else
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + directoryName);
        }
    }
}
