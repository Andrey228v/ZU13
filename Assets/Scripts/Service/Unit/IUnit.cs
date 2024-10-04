namespace Assets.Scripts.Service.Unit
{
    public interface IUnit
    {
        public IDamageDealer DamageDealer {  get; }
        public IMoveUnit Move { get; }
    }
}
