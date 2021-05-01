using System.Collections;
using System.Collections.Generic;

namespace Liplum
{
    public class ParallelArray<P, S> : IEnumerable<(P, S)>
    {

        public P[] Primarys
        {
            get; init;
        }


        public S[] Secondarys
        {
            get; init;
        }

        public int Count
        {
            get; init;
        }


        public ParallelArray(int length)
        {
            Count = length;
            Primarys = new P[length];
            Secondarys = new S[length];
        }

        IEnumerator<(P, S)> IEnumerable<(P, S)>.GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return (Primarys[i], Secondarys[i]);
            }
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return (Primarys[i], Secondarys[i]);
            }
        }
    }
}
