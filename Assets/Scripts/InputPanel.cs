using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class InputPanel : MonoBehaviour
{
    [SerializeField]
    private CustomInputField
        spawnTimeField,
        speedField,
        distanceField;

    public IObservable<string> spawnTimeFieldObservable { get; private set; }
    public IObservable<string> speedFieldObservable { get; private set; }
    public IObservable<string> distanceFieldObservable { get; private set; }

    public List<InputField> fields { get; private set; }
  
    void Awake()
    {
        spawnTimeField.onValidateInput += ValidateInputField;
        speedField.onValidateInput += ValidateInputField;
        distanceField.onValidateInput += ValidateInputField;

        spawnTimeFieldObservable = spawnTimeField.OnValueChangedAsObservable();
        speedFieldObservable = speedField.OnValueChangedAsObservable();
        distanceFieldObservable = distanceField.OnValueChangedAsObservable();

        fields = new List<InputField>(){ spawnTimeField, speedField, distanceField };
    }

    private char ValidateInputField(string text, int charIndex, char addedChar)
    {
        return (char.IsDigit(addedChar)) ? addedChar : default;
    }
}
