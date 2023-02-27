using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadVisuals : MonoBehaviour
{
    public VisualsData data;
    GameObject visuals;

    void Start()
    {
        if (data != null)
        {
            LoadNPCVisuals(data);
        }
    }

    public void LoadNPCVisuals(VisualsData data)
    {
        /*foreach (Transform child in this.transform)
        {
            if (Application.isEditor)
            {
                DestroyImmediate(child.gameObject);

            }
            else
            {
                Destroy(child.gameObject);
            }
        }*/

        visuals = Instantiate(data.visuals);
        visuals.transform.SetParent(this.transform);
        visuals.transform.localPosition = Vector2.zero;
        visuals.transform.rotation = Quaternion.identity;
    }

}
