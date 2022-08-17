namespace MovieRestApiWithEF
{
    public class ConfigVars
    {
        private readonly IConfiguration _configuration;

        public ConfigVars(IConfiguration configuration)
        {
            _configuration = configuration;
            JWTSecretKey = configuration["JWT:Secret"];
        }

        public string JWTSecretKey { get; }
    }
}
