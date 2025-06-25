using static Functions.Enum;

namespace Site.Models
{
    public class ResultViewModel
    {
        public ResultViewModel()
        {

        }

        public ResultViewModel(TypeResult Result)
        {
            this.Result = Result == TypeResult.Ok ? true : false;
        }

        public ResultViewModel(TypeResult Result, string Return = null)
        {
            this.Result = Result == TypeResult.Ok ? true : false;
            this.Return = Return ?? "";
        }

        public ResultViewModel(TypeResult Result, TypeMessage Type, string Message = null, string Return = null)
        {
            this.Result = Result == TypeResult.Ok ? true : false;
            this.Message = Message ?? "";
            this.Type = Type;
            this.Return = Return ?? "";
        }

        public bool Result { get; set; }
        public TypeMessage Type { get; set; }
        public string Message { get; set; }
        public string Return { get; set; }
    }
}
