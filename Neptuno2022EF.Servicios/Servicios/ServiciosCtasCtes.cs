using Neptuno2022EF.Datos.Interfaces;
using Neptuno2022EF.Datos;
using Neptuno2022EF.Entidades.Entidades;
using Neptuno2022EF.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptuno2022EF.Datos.Repositorios;
using Neptuno2022EF.Entidades.Dtos.CtaCte;
using System.Transactions;

namespace Neptuno2022EF.Servicios.Servicios
{
    public class ServiciosCtasCtes : IServiciosCtasCtes
    {
        private readonly IRepositorioCtasCtes _repositorio;
        private readonly IRepositorioClientes _repositorioClientes;
        private readonly IUnitOfWork _unitOfWork;

        public ServiciosCtasCtes(IRepositorioCtasCtes repositorio, IRepositorioClientes repositorioClientes, IUnitOfWork unitOfWork)
        {
            _repositorio = repositorio;
            _repositorioClientes = repositorioClientes;
            _unitOfWork = unitOfWork;
        }
        public void Borrar(int id)
        {
            try
            {
                _repositorio.Borrar(id);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EstaRelacionado(CtaCte ctaCte)
        {
            try
            {
                return _repositorio.EstaRelacionado(ctaCte);
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
                return _repositorio.Existe(ctaCte);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetCantidad()
        {
            try
            {
                return _repositorio.GetCantidad();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CtaCte GetCtaCtePorId(int ctaCteId)
        {
            try
            {
                return _repositorio.GetCtaCtePorId(ctaCteId);
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
                return _repositorio.GetCtaCtePorClienteId(ctaCteClienteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<CtaCte> GetCtaCtes()
        {
            try
            {
                return _repositorio.GetCtasCtes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CtaCteListDto> GetCtaCtesPorPagina(int cantidad, int pagina)
        {
            try
            {
                return _repositorio.GetCtasCtesPorPagina(cantidad, pagina);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CtaCteResumen> GetCtaCtes_()
        {
            try
            {
                return _repositorio.GetCtasCtes_();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Guardar(CtaCte ctaCte)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    var ctaCteAGuardar = new CtaCte()
                    {
                        ClienteId = ctaCte.ClienteId,
                        NumeroFactura = ctaCte.NumeroFactura,
                        FechaMovimiento = ctaCte.FechaMovimiento,
                        Debe = ctaCte.Debe,
                        Haber = ctaCte.Haber,
                        Saldo = ctaCte.Saldo,
                        Movimiento = ctaCte.Movimiento
                    };
                    _repositorio.Agregar(ctaCteAGuardar);
                    _unitOfWork.SaveChanges();
                    
                    transaction.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
