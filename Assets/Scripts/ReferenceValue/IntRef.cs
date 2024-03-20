using Sirenix.OdinInspector;
using UnityEngine;

namespace ReferenceValue
{
    [CreateAssetMenu(fileName = "Int Value", menuName = "Values/Int")]
    public class IntRef : RefValue
    {
        [ShowInInspector]
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueHasChanged();
            }
        }

        private int _value;
        
        
        public void MathAdd(int amount)
        {
            Value += amount;
        }
        
        public void MathSubtract(int amount)
        {
            Value -= amount;
        }
        
        public void MathMultiply(float amount)
        {
            Value = (int) (Value * amount);
        }
        
        public void MathDivide(float amount)
        {
            Value = Mathf.RoundToInt(Value / amount);
        }


        public override void SetToAnotherRef(RefValue value)
        {
            if (value is IntRef intRef)
            {
                Value = intRef.Value;
            }
        }
    }
}