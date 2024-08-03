using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minseok.Attack;

namespace Minseok.Animation
{
    public class PlayerAni : MonoBehaviour
    {
        [SerializeField]
        public GameObject First;

        [SerializeField]
        public GameObject Second;

        private Weapon equipWeapon = null;
        private Animator animator;
        public float cooldownTime = 2f;
        private float nextFireTime = 0f;
        public static int AttacknoOfClicks = 0;
        public static int AxenoOfClicks = 0;
        float lastClickedTime = 0;
        float maxComboDelay = 1;

        private void Awake()
        {
            First.SetActive(true);
            equipWeapon = First.GetComponent<Weapon>();
            animator = GetComponent<Animator>();
        }

        private void LateUpdate()
        {
            WeaponSwap();

            #region Attack
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
            {
                animator.SetBool("hit1", false);
            }

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
            {
                animator.SetBool("hit2", false);
            }

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
            {
                animator.SetBool("hit3", false);
                AttacknoOfClicks = 0;
            }
            #endregion

            #region Axe
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Axe1"))
            {
                animator.SetBool("Axe1", false);
            }

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Axe2"))
            {
                animator.SetBool("Axe2", false);
                AxenoOfClicks = 0;
            }
            #endregion

            if (Time.time - lastClickedTime > maxComboDelay)
            {
                AttacknoOfClicks = 0;
                AxenoOfClicks = 0;
            }

            if (Time.time > nextFireTime && Define._AttackCombo1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    equipWeapon.Use();
                    AttackCombo();
                }
                    
            }

            if (Time.time > nextFireTime && Define._AxeCombo1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    equipWeapon.Use();
                    AxeCombo();
                }
                    
            }
        }

        void WeaponSwap()
        {
            if (equipWeapon == null)
                return;

            // ¹«±â »©±â
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                First.SetActive(true);
                Second.SetActive(false);
                equipWeapon = First.GetComponent<Weapon>();
                Define._AttackCombo1 = true;
                Define._AxeCombo1 = false;
            }

            // ¹«±â ÀåÂø
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                First.SetActive(false);
                Second.SetActive(true);
                equipWeapon = Second.GetComponent<Weapon>();
                Define._AxeCombo1 = true;
                Define._AttackCombo1 = false;
            }
        }

        void AttackCombo()
        {
            lastClickedTime = Time.time;
            AttacknoOfClicks++;
            if (AttacknoOfClicks == 1)
            {
                animator.SetBool("hit1", true);
            }
            AttacknoOfClicks = Mathf.Clamp(AttacknoOfClicks, 0, 3);

            if (AttacknoOfClicks >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
            {
                animator.SetBool("hit1", false);
                animator.SetBool("hit2", true);
            }

            if (AttacknoOfClicks >= 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
            {
                animator.SetBool("hit2", false);
                animator.SetBool("hit3", true);
            }
        }

        void AxeCombo()
        {
            lastClickedTime = Time.time;
            AxenoOfClicks++;
            if (AxenoOfClicks == 1)
            {
                animator.SetBool("Axe1", true);
            }
            AxenoOfClicks = Mathf.Clamp(AxenoOfClicks, 0, 2);

            if (AxenoOfClicks >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Axe1"))
            {
                animator.SetBool("Axe1", false);
                animator.SetBool("Axe2", true);
            }
        }
    }
}