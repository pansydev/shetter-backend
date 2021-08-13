using System;

namespace PansyDev.Shetter.Domain.Aggregates.PostAggregate
{
    public class PostImage
    {
        public PostImage(Guid id, Uri url, string blurHash)
        {
            Id = id;
            Url = url;
            BlurHash = blurHash;
        }

        protected PostImage() { }

        public Guid Id { get; private set; }
        public Uri Url { get; internal set; } = null!;
        public string BlurHash { get; internal set; } = null!;

        private bool Equals(PostImage other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((PostImage)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}