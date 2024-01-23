using program.magic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace program {
    public class CardTypeGSSReader : GSSReader {

        protected override void SetUp() {

            //ステータスの最大値の変更
            foreach (MonsterData data in pr_ScriptsGetter.pr_CardList.monsterDatas) {
                if (data.level > pr_ScriptsGetter.pr_CardList.maxLevel) pr_ScriptsGetter.pr_CardList.SetMaxLevel(data.level);
                if (data.atk > pr_ScriptsGetter.pr_CardList.maxAtk) pr_ScriptsGetter.pr_CardList.SetMaxAtk(data.atk);
                if (data.def > pr_ScriptsGetter.pr_CardList.maxDef) pr_ScriptsGetter.pr_CardList.SetMaxDef(data.def);
            }

            //検索ボタンの作成関数
            pr_ScriptsGetter.pr_UIButtonListMaker.Make_TagButtonList();
        }

        protected override void ResetData() {
            pr_ScriptsGetter.pr_CardList.ResetAllCardTypeList();
        }

        protected override void SetData(string data) {

            string textData = "";
            if ((textData = StringReader(data, 1)) != "") pr_ScriptsGetter.pr_CardList.AddCardRole(textData);
            if ((textData = StringReader(data, 2)) != "") pr_ScriptsGetter.pr_CardList.AddMonsterAttribute(textData);
            if ((textData = StringReader(data, 3)) != "") pr_ScriptsGetter.pr_CardList.AddMonsterTribe(textData);
            if ((textData = StringReader(data, 4)) != "") pr_ScriptsGetter.pr_CardList.AddMagicAttribute(textData);


        }
    }
}