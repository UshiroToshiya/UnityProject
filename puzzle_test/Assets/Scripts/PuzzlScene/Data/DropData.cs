// ドロップの種類
public enum DropType
{
    Fire, //火
    Water,　//水
    Wind,//風
    Thunder,//雷
    Earth,//土

    // Lv2
    Ice,//氷　(水、風)
    Wood, //木　(水、土)
    Storm, //嵐　(水、雷)
    Lava, //溶岩　(火、土)
    Steam, //蒸気  (水、火)

    // Lv3
    Penetration //貫通　(火、土、風)
}
// パズルの「中身」だけを持つクラス
public class DropData
{
    // =========================
    // グリッド情報
    // =========================
    public int row;
    public int col;

    // =========================
    // ドロップ属性
    // =========================
    public DropType type;

    // =========================
    // 状態フラグ（将来拡張）
    // =========================
    public bool isMatched;   // 消去対象か
    public bool isLocked;    // 操作不可（お邪魔等）

    // =========================
    // コンストラクタ（任意）
    // =========================
    public DropData(int row, int col, DropType type)
    {
        this.row = row;
        this.col = col;
        this.type = type;
        isMatched = false;
        isLocked = false;
    }

    // デフォルト生成用（Random生成時）
    public DropData() { }
}
