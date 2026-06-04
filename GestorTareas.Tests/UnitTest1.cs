using GestorTareas.Application;
using GestorTareas.Domain;
using Xunit;

namespace GestorTareas.Tests;

public class GestorTareasServiceTests
{
    [Fact]
    public void AgregarTarea_GuardarLaTarea()
    {
        var repo = new GestorTareas();

        var service = new GestorTareasService(repo);

        var tarea = service.Crear("Test", null, 1);

        Assert.Single(repo.ObtenerTodas());
        Assert.Equal("Test", tarea.Titulo);
    }

    [Fact]
    public void BuscarPorId_DevuelveLaTarea()
    {
        var repo = new GestorTareas();

        repo.Agregar(new Tarea
        {
            Id = 1,
            Titulo = "Tarea Test",
            Completada = false
        });

        var service = new GestorTareasService(repo);

        var resultado = service.ObtenerPorId(1);

        Assert.NotNull(resultado);
        Assert.Equal("Tarea Test", resultado!.Titulo);
    }
    [Fact]
    public void ObtenerTodas_DevuelveListaConElementos()
    {
        var repo = new GestorTareas();

        repo.Agregar(new Tarea { Id = 1, Titulo = "Tarea 1" });
        repo.Agregar(new Tarea { Id = 2, Titulo = "Tarea 2" });

        var service = new GestorTareasService(repo);

        var resultado = service.ObtenerTodas();

        Assert.Equal(2, resultado.Count);
    }
    [Fact]
    public void ObtenerTodas_SinTareas_DevuelveListaVacia()
    {
        var repo = new GestorTareas();
        var service = new GestorTareasService(repo);

        var resultado = service.ObtenerTodas();

        Assert.Empty(resultado);
    }
    [Fact]
    public void Crear_AsignaTituloCorrectamente()
    {
        var repo = new GestorTareas();
        var service = new GestorTareasService(repo);

        var tarea = service.Crear("Mi tarea", null, 1);

        Assert.Equal("Mi tarea", tarea.Titulo);
    }
    [Fact]
    public void Crear_TareaNoEstaCompletadaPorDefecto()
    {
        var repo = new GestorTareas();
        var service = new GestorTareasService(repo);

        var tarea = service.Crear("Prueba", null, 1);

        Assert.False(tarea.Completada);
    }
    [Fact]
    public void Crear_GuardaVariasTareas()
    {
        var repo = new GestorTareas();
        var service = new GestorTareasService(repo);

        service.Crear("Tarea 1", null, 1);
        service.Crear("Tarea 2", null, 1);

        Assert.Equal(2, repo.ObtenerTodas().Count);
    }
    [Fact]
    public void BuscarPorId_Inexistente_DevuelveNull()
    {
        var repo = new GestorTareas();
        var service = new GestorTareasService(repo);

        var resultado = service.ObtenerPorId(999);

        Assert.Null(resultado);
    }
    [Fact]
    public void Crear_GuardaUsuarioIdCorrectamente()
    {
        var repo = new GestorTareas();
        var service = new GestorTareasService(repo);

        var tarea = service.Crear("Prueba", null, 5);

        Assert.Equal(5, tarea.UsuarioId);
    }
    [Fact]
    public void Crear_ConFechaLimite_GuardaFecha()
    {
        var repo = new GestorTareas();
        var service = new GestorTareasService(repo);

        var fecha = DateTime.Now.AddDays(7);

        var tarea = service.Crear("Prueba", fecha, 1);

        Assert.Equal(fecha, tarea.FechaLimite);
    }


    [Fact]
    public void AdminUsuario_DevuelveRolAdministrador()
    {
        Usuario usuario = new AdminUsuario();

        Assert.Equal("Administrador", usuario.ObtenerRol());
    }
}