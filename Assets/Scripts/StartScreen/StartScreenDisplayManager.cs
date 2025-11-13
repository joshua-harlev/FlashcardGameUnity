using UnityEngine;

public class StartScreenDisplayManager : MonoBehaviour {
    [SerializeField] private GameObject StartScreen;
    [SerializeField] private GameObject OptionsMenu;

    private void OnEnable() {
        GameActions.OnStartButtonClick += ShowOptionsMenu;
        if (Game.Instance.StateMachine.CurrentState is SelectingOptionState selectingOptionState) {
            ShowOptionsMenu();
        }
    }

    private void OnDisable() {
        GameActions.OnStartButtonClick -= ShowOptionsMenu;
    }

    private void ShowOptionsMenu() {
        StartScreen.SetActive(false);
        OptionsMenu.SetActive(true);
    }
}