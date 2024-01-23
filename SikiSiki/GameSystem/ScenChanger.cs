using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using sikisiki.InGame;

namespace sikisiki.GmaneSystem {
    public class ScenChanger : MonoBehaviour {
        [SerializeField] private int myStageNum = 0;
        [SerializeField] private Text myText;
        [SerializeField] private Text StageNameText, PleaseClickText;
        [SerializeField] private string[] StageName = new string[3];
        public static bool isFirstLoad;
        [SerializeField] private RectTransform myRect;
        Vector2 mySD;
        [SerializeField] private bool isFirstStage = false, isRitry = false, isReturn = false, isStageSelect = false;
        private void Awake() {
            if (myText != null) myText.text = myStageNum.ToString();
            if (isStageSelect && GameObject.Find("StageNameText").GetComponent<Text>() != null) StageNameText = GameObject.Find("StageNameText").GetComponent<Text>();
            if (myRect != null) mySD = myRect.sizeDelta;
        }

        public void OnClickThisButton(string SceneName) {
            Time.timeScale = 1;

            if (isFirstStage) { isFirstLoad = true; StageMaker.StageNum_wave = 0; }
            else isFirstLoad = false;
            Debug.Log("--------------------------------------------------------\n" + "isFirstLoad:" + isFirstLoad);
            if (isRitry || isReturn) { SceneManager.LoadScene(SceneName); return; }

            StageMaker.StageNum = myStageNum - 1;
            StageMaker.StageNum_wave = 0;
            SceneManager.LoadScene(SceneName);
        }

        public void OnStayThisButton() {
            PleaseClickText.gameObject.SetActive(true);
            if (myRect != null) myRect.sizeDelta = mySD * 1.1f;
            if (StageNameText != null && StageName.Length == 3) StageNameText.text = StageName[0] + "\n\n" + StageName[1] + "\n\n" + StageName[2];
        }
        public void OnExitThisButton() {
            PleaseClickText.gameObject.SetActive(false);
            if (myRect != null) myRect.sizeDelta = mySD;
        }
    }
}