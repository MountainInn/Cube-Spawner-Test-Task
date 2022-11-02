using UnityEngine;
using UnityEngine.EventSystems;

public class InputFieldsFocus : MonoBehaviour
{
    [SerializeField]
    InputPanel inputPanel;

    private int fieldId;
    private EventSystem evSystem;

    private void Awake()
    {
        evSystem = EventSystem.current;
    }

    private void Start()
    {
        SelectNextInputField();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool shiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            if (shiftPressed)
            {
                fieldId -= 1;
                if (fieldId < 0) fieldId += inputPanel.fields.Count;
            }
            else
            {
                fieldId = (fieldId+1) % inputPanel.fields.Count;
            }

            SelectNextInputField();
        }
    }

    private void SelectNextInputField()
    {
        var field = inputPanel.fields[fieldId];

        field.OnPointerClick(new PointerEventData(evSystem));
        evSystem.SetSelectedGameObject(field.gameObject, new BaseEventData(evSystem));
    }
}
