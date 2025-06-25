using Domain;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Repository
{
    public class UsuarioSenhaRepository : RepositoryBase<UsuarioSenha>, IUsuarioSenhaRepository
    {
        public override UsuarioSenha Add(UsuarioSenha Entity)
        {
            Entity.DataCadastro = DateTime.Now;

            return base.Add(Entity);
        }

        public override List<UsuarioSenha> AddAll(List<UsuarioSenha> List)
        {
            foreach (var item in List)
            {
                item.DataCadastro = DateTime.Now;
            }

            return base.AddAll(List);
        }

        public UsuarioSenha VerificationPassword(string Email, string Senha)
        {
            string senha = this.MD5Hash(Senha + Email);

            return this.GetFirst(a => a.Senha == senha);
        }

        public string UserRegister(string Name, string Email, string Password)
        {
            IUsuarioRepository usuarioRepository = new UsuarioRepository();

            Usuario Usuario = new Usuario()
            {
                Nome = Name,
                Email = Email
            };

            usuarioRepository.Add(Usuario);

            UsuarioSenha UserPassword = new UsuarioSenha()
            {
                Senha = this.MD5Hash(Password + Usuario.Email),
                Guid = Guid.NewGuid().ToString(),
                UsuarioId = Usuario.Id
            };

            this.Add(UserPassword);

            return UserPassword.Guid;
        }

        public void ChangePassword(int UsuarioId, string Senha)
        {
            IUsuarioRepository usuarioRepository = new UsuarioRepository();

            var Model = this.Get(a => a.UsuarioId == UsuarioId).FirstOrDefault();
            var Usuario = usuarioRepository.GetById(UsuarioId);

            if (Model != null)
            {
                Model.Senha = this.MD5Hash(Senha + Usuario.Email);

                this.Attach(Model);
            }
        }

        public void ChangePassword(string Guid, string Email, string Senha)
        {
            var Model = this.Get(a => a.Guid == Guid).FirstOrDefault();

            if (Model != null)
            {
                Model.Senha = this.MD5Hash(Senha + Email);
                Model.Guid = null;

                this.Attach(Model);
            }
        }

        public string ChangeGuid(string Email)
        {
            IUsuarioRepository usuarioRepository = new UsuarioRepository();

            var Usuario = usuarioRepository.Get(a => a.Email == Email).FirstOrDefault();
            var Model = this.Get(a => a.UsuarioId == Usuario.Id).FirstOrDefault();

            if (Model != null)
            {
                Model.Guid = Guid.NewGuid().ToString();

                this.Attach(Model);
            }

            return Model.Guid;
        }

        public string MD5Hash(string Password)
        {
            string Hash = "!@#$%¨&*123456789?" + Password;

            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(Hash);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}