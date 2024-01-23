using UnityEngine;
using UnityEngine.UI;
using sikisiki.GmaneSystem;
using System.Linq;

namespace sikisiki.InGame.UI {
    public class UIManager : MonoBehaviour {
        [SerializeField] private PlayerController playerProg;
        [SerializeField] private Text HPNumText;

        float myTime = 0;
        public static float stageTime = 0;
        [SerializeField] private Text[] TimeText = new Text[4];

        [SerializeField] private Image[] colorName = new Image[3];
        int[] imagePosList = new int[3];
        [SerializeField] private Vector3[] colorNamePos = new Vector3[3], colorNameScale = new Vector3[3];

        [SerializeField] private Image[] HereIm = new Image[3];
        [SerializeField] private Image NowIm;

        [SerializeField] private GameObject Im;

        [SerializeField] private CameraColor ccProg;

        private void OnEnable() {
            Debug.Log("UIisFirstLoad:" + ScenChanger.isFirstLoad);
            if (!ScenChanger.isFirstLoad) myTime = stageTime;
        }

        // Start is called before the first frame update
        void Start() {
            int i = 0;
            foreach (Image c in colorName) {
                colorNamePos[i] = c.rectTransform.position;
                colorNameScale[i] = c.rectTransform.localScale;
                i++;
            }
            imagePosList[0] = 0;
            imagePosList[1] = 1;
            imagePosList[2] = 2;
            NowIm.rectTransform.position = HereIm[StageMaker.StageNum_wave].rectTransform.position;
        }

        // Update is called once per frame
        void Update() {
            HPNumText.text = playerProg.hp.ToString();
            TimeSet();
        }

        public bool isColorChanging { get; private set; } = false;
        public void UIColorChanger(int mainColorNum, bool isColorDisplay) {

            Im.SetActive(isColorDisplay);
            if (isColorDisplay) return;

            for (int i = 0; i < colorName.Length; i++) {
                imagePosList[i] = (imagePosList[i] - mainColorNum + 3) % 3;
                colorName[i].rectTransform.position = colorNamePos[imagePosList[i]];
                colorName[i].rectTransform.localScale = colorNameScale[imagePosList[i]];
            }
        }

        void TimeSet() {
            myTime += Time.deltaTime;
            TimeText[0].text = ((int)(myTime / 600 % 6)).ToString();
            TimeText[1].text = ((int)(myTime / 60 % 10)).ToString();
            TimeText[2].text = ((int)(myTime / 10 % 6)).ToString();
            TimeText[3].text = ((int)(myTime % 10)).ToString();
            stageTime = myTime;
        }


        [SerializeField] private GameObject endPnel;
        [SerializeField] private Image[] star;
        [SerializeField] private Text clearText, scoreText;
        int score = 0;

        public void IsEnd(bool isClear) {
            endPnel.SetActive(true);
            Time.timeScale = 0;

            float myscore = (playerProg.hp / myTime) / CameraColor.colorChangeCounter * 11741730 * (StageMaker.StageNum + 1) / 10;
            scoreText.text = "SCORE  " + (int)myscore;

            if (isClear) {
                clearText.text = "SUCCEED";
                score++;
                if (playerProg.hp == playerProg.oldHp) score++;
                if (myTime < 180) score++;
                int i = 0;
                for (i = 0; i < score; i++) {
                    if (star[i] != null) star[i].gameObject.SetActive(true);
                }

            }
            else clearText.text = "FAILED";

            Debug.Log("isEnd");
        }
    }
}