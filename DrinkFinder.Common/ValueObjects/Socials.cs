using System;
using System.Collections.Generic;

namespace DrinkFinder.Common.ValueObjects
{
    public class Socials : ValueObject
    {
        public string Instagram { get; private set; }
        public string Facebook { get; private set; }
        public string Twitter { get; private set; }
        public string LinkedIn { get; private set; }

        private Socials() { }

        public Socials(string instagram, string facebook, string twitter, string linkedIn)
        {
            Instagram = instagram;
            Facebook = facebook;
            Twitter = twitter;
            LinkedIn = linkedIn;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Instagram;
            yield return Facebook;
            yield return Twitter;
            yield return LinkedIn;
        }
    }
}
