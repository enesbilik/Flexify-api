using System.Security.Claims;
using Application.Features.Appointment.Commands.Create;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Appointment.Commands.Create;

public class CreateAppointmentCommand : IRequest<CreatedAppointmentResponse>, ITransactionalRequest,
    ILoggableRequest
{
    public Guid ConsultantId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }


    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, CreatedAppointmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IClientRepository _clientRepository;

        public CreateAppointmentCommandHandler(IMapper mapper, IAppointmentRepository appointmentRepository,
            IHttpContextAccessor httpContextAccessor,
            IClientRepository clientRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _httpContextAccessor = httpContextAccessor;
            _clientRepository = clientRepository;
        }


        public async Task<CreatedAppointmentResponse> Handle(CreateAppointmentCommand request,
            CancellationToken cancellationToken)
        {
            var mail = _httpContextAccessor.HttpContext.User.Identity.Name;

            var currentUserMail = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);

            var currentClient = await _clientRepository.GetAsync(c => c.Email == currentUserMail,
                cancellationToken: cancellationToken);


            var appointment = _mapper.Map<Domain.Entities.Appointment>(request);
            appointment.ClientId = currentClient.Id;

            await _appointmentRepository.AddAsync(appointment);

            var createdAppointmentResponse = _mapper.Map<CreatedAppointmentResponse>(appointment);

            return createdAppointmentResponse;
        }
    }
}