using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//Author:Giulio Ammannato
public class HandleTextFile : MonoBehaviour
{

    public static HandleTextFile handleTextFile;

    private void Awake()
    {
        if (handleTextFile == null)
        {
            handleTextFile = GameObject.FindGameObjectWithTag("handleTextFile").GetComponent<HandleTextFile>();
        }

        //cupTaskStats.Init();
        //playerStats.Init();

        DontDestroyOnLoad(this);

        // Debug.Log(playerStats.currentMoney);
    }

    //[MenuItem("Tools/Write file")]
    public void WriteString(string text)
    {
        string path;

        if (Application.platform == RuntimePlatform.WebGLPlayer)
            path = Application.persistentDataPath + @"/test.txt";
        else
            path = "Assets/Output/test.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(text);
        writer.Close();

        /**
        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
        TextAsset asset = Resources.Load("/Output/test") as TextAsset;

        //Print the text from the file
        Debug.Log(asset);
**/
    }

    public void WriteStringOnSameLine(string text)
    {
        string path;

        if (Application.platform == RuntimePlatform.WebGLPlayer)
            path = Application.persistentDataPath + @"/unaLineaPerSoggetto.txt";
        else
            path = "Assets/Output/unaLineaPerSoggetto.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.Write(text + ", ");
        writer.Close();

        /**
        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
        TextAsset asset = Resources.Load("/Output/test") as TextAsset;

        //Print the text from the file
        Debug.Log(asset);
**/
    }

    public void WriteEndOfLine()
    {
        string path;

        if (Application.platform == RuntimePlatform.WebGLPlayer)
            path = Application.persistentDataPath + @"/unaLineaPerSoggetto.txt";
        else
            path = "Assets/Output/unaLineaPerSoggetto.txt";

        StreamWriter writer = new StreamWriter(path, true);
        //the last thing to write is the time when the task ended
        writer.WriteLine(Statistics.stats.playerStats.taskEndtime);
        writer.Close();
    }

    //[MenuItem("Tools/Read file")]
    public void ReadString(bool gain)
    {
        string path;
        //if we are in the gain condition read from the gain trials
        if (gain)
            path = "Assets/Output/gainTrials.txt";
        //else we are in the loss, read from the loss trials
        else
            path = "Assets/Output/lossTrials.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }



}
