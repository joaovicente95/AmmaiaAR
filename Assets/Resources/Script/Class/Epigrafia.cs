using System;
using System.Collections.Generic;


public class Epigrafias
{
    public List<Epigrafia> epigrafias = new List<Epigrafia>();
}

[Serializable]
public class Epigrafia
{
    public string nameVuforia;
    public string nameDisplay;
    public string pathPage;
    public Images images;
    public Video video;
    public Sound sound;
    public TextToRead textToRead;
}

[Serializable]
public class Images
{
    public string positionButton;
    public List<ImageList> imageList = new List<ImageList>();
}

[Serializable]
public class ImageList
{
    public string title;
    public string description;
    public string path;
}

[Serializable]
public class Video
{
    public string positionButton;
    public string path;
}


[Serializable]
public class Sound
{
    public string positionButton;
    public string path;
}

[Serializable]
public class TextToRead
{
    public string positionButton;
    public string textToSpeech;
}
