using System;

namespace Lob
{
    public static partial class ApiUrls
    {
        static readonly Uri _letters = new Uri("letters", UriKind.Relative);

        public static Uri Letters()
        {
            return _letters;
        }
    }   
}