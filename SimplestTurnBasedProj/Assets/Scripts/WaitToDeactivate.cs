using System.Collections;
using UnityEngine;

public class WaitToDeactivate : MonoBehaviour
{
    [SerializeField] private float secondsToWait = 1f;

    private void OnEnable()
    {
        StartCoroutine(WaitToDeactivateRoutine());
    }

    private IEnumerator WaitToDeactivateRoutine()
    {
        yield return new WaitForSeconds(secondsToWait);
        gameObject.SetActive(false);
    }
}
