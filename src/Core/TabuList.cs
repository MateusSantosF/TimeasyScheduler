
using TimeasyCore.src.Core;

namespace TimeasyScheduler.src.Core
{
    public class TabuList<T>
    {
        private Dictionary<T, bool> tabuList;
        private int maxSize;

        public TabuList(int? tabuTenure = null)
        {
            tabuList = new Dictionary<T, bool>();
            this.maxSize = tabuTenure ?? 10;
        }

        public void AddTabu(T move)
        {
            if (!tabuList.ContainsKey(move))
            {
                tabuList.Add(move, true);
                if (tabuList.Count >= maxSize)
                {
                    tabuList.Remove(tabuList.Keys.First());
                }
            }
        }

        public bool IsTabu(T move)
        {
            return tabuList.ContainsKey(move);
        }
    }
}
