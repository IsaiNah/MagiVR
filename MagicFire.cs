using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class MagicFire : MonoBehaviour
{

    //[SerializeField] private List<GameObject> _magicSpells;
    [SerializeField] private FireBall _fireSpellPrefab;
    [SerializeField] private Transform _spellSpawnPosition;
    [SerializeField] private float _shotPower = 25.0f;
    public bool canCast = true;


    public void readSpellPerformed(string spellGesture)
    {
        Debug.Log("Spell Gesture Performed " + spellGesture);
        
                Debug.Log("Fireball Spell");
                fireSpell(spellGesture);
    }

   private void fireSpell(string  spellGesture)
   {
      // _fireSpellPrefab
      Instantiate(_fireSpellPrefab, _spellSpawnPosition.position, _spellSpawnPosition.rotation).SetBulletLetter(spellGesture);
    //  _fireSpellPrefab.GetComponent<Rigidbody>().AddForce(_spellSpawnPosition.forward * _shotPower);
       //Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
   }
}
