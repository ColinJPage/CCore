using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Changes the slider as the variable changes and changes the variable as the slider changes
public class SliderFloatVariable : MonoBehaviour
{
    [SerializeField] VariableSO<float> variable;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }
    private void OnEnable()
    {
        slider.value = variable.Value;
        slider.onValueChanged.AddListener(OnSliderChange);
        variable.Subscribe(OnVariableChange);
    }
    private void OnDisable()
    {
        slider?.onValueChanged.RemoveListener(OnSliderChange);
        variable.Unsubscribe(OnVariableChange);
    }
    void OnSliderChange(float newValue)
    {
        if(newValue != variable.Value)
            variable.Value = newValue;
    }
    void OnVariableChange(float newValue)
    {
        slider.value = newValue;
    }
}
