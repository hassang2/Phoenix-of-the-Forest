using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

   [SerializeField] PlayerHealth health;
   void Update() {
      GetComponent<Text>().text = health.GetValue().ToString(); 
   }
}
