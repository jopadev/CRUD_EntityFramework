using CRUD_EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_EntityFramework.Controllers
{
    public class LivrosController : Controller
    {
        MeuContexto _meuContexto;

        public LivrosController()
        {
            _meuContexto = new MeuContexto();
        }

        // GET: Livros
        public ActionResult Index()
        {
            return View(_meuContexto.Livros.ToList());
        }

        public ActionResult Adicionar()
        {
            CarregarCondicoesLivros();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(Livro livro)
        {
            if (ModelState.IsValid)
            {
                _meuContexto.Livros.Add(livro);
                _meuContexto.SaveChanges();

                return RedirectToAction("Index");
            }

            CarregarCondicoesLivros();

            return View(livro);
        }

        public ActionResult Editar(int id)
        {
            var oLivro = _meuContexto.Livros.Find(id);

            if (oLivro == null)
                return HttpNotFound();

            CarregarCondicoesLivros();

            return View(oLivro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Livro livro)
        {
            if (ModelState.IsValid)
            {
                _meuContexto.Entry(livro).State = System.Data.Entity.EntityState.Modified;
                _meuContexto.SaveChanges();

                return RedirectToAction("Index");
            }

            CarregarCondicoesLivros();

            return View(livro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var oLivro = _meuContexto.Livros.Find(id);

            if (oLivro != null)
            {
                _meuContexto.Livros.Remove(oLivro);
                _meuContexto.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public void CarregarCondicoesLivros()
        {
            ViewData["CondicoesLivros"] = new SelectList(_meuContexto.CondicaoLivros.ToList(), "CondicaoId", "Descricao");
        }
    }
}