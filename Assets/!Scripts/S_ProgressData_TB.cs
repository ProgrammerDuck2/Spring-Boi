[System.Serializable]
public class S_ProgressData_TB
{
    public float[] playerPosition;
    public int level;

    #region Quests
    public int[] currentQuests;
    public int[] completedQuests;
    #endregion
    public S_ProgressData_TB(S_Player_TB player, S_GameManager_TB gameManager)
    {
        playerPosition = new float[3];
        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
        playerPosition[2] = player.transform.position.z;

        level = gameManager.level;

        currentQuests = new int[S_Quests_TB.activeQuests.Count];

        for (int i = 0; i < S_Quests_TB.activeQuests.Count; i++)
        {
            currentQuests[i] = S_Quests_TB.activeQuests[i].ID;
        }

        completedQuests = new int[S_Quests_TB.completedQuests.Count];

        for (int i = 0; i < S_Quests_TB.completedQuests.Count; i++)
        {
            completedQuests[i] = S_Quests_TB.completedQuests[i].ID;
        }
    }
}
