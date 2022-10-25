namespace dotnetRoutes.Comuns
{
    public class ClasseBase
    {
        private readonly IConfiguration _configuration;

        public ClasseBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected string GetMsgField(string nmField)
        {
            return String.Format(
                _configuration.GetSection("MsgRequiredField").Value, nmField
            );
        }        
    }
}
