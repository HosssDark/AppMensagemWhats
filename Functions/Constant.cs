using static Functions.Enum;

namespace Functions
{
    public static class Constant
    {
        public readonly static string MsgGravadoSucesso = "Registro gravado com sucesso.";
        public readonly static string MsgAlteradoSucesso = "Registro alterado com sucesso.";
        public readonly static string MsgRegistroNaoEncontrado = "Registro não encontrado.";
        public readonly static string MsgRegistroInativado = "Registro inativado com sucesso.";
        public readonly static string MsgObrigatorio = "Obrigatório.";
        public readonly static string MsgErroTentarExcluir = "Erro ao tentar Excluir o Registro!";
        public readonly static string MsgErroObterRegistro = "Erro ao Obter Registro";
        public readonly static string MsgErroTentarGravarRegistro = "Erro ao tentar Gravar Registro!";
        public readonly static string MsgErroTentarAlterarRegistro = "\"Erro ao tentar Alterar o Registro!\"";

        public static string MsgInvalido(string value)
        {
            return string.Format("{0} Inválido.", value);
        }

        public static string MsgJaCasdastrado(string value)
        {
            return string.Format("{0} já cadastrado.", value);
        }

        public static string MsgConsulta(string value, TypeMessage type)
        {
            switch (type)
            {
                case TypeMessage.Success:
                    return string.Format("{0} consultado com sucesso.", value);
                case TypeMessage.Error:
                    return string.Format("{0} erro ao consultar.", value);
            }

            return "";
        }
    }
}
