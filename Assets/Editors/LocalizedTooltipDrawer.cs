#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace NCat.Tools
{
    [CustomPropertyDrawer(typeof(LocalizedTooltipAttribute))]
    public class LocalizedTooltipDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var tooltipAttribute = attribute as LocalizedTooltipAttribute;
            if (tooltipAttribute == null) return;
            string tooltipText = tooltipAttribute.Get();
            var content = new GUIContent(label.text, tooltipText);
            if (property.isArray)
            {
                EditorGUI.BeginProperty(position, label, property);
                EditorGUI.PropertyField(position, property, label, true);
                EditorGUI.EndProperty();
            }
            EditorGUI.PropertyField(position, property, content, true);
        }
    }
}
#endif