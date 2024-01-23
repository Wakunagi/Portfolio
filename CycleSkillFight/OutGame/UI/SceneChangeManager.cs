using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Program.GameSystem.Data;

namespace Program.OutGame.UI {
    public class SceneChangeManager : MonoBehaviour{
        [SerializeField] int decision_count = 0;

        [SerializeField] private string title= "TitleScenes";
        [SerializeField] private string scene;

        [SerializeField] float t = 0;
        [SerializeField] private float time_max=300;

        private void Update() {
            t += Time.deltaTime;
            if(t>time_max)
                SceneManager.LoadScene(title);
        }

        public void SceneChanger(int decision) {
            decision_count += decision;

            if(decision_count == MyConst.PLAYER_MAX) {
                Debug.Log("change!");
                SceneManager.LoadScene(scene);
            }
        }

        public void TimeReset() {
            t = 0;
        }

    }
}