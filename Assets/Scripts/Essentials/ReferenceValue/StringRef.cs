using Sirenix.OdinInspector;
using UnityEngine;

namespace ReferenceValue
{
    [CreateAssetMenu(fileName = "String Value", menuName = "Values/String")]
    public class StringRef : RefValue
    {
        [ShowInInspector]
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueHasChanged();
            }
        }

        private string _value;
        public override void SetToAnotherRef(RefValue value)
        {
            if (value is StringRef stringRef)
            {
                Value = stringRef.Value;
            }
        }
    }
}