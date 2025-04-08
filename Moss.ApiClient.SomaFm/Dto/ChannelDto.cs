namespace Moss.ApiClient.SomaFm.Dto;

internal record ChannelDto(
    string Id,
    string Title,
    string Description,
    string Dj,
    string Djmail,
    string Genre,
    Uri Image,
    Uri LargeImage,
    Uri XlImage,
    long Updated,
    PlaylistDto[] Playlists,
    Uri[] PreRoll,
    uint Listeners,
    string LastPlaying);
