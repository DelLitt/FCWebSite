namespace FCCore.Model.Refs
{
    using System.Collections.Generic;

    public class PersonGroups
    {
        public IEnumerable<int> Unknown { get; private set; } = new int[] { 1 };
        public IEnumerable<int> Player { get; private set; } = new int[] { 2 };
        public IEnumerable<int> Staff { get; private set; } = new int[] { 3, 4, 5, 6 };
    }
}
