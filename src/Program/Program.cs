using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;
using TwitterUCU;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture(@"beer.jpg");

            FilterSave filterSave = new FilterSave(@"Negativo.jpg");
            FilterSave filterSave2 = new FilterSave(@"Grey.jpg");
            FilterSave filterSave3 = new FilterSave(@"Modificado.jpg");
            SubirTwiter subir = new SubirTwiter(@"Negativo.jpg", "Negativo");
            SubirTwiter subir2 = new SubirTwiter(@"Grey.jpg", "Grey");
            FilterNegative filterNegative = new FilterNegative();
            FilterGreyscale filterGreyscale = new FilterGreyscale();
            FilterConditional filterConditional = new FilterConditional(@"Grey.jpg");

            PipeNull pipeNull = new PipeNull();
            PipeSerial pipeSerial = new PipeSerial(filterSave3, pipeNull);
            pipeSerial = new PipeSerial(filterConditional, pipeSerial);
            // Esta parte esta comentada porque sino, no se puede detectar la cara
            /*pipeSerial = new PipeSerial(subir, pipeSerial);
            pipeSerial = new PipeSerial(filterSave, pipeSerial);
            pipeSerial = new PipeSerial(filterNegative, pipeSerial);*/
            pipeSerial = new PipeSerial(subir2, pipeSerial);
            pipeSerial = new PipeSerial(filterSave2, pipeSerial);
            pipeSerial = new PipeSerial(filterGreyscale, pipeSerial);
            picture = pipeSerial.Send(picture);

            provider.SavePicture(picture, @"beer2.jpg");
        }
    }
}
