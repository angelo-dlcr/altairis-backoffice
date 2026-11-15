using Altairis.Application.DTOs;
using Altairis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Application.Helpers.MappingExtensions
{
    public static class ReservationMappingExtension
    {
        public static ReservationDTO ToDto(this Reservation entity) => new()
        {
            Id = entity.Id,
            CheckInDate = entity.CheckInDate,
            CheckOutDate = entity.CheckOutDate,
            CreationDate = entity.CreationDate,
            GuestEmail = entity.GuestEmail,
            GuestName = entity.GuestName,
            Reference = entity.Reference,
            RoomsCount = entity.RoomsCount,
            RoomTypeId = entity.RoomTypeId,
            Status = entity.Status,
            RoomType = entity.RoomType?.ToDto(),
        };

        public static Reservation ToEntity(this ReservationDTO dto) => new()
        {
            Id = dto.Id,
            CheckInDate = dto.CheckInDate,
            CheckOutDate = dto.CheckOutDate,
            CreationDate = dto.CreationDate,
            GuestEmail = dto.GuestEmail,
            GuestName = dto.GuestName,
            Reference = dto.Reference,
            RoomsCount = dto.RoomsCount,
            RoomTypeId = dto.RoomTypeId,
            Status = dto.Status,
            //RoomType = dto.RoomType?.ToEntity(),
        };
    }
}
