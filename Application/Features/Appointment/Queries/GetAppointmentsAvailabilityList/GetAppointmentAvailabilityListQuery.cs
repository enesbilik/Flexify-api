using Application.Features.Appointment.Queries.GetListByDynamic;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Responses;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Appointment.Queries.GetAppointmentsAvailabilityList;

public class GetAppointmentAvailabilityListQuery : IRequest<List<GetAppointmentAvailabilityListItemDto>>
{
    public Guid ConsultantId { get; set; }


    public class GetAppointmentAvailabilityListQueryHandler : IRequestHandler<GetAppointmentAvailabilityListQuery,
        List<GetAppointmentAvailabilityListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAppointmentAvailabilityListQueryHandler(IMapper mapper, IAppointmentRepository appointmentRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<List<GetAppointmentAvailabilityListItemDto>> Handle(GetAppointmentAvailabilityListQuery request,
            CancellationToken cancellationToken)
        {
            int numberOfBusinessDays = 5;
            DateTime tomorrow = DateTime.Today.AddDays(1); // Tomorrow
            List<GetAppointmentAvailabilityListItemDto> availabilityList = new List<GetAppointmentAvailabilityListItemDto>();

            var appointments = await _appointmentRepository.GetListAsync(
                include: a =>
                    a.Include(co => co.Consultant)
                        .Include(cl => cl.Client),
                cancellationToken: cancellationToken,
                size: 200,
                predicate: a => a.ConsultantId == request.ConsultantId && a.StartTime >= tomorrow
            );


            for (int i = 0; availabilityList.Count < numberOfBusinessDays; i++)
            {
                DateTime currentDay = tomorrow.AddDays(i);

                if (currentDay.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
                    continue;

                GetAppointmentAvailabilityListItemDto availability = new GetAppointmentAvailabilityListItemDto
                {
                    Day = currentDay.ToString("yyyy-MM-dd"),
                    TimeSlots = new List<TimeSlot>()
                };

                TimeSpan slotDuration = TimeSpan.FromMinutes(60);
                DateTime slotStart = currentDay.Date.AddHours(8);
                DateTime slotEnd = slotStart.Add(slotDuration);

                while (slotEnd <= currentDay.Date.AddHours(17))
                {
                    var slotStatus = (int)AppointmentStatus.Empty;
                    foreach (var appointment in appointments.Items)
                    {
                        // Randevu zaman aralığını kontrol et
                        DateTime appointmentStart = appointment.StartTime;
                        DateTime appointmentEnd = appointment.EndTime;

                        if (slotStart >= appointmentStart && slotEnd <= appointmentEnd)
                        {
                            // Eğer zaman dilimi randevu ile örtüşüyorsa, randevunun durumunu al
                            slotStatus = (int)appointment.Status;
                        }
                    }

                    availability.TimeSlots.Add(new TimeSlot
                    {
                        StartTime = slotStart.ToString("HH:mm"),
                        EndTime = slotEnd.ToString("HH:mm"),
                        Status = slotStatus // Slotun durumunu güncelle
                    });

                    slotStart = slotEnd;
                    slotEnd = slotStart.Add(slotDuration);
                }

                availabilityList.Add(availability);
            }

            return availabilityList;
        }
    }
}