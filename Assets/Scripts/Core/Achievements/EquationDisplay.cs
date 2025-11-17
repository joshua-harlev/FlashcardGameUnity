/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * Overview:
 * EquationDisplay.cs | Equation Display, holds pointer event handler
 */

using UnityEngine;
using UnityEngine.EventSystems;
public class EquationDisplay : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventData) {
        AchievementEvents.OnEquationClicked?.Invoke();
    }
}