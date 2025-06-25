using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using System.Collections.Generic;
using System.Linq;

namespace Site
{
    public class HelperLista
    {
        public static List<SelectListItem> DiaSemana(string Dia = "")
        {
            string[] DiaSemana = new string[5];

            DiaSemana[0] = "Segunda-Feira";
            DiaSemana[1] = "Terça-Feira";
            DiaSemana[2] = "Quarta-Feira";
            DiaSemana[3] = "Quinta-Feira";
            DiaSemana[4] = "Sexta-Feira";

            var Lista = new List<SelectListItem>();

            foreach (var item in DiaSemana)
            {
                Lista.Add(new SelectListItem
                {
                    Text = item,
                    Value = item,
                    Selected = (Dia != "" && item == Dia)
                });
            }

            return Lista;
        }

        public static List<SelectListItem> Periodo(string Periodo = "")
        {
            string[] Periodos = new string[2];

            Periodos[0] = "Matutino";
            Periodos[1] = "Noturno";

            var Lista = new List<SelectListItem>();

            foreach (var item in Periodos)
            {
                Lista.Add(new SelectListItem
                {
                    Text = item,
                    Value = item,
                    Selected = (Periodo != "" && item == Periodo)
                });
            }

            return Lista.OrderBy(a => a.Text).ToList();
        }

        public static List<SelectListItem> DiscenteStatus(int? StatusId = null)
        {
            IStatusRepository staRep = new StatusRepository();

            var Status = staRep.GetAll();

            var Lista = new List<SelectListItem>();

            foreach (var item in Status)
            {
                Lista.Add(new SelectListItem
                {
                    Text = item.Descricao,
                    Value = item.Id.ToString(),
                    Selected = (StatusId != null && item.Id == StatusId)
                });
            }

            return Lista;
        }

        public static List<SelectListItem> Status(int? StatusId = null)
        {
            IStatusRepository staRep = new StatusRepository();

            var Status = staRep.GetAll();

            var Lista = new List<SelectListItem>();

            foreach (var item in Status)
            {
                if (item.Id != 2)
                {
                    Lista.Add(new SelectListItem
                    {
                        Text = item.Descricao,
                        Value = item.Id.ToString(),
                        Selected = (StatusId != null && item.Id == StatusId)
                    });
                }
            }

            return Lista;
        }

        public static List<SelectListItem> Discentes(int? DiscenteId = null, int StatusId = 1)
        {
            IDiscenteRepository dicRep = new DiscenteRepository();

            var Discentes = dicRep.GetAll();

            var Lista = new List<SelectListItem>();

            foreach (var item in Discentes)
            {
                Lista.Add(new SelectListItem
                {
                    Text = item.Nome,
                    Selected = (DiscenteId != null && item.Id == DiscenteId)
                });
            }

            return Lista.OrderBy(a => a.Text).ToList();
        }

        public static List<SelectListItem> Curso(int? curso = null)
        {
            ICursoRepository cursoRepository = new CursoRepository();

            var Cursos = cursoRepository.GetAll();

            var Lista = new List<SelectListItem>();

            foreach (var item in Cursos)
            {
                Lista.Add(new SelectListItem
                {
                    Text = item.Descricao,
                    Value = item.Id.ToString(),
                    Selected = item.Id == curso
                });
            }

            return Lista.OrderBy(a => a.Text).ToList();
        }

        public static List<SelectListItem> TipoUsuario(string tipo = "")
        {
            Dictionary<string, string> lista = new Dictionary<string, string>();

            lista.Add("Admin", "Administrador");
            lista.Add("Estagiario", "Estagiário");

            var Lista = new List<SelectListItem>();

            foreach (var item in lista)
            {
                Lista.Add(new SelectListItem
                {
                    Text = item.Value,
                    Value = item.Key.ToString(),
                    Selected = (tipo != "" && item.Key == tipo)
                });
            }

            return Lista;
        }


        public static List<SelectListItem> SimOuNao(bool? simOuNao = null)
        {
            Dictionary<bool, string> lista = new Dictionary<bool, string>();

            lista.Add(true, "Sim");
            lista.Add(false, "Não");

            var Lista = new List<SelectListItem>();

            foreach (var item in lista)
            {
                Lista.Add(new SelectListItem
                {
                    Text = item.Value,
                    Value = item.Key.ToString(),
                    Selected = (item.Key == simOuNao)
                });
            }

            return Lista;
        }
    }
}