using Sirenix.OdinInspector;
using UnityEngine;

namespace ReferenceValue
{
    [CreateAssetMenu(fileName = "Transform Reference", menuName = "Values/Transform")]
    public class TransformRef : RefValue
    {
        [ShowInInspector]
        public Transform Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueHasChanged();
            }
        }

        private Transform _value;


        public void SetToNull()
        {
            Value = null;
        }

        public override void SetToAnotherRef(RefValue value)
        {
            if (value is TransformRef transformRef)
            {
                Value = transformRef.Value;
            }
        }
    }
}