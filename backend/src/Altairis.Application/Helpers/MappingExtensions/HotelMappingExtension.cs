using Altairis.Application.DTOs;
using Altairis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Application.Helpers.MappingExtensions
{
    public static class HotelMappingExtension
    {
        public static HotelDTO ToDto(this Hotel entity) => new()
        {
            Id = entity.Id,
            Address = entity.Address,
            City = entity.City,
            Code = entity.Code,
            Country = entity.Country,
            CreationDate = entity.CreationDate,
            IsActive = entity.IsActive,
            Name = entity.Name,
            RoomTypes = entity.RoomTypes
        };

        public static Hotel ToEntity(this HotelDTO dto) => new()
        {
            Id = dto.Id,
            Address = dto.Address,
            City = dto.City,
            Code = dto.Code,
            Country = dto.Country,
            CreationDate = dto.CreationDate,
            IsActive = dto.IsActive,
            Name = dto.Name,
            RoomTypes = dto.RoomTypes
        };
    }
}
