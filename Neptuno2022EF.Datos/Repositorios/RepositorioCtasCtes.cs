using Neptuno2022EF.Datos.Interfaces;
using Neptuno2022EF.Entidades.Dtos.Ciudad;
using Neptuno2022EF.Entidades.Dtos.CtaCte;
using Neptuno2022EF.Entidades.Dtos.Producto;
using Neptuno2022EF.Entidades.Entidades;
using NuevaAppComercial2022.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Neptuno2022EF.Datos.Repositorios
{
    public class RepositorioCtasCtes : IRepositorioCtasCtes
    {
        private readonly NeptunoDbContext _context;

        public RepositorioCtasCtes(NeptunoDbContext context)
        {
            _context = context;
        }
        public void Agregar(CtaCte ctaCte)
        {
            _context.CtasCtes.Add(ctaCte);
        }

        public void Borrar(int id)
        {
            try
            {
                var ctaCteInDb = _context.CtasCtes.SingleOrDefault(c => c.CtaCteId == id);
                if (ctaCteInDb == null)
                {
                    Exception ex = new Exception("Borrado por otro usuario");
                    throw ex;
                }
                _context.Entry(ctaCteInDb).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Editar(CtaCte ctaCte)
        {
            try
            {
                var ctaCteInDb = _context.CtasCtes.SingleOrDefault(c => c.CtaCteId == ctaCte.CtaCteId);
                if (ctaCteInDb == null)
                {
                    throw new Exception("Borrado por otro usuario");
                }
                ctaCteInDb.Debe = ctaCte.Debe;
                ctaCteInDb.Haber = ctaCte.Haber;
                ctaCteInDb.Saldo = ctaCte.Saldo;

                _context.Entry(ctaCteInDb).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EstaRelacionado(CtaCte ctaCte)
        {
            try
            {
                return _context.Clientes.Any(c => c.Id == ctaCte.ClienteId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(CtaCte ctaCte)
        {
            try
            {
                if (ctaCte.CtaCteId == 0)
                {
                    return _context.CtasCtes.Any(c => c.ClienteId == ctaCte.ClienteId);
                }
                return _context.CtasCtes.Any(c => c.ClienteId == ctaCte.ClienteId && c.CtaCteId != ctaCte.CtaCteId);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetCantidad()
        {
            return _context.CtasCtes.Count();
        }

        public CtaCte GetCtaCtePorId(int ctaCteId)
        {
            try
            {
                var ctaCteInDb = _context.CtasCtes
                    .SingleOrDefault(c => c.CtaCteId == ctaCteId);
                return ctaCteInDb;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<CtaCte> GetCtaCtePorClienteId(int ctaCteClienteId)
        {
            try
            {
                var listaCtaCteInDb = _context.CtasCtes
                .Include(c => c.Cliente)
                .Where(c => c.ClienteId == ctaCteClienteId).ToList();
                return listaCtaCteInDb;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<CtaCteResumen> GetCtasCtes_()
        {
            List<CtaCteResumen> lista=new List<CtaCteResumen>();
            var listaCtas = _context.CtasCtes.Include(c => c.Cliente)
                .GroupBy(c => c.Cliente.Nombre)
                .ToList();

            foreach (var g in listaCtas)
            {
                var cta = new CtaCteResumen
                {
                    Cliente = g.Key,
                    Saldo = g.Sum(x=>x.Debe-x.Haber),
                    ClienteId = g.First().ClienteId,
                };
                lista.Add(cta);
            }
            return lista;
        }

        public List<CtaCteListDto> GetCtasCtesPorPagina(int cantidad, int pagina)
        {
            return _context.CtasCtes.Include(c => c.Cliente)
                .OrderBy(c=>c.Saldo)
                .Skip(cantidad * (pagina - 1))
                .Take(cantidad)
                .Select(c => new CtaCteListDto
                {
                    CtaCteId = c.CtaCteId,
                    Saldo = c.Saldo,
                    Debe = c.Debe,
                    Haber= c.Haber,
                    NombreCliente = c.Cliente.Nombre
                }).ToList();
        }

        public List<CtaCte> GetCtasCtes()
        {
            List<CtaCte> lista = new List<CtaCte>();
            var listaCtas = _context.CtasCtes.Include(c => c.Cliente)
                .GroupBy(c => c.Cliente.Nombre)
                .ToList();

            foreach (var g in listaCtas)
            {
                var cta = new CtaCte
                { 
                    Saldo = g.First().Debe - g.Sum(x => x.Haber),
                    ClienteId = g.First().ClienteId,
                    NumeroFactura = g.First().NumeroFactura,
                    FechaMovimiento = g.First().FechaMovimiento,
                };
                lista.Add(cta);
            }
            return lista;
        }
    }
}
