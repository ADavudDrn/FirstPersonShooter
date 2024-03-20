using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ReferenceValue
{
    [AddComponentMenu("Values/Value Observer")]
    public class ValueObserver : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField, Required] private RefValue Reference;
        [SerializeField] private bool WaitUntilStart = true;

        [SerializeField, ShowIf(nameof(ValidateIfComparable))]
        private bool HasCondition;

        [LabelText("Response if"), LabelWidth(80), Indent, SerializeField, ShowIf(nameof(ValidateIfComparableNumber)),
         EnableIf(nameof(HasCondition))]
        [HorizontalGroup("Split", 180)]
        [HorizontalGroup("Split/Left")]
        private NumberValueComparisonRule ResponseIfNumberComparison;

        [LabelText("Response if"), SerializeField, Indent, ShowIf("@ValidateIfComparableBool() && !UseRefValueForComparison()"),
         EnableIf(nameof(HasCondition))]
        [HorizontalGroup("Split/Right")]
        private BoolValueComparisonRule CompareToValueBool;
        
        [LabelText("Response if"), SerializeField, Indent, ShowIf("@ValidateIfComparableBool() && UseRefValueForComparison()"),
         EnableIf(nameof(HasCondition))]
        [HorizontalGroup("Split/Right")]
        private BoolRef CompareToValueBoolRef;

        [HideLabel, SerializeField, ShowIf("@ValidateIfComparableNumber() && Reference is FloatRef && !UseRefValueForComparison()"),
         EnableIf(nameof(HasCondition))]
        [HorizontalGroup("Split/Right")]
        private float CompareToValueFloat;
        
        [HideLabel, SerializeField, ShowIf("@ValidateIfComparableNumber() && Reference is FloatRef && UseRefValueForComparison()"),
         EnableIf(nameof(HasCondition))]
        [HorizontalGroup("Split/Right")]
        private FloatRef CompareToValueFloatRef;

        [HideLabel, SerializeField, Range(0f, 1f), ShowIf("@ValidateIfComparableNumber() && Reference is Float01Ref && !UseRefValueForComparison()"),
         EnableIf(nameof(HasCondition))]
        [HorizontalGroup("Split/Right")]
        private float CompareToValueFloat01;
        
        [HideLabel, SerializeField, ShowIf("@ValidateIfComparableNumber() && Reference is Float01Ref && UseRefValueForComparison()"),
         EnableIf(nameof(HasCondition))]
        [HorizontalGroup("Split/Right")]
        private Float01Ref CompareToValueFloat01Ref;

        [HideLabel, SerializeField, ShowIf("@ValidateIfComparableNumber() && Reference is IntRef && !UseRefValueForComparison()"),
         EnableIf(nameof(HasCondition))]
        [HorizontalGroup("Split/Right")]
        private int CompareToValueInt;
        
        [HideLabel, SerializeField, ShowIf("@ValidateIfComparableNumber() && Reference is IntRef && UseRefValueForComparison()"),
         EnableIf(nameof(HasCondition))]
        [HorizontalGroup("Split/Right")]
        private IntRef CompareToValueIntRef;

        [SerializeField, Space] private UnityEvent OnReferenceChanged;
        
        [SerializeField, HideInInspector] private bool UsingRefValueForComparison;

//------Private Variables-------//
        private bool _isActive;
        private int ValueToBeComparedInt=> UsingRefValueForComparison ? CompareToValueIntRef.Value : CompareToValueInt;
        private float ValueToBeComparedFloat=> UsingRefValueForComparison ? CompareToValueFloatRef.Value : CompareToValueFloat;
        private float ValueToBeComparedFloat01=> UsingRefValueForComparison ? CompareToValueFloat01Ref.Value : CompareToValueFloat01;
        private bool ValueToBeComparedBool=> UsingRefValueForComparison ? CompareToValueBoolRef.Value : CompareToValueBool == BoolValueComparisonRule.True;


