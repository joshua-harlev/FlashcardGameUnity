/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * For: PA1
 * Overview:
 * GameplayContentDisplay.cs | Manages gameplay content display group.
 */

using UnityEngine;

/// <summary>
/// Handles the display of gameplay content, including showing and hiding the
/// associated gameplay content group.
/// </summary>
public class GameplayContentDisplay : MonoBehaviour {
    /// <summary>
    /// Represents the group of gameplay content to be displayed or hidden.
    /// </summary>
    [SerializeField]
    private GameObject GameplayContentGroup;

    /// <summary>
    /// Subscribes to the OnGameplayEnd event during the initialization phase of this object
    /// to handle hiding the gameplay content when the gameplay ends.
    /// </summary>
    private void Awake() {
        GameActions.OnGameplayEnd += HideGameplayContent;
    }

    /// <summary>
    /// Unsubscribes from the OnGameplayEnd event to prevent memory leaks or unwanted behavior when the object is destroyed.
    /// </summary>
    private void OnDestroy() {
        GameActions.OnGameplayEnd -= HideGameplayContent;
    }

    /// <summary>
    /// Hides the gameplay content group by disabling its visibility.
    /// </summary>
    private void HideGameplayContent() {
        GameplayContentGroup.SetActive(false);
    }
}
