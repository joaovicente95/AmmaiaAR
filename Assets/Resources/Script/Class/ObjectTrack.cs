/// <summary>
///        y 
/// </summary>
public static class ObjectTrack
{
    private static string url = "ftp://192.168.137.1/";
    
    //private static string keyTextToSpeech = "";

    private static string objectName;
    private static int indexOfObject;
    public static Epigrafias objectData;    


    public static string ObjectName
    {
        get { return objectName; }
        set { objectName = value; }
    }

    public static int IndexOfObject
    {
        get { return indexOfObject; }
        set { indexOfObject = value; }
    }

    public static string Objecturl
    {
        get { return url; }
    }

    /*
    public static string KeyTextToSpeech
    {
        get { return keyTextToSpeech; }
    }
    */

}
