using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioStone.Models
{
    public class Item
    {
        public ObjectId Id { get; set; }
        [BsonElement("codigo")]
        public int Codigo { get; set; }
        [BsonElement("descricao")]
        public String Descricao { get; set; }
        [BsonElement("livre")]
        public Boolean Livre { get; set; }
        [BsonElement("andar")]
        public int Andar { get; set; }
        [BsonElement("criado_em")]
        public DateTime CriadoEm { get; set; }
        [BsonElement("atualizado_em")]
        public DateTime AtualizadoEm { get; set; }
    }
}
