using DG.Tweening;
using ReferenceValue;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableEvent
{
    [AddComponentMenu("Events/Game Event Listener")]
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField, Required] private GameEvent Event;

        [SerializeField] private bool UseDelay;

        [SerializeField, Indent, EnumToggleButtons, ShowIf(nameof(UseDelay))]
        private FloatOrFloatRef DelayType;

        [SerializeField, Indent, MinValue(0f), SuffixLabel("sec"), ShowIf("@UseDelay && DelayType == FloatOrFloatRef.Float")]
        private float FloatDelay;

        [SerializeField, Indent,  SuffixLabel("sec"), ShowIf("@UseDelay && DelayType == FloatOrFloatRef.RefValue")]
        private FloatRef RefDelay;

        [SerializeField] private bool HasCondition;

        [SerializeField, ShowIf(nameof(HasCondition))]
        private RefValue Reference;

        [LabelText("Response if"), LabelWidth(80), Indent, SerializeField, ShowIf(nameof(ValidateIfComparableNumber)),
         EnableIf(nameof(HasCondition))]
        [HorizontalGroup("Split", .6f)]
        [HorizontalGroup("Split/Left")]
        private NumberValueComparisonRule ResponseIfNumberComparison;

        [LabelText("Response if"), SerializeField, Indent, ShowIf(nameof(ValidateIfComparableBool)),
         EnableIf(nameof(HasCondition))]
        private BoolValueComparisonRule CompareToValueBool;

        [HideLabel, SerializeField, ShowIf("@ValidateIfComparableNumber() && Reference is FloatRef"),
         EnableIf(nameof(HasCondition))]
        [HorizontalGroup("Split/Right")]
        private float CompareToValueFloat;

        [HideLabel, SerializeField, Range(0f, 1f), ShowIf("@ValidateIfComparableNumber() && Reference is Float01Ref"),
         EnableIf(nameof(HasCondition))]
        [HorizontalGroup("Split/Right")]
        private float CompareToValueFloat01;

        [HideLabel, SerializeField, ShowIf("@ValidateIfComparableNumber() && Reference is IntRef"),
         EnableIf(nameof(HasCondition))]
        [HorizontalGroup("Split/Right")]
        private int CompareToValueInt;

        [SerializeField] private UnityEvent Response;

#if UNITY_EDITOR
        private void Awake()
        {
            Event.Initialize();
        }
#endif

        public void OnEventRaised()
        {
            GiveResponse();
        }

        private void GiveResponse()
        {
            if (ValidateIfComparable() && HasCondition && !GetComparisonResult())
                return;
            if (!UseDelay)
                Response?.Invoke();
            else
            {
                var delay = DelayType == FloatOrFloatRef.Float ? FloatDelay : RefDelay.Value;
                DOVirtual.DelayedCall(delay, () => Response?.Invoke());
            }
        }

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
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
                            NumberValueComparisonRule.LessThan => intRef.Value < CompareToValueInt,
                            NumberValueComparisonRule.LessOrEqual => intRef.Value <= CompareToValueInt,
                            NumberValueComparisonRule.EqualTo => intRef.Value == CompareToValueInt,
                            NumberValueComparisonRule.GreaterOrEqual => intRef.Value >= CompareToValueInt,
                            NumberValueComparisonRule.GreaterThan => intRef.Value > CompareToValueInt,
                            _ => false
                        };
                    case FloatRef floatRef:
                        return ResponseIfNumberComparison switch
                        {
                            NumberValueComparisonRule.LessThan => floatRef.Value < CompareToValueFloat,
                            NumberValueComparisonRule.LessOrEqual => floatRef.Value <= CompareToValueFloat,
                            NumberValueComparisonRule.EqualTo => Mathf.Approximately(floatRef.Value,
                                CompareToValueFloat),
                            NumberValueComparisonRule.GreaterOrEqual => floatRef.Value >= CompareToValueFloat,
                            NumberValueComparisonRule.GreaterThan => floatRef.Value > CompareToValueFloat,
                            _ => false
                        };
                    case Float01Ref float01Ref:
                        return ResponseIfNumberComparison switch
                        {
                            NumberValueComparisonRule.LessThan => float01Ref.Value < CompareToValueFloat01,
                            NumberValueComparisonRule.LessOrEqual => float01Ref.Value <= CompareToValueFloat01,
                            NumberValueComparisonRule.EqualTo => Mathf.Approximately(float01Ref.Value,
                                CompareToValueFloat01),
                            NumberValueComparisonRule.GreaterOrEqual => float01Ref.Value >= CompareToValueFloat01,
                            NumberValueComparisonRule.GreaterThan => float01Ref.Value > CompareToValueFloat01,
                            _ => false
                        };
                }
            }
            else if (ValidateIfComparableBool())
            {
                if (Reference is BoolRef boolRef)
                    return boolRef.Value == (CompareToValueBool == BoolValueComparisonRule.True);
            }

            return false;
        }

        private bool ValidateIfComparable()
        {
            return ValidateIfComparableBool() || ValidateIfComparableNumber();
        }

        private bool ValidateIfComparableNumber()
        {
            return Reference is IntRef || Reference is FloatRef || Reference is Float01Ref;
        }

        private bool ValidateIfComparableBool()
        {
            return Reference is BoolRef;
        }
    }
}