using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveController
{
    public static int curLoadLevel;

    public static bool IsThisTheFirstLaunch()
    {
        string fileName = Application.persistentDataPath + "/GameParametrs.game";
        try
        { 
            FileStream fs = new FileStream(fileName, FileMode.Open);
            fs.Close();
        }
        catch
        {
            return false;
        }

        return true;
    }



    public static GameParametrs LoadGameParametrs()
    {
        string fileName = Application.persistentDataPath + "/GameParametrs.game";

        BinaryFormatter br = new BinaryFormatter();
        FileStream fs = new FileStream(fileName, FileMode.Open);

        GameParametrs gameParametrs = (GameParametrs)br.Deserialize(fs);
        fs.Close();

        return gameParametrs;
        try
        {
            
        }
        catch
        {
            return null;
        }
    }

    public static void SaveGameParametrs(GameParametrs gameParametrs)
    {
        string fileName = Application.persistentDataPath + "/GameParametrs.game";

        BinaryFormatter br = new BinaryFormatter();
        FileStream fs = new FileStream(fileName, FileMode.Create);

        br.Serialize(fs, gameParametrs);
        fs.Close();
    }

    public static LevelParametrs LoadLevelParametrs(int levelIndex)
    {
        string fileName = Application.persistentDataPath + "/LevelParametrs" + levelIndex.ToString() + ".game";
        try
        {
            BinaryFormatter br = new BinaryFormatter();
            FileStream fs = new FileStream(fileName, FileMode.Open);
            
            LevelParametrs levelParametrs = (LevelParametrs)br.Deserialize(fs);
            fs.Close();

            return levelParametrs;
        }
        catch
        {
            return null;
        }  
    }


    public static void SaveLevelParametrs(int levelIndex, LevelParametrs levelParametrs)
    {
        string fileName = Application.persistentDataPath + "/LevelParametrs" + levelIndex.ToString() + ".game";

        BinaryFormatter br = new BinaryFormatter();
        FileStream fs = new FileStream(fileName, FileMode.Create);

        br.Serialize(fs, levelParametrs);
        fs.Close();
    }
}


[System.Serializable]
public class GameParametrs
{
    public int levelCount;
}


[System.Serializable]
public class LevelParametrs
{
    public bool open;
    public bool completed;

    public int asteroidsCount;
    public float asteroidSpeed;
    public float asteroidSpawnDelay;
    public int pointsForAsteroid;
}