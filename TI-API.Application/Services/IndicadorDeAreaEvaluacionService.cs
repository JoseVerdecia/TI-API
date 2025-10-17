using TI_API.Application.Common.Extensions;
using TI_API.Application.Common.Interfaces;
using TI_API.Domain.Entities;
using TI_API.Domain.Enums;

namespace TI_API.Application.Services
{
    public class IndicadorDeAreaEvaluacionService : IEvaluacionService<IndicadorDeAreaModel>
    {
        public EvaluacionType Evaluar(IndicadorDeAreaModel indicadorDeArea)
        {
            if (indicadorDeArea.DecimalMetaCumplirArea == 0)
                return EvaluacionType.NoEvaluado;

            if (indicadorDeArea.DecimalMetaRealArea == 0)
                return EvaluacionType.NoEvaluado;

            decimal porcentajeCumplimiento = (indicadorDeArea.DecimalMetaRealArea / indicadorDeArea.DecimalMetaCumplirArea) * 100;

            if (porcentajeCumplimiento > 100)
                return EvaluacionType.Sobrecumplido;
            else if (porcentajeCumplimiento == 100)
                return EvaluacionType.Cumplido;
            else if (porcentajeCumplimiento >= 80 && porcentajeCumplimiento < 100)
                return EvaluacionType.ParcialmenteCumplido;
            else
                return EvaluacionType.Incumplido;
        }

        public void SetMetaCumplir(IndicadorDeAreaModel indicadorDeArea, string metaValue)
        {
            if (string.IsNullOrWhiteSpace(metaValue))
            {
                indicadorDeArea.MetaCumplirArea = string.Empty;
                indicadorDeArea.DecimalMetaCumplirArea = 0;
                indicadorDeArea.IsMetaCumplirAreaPorcentage = false;
                return;
            }

            if (metaValue.EndsWith("%"))
            {
                indicadorDeArea.IsMetaCumplirAreaPorcentage = true;
                var valueWithoutPercent = metaValue.Replace("%", "").Trim();
                if (decimal.TryParse(valueWithoutPercent, out decimal percentValue))
                {
                    indicadorDeArea.DecimalMetaCumplirArea = percentValue;
                    indicadorDeArea.MetaCumplirArea = metaValue;
                }
                else
                {
                    throw new ArgumentException("El valor porcentual no es válido");
                }
            }
            else
            {
                indicadorDeArea.IsMetaCumplirAreaPorcentage = false;
                if (decimal.TryParse(metaValue, out decimal absoluteValue))
                {
                    indicadorDeArea.DecimalMetaCumplirArea = absoluteValue;
                    indicadorDeArea.MetaCumplirArea = metaValue;
                }
                else
                {
                    throw new ArgumentException("El valor absoluto no es válido");
                }
            }
        }

        public void SetMetaReal(IndicadorDeAreaModel indicadorDeArea, string metaValue)
        {
            if (string.IsNullOrWhiteSpace(metaValue))
            {
                indicadorDeArea.MetaRealArea = string.Empty;
                indicadorDeArea.DecimalMetaRealArea = 0;
                indicadorDeArea.IsMetaRealAreaPorcentage = false;
                return;
            }

            if (metaValue.EndsWith("%"))
            {
                indicadorDeArea.IsMetaRealAreaPorcentage = true;
                var valueWithoutPercent = metaValue.Replace("%", "").Trim();
                if (decimal.TryParse(valueWithoutPercent, out decimal percentValue))
                {
                    indicadorDeArea.DecimalMetaRealArea = percentValue;
                    indicadorDeArea.MetaRealArea = metaValue;
                }
                else
                {
                    throw new ArgumentException("El valor porcentual no es válido");
                }
            }
            else
            {
                indicadorDeArea.IsMetaRealAreaPorcentage = false;
                if (decimal.TryParse(metaValue, out decimal absoluteValue))
                {
                    indicadorDeArea.DecimalMetaRealArea = absoluteValue;
                    indicadorDeArea.MetaRealArea = metaValue;
                }
                else
                {
                    throw new ArgumentException("El valor absoluto no es válido");
                }
            }
        }

        public string GetEvaluacionDisplay(IndicadorDeAreaModel indicadorDeArea)
        {
            return indicadorDeArea.Evaluacion.GetDisplayName();
        }
    }
}
