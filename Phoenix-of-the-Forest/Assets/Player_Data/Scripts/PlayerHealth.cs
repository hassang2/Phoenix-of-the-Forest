using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(menuName = "Player/PlayerHealth")]
public class PlayerHealth : ScriptableObject {
   int health;

   public int GetValue() {
      return health;
   }

   public int GetUIValue() {
      return Mathf.Max(health, 0);
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
