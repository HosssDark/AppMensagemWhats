using System;

namespace Site.Areas.Admin.Controllers.ViewModel
{
    public class DashCardsViewModel
    {
        public int MensagemEnviadaTotal { get; set; }
        public int MensagemNaoEnviadaTotal { get; set; }
        public int MensagemTotal { get; set; }
        public int UsuarioTotal { get; set; }
        public int DiscentesTotal { get; set; }
        public int CursosTotal { get; set; }
    }

    public class DashProjetosViewModel
    {
        public int ProjetoId { get; set; }
        public string Descricao { get; set; }
        public int DocenteId { get; set; }
        public string Docente { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public string Classe { get; set; }
        public string Cor { get; set; }
        public double CargaHoraria { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public int Progresso { get; set; }
    }

    public class DashTurmaViewModel
    {
        public int TurmaId { get; set; }
        public string Descricao { get; set; }
    }

    public class DashDiscentesViewModel
    {
        public int DiscenteId { get; set; }
        public DateTime DataCadastro { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
    }

    public class DashDocentesViewModel
    {
        public int DocenteId { get; set; }
        public DateTime DataCadastro { get; set; }
        public int AreaAtuacaoId { get; set; }
        public string AreaAtuacao { get; set; }
        public int TituloId { get; set; }
        public string Titulo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string EmailLattes { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public double CargaHoraria { get; set; }
        public DateTime DataPosse { get; set; }
        public int TipoId { get; set; }
        public string Tipo { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
    }
}