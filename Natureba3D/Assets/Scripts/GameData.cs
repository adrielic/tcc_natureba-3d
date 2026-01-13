using UnityEngine;

public static class GameData
{
    const string LEVEL_KEY = "currentLevelIndex";

    public static void Save(int newIndex)
    {
        PlayerPrefs.SetInt(LEVEL_KEY, newIndex);
        PlayerPrefs.Save();
        Debug.Log($"Player progress saved (Current Level: {newIndex}).");
    }

    public static int Load()
    {
        int levelIndex = PlayerPrefs.GetInt(LEVEL_KEY, 1);
        Debug.Log($"Player progress loaded (Current Level: {levelIndex}).");
        return levelIndex;
    }
}
