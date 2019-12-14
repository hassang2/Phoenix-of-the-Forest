using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

   public bool gameOver { get; private set; }
   void Start() {
      gameOver = false;
      Time.timeScale = 1.0f;
   }

   public void EndGame() {
      gameOver = true;
      Time.timeScale = 0.0f;
   }

   public void RestartScene() {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }

   public void LoadMainMenu() {
      SceneManager.LoadScene("MainMenu");
   }

   public void CloseGame() {
      Application.Quit();
   }
}
