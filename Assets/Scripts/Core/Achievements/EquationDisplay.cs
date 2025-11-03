using UnityEngine;
using UnityEngine.EventSystems;
/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * For: PA2
 * Overview:
 * EquationDisplay.cs | Equation Display, holds pointer event handler
 */
public class EquationDisplay : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventData) {
        AchievementEvents.OnEquationClicked?.Invoke();
    }
}