﻿namespace Teist.Data.Managers
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;

    using Teist.Common.ViewModels;
    using Teist.Data.Models;
    using Teist.Data.Repositories;
    using Teist.Common.Enums;

    public class AlbumManager
    {
        private readonly AlbumRepository albumRepository;
        private readonly ArtistRepository artistRepository;
        private readonly PieceRepository pieceRepository;

        public AlbumManager(AlbumRepository albumRepository , ArtistRepository artistRepository, PieceRepository pieceRepository)
        {
            this.albumRepository = albumRepository;
            this.artistRepository = artistRepository;
            this.pieceRepository = pieceRepository;
        }

        public void CreateAlbum(AlbumViewModel album)
        {
            Object Genre;

            Enum.TryParse(typeof(Genre), album.Genre, out Genre);
            var albumToCreate = new Album()
            {
                Name = album.Name,
                Genre = (Genre)Genre,
                Performer = this.artistRepository.GetByName(album.Performer),
                Collaborators = this.artistRepository.GetRange(album.Collaborators),
                Pieces = this.pieceRepository.GetRange(album.Pieces),
            };

            this.albumRepository.Add(albumToCreate);
        }

        public IEnumerable<Album> GetAll()
        {
            return this.albumRepository.All();
        }

        public Album Get(string name)
        {
            return this.albumRepository.GetByName(name);
        }

        public Album Update(Album old, AlbumViewModel updated)
        {
            var albumToCreate = Mapper.Map<Album>(updated);

            var collabs = this.artistRepository.GetRange(updated.Collaborators);
            var performer = this.artistRepository.GetByName(updated.Performer);
            var pieces = this.pieceRepository.GetRange(updated.Pieces);

            albumToCreate.Collaborators = collabs;
            albumToCreate.Performer = performer;
            albumToCreate.Pieces = pieces;


            this.albumRepository.HardDelete(old);
            this.albumRepository.Add(albumToCreate);

            return this.Get(albumToCreate.Name);
        }

        public void Delete(string name)
        {
            var album = this.Get(name);

            this.albumRepository.Delete(album);
        }
    }
}
