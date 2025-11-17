/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * Overview:
 * ButtonToggleGroup.cs | A button toggle group.
 */
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToggleGroup : MonoBehaviour {
    public Button[] buttons;
    public string SelectedValue { get; private set; }

    private void Awake() {
        Setup();
    }

    private void Setup() {
        for(int i = 0; i<buttons.Length; i++) {
            int index = i;
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener( () => OnButtonClick(index));
        }
    }

    private void OnButtonClick(int buttonIndex) {
        for (int i = 0; i < buttons.Length; i++) {
                Button button = buttons[i];
                TMP_Text textComponent = button.GetComponentInChildren<TMP_Text>();
            if (i != buttonIndex) {
                textComponent.color = new Color(120 / 255f, 120 / 255f, 120 / 255f, 1f);
            }
            else {
                textComponent.color = new Color(255 / 255f, 230 / 255f, 140 / 255f, 1f);
                SelectedValue = textComponent.text;
            }
        }
    }
}