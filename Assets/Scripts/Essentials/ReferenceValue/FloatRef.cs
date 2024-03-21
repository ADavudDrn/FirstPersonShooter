using Sirenix.OdinInspector;
using UnityEngine;

namespace ReferenceValue
{
    [CreateAssetMenu(fileName = "Float Value", menuName = "Values/Float")]
    public class FloatRef : RefValue
    {
        [ShowInInspector]
        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueHasChanged();
            }
        }

        private float _value;
        
        
        public void MathAdd(float amount)
        {
            Value += amount;
        }
        
        public void MathSubtract(float amount)
        {
            Value -= amount;
        }
        
        public void MathMultiply(float amount)
        {
            Value *= amount;
        }
        
        public void MathDivide(float amount)
        {
            Value /= amount;
        }

        public override void SetToAnotherRef(RefValue value)
        {
            if (value is FloatRef floatRef)
            {
                Value = floatRef.Value;
            }
            else if (value is Float01Ref float01Ref)
            {
                Value = float01Ref.Value;
            }
        }
    }
}
