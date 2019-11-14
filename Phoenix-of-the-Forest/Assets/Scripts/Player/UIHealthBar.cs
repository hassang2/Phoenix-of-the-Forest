using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour {

   [SerializeField] PlayerHealth health;
   void Update() {
      GetComponent<Text>().text = health.GetUIValue().ToString(); 
   }
}
