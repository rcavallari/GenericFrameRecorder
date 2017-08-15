using System;
using UnityEngine;
using UnityEngine.FrameRecorder;
using UnityEngine.FrameRecorder.Input;

namespace UnityEditor.FrameRecorder
{
    [CustomEditor(typeof(ImageRecorderSettings))]
    public class ImageRecorderEditor : RecorderEditor
    {
        SerializedProperty m_OutputFormat;
        RTInputSelector m_RTInputSelector;
        
        [MenuItem("Window/Recorder/Video")]
        static void ShowRecorderWindow()
        {
            RecorderWindow.ShowAndPreselectCategory("Video");
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (target == null)
                return;

            m_RTInputSelector = new RTInputSelector("Pixels", false);

            var pf = new PropertyFinder<ImageRecorderSettings>(serializedObject);
            m_Inputs = pf.Find(w => w.m_SourceSettings);
            m_OutputFormat = pf.Find(w => w.m_OutputFormat);
        }

        protected override void OnEncodingGroupGui()
        {
            // hiding this group by not calling parent class's implementation.  
        }

        protected override void OnInputGui( int inputIndex)
        {
            var input = m_Inputs.GetArrayElementAtIndex(inputIndex).objectReferenceValue as RecorderInputSetting;
            if (m_RTInputSelector.OnInputGui(ref input))
            {
                if( input is CBRenderTextureInputSettings )
                    (input as CBRenderTextureInputSettings).m_FlipVertical = true;

                ChangeInputSettings(inputIndex, input);                
            }

            base.OnInputGui(inputIndex);
        }

        protected override void OnOutputGui()
        {
            AddProperty(m_OutputFormat, () => EditorGUILayout.PropertyField(m_OutputFormat, new GUIContent("Output format")));
            base.OnOutputGui();
        }
    }
}
