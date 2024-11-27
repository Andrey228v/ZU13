namespace Assets.Scripts.Service
{
    public interface IUserInput
    {
        public bool Jump { get;}
        public bool Attack { get;}
        public bool RightMouseButton { get;}
        public float HorizontalMove { get;}
    }
}
