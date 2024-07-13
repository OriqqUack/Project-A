using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minseok.Animation
{
    public class PlayerAni : MonoBehaviour
    {
        private Animator animator;
        public float cooldownTime = 2f;
        private float nextFireTime = 0f;
        public static int noOfClicks = 0;
        float lastClickedTime = 0;
        float maxComboDelay = 1;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
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
                noOfClicks = 0;
            }

            if (Time.time - lastClickedTime > maxComboDelay)
            {
                noOfClicks = 0;
            }

            if (Time.time > nextFireTime)
            {
                if (Input.GetMouseButtonDown(0))
                    OnClick();
            }
        }

        void OnClick()
        {
            lastClickedTime = Time.time;
            noOfClicks++;
            if (noOfClicks == 1)
            {
                animator.SetBool("hit1", true);
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

            if (noOfClicks >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
            {
                animator.SetBool("hit1", false);
                animator.SetBool("hit2", true);
            }

            if (noOfClicks >= 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
            {
                animator.SetBool("hit2", false);
                animator.SetBool("hit3", true);
            }
        }
    }
}

