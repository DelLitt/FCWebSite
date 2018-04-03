namespace FCBLL.Implementations.Components
{
    using System.Linq;
    using FCCore.Abstractions.Bll.Components;

    public class GameFormatManager : IGameFormatManager
    {
        public byte GetRoundTime(int minutesWithExtra)
        {
            // TODO: Hardcoded for now. Needs to be implemented correctly
            byte[] periodDuration = new byte[] { 45, 90, 105, 120 };

            byte selDuration = periodDuration.First();
            
            foreach(byte duration in periodDuration)
            {
                if(duration > minutesWithExtra)
                {
                    return selDuration;
                }

                selDuration = duration;
            }

            return selDuration;
        }
    }
}
