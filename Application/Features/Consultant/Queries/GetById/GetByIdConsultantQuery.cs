using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Consultant.Queries.GetById;

public class GetByIdConsultantQuery : IRequest<GetByIdConsultantResponse>
{
    public Guid Id { get; set; }


    public class GetByIdConsultantQueryHandler : IRequestHandler<GetByIdConsultantQuery, GetByIdConsultantResponse>
    {
        private readonly IMapper _mapper;
        private readonly IConsultantRepository _consultantRepository;

        public GetByIdConsultantQueryHandler(IMapper mapper, IConsultantRepository consultantRepository)
        {
            _mapper = mapper;
            _consultantRepository = consultantRepository;
        }

        public async Task<GetByIdConsultantResponse> Handle(GetByIdConsultantQuery request,
            CancellationToken cancellationToken)
        {
            var consultant = await _consultantRepository.GetAsync(
                predicate: c => c.Id == request.Id,
                withDeleted: true,
                cancellationToken: cancellationToken
            );

            var response = _mapper.Map<GetByIdConsultantResponse>(consultant);
            return response;
        }
    }
}