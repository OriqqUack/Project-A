using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minseok.Attack
{
    public class Weapon : MonoBehaviour
    {
        public enum Type { Melee, Range };
        public Type type;
        public int damage;
        public float rate;
        public BoxCollider meleeArea;
        public TrailRenderer trailEffect;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Monster"))
            {
                Debug.Log("Attack");
            }
        }

        public void Use()
        {
            if (type == Type.Melee)
            {
                StopCoroutine("Swing");
                StartCoroutine("Swing");
            }
        }

        IEnumerator Swing()
        {
            yield return new WaitForSeconds(0.3f);
            meleeArea.enabled = true;
            trailEffect.enabled = true;

            yield return new WaitForSeconds(0.5f);
            meleeArea.enabled = false;

            yield return new WaitForSeconds(0.5f);
            trailEffect.enabled = false;
        }
    }

}
