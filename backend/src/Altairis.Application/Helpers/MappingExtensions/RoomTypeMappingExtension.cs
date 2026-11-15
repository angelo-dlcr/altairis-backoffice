using Altairis.Application.DTOs;
using Altairis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Application.Helpers.MappingExtensions
{
    public static class RoomTypeMappingExtension
    {
        public static RoomTypeDTO ToDto(this RoomType entity) => new()
        {
            Id = entity.Id,
            Code = entity.Code,
            CreationDate = entity.CreationDate,
            Description = entity.Description,
            HotelId = entity.HotelId,
            IsActive = entity.IsActive,
            MaxOccupancy = entity.MaxOccupancy,
            Name = entity.Name,
            //Hotel = entity.Hotel?.ToDto(),
            //DailyInventories = entity.DailyInventories?.Select(x => x.ToDto()).ToList(),
            //Reservations = entity.Reservations?.Select(x => x.ToDto()).ToList(),
        };

        public static RoomType ToEntity(this RoomTypeDTO dto) => new()
        {
            Id = dto.Id,
            Code = dto.Code,
            CreationDate = dto.CreationDate,
            Description = dto.Description,
            HotelId = dto.HotelId,
            IsActive = dto.IsActive,
            MaxOccupancy = dto.MaxOccupancy,
            Name = dto.Name,
            //Hotel = dto.Hotel?.ToEntity(),
            //DailyInventories = dto.DailyInventories,
            //Reservations = dto.Reservations,
        };
    }
}
