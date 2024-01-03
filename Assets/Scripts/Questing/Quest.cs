public class Quest {
    private string questText;
    private int questType;
    private int enemyType;
    private int enemyCount;

    public Quest(string _questText, int _questType, int _enemyType, int _enemyCount){
        this.questText = _questText;
        this.questType = _questType;
        this.enemyType = _enemyType;
        this.enemyCount = _enemyCount;
    }

    public string getText() { return this.questText; }
    public int getType() { return this.questType; }
    public int getEType() { return this.enemyType; }
    public int getECount() { return this.enemyCount; }
}