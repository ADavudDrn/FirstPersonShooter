using Sirenix.OdinInspector;
using UnityEngine;

namespace ReferenceValue
{
    [CreateAssetMenu(fileName = "Float01 Value", menuName = "Values/Float01", order = 0)]
    public class Float01Ref : RefValue
    {
        [ShowInInspector, PropertyRange(0f, 1f)]
        public float Value
        {
            get => _value;
            set
            {
                _value = Mathf.Clamp01(value);
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
            if (value is Float01Ref float01Ref)
            {
                Value = float01Ref.Value;
            }
            else if (value is FloatRef floatRef)
            {
                Value = floatRef.Value;
            }
        }
    }
}