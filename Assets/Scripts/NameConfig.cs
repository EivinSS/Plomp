using System.Collections.Generic;
public class NameConfig
{
    public static string mainMenu = "MainMenu";
    public static string levels = "LevelsScene";
    public static string currentMaxLevel = "currentMaxLevel";
    public static string music = "music";
    public static string sound = "sound";

    public static string adManager = "AdManager";
    public static Dictionary<string, int> levelDictionary = new Dictionary<string, int>{
        { "Level1", 1 },
        { "Level2", 2 },
        { "Level3", 3 },
        { "Level4", 4 },
        { "Level5", 5 },
        { "Level6", 6 },
        { "Level7", 7 },
        { "Level8", 8 },
        { "Level9", 9 },
        { "Level10", 10 }
        };

}
