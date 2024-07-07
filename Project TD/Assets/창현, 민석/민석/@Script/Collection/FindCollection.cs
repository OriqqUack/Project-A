using System;
using Data;
using Minseok.Collection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minseok.Collection
{
    // 도감을 열 수 있는 조건을 적으면 됨
    public class FindCollection : MonoBehaviour
    {
        private void Update()
        {
            // Achievement
            #region
            FindAchievement0();
            FindAchievement1();
            FindAchievement2();
            FindAchievement3();
            FindAchievement4();
            FindAchievement5();
            FindAchievement6();
            FindAchievement7();
            FindAchievement8();
            FindAchievement9();
            FindAchievement10();
            FindAchievement11();
            FindAchievement12();
            FindAchievement13();
            FindAchievement14();
            FindAchievement15();
            FindAchievement16();
            FindAchievement17();
            FindAchievement18();
            FindAchievement19();
            #endregion

            // Monster
            #region
            FindMonster0();
            FindMonster1();
            FindMonster2();
            FindMonster3();
            FindMonster4();
            FindMonster5();
            FindMonster6();
            FindMonster7();
            FindMonster8();
            FindMonster9();
            FindMonster10();
            FindMonster11();
            FindMonster12();
            FindMonster13();
            FindMonster14();
            FindMonster15();
            FindMonster16();
            FindMonster17();
            FindMonster18();
            FindMonster19();
            #endregion

            // Weapon
            #region
            FindWeapon0();
            FindWeapon1();
            FindWeapon2();
            FindWeapon3();
            FindWeapon4();
            FindWeapon5();
            FindWeapon6();
            FindWeapon7();
            FindWeapon8();
            FindWeapon9();
            FindWeapon10();
            FindWeapon11();
            FindWeapon12();
            FindWeapon13();
            FindWeapon14();
            FindWeapon15();
            FindWeapon16();
            FindWeapon17();
            FindWeapon18();
            FindWeapon19();
            #endregion
        }

        // Achievement
        #region
        private void FindAchievement0()
        {
            GameObject root = Managers.UI.Root.gameObject;

            // 테블릿을 열면 업적 달성
            GameObject go = Util.FindChild(root, "Tablet", true);
            if (go && Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
            {
                value.Index[0] = true;
            }
        }

        private void FindAchievement1()
        {
            if (Input.GetKey(KeyCode.P) && Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
                value.Index[1] = true;
        }

        private void FindAchievement2()
        {
            // TODO
        }

        private void FindAchievement3()
        {
            // TODO
        }

        private void FindAchievement4()
        {
            // TODO

        }

        private void FindAchievement5()
        {
            // TODO
        }

        private void FindAchievement6()
        {
            // TODO
        }

        private void FindAchievement7()
        {
            // TODO
        }

        private void FindAchievement8()
        {
            // TODO
        }

        private void FindAchievement9()
        {
            // TODO
        }

        private void FindAchievement10()
        {
            // TODO
        }

        private void FindAchievement11()
        {
            // TODO
        }

        private void FindAchievement12()
        {
            // TODO
        }

        private void FindAchievement13()
        {
            // TODO
        }

        private void FindAchievement14()
        {
            // TODO
        }

        private void FindAchievement15()
        {
            // TODO
        }

        private void FindAchievement16()
        {
            // TODO
        }

        private void FindAchievement17()
        {
            // TODO
        }

        private void FindAchievement18()
        {
            // TODO
        }

        private void FindAchievement19()
        {
            // TODO
        }
        #endregion

        // Monster
        #region
        private void FindMonster0()
        {
            // TODO
        }

        private void FindMonster1()
        {
            // TODO
        }

        private void FindMonster2()
        {
            // TODO
        }

        private void FindMonster3()
        {
            // TODO
        }

        private void FindMonster4()
        {
            // TODO

        }

        private void FindMonster5()
        {
            // TODO
        }

        private void FindMonster6()
        {
            // TODO
        }

        private void FindMonster7()
        {
            // TODO
        }

        private void FindMonster8()
        {
            // TODO
        }

        private void FindMonster9()
        {
            // TODO
        }

        private void FindMonster10()
        {
            // TODO
        }

        private void FindMonster11()
        {
            // TODO
        }

        private void FindMonster12()
        {
            // TODO
        }

        private void FindMonster13()
        {
            // TODO
        }

        private void FindMonster14()
        {
            // TODO
        }

        private void FindMonster15()
        {
            // TODO
        }

        private void FindMonster16()
        {
            // TODO
        }

        private void FindMonster17()
        {
            // TODO
        }

        private void FindMonster18()
        {
            // TODO
        }

        private void FindMonster19()
        {
            // TODO
        }
        #endregion

        // Weapon
        #region
        private void FindWeapon0()
        {
            // TODO
        }

        private void FindWeapon1()
        {
            // TODO
        }

        private void FindWeapon2()
        {
            // TODO
        }

        private void FindWeapon3()
        {
            // TODO
        }

        private void FindWeapon4()
        {
            // TODO

        }

        private void FindWeapon5()
        {
            // TODO
        }

        private void FindWeapon6()
        {
            // TODO
        }

        private void FindWeapon7()
        {
            // TODO
        }

        private void FindWeapon8()
        {
            // TODO
        }

        private void FindWeapon9()
        {
            // TODO
        }

        private void FindWeapon10()
        {
            // TODO
        }

        private void FindWeapon11()
        {
            // TODO
        }

        private void FindWeapon12()
        {
            // TODO
        }

        private void FindWeapon13()
        {
            // TODO
        }

        private void FindWeapon14()
        {
            // TODO
        }

        private void FindWeapon15()
        {
            // TODO
        }

        private void FindWeapon16()
        {
            // TODO
        }

        private void FindWeapon17()
        {
            // TODO
        }

        private void FindWeapon18()
        {
            // TODO
        }

        private void FindWeapon19()
        {
            // TODO
        }
        #endregion
    }
}

