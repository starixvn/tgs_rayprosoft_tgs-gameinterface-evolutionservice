using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tgs.GameInterface.EvolutionService.Core.Models
{
    public record AuthenticationPlayerSession
    {
        public string Id { get; set; } = default!;
        public string Ip { get; set; } = default!;
    }
}