#region UNITY_METHODS

        private void Awake()
        {
            if(WaitUntilStart)
                return;
            _isActive = true;
        }

        private void OnEnable()
        {
            Reference.OnValueChanged += ResponseToValueChange;
        }

        private void OnDisable()
        {
            Reference.OnValueChanged -= ResponseToValueChange;
        }

        private void Start()
        {
            if(!WaitUntilStart)
                return;
            StartCoroutine(WaitForNextFrameToActivate());
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        private IEnumerator WaitForNextFrameToActivate()
        {
            yield return new WaitForEndOfFrame();
            _isActive = true;
        }

        private void ResponseToValueChange()
        {
            if(!_isActive)
                return;
            if (ValidateIfComparable() && HasCondition && !GetComparisonResult())
                return;
            OnReferenceChanged?.Invoke();
        }

        private bool GetComparisonResult()
        {
            if (ValidateIfComparableNumber())
            {
                switch (Reference)
                {
                    case IntRef intRef:
                        return ResponseIfNumberComparison switch
                        {
                            NumberValueComparisonRule.LessThan => intRef.Value < ValueToBeComparedInt,
                            NumberValueComparisonRule.LessOrEqual => intRef.Value <= ValueToBeComparedInt,
                            NumberValueComparisonRule.EqualTo => intRef.Value == ValueToBeComparedInt,
                            NumberValueComparisonRule.GreaterOrEqual => intRef.Value >= ValueToBeComparedInt,
                            NumberValueComparisonRule.GreaterThan => intRef.Value > ValueToBeComparedInt,
                            _ => false
                        };
                    case FloatRef floatRef:
                        return ResponseIfNumberComparison switch
                        {
                            NumberValueComparisonRule.LessThan => floatRef.Value < ValueToBeComparedFloat,
                            NumberValueComparisonRule.LessOrEqual => floatRef.Value <= ValueToBeComparedFloat,
                            NumberValueComparisonRule.EqualTo => Mathf.Approximately(floatRef.Value,
                                ValueToBeComparedFloat),
                            NumberValueComparisonRule.GreaterOrEqual => floatRef.Value >= ValueToBeComparedFloat,
                            NumberValueComparisonRule.GreaterThan => floatRef.Value > ValueToBeComparedFloat,
                            _ => false
                        };
                    case Float01Ref float01Ref:
                        return ResponseIfNumberComparison switch
                        {
                            NumberValueComparisonRule.LessThan => float01Ref.Value < ValueToBeComparedFloat01,
                            NumberValueComparisonRule.LessOrEqual => float01Ref.Value <= ValueToBeComparedFloat01,
                            NumberValueComparisonRule.EqualTo => Mathf.Approximately(float01Ref.Value,
                                ValueToBeComparedFloat01),
                            NumberValueComparisonRule.GreaterOrEqual => float01Ref.Value >= ValueToBeComparedFloat01,
                            NumberValueComparisonRule.GreaterThan => float01Ref.Value > ValueToBeComparedFloat01,
                            _ => false
                        };
                }
            }
            else if (ValidateIfComparableBool())
            {
                if (Reference is BoolRef boolRef)
                    return boolRef.Value == ValueToBeComparedBool;
            }

            return false;
        }

        private bool ValidateIfComparable()
        {
            return ValidateIfComparableBool() || ValidateIfComparableNumber();
        }

        private bool ValidateIfComparableNumber()
        {
            return Reference is IntRef or FloatRef or Float01Ref;
        }

        private bool ValidateIfComparableBool()
        {
            return Reference is BoolRef;
        }

        private bool UseRefValueForComparison()
        {
            return UsingRefValueForComparison;
        }

        [LabelText("@UseRefValueForComparison() ? \"Manual\" : \"Ref\""), HorizontalGroup("Split/Right/Edge", Width = 55), Button]
        private void ToggleRefValueComparison()
        {
            UsingRefValueForComparison = !UsingRefValueForComparison;
        }

#endregion
    }
}