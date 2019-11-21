using UnityEngine;

public class UIDeathMenu : MonoBehaviour {
   // Start is called before the first frame update
   void Start() {
      gameObject.SetActive(false);
   }

   public void ShouldDisplay(bool state) {
      gameObject.SetActive(state);
   }
}
