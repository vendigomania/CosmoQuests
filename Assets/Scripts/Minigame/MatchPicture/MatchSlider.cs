using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Minigames.MatchPicture
{
    public class MatchSlider : Slider, IEndDragHandler
    {
        public UnityEvent<float> OnEndDragEvent = new UnityEvent<float>();

        public void OnEndDrag(PointerEventData eventData)
        {
            OnEndDragEvent?.Invoke(value);
        }
    }
}
