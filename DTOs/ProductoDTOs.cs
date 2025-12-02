namespace ProyectoAPI.DTOs
{
    public class ProductoCreacionDTO
    {
        public required string Nombre { get; set; }
        public required string Ubicacion { get; set; }
        public required string Descripcion { get; set; }
        public int StockTotal { get; set; }
        public int StockActual { get; set; }
        public int IdCategoria { get; set; }
        public int IdProveedor { get; set; }
    }

    public class ProductoDTO
    {
        public int IdProd { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int StockTotal { get; set; }
        public int StockActual { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Proveedor { get; set; } = string.Empty;
    }
}
