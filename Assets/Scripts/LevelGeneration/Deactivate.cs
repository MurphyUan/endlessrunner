using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    [SerializeField]
    bool deactiveScheduled = false;

    private void LateUpdate() {
        if(this.transform.position.z <= -1 && gameObject.activeInHierarchy && !deactiveScheduled)
        {
            Invoke("DeactivateObject", 1.5f);
            deactiveScheduled = true;
        }
    }

    private void DeactivateObject()
    {
        this.gameObject.SetActive(false);
        deactiveScheduled = false;
    }
}
