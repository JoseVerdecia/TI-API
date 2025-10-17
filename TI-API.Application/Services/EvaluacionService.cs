using TI_API.Application.Common.Extensions;
using TI_API.Application.Common.Interfaces;
using TI_API.Domain.Entities;
using TI_API.Domain.Enums;

namespace TI_API.Application.Services
{
    public class EvaluacionService : IEvaluacionService<IEvaluableIndicador>
    {
        public EvaluacionType Evaluar(IEvaluableIndicador indicador)
        {
            if (indicador.DecimalMetaCumplir == 0)
                return EvaluacionType.NoEvaluado;

            if (indicador.DecimalMetaReal == 0)
                return EvaluacionType.NoEvaluado;

            decimal porcentajeCumplimiento = (indicador.DecimalMetaReal / indicador.DecimalMetaCumplir) * 100;

            if (porcentajeCumplimiento > 100)
                return EvaluacionType.Sobrecumplido;
            else if (porcentajeCumplimiento == 100)
                return EvaluacionType.Cumplido;
            else if (porcentajeCumplimiento >= 80 && porcentajeCumplimiento < 100)
                return EvaluacionType.ParcialmenteCumplido;
            else
                return EvaluacionType.Incumplido;
        }

        public void SetMetaCumplir(IEvaluableIndicador indicador, string metaValue)
        {
            if (string.IsNullOrWhiteSpace(metaValue))
            {
                indicador.MetaCumplir = string.Empty;
                indicador.DecimalMetaCumplir = 0;
                indicador.IsMetaCumplirPorcentage = false;
                return;
            }

            if (metaValue.EndsWith("%"))
            {
                indicador.IsMetaCumplirPorcentage = true;
                var valueWithoutPercent = metaValue.Replace("%", "").Trim();
                if (decimal.TryParse(valueWithoutPercent, out decimal percentValue))
                {
                    indicador.DecimalMetaCumplir = percentValue;
                    indicador.MetaCumplir = metaValue;
                }
                else
                {
                    throw new ArgumentException("El valor porcentual no es válido");
                }
            }
            else
            {
                indicador.IsMetaCumplirPorcentage = false;
                if (decimal.TryParse(metaValue, out decimal absoluteValue))
                {
                    indicador.DecimalMetaCumplir = absoluteValue;
                    indicador.MetaCumplir = metaValue;
                }
                else
                {
                    throw new ArgumentException("El valor absoluto no es válido");
                }
            }
        }

        public void SetMetaReal(IEvaluableIndicador indicador, string metaValue)
        {
            if (string.IsNullOrWhiteSpace(metaValue))
            {
                indicador.MetaReal = string.Empty;
                indicador.DecimalMetaReal = 0;
                indicador.IsMetaRealPorcentage = false;
                return;
            }

            if (metaValue.EndsWith("%"))
            {
                indicador.IsMetaRealPorcentage = true;
                var valueWithoutPercent = metaValue.Replace("%", "").Trim();
                if (decimal.TryParse(valueWithoutPercent, out decimal percentValue))
                {
                    indicador.DecimalMetaReal = percentValue;
                    indicador.MetaReal = metaValue;
                }
                else
                {
                    throw new ArgumentException("El valor porcentual no es válido");
                }
            }
            else
            {
                indicador.IsMetaRealPorcentage = false;
                if (decimal.TryParse(metaValue, out decimal absoluteValue))
                {
                    indicador.DecimalMetaReal = absoluteValue;
                    indicador.MetaReal = metaValue;
                }
                else
                {
                    throw new ArgumentException("El valor absoluto no es válido");
                }
            }
        }

        public string GetEvaluacionDisplay(IEvaluableIndicador indicador)
        {
            return indicador.Evaluacion.GetDisplayName();
        }
    }
}