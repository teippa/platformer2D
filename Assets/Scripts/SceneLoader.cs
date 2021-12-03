using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scene
    {
        GameScene,
        SettingsScene,
        CreditsScene,
        MainMenuScene,
        GameOverScene,
        LoadingScene,
        QuitGame,
        none,
    }

    public static void Load(Scene scene)
    {
        if (scene == Scene.QuitGame)
            QuitGame();
        else if (scene == Scene.none)
            return;
        else
            SceneManager.LoadScene(scene.ToString());
    }
    public static void QuitGame()
    {
        Application.Quit();
    }
}
