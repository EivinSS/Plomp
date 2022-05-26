using System.Collections.Generic;
public class NameConfig
{
    public static string mainMenu = "MainMenu";
    public static string levels = "Levels";
    public static string currentMaxLevel = "currentMaxLevel";

    public static string adManager = "AdManager";
    public static Dictionary<string, int> levelDictionary = new Dictionary<string, int>{
        { "Level1", 1 },
        { "Level2", 2 },
        { "Level3", 3 },
        { "Level4", 4 },
        { "Level5", 5 },
        { "Level6", 6 },
        { "Level7", 7 }
        };

}
