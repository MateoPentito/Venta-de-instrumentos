namespace Ventas_2024.Data
{
    public class DbContext
    {
        public DbContext(string valor) => Valor = valor;

        public string Valor { get; }

    }
}
