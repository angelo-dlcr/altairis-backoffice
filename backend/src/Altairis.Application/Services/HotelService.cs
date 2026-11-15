using Altairis.Application.DTOs;
using Altairis.Application.DTOs.Filters;
using Altairis.Application.Helpers;
using Altairis.Application.Helpers.MappingExtensions;
using Altairis.Application.Interfaces.Repositories;
using Altairis.Application.Interfaces.Services;
using Altairis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Text;

namespace Altairis.Application.Services
{
    public class HotelService : IHotelService
    {
        private readonly IBaseRepository<Hotel> _repository;

        public HotelService(IBaseRepository<Hotel> repository)
        {
            _repository = repository;
        }

        public async Task<SingleApiResponse<HotelDTO>> GetByIdAsync(int id)
        {
            try
            {
                var hotel = await _repository.GetByIdAsync(id);

                return (hotel is not null)
                    ? new SingleApiResponse<HotelDTO> { Data = hotel?.ToDto() }
                    : new SingleApiResponse<HotelDTO>
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Registro no encontrado."
                        };
            }
            catch (Exception ex)
            {
                return new SingleApiResponse<HotelDTO>()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ListApiResponse<HotelDTO>> GetAllAsync()
        {
            try
            {
                var hotels = await _repository.GetAllAsync();

                return (hotels is not null && hotels.Any())
                    ? new ListApiResponse<HotelDTO> { Items = hotels?.Select(h => h.ToDto())?.ToList() }
                    : new ListApiResponse<HotelDTO>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "No se hallaron registros.",
                    };
            }
            catch (Exception ex)
            {
                return new ListApiResponse<HotelDTO>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<PagedListApiResponse<HotelDTO>> GetAllPagedAsync(PaginationFilters pagination, HotelFiltersDTO? filter = null)
        {
            try
            {
                IReadOnlyList<Hotel>? hotels;

                if (filter is not null && !filter.AreAllPropertiesNull())
                {
                    Expression<Func<Hotel, bool>> predicate = (hotel) =>
                    (
                        (string.IsNullOrEmpty(filter.NameOrCode) ||
                            (hotel.Name.ToUpper().Contains(filter.NameOrCode.ToUpper().Trim())
                          || hotel.Code.ToUpper().Contains(filter.NameOrCode.ToUpper().Trim()))
                        )
                        && (string.IsNullOrEmpty(filter.Country) || hotel.Country.ToUpper().Contains(filter.Country.ToUpper().Trim()))
                        && (string.IsNullOrEmpty(filter.City) || hotel.City.ToUpper().Contains(filter.City.ToUpper().Trim()))
                        && (filter.IsActive == null || hotel.IsActive == filter.IsActive)
                        && (filter.StartDate == null || hotel.CreationDate >= filter.StartDate)
                        && (filter.EndDate == null || hotel.CreationDate <= filter.EndDate.Value.AddDays(1).AddSeconds(-1))
                    );

                    hotels = await _repository.GetByConditionOrderedByAsync(predicate, x => x.CreationDate, true);
                }
                else
                {
                    hotels = await _repository.GetAllOrderedByAsync(x => x.CreationDate, true);
                }


                return (hotels is not null && hotels.Any())
                    ? new PagedListApiResponse<HotelDTO>
                        {
                            Items = hotels?.Skip(pagination.Skip).Take(pagination.Take).Select(h => h.ToDto())?.ToList(),
                            TotalCount = hotels?.Count ?? 0,
                            PageNumber = pagination.PageNumber,
                            PageSize = pagination.PageSize,
                        }
                    : new PagedListApiResponse<HotelDTO>
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "No se hallaron registros.",
                        };
            }
            catch (Exception ex)
            {
                return new PagedListApiResponse<HotelDTO>()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
