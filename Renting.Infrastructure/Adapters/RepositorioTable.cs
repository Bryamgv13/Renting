using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Azure;
using Azure.Data.Tables;
using Renting.Domain.Entities;
using Renting.Domain.Ports;

namespace Renting.Infrastructure.Adapters
{
    public class RepositorioTable : IRepositorioTable
    {
        private readonly TableClient TableClient;
        private readonly IMapper Mapper;

        public RepositorioTable(TableClient tableClient, IMapper mapper)
        {
            TableClient = tableClient;
            Mapper = mapper;
        }

        public IEnumerable<PicoYPlaca> ObtenerPicoYPlacas()
        {
            Pageable<TableEntity> entities = TableClient.Query<TableEntity>();
            return entities.Select(e => Mapper.Map<PicoYPlaca>(e));
        }

        public IEnumerable<PicoYPlaca> ObtenerPicoYPlacaPorDiaYTipo(int dia, int tipo)
        {
            Pageable<TableEntity> entities = TableClient.Query<TableEntity>($"Dia eq {dia} and Tipo eq {tipo}");
            return entities.Select(e => Mapper.Map<PicoYPlaca>(e));
        }

        public void UpsertPicoYPlaca(PicoYPlaca picoYPlaca)
        {
            TableEntity entity = new TableEntity();
            entity.PartitionKey = $"{picoYPlaca.Dia}-{picoYPlaca.Tipo}";
            entity.RowKey = picoYPlaca.Dia.ToString();
            entity["Dia"] = picoYPlaca.Dia;
            entity["Tipo"] = (int)picoYPlaca.Tipo;
            entity["Placas"] = picoYPlaca.Placas;
            TableClient.UpsertEntity(entity);
        }

        public void EliminarPicoYPlaca(int dia)
        {
            TableClient.DeleteEntity(dia.ToString(), dia.ToString());
        }
    }
}
