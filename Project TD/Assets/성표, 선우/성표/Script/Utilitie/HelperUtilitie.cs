using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class HelperUtilitie
{

    public static bool ValidateCheckEmptySring(Object thisObject, string fieldName, string stringToCheck)
    {
        if (stringToCheck == "")
        {
            Debug.Log("is empty and must contain a value in object" + thisObject.name.ToString());
            return true;
        }
        return false;
    }

    public static bool ValidateCheckEnumerableValues(Object thisObject, string fieldName, IEnumerable enumerableObjectToCheck)
    {
        bool error = false;
        int count = 0;

        if (enumerableObjectToCheck == null)
        {
            Debug.Log(fieldName + "is null in object " + thisObject.name.ToString());
            return true;
        }

        foreach (var item in enumerableObjectToCheck)
        {
            if (item == null)
            {
                Debug.Log(fieldName + "has null values in object" + thisObject.name.ToString());
                error = true;
            }
            else
            {
                count++;
            }
        }

        if (count == 0)
        {
            Debug.Log(fieldName + "has no values in object" + thisObject.name.ToString());
            error = true;
        }

        return error;
    }

    /// <summary>
    /// empty string debug check
    /// </summary>
    /// <param name="thisObject"></param>
    /// <param name="fieldName"></param>
    /// <param name="stringToCheck"></param>
    /// <returns></returns>
    public static bool ValidateCheckEmptyString(UnityEngine.Object thisObject, string fieldName, string stringToCheck)
    {
        if (stringToCheck == "")
        {
            Debug.Log("is empty and must contain a value in object" + thisObject.name.ToString());
            return true;
        }
        return false;
    }

    /// <summary>
    /// null value debug check 
    /// </summary>
    /// <param name="thisObject"></param>
    /// <param name="fieldName"></param>
    /// <param name="objectTocheck"></param>
    /// <returns></returns>
    public static bool ValidateCheckNullValue(UnityEngine.Object thisObject, string fieldName, UnityEngine.Object objectTocheck)
    {
        if (objectTocheck == null)
        {
            Debug.Log(fieldName + "is null and must contain a value in object" + thisObject.name.ToString());
            return true;
        }
        return false;
    }

    /// <summary>
    /// positive value debug check - if zero is allowed set isZeroAllowed to true. Returns true if there is an error
    /// </summary>
    /// <param name="thisObject"></param>
    /// <param name="fieldName"></param>
    /// <param name="valueToCheck"></param>
    /// <param name="isZeroAllowed"></param>
    /// <returns></returns>
    public static bool ValidateCheckPositiveValue(UnityEngine.Object thisObject, string fieldName, float valueToCheck, bool isZeroAllowed)
    {
        bool error = false;

        if (isZeroAllowed)
        {
            if (valueToCheck < 0)
            {
                Debug.Log(fieldName + " must contain a positive value or zero in object" + thisObject.name.ToString());
                error = true;
            }
        }
        else
        {
            if (valueToCheck <= 0)
            {
                Debug.Log(fieldName + " must contain a positive value in object" + thisObject.name.ToString());
                error = true;
            }
        }
        return error;
    }

    /// <summary>
    /// positive value debug check - if zero is allowed set isZeroAllowed to true. Return true if is an error.
    /// </summary>
    /// <param name="thisObject"></param>
    /// <param name="fieldName"></param>
    /// <param name="valueToCheck"></param>
    /// <param name="isZeroAllowed"></param>
    /// <returns></returns>
    public static bool ValidateCheckPositiveValue(UnityEngine.Object thisObject, string fieldName, int valueToCheck, bool isZeroAllowed)
    {
        bool error = false;

        if (isZeroAllowed)
        {
            if (valueToCheck < 0)
            {
                Debug.Log(fieldName + " must contain a positive value or zero in object " + thisObject.name.ToString());
                error = true;
            }
        }
        else
        {
            if (valueToCheck <= 0)
            {
                Debug.Log(fieldName + " must contain a positive value in object" + thisObject.name.ToString());
                error = true;
            }
        }

        return error;
    }

    public static void Suffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, (n + 1));
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
