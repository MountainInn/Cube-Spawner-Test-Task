using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomInputField : InputField
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        TouchScreenKeyboard.Open(this.text, TouchScreenKeyboardType.NumberPad);
    }
}
