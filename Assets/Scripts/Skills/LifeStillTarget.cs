namespace Assets.Scripts.Skills
{
    public class LifeStillTarget
    {
        public EnemyBody Target {get; private set;}

        public void SetTarget(EnemyBody target) =>
            Target = target;
    }
}
