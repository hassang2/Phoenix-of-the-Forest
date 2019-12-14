using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelGoal : MonoBehaviour {
   void OnTriggerEnter2D(Collider2D other) {
      if (other.gameObject.CompareTag("Player")) 
         SceneManager.LoadScene("MainMenu");
      
   }
}
