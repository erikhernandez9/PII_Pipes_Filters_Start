using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompAndDel;
using CompAndDel.Filters;

namespace CompAndDel.Pipes
{
    public class PipeConditionalFork : IPipe
    {
        private string path {get;set;}
        protected IFilter filtro;     
        protected IPipe nextPipe;   
        /// <summary>
        /// La cañería recibe una imagen, le aplica un filtro y la envía a la siguiente cañería
        /// </summary>
        /// <param name="filtro">Filtro que se debe aplicar sobre la imagen</param>
        /// <param name="nextPipe">Siguiente cañería</param>
        public PipeConditionalFork (IFilter filtro, string path, IPipe nextPipe)
        {
            this.filtro = filtro;
            this.path = path;
            this.nextPipe = nextPipe;
        }
        /// <summary>
        /// Devuelve el proximo IPipe
        /// </summary>
        /// <summary>
        /// Devuelve el IFilter que aplica este pipe
        /// </summary>
        public IFilter Filter
        {
            get { return this.filtro; }
        }

        public IPipe Next
        {
            get { return this.nextPipe; }
        }
        /// <summary>
        /// Recibe una imagen, le aplica un filtro y la envía al siguiente Pipe
        /// </summary>
        /// <param name="picture">Imagen a la cual se debe aplicar el filtro</param>
        public IPicture Send(IPicture picture)
        {
            FilterConditional filterConditional = new FilterConditional(this.path);
            picture = filterConditional.Filter(picture);
            if (filterConditional.Face == true)
            {
                this.filtro = new SubirTwiter(this.path, "Tiene cara");
            }
            else
            {
                this.filtro = new FilterNegative();
            }
            picture = this.filtro.Filter(picture);
            return this.nextPipe.Send(picture);
        }
    }
}