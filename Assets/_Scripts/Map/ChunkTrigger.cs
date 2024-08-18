using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    MapController mapController;

    public GameObject targetMap;

    private void Start()
    {
        this.mapController = FindObjectOfType<MapController>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            this.mapController.currentChunk = this.targetMap;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (this.mapController.currentChunk == this.targetMap)
            {
                this.mapController.currentChunk = null;
            }
        }
    }
}
