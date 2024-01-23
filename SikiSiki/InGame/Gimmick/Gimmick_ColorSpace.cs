using UnityEngine;

namespace sikisiki.InGame.Gimmick {
    public class Gimmick_ColorSpace : GimmickParent {
        int isInsidePlayer = 0;
        public int myColor { get; private set; }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.tag == "Player") isInsidePlayer++;
        }
        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.gameObject.tag == "Player") isInsidePlayer--;
        }

        protected override void SabStart() {
            base.SabStart();
            isInsidePlayer = 0;
            SkinColorSetter();
            myColor = (int)myColorName;
        }
        protected override void SabUpdate() {
            base.SabUpdate();

            if (isInsidePlayer > 0) {
                SkinColorSetter(0);
            }
            else {
                SkinColorSetter();
            }
        }

    }
}