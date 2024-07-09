using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Bookings
{
    public class BookingRepository(PetHealthDBContext dbContext, ILogger<BookingRepository> logger) : BaseRepository<Booking>(dbContext, logger), IBookingRepository
    {
        public async Task<(bool, string)> CheckReviewBooking(Booking booking)
        {
            try
            {
                int result;
                DateTime checkTime = booking.BookingTime;
                var query = dbContext.Bookings.AsQueryable();
                query = query.Where(b => b.Status == BookingStatus.Review)
                             .Where(b => b.Shift == booking.Shift);
                if (await query.CountAsync() <= 20)
                {
                    result = await query.Where(b => b.DoctorId == booking.DoctorId)
                                        .CountAsync();
                    if (result <= 5)
                    {
                        return (true, "Create Booking success");
                    }
                    else
                    {
                        return (false, "Doctor cant receive more!");
                    }
                }
                else
                {
                    return (false, "shift is full!");

                }
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Error at BookingRepository with ID {booking.Id} error {ex}");
                return (false,"Internal Error occur");
            }
        }

        public async Task<Booking?> GetBookingDetail(int bookingId)
        {
            var query = dbContext.Bookings.AsQueryable();
            query = query.Where(b => b.Id == bookingId)
                         .Include(b => b.BookingServices)
                            .ThenInclude(bs => bs.Service)
                         .Include(b => b.Pet)
                         .Include(b => b.Customer)
                         .Include(b => b.Staff);
            var result = await query.SingleOrDefaultAsync();
            return result;
        }

        public async Task<(IEnumerable<Booking> Bookings, int TotalCount)> GetListBooking(DateTime? bookingDate = null, BookingStatus status = BookingStatus.Review, int pageNumber = 1, int pageSize = 10)
        {
            var query = dbContext.Bookings.AsQueryable();

            if (bookingDate != null)
            {
                query = query.Where(b => b.BookingTime == bookingDate);
            }

            // Include related BookingServices
            query = query.Include(b => b.BookingServices)
                         .Include(b => b.Pet)
                         .Include(b => b.Doctor)
                         .Include(b => b.Doctor.User);


            query = query.Where(s => s.Status == status);

            var totalCount = await query.CountAsync();

            // Retrieve bookings with null-checking
            var bookings = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new Booking
                {
                    Id = b.Id,
                    BookingTime = b.BookingTime,
                    Status = b.Status,
                    Note = b.Note,
                    // Include Pet details
                    Pet = new Pet
                    {
                        Id = b.Pet.Id,
                        PetName = b.Pet.PetName,
                        KindOfPet = b.Pet.KindOfPet,
                        Gender = b.Pet.Gender,
                        Birthday = b.Pet.Birthday,
                        Species = b.Pet.Species,
                        CustomerId = b.Pet.CustomerId,
                        Customer = new Customer
                        {
                            Id = b.Pet.Customer.Id,
                            User = b.Pet.Customer.User != null ? new User
                            {
                                Id = b.Pet.Customer.User.Id,
                                FirstName = b.Pet.Customer.User.FirstName,
                                LastName = b.Pet.Customer.User.LastName
                            } : null
                        }
                    },
                    // Include BookingServices details
                    BookingServices = b.BookingServices.Select(bs => new BookingService
                    {
                        Id = bs.Id,
                        Service = new Service
                        {
                            TypeId = bs.Service.TypeId,
                            Code = bs.Service.Code,
                            Name = bs.Service.Name,
                            Price = bs.Service.Price,
                            Note = bs.Service.Note,
                            Unit = bs.Service.Unit
                        }
                        // Include other properties of BookingService
                    }).ToList(),
                    // Include Doctor details with null-checking for Doctor and Doctor.User
                    Doctor = b.Doctor != null ? new Doctor
                    {
                        Id = b.Doctor.Id,
                        // Include other properties of Doctor

                        // Include User details if Doctor.User is not null
                        User = b.Doctor.User != null ? new User
                        {
                            Id = b.Doctor.User.Id,
                            FirstName = b.Doctor.User.FirstName,
                            LastName = b.Doctor.User.LastName,
                            // Include other properties of User
                        } : null
                    } : null
                })
                    .ToListAsync();

            return (bookings, totalCount);
        }
    }
}
