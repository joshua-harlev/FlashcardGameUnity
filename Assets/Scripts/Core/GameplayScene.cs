using System.Collections;
using UnityEngine;

public class GameplayScene : MonoBehaviour
{
   private void Awake() {
      StartCoroutine(TriggerSceneLoadEvent());
   }

   private IEnumerator TriggerSceneLoadEvent() {
      // wait one frame for event subscriptions to occur
      yield return null;
      Debug.Log("[GameplayScene] Invoking OnGameSceneLoad");
      GameActions.OnGameSceneLoad?.Invoke();
   }
}
