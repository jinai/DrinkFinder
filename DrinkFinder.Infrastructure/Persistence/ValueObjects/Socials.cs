using DrinkFinder.Common.Abstract;
using System;
using System.Collections.Generic;

namespace DrinkFinder.Infrastructure.Persistence.ValueObjects
{
    public class Socials : ValueObject
    {
        public Uri Instagram { get; private set; }
        public Uri Facebook { get; private set; }
        public Uri Twitter { get; private set; }
        public Uri LinkedIn { get; private set; }

        private Socials() { }

        public Socials(Uri instagram, Uri facebook, Uri twitter, Uri linkedIn)
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
