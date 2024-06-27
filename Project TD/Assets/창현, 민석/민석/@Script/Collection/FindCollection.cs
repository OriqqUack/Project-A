using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minseok.Collection
{
    public class FindCollection : MonoBehaviour
    {
        // Achievement
        #region
        public static bool FCAchievement0 = false;
        public static bool FCAchievement1 = false;
        public static bool FCAchievement2 = false;
        public static bool FCAchievement3 = false;
        public static bool FCAchievement4 = false;
        public static bool FCAchievement5 = false;
        public static bool FCAchievement6 = false;
        public static bool FCAchievement7 = false;
        public static bool FCAchievement8 = false;
        public static bool FCAchievement9 = false;
        public static bool FCAchievement10 = false;
        public static bool FCAchievement11 = false;
        public static bool FCAchievement12 = false;
        public static bool FCAchievement13 = false;
        public static bool FCAchievement14 = false;
        public static bool FCAchievement15 = false;
        public static bool FCAchievement16 = false;
        public static bool FCAchievement17 = false;
        public static bool FCAchievement18 = false;
        public static bool FCAchievement19 = false;
        #endregion

        // Monster
        #region
        public static bool FCMonster0 = false;
        public static bool FCMonster1 = false;
        public static bool FCMonster2 = false;
        public static bool FCMonster3 = false;
        public static bool FCMonster4 = false;
        public static bool FCMonster5 = false;
        public static bool FCMonster6 = false;
        public static bool FCMonster7 = false;
        public static bool FCMonster8 = false;
        public static bool FCMonster9 = false;
        public static bool FCMonster10 = false;
        public static bool FCMonster11 = false;
        public static bool FCMonster12 = false;
        public static bool FCMonster13 = false;
        public static bool FCMonster14 = false;
        public static bool FCMonster15 = false;
        public static bool FCMonster16 = false;
        public static bool FCMonster17 = false;
        public static bool FCMonster18 = false;
        public static bool FCMonster19 = false;
        #endregion

        // Weapon
        #region
        public static bool FCWeapon0 = false;
        public static bool FCWeapon1 = false;
        public static bool FCWeapon2 = false;
        public static bool FCWeapon3 = false;
        public static bool FCWeapon4 = false;
        public static bool FCWeapon5 = false;
        public static bool FCWeapon6 = false;
        public static bool FCWeapon7 = false;
        public static bool FCWeapon8 = false;
        public static bool FCWeapon9 = false;
        public static bool FCWeapon10 = false;
        public static bool FCWeapon11 = false;
        public static bool FCWeapon12 = false;
        public static bool FCWeapon13 = false;
        public static bool FCWeapon14 = false;
        public static bool FCWeapon15 = false;
        public static bool FCWeapon16 = false;
        public static bool FCWeapon17 = false;
        public static bool FCWeapon18 = false;
        public static bool FCWeapon19 = false;
        #endregion

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
            if (go)
                FCAchievement0 = true;
        }

        private void FindAchievement1()
        {
            // TODO
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

