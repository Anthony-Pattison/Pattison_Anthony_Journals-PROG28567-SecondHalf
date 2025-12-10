using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SwingingPlatform : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Swinging());
    }

    IEnumerator Swinging()
    {
        while (true)
        {
            Vector3 currentRotation = transform.eulerAngles * Mathf.Deg2Rad;
            while (Mathf.Round(currentRotation.z) != 1)
            {
                transform.eulerAngles += new Vector3(0, 0, 20) * Time.deltaTime;
                currentRotation = transform.eulerAngles * Mathf.Deg2Rad;
                yield return null;
            }
            while (Mathf.Round(currentRotation.z) != 5)
            {
                transform.eulerAngles -= new Vector3(0, 0, 20) * Time.deltaTime;
                currentRotation = transform.eulerAngles * Mathf.Deg2Rad;
                yield return null;
            }
        }
    }

    
}
