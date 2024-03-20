using Sirenix.OdinInspector;
using UnityEngine;

namespace ReferenceValue
{
    [CreateAssetMenu(fileName = "Vector2 Value", menuName = "Values/Vector2")]
    public class Vector2Ref : RefValue
    {
        [ShowInInspector]
        public Vector2 Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueHasChanged();
            }
        }

        private Vector2 _value;
        public override void SetToAnotherRef(RefValue value)
        {
            if (value is Vector2Ref vector2Ref)
            {
                Value = vector2Ref.Value;
            }
        }
    }
}
