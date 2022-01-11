using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapping : MonoBehaviour
{
    private Vector3[] snapPointsPosition = new Vector3[9];

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            snapPointsPosition[i] = transform.GetChild(i).transform.position;
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameData.gameData.SelectedObject == this.gameObject)
        {
            Debug.Log("Lol");
            foreach (Vector3 snapPosition in other.GetComponent<Snapping>().snapPointsPosition)
            {
                var xPos = this.gameObject.transform.position.x;
                var zPos = this.gameObject.transform.position.z;
                if (Mathf.Abs(xPos - snapPosition.x) < 1 && Mathf.Abs(zPos - snapPosition.z) < 1)
                {
                    this.gameObject.transform.parent.position = snapPosition;
                    Debug.Log("YAYY");
                    return;
                }
            }
        }
    }
}
