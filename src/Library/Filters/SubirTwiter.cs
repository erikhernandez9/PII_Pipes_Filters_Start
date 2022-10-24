using System;
using System.Drawing;
using TwitterUCU;

namespace CompAndDel.Filters
{
    /// <summary>
    /// Un filtro que recibe una imagen y retorna su negativo.
    /// </remarks>
    public class SubirTwiter : IFilter
    {
        public string Image {get;set;}
        public string Text {get;set;}
        /// Un filtro que retorna el negativo de la imagen recibida.
        /// </summary>
        /// <param name="image">La imagen a la cual se le va a aplicar el filtro.</param>
        /// <returns>La imagen recibida pero en negativo.</returns>
        
        public SubirTwiter(string image, string text)
        {
            this.Image = image;
            this.Text = text;
        }
        public IPicture Filter(IPicture image)
        {
            var twitter = new TwitterImage();
            Console.WriteLine(twitter.PublishToTwitter(this.Text, $"../Program/{this.Image}.jpg"));
            return image;
        }
    }
}