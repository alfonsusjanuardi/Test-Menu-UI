using System;
using System.IO;
using UnityEngine;

public class ReadWriteFile 
{
    /**
    * <summary>
    * Read a Text (.txt / .json) File
    * </summary>
    * <param name="filePath">
    * The Path of the .txt File
    * </param>
    * <returns>
    * string
    * </returns>
    */
    public static string ReadTextFile(string filePath)
    {
        string line = string.Empty;
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using StreamReader sr = new(filePath);
            // Read and display lines from the file until the end of
            // the file is reached.
            line = sr.ReadToEnd();

            sr.Close();
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Debug.Log("The file could not be read:" + "\n" + e.Message);
            line = string.Empty;
        }

        return line;
    }

    /**
    * <summary>
    * Read a Text (.txt / .json) File On The Persistent Data Path
    * </summary>
    * <param name="fileType">
    * The Type Of File
    * </param>
    * <returns>
    * string
    * </returns>
    */
    public static string ReadTextFilePersistent(string fileName)
    {
        string line = string.Empty;
        try
        {
            fileName = !string.IsNullOrEmpty(Path.GetExtension(fileName)) ? fileName : fileName + ".txt";

            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using StreamReader sr = new(Application.persistentDataPath + "/" + fileName);
            // Read and display lines from the file until the end of
            // the file is reached.
            line = sr.ReadToEnd();

            sr.Close();
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Debug.Log("The file could not be read:" + "\n" + e.Message);
            line = string.Empty;
        }

        return line;
    }

    /**
    * <summary>
    * Write a Text (.txt / .json) File
    * </summary>
    * <param name="filePath">
    * The Path of the .txt File
    * </param>
    * <param name="content">
    * The Content The User Want To Insert
    * </param>
    */
    public static void WriteTextFile(string filePath, string content)
    {
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using StreamWriter sr = new(filePath);
            // Read and display lines from the file until the end of
            // the file is reached.
            sr.Write(content);

            sr.Close();
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Debug.Log("The file could not be found:" + "\n" + e.Message);
        }
    }

    /**
    * <summary>
    * Write a Text (.txt / .json) File On The Persistent Data Path
    * </summary>
    * <param name="content">
    * The Content The User Want To Insert
    * </param>
    * <param name="fileType">
    * The Type Of File
    * </param>
    */
    public static void WriteTextFilePersistent(string content, string fileName)
    {
        try
        {
            fileName = !string.IsNullOrEmpty(Path.GetExtension(fileName)) ? fileName : fileName + ".txt";
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using StreamWriter sr = new(Application.persistentDataPath + "/" + fileName);
            // Read and display lines from the file until the end of
            // the file is reached.
            sr.Write(content);

            sr.Close();
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Debug.Log("The file could not be found:" + "\n" + e.Message);
        }
    }
}
