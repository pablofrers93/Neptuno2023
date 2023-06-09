﻿using Neptuno2022EF.Entidades.Dtos.Ciudad;
using Neptuno2022EF.Entidades.Dtos.Producto;
using Neptuno2022EF.Entidades.Entidades;
using System;
using System.Collections.Generic;

namespace Neptuno2022EF.Datos.Interfaces
{
    public interface IRepositorioProductos
    {
        void Agregar(Producto producto);
        void Borrar(int id);
        void Editar(Producto producto);
        bool EstaRelacionado(Producto producto);
        bool Existe(Producto producto);
        Producto GetProductoPorId(int id);
        List<ProductoListDto> GetProductos();
        List<ProductoListDto> GetProductos(int categoriaId);
        List<ProductoListDto> Filtrar(Func<Producto, bool> predicado);
        List<ProductoListDto> Filtrar(Func<Producto, bool> predicado, int cantidad, int pagina);
        List<ProductoListDto> GetProductosPorPagina(int cantidad, int pagina);
        void ActualizarStock(int productoId, int cantidad);
        int GetCantidad(Func<Producto, bool> predicado);
        int GetCantidad();
    }
}
