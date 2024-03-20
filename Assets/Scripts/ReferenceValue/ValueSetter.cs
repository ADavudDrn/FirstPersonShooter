using Sirenix.OdinInspector;
using UnityEngine;

namespace ReferenceValue
{
    [AddComponentMenu("Values/Value Setter")]
    public class ValueSetter : MonoBehaviour
    {
        private enum SetTime
        {
            Awake,
            OnEnable,
            Start,
        }
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField, Required] private RefValue Reference;

        [SerializeField] private SetTime SetOn; 

        [SerializeField, ShowIf("@Reference is IntRef"), LabelText("Value"), Indent]
        private int IntValue;
        
        [SerializeField, ShowIf("@Reference is FloatRef"), LabelText("Value"), Indent]
        private float FloatValue;
        
        [SerializeField, ShowIf("@Reference is Float01Ref"), LabelText("Value"), Indent, Range(0,1)]
        private float Float01Value;
        
        [SerializeField, ShowIf("@Reference is BoolRef"), LabelText("Value"), Indent]
        private bool BoolValue;
        
        [SerializeField, ShowIf("@Reference is Vector3Ref"), LabelText("Value"), Indent]
        private Vector3 Vector3Value;
        
        [SerializeField, ShowIf("@Reference is Vector2Ref"), LabelText("Value"), Indent]
        private Vector2 Vector2Value;
        
        [SerializeField, ShowIf("@Reference is TransformRef"), LabelText("Value"), Indent]
        private Transform TransformValue;

//------Private Variables-------//



#region UNITY_METHODS

        private void Awake()
        {
            if(SetOn != SetTime.Awake)
                return;
            SetValue();
        }

        private void OnEnable()
        {
            if(SetOn != SetTime.OnEnable)
                return;
            SetValue();
        }

        private void Start()
        {
            if(SetOn != SetTime.Start)
                return;
            SetValue();
        }



#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS
        private void SetValue()
        {
            switch (Reference)
            {
                case IntRef intRef:
                    intRef.Value = IntValue;
                    break;
                case FloatRef floatRef:
                    floatRef.Value = FloatValue;
                    break;
                case Float01Ref float01Ref:
                    float01Ref.Value = Float01Value;
                    break;
                case BoolRef boolRef:
                    boolRef.Value = BoolValue;
                    break;
                case Vector2Ref vector2Ref:
                    vector2Ref.Value = Vector2Value;
                    break;
                case Vector3Ref vector3Ref:
                    vector3Ref.Value = Vector3Value;
                    break;
                case TransformRef transformRef:
                    transformRef.Value = TransformValue;
                    break;
            }
        }
#endregion


    }
}