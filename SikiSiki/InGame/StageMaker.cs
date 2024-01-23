using UnityEngine;
using sikisiki.Databese;
using sikisiki.GmaneSystem;

namespace sikisiki.InGame {
    public class StageMaker : MonoBehaviour {

        public static int StageNum = 0;     //���X�e�[�W�ڂ�
        public static int StageNum_wave = 0;  //���E�F�[�u�ڂ�
        [SerializeField] private StageDatabase sl;

        private void Awake() {
            GameObject go = Instantiate(sl.SetStageList(StageNum)[StageNum_wave]);

            Debug.Log("StageisFirstLoad:" + ScenChanger.isFirstLoad);
        }

        private void Start() {
            if (ScenChanger.isFirstLoad) { 
                Debug.Log("Stage2isFirstLoad:" + ScenChanger.isFirstLoad); 
                ScenChanger.isFirstLoad = false;
            }
        }

    }
}