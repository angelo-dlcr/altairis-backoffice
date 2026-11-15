using Altairis.Application.DTOs;
using Altairis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Application.Helpers.MappingExtensions
{
    public static class DailyInventoryMappingExtension
    {
        public static DailyInventoryDTO ToDto(this DailyInventory entity) => new()
        {
            Id = entity.Id,
            CreationDate = entity.CreationDate,
            RoomTypeId = entity.RoomTypeId,
            StayDate = entity.StayDate,
            TotalUnits = entity.TotalUnits,
            //RoomType = entity.RoomType.ToDto(),
        };

        public static DailyInventoryDTO ToDto(this DailyInventory entity, int reservedUnits) => new()
        {
            Id = entity.Id,
            CreationDate = entity.CreationDate,
            RoomTypeId = entity.RoomTypeId,
            StayDate = entity.StayDate,
            TotalUnits = entity.TotalUnits,
            ReservedUnits = reservedUnits,
            AvailableUnits = (entity.TotalUnits - reservedUnits) <= 0 ? 0 : entity.TotalUnits - reservedUnits,
            //RoomType = entity.RoomType.ToDto(),
        };

        public static DailyInventory ToEntity(this DailyInventoryDTO dto) => new()
        {
            Id = dto.Id,
            CreationDate = dto.CreationDate,
            RoomTypeId = dto.RoomTypeId,
            StayDate = dto.StayDate,
            TotalUnits = dto.TotalUnits,
            //RoomType = dto.RoomType.ToEntity(),
        };
    }
}
