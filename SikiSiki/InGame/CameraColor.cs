using System.Collections.Generic;
using UnityEngine;
using sikisiki.InGame.UI;
using sikisiki.GmaneSystem;

namespace sikisiki.InGame {
    public class CameraColor : MonoBehaviour {

        //各色の値
        public enum colorName {
            white, red, blue, yellow, parple, orange, green, black,
        }

        public Material MAcolor;
        [SerializeField] private List<Color> myColorList = new List<Color>();
        public static List<Color> colorList = new List<Color>();

        int colorListLen = 0;
        public int colorNum { get; private set; } = 0;      //色の合計（見えている色）
        public int mainColorNum { get; private set; } = 1;  //メインカラー（Playerが操作できる色）
        public int sabColorNum { get; private set; } = 0;   //サブカラー（カラースペースなどで変更される色）
        public bool isUnColor { get; private set; } = false;
        public static int colorChangeCounter { get; private set; } = 0;

        [SerializeField] private UIManager UIProg;

        public void SetSabColorNum(int num) {
            sabColorNum = num;
            Debug.Log("scNum : " + sabColorNum);
            ColorNumberChanger(0, 1);
        }

        private void Awake() {
            colorList = myColorList;
        }

        private void OnEnable() {
            Debug.Log("HPisFirstLoad:" + ScenChanger.isFirstLoad);
            if (ScenChanger.isFirstLoad) colorChangeCounter = 0;
        }
        void Start() {
            colorListLen = myColorList.Count;
            ColorNumberChanger(0, 0);
        }

        void Update() {

            if (!UIProg.isColorChanging) {

                bool set_upColor = Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W),
                     set_downColor = Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S),
                     setColor = Input.GetKeyUp(KeyCode.Space);

                if (set_upColor) { ColorNumberChanger(1, 0); }
                if (set_downColor) { ColorNumberChanger(-1, 0); }
                if (setColor) ColorNumberChanger(0, 0);
            }
        }

        void ColorNumberChanger(int mainColor, int sabColor) {
            if (!(mainColor == 0 && sabColor == 0) && !isUnColor) colorChangeCounter += 10;
            if (mainColor == 0 && sabColor == 0) {
                isUnColor = !isUnColor;
                colorChangeCounter += 5;
                UIProg.UIColorChanger(mainColor, true);
            }
            else if (mainColor == 0 && sabColor == 1) {
                isUnColor = false;
            }
            //Debug.Log("CCcount:" + colorChangeCounter);
            if (mainColor == 0 && isUnColor && sabColorNum <= 0) colorNum = 0;
            else if (mainColor == 0 && isUnColor && sabColorNum >= 0) colorNum = sabColorNum;
            else if (!isUnColor) {
                mainColorNum += mainColor;
                mainColorNum = ((mainColorNum + 2) % 3) + 1;

                if (mainColorNum == sabColorNum) colorNum = mainColorNum;
                else if (sabColorNum > 0) colorNum = mainColorNum + sabColorNum + 1;
                else colorNum = mainColorNum;

                if (colorNum >= colorListLen - 1) colorNum = colorListLen - 1;
                UIProg.UIColorChanger(mainColor, false);
            }
            SetColor();
        }

        //カメラに変更を適用
        void SetColor() {
            this.MAcolor.SetFloat("_red", myColorList[colorNum].r);
            this.MAcolor.SetFloat("_green", myColorList[colorNum].g);
            this.MAcolor.SetFloat("_blue", myColorList[colorNum].b);
        }

        void OnRenderImage(RenderTexture src, RenderTexture dest) {
            Graphics.Blit(src, dest, MAcolor);
        }
    }
}