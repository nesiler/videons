using Videons.Business.Abstract;
using Videons.Core.Utilities.Results;
using Videons.DataAccess.Abstract;
using Videons.Entities.Concrete;
using Videons.Entities.DTOs;

namespace Videons.Business.Concrete;

public class PlaylistManager : IPlaylistService
{
    private readonly IChannelDal _channelDal;
    private readonly IPlaylistDal _playlistDal;
    private readonly IVideoDal _videoDal;

    public PlaylistManager(IPlaylistDal playlistDal, IChannelDal channelDal, IVideoDal videoDal)
    {
        _playlistDal = playlistDal;
        _channelDal = channelDal;
        _videoDal = videoDal;
    }

    public IDataResult<IList<Playlist>> GetList()
    {
        var playlists = _playlistDal.GetList();
        return new SuccessDataResult<IList<Playlist>>(playlists);
    }

    public IDataResult<IList<Playlist>> GetListByChannelId(Guid channelId)
    {
        var playlists = _playlistDal.GetList(p => p.ChannelId == channelId);
        return new SuccessDataResult<IList<Playlist>>(playlists);
    }

    public Playlist GetById(Guid playlistId)
    {
        return _playlistDal.Get(p => p.Id == playlistId);
    }

    public IResult Add(PlaylistDto playlistDto)
    {
        var channelExist = _channelDal.Get(c => c.Id == playlistDto.ChannelId);
        if (channelExist == null) return new ErrorResult("Invalid channel");

        var playlist = new Playlist
        {
            Name = playlistDto.Name,
            Description = playlistDto.Description,
            Visibility = playlistDto.Visibility,
            ChannelId = playlistDto.ChannelId
        };

        if (!_playlistDal.Add(playlist)) return new ErrorResult("Playlist cannot created!");

        return new SuccessResult("Playlist created.");
    }

    public IResult Update(Guid id, PlaylistUpdateDto playlistUpdateDto)
    {
        var playlist = GetById(id);

        if (playlist == null) return new ErrorResult("Playlist cannot found!");

        playlist.Name = playlistUpdateDto.Name;
        playlist.Description = playlistUpdateDto.Description;
        playlist.Visibility = playlistUpdateDto.Visibility;

        return _playlistDal.Update(playlist)
            ? new SuccessResult("Playlist updated.")
            : new ErrorResult("Playlist cannot updated!");
    }

    public IResult AddVideo(Guid playlistId, Guid videoId)
    {
        var playlist = GetById(playlistId);
        if (playlist == null) return new ErrorResult("Playlist cannot found!");
        
        var video = _videoDal.Get(v => v.Id == videoId);
        if (video == null) return new ErrorResult("Video cannot found!");
        
        var playlistVideo = new PlaylistVideo
        {
            PlaylistId = playlistId,
            VideoId = videoId
        };
        
        return _playlistDal.AddVideoToPlaylist(playlistVideo)
            ? new SuccessResult("Video added to playlist.")
            : new ErrorResult("Video cannot added to playlist!");
    }
    public IResult Delete(Guid id)
    {
        var playlist = GetById(id);
        if (playlist == null) return new ErrorResult("Playlist cannot found!");

        return _playlistDal.Delete(playlist)
            ? new SuccessResult("Playlist deleted.")
            : new ErrorResult("Palylsit cannot deleted!");
    }
}