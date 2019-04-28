namespace Versiones.Models
{
    using Newtonsoft.Json;

    public class User
    {

         
          /*  [JsonProperty(PropertyName = "id")]
            public string Id { get; set; }*/

            [JsonProperty(PropertyName = "usuario")]
            public string Usuario { get; set; }

        [JsonProperty(PropertyName = "nombre")]
        public string Nombre { get; set; }

        /* [JsonProperty(PropertyName = "password")]
         public string Password { get; set; }

         [JsonProperty(PropertyName = "nivel")]
         public string Nivel { get; set; }



         [JsonProperty(PropertyName = "correo")]
         public string Correo { get; set; }*/

    }
}
