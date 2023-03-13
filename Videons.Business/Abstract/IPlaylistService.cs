using Videons.Core.Utilities.Results;
using Videons.Entities.Concrete;
using Videons.Entities.DTOs;

namespace Videons.Business.Abstract;

public interface IPlaylistService
{
    IDataResult<IList<Playlist>> GetList();
    IDataResult<IList<Playlist>> GetListByChannelId(Guid channelId);
    Playlist GetById(Guid playlistId);
    IResult Add(PlaylistDto playlistDto);
    IResult Update(Guid id, PlaylistUpdateDto playlistUpdateDto);
    IResult AddVideo(Guid playlistId, Guid videoId);
    IResult Delete(Guid id);

    IResult AdminRemovePlaylist(Guid playlistId);
}