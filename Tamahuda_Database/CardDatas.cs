using System;

[Serializable]
public class CardDatas {
    public int id;          //ID

    public string name;     //名前
    public string furigana; //フリガナ
    public string role;     //役割（カードタイプ）
    public string effect;   //効果
}

[Serializable]
public class MonsterData : CardDatas {
    public int level;       //レベル
    public string attribute;//属性
    public string tribe;    //種族
    public int atk;         //攻撃力
    public int def;         //守備力
}

[Serializable]
public class MagicData : CardDatas {
    public string attribute;//魔法の種類
}

[Serializable]
public class DeckList {
    public Deck[] decks;
}

[Serializable]
public class Deck {
    public int user_id; //ユーザid
    public string pass; //パスワード

    public int id;      //内部ID
    public bool isOpen = false; //公開するか

    public string name; //デッキ名
    public string deck; //デッキ内容

    public string created;  //作成日
    public string updated;  //更新日
}

[Serializable]
public class UserData {
    public int id;
    public string name;
    public string pass;
}