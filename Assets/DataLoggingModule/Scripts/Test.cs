using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Test : MonoBehaviour
{
    private float _gameTimer = 0;
    
    /// <summary>
    /// This is just to serve as an example how the CsvWrite function can be called,
    /// Just invoke the function in some thread of your game whenever you want to log the data and delete or comment this script
    /// </summary>
    private void Update ()
    {
        //replace the content of the list by the variables you want to log (converted to string)
        var dataTest = new List<string>
        {
            _gameTimer.ToString(CultureInfo.InvariantCulture),
            "Variable1Value",
            "Variable2Value"
        };

        //Invoke the CsvWrite function with the updated dataTest list making sure that the file is still open
        if(!GameDataLogging.StopLog)
		    GameDataLogging.CsvWrite(dataTest);

        _gameTimer += Time.deltaTime;

    }
}
