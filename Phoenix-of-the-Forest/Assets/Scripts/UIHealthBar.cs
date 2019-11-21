using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour {

   [SerializeField] PlayerHealth health;
   void Update() {
      GetComponentInChildren<Text>().text = health.GetUIValue().ToString(); 
   }
}
