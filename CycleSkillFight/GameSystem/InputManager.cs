using UnityEngine;
using Program.GameSystem.Data;
using Program.OutGame.UI;

namespace Program.GameSystem {

    public class InputManager : MonoBehaviour {

        [SerializeField]protected SceneChangeManager sceneChangeManager;

        public void InputAnyButton(InputPattern pattern) {
            sceneChangeManager.TimeReset();
            InputButton(pattern);
            if (IsInputStick(pattern)) InputStick(pattern);
            if (IsInputArrow(pattern)) InputArrow(pattern);
            if (IsInputDecision(pattern)) InputDecision();
            if(IsInputCancel(pattern)) InputCancel();   
        }

        protected virtual void InputButton(InputPattern pattern) { }

        protected virtual void InputStick(InputPattern pattern) { }

        protected virtual void InputArrow(InputPattern pattern) { }

        protected virtual void InputDecision() { }

        protected virtual void InputCancel() { }


        //���͂��X�e�B�b�N���̔���
        public bool IsInputStick(InputPattern pattern) {
            if (pattern == InputPattern.Right ||
                pattern == InputPattern.Left ||
                pattern == InputPattern.Up ||
                pattern == InputPattern.Down
                )
                return true;
            return false;
        }

        //���͂��{�^�����̔���
        public bool IsInputArrow(InputPattern pattern) {
            if (pattern == InputPattern.RightArrow ||
                pattern == InputPattern.LeftArrow ||
                pattern == InputPattern.UpArrow ||
                pattern == InputPattern.DownArrow
                )
                return true;
            return false;
        }

        //���͂����艻�̔���
        public bool IsInputDecision(InputPattern pattern) {
            if (pattern == InputPattern.Decision) return true;
            return false;
        }

        //���͂��L�����Z�����̔���
        public bool IsInputCancel(InputPattern pattern) {
            if (pattern == InputPattern.Cancel) return true;
            return false;
        }
    }
}