using System.Collections;
using UnityEngine;

static public class CoroutineHelper
{
    static public Coroutine InvokeAfter(this MonoBehaviour mono, float seconds, System.Action action)
    {
        IEnumerator Invoke()
        {
            yield return new WaitForSeconds(seconds);

            action.Invoke();
        }

        return mono.StartCoroutine(Invoke());
    }
}
