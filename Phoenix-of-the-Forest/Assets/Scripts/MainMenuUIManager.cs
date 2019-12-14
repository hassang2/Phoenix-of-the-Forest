using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    public void CloseGame() {
      Application.Quit();
   }

   public void LoadLevel1() {
      SceneManager.LoadScene("Level1");
   }

   public void LoadLevel2() {
      SceneManager.LoadScene("Level2");
   }

   public void LoadLevel3() {
      SceneManager.LoadScene("Level3");
   }
}
