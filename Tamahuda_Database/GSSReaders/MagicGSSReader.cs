using System.Collections.Generic;
using UnityEngine;

namespace program {
    namespace magic {
        public class MagicGSSReader : GSSReader {

            protected override void SetUp() {
                pr_ScriptsGetter. pr_UIButtonListMaker.Make_MagicButtonList();
            }
            protected override void ResetData() {
                pr_ScriptsGetter.pr_CardList.ResetMagicList();
            }

            //各データを加工して保存
            protected override void SetData(string text) {
                MagicData data = new MagicData();
                data.id = int.Parse(StringReader(text, 1));
                data.name = StringReader(text, 2);
                data.furigana = StringReader(text, 3);
                data.attribute = StringReader(text, 4);
                data.role = StringReader(text, 5);
                data.effect = StringReader(text, 6);

                pr_ScriptsGetter.pr_CardList.AddCardData(data);
            }
        }

    }
}