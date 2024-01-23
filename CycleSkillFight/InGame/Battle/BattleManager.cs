using Program.GameSystem.Data;
using Program.InGame.Skill;
using Program.OutGame.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Program.InGame.Battle{
    public class BattleManager :MonoBehaviour {

     [field:SerializeField]   public AnyPhase phase { private set; get; } = AnyPhase.PreparationPhase;
        int turn = 0;

        int decision_count = 0;
        int order_count = 0;

        [SerializeField] FighterSkillSetting[] fighters = new FighterSkillSetting[MyConst.PLAYER_MAX];

        int fighters_count = 0;
        [SerializeField]int[] using_order;

        public WinLose[] win_lose { private set; get; } = new WinLose[MyConst.PLAYER_MAX];

        bool isCallBack = false;
        public void CallBack() { Debug.Log("CalllBack"); isCallBack = true; }

        [SerializeField] Text phase_text,turn_text;
        [SerializeField] float phaseChange_animation_time = 1.2f;

        void CameraPosChanger() {

            float pos_avg = 0, pos_y = 0;
            for (int i = 0; i < fighters.Length; i++) pos_avg += fighters[i].status.pos;

            for (int i = 0; i < fighters.Length; i++) fighters[i].status.pos -= (int)pos_avg;

            pos_avg = 0;
            for (int i = 0; i < fighters.Length; i++) pos_avg += fighters[i].status.pos;

            pos_avg /= fighters.Length;

            pos_y = math.abs(fighters[0].status.pos - pos_avg);
            Camera.main.transform.position = new Vector3(pos_avg, pos_y, -10);
            Camera.main.orthographicSize = pos_y + 0.5f;
        }

        //ファイターのステータスを設定&番号を割り当て
        public int SetFighterStatus(FighterStatus status) {
            int num = fighters_count;
            fighters_count++;

            win_lose[num] = WinLose.End;
            fighters[num] = new FighterSkillSetting();
            fighters[num].me = num;
            fighters[num].enemy = ElseFighterGetter(num);
            fighters[num].status = status;
            return num;
        }

        public void PhaseSetUP(int count) {
            decision_count += count;

            if (decision_count == MyConst.PLAYER_MAX) {
                decision_count = 0;
                PreparationPhase();
            }
        }

        //フェイズの切り替え
        public void PhaseChange(int count) {
            decision_count += count;

            if(decision_count == MyConst.PLAYER_MAX) {
                PhaseChange();
            }
        }

        //フェイズの切り替え(内部)
        public void PhaseChange() {
            phase++;
            if (phase == AnyPhase.End) phase = AnyPhase.PreparationPhase;

            decision_count = 0;
            order_count = 0;

            if (phase == AnyPhase.PreparationPhase) PreparationPhase();
            else if (phase == AnyPhase.BattlePhase) BattlePhase();
            else if (phase == AnyPhase.EndPhase) EndPhase();
        }

        void PreparationPhase() {
            turn++;
            turn_text.text = turn.ToString();
            StartCoroutine(PreparationPhase_Animation());
        }

        IEnumerator PreparationPhase_Animation() {

            float c_alpha = phaseChange_animation_time;
            phase_text.text = "TURN " + turn;
            while (true) {
                c_alpha -= Time.deltaTime;
                Color white = new Color(1, 1, 1, c_alpha);
                phase_text.color = white;

                if (c_alpha <= 0) break;
                yield return null;
            }
            PhaseChange();
        }

        //スキルの選択
        public void SelectSkill(SkillParent skill , int fNum) {
            fighters[fNum].skill = skill;
            fighters[fNum].order = order_count;
            order_count++;
        }

        //スキルのキャンセル
        public void SelectCancel(int fNum) {
            fighters[fNum].order = order_count;
            order_count++;
        }

        //バトルフェイズ処理
        public void BattlePhase() { 
            StartCoroutine(BattlePhase_Animation());
        }

        IEnumerator BattlePhase_Animation() {

            float c_alpha = phaseChange_animation_time;
            phase_text.text = "BATTLE!";
            while (true) {
                c_alpha -= Time.deltaTime;
                Color white = new Color(1, 1, 1, c_alpha);
                phase_text.color = white;

                if (c_alpha <= 0) break;
                yield return null;
            }

            using_order = new int[MyConst.PLAYER_MAX];

            for (int i = 0; i < MyConst.PLAYER_MAX; i++) using_order[i] = fighters[i].me;

            //スキル使用順序の変更
            for (int i = MyConst.PLAYER_MAX - 1; i > 0; i--) {
                for (int j = 0; j < i; j++) {
                    FighterSkillSetting j0 = fighters[j];
                    FighterSkillSetting j1 = fighters[j + 1];
                    if (j0.skill.priority < j1.skill.priority) change();
                    else
                    if (j0.skill.priority == j1.skill.priority) {
                        if (j0.status.spd < j1.status.spd) change();
                        else
                        if (j0.status.spd == j1.status.spd) {
                            if (j0.order > j1.order) change();
                        }
                    }
                    void change() {
                        using_order[j] = fighters[j + 1].me;
                        using_order[j + 1] = fighters[j].me;
                    }
                }
            }

            //スキルの使用
            for (int i = 0; i < MyConst.PLAYER_MAX; i++) {
                FighterSkillSetting fighter = fighters[using_order[i]];

                isCallBack = false;
                fighter.skill.Ability(fighters[fighter.me].status, fighters[fighter.enemy].status);
                
                while(!isCallBack)yield return null;

                if (fighters[fighter.me].status.life < 0) {
                    win_lose[fighter.me] = WinLose.Lose;
                    win_lose[fighter.enemy] = WinLose.Win;
                    phase = AnyPhase.End;
                   yield break;
                }
                if (fighters[fighter.enemy].status.life < 0) {
                    win_lose[fighter.me] = WinLose.Win;
                    win_lose[fighter.enemy] = WinLose.Lose;
                    phase = AnyPhase.End;
                    yield break;
                }
            }
            PhaseChange();
        }

        //エンドフェイズ処理
        public void EndPhase() {
            //スキルの使用
            for (int i = 0; i < MyConst.PLAYER_MAX; i++) {
                FighterSkillSetting fighter = fighters[using_order[i]];
                fighter.skill.EndPhase(fighters[fighter.me].status, fighters[fighter.enemy].status);
                fighter.status.isDamaged = false;
            }
            CameraPosChanger();
        }

        //敵プレイヤーを取得
        public int ElseFighterGetter(int num) {
            if (num == 0) return 1;
            else if (num == 1) return 0;
            else return -1;
        }

        [System.Serializable]
        public class FighterSkillSetting {
            public FighterStatus status = null;
            public SkillParent skill = null;
            public int me = 0, enemy = 0;
            public int order = 0;
        }

    }
}