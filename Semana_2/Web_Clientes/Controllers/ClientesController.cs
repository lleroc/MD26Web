using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Clientes.Models;

namespace Web_Clientes.Controllers
{
    public class ClientesController : Controller
    {
        //simular una base de datos
       private static List<ClienteModel> _lista_Clientes = new List<ClienteModel>() {
            new ClienteModel{
                id = 1,
                Nombres_Cliente="Luis Antonio",
                Apellidos = "Llerena Ocaña",
                Direccion = "Ambato",
                Telefono = "0987654321",
                Correo = "lleroc1@gmail.com"
            },
            new ClienteModel{
                id = 2,
                Nombres_Cliente="Otro Luis",
                Apellidos = "Otro Llerena",
                Direccion = "Quero",
                Telefono = "0999999999",
                Correo = "otro@gmail.com"
            }
        };
        // GET: ClientesController
        public ActionResult Index()
        {
            return View(_lista_Clientes);
        }

        // GET: ClientesController/Details/5
        public ActionResult Details(int id)
        {
            var cliente = _lista_Clientes.FirstOrDefault(c => c.id == id);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        // GET: ClientesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteModel cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.id = _lista_Clientes.Count > 0
                    ? _lista_Clientes.Max(c => c.id) + 1 : 1;
                /*if (_lista_Clientes.Count > 0)
                {
                    cliente.id = _lista_Clientes.Count() + 1;
                }
                else {
                    cliente.id = 1;
                }*/
                _lista_Clientes.Add(cliente);
                return RedirectToAction(nameof(Index));
            }
            else { 
                return View(cliente);
            }
        }

        // GET: ClientesController/Edit/5
        public ActionResult Edit(int id)
        {
            var cliente = _lista_Clientes.FirstOrDefault(c => c.id == id);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        // POST: ClientesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ClienteModel cliente)
        {
            if(id != cliente.id) return BadRequest("No se enconto el cliente");
            
                var clienteExistente = _lista_Clientes.FirstOrDefault(c => c.id == id);
                if (cliente == null) return NotFound();

                clienteExistente.id = cliente.id;
                clienteExistente.Nombres_Cliente = cliente.Nombres_Cliente;
                clienteExistente.Apellidos = cliente.Apellidos;
                clienteExistente.Direccion = cliente.Direccion;
                clienteExistente.Telefono = cliente.Telefono;
                clienteExistente.Correo = cliente.Correo;

                return RedirectToAction(nameof(Index));
           
          
        }

        // GET: ClientesController/Delete/5
        public ActionResult Delete(int id)
        {
            var cliente = _lista_Clientes.FirstOrDefault(c => c.id == id);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        // POST: ClientesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var cliente = _lista_Clientes.FirstOrDefault(c => c.id == id);
            if (cliente == null) return NotFound();
            _lista_Clientes.Remove(cliente);
            return RedirectToAction(nameof(Index));

        }
    }
}
