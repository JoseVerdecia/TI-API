using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TI_API.Controllers.Indicador
{
    [ApiController]
    [Route("api/[controller]")]
    public class IndicatorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IndicatorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //    [HttpPost("evaluate/{id}")]
        //    [Authorize(Roles = "Admin,JefeProceso")]
        //    public async Task<ActionResult<IndicadorResponseDTO>> EvaluateIndicator(int id)
        //    {
        //        var result = await _mediator.Send(new EvaluateIndicatorCommand { IndicadorId = id });
        //        return Ok(result);
        //    }

        //    [HttpPost("evaluate-all")]
        //    [Authorize(Roles = "Admin,JefeProceso")]
        //    public async Task<ActionResult<List<IndicadorResponseDTO>>> EvaluateAllIndicators()
        //    {
        //        var result = await _mediator.Send(new EvaluateAllIndicatorsCommand());
        //        return Ok(result);
        //    }

        //    [HttpGet("{id}")]
        //    [Authorize]
        //    public async Task<ActionResult<IndicadorResponseDTO>> GetIndicatorById(int id)
        //    {
        //        var result = await _mediator.Send(new GetIndicatorByIdQuery { IndicadorId = id });
        //        return Ok(result);
        //    }

        //    [HttpGet]
        //    [Authorize]
        //    public async Task<ActionResult<List<IndicadorResponseDTO>>> GetAllIndicators()
        //    {
        //        var result = await _mediator.Send(new GetAllIndicatorsQuery());
        //        return Ok(result);
        //    }
        //}
    }
}