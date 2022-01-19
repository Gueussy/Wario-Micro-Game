using System;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(BPMRangeAttribute))]
// ReSharper disable once CheckNamespace
public class BPMRangeDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.Integer)
        {
            EditorGUI.LabelField(position, label.text, "Use Range with int.");
            return;
        }

        int min = 50;
        int max = 80;
        int step = 10;
        int numberOfSteps = (max - min) / step;
        float range = ((float) property.intValue - min) / (max - min) * numberOfSteps;
        int ceil = Mathf.RoundToInt(range);
        property.intValue = ceil * step + min;
        
        EditorGUI.IntSlider(position, property, Convert.ToInt32(min), Convert.ToInt32(max), label);
        
    }
}
