using TI_API.Domain.Enums;

namespace TI_API.Application.Common.Interfaces
{
    public interface IEvaluacionService<T> where T : class
    {
        EvaluacionType Evaluar(T entity);
        void SetMetaCumplir(T entity, string metaValue);
        void SetMetaReal(T entity, string metaValue);
        string GetEvaluacionDisplay(T entity);
    }
}
