namespace Base.Interfaces
{
    public interface IInjectionReceiver<in T>
    {
        public void Inject(T injection);
    }
}