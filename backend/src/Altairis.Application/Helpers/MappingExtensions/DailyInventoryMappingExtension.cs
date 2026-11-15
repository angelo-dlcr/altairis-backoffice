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
            RoomType = entity.RoomType,
            RoomTypeId = entity.RoomTypeId,
            StayDate = entity.StayDate,
            TotalUnits = entity.TotalUnits
        };

        public static DailyInventory ToEntity(this DailyInventoryDTO dto) => new()
        {
            Id = dto.Id,
            CreationDate = dto.CreationDate,
            RoomType = dto.RoomType,
            RoomTypeId = dto.RoomTypeId,
            StayDate = dto.StayDate,
            TotalUnits = dto.TotalUnits
        };
    }
}
