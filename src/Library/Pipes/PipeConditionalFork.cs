using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompAndDel;

namespace CompAndDel.Pipes
{
    public class PipeConditionalFork : IPipe
    {
        protected IFilter filtro;
        protected IPipe nextPipe;
        
        /// <summary>
        /// La cañería recibe una imagen, le aplica un filtro y la envía a la siguiente cañería
        /// </summary>
        /// <param name="filtro">Filtro que se debe aplicar sobre la imagen</param>
        /// <param name="nextPipe">Siguiente cañería</param>
        public PipeConditionalFork (IFilter filtro, IPipe nextPipe)
        {
            this.nextPipe = nextPipe;
            this.filtro = filtro;
        }
        /// <summary>
        /// Devuelve el proximo IPipe
        /// </summary>
        public IPipe Next
        {
            get { return this.nextPipe; }
        }
        /// <summary>
        /// Devuelve el IFilter que aplica este pipe
        /// </summary>
        public IFilter Filter
        {
            get { return this.filtro; }
        }
        /// <summary>
        /// Recibe una imagen, le aplica un filtro y la envía al siguiente Pipe
        /// </summary>
        /// <param name="picture">Imagen a la cual se debe aplicar el filtro</param>
        public IPicture Send(IPicture picture)
        {
            picture = this.filtro.Filter(picture);
            if (this.filtro.Face) 
            {
                SubirTwiter subir = new SubirTwiter(this.Path, "Tiene cara");
                pipe = new PipeSerial(subir, pipeNull);
                pipe.Send(result);
            }
            else
            {
                FilterNegative negativo = new FilterNegative();
                pipe = new PipeSerial(negativo, pipeNull);
                result = pipe.Send(result);
            }
            return this.nextPipe.Send(picture);
        }
    }
}