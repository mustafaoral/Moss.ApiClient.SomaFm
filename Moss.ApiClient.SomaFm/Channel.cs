using System;

namespace Moss.ApiClient.SomaFm
{
    public class Channel
    {
        public string Id { get; }
        public string Title { get; }
        public string Description { get; }
        public string Dj { get; }
        public string DjEmail { get; }
        public string Genre { get; }
        public Uri Image { get; }
        public Uri ImageLarge { get; }
        public Uri ImageExtraLarge { get; }
        public DateTime UpdatedUtc { get; }
        public uint Listeners { get; }
        public string LastPlaying { get; }

        public Channel(string id,
                       string title,
                       string description,
                       string dj,
                       string djEmail,
                       string genre,
                       Uri image,
                       Uri imageLarge,
                       Uri imageExtraLarge,
                       DateTime updatedUtc,
                       uint listeners,
                       string lastPlaying)
        {
            Id = id;
            Title = title;
            Description = description;
            Dj = dj;
            DjEmail = djEmail;
            Genre = genre;
            Image = image;
            ImageLarge = imageLarge;
            ImageExtraLarge = imageExtraLarge;
            UpdatedUtc = updatedUtc;
            Listeners = listeners;
            LastPlaying = lastPlaying;
        }
    }
}
