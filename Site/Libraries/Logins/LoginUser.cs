using Domain;
using Newtonsoft.Json;

namespace Site.Libraries
{
    public class LoginUser
    {
        #region private props

        private readonly string Key = "Login.User";
        private readonly Session _Session;

        #endregion

        #region ctor

        public LoginUser(Session session)
        {
            _Session = session;
        }

        #endregion

        #region methods

        public bool LoggedUser()
        {
            return _Session.Exist(Key);
        }

        public void SetUser(Usuario User)
        {
            string Value = JsonConvert.SerializeObject(User);

            _Session.Register(Key, Value);
        }

        public Usuario GetUser()
        {
            return JsonConvert.DeserializeObject<Usuario>(_Session.Get(Key));
        }

        public void Exit()
        {
            _Session.RemoveAll();
        }

        #endregion
    }
}