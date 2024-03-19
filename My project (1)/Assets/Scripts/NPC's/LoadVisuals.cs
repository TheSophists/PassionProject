using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadVisuals : MonoBehaviour
{
    public VisualsData data;
    GameObject visuals;

    void Start()
    {
        if (data != null)       //check to see if the empty npc (friendly or enemy) has data about its visuals attached to it
        {
            LoadNPCVisuals(data);       //call LoadNPCVisuals with that data if it exists.
        }
    }

    public void LoadNPCVisuals(VisualsData data)
    {
        /*foreach (Transform child in this.transform)       //I believe that this code was written as an example in the explanation about how to load npc visuals. 
        {                                                   //it will stay as a comment for now, as I may realise I need it later. 
            if (Application.isEditor)
            {
                DestroyImmediate(child.gameObject);

            }
            else
            {
                Destroy(child.gameObject);
            }
        }*/

        visuals = Instantiate(data.visuals);                //instantiate the "visuals" sprite. 
        visuals.transform.SetParent(this.transform);        //set the object this script is attached to as the parent object.
        visuals.transform.localPosition = Vector3.zero;     //set the position of the visuals to be (0,0) compared to its parent. (so equal to its parent's position.
        visuals.transform.rotation = Quaternion.identity;   //set the rotation the same as the parent as well. 
    }

}
