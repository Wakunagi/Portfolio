using UnityEngine;

namespace sikisiki.InGame.UI {
    public class Display_InfoPanel : MonoBehaviour {

        bool wasDisplay = false;

        private void OnEnable() {
            Time.timeScale = 0;
            if (!(StageMaker.StageNum == 0 && StageMaker.StageNum_wave == 0) && !wasDisplay) {
                gameObject.SetActive(false);
                wasDisplay = true;
            }
        }

        private void OnDisable() {
            Time.timeScale = 1;
        }
    }
}