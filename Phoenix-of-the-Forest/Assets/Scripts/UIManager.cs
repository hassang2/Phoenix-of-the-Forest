using UnityEngine;

public class UIManager : MonoBehaviour {
   [SerializeField] GameObject deathUI;

   void Start() {
      DisplayDeathUI(false);
   }

   public void DisplayDeathUI(bool state) {
      deathUI.SetActive(state);
   }
}
