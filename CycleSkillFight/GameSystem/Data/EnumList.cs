namespace Program.GameSystem.Data {

    public class EnumList { }

    //�X�L���p���[
    public enum SkillPower {
        Low,
        Middle,
        High,
        End,
    }

    public enum AbilityType {
        Low,
        Middle,
        High,
        Charge,
        End,
    }

    //���̓p�^�[��
    public enum InputPattern {
        Right, Left, Up, Down,
        RightArrow, LeftArrow, UpArrow, DownArrow,
        Home, Option, R, L,

        Decision, Cancel,

        End,
    }

    //�e�t�F�C�Y
    public enum AnyPhase {
        PreparationPhase,
        SelectPhase,
        BattlePhase,
        EndPhase,

        End,
    }

    public enum WinLose {
        Win,Lose,End,
    }
}