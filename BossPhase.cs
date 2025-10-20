using UnityEngine;

[CreateAssetMenu(menuName = "ArcadeCricket/BossPhase") ]
public class BossPhase : ScriptableObject
{
    public ProjectileData[] patterns;
    public float interval = 1.5f;
    public int shotsToNextPhase = 8;
    public float windupTime = 0.6f;
}
