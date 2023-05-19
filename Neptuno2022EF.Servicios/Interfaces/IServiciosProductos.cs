using Neptuno2022EF.Entidades.Dtos.Ciudad;
using Neptuno2022EF.Entidades.Dtos.Producto;
using Neptuno2022EF.Entidades.Entidades;
using System;
using System.Collections.Generic;

namespace Neptuno2022EF.Servicios.Interfaces
{
    public interface IServiciosProductos
    {
        void Guardar(Producto producto);
        void Borrar(int id);
        bool EstaRelacionado(Producto producto);
        bool Existe(Producto producto);
        Producto GetProductoPorId(int id);
        List<ProductoListDto> GetProductos();
        List<ProductoListDto> GetProductos(int categoriaId);
        List<ProductoListDto> Filtrar(Func<Producto, bool> predicado);
        List<ProductoListDto> Filtrar(Func<Producto, bool> predicado, int cantidad, int pagina);
        List<ProductoListDto> GetProductosPorPagina(int cantidad, int pagina);
        void ActualizarUnidadesEnPedido(int productoId, int cantidad);
        int GetCantidad(Func<Producto, bool> predicado);
        int GetCantidad();
    }
}
