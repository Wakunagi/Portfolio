using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace program {
    namespace monster {
        public class MonsterGSSReader : GSSReader {

            protected override void SetUp() {
                pr_ScriptsGetter.pr_UIButtonListMaker.Make_MonsterButtonList();
            }
            protected override void ResetData() {
                pr_ScriptsGetter.pr_CardList.ResetMonsterList();
            }

            //各データを加工して保存
            protected override void SetData(string text) {
                MonsterData data = new MonsterData();
                data.id = int.Parse(StringReader(text, 1));
                data.name = StringReader(text, 2);
                data.furigana = StringReader(text, 3);
                data.level = int.Parse(StringReader(text, 4));
                data.attribute = StringReader(text, 5);
                data.atk = int.Parse(StringReader(text, 6));
                data.def = int.Parse(StringReader(text, 7));
                data.role = StringReader(text, 8);
                data.tribe = StringReader(text, 9);
                data.effect = StringReader(text, 10);

                pr_ScriptsGetter.pr_CardList.AddCardData(data);
            }
        }
    }
}