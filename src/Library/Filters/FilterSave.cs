using System;
using System.Drawing;

namespace CompAndDel.Filters
{
    /// <summary>
    /// Un filtro que recibe una imagen y retorna su negativo.
    /// </remarks>
    public class FilterSave : IFilter
    {
        public string Name {get;set;}
        /// Un filtro que retorna el negativo de la imagen recibida.
        /// </summary>
        /// <param name="image">La imagen a la cual se le va a aplicar el filtro.</param>
        /// <returns>La imagen recibida pero en negativo.</returns>
        
        public FilterSave(string name)
        {
            this.Name = name;
        }
        public IPicture Filter(IPicture image)
        {
            IPicture result = image.Clone();
            new PictureProvider().SavePicture(result, $"{this.Name}.jpg");
            return result;
        }
    }
}
