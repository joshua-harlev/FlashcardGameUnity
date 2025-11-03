using UnityEngine;
using UnityEngine.UIElements;
/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * For: PA2
 * Overview:
 * StartScreen.cs | Manages button handlers for start screen
 */

/// <summary>
/// Manages button handlers for the title screen.
/// </summary>
[RequireComponent(typeof(UIDocument))]
public class StartScreen : MonoBehaviour {
    [SerializeField] 
    private UIDocument startScreenDocument;

    private Button quitButton;
    private Button startButton;
    private Button achievementsButton;

    [SerializeField] private GameOptions gameOptions;
    private void Start() {
        VisualElement root = startScreenDocument.rootVisualElement;
        quitButton = root.Query<Button>("ExitButton");
        quitButton.clicked += gameOptions.OnQuitButtonPressed;
        startButton = root.Query<Button>("StartButton");
        startButton.clicked += gameOptions.OnStartButtonPressed;
        achievementsButton = root.Query<Button>("AchievementsButton");
        achievementsButton.clicked += gameOptions.OnAchievementsButtonPressed;
    }

    private void OnDestroy() {
        quitButton.clicked -= gameOptions.OnQuitButtonPressed;
        startButton.clicked -= gameOptions.OnStartButtonPressed;
        achievementsButton.clicked -= gameOptions.OnAchievementsButtonPressed;
    }
}
