using System.Collections;
using System.Linq;
using Extentions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputActionRebindingExtensions;

public class Rebinding : MonoBehaviour
{
    [SerializeField] private InputActionReference inputAction;
    [SerializeField] private Directions direction = Directions.Up;
    [SerializeField] private Text bindText;
    [SerializeField] private Text actionText;

    private void OnEnable()
    {
        UpdateBindingText();
    }

    public void Rebind()
    {
        inputAction.action.PerformInteractiveRebinding((int)direction + 1)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation =>
            {
                RebindComplete(operation);
                UpdateBindingText();
            })
            .Start();
    }

    private void RebindComplete(RebindingOperation operation)
    {
        Debug.Log(operation.action.GetBindingDisplayString());
    }

    private void UpdateBindingText()
    {
        string name = inputAction.action.name;
        InputBinding binding = inputAction.action.bindings.Where(p => p.action == name && p.isPartOfComposite).ToList()[(int)direction];

        bindText.text = InputControlPath.ToHumanReadableString(
            binding.effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice | InputControlPath.HumanReadableStringOptions.UseShortNames);
        actionText.text = binding.name.FirstCharToUpper();
    }

    private enum Directions
    {
        Up,
        Down,
        Left,
        Right
    }
}
