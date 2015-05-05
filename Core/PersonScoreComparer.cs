namespace BalloonsPops
{
    using System.Collections.Generic;

    class PersonScoreComparer : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            return x.Score.CompareTo(y.Score);
        }
    }
}
