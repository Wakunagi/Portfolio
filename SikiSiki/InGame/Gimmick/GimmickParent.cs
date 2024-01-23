using UnityEngine;

namespace sikisiki.InGame.Gimmick {
    public class GimmickParent : MonoBehaviour {
        [SerializeField] protected CameraColor.colorName myColorName;
        int myColorNum;
        Camera myCamera;
        CameraColor myCameraColor;
        public bool isColorTrue { private set; get; } = false;

        public IsAffectedByButton myIABB { private set; get; } = null;

        // Start is called before the first frame update
        void Start() {
            myCamera = Camera.main;
            if (myCamera.GetComponent<CameraColor>() != null) myCameraColor = myCamera.GetComponent<CameraColor>();
            myColorNum = (int)myColorName;
            if (GetComponent<IsAffectedByButton>() != null) myIABB = GetComponent<IsAffectedByButton>();
            SabStart();
        }

        void Update() {
            SabUpdate();
            if (myIABB != null && myIABB.isAffected) {
                TrueColor();
                if (myColorNum == myCameraColor.colorNum) isColorTrue = true;
                else isColorTrue = false;
            }
            else {
                if (myColorNum == myCameraColor.colorNum ||
                    (!myCameraColor.isUnColor && (myColorNum == myCameraColor.mainColorNum || myColorNum == myCameraColor.sabColorNum))) {

                    TrueColor(); isColorTrue = true;
                }
                else { FalseColor(); isColorTrue = false; }
            }
        }

        protected virtual void SabStart() { }
        protected virtual void SabUpdate() { }

        protected virtual void TrueColor() { }
        protected virtual void FalseColor() { }


        protected void SkinColorSetter(GameObject[] objList) {
            if (GetComponent<SpriteRenderer>() != null)
                GetComponent<SpriteRenderer>().color = colorSetter(CameraColor.colorList[myColorNum]);

            foreach (GameObject obj in objList)
                if (obj.GetComponent<SpriteRenderer>() != null)
                    obj.GetComponent<SpriteRenderer>().color = CameraColor.colorList[myColorNum];
        }

        protected void SkinColorSetter() {
            if (GetComponent<SpriteRenderer>() != null)
                GetComponent<SpriteRenderer>().color = colorSetter(CameraColor.colorList[myColorNum]);
        }
        protected void SkinColorSetter(int i) {
            if (GetComponent<SpriteRenderer>() != null && i <= CameraColor.colorList.Count - 1 && i >= 0)
                GetComponent<SpriteRenderer>().color = colorSetter(CameraColor.colorList[i]);
        }

        protected void ObjectHideFanc(GameObject[] objList) {
            foreach (GameObject obj in objList) obj.SetActive(false);
        }
        protected void ObjectShowFanc(GameObject[] objList) {
            foreach (GameObject obj in objList) obj.SetActive(true);
        }

        Color colorSetter(Color color) {
            color.r = (color.r - 0.5f) * 0.4f + 0.5f;
            color.g = (color.g - 0.5f) * 0.4f + 0.5f;
            color.b = (color.b - 0.5f) * 0.4f + 0.5f;
            return color;
        }

    }
}