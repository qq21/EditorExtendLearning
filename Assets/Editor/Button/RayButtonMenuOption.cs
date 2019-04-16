using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityEditor.UI
{
   
    public class RayButtonMenuOption 
    {
        private const float kWidth = 160f;
        private const float kThickHeight = 30f;
        
       // static GameObject rayButton = Resources.Load<GameObject>("Prefabs/"+ "UI/RayBtn");
    
        public static Vector2 CanvasSize = new Vector2(1920, 1080);


        [MenuItem("GameObject/UI/RayButton", false, 2029)]
        static public void AddButton(MenuCommand menuCommand)
        {

            GameObject parent = menuCommand.context as GameObject;


            GameObject buttonRoot = CreateUIElementRoot("Button",  new Vector2(kWidth, kThickHeight));

            GameObject childText = new GameObject("Text");
            childText.AddComponent<RectTransform>();
            SetParentAndAlign(childText, buttonRoot);
            Image image = buttonRoot.AddComponent<Image>();

            image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");

            image.type = Image.Type.Sliced;
            image.color = new Color(1f, 1f, 1f, 1f);

            Button bt = buttonRoot.AddComponent<RayButton>();
            SetDefaultColorTransitionValues(bt);

            Text text = childText.AddComponent<Text>();
            text.text = "RayButton";
            text.alignment = TextAnchor.MiddleCenter;
            SetDefaultTextValues(text);

            RectTransform textRectTransform = childText.GetComponent<RectTransform>();
            textRectTransform.anchorMin = Vector2.zero;
            textRectTransform.anchorMax = Vector2.one;
            textRectTransform.sizeDelta = Vector2.zero;


            //if (rayButton == null)
            //{
            //   // Debug.LogError("The Path is Wrong");
             
            //}

            //rayButton.name = "RayButton";

            if (parent == null)
            {
                Canvas ca = GameObject.FindObjectOfType<Canvas>();
                
                if (ca == null)
                {
                    //Create Canvas
                    parent = CreatecCanvas(CanvasSize);
                }
                else
                {
                    parent = ca.gameObject;
                }
            }else if (parent.GetComponentInParent<Canvas>() == null&&parent.GetComponentInChildren<Canvas>()==null)
            {
                Canvas ca = GameObject.FindObjectOfType<Canvas>();
                if (ca == null)
                {
                    //Create Canvas
                    parent = CreatecCanvas(CanvasSize);

                }
                else
                {
                    parent = ca.gameObject;
                }
            }
            else
            {
                if (parent.GetComponentInParent<Canvas>() != null)
                {
                    parent = parent.GetComponentInParent<Canvas>().transform.gameObject;
                }

                if (parent.GetComponentInChildren<Canvas>() != null)
                {
                    parent = parent.GetComponentInChildren<Canvas>().transform.gameObject;
                }
                
            }


            GameObject go = buttonRoot;  //Instantiate(rayButton, parent.transform);
            go.transform.SetParent(parent.transform);
            Selection.activeGameObject = go;
            go.name = buttonRoot.name;
            go.transform.localPosition = Vector3.zero;
            Undo.RegisterCreatedObjectUndo(go, "Create go");

        }

       static  GameObject CreatecCanvas(Vector2 size)
        {

           GameObject  root = new GameObject("Canvas");
            root.layer = LayerMask.NameToLayer("UI");
            Canvas canvas = root.AddComponent<Canvas>();

#if XRMODE
            canvas.renderMode = RenderMode.WorldSpace;

#else
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            UnityEngine.EventSystems.EventSystem tempSystem =
                GameObject.FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
            if (tempSystem==null)
            {
                GameObject go = new GameObject("EventSystem");
                tempSystem = go.AddComponent<UnityEngine.EventSystems.EventSystem>();
                Undo.AddComponent<StandaloneInputModule>(go);
            }
#endif
            root.AddComponent<CanvasScaler>();
            root.AddComponent<GraphicRaycaster>();
            root.GetComponent<RectTransform>().sizeDelta =size;
            root.transform.position = Vector3.zero;

            Undo.RegisterCreatedObjectUndo(root, "Create " + root.name);

           return root;

        }

        #region Tool's method

        private static void SetDefaultTextValues(Text lbl)
        {
            // Set text values we want across UI elements in default controls.
            // Don't set values which are the same as the default values for the Text component,
            // since there's no point in that, and it's good to keep them as consistent as possible.

            lbl.color =Color.black;

            // Reset() is not called when playing. We still want the default font to be assigned
            lbl.font= Resources.GetBuiltinResource<Font>("Arial.ttf");
        }
        private static void SetDefaultColorTransitionValues(Selectable slider)
        {
            ColorBlock colors = slider.colors;
            colors.highlightedColor = new Color(0.882f, 0.882f, 0.882f);
            colors.pressedColor = new Color(0.698f, 0.698f, 0.698f);
            colors.disabledColor = new Color(0.521f, 0.521f, 0.521f);
        }
        private static GameObject CreateUIElementRoot(string name, Vector2 size)
        {
            GameObject child = new GameObject(name);
            RectTransform rectTransform = child.AddComponent<RectTransform>();
            rectTransform.sizeDelta = size;
            return child;
        }
        private static void SetParentAndAlign(GameObject child, GameObject parent)
        {
            if (parent == null)
                return;

            child.transform.SetParent(parent.transform, false);
            SetLayerRecursively(child, parent.layer);
        }
        private static void SetLayerRecursively(GameObject go, int layer)
        {
            go.layer = layer;
            Transform t = go.transform;
            for (int i = 0; i < t.childCount; i++)
                SetLayerRecursively(t.GetChild(i).gameObject, layer);
        }
        #endregion
    }
}
