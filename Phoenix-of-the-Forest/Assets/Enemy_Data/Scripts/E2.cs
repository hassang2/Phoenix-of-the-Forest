using System.Collections;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemies/E2")]
public class E2 : Enemy {
   [SerializeField] public float projectileSpeed = 10.0f;
   [SerializeField] public GameObject projectileObject;

}
