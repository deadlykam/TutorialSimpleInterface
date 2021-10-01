using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.Events;

namespace KamranWali.SimpleInterface.Editor.Layouts
{
    public class CustomLayout : BaseLayout
    {
        private AnimBool _customGroup;
        private float _value;

        public CustomLayout(UnityAction repaint) : base(repaint)
        {
        }

        public override void Hide() { if (IsShown()) _customGroup.target = false; }
        public override bool IsShown() => _customGroup.target;

        public override void SetupOnGUI()
        {
            _customGroup.target = ToggleLeft("Custom Layout (F)", "Toggle to place prefab in given fixed Y axis position. Hotkey = 'F'", _customGroup);

            if (BeginFadeGroup(_customGroup.faded)) _value = FloatField(_value);
            EndFadeGroup();
            HideOtherLayouts();
        }

        public override void Update(Event currentEvent)
        {
            if(currentEvent.keyCode == KeyCode.F && currentEvent.type == EventType.KeyDown)
            {
                _customGroup.target = !_customGroup.target;
                HideOtherLayouts();
            }
        }

        public override Vector3 GetPosition(Vector3 position)
        {
            position.Set(position.x, _value, position.z);
            return position;
        }

        protected override void SetupOnEnable()
        {
            _customGroup = new AnimBool(false);
            _customGroup.valueChanged.AddListener(repaint);
        }
    }
}