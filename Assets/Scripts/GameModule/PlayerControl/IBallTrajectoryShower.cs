namespace GameModule
{
    public interface IBallTrajectoryShower
    {
        void Initialize();
        void Activate();
        void Deactivate();
    }

    public class ClassicBallTrajectoryShower : IBallTrajectoryShower
    {
        public void Activate()
        {
            throw new System.NotImplementedException();
        }

        public void Deactivate()
        {
            throw new System.NotImplementedException();
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
