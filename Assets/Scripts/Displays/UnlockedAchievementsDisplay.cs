/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * For: PA2
 * Overview:
 * UnlockedAchievementsDisplay.cs | Click handler for the end-of-game unlocked achievements display
 */
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// Click handler for the end-of-game unlocked achievements display.
/// </summary>
/// <remarks>
/// This class provides functionality to navigate to the Achievements scene when the associated element is clicked.
/// It implements the <see cref="IPointerClickHandler"/> interface to listen for pointer click events.
/// </remarks>
/// <example>
/// Attach this script to a UI element to enable navigation to the Achievements scene when clicked.
/// Ensure the "Scenes/Achievements" scene is added to the build settings.
/// </example>
public class UnlockedAchievementsDisplay : MonoBehaviour, IPointerClickHandler
{
    // the actual display functionality is delegated to the EndGameDisplay class

    public void OnPointerClick(PointerEventData eventData) {
        SceneManager.LoadScene("Scenes/Achievements");
    }
}
