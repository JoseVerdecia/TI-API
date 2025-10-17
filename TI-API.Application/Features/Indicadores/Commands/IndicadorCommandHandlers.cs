using AutoMapper;
using MediatR;
using TI_API.Application.Common.Interfaces;
using TI_API.Application.Features.Indicadores.Dtos;
using TI_API.Domain.Entities;

namespace TI_API.Application.Features.Indicadores.Commands
{
    public class CreateIndicadorCommandHandler : IRequestHandler<CreateIndicadorCommand, IndicadorDto>
    {
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IMapper _mapper;
        public CreateIndicadorCommandHandler(IUnitOfWorks unitOfWorks, IMapper mapper)
        {
            _unitOfWorks = unitOfWorks;
            _mapper = mapper;
        }
        public async Task<IndicadorDto> Handle(CreateIndicadorCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<IndicadorModel>(request.Dto);
            await _unitOfWorks.Indicador.AddAsync(entity);
            await _unitOfWorks.SaveChangesAsync();
            return _mapper.Map<IndicadorDto>(entity);
        }
    }

    public class UpdateIndicadorCommandHandler : IRequestHandler<UpdateIndicadorCommand, IndicadorDto>
    {
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IMapper _mapper;
        public UpdateIndicadorCommandHandler(IUnitOfWorks unitOfWorks, IMapper mapper)
        {
            _unitOfWorks = unitOfWorks;
            _mapper = mapper;
        }
        public async Task<IndicadorDto> Handle(UpdateIndicadorCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWorks.Indicador.GetByIdAsync(request.Dto.Id);
            if (entity == null) return null;
            _mapper.Map(request.Dto, entity);
            _unitOfWorks.Indicador.Update(entity);
            await _unitOfWorks.SaveChangesAsync();
            return _mapper.Map<IndicadorDto>(entity);
        }
    }

    public class DeleteIndicadorCommandHandler : IRequestHandler<DeleteIndicadorCommand, bool>
    {
        private readonly IUnitOfWorks _unitOfWorks;
        public DeleteIndicadorCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<bool> Handle(DeleteIndicadorCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWorks.Indicador.GetByIdAsync(request.Id);
            if (entity == null) return false;
            _unitOfWorks.Indicador.Remove(entity);
            await _unitOfWorks.SaveChangesAsync();
            return true;
        }
    }
}
