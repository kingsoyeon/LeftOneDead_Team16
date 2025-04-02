using System.Collections;
using UnityEngine;

public class MuzzleFX : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
