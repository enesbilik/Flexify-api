using System.Security.Claims;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.ConsultantPreferences.Commands.Update;

public class UpdateConsultantPreferencesCommand : IRequest<UpdatedConsultantPreferencesResponse>, ITransactionalRequest,
    ILoggableRequest
{
    //Create a property for the update entity
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int AppointmentInterval { get; set; }
    public TimeSpan LunchBreakStartTime { get; set; }
    public TimeSpan LunchBreakEndTime { get; set; }
    public int DaysAheadForAppointment { get; set; }
    public bool IsWeekendAppointmentAllowed { get; set; }

    //Create a class for the handler

    public class UpdateConsultantPreferencesCommandHandler : IRequestHandler<UpdateConsultantPreferencesCommand,
        UpdatedConsultantPreferencesResponse>
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConsultantPreferencesRepository _consultantPreferencesRepository;
        private readonly IConsultantRepository _consultantRepository;

        public UpdateConsultantPreferencesCommandHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor,
            IConsultantPreferencesRepository consultantPreferencesRepository, IConsultantRepository consultantRepository)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _consultantPreferencesRepository = consultantPreferencesRepository;
            _consultantRepository = consultantRepository;
        }


        public async Task<UpdatedConsultantPreferencesResponse> Handle(UpdateConsultantPreferencesCommand request,
            CancellationToken cancellationToken)
        {
            var currentUserMail = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);

            var currentConsultant = await _consultantRepository.GetAsync(c => c.Email == currentUserMail,
                cancellationToken: cancellationToken);

            var consultantPreferences = await _consultantPreferencesRepository.GetAsync(c => c.ConsultantId == currentConsultant!.Id,
                cancellationToken: cancellationToken);

            consultantPreferences = _mapper.Map(request, consultantPreferences);

            await _consultantPreferencesRepository.UpdateAsync(consultantPreferences!);

            var response = _mapper.Map<UpdatedConsultantPreferencesResponse>(consultantPreferences);

            return response;
        }
    }
}