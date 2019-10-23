using System;

namespace Initial.Api.Models.Templates
{
    public abstract class EntityRequest
    {
        /// <summary>
        /// Versão da entidade, utilizada para processos de concorrência.
        /// Caso a EntityVersion do Request for diferente do atual da aplicação, apresentará um erro de concorrência.
        /// Caso seja nulo, ignorará a concorrência.
        /// </summary>
        public DateTime? EntityVersion { get; set; }
    }
}
