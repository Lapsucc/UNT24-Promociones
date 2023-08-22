using System.Collections;
using UnityEngine;

public class Tweening : MonoBehaviour
{
    public static IEnumerator SetScale(Transform obj, Vector3 scale, bool active = true, float speed = 8)
    {
        if (active) obj.gameObject.SetActive(active);
        while (obj.localScale != scale)
        {
            obj.localScale = Vector3.MoveTowards(obj.localScale, scale, speed * Time.deltaTime);
            yield return null;
        }
        if (!active) obj.gameObject.SetActive(false);
        yield break;
    }
    public static IEnumerator SetPosition(Transform obj, Vector3 pos, bool local = false, bool active = true, float speed = 25)
    {
        if (active) obj.gameObject.SetActive(active);
        if (local)
        {
            while (obj.localPosition != pos)
            {
                obj.localPosition = Vector3.MoveTowards(obj.localPosition, pos, speed * Time.deltaTime);
                yield return null;
            }
        }
        else
        {
            while (obj.position != pos)
            {
                obj.position = Vector3.MoveTowards(obj.position, pos, speed * Time.deltaTime);
                yield return null;
            }
        }
        if (!active) obj.gameObject.SetActive(false);
        yield break;
    }
    public IEnumerator SetScaledPosition()
    {
        yield return null;
    }
}
