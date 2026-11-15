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
            //DailyInventories = entity.DailyInventories,
            Description = entity.Description,
            //Hotel = entity.Hotel,
            HotelId = entity.HotelId,
            IsActive = entity.IsActive,
            MaxOccupancy = entity.MaxOccupancy,
            Name = entity.Name
            //Reservations = entity.Reservations
        };

        public static RoomType ToEntity(this RoomTypeDTO dto) => new()
        {
            Id = dto.Id,
            Code = dto.Code,
            CreationDate = dto.CreationDate,
            //DailyInventories = dto.DailyInventories,
            Description = dto.Description,
            //Hotel = dto.Hotel,
            HotelId = dto.HotelId,
            IsActive = dto.IsActive,
            MaxOccupancy = dto.MaxOccupancy,
            Name = dto.Name
            //Reservations = dto.Reservations
        };
    }
}
