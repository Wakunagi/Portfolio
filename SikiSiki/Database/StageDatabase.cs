using System.Collections.Generic;
using UnityEngine;

namespace sikisiki.Databese {

    [CreateAssetMenu(menuName = "ScriptableObject/StageList")]
    public class StageDatabase : ScriptableObject {
        [SerializeField]
        private List<StageData> StageDataList = new List<StageData>();

        [System.SerializableAttribute]
        public class StageData {
            public List<GameObject> Data = new List<GameObject>();
        }

        public List<GameObject> SetStageList(int n) {
            List<GameObject> list = StageDataList[n].Data;
            return list;

        }
    }
}