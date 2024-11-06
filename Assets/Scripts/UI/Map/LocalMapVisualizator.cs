using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Map
{
    public class LocalMapVisualizator : MonoBehaviour
    {
        [SerializeField] private RectTransform root;
        [SerializeField] private RectTransform[] points;

        public RectTransform Root => root;
        public RectTransform[] Points => points;
    }
}
