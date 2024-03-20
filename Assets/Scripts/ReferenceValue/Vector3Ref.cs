using Sirenix.OdinInspector;
using UnityEngine;

namespace ReferenceValue
{
    [CreateAssetMenu(fileName = "Vector3 Value", menuName = "Values/Vector3")]
    public class Vector3Ref : RefValue
    {
        [ShowInInspector]
        public Vector3 Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueHasChanged();
            }
        }

        private Vector3 _value;
        public override void SetToAnotherRef(RefValue value)
        {
            if (value is Vector3Ref vector3Ref)
            {
                Value = vector3Ref.Value;
            }
        }
    }
}
