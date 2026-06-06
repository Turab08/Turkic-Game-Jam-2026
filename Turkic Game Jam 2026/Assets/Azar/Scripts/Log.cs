using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnDraggingProcess += Handle_OnDraggingProcess;
    }

    private void OnDisable()
    {
        EventManager.OnDraggingProcess -= Handle_OnDraggingProcess;
    }

    private void Handle_OnDraggingProcess(bool dragState, GameObject draggedObject)
    {
        if(draggedObject != this.gameObject)
            return;

        if(dragState)
        {
            GameManager.instance.RemoveItem(this.gameObject);
            return;
        }

        Destroy(this.gameObject);
    }
}
