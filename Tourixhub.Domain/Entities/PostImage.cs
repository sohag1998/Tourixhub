using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tourixhub.Domain.Entities
{
    public class PostImage : IEntity<Guid>
    {
        public Guid Id { get ; set ; }
        public Guid PostId { get; set ; }
        public string ImageUrl { get; set ; }

        [JsonIgnore]
        public Post Post { get; set ; }
    }
}
