using Sirenix.OdinInspector;
using UnityEngine;

namespace ReferenceValue
{
    [CreateAssetMenu(fileName = "Bool Value", menuName = "Values/Bool", order = 0)]
    public class BoolRef : RefValue
    {
        [ShowInInspector]
        public bool Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueHasChanged();
            }
        }

        private bool _value;
        
        
        public void Toggle()
        {
            Value = !Value;
        }

        public override void SetToAnotherRef(RefValue value)
        {
            if (value is BoolRef boolRef)
            {
                Value = boolRef.Value;
            }
        }
    }
}