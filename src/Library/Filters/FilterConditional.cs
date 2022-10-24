using System;
using System.Drawing;
using CognitiveCoreUCU;
using CompAndDel.Pipes;
namespace CompAndDel.Filters
{
    /// <summary>
    /// Un filtro que recibe una imagen y retorna su negativo.
    /// </remarks>
    public class FilterConditional : IFilter
    {
        public string Path {get;set;}
        public bool Face {get;set;}
        /// Un filtro que retorna el negativo de la imagen recibida.
        /// </summary>
        /// <param name="image">La imagen a la cual se le va a aplicar el filtro.</param>
        /// <returns>La imagen recibida pero en negativo.</returns>
        
        public FilterConditional(string path)
        {
            this.Path = path;
        }
        public IPicture Filter(IPicture image)
        {
            PipeSerial pipe;
            PipeNull pipeNull = new PipeNull();
            IPicture result = image.Clone();
            
            CognitiveFace cog = new CognitiveFace(true, Color.Black);
            cog.Recognize(this.Path);
            this.Face = cog.FaceFound;

            if (cog.FaceFound == true) 
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
            return result;
        }
    }
}
