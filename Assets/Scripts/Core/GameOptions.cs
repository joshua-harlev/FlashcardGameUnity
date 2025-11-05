/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * For: PA1
 * Overview:
 * GameOptions.cs | Manages game option buttons.
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// GameOptions is responsible for controlling navigation actions
/// such as restarting, quitting, or starting the game.
/// It facilitates scene transitions and can display a loading screen during these processes.
/// </summary>
public class GameOptions : MonoBehaviour {
    /// <summary>
    /// A prefab used to display a loading screen during scene transitions.
    /// </summary>
    [SerializeField] private GameObject LoadingScreenPrefab;

    /// <summary>
    /// Called when the restart button is pressed. Initiates loading of the game scene.
    /// </summary>
    public void OnRestartButtonPressed() {
        if (Game.Instance.StateMachine.CurrentState is ResultsState resultsState) {
            resultsState.OnRestartButtonPressed();
        }
        else {
            DebugLogger.Log(LogChannel.Systems,
                $"Restart button was pressed but game was in {nameof(Game.Instance.StateMachine.CurrentState)} state");
        }
        StartCoroutine(LoadSceneAsync("Scenes/FlashcardGame"));
    }

    /// <summary>
    /// Called when the quit button is pressed. Exits the application or stops play mode in the editor.
    /// </summary>
    public void OnQuitButtonPressed() {
        # if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        # else
        Application.Quit();
        #endif
    }

    /// <summary>
    /// Called when the start button is pressed. Initiates loading of the game scene.
    /// </summary>
    public void OnStartButtonPressed() {
        if (Game.Instance.StateMachine.CurrentState is MainMenuState mainMenuState) {
            mainMenuState.OnStartButtonPressed();
        }
        else {
            DebugLogger.Log(LogChannel.Systems,
                $"Start button was pressed but game was not in {nameof(Game.Instance.StateMachine.CurrentState)} state");
        }
    }

    public void OnAchievementBackButtonPressed() {
        if (Game.Instance.StateMachine.CurrentState is AchievementsState achievementsState) {
            achievementsState.OnBackButtonPressed();
            StartCoroutine(LoadSceneAsync("Scenes/TitleScreen"));
        }
        else {
            DebugLogger.Log(LogChannel.Systems,
                $"Back button was pressed but game was in {nameof(Game.Instance.StateMachine.CurrentState)} state");
        }
    }

    /// <summary>
    /// Loads a scene asynchronously and displays a loading screen during the process.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    /// <returns>An enumerator for the asynchronous operation.</returns>
    private IEnumerator LoadSceneAsync(string sceneName) {
        GameObject LoadingScreen = Instantiate(LoadingScreenPrefab);
        LoadingScreen.SetActive(true);
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);
        loadOperation.allowSceneActivation = true;
        while (!loadOperation.isDone) {
            yield return null;
        }
    }

    public void OnAchievementsButtonPressed() {
        if (Game.Instance.StateMachine.CurrentState is MainMenuState mainMenuState) {
            mainMenuState.OnAchievementsButtonPressed();
        }
        else {
            DebugLogger.Log(LogChannel.Systems,
                $"Achievements button was pressed but game was in {nameof(Game.Instance.StateMachine.CurrentState)} state");
        }
        StartCoroutine(LoadSceneAsync("Scenes/Achievements"));
    }
}
