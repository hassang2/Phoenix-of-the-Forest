using UnityEngine;

public class UIManager : MonoBehaviour {

     [SerializeField] GameObject deathUI;
     [SerializeField] GameObject guardianScript;


   void Start() {
        DisplayDeathUI(false);
        guardianScript.SetActive(true);
   }

   public void DisplayDeathUI(bool state) {
        deathUI.SetActive(state);
        guardianScript.SetActive(false);
   }
}
