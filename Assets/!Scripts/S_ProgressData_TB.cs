[System.Serializable]
public class S_ProgressData_TB
{
    public float playerPositionX, playerPositionY, playerPositionZ;
    public int level;
    public S_ProgressData_TB(S_Player_TB player)
    {
        playerPositionX = player.transform.position.x;
        playerPositionY = player.transform.position.y;
        playerPositionZ = player.transform.position.z;
    }
}
