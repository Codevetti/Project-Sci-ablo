using UnityEngine;
using System.Collections;

[AddComponentMenu("MiniMap/Map arrow")]
public class MapArrow : MonoBehaviour {

    private RectTransform _arrowRect;

    public RectTransform ArrowRect
    {
        get
        {
            if (!_arrowRect)
            {
                _arrowRect = GetComponent<RectTransform>();
                if (!_arrowRect)
                {
                    Debug.LogError("RectTransform not found. Image not attached.");
                }
            }

            return _arrowRect;
        }
    }

    public void rotate(Quaternion quat)
    {
        ArrowRect.rotation = quat;
    }

}
