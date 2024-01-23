using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace program {
    [CreateAssetMenu(menuName = "ScriptableObjects/MonsterList ")]
    public class so_CardList : ScriptableObject {

        //カードのデータ
        [field: SerializeField] public List<MonsterData> monsterDatas { private set; get; } = new List<MonsterData>();
        [field: SerializeField] public List<MagicData> magicDatas { private set; get; } = new List<MagicData>();

        //モンスターのステータスの最大値
        [field: SerializeField] public int maxLevel { private set; get; } = 0;  /* レベル */
        [field: SerializeField] public int maxAtk { private set; get; } = 0;    /* 攻撃力 */
        [field: SerializeField] public int maxDef { private set; get; } = 0;    /* 守備力 */

        //カードのタイプ
        [field: SerializeField] public List<string> CardRoleDatas { private set; get; } = new List<string>();           /* 役割 */
        [field: SerializeField] public List<string> MonsterAttributeDatas { private set; get; } = new List<string>();   /* モンスターの属性 */
        [field: SerializeField] public List<string> MonsterTribeDatas { private set; get; } = new List<string>();       /* モンスターの種族 */
        [field: SerializeField] public List<string> MagicAttributeDatas { private set; get; } = new List<string>();     /* 魔法の属性 */

        //モンスターの属性の色情報
        [field: SerializeField] public List<Color> MonsterAttribute_ColorDatas { private set; get; } = new List<Color>();


        //カードデータのリセット関数
        public void ResetMonsterList() { monsterDatas = new List<MonsterData>(); }
        public void ResetMagicList() { magicDatas = new List<MagicData>(); }

        //カードタイプのリセット関数
        public void ResetAllCardTypeList() {
            CardRoleDatas = new List<string>();
            MonsterAttributeDatas = new List<string>();
            MonsterTribeDatas = new List<string>();
            MagicAttributeDatas = new List<string>();
        }


        //カードデータの追加関数
        public void AddCardData(MonsterData monsterData) { monsterDatas.Add(monsterData); }
        public void AddCardData(MagicData magicData) { magicDatas.Add(magicData); }

        //モンスターのステータスの最大値の追加関数
        public void SetMaxLevel(int max) { maxLevel = max; }
        public void SetMaxAtk(int max) { maxAtk = max; }
        public void SetMaxDef(int max) { maxDef = max; }

        //カードタイプの追加関数
        public void AddCardRole(string type) { CardRoleDatas.Add(type); }
        public void AddMonsterAttribute(string type) { MonsterAttributeDatas.Add(type); }
        public void AddMonsterTribe(string type) { MonsterTribeDatas.Add(type); }
        public void AddMagicAttribute(string type) { MagicAttributeDatas.Add(type); }


        //モンスターの属性から色を取得
        public Color GetAttributeColor(string atb) {
            Color color = Color.white;
            for (int i = 0; i < (int)AttributeData.end; i++) {
                if (atb == MonsterAttributeDatas[i]) {
                    color = MonsterAttribute_ColorDatas[i]; break;
                }
            }
            return color;
        }

        //IDから名前を取得
        public string GetCardName(int id) {
            foreach (MonsterData data in monsterDatas) {
                if (id == data.id) return data.name;
            }

            foreach (MagicData data in magicDatas) {
                if (id == data.id) return data.name;
            }

            return null;
        }

        //IDからモンスターを取得
        public MonsterData MonsterInID(int id) {
            foreach (MonsterData data in monsterDatas) {
                if (id == data.id) return data;
            }
            return null;
        }

        //IDから魔法を取得
        public MagicData MagicInID(int id) {
            foreach (MagicData data in magicDatas) {
                if (id == data.id) return data;
            }
            return null;
        }


        //タグからモンスターのデータを返す
        public string MonsterDataGetter(MonsterData data, CardTag tag) {
            switch (tag) {
                case CardTag.attribute: return data.attribute;
                case CardTag.tribe: return data.tribe;
                case CardTag.role: return data.role;
                case CardTag.level: return data.level.ToString();
                case CardTag.atk: return data.atk.ToString();
                case CardTag.def: return data.def.ToString();
                case CardTag.name: return data.name;
                case CardTag.effect: return data.effect;
                default: return null;
            }
        }

        //タグから魔法のデータを返す
        public string MagicDataGetter(MagicData data, CardTag tag) {
            switch (tag) {
                case CardTag.attribute: return data.attribute;
                case CardTag.role: return data.role;
                case CardTag.name: return data.name;
                case CardTag.effect: return data.effect;
                default: return null;
            }
        }

        //タグからStringのデータを渡す
        public List<string> MonsterCardTagDataGetter(CardTag tag) {
            switch (tag) {
                case CardTag.attribute: return MonsterAttributeDatas;
                case CardTag.role: return CardRoleDatas;
                case CardTag.tribe: return MonsterTribeDatas;
                default: return null;
            }
        }


        //タグからMaxを渡す
        public int MonsterStatusDataGetter(CardTag tag) {
            switch (tag) {
                case CardTag.level: return maxLevel;
                case CardTag.atk: return maxAtk;
                case CardTag.def: return maxDef;
                default: return -1;
            }
        }
    }

    public enum MonsterCardData {
        Name, Furigana, Level, Attribute, Atk, Def, Type, Race, Effect, Count, end
    }

    public enum MagicCardData {
        Name, Furigana, Attribute, Type, Effect, Count, end
    }
    public enum AttributeData {
        Fire, Water, Wind, Ground, Light, Dark, end
    }

    public enum CardTag {
        attribute, tribe, role, level, atk, def, name, effect, end
    }

    public enum CardClass {
        monster, magic, end,
    }

}