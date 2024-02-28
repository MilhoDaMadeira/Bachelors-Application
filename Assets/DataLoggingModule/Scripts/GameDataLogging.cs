using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameDataLogging : MonoBehaviour {

    private List<string> _header = new List<string>();
    private static string _path, _filepath;
    private static TextWriter _file;

    public static bool StopLog;

    private void Start ()
    {
        DontDestroyOnLoad(this);

        //Initialize the _header list with the names of the variables that you want to save
        //Your _data list must have the same size of this list and the order of the variables must match
        _header = new List<string>
        {
            "GameTimer", "Variable1", "Variable2"
        }
        ;

        LogInit();
    }
	
	private void Update ()
    {
        //You always need to close the file before quitting Unity otherwise you0ll lose your data
        if (StopLog)
            _file.Close();
    }

    private void LogInit()
    {
        //Replace YourGameName by the real game name and instead YourUserId use a variable that stores the user id
        _path = Application.dataPath + "/Estagio/" + "John" + "/";

        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }

        _filepath = _path + "John" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
        _file = new StreamWriter(_filepath, true);

        //builds the string that will be the _header of the csv _file
        var fillHeader = _header[0];

        for (var i = 1; i < _header.Count; i++)
        {
            fillHeader = fillHeader + "," + _header[i];
        }

        //writes the first line of the _file (_header)
        _file.WriteLine(fillHeader);
    }


    /// <summary>
    /// Writes a line in the csv file. This function is static so you can invoke it from any thread you want
    /// </summary>
    /// <param name="data"></param>
    public static void CsvWrite(List<string> data)
    {
        var newLine = data[0];

        for (var i = 1; i < data.Count; i++)
        {
            newLine = newLine + "," + data[i];
        }

        _file.Write(newLine);
        _file.WriteLine("");
    }

    
    /// <summary>
    /// Make sure somewhere in your game you set StopLog to true, you can also add this to the function OnApplicationQuit()
    /// </summary>
    public void StopLogging()
    {
        StopLog = true;
    }
}
