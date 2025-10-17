using System.ComponentModel;

namespace TI_API.Application.Common.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Obtiene el valor del atributo [Description] de un enum
        /// </summary>
        /// <param name="enumValue">Valor del enum</param>
        /// <returns>Descripción del enum o su nombre si no tiene atributo Description</returns>
        public static string GetDisplayName(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo == null)
                return enumValue.ToString();

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (descriptionAttributes?.Length > 0)
                return descriptionAttributes[0].Description;

            // Si no tiene Description, intenta con Display
            var displayAttributes = fieldInfo.GetCustomAttributes(
                typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false)
                as System.ComponentModel.DataAnnotations.DisplayAttribute[];

            if (displayAttributes?.Length > 0)
                return displayAttributes[0].Name ?? enumValue.ToString();

            return enumValue.ToString();
        }

        /// <summary>
        /// Obtiene todos los valores de un enum con sus descripciones
        /// </summary>
        /// <typeparam name="T">Tipo del enum</typeparam>
        /// <returns>Lista de pares (valor, descripción)</returns>
        public static List<(T Value, string Description)> GetAllWithDescription<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => (e, e.GetDisplayName()))
                .ToList();
        }
    }
}
