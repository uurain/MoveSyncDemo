using UnityEngine;
using System.Collections;

public class FileHelp  {

	public static string[]  GetFileLines(string filePath)
    {
        string[] lines = System.IO.File.ReadAllLines(filePath);
        return lines;
    }


}
