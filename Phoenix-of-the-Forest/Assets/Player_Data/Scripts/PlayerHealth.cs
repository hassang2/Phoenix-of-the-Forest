using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(menuName = "Player/PlayerHealth")]
public class PlayerHealth : ScriptableObject {
   int health;

   public int GetValue() {
      return health;
   }

   public void SetValue(int h) {
      Assert.IsTrue(h >= 0);
      health = h;
   }

   public void AddValue(int h) {
      health += h;
      if (health <= 0) {
         // TRIGGER DEATH EVENT
      }
   }
}
