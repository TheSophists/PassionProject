using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Screenshot : MonoBehaviour
{
    Camera camera;
    public List<GameObject> sceneObjects;
    public List<InventoryItemData> dataObjects;

    void TakeScreenshot(string fullPath)
    {
        if (camera = null)
        {
            camera = GetComponent<Camera>();
        }

        RenderTexture rt = new RenderTexture(256, 256, 24);
        GetComponent<Camera>().targetTexture = rt;
        Texture2D screenShot = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        GetComponent<Camera>().Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
        GetComponent<Camera>().targetTexture = null;
        RenderTexture.active = null;

        if (Application.isEditor)
        {
            DestroyImmediate(rt);
        }
        else
        {
            Destroy(rt);
        }

        byte[] bytes = screenShot.EncodeToPNG();
        System.IO.File.WriteAllBytes(fullPath, bytes);
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    [ContextMenu("Screenshot")]
    private void ProcessScreenshots()
    {
        StartCoroutine(ScreenShot());
    }

    private IEnumerator ScreenShot()
    {
        for(int i = 0; i < sceneObjects.Count; i++)
        {
            Debug.Log(i);
            GameObject obj = sceneObjects[i];
            InventoryItemData data = dataObjects[i];

            obj.gameObject.SetActive(true);

            yield return null;

            TakeScreenshot($"{Application.dataPath}/{"Icons"}/{data.id}_Icon.png");

            yield return null;
            obj.gameObject.SetActive(false);

            Sprite s = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/Icons/{data.id}_Icon.png");

            if(s != null)
            {
                data.icon = s;
                EditorUtility.SetDirty(data);
            }

            yield return null;
        }
    }
}
