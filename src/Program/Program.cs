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

            FilterSave filterSave = new FilterSave("Negativo");
            FilterSave filterSave2 = new FilterSave("Grey");
            SubirTwiter subir = new SubirTwiter("Negativo", "Negativo");
            SubirTwiter subir2 = new SubirTwiter("Grey", "Grey");
            FilterNegative filterNegative = new FilterNegative();
            FilterGreyscale filterGreyscale = new FilterGreyscale();

            PipeNull pipeNull = new PipeNull();
            PipeSerial pipeSerial = new PipeSerial(subir, pipeNull);
            pipeSerial = new PipeSerial(filterSave, pipeSerial);
            pipeSerial = new PipeSerial(filterNegative, pipeSerial);
            pipeSerial = new PipeSerial(subir2, pipeSerial);
            pipeSerial = new PipeSerial(filterSave2, pipeSerial);
            pipeSerial = new PipeSerial(filterGreyscale, pipeSerial);

            picture = pipeSerial.Send(picture);

            provider.SavePicture(picture, @"beer2.jpg");
        }
    }
}
